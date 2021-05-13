#region Copyright (C) 2007-2020 Team MediaPortal

/*
    Copyright (C) 2007-2020 Team MediaPortal
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
using MediaPortal.UI.Presentation.Workflow;
using System;

namespace MediaPortal.Plugins.SlimTv.Client.MediaLists
{
  public class SlimTvSchedulesMediaListProvider : BaseSchedulesMediaListProvider
  {
    public SlimTvSchedulesMediaListProvider()
    {
      _showSchedules = ShowSchedules;
    }

    public static void ShowSchedules()
    {
      Guid WF_STATE_ID_SCHEDULE_LIST = new Guid("88842E97-2EF9-4658-AD35-8D74E3C689A4");
      IWorkflowManager workflowManager = ServiceRegistration.Get<IWorkflowManager>();
      workflowManager.NavigatePush(WF_STATE_ID_SCHEDULE_LIST);
    }
  }
}
