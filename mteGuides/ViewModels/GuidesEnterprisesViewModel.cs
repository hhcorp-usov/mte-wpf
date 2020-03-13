﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using mteModels.Models;
using Prism.Services.Dialogs;
using System.Data.Entity;

namespace mteGuides.ViewModels
{
    public class GuidesEnterprisesViewModel : BindableBase, IDialogAware
    {
        private int _enteprisesId;

        private string _enterprisesName;
        public string EnterprisesName
        {
            get { return _enterprisesName; }
            set { SetProperty(ref (_enterprisesName), value); }
        }

        private string _enterprisesInn;
        public string EnterprisesInn
        {
            get { return _enterprisesInn; }
            set { SetProperty(ref (_enterprisesInn), value); }
        }

        public DelegateCommand<object> CloseGuidesPopupCommand { get; set; }
        private void CloseGuidesPopup(object Parameters)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        public DelegateCommand<object> ApplyGuidesPopupCommand { get; set; }
        private void ApplyGuidesPopup(object Parameters)
        {
            Enterprises item = new Enterprises();
            //IDialogParameters param = new IDialogParameters();

            //item.SaveChanges(param);



            if (_enteprisesId > 0)
            {
                var Item = CurrentSession._dbContext.Enterprises.Where(w => w.Id == _enteprisesId).FirstOrDefault();
                Item.Inn = EnterprisesInn;
                Item.Name = EnterprisesName;
                CurrentSession._dbContext.Entry(Item).State = EntityState.Modified;
            }
            else
            {
                Enterprises Item = new Enterprises()
                {
                    Inn = _enterprisesInn, 
                    Name = _enterprisesName
                };
                CurrentSession._dbContext.Enterprises.Add(Item);
            }
            CurrentSession._dbContext.SaveChanges();
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }

        public bool CanCloseDialog() 
        {
            return true;
        }

        public void OnDialogClosed() {}
        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.Count > 0)
            {
                var value = parameters.GetValue<Enterprises>("Item");
                if (value != null)
                {
                    _enteprisesId = value.Id;
                    EnterprisesName = value.Name;
                    EnterprisesInn = value.Inn;
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

        public GuidesEnterprisesViewModel(IRegionManager RegionManager)
        {
            Title = "Организация";
            ApplyGuidesPopupCommand = new DelegateCommand<object>(ApplyGuidesPopup);
            CloseGuidesPopupCommand = new DelegateCommand<object>(CloseGuidesPopup);
        }
    }
}
