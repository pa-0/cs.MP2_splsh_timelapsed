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

using System;
using System.Globalization;
using System.Xml.Linq;
using WixToolset.Mba.Core;

namespace MP2BootstrapperApp.BundlePackages
{
  /// <summary>
  /// 
  /// </summary>
  public class BundlePackage : IBundlePackage
  {
    protected string _packageIdString;
    protected PackageId _packageId;
    protected string _version;
    protected string _displayName;
    protected string _description;
    protected long _installedSize;
    protected string _installCondition;
    protected bool _vital;

    // Default to true unless explicitly set
    protected bool _evaluatedInstallCondition = true;

    public BundlePackage(XElement packageElement)
    {
      SetXmlProperties(packageElement);
    }

    private void SetXmlProperties(XElement packageElement)
    {
      _packageIdString = packageElement.Attribute("Package")?.Value;
      _packageId = Enum.TryParse(_packageIdString, out PackageId id) ? id : PackageId.Unknown;
      _displayName = packageElement.Attribute("DisplayName")?.Value;
      _description = packageElement.Attribute("Description")?.Value;
      _installCondition = packageElement.Attribute("InstallCondition")?.Value;
      _installedSize = long.TryParse(packageElement.Attribute("InstalledSize")?.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out long installedSize) ? installedSize : 0;
      _version = packageElement.Attribute("Version")?.Value;
      _vital = !string.Equals(packageElement.Attribute("Vital")?.Value, "no", StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public PackageId PackageId
    {
      get { return _packageId; }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string Id
    {
      get { return _packageIdString; }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string Version
    {
      get { return _version; }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string DisplayName
    {
      get { return _displayName; }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string Description
    {
      get { return _description; }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public long InstalledSize
    {
      get { return _installedSize; }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string InstallCondition
    {
      get { return _installCondition; }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool Vital
    {
      get { return _vital; }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool EvaluatedInstallCondition
    {
      get { return _evaluatedInstallCondition; }
      set { _evaluatedInstallCondition = value; }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string InstalledVersion { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public PackageState CurrentInstallState { get; set; }
  }
}
