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
using MediaPortal.Common.Async;
using MediaPortal.Common.Commands;
using MediaPortal.Common.General;
using MediaPortal.Plugins.SlimTv.Client.Helpers;
using MediaPortal.Plugins.SlimTv.Client.Messaging;
using MediaPortal.Plugins.SlimTv.Interfaces;
using MediaPortal.Plugins.SlimTv.Interfaces.Items;
using MediaPortal.UI.Presentation.DataObjects;
using MediaPortal.UI.Presentation.Screens;
using MediaPortal.UI.Presentation.Workflow;
using MediaPortal.UiComponents.Media.General;
using MediaPortal.Utilities;

namespace MediaPortal.Plugins.SlimTv.Client.Models
{
  /// <summary>
  /// <see cref="SlimTvProgramSearchModel"/> holds all data for extended scheduling options.
  /// </summary>
  public class SlimTvProgramSearchModel : SlimTvGuideModelBase
  {
    public const string MODEL_ID_STR = "71F1D594-21BF-4639-9F8A-3CE8D8170333";
    public static readonly Guid MODEL_ID = new Guid(MODEL_ID_STR);

    #region Protected classes

    /// <summary>
    /// Used to save and restore the current state of the <see cref="SlimTvProgramSearchModel"/>
    /// for a given workflow state.
    /// </summary>
    protected class ProgramSearchMemento
    {
      public string ProgramSearchText { get; set; }
      public bool UseContainsQuery { get; set; }
      public int LastProgramId { get; set; }
    }

    #endregion

    #region Fields

    protected IDictionary<Guid, ProgramSearchMemento> _stateMementos = new Dictionary<Guid, ProgramSearchMemento>();
    protected bool _isRestoring;
    protected int _lastProgramId;
    protected AbstractProperty _channelNameProperty = null;
    protected AbstractProperty _channelNumberProperty = null;
    protected AbstractProperty _channelLogoTypeProperty = null;
    protected AbstractProperty _programSearchTextProperty = null;
    protected AbstractProperty _useContainsQueryProperty = null;
    protected readonly ItemsList _programsList = new ItemsList();

    #endregion

    #region GUI properties and methods

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
    /// Exposes the current search text.
    /// </summary>
    public string ProgramSearchText
    {
      get { return (string)_programSearchTextProperty.GetValue(); }
      set { _programSearchTextProperty.SetValue(value); }
    }

    /// <summary>
    /// Exposes the current channel name to the skin.
    /// </summary>
    public AbstractProperty ProgramSearchTextProperty
    {
      get { return _programSearchTextProperty; }
    }

    /// <summary>
    /// Indicates if the entered search term should be a "contains" query. By default, the search does a "starts with" query.
    /// </summary>
    public bool UseContainsQuery
    {
      get { return (bool)_useContainsQueryProperty.GetValue(); }
      set { _useContainsQueryProperty.SetValue(value); }
    }

    /// <summary>
    /// Indicates if the entered search term should be a "contains" query. By default, the search does a "starts with" query.
    /// </summary>
    public AbstractProperty UseContainsQueryProperty
    {
      get { return _useContainsQueryProperty; }
    }

    /// <summary>
    /// Exposes the list of channels in current group.
    /// </summary>
    public ItemsList ProgramsList
    {
      get { return _programsList; }
    }

    public void RecordMenu()
    {
      ProgramListItem item = SlimTvExtScheduleModel.CurrentItem as ProgramListItem;
      if (item != null)
        ShowProgramActions(item.AdditionalProperties["PROGRAM"] as IProgram);
    }

    public void RecordSingleProgram(IProgram program)
    {
      _ = CreateOrDeleteSchedule(program);
    }

    public void RecordSeries(IProgram program)
    {
      InitSeriesTypeList(program);
      IScreenManager screenManager = ServiceRegistration.Get<IScreenManager>();
      screenManager.ShowDialog("DialogExtSchedule");
    }

    public void RecordOrCancelSeries(IProgram program, ScheduleRecordingType scheduleRecordingType)
    {
      CreateOrDeleteSchedule(program, scheduleRecordingType).Wait();
    }

    public void CancelSchedule(IProgram program)
    {
      InitDeleteChoicesList(program);
      IScreenManager screenManager = ServiceRegistration.Get<IScreenManager>();
      screenManager.ShowDialog("DialogExtSchedule");
    }

    #endregion

    #region Inits and Updates

