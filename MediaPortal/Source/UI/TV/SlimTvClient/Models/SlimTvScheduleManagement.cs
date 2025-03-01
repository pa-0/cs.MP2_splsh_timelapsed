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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPortal.Common;
using MediaPortal.Common.Commands;
using MediaPortal.Common.General;
using MediaPortal.Plugins.SlimTv.Client.Helpers;
using MediaPortal.Plugins.SlimTv.Interfaces;
using MediaPortal.Plugins.SlimTv.Interfaces.Items;
using MediaPortal.UI.Presentation.DataObjects;
using MediaPortal.UI.Presentation.Screens;
using MediaPortal.UI.Presentation.Workflow;
using MediaPortal.UiComponents.Media.General;
using MediaPortal.UI.SkinEngine.MpfElements;
using MediaPortal.Utilities;

namespace MediaPortal.Plugins.SlimTv.Client.Models
{
  public class SlimTvScheduleManagement : SlimTvModelBase
  {
    public const string MODEL_ID_STR = "7610403A-4488-47A4-8C27-FD1FE833E52B";
    public static readonly Guid MODEL_ID = new Guid(MODEL_ID_STR);

    #region Fields

    protected ISchedule _selectedSchedule;
    protected AbstractProperty _scheduleSeriesModeProperty = null;
    protected AbstractProperty _channelNameProperty = null;
    protected AbstractProperty _channelNumberProperty = null;
    protected AbstractProperty _channelLogoTypeProperty = null;
    protected AbstractProperty _scheduleNameProperty = null;
    protected AbstractProperty _scheduleTypeProperty = null;
    protected AbstractProperty _scheduleTypeEnumProperty = null;
    protected AbstractProperty _startTimeProperty = null;
    protected AbstractProperty _endTimeProperty = null;
    protected AbstractProperty _currentProgramProperty = null;
    protected readonly ItemsList _schedulesList = new ItemsList();

    #endregion

    #region GUI properties and methods

    /// <summary>
    /// Exposes the current program of tuned channel to the skin.
    /// </summary>
    public ProgramProperties CurrentProgram
    {
      get { return (ProgramProperties)_currentProgramProperty.GetValue(); }
      set { _currentProgramProperty.SetValue(value); }
    }

    /// <summary>
    /// Exposes the current program of tuned channel to the skin.
    /// </summary>
    public AbstractProperty CurrentProgramProperty
    {
      get { return _currentProgramProperty; }
    }

    /// <summary>
    /// Enables series schedule mode (<c>true</c>) or shows all single upcoming programs (<c>false</c>).
    /// </summary>
    public bool ScheduleSeriesMode
    {
      get { return (bool)_scheduleSeriesModeProperty.GetValue(); }
      set { _scheduleSeriesModeProperty.SetValue(value); }
    }

    /// <summary>
    /// Enables series schedule mode (<c>true</c>) or shows all single upcoming programs (<c>false</c>).
    /// </summary>
    public AbstractProperty ScheduleSeriesModeProperty
    {
      get { return _scheduleSeriesModeProperty; }
    }

    /// <summary>
    /// Exposes the current channel name to the skin.
    /// </summary>
    public string ChannelName
    {
      get { return (string)_channelNameProperty.GetValue(); }
      set { _channelNameProperty.SetValue(value); }
    }

    /// <summary>
    /// Exposes the current channel name to the skin.
    /// </summary>
    public AbstractProperty ChannelNameProperty
    {
      get { return _channelNameProperty; }
    }

    /// <summary>
    /// Exposes the current channel logo type to the skin.
    /// </summary>
    public string ChannelLogoType
    {
      get { return (string)_channelLogoTypeProperty.GetValue(); }
      set { _channelLogoTypeProperty.SetValue(value); }
    }

    /// <summary>
    /// Exposes the current channel logo type to the skin.
    /// </summary>
    public AbstractProperty ChannelLogoTypeProperty
    {
      get { return _channelLogoTypeProperty; }
    }

    /// <summary>
    /// Exposes the current channel number to the skin.
    /// </summary>
    public int ChannelNumber
    {
      get { return (int)_channelNumberProperty.GetValue(); }
      set { _channelNumberProperty.SetValue(value); }
    }

