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

using WixToolset.Mba.Core;

namespace MP2BootstrapperApp.Models
{
  public class Package
  {
    public string Id { get; set; }

    public string DisplayName { get; set; }

    public string Description { get; set; }

    public string LocalizedDescription { get; set; }

    public long InstalledSize { get; set; }
    
    public string ImagePath { get; set; }
    
    public string InstalledVersion { get; set; }
    
    public string BundleVersion { get; set; }

    public RequestState RequestState { get; set; } 
    
    public PackageState PackageState { get; set; }

    public bool Uninstalling
    {
      get { return RequestState == RequestState.Absent; }
    }

    public bool Present
    {
      get { return PackageState == PackageState.Present && !Uninstalling; }
    }

    public bool Upgrading
    {
      get { return RequestState == RequestState.Present && !string.IsNullOrEmpty(InstalledVersion); }
    }

    public bool Installing
    {
      get { return RequestState == RequestState.Present && !Upgrading; }
    }
  }
}