    protected override void InitModel()
    {
      if (!_isInitialized)
      {
        _channelNameProperty = new WProperty(typeof(string), string.Empty);
        _channelNumberProperty = new WProperty(typeof(int), 0);
        _channelLogoTypeProperty = new WProperty(typeof(string), string.Empty);
        _programSearchTextProperty = new WProperty(typeof(string), string.Empty);
        _programSearchTextProperty.Attach(ProgramSearchTextChanged);
        _useContainsQueryProperty = new WProperty(typeof(bool), false);
        _useContainsQueryProperty.Attach(ProgramSearchTextChanged);
      }
      base.InitModel();
    }

    private void ProgramSearchTextChanged(AbstractProperty property, object oldvalue)
    {
      if (!_isRestoring)
        _ = UpdatePrograms();
    }

    private void InitSeriesTypeList(IProgram program)
    {
      var wf = ServiceRegistration.Get<IWorkflowManager>();
      var model = wf.GetModel(SlimTvExtScheduleModel.MODEL_ID) as SlimTvExtScheduleModel;
      if (model == null)
        return;
      DialogHeader = "[SlimTvClient.ChooseScheduleType]";
      model.DialogActionsList.Clear();
      foreach (string name in Enum.GetNames(typeof(ScheduleRecordingType)))
      {
        string currentName = name;
        ListItem recTypeItem = new ListItem(Consts.KEY_NAME, string.Format("[SlimTvClient.ScheduleRecordingType_{0}]", name))
        {
          Command = new MethodDelegateCommand(() => RecordOrCancelSeries(program, (ScheduleRecordingType)Enum.Parse(typeof(ScheduleRecordingType), currentName)))
        };
        model.DialogActionsList.Add(recTypeItem);
      }
      model.DialogActionsList.FireChange();
    }

    private void InitDeleteChoicesList(IProgram program)
    {
      var wf = ServiceRegistration.Get<IWorkflowManager>();
      var model = wf.GetModel(SlimTvExtScheduleModel.MODEL_ID) as SlimTvExtScheduleModel;
      if (model == null)
        return;
      DialogHeader = "[SlimTvClient.DeleteScheduleType]";
      model.DialogActionsList.Clear();
      ListItem recTypeItem = new ListItem(Consts.KEY_NAME, "[SlimTvClient.DeleteSingle]")
      {
        Command = new MethodDelegateCommand(() => RecordOrCancelSeries(program, ScheduleRecordingType.Once))
      };
      model.DialogActionsList.Add(recTypeItem);
      recTypeItem = new ListItem(Consts.KEY_NAME, "[SlimTvClient.DeleteFullSchedule]")
      {
        Command = new MethodDelegateCommand(() => RecordOrCancelSeries(program, ScheduleRecordingType.EveryTimeOnEveryChannel))
      };
      model.DialogActionsList.Add(recTypeItem);
    }

    #endregion

    #region Channel, groups and programs

    protected override void Update()
    {
    }

    //protected override void UpdateCurrentChannel()
    //{
    //}

    protected override void UpdateProgramStatus(IProgram program)
    {
      base.UpdateProgramStatus(program);

      var result = _tvHandler.ChannelAndGroupInfo.GetChannelAsync(program.ChannelId).Result;

      ChannelName = result.Success ? result.Result.Name : string.Empty;
      ChannelNumber = result.Success ? result.Result.ChannelNumber : 0;
      ChannelLogoType = result.Result.GetFanArtMediaType();
    }

    protected override bool UpdateRecordingStatus(IProgram program)
    {
      IProgramRecordingStatus recordingStatus = program as IProgramRecordingStatus;
      if (recordingStatus == null)
        return false;

      foreach (var programItem in _programsList.OfType<ProgramListItem>().Where(programItem => programItem.Program.ProgramId == program.ProgramId))
        programItem.Program.UpdateState(recordingStatus.RecordingStatus);
      return true;
    }

    /// <summary>
    /// For extended scheduling we will load all programs with same title independent from channel.
    /// </summary>
    protected async Task UpdatePrograms()
    {
      if (_tvHandler.ProgramInfo == null)
        return;

      if (string.IsNullOrEmpty(ProgramSearchText) || ProgramSearchText.Length < 2)
      {
        SetEmptyPrograms();
        return;
      }

      DateTime dtDay = DateTime.Now;
      var result = await _tvHandler.ProgramInfo.GetProgramsAsync(GetSearchTerm(), dtDay, dtDay.AddDays(28));
      if (!result.Success)
      {
        SetEmptyPrograms();
        return;
      }

      Dictionary<int, IChannel> programChannels = new Dictionary<int, IChannel>();
      AsyncResult<IChannel> channelResult;
      foreach (IProgram program in result.Result)
        if (!programChannels.ContainsKey(program.ChannelId) && (channelResult = await _tvHandler.ChannelAndGroupInfo.GetChannelAsync(program.ChannelId)).Result != null)
          programChannels.Add(program.ChannelId, channelResult.Result);

      _programs = result.Result.Where(p => !programChannels.TryGetValue(p.ChannelId, out IChannel c) || c.MediaType == _mediaType).ToList();
      FillProgramsList(_programs, programChannels);
    }