    /// <summary>
    /// Exposes the current channel number to the skin.
    /// </summary>
    public AbstractProperty ChannelNumberProperty
    {
      get { return _channelNumberProperty; }
    }

    /// <summary>
    /// Exposes the schedule type of current schedule to the skin.
    /// </summary>
    public string ScheduleName
    {
      get { return (string)_scheduleNameProperty.GetValue(); }
      set { _scheduleNameProperty.SetValue(value); }
    }

    /// <summary>
    /// Exposes the schedule type of current schedule to the skin.
    /// </summary>
    public AbstractProperty ScheduleNameProperty
    {
      get { return _scheduleNameProperty; }
    }

    /// <summary>
    /// Exposes the schedule type of current schedule to the skin.
    /// </summary>
    public string ScheduleType
    {
      get { return (string)_scheduleTypeProperty.GetValue(); }
      set { _scheduleTypeProperty.SetValue(value); }
    }

    /// <summary>
    /// Exposes the localized schedule type of current schedule to the skin.
    /// </summary>
    public AbstractProperty ScheduleTypeProperty
    {
      get { return _scheduleTypeProperty; }
    }

    /// <summary>
    /// Exposes the schedule type (enum name) of current schedule to the skin.
    /// </summary>
    public string ScheduleTypeEnum
    {
      get { return (string)_scheduleTypeEnumProperty.GetValue(); }
      set { _scheduleTypeEnumProperty.SetValue(value); }
    }

    /// <summary>
    /// Exposes the schedule type of current schedule to the skin.
    /// </summary>
    public AbstractProperty ScheduleTypeEnumProperty
    {
      get { return _scheduleTypeEnumProperty; }
    }

    /// <summary>
    /// Exposes the list of schedules.
    /// </summary>
    public ItemsList SchedulesList
    {
      get { return _schedulesList; }
    }

    /// <summary>
    /// Exposes the schedule's start time.
    /// </summary>
    public DateTime StartTime
    {
      get { return (DateTime)_startTimeProperty.GetValue(); }
      set { _startTimeProperty.SetValue(value); }
    }

    /// <summary>
    /// Exposes the schedule's start time.
    /// </summary>
    public AbstractProperty StartTimeProperty
    {
      get { return _startTimeProperty; }
    }

    /// <summary>
    /// Exposes the schedule's end time.
    /// </summary>
    public DateTime EndTime
    {
      get { return (DateTime)_endTimeProperty.GetValue(); }
      set { _endTimeProperty.SetValue(value); }
    }

    /// <summary>
    /// Exposes the schedule's start time.
    /// </summary>
    public AbstractProperty EndTimeProperty
    {
      get { return _endTimeProperty; }
    }

    public void UpdateSchedule(object sender, SelectionChangedEventArgs e)
    {
      var selectedItem = e.FirstAddedItem as ListItem;
      if (selectedItem == null)
        return;
      ISchedule schedule = (ISchedule)selectedItem.AdditionalProperties["SCHEDULE"];
      UpdateScheduleDetails(schedule).Wait();
      if (selectedItem.AdditionalProperties.ContainsKey("PROGRAM"))
      {
        IProgram program = (IProgram)selectedItem.AdditionalProperties["PROGRAM"];
        CurrentProgram.SetProgram(program);
      }
      else
      {
        CurrentProgram.SetProgram(null);
      }
    }

    public void RecordMenu()
    {
      ListItem item = SlimTvExtScheduleModel.CurrentItem;
      if(item != null)
        ShowActions(item.AdditionalProperties["SCHEDULE"] as ISchedule, item.AdditionalProperties["PROGRAM"] as IProgram);
    }

