#region Copyright (C) 2007-2010 Team MediaPortal

/*
    Copyright (C) 2007-2010 Team MediaPortal
    http://www.team-mediaportal.com
 
    This file is part of MediaPortal 2

    MediaPortal 2 is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    MediaPortal 2 is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with MediaPortal 2.  If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

using System;
using System.Collections.Generic;
using MediaPortal.Core;
using MediaPortal.Core.General;
using MediaPortal.UI.Presentation.Models;
using MediaPortal.UI.Presentation.Players;
using MediaPortal.UI.Presentation.Workflow;
using MediaPortal.UiComponents.Media.General;

namespace MediaPortal.UiComponents.Media.Models
{
  /// <summary>
  /// Attends the CurrentlyPlaying and FullscreenContent states for audio players.
  /// </summary>
  public class AudioPlayerModel : BaseTimerControlledUIModel, IWorkflowModel
  {
    public const string MODEL_ID_STR = "D8998340-DA2D-42be-A29B-6D7A72AEA2DC";
    public static readonly Guid MODEL_ID = new Guid(MODEL_ID_STR);

    protected MediaWorkflowStateType _currentMediaWorkflowStateType = MediaWorkflowStateType.None;

    protected AbstractProperty _currentPlayerIndexProperty;

    public AudioPlayerModel() : base(300)
    {
      _currentPlayerIndexProperty = new WProperty(typeof(int), 0);
      // Don't StartTimer here, since that will be done in method EnterModelContext
    }

    protected override void Update()
    {
      IPlayerContextManager playerContextManager = ServiceRegistration.Get<IPlayerContextManager>();

      CurrentPlayerIndex = playerContextManager.CurrentPlayerIndex;
    }

    protected static bool CanHandlePlayer(IPlayer player)
    {
      return player is IAudioPlayer;
    }

    protected void UpdateAudioStateType(NavigationContext newContext)
    {
      if (newContext.WorkflowState.StateId == Consts.CURRENTLY_PLAYING_AUDIO_WORKFLOW_STATE_ID)
        _currentMediaWorkflowStateType = MediaWorkflowStateType.CurrentlyPlaying;
      else if (newContext.WorkflowState.StateId == Consts.FULLSCREEN_AUDIO_WORKFLOW_STATE_ID)
        _currentMediaWorkflowStateType = MediaWorkflowStateType.FullscreenContent;
      else
        _currentMediaWorkflowStateType = MediaWorkflowStateType.None;
    }

    #region Members to be accessed from the GUI

    public AbstractProperty CurrentPlayerIndexProperty
    {
      get { return _currentPlayerIndexProperty; }
    }

    public int CurrentPlayerIndex
    {
      get { return (int) _currentPlayerIndexProperty.GetValue(); }
      set { _currentPlayerIndexProperty.SetValue(value); }
    }

    #endregion

    #region IWorkflowModel implementation

    public override Guid ModelId
    {
      get { return MODEL_ID; }
    }

    public bool CanEnterState(NavigationContext oldContext, NavigationContext newContext)
    {
      IPlayerContextManager playerContextManager = ServiceRegistration.Get<IPlayerContextManager>();
      IPlayerContext pc = null;
      if (newContext.WorkflowState.StateId == Consts.CURRENTLY_PLAYING_AUDIO_WORKFLOW_STATE_ID)
        // The "currently playing" screen is always bound to the "current player"
        pc = playerContextManager.CurrentPlayerContext;
      else if (newContext.WorkflowState.StateId == Consts.FULLSCREEN_AUDIO_WORKFLOW_STATE_ID)
        // The "fullscreen content" screen is always bound to the "primary player"
        pc = playerContextManager.GetPlayerContext(PlayerManagerConsts.PRIMARY_SLOT);
      return pc != null && CanHandlePlayer(pc.CurrentPlayer);
    }

    public void EnterModelContext(NavigationContext oldContext, NavigationContext newContext)
    {
      StartTimer(); // Lazily start our timer
      UpdateAudioStateType(newContext);
    }

    public void ExitModelContext(NavigationContext oldContext, NavigationContext newContext)
    {
      StopTimer(); // Reduce workload when none of our states is used
      UpdateAudioStateType(newContext);
    }

    public void ChangeModelContext(NavigationContext oldContext, NavigationContext newContext, bool push)
    {
      UpdateAudioStateType(newContext);
    }

    public void Deactivate(NavigationContext oldContext, NavigationContext newContext)
    {
      // Nothing to do
    }

    public void ReActivate(NavigationContext oldContext, NavigationContext newContext)
    {
      // Nothing to do
    }

    public void UpdateMenuActions(NavigationContext context, IDictionary<Guid, WorkflowAction> actions)
    {
      // Nothing to do
    }

    public ScreenUpdateMode UpdateScreen(NavigationContext context, ref string screen)
    {
      switch (_currentMediaWorkflowStateType)
      {
        case MediaWorkflowStateType.CurrentlyPlaying:
          screen = Consts.CURRENTLY_PLAYING_AUDIO_SCREEN;
          break;
        case MediaWorkflowStateType.FullscreenContent:
          screen = Consts.FULLSCREEN_AUDIO_SCREEN;
          break;
      }
      return ScreenUpdateMode.AutoWorkflowManager;
    }

    #endregion
  }
}
