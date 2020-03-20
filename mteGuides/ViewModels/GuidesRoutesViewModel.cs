using mteModels.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace mteGuides.ViewModels
{
    public class GuidesRoutesViewModel : BindableBase, IDialogAware
    {
        private int _routesId;

        private ObservableCollection<Enterprises> _enterprisesList;
        public ObservableCollection<Enterprises> EnterprisesList
        {
            get { return _enterprisesList; }
            set { SetProperty(ref _enterprisesList, value); }
        }

        private int _routesEnterprisesId;
        public int RoutesEnterprisesId
        {
            get { return _routesEnterprisesId; }
            set { SetProperty(ref (_routesEnterprisesId), value); }
        }
        private string _routesName;
        public string RoutesName
        {
            get { return _routesName; }
            set { SetProperty(ref (_routesName), value); }
        }
        private string _routesNomer;
        public string RoutesNomer
        {
            get { return _routesNomer; }
            set { SetProperty(ref (_routesNomer), value); }
        }
        private string _routesLineName;
        public string RoutesLineName
        {
            get { return _routesLineName; }
            set { SetProperty(ref (_routesLineName), value); }
        }
        private string _routesBackName;
        public string RoutesBackName
        {
            get { return _routesBackName; }
            set { SetProperty(ref (_routesBackName), value); }
        }

        public DelegateCommand<object> CloseGuidesPopupCommand { get; set; }
        private void CloseGuidesPopup(object Parameters)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        public DelegateCommand<object> ApplyGuidesPopupCommand { get; set; }
        private void ApplyGuidesPopup(object Parameters)
        {
            int sres = SessionsHelper.GuidesItemSave(new Routes()
            {
                Id = _routesId,
                Name = RoutesName,
                Nomer = RoutesNomer,
                LineName = RoutesLineName,
                BackName = RoutesBackName,
                EnterprisesId = RoutesEnterprisesId
            });
            RaiseRequestClose(new DialogResult(sres > 0 ? ButtonResult.OK : ButtonResult.No));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed() { }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = "Транспортное средство / " + (parameters.Count > 0 ? "Изменение" : "Создание");
            if (parameters.Count > 0)
            {
                var value = parameters.GetValue<Routes>("Item");
                if (value != null)
                {
                    _routesId = value.Id;
                    RoutesName = value.Name;
                    RoutesNomer = value.Nomer;
                    RoutesLineName = value.LineName;
                    RoutesBackName = value.BackName;
                    RoutesEnterprisesId = value.EnterprisesId;
                }
            }
        }

        public event Action<IDialogResult> RequestClose;

        public virtual void RaiseRequestClose(IDialogResult DialogResult)
        {
            RequestClose?.Invoke(DialogResult);
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref (_title), value); }
        }

        public GuidesRoutesViewModel(IRegionManager RegionManager)
        {
            ApplyGuidesPopupCommand = new DelegateCommand<object>(ApplyGuidesPopup);
            CloseGuidesPopupCommand = new DelegateCommand<object>(CloseGuidesPopup);

            EnterprisesList = new ObservableCollection<Enterprises>();
            EnterprisesList.AddRange(SessionsHelper.GetEnterprisesList());
        }
    }
}