    private async Task UpdateScheduleDetails(ISchedule schedule)
    {
      // Clear properties if no schedule is given
      if (schedule == null)
      {
        StartTime = EndTime = DateTime.MinValue;
        ChannelName = ScheduleName = ScheduleType = ScheduleTypeEnum = string.Empty;
        ChannelNumber = 0;
        return;
      }

      IChannel channel = null;
      if (_tvHandler.ChannelAndGroupInfo != null)
      {
        var result = await _tvHandler.ChannelAndGroupInfo.GetChannelAsync(schedule.ChannelId);
        if (result.Success)
        {
          channel = result.Result;
        }
      }

      StartTime = schedule.StartTime;
      EndTime = schedule.EndTime;
      ChannelName = channel?.Name ?? String.Empty;
      ChannelNumber = channel?.ChannelNumber ?? 0;
      ChannelLogoType = channel.GetFanArtMediaType();
      ScheduleName = schedule.Name;
      ScheduleType = string.Format("[SlimTvClient.ScheduleRecordingType_{0}]", schedule.RecordingType);
      ScheduleTypeEnum = schedule.RecordingType.ToString();
    }
    #endregion

    private void ToggleSeriesMode(AbstractProperty property, object oldvalue)
    {
      _ = LoadSchedules();
    }

    protected async Task LoadSchedules()
    {
      await UpdateScheduleDetails(null);
      if (_tvHandler.ScheduleControl == null)
        return;

      var result = await _tvHandler.ScheduleControl.GetSchedulesAsync();
      if (!result.Success)
        return;

      IList<ISchedule> schedules = result.Result;

      _schedulesList.Clear();

      // Temporary list for sorting
      List<ListItem> sortList = new List<ListItem>();
      Comparison<ListItem> sortMode;

      if (!ScheduleSeriesMode)
      {
        Dictionary<ISchedule, IList<IProgram>> allPrograms = new Dictionary<ISchedule, IList<IProgram>>();
        // Load all series type schedules and their programs which will be recorded.
        foreach (ISchedule schedule in schedules)
        {
          var progResult = await _tvHandler.ScheduleControl.GetProgramsForScheduleAsync(schedule);
          if (!progResult.Success || progResult.Result.Count == 0)
          {
            if(schedule.Name != "Manual" || schedule.EndTime < DateTime.Now)
              continue;
            // Make dummy program for manual schedule
            MediaPortal.Plugins.SlimTv.Interfaces.UPnP.Items.Program program = new Interfaces.UPnP.Items.Program()
            {
              ChannelId = schedule.ChannelId,
              Title = "[SlimTvClient.ManualRecordingTitle]",
              StartTime = schedule.StartTime,
              EndTime = schedule.EndTime,
              RecordingStatus = RecordingStatus.Scheduled
            };
            if (program.StartTime <= DateTime.Now && program.EndTime >= DateTime.Now)
              program.RecordingStatus |= RecordingStatus.Recording;
            if (schedule.RecordingType != ScheduleRecordingType.Once)
              program.RecordingStatus |= RecordingStatus.SeriesScheduled;
            progResult.Result = new List<IProgram>();
            progResult.Result.Add(program);
          }

          IList<IProgram> schedulePrograms = progResult.Result;

          // The GetProgramsForSchedule returns all matching programs, also the canceled ones. So we need to filter them out here.
          allPrograms[schedule] = schedulePrograms.OfType<IProgramRecordingStatus>().Where(p => p.RecordingStatus != RecordingStatus.None).Cast<IProgram>().ToList();
        }

        foreach (var schedule in allPrograms.Keys)
        {
          foreach (var program in allPrograms[schedule])
          {
            var item = CreateProgramItem(program, schedule);
            sortList.Add(item);
          }
        }

        sortMode = ProgramStartTimeComparison;
      }
      else
      {
        foreach (ISchedule schedule in schedules)
        {
          var item = CreateScheduleItem(schedule);
          sortList.Add(item);
        }

        sortMode = ChannelAndProgramStartTimeComparison;
      }
      sortList.Sort(sortMode);
      CollectionUtils.AddAll(_schedulesList, sortList);
      _schedulesList.FireChange();
    }