    private string GetSearchTerm()
    {
      var text = ProgramSearchText.Trim('%', ' ');
      if (UseContainsQuery)
      {
        text = "%" + text;
      }
      return text;
    }

    private void SetEmptyPrograms()
    {
      _programs = new List<IProgram>();
      FillProgramsList(_programs, null);
    }

    private void FillProgramsList(IEnumerable<IProgram> programs, IDictionary<int, IChannel> programChannels)
    {
      lock (_programsList.SyncRoot)
      {
        _programsList.Clear();
        foreach (var program in programs)
          _programsList.Add(CreateProgramListItem(program, programChannels?.TryGetValue(program.ChannelId, out IChannel c) == true ? c : null));
      }

      _programsList.FireChange();
    }

    protected ProgramListItem CreateProgramListItem(IProgram program, IChannel channel)
    {
      ProgramProperties programProperties = new ProgramProperties();
      programProperties.SetProgram(program, channel);

      ProgramListItem item = new ProgramListItem(programProperties)
      {
        Command = new MethodDelegateCommand(() =>
        {
          var isSingle = programProperties.IsScheduled;
          var isSeries = programProperties.IsSeriesScheduled;
          if (isSingle || isSeries)
            CancelSchedule(program);
          else
            RecordSeries(program);
        })
      };
      item.AdditionalProperties["PROGRAM"] = program;
      item.Selected = _lastProgramId == program.ProgramId; // Restore focus
      return item;
    }

    protected override async Task<RecordingStatus?> CreateOrDeleteSchedule(IProgram program, ScheduleRecordingType recordingType = ScheduleRecordingType.Once)
    {
      _lastProgramId = program.ProgramId;
      var newStatus = await base.CreateOrDeleteSchedule(program, recordingType);

      if (!newStatus.HasValue)
        return RecordingStatus.None;

      await UpdatePrograms(); // Reload all programs, as series scheduling will affect multiple programs
      NotifyAllPrograms();
      return newStatus.Value;
    }

    private void NotifyAllPrograms()
    {
      // Send message to all listeners that programs might have been changed
      foreach (IProgram program in _programs)
        SlimTvClientMessaging.SendSlimTvProgramChangedMessage(program);
    }

    /// <summary>
    /// Saves the current state of the model so it can be restored when returning
    /// to a <see cref="NavigationContext"/> with the same <see cref="WorkflowState.StateId"/>.
    /// </summary>
    /// <param name="context">The context containing the workflow state to save.</param>
    protected void SaveCurrentState(NavigationContext context)
    {
      _stateMementos[context.WorkflowState.StateId] = new ProgramSearchMemento
      {
        ProgramSearchText = ProgramSearchText,
        UseContainsQuery = UseContainsQuery,
        LastProgramId = _lastProgramId,
      };
    }

    /// <summary>
    /// Restores the model state based on the <see cref="WorkflowState.StateId"/> of a <see cref="NavigationContext"/>.
    /// </summary>
    /// <param name="context">The context containing the workflow state to restore.</param>
    protected void RestoreLastState(NavigationContext context)
    {
      // prevent change handlers from firing
      _isRestoring = true;
      if(_stateMementos.TryGetValue(context.WorkflowState.StateId, out var state))
      {
        ProgramSearchText = state.ProgramSearchText;
        UseContainsQuery = state.UseContainsQuery;
        _lastProgramId = state.LastProgramId;
      }
      else
      {
        ProgramSearchText = null;
        UseContainsQuery = false;
        _lastProgramId = -1;
      }

      // allow change handlers to fire
      _isRestoring = false;
      // always assume the model has changed as the workflow state may have changed between tv or radio search
      // so the same search term may return different results.
      _ = UpdatePrograms();
    }

    #endregion

    #region IWorkflowModel implementation

    public override Guid ModelId
    {
      get { return MODEL_ID; }
    }

    public override void EnterModelContext(NavigationContext oldContext, NavigationContext newContext)
    {
      base.EnterModelContext(oldContext, newContext);
      RestoreLastState(newContext);
    }

    public override void ChangeModelContext(NavigationContext oldContext, NavigationContext newContext, bool push)
    {
      SaveCurrentState(oldContext);
      base.ChangeModelContext(oldContext, newContext, push);
      RestoreLastState(newContext);
    }

    public override void ExitModelContext(NavigationContext oldContext, NavigationContext newContext)
    {
      SaveCurrentState(oldContext);
      base.ExitModelContext(oldContext, newContext);
    }

    #endregion
  }
}
