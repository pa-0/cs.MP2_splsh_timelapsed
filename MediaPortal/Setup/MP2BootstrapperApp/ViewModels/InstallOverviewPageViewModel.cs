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

using MP2BootstrapperApp.ActionPlans;
using MP2BootstrapperApp.BundlePackages;
using MP2BootstrapperApp.Models;
using MP2BootstrapperApp.WizardSteps;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WixToolset.Dtf.WindowsInstaller;
using WixToolset.Mba.Core;

namespace MP2BootstrapperApp.ViewModels
{
  public class InstallOverviewPageViewModel : InstallWizardPageViewModelBase<InstallOverviewStep>
  {
    public InstallOverviewPageViewModel(InstallOverviewStep step)
      : base(step)
    {
      Header = "[InstallOverviewPageView.Header]";
      ButtonNextContent = "[InstallOverviewPageView.InstallButton]";

      Packages = new ObservableCollection<Package>();
      AddPackages(step.BootstrapperApplicationModel.BundlePackages, step.ActionPlan);
    }

    public ObservableCollection<Package> Packages { get; }

    protected void AddPackages(IEnumerable<IBundlePackage> bundlePackages, IPlan plan)
    {
      foreach (IBundlePackage bundlePackage in bundlePackages)
      {
        // The main package is added as individual features below, so don't add it here
        if (bundlePackage.PackageId != _step.BootstrapperApplicationModel.MainPackage.PackageId)
        {
          RequestState? requestState = plan.GetRequestedInstallState(bundlePackage);
          Packages.Add(bundlePackage.CreatePackageModel(requestState));
        }
      }

      IBundleMsiPackage mainPackage = _step.BootstrapperApplicationModel.MainPackage;
      foreach (IBundlePackageFeature feature in mainPackage.Features)
      {
        // Only add features that are direct children of the main feature
        if (!feature.Attributes.HasFlag(FeatureAttributes.UIDisallowAbsent))
          Packages.Add(feature.CreateFeatureModel(mainPackage.Version, mainPackage.InstalledVersion, plan.GetRequestedInstallState(feature)));
      }
    }
  }
}
