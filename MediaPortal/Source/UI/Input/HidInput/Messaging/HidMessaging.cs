﻿#region Copyright (C) 2007-2021 Team MediaPortal

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
using MediaPortal.Common.Logging;
using MediaPortal.Common.Messaging;
using SharpLib.Hid;

namespace HidInput.Messaging
{
  public static class HidMessaging
  {
    // Message channel name
    public const string PREVIEW_CHANNEL = "HidEvent.Preview";

    public const string CHANNEL = "HidEvent";

    // Message type
    public enum MessageType
    {
      HidEvent,
      DeviceArrived,
      DeviceRemoved,
      Activated,
      Deactivated
    }

    public const string EVENT = "Event";
    public const string HANDLED = "Handled";

    public const string DEVICE_NAME = "DeviceName";

    public static bool BroadcastHidEventMessage(Event hidEvent)
    {
      SystemMessage message = new SystemMessage(MessageType.HidEvent);
      message.MessageData[EVENT] = hidEvent;
      message.MessageData[HANDLED] = false;

      BroadcastMessage(message, true);

      return message.MessageData[HANDLED] as bool? == true;
    }

    public static void BroadcastDeviceArrivedMessage(string deviceName)
    {
#if EXTENDED_INPUT_LOGGING
      ServiceRegistration.Get<ILogger>().Debug($"{nameof(HidMessaging)}: Sending device arrived message for device {{0}}", deviceName);
#endif

      SystemMessage message = new SystemMessage(MessageType.DeviceArrived);
      message.MessageData[DEVICE_NAME] = deviceName;
      BroadcastMessage(message);
    }

    public static void BroadcastDeviceRemovedMessage(string deviceName)
    {
#if EXTENDED_INPUT_LOGGING
      ServiceRegistration.Get<ILogger>().Debug($"{nameof(HidMessaging)}: Sending device removed message for device {{0}}", deviceName);
#endif

      SystemMessage message = new SystemMessage(MessageType.DeviceRemoved);
      message.MessageData[DEVICE_NAME] = deviceName;
      BroadcastMessage(message);
    }

    public static void BroadcastActivatedMessage()
    {
      BroadcastMessage(new SystemMessage(MessageType.Activated));
    }

    public static void BroadcastDeactivatedMessage()
    {
      BroadcastMessage(new SystemMessage(MessageType.Deactivated));
    }

    static void BroadcastMessage(SystemMessage message, bool sendPreviewMessage = false)
    {
      IMessageBroker messageBroker = ServiceRegistration.Get<IMessageBroker>();
      if (sendPreviewMessage)
        messageBroker.Send(PREVIEW_CHANNEL, message);
      messageBroker.Send(CHANNEL, message);
    }
  }
}
