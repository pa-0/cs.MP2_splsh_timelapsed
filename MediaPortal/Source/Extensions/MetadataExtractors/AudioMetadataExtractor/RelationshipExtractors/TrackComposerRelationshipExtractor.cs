﻿#region Copyright (C) 2007-2015 Team MediaPortal

/*
    Copyright (C) 2007-2015 Team MediaPortal
    http://www.team-mediaportal.com

    This file is part of MediaPortal 2

    MediaPortal 2 is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    MediaPortal 2 is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with MediaPortal 2. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

using System;
using System.Collections.Generic;
using MediaPortal.Common;
using MediaPortal.Common.Logging;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;
using MediaPortal.Common.MediaManagement.Helpers;
using System.Linq;
using MediaPortal.Common.General;
using MediaPortal.Extensions.OnlineLibraries;
using MediaPortal.Common.Settings;
using MediaPortal.Extensions.MetadataExtractors.AudioMetadataExtractor.Settings;

namespace MediaPortal.Extensions.MetadataExtractors.AudioMetadataExtractor
{
  class TrackComposerRelationshipExtractor : IRelationshipRoleExtractor, IAudioRelationshipExtractor
  {
    private static readonly Guid[] ROLE_ASPECTS = { AudioAspect.ASPECT_ID };
    private static readonly Guid[] LINKED_ROLE_ASPECTS = { PersonAspect.ASPECT_ID };
    private CheckedItemCache<TrackInfo> _checkCache = new CheckedItemCache<TrackInfo>(AudioMetadataExtractor.MINIMUM_HOUR_AGE_BEFORE_UPDATE);
    private bool _includeDetails = true;

    public TrackComposerRelationshipExtractor()
    {
      _includeDetails = ServiceRegistration.Get<ISettingsManager>().Load<AudioMetadataExtractorSettings>().IncludeComposerDetails;
    }

    public bool BuildRelationship
    {
      get { return true; }
    }

    public Guid Role
    {
      get { return AudioAspect.ROLE_TRACK; }
    }

    public Guid[] RoleAspects
    {
      get { return ROLE_ASPECTS; }
    }

    public Guid LinkedRole
    {
      get { return PersonAspect.ROLE_COMPOSER; }
    }

    public Guid[] LinkedRoleAspects
    {
      get { return LINKED_ROLE_ASPECTS; }
    }

    public bool TryExtractRelationships(IDictionary<Guid, IList<MediaItemAspect>> aspects, out ICollection<IDictionary<Guid, IList<MediaItemAspect>>> extractedLinkedAspects, bool forceQuickMode)
    {
      extractedLinkedAspects = null;

      if (!_includeDetails)
        return false;

      if (forceQuickMode)
        return false;

      if (BaseInfo.IsVirtualResource(aspects))
        return false;

      TrackInfo trackInfo = new TrackInfo();
      if (!trackInfo.FromMetadata(aspects))
        return false;

      if (_checkCache.IsItemChecked(trackInfo))
        return false;

      OnlineMatcherService.UpdateTrackPersons(trackInfo, PersonAspect.OCCUPATION_COMPOSER, forceQuickMode);

      if (trackInfo.Composers.Count == 0)
        return false;

      bool relationshipFound = false;
      IList<MultipleMediaItemAspect> relationships;
      if (MediaItemAspect.TryGetAspects(aspects, RelationshipAspect.Metadata, out relationships))
      {
        foreach (MultipleMediaItemAspect relationship in relationships)
        {
          if (relationship.GetAttributeValue<Guid>(RelationshipAspect.ATTR_LINKED_ROLE) == LinkedRole ||
            relationship.GetAttributeValue<Guid>(RelationshipAspect.ATTR_ROLE) == LinkedRole)
          {
            relationshipFound = true;
            break;
          }
        }
      }

      if (!relationshipFound)
        trackInfo.HasChanged = true; //Force save if no relationship exists

      if (!trackInfo.HasChanged && !forceQuickMode)
        return false;

      extractedLinkedAspects = new List<IDictionary<Guid, IList<MediaItemAspect>>>();

      foreach (PersonInfo person in trackInfo.Composers)
      {
        IDictionary<Guid, IList<MediaItemAspect>> personAspects = new Dictionary<Guid, IList<MediaItemAspect>>();
        person.SetMetadata(personAspects);

        if (personAspects.ContainsKey(ExternalIdentifierAspect.ASPECT_ID))
          extractedLinkedAspects.Add(personAspects);
      }
      return extractedLinkedAspects.Count > 0;
    }

    public bool TryMatch(IDictionary<Guid, IList<MediaItemAspect>> extractedAspects, IDictionary<Guid, IList<MediaItemAspect>> existingAspects)
    {
      if (!existingAspects.ContainsKey(PersonAspect.ASPECT_ID))
        return false;

      PersonInfo linkedPerson = new PersonInfo();
      if (!linkedPerson.FromMetadata(extractedAspects))
        return false;

      PersonInfo existingPerson = new PersonInfo();
      if (!existingPerson.FromMetadata(existingAspects))
        return false;

      return linkedPerson.Equals(existingPerson);
    }

    public bool TryGetRelationshipIndex(IDictionary<Guid, IList<MediaItemAspect>> aspects, IDictionary<Guid, IList<MediaItemAspect>> linkedAspects, out int index)
    {
      index = -1;

      SingleMediaItemAspect linkedAspect;
      if (!MediaItemAspect.TryGetAspect(linkedAspects, PersonAspect.Metadata, out linkedAspect))
        return false;

      string name = linkedAspect.GetAttributeValue<string>(PersonAspect.ATTR_PERSON_NAME);

      SingleMediaItemAspect aspect;
      if (!MediaItemAspect.TryGetAspect(aspects, AudioAspect.Metadata, out aspect))
        return false;

      IEnumerable<object> persons = aspect.GetCollectionAttribute<object>(AudioAspect.ATTR_COMPOSERS);
      List<string> nameList = new List<string>(persons.Cast<string>());

      index = nameList.IndexOf(name);
      return index >= 0;
    }

    public void ClearCache()
    {
      _checkCache.ClearCache();
    }

    internal static ILogger Logger
    {
      get { return ServiceRegistration.Get<ILogger>(); }
    }
  }
}
