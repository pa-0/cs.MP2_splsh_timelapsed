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
using MediaPortal.Common.Logging;
using MediaPortal.Extensions.MediaServer.DLNA;
using System.IO;
#if NET5_0_OR_GREATER
using Microsoft.AspNetCore.Http;
#else
using Microsoft.Owin;
#endif

namespace MediaPortal.Extensions.MediaServer.Protocols
{
  public class GenericAccessProtocol
  {
    public enum ResourceAccessProtocol
    {
      GenericAccessProtocol,
      SamsungAccessProtocol,
      XBoxAccessProtocol
    }
    public static GenericAccessProtocol GetProtocolResourceHandler(ResourceAccessProtocol ResourceProtocol)
    {
      if (ResourceProtocol == ResourceAccessProtocol.SamsungAccessProtocol)
      {
        return new SamsungProtocolHandler();
      }
      else if (ResourceProtocol == ResourceAccessProtocol.XBoxAccessProtocol)
      {
        return new XBoxProtocolHandler();
      }
      return new GenericAccessProtocol();
    }

#if NET5_0_OR_GREATER
    public virtual bool HandleRequest(HttpContext context, DlnaMediaItem item)
#else
    public virtual bool HandleRequest(IOwinContext context, DlnaMediaItem item)
#endif
    {
      return false;
    }

#if NET5_0_OR_GREATER
    public virtual bool CanHandleRequest(HttpRequest request)
#else
    public virtual bool CanHandleRequest(IOwinRequest request)
#endif
    {
      return false;
    }

#if NET5_0_OR_GREATER
    public virtual Stream HandleResourceRequest(HttpContext context, DlnaMediaItem item)
#else
    public virtual Stream HandleResourceRequest(IOwinContext context, DlnaMediaItem item)
#endif
    {
      return null;
    }

    protected static ILogger Logger
    {
      get { return ServiceRegistration.Get<ILogger>(); }
    }
  }
}
