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
    public class GuidesPointTypesViewModel : BindableBase, IDialogAware
    {
        private int _pointTypesId;

        private string _pointTypesName;
        public string PointTypesName
        {
            get { return _pointTypesName; }
            set { SetProperty(ref (_pointTypesName), value); }
        }

        private string _pointTypesShortName;
        public string PointTypesShortName
        {
            get { return _pointTypesShortName; }
            set { SetProperty(ref (_pointTypesShortName), value); }
        }
        
        public DelegateCommand<object> CloseGuidesPopupCommand { get; set; }
        private void CloseGuidesPopup(object Parameters)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        public DelegateCommand<object> ApplyGuidesPopupCommand { get; set; }
        private void ApplyGuidesPopup(object Parameters)
        {
            int sres = SessionsHelper.PointTypesSaveChanges(new PointTypes()
            {
                Id = _pointTypesId,
                Name = PointTypesName,
                ShortName = PointTypesShortName
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
            Title = "Тип остановочного пункта / " + (parameters.Count > 0 ? "Изменение" : "Создание");
            if (parameters.Count > 0)
            {
                var value = parameters.GetValue<PointTypes>("Item");
                if (value != null)
                {
                    _pointTypesId = value.Id;
                    PointTypesName = value.Name;
                    PointTypesShortName = value.ShortName;
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

        public GuidesPointTypesViewModel(IRegionManager RegionManager)
        {
            ApplyGuidesPopupCommand = new DelegateCommand<object>(ApplyGuidesPopup);
            CloseGuidesPopupCommand = new DelegateCommand<object>(CloseGuidesPopup);
        }
    }
}
