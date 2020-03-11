using MahApps.Metro.Controls.Dialogs;
using mte.Models;
using mte.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace mte.ViewModels
{
    public class MainWindowViewModel : BindableBase, INotifyPropertyChanged
    {
        private string _title = "MTE";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private MenuNavigatorItem _menuNavSelectedItem;
        public MenuNavigatorItem MenuNavSelectedItem
        {
            get { return _menuNavSelectedItem; }
            set
            {
                _menuNavSelectedItem = value;
                RaisePropertyChanged("MenuNavSelectedItem");
            }
        }

        public DelegateCommand<object> MenuNavigatorSelectedItemCommand { get; set; }

        private void MenuNavigatorSelectedItem(object parm)
        {
            if (parm is MenuNavigatorItem)
            {
                MenuNavigatorItem SelectedMenuItem = (parm as MenuNavigatorItem);
                switch (SelectedMenuItem.Id)
                {
                    case 1:
                        GuidesDataList = CurrentSession.GetEnterprisesList();
                        break;
                    case 2:
                        GuidesDataList = CurrentSession.GetUsersList();
                        break;
                }
            }
        }

        private IReadOnlyList<IDataList> _guidesDataList;
        public IReadOnlyList<IDataList> GuidesDataList
        {
            get { return _guidesDataList; }
            set { SetProperty(ref _guidesDataList, value); }
        }



        public string CurrentSessionUserName
        {
            get { return CurrentSession.CurrentUser.Name; }
        }

        public MainWindowViewModel()
        {
            MenuNavigatorSelectedItemCommand = new DelegateCommand<object>(MenuNavigatorSelectedItem);
        }
    }
}