    /// <summary>
    /// Default sorting for single programs: first by <see cref="IProgram.StartTime"/>, then by <see cref="IChannel.Name"/>.
    /// </summary>
    private int ProgramStartTimeComparison(ListItem p1, ListItem p2)
    {
      var program1 = (IProgram)p1.AdditionalProperties["PROGRAM"];
      var program2 = (IProgram)p2.AdditionalProperties["PROGRAM"];
      int res = DateTime.Compare(program1.StartTime, program2.StartTime);
      if (res != 0)
        return res;

      var ch1 = _tvHandler.ChannelAndGroupInfo.GetChannelAsync(program1.ChannelId).Result;
      var ch2 = _tvHandler.ChannelAndGroupInfo.GetChannelAsync(program2.ChannelId).Result;
      if (ch1.Success && ch2.Success)
        return String.Compare(ch1.Result.Name, ch2.Result.Name, StringComparison.InvariantCultureIgnoreCase);

      return 0;
    }

    /// <summary>
    /// Default sorting for single programs: first by <see cref="ISchedule.RecordingType"/>=="Once", then <see cref="ISchedule.Name"/>, then by <see cref="IChannel.Name"/>.
    /// </summary>
    private int ChannelAndProgramStartTimeComparison(ListItem p1, ListItem p2)
    {
      var schedule1 = (ISchedule)p1.AdditionalProperties["SCHEDULE"];
      var schedule2 = (ISchedule)p2.AdditionalProperties["SCHEDULE"];

      // The "Once" schedule should appear first
      if (schedule1.RecordingType == ScheduleRecordingType.Once && schedule2.RecordingType != ScheduleRecordingType.Once)
        return -1;
      if (schedule1.RecordingType != ScheduleRecordingType.Once && schedule2.RecordingType == ScheduleRecordingType.Once)
        return +1;

      int res;
      if (schedule1.RecordingType == ScheduleRecordingType.Once && schedule2.RecordingType == ScheduleRecordingType.Once)
      {
        res = DateTime.Compare(schedule1.StartTime, schedule2.StartTime);
        if (res != 0)
          return res;
      }

      res = String.Compare(schedule1.Name, schedule2.Name, StringComparison.InvariantCultureIgnoreCase);
      if (res != 0)
        return res;

      var ch1 = _tvHandler.ChannelAndGroupInfo.GetChannelAsync(schedule1.ChannelId).Result;
      var ch2 = _tvHandler.ChannelAndGroupInfo.GetChannelAsync(schedule2.ChannelId).Result;
      if (ch1.Success && ch2.Success)
        return String.Compare(ch1.Result.Name, ch2.Result.Name, StringComparison.InvariantCultureIgnoreCase);

      return 0;
    }

    private ListItem CreateScheduleItem(ISchedule schedule)
    {
      ISchedule currentSchedule = schedule;
      ListItem item = new ListItem("Name", schedule.Name == "Manual" ? "[SlimTvClient.ManualRecordingTitle]" : schedule.Name)
      {
        Command = new MethodDelegateCommand(() => ShowActions(currentSchedule))
      };
      var result = _tvHandler.ChannelAndGroupInfo.GetChannelAsync(currentSchedule.ChannelId).Result;
      if (result.Success)
      {
        item.SetLabel("ChannelName", result.Result.Name);
        item.SetLabel("ChannelLogoType", result.Result.GetFanArtMediaType());
      }

      item.SetLabel("StartTime", schedule.StartTime.FormatProgramStartTime());
      item.SetLabel("EndTime", schedule.EndTime.FormatProgramEndTime());
      item.SetLabel("ScheduleType", string.Format("[SlimTvClient.ScheduleRecordingType_{0}]", schedule.RecordingType));
      item.SetLabel("ScheduleTypeEnum", schedule.RecordingType.ToString());
      item.AdditionalProperties["SCHEDULE"] = currentSchedule;
      return item;
    }

    private ListItem CreateProgramItem(IProgram program, ISchedule schedule)
    {
      ProgramProperties programProperties = new ProgramProperties();
      IProgram currentProgram = program;
      var result = _tvHandler.ChannelAndGroupInfo.GetChannelAsync(currentProgram.ChannelId).Result;
      var channel = !result.Success ? null : result.Result;
      programProperties.SetProgram(currentProgram, channel);

      ListItem item = new ProgramListItem(programProperties)
      {
        Command = new MethodDelegateCommand(() => ShowActions(schedule, program))
      };
      if (channel != null)
      {
        item.SetLabel("ChannelName", channel.Name);
        item.SetLabel("ChannelLogoType", channel.GetFanArtMediaType());
      }
      item.SetLabel("ScheduleType", string.Format("[SlimTvClient.ScheduleRecordingType_{0}]", schedule.RecordingType));
      item.SetLabel("ScheduleTypeEnum", schedule.RecordingType.ToString());

      item.AdditionalProperties["PROGRAM"] = currentProgram;
      item.AdditionalProperties["SCHEDULE"] = schedule;
      return item;
    }

