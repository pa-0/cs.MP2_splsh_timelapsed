﻿#region Copyright (C) 2007-2017 Team MediaPortal

/*
    Copyright (C) 2007-2017 Team MediaPortal
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
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MediaPortal.Common.Logging;
using UPnP.Infrastructure.CP.DeviceTree;

namespace MediaPortal.Common.Services.ServerCommunication
{
  /// <summary>
  /// Wrapper class for common async method calls. It contains the <see cref="Success"/> return values and one additional <see cref="Result"/>.
  /// It is used to transform methods with out parameters more easy into async pattern where "out" and "ref" are not possible. 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class AsyncResult<T>
  {
    public AsyncResult() { }

    public AsyncResult(bool success, T result)
    {
      Success = success;
      Result = result;
    }
    /// <summary>
    /// Returns <c>true</c> if successful.
    /// </summary>
    public bool Success { get; set; }
    /// <summary>
    /// Returns <c>true</c> if successful.
    /// </summary>
    public T Result { get; set; }
  }

  public static class AsyncExtensions
  {
    /// <summary>
    /// Adds a try/catch block around the given <paramref name="task"/> and logs possible exceptions as warnings.
    /// </summary>
    /// <param name="task">Task to execute</param>
    public static async Task Try(this Task task)
    {
      try
      {
        await task;
      }
      catch (Exception e)
      {
        ServiceRegistration.Get<ILogger>().Warn("Exception in async taks: ", e);
      }
    }
    /// <summary>
    /// Adds a try/catch block around the given <paramref name="task"/> and logs possible exceptions as warnings.
    /// </summary>
    /// <param name="task">Task to execute</param>
    public static async Task<T> Try<T>(this Task<T> task)
    {
      try
      {
        return await task;
      }
      catch (Exception e)
      {
        ServiceRegistration.Get<ILogger>().Warn("Exception in async taks: ", e);
        return default(T);
      }
    }

    /// <summary>
    /// Adds a try/catch block around the given <paramref name="task"/>, logs possible exceptions as warnings and synchronously waits for completion.
    /// </summary>
    /// <param name="task">Task to execute</param>
    public static void TryWait(this Task task)
    {
      task.Try().Wait();
    }

    /// <summary>
    /// Adds a try/catch block around the given <paramref name="task"/>, logs possible exceptions as warnings and synchronously waits for completion.
    /// </summary>
    /// <param name="task">Task to execute</param>
    public static T TryWait<T>(this Task<T> task)
    {
      return task.Try().Result;
    }

    public static async Task<IList<object>> InvokeAsyncTask(this CpAction action, IList<object> inParameters)
    {
      return await Task.Factory.FromAsync((callback, stateObject) => action.BeginInvokeAction(inParameters, callback, stateObject), action.EndInvokeAction, null).ConfigureAwait(false);
    }
    public static ConfiguredTaskAwaitable<IList<object>> InvokeAsync(this CpAction action, IList<object> inParameters)
    {
      return Task.Factory.FromAsync((callback, stateObject) => action.BeginInvokeAction(inParameters, callback, stateObject), action.EndInvokeAction, null).ConfigureAwait(false);
    }
  }
}
