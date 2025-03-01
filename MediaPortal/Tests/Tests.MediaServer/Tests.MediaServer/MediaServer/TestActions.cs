﻿using MediaPortal.Backend.MediaLibrary;
using MediaPortal.Common;
using MediaPortal.Common.Localization;
using MediaPortal.Common.Logging;
using MediaPortal.Common.UserProfileDataManagement;
using MediaPortal.Extensions.MediaServer;
using MediaPortal.Extensions.MediaServer.Profiles;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UPnP.Infrastructure.Common;
using UPnP.Infrastructure.Dv;
using UPnP.Infrastructure.Dv.DeviceTree;
#if NET5_0_OR_GREATER
using Microsoft.AspNetCore.Http;
#else
using Microsoft.Owin;
#endif

namespace Tests.Server.MediaServer
{
  [TestFixture]
  public class TestActions
  {
    private TestMediaLibrary _library;

    [OneTimeSetUp]
    public void Init()
    {
      ServiceRegistration.Set<ILogger>(new NoLogger());
      ServiceRegistration.Set<ILocalization>(new NoLocalization());
      ServiceRegistration.Set<IMediaLibrary>(_library = new TestMediaLibrary());
      ServiceRegistration.Set<IUserProfileDataManagement>(new TestUserProfileDataManagement());

      ProfileManager.Profiles["DLNADefault"] = new EndPointProfile();
    }

    [SetUp]
    public void BeforeTest()
    {
      _library.Clear();
      _library.AddShare("11111111-aaaa-aaaa-aaaa-111111111111", "Test", "/Test", "Share", new[] { "Audio" });
    }

    private CallContext CreateContext()
    {
#if NET5_0_OR_GREATER
      HttpContext httpContext = new DefaultHttpContext();
      CallContext context = new CallContext(httpContext.Request, new DefaultHttpContext(), null);
      context.HttpContext.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("127.0.0.1");
      context.HttpContext.Connection.RemotePort = 1000;
#else
      CallContext context = new CallContext(new OwinRequest(), new OwinContext(), null);
      context.Request.RemoteIpAddress = "127.0.0.1";
      context.Request.RemotePort = 1000;
#endif
      return context;
    }

    [Test]
    public void TestBrowse()
    {
      UPnPContentDirectoryServiceImpl service = new UPnPContentDirectoryServiceImpl();

      DvAction browse = service.Actions["Browse"];

      IList<object> input = new object[] { "0", "BrowseDirectChildren", "*", (uint)0, (uint)200, "" };
      IList<object> output;
      UPnPError error = browse.InvokeAction(input, out output, false, CreateContext());
      Console.WriteLine("Error: {0}", error);
      Console.WriteLine("Output: {0}", output);
      Console.WriteLine("Output list: [{0}]", string.Join(",", output));
    }

    [Test]
    public void TestSearch()
    {
      UPnPContentDirectoryServiceImpl service = new UPnPContentDirectoryServiceImpl();

      DvAction browse = service.Actions["Search"];

      IList<object> input = new object[] { "0", "upnp:class derivedfrom \"object.container.playlistContainer\" and @refID exists false", "dc:title,microsoft:folderPath", (uint)0, (uint)200, "+dc:title" };
      IList<object> output;
      UPnPError error = browse.InvokeAction(input, out output, false, CreateContext());
      Console.WriteLine("Error: {0}", error);
      Console.WriteLine("Output: {0}", output);
      Console.WriteLine("Output list: [{0}]", string.Join(",", output));
    }
  }
}
