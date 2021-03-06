﻿using mteModels.Models;
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
    public class GuidesWorkersViewModel : BindableBase, IDialogAware
    {
        private int _workersId;

        private ObservableCollection<Enterprises> _enterprisesList;
        public ObservableCollection<Enterprises> EnterprisesList
        {
            get { return _enterprisesList; }
            set { SetProperty(ref _enterprisesList, value); }
        }

        private ObservableCollection<Posts> _postsList;
        public ObservableCollection<Posts> PostsList
        {
            get { return _postsList; }
            set { SetProperty(ref _postsList, value); }
        }

        private int _workersEnterprisesId;
        public int WorkersEnterprisesId
        {
            get { return _workersEnterprisesId; }
            set { SetProperty(ref (_workersEnterprisesId), value); }
        }
        private int _workersPostsId;
        public int WorkersPostsId
        {
            get { return _workersPostsId; }
            set { SetProperty(ref (_workersPostsId), value); }
        }
        private string _workersName;
        public string WorkersName
        {
            get { return _workersName; }
            set { SetProperty(ref (_workersName), value); }
        }
        private string _workersFirstName;
        public string WorkersFirstName
        {
            get { return _workersFirstName; }
            set { SetProperty(ref (_workersFirstName), value); }
        }
        private string _workersLastName;
        public string WorkersLastName
        {
            get { return _workersLastName; }
            set { SetProperty(ref (_workersLastName), value); }
        }

        public DelegateCommand<object> CloseGuidesPopupCommand { get; set; }
        private void CloseGuidesPopup(object Parameters)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        public DelegateCommand<object> ApplyGuidesPopupCommand { get; set; }
        private void ApplyGuidesPopup(object Parameters)
        {
            int sres = SessionsHelper.GuidesItemSave(new Workers()
            {
                Id = _workersId,
                Name = WorkersName,
                FirstName = WorkersFirstName,
                LastName = WorkersLastName,
                EnterprisesId= WorkersEnterprisesId,
                PostsId= WorkersPostsId
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
            Title = "Сотрудник / " + (parameters.Count > 0 ? "Изменение" : "Создание");
            if (parameters.Count > 0)
            {
                var value = parameters.GetValue<Workers>("Item");
                if (value != null)
                {
                    _workersId = value.Id;
                    WorkersName = value.Name;
                    WorkersFirstName = value.FirstName;
                    WorkersLastName = value.LastName;
                    WorkersEnterprisesId = value.EnterprisesId;
                    WorkersPostsId = value.PostsId;
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

        public GuidesWorkersViewModel(IRegionManager RegionManager)
        {
            EnterprisesList = new ObservableCollection<Enterprises>();
            EnterprisesList.AddRange(SessionsHelper.GetEnterprisesList());

            PostsList = new ObservableCollection<Posts>();
            PostsList.AddRange(SessionsHelper.GetPostsList());

            ApplyGuidesPopupCommand = new DelegateCommand<object>(ApplyGuidesPopup);
            CloseGuidesPopupCommand = new DelegateCommand<object>(CloseGuidesPopup);
        }
    }
}
