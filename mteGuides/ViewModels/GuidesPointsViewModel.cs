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
    public class GuidesPointsViewModel : BindableBase, IDialogAware
    {
        private int _pointsId;

        private string _pointsName;
        public string PointsName
        {
            get { return _pointsName; }
            set { SetProperty(ref (_pointsName), value); }
        }

        private ObservableCollection<PointTypes> _pointTypesList;
        public ObservableCollection<PointTypes> PointTypesList
        {
            get { return _pointTypesList; }
            set { SetProperty(ref _pointTypesList, value); }
        }

        private int _pointsPointTypesId;
        public int PointsPointTypesId
        {
            get { return _pointsPointTypesId; }
            set { SetProperty(ref (_pointsPointTypesId), value); }
        }

        public DelegateCommand<object> CloseGuidesPopupCommand { get; set; }
        private void CloseGuidesPopup(object Parameters)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        public DelegateCommand<object> ApplyGuidesPopupCommand { get; set; }
        private void ApplyGuidesPopup(object Parameters)
        {
            int sres = SessionsHelper.PointsSaveChanges(new Points()
            {
                Id = _pointsId,
                Name = PointsName,
                PointTypesId = PointsPointTypesId
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
            Title = "Остановка / " + (parameters.Count > 0 ? "Изменение" : "Создание");
            if (parameters.Count > 0)
            {
                var value = parameters.GetValue<Points>("Item");
                if (value != null)
                {
                    _pointsId = value.Id;
                    PointsName = value.Name;
                    PointsPointTypesId = value.PointTypesId;
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

        public GuidesPointsViewModel(IRegionManager RegionManager)
        {
            PointTypesList = new ObservableCollection<PointTypes>();
            PointTypesList.AddRange(SessionsHelper.GetPointTypesList());

            ApplyGuidesPopupCommand = new DelegateCommand<object>(ApplyGuidesPopup);
            CloseGuidesPopupCommand = new DelegateCommand<object>(CloseGuidesPopup);
        }
    }
}
