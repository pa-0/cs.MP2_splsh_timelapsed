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

using MediaPortal.UI.Presentation.DataObjects;
using MediaPortal.UI.Presentation.Models;
using MediaPortal.UiComponents.Media.General;
using MediaPortal.UiComponents.Media.Models.ScreenData;
using System.Collections.Generic;

namespace MediaPortal.UiComponents.Nereus.Models.HomeContent
{
  public class ImageHomeContent : AbstractHomeContent
  {
    public ImageHomeContent()
    {
      _availableMediaLists.Add(new LatestImageList());
      _availableMediaLists.Add(new FavoriteImageList());
      _availableMediaLists.Add(new UnplayedImageList());

      _shortcutLists.Add(new MediaShortcutListWrapper(new List<ListItem>
      {
        new ImageYearShortcut(),
        new ImageLocationShortcut(),
        new ImageSystemShortcut(),
        new ImageSizeShortcut(),
        new ImageSearchShortcut()
      }));
    }

    protected override IContentListModel GetContentListModel()
    {
      return GetMediaListModel();
    }
  }

  public class LatestImageList : MediaListItemsListWrapper
  {
    public LatestImageList()
      : base("LatestImages", "[Nereus.Home.LatestAdded]")
    { }
  }

  public class FavoriteImageList : MediaListItemsListWrapper
  {
    public FavoriteImageList()
      : base("FavoriteImages", "[Nereus.Home.Favorites]")
    { }
  }

  public class UnplayedImageList : MediaListItemsListWrapper
  {
    public UnplayedImageList()
      : base("UnplayedImages", "[Nereus.Home.Unplayed]")
    { }
  }

  public class ImageYearShortcut : YearShortcutItem
  {
    public ImageYearShortcut()
      : base(Consts.WF_STATE_ID_IMAGES_NAVIGATION_ROOT, typeof(ImagesFilterByYearScreenData))
    { }
  }

  public class ImageLocationShortcut : MediaScreenShortcutItem
  {
    public ImageLocationShortcut()
      : base(Consts.WF_STATE_ID_IMAGES_NAVIGATION_ROOT, typeof(ImagesFilterByCityScreenData))
    { }
  }

  public class ImageSystemShortcut : MediaScreenShortcutItem
  {
    public ImageSystemShortcut()
      : base(Consts.WF_STATE_ID_IMAGES_NAVIGATION_ROOT, typeof(ImagesFilterBySystemScreenData))
    { }
  }

  public class ImageSizeShortcut : MediaScreenShortcutItem
  {
    public ImageSizeShortcut()
      : base(Consts.WF_STATE_ID_IMAGES_NAVIGATION_ROOT, typeof(ImagesFilterBySizeScreenData))
    { }
  }

  public class ImageSearchShortcut : SearchShortcutItem
  {
    public ImageSearchShortcut()
      : base(Consts.WF_STATE_ID_IMAGES_NAVIGATION_ROOT, typeof(ImagesSimpleSearchScreenData))
    { }
  }
}
