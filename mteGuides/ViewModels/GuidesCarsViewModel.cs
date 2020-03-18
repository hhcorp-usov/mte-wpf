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
    public class GuidesCarsViewModel : BindableBase, IDialogAware
    {
        private int _carsId;

        private ObservableCollection<Enterprises> _enterprisesList;
        public ObservableCollection<Enterprises> EnterprisesList
        {
            get { return _enterprisesList; }
            set { SetProperty(ref _enterprisesList, value); }
        }

        private ObservableCollection<CarTypes> _carTypesList;
        public ObservableCollection<CarTypes> CarTypesList
        {
            get { return _carTypesList; }
            set { SetProperty(ref _carTypesList, value); }
        }

        private int _carsEnterprisesId;
        public int CarsEnterprisesId
        {
            get { return _carsEnterprisesId; }
            set { SetProperty(ref (_carsEnterprisesId), value); }
        }
        private int _carsCarTypesId;
        public int CarsCarTypesId
        {
            get { return _carsCarTypesId; }
            set { SetProperty(ref (_carsCarTypesId), value); }
        }
        private string _carsINomer;
        public string CarsINomer
        {
            get { return _carsINomer; }
            set { SetProperty(ref (_carsINomer), value); }
        }
        private string _carsSNomer;
        public string CarsSNomer
        {
            get { return _carsSNomer; }
            set { SetProperty(ref (_carsSNomer), value); }
        }

        public DelegateCommand<object> CloseGuidesPopupCommand { get; set; }
        private void CloseGuidesPopup(object Parameters)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        public DelegateCommand<object> ApplyGuidesPopupCommand { get; set; }
        private void ApplyGuidesPopup(object Parameters)
        {
            int sres = SessionsHelper.CarsSaveChanges(new Cars()
            {
                Id = _carsId,
                INomer = CarsINomer,
                SNomer = CarsSNomer,
                CarTypesId = CarsCarTypesId,
                EnterprisesId = CarsEnterprisesId
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
                var value = parameters.GetValue<Cars>("Item");
                if (value != null)
                {
                    _carsId = value.Id;
                    CarsINomer = value.INomer;
                    CarsSNomer = value.SNomer;
                    CarsEnterprisesId = value.EnterprisesId;
                    CarsCarTypesId = value.CarTypesId;
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

        public GuidesCarsViewModel(IRegionManager RegionManager)
        {
            ApplyGuidesPopupCommand = new DelegateCommand<object>(ApplyGuidesPopup);
            CloseGuidesPopupCommand = new DelegateCommand<object>(CloseGuidesPopup);

            EnterprisesList = new ObservableCollection<Enterprises>();
            EnterprisesList.AddRange(SessionsHelper.GetEnterprisesList());

            CarTypesList = new ObservableCollection<CarTypes>();
            CarTypesList.AddRange(SessionsHelper.GetCarTypesList());
        }
    }
}