    private void ShowActions(ISchedule currentSchedule, IProgram program = null)
    {
      DialogHeader = currentSchedule.Name;
      _dialogActionsList.Clear();

      if (program != null && currentSchedule.IsSeries)
      {
        // In program list, offer to delete single program of series
        _dialogActionsList.Add(new ListItem(Consts.KEY_NAME, "[SlimTvClient.DeleteSingle]")
        {
          Command = new AsyncMethodDelegateCommand(() => CreateOrDeleteSchedule(program))
        });
      }
      // Always offer to delete schedule (prompt is same as single program if recording isn't a series)
      _dialogActionsList.Add(new ListItem(Consts.KEY_NAME, currentSchedule.IsSeries ? "[SlimTvClient.DeleteFullSchedule]" : "[SlimTvClient.DeleteSingle]")
      {
        Command = new AsyncMethodDelegateCommand(() => DeleteSchedule(currentSchedule))
      });
      if (program == null && currentSchedule.IsSeries)
      {
        // In series list - offer to delete individual programs - will go to ExtSchedule to show all the programs
        _dialogActionsList.Add(new ListItem(Consts.KEY_NAME, "[SlimTvClient.CancelProgramsOfSeriesSchedule]")
        {
          Command = new MethodDelegateCommand(() => ShowAndEditPrograms(currentSchedule))
        });
      }
      _dialogActionsList.FireChange();

      IScreenManager screenManager = ServiceRegistration.Get<IScreenManager>();
      screenManager.ShowDialog("DialogScheduleManagement");
    }

    private void ShowAndEditPrograms(ISchedule schedule)
    {
      SlimTvExtScheduleModel.Show(schedule);
    }

    private async Task DeleteSchedule(ISchedule schedule)
    {
      if (_tvHandler.ScheduleControl != null && await _tvHandler.ScheduleControl.RemoveScheduleAsync(schedule))
      {
        await LoadSchedules();
      }
    }

    protected override async Task<RecordingStatus?> CreateOrDeleteSchedule(IProgram program, ScheduleRecordingType recordingType = ScheduleRecordingType.Once)
    {
      var result = await base.CreateOrDeleteSchedule(program, recordingType);
      await LoadSchedules();
      return result;
    }

    protected override void InitModel()
    {
      if (!_isInitialized)
      {
        _scheduleSeriesModeProperty = new WProperty(typeof(bool), false);
        _scheduleSeriesModeProperty.Attach(ToggleSeriesMode);
        _channelNameProperty = new WProperty(typeof(string), string.Empty);
        _channelNumberProperty = new WProperty(typeof(int), 0);
        _channelLogoTypeProperty = new WProperty(typeof(string), string.Empty);
        _scheduleNameProperty = new WProperty(typeof(string), string.Empty);
        _scheduleTypeProperty = new WProperty(typeof(string), string.Empty);
        _scheduleTypeEnumProperty = new WProperty(typeof(string), string.Empty);
        _startTimeProperty = new WProperty(typeof(DateTime), DateTime.MinValue);
        _endTimeProperty = new WProperty(typeof(DateTime), DateTime.MinValue);
        _currentProgramProperty = new WProperty(typeof(ProgramProperties), new ProgramProperties());
        _isInitialized = true;
      }
      base.InitModel();
    }

    protected override void Update()
    {
    }

    public override void Reactivate(NavigationContext oldContext, NavigationContext newContext)
    {
      base.Reactivate(oldContext, newContext);
      _ = LoadSchedules();
    }

    public override void EnterModelContext(NavigationContext oldContext, NavigationContext newContext)
    {
      base.EnterModelContext(oldContext, newContext);
      _ = LoadSchedules();
    }

    public override Guid ModelId
    {
      get { return MODEL_ID; }
    }
  }
}
