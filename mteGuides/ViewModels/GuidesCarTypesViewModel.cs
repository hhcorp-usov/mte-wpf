using mteModels.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace mteGuides.ViewModels
{
    public class GuidesCarTypesViewModel : BindableBase, IDialogAware
    {
        private int _carTypesId;

        private string _carTypesName;
        public string CarTypesName
        {
            get { return _carTypesName; }
            set { SetProperty(ref (_carTypesName), value); }
        }

        public DelegateCommand<object> CloseGuidesPopupCommand { get; set; }
        private void CloseGuidesPopup(object Parameters)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        public DelegateCommand<object> ApplyGuidesPopupCommand { get; set; }
        private void ApplyGuidesPopup(object Parameters)
        {
            int sres = SessionsHelper.GuidesItemSave(new CarTypes()
            {
                Id = _carTypesId,
                Name = CarTypesName
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
            Title = "Марки ТС / " + (parameters.Count > 0 ? "Изменение" : "Создание");
            if (parameters.Count > 0)
            {
                var value = parameters.GetValue<CarTypes>("Item");
                if (value != null)
                {
                    _carTypesId = value.Id;
                    CarTypesName = value.Name;
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

        public GuidesCarTypesViewModel(IRegionManager RegionManager)
        {
            ApplyGuidesPopupCommand = new DelegateCommand<object>(ApplyGuidesPopup);
            CloseGuidesPopupCommand = new DelegateCommand<object>(CloseGuidesPopup);
        }
    }
}
