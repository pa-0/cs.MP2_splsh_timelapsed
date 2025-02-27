#region Copyright (C) 2007-2021 Team MediaPortal

/*
    Copyright (C) 2007-2021 Team MediaPortal
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

using MediaPortal.Common;
using MediaPortal.Common.Commands;
using MediaPortal.UI.Presentation.DataObjects;
using MediaPortal.UI.Presentation.Models;
using MediaPortal.UI.Presentation.Workflow;
using System;
using System.Collections.Generic;

namespace MediaPortal.UiComponents.Nereus.Models.HomeContent
{
  public class TVHomeContent : AbstractHomeContent
  {
    public TVHomeContent()
    {
      _availableMediaLists.Add(new LastPlayTVList());
      _availableMediaLists.Add(new FavoriteTVList());
      _availableMediaLists.Add(new CurrentTVList());
      _availableMediaLists.Add(new CurrentSchedulesList());

      _shortcutLists.Add(new MediaShortcutListWrapper(new List<ListItem>
      {
        new LiveTVShortcut(),
        new EPGShortcut(),
        new SchedulesShortcut(),
        new RecordingsShortcut(),
        new TVSearchShortcut()
      }));
    }

    protected override IContentListModel GetContentListModel()
    {
      return GetMediaListModel();
    }
  }

  public class LastPlayTVList : MediaListItemsListWrapper
  {
    public LastPlayTVList()
      : base("LastPlayTV", "[Nereus.Home.LatestPlayed]")
    { }
  }

  public class FavoriteTVList : MediaListItemsListWrapper
  {
    public FavoriteTVList()
      : base("FavoriteTV", "[Nereus.Home.Favorites]")
    { }
  }

  public class CurrentTVList : MediaListItemsListWrapper
  {
    public CurrentTVList()
      : base("CurrentTVPrograms", "[Nereus.Home.CurrentPrograms]")
    { }
  }

  public class CurrentSchedulesList : MediaListItemsListWrapper
  {
    public CurrentSchedulesList()
      : base("CurrentSchedules", "[Nereus.Home.CurrentSchedules]")
    { }
  }

  public class LiveTVShortcut : WorkflowNavigationShortcutItem
  {
    public LiveTVShortcut() : base(new Guid("C7646667-5E63-48c7-A490-A58AC9518CFA")) { }
  }

  public class EPGShortcut : WorkflowNavigationShortcutItem
  {
    public EPGShortcut() : base(new Guid("7323BEB9-F7B0-48c8-80FF-8B59A4DB5385")) { }
  }

  public class SchedulesShortcut : WorkflowNavigationShortcutItem
  {
    public SchedulesShortcut() : base(new Guid("88842E97-2EF9-4658-AD35-8D74E3C689A4")) { }
  }

  public class RecordingsShortcut : WorkflowNavigationShortcutItem
  {
    public RecordingsShortcut() : base(new Guid("9D5B01A7-035F-46CF-8246-3C158C6CA960")) { }
  }

  public class TVSearchShortcut : SearchShortcutItem
  {
    public TVSearchShortcut()
    {
      Command = new MethodDelegateCommand(() =>
      {
        var wm = ServiceRegistration.Get<IWorkflowManager>();
        wm.NavigatePush(new Guid("CB5D4851-27D2-4222-B6A0-703EDC2071B5"));
      });
    }
  }
}
