using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using mteModels.Models;

namespace mteGuides.ViewModels
{
    public class GuidesEnterprisesViewModel : BindableBase
    {
        private string _enterprisesName;
        private string _enterprisesInn;
        public string EnterprisesName
        {
            get { return _enterprisesName; }
            set { SetProperty(ref (_enterprisesName), value); }
        }
        public string EnterprisesInn
        {
            get { return _enterprisesInn; }
            set { SetProperty(ref (_enterprisesInn), value); }
        }

        public DelegateCommand<object> CloseGuidesPopupCommand { get; set; }
        private void CloseGuidesPopup(object Parameters)
        {
            _currentRegionManager.Regions["PopupRegion"].RemoveAll();
        }

        public DelegateCommand<object> ApplyGuidesPopupCommand { get; set; }
        private void ApplyGuidesPopup(object Parameters)
        {
            Enterprises Item = new Enterprises()
            {
                Inn = _enterprisesInn, 
                Name = _enterprisesName                
            };
            CurrentSession._dbContext.Enterprises.Add(Item);
            CurrentSession._dbContext.SaveChanges();
            CloseGuidesPopup(Parameters);
        }

        public string WindowHeader
        {
            get { return "Организация"; }
        }

        private readonly IRegionManager _currentRegionManager;
        public GuidesEnterprisesViewModel(IRegionManager RegionManager)
        {
            _currentRegionManager = RegionManager;

            ApplyGuidesPopupCommand = new DelegateCommand<object>(ApplyGuidesPopup);
            CloseGuidesPopupCommand = new DelegateCommand<object>(CloseGuidesPopup);
        }

    }
}
