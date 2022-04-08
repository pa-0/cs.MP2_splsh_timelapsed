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

using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;
using MP2BootstrapperApp.ActionPlans;
using MP2BootstrapperApp.BundlePackages;
using MP2BootstrapperApp.BundlePackages.Plugins;
using MP2BootstrapperApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MP2BootstrapperApp.WizardSteps
{
  /// <summary>
  /// Custom install step that provides optional plugins that can be individually selected for installation.
  /// </summary>
  public class InstallCustomPluginsStep : AbstractInstallStep, IStep
  {
    protected static readonly PluginBase[] PLUGINS = new PluginBase[] { new TvService3(), new TvService35() };

    public static IEnumerable<PluginBase> GetAvailablePlugins(IPlan plan, IEnumerable<IBundlePackageFeature> allFeatures)
    {
      return PLUGINS.Where(g => g.GetFeaturesWhereParentIsBeingInstalled(plan, allFeatures).Count > 0).ToList();
    }

    protected InstallPlan _installPlan;
    protected ICollection<IBundlePackageFeature> _allFeatures;

    public InstallCustomPluginsStep(IBootstrapperApplicationModel bootstrapperApplicationModel, InstallPlan installPlan)
      : base(bootstrapperApplicationModel)
    {
      _installPlan = installPlan;
      _allFeatures = _bootstrapperApplicationModel.MainPackage.Features;
      AvailablePlugins = GetAvailablePlugins(_installPlan, _allFeatures).ToList();
      SelectedPlugins = new List<PluginBase>();
    }

    /// <summary>
    /// All plugins available for installation.
    /// </summary>
    public ICollection<PluginBase> AvailablePlugins { get; }

    /// <summary>
    /// Plugins that have been selected for installation.
    /// </summary>
    public ICollection<PluginBase> SelectedPlugins { get; protected set; }

    public bool CanGoBack()
    {
      return true;
    }

    public bool CanGoNext()
    {
      return true;
    }

    public IStep Next()
    {
      ICollection<IBundlePackageFeature> allFeatures = _bootstrapperApplicationModel.MainPackage.Features;

      List<string> pluginsPlanned = new List<string>();
      foreach (PluginBase plugin in SelectedPlugins)
      {
        IEnumerable<string> conflictingPlugins = pluginsPlanned.Where(p => plugin.ConflictsWith(p));
        if (conflictingPlugins.Any())
        {
          _bootstrapperApplicationModel.LogMessage(LogLevel.Error, $"Skipping conflicting plugin, '{plugin.Id}' conflicts with '{string.Join(",", conflictingPlugins.ToArray())}'");
          continue;
        }

        foreach (string featureId in plugin.GetFeaturesWhereParentIsBeingInstalled(_installPlan, allFeatures))
          _installPlan.PlanFeature(featureId);

        pluginsPlanned.Add(plugin.Id);
      }      
      return new InstallCustomPropertiesStep(_bootstrapperApplicationModel, _installPlan);
    }
  }
}
