#region Copyright (C) 2007-2015 Team MediaPortal

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
using MediaPortal.Common.General;
using MediaPortal.Common.MediaManagement;
using MediaPortal.Common.MediaManagement.DefaultItemAspects;
using MediaPortal.UI.SkinEngine.Controls.Visuals;

namespace MediaPortal.UiComponents.Media.Models.AspectWrappers
{
/// <summary>
/// EpisodeAspectWrapper wraps the contents of <see cref="EpisodeAspect"/> into properties that can be bound from xaml controls.
/// Note: this code was automatically created by the MediaItemAspectModelBuilder helper tool under Resources folder.
/// </summary>
public class EpisodeAspectWrapper: Control
{
#region Constants

public static readonly ICollection<string> EMPTY_STRING_COLLECTION = new List<string>().AsReadOnly();

#endregion

#region Fields

protected AbstractProperty _seriesNameProperty;
protected AbstractProperty _seasonProperty;
protected AbstractProperty _seriesSeasonNameProperty;
protected AbstractProperty _episodeProperty;
protected AbstractProperty _dvdEpisodeProperty;
protected AbstractProperty _episodeNameProperty;
protected AbstractProperty _firstAiredProperty;
protected AbstractProperty _totalRatingProperty;
protected AbstractProperty _ratingCountProperty;
protected AbstractProperty _storyPlotProperty;
protected AbstractProperty _actorsProperty;
protected AbstractProperty _directorsProperty;
protected AbstractProperty _writersProperty;
protected AbstractProperty _charactersProperty;
protected AbstractProperty _genresProperty;
protected AbstractProperty _mediaItemProperty;

#endregion

#region Properties

public AbstractProperty SeriesNameProperty
{
  get{ return _seriesNameProperty; }
}

public string SeriesName
{
  get { return (string) _seriesNameProperty.GetValue(); }
  set { _seriesNameProperty.SetValue(value); }
}

public AbstractProperty SeasonProperty
{
  get{ return _seasonProperty; }
}

public int? Season
{
  get { return (int?) _seasonProperty.GetValue(); }
  set { _seasonProperty.SetValue(value); }
}

public AbstractProperty SeriesSeasonNameProperty
{
  get{ return _seriesSeasonNameProperty; }
}

public string SeriesSeasonName
{
  get { return (string) _seriesSeasonNameProperty.GetValue(); }
  set { _seriesSeasonNameProperty.SetValue(value); }
}

public AbstractProperty EpisodeProperty
{
  get{ return _episodeProperty; }
}

public IEnumerable<int> Episode
{
  get { return (IEnumerable<int>) _episodeProperty.GetValue(); }
  set { _episodeProperty.SetValue(value); }
}

public AbstractProperty DvdEpisodeProperty
{
  get{ return _dvdEpisodeProperty; }
}

public IEnumerable<double> DvdEpisode
{
  get { return (IEnumerable<double>) _dvdEpisodeProperty.GetValue(); }
  set { _dvdEpisodeProperty.SetValue(value); }
}

public AbstractProperty EpisodeNameProperty
{
  get{ return _episodeNameProperty; }
}

public string EpisodeName
{
  get { return (string) _episodeNameProperty.GetValue(); }
  set { _episodeNameProperty.SetValue(value); }
}

public AbstractProperty FirstAiredProperty
{
  get{ return _firstAiredProperty; }
}

public DateTime? FirstAired
{
  get { return (DateTime?) _firstAiredProperty.GetValue(); }
  set { _firstAiredProperty.SetValue(value); }
}

public AbstractProperty TotalRatingProperty
{
  get{ return _totalRatingProperty; }
}

public double? TotalRating
{
  get { return (double?) _totalRatingProperty.GetValue(); }
  set { _totalRatingProperty.SetValue(value); }
}

public AbstractProperty RatingCountProperty
{
  get{ return _ratingCountProperty; }
}

public int? RatingCount
{
  get { return (int?) _ratingCountProperty.GetValue(); }
  set { _ratingCountProperty.SetValue(value); }
}

public AbstractProperty StoryPlotProperty
{
  get{ return _storyPlotProperty; }
}

public string StoryPlot
{
  get { return (string) _storyPlotProperty.GetValue(); }
  set { _storyPlotProperty.SetValue(value); }
}

public AbstractProperty ActorsProperty
{
  get{ return _actorsProperty; }
}

public IEnumerable<string> Actors
{
  get { return (IEnumerable<string>) _actorsProperty.GetValue(); }
  set { _actorsProperty.SetValue(value); }
}

public AbstractProperty DirectorsProperty
{
  get{ return _directorsProperty; }
}

public IEnumerable<string> Directors
{
  get { return (IEnumerable<string>) _directorsProperty.GetValue(); }
  set { _directorsProperty.SetValue(value); }
}

public AbstractProperty WritersProperty
{
  get{ return _writersProperty; }
}

public IEnumerable<string> Writers
{
  get { return (IEnumerable<string>) _writersProperty.GetValue(); }
  set { _writersProperty.SetValue(value); }
}

public AbstractProperty CharactersProperty
{
  get{ return _charactersProperty; }
}

public IEnumerable<string> Characters
{
  get { return (IEnumerable<string>) _charactersProperty.GetValue(); }
  set { _charactersProperty.SetValue(value); }
}

public AbstractProperty GenresProperty
{
  get{ return _genresProperty; }
}

public IEnumerable<string> Genres
{
  get { return (IEnumerable<string>) _genresProperty.GetValue(); }
  set { _genresProperty.SetValue(value); }
}

public AbstractProperty MediaItemProperty
{
  get{ return _mediaItemProperty; }
}

public MediaItem MediaItem
{
  get { return (MediaItem) _mediaItemProperty.GetValue(); }
  set { _mediaItemProperty.SetValue(value); }
}

#endregion

#region Constructor

public EpisodeAspectWrapper()
{
  _seriesNameProperty = new SProperty(typeof(string));
  _seasonProperty = new SProperty(typeof(int?));
  _seriesSeasonNameProperty = new SProperty(typeof(string));
  _episodeProperty = new SProperty(typeof(IEnumerable<int>));
  _dvdEpisodeProperty = new SProperty(typeof(IEnumerable<double>));
  _episodeNameProperty = new SProperty(typeof(string));
  _firstAiredProperty = new SProperty(typeof(DateTime?));
  _totalRatingProperty = new SProperty(typeof(double?));
  _ratingCountProperty = new SProperty(typeof(int?));
  _storyPlotProperty = new SProperty(typeof(string));
  _actorsProperty = new SProperty(typeof(IEnumerable<string>));
  _directorsProperty = new SProperty(typeof(IEnumerable<string>));
  _writersProperty = new SProperty(typeof(IEnumerable<string>));
  _charactersProperty = new SProperty(typeof(IEnumerable<string>));
  _genresProperty = new SProperty(typeof(IEnumerable<string>));
  _mediaItemProperty = new SProperty(typeof(MediaItem));
  _mediaItemProperty.Attach(MediaItemChanged);
}

#endregion

#region Members

private void MediaItemChanged(AbstractProperty property, object oldvalue)
{
  Init(MediaItem);
}

public void Init(MediaItem mediaItem)
{
  SingleMediaItemAspect aspect;
  if (mediaItem == null ||!MediaItemAspect.TryGetAspect(mediaItem.Aspects, EpisodeAspect.Metadata, out aspect))
  {
     SetEmpty();
     return;
  }

  SeriesName = (string) aspect[EpisodeAspect.ATTR_SERIES_NAME];
  Season = (int?) aspect[EpisodeAspect.ATTR_SEASON];
  SeriesSeasonName = (string) aspect[EpisodeAspect.ATTR_SERIES_SEASON];
  Episode = (IEnumerable<int>) aspect[EpisodeAspect.ATTR_EPISODE];
  DvdEpisode = (IEnumerable<double>) aspect[EpisodeAspect.ATTR_DVDEPISODE];
  EpisodeName = (string) aspect[EpisodeAspect.ATTR_EPISODE_NAME];
  FirstAired = (DateTime?) aspect[EpisodeAspect.ATTR_FIRSTAIRED];
  TotalRating = (double?) aspect[EpisodeAspect.ATTR_TOTAL_RATING];
  RatingCount = (int?) aspect[EpisodeAspect.ATTR_RATING_COUNT];
  Actors = (IEnumerable<string>) aspect[EpisodeAspect.ATTR_ACTORS] ?? EMPTY_STRING_COLLECTION;
  Directors = (IEnumerable<string>) aspect[EpisodeAspect.ATTR_DIRECTORS] ?? EMPTY_STRING_COLLECTION;
  Writers = (IEnumerable<string>) aspect[EpisodeAspect.ATTR_WRITERS] ?? EMPTY_STRING_COLLECTION;
  Characters = (IEnumerable<string>) aspect[EpisodeAspect.ATTR_CHARACTERS] ?? EMPTY_STRING_COLLECTION;
  Genres = (IEnumerable<string>) aspect[EpisodeAspect.ATTR_GENRES] ?? EMPTY_STRING_COLLECTION;
  StoryPlot = (string) aspect[EpisodeAspect.ATTR_STORYPLOT];
}

public void SetEmpty()
{
  SeriesName = null;
  Season = null;
  SeriesSeasonName = null;
  Episode = new List<Int32>();
  DvdEpisode = new List<Double>();
  EpisodeName = null;
  FirstAired = null;
  TotalRating = null;
  RatingCount = null;
  Actors = EMPTY_STRING_COLLECTION;
  Directors = EMPTY_STRING_COLLECTION;
  Writers = EMPTY_STRING_COLLECTION;
  Characters = EMPTY_STRING_COLLECTION;
  Genres = EMPTY_STRING_COLLECTION;
  StoryPlot = null;
}


#endregion

}

}
