using mteModels.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace mte.ViewModels
{
    public class MasterWindowViewModel : BindableBase, INotifyPropertyChanged
    {
        private readonly IRegionManager CurrentRegionManager;

        public string CurrentSessionUserView
        {
            get { return SessionsHelper.GetCurrentUserView(); }
        }

        public DelegateCommand<string> SwitchApplicationModeCommand { get; }
        private void SwitchApplicationMode(string RegionViewName)
        {
            if (RegionViewName != null) CurrentRegionManager.RequestNavigate("ContentRegion", RegionViewName); 
        }

        public MasterWindowViewModel(IRegionManager RegionManager)
        {
            CurrentRegionManager = RegionManager;
            SwitchApplicationModeCommand = new DelegateCommand<string>(SwitchApplicationMode);
        }
    }
}