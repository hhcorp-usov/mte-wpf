using ControlzEx.Standard;
using mteModels.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media.Effects;

namespace mteGuides.ViewModels
{
    public class DefaultGuidesPageViewModel : BindableBase, INotifyPropertyChanged
    {
        private MenuNavigatorItem _guidesMenuSelectedItem;
        public MenuNavigatorItem GuidesMenuSelectedItem
        {
            get { return _guidesMenuSelectedItem; }
            set
            {
                _guidesMenuSelectedItem = value;
                RaisePropertyChanged("GuidesMenuSelectedItem");
            }
        }

        private IReadOnlyList<IDataList> _guidesDataItems;
        public IReadOnlyList<IDataList> GuidesDataItems
        {
            get { return _guidesDataItems; }
            set { SetProperty(ref (_guidesDataItems), value); }
        }

        private string _guidesName;
        public string GuidesName 
        {
            get { return _guidesName; }
            set { SetProperty(ref (_guidesName), value); }
        }

        public DelegateCommand<object> GuidesMenuNavigatorSelectedItemCommand { get; set; }
        private void GuidesMenuNavigatorSelectedItem(object MenuItem)
        {
            if (MenuItem is MenuNavigatorItem)
            {
                var _currentMenuItem = (MenuItem as MenuNavigatorItem);
                GuidesDataItems = CurrentSession.GetGuidesDataForDataGrid(_currentMenuItem.Id);
                GuidesName = _currentMenuItem.Text;
            }
        }

        private object _guidesDataSelectedItems;
        public object GuidesDataSelectedItems
        {
            get { return _guidesDataSelectedItems; }
            set { SetProperty(ref (_guidesDataSelectedItems), value); }
        }

        public DelegateCommand<object> GuidesAddItemCommand { get; set; }
        public DelegateCommand<object> GuidesDeleteItemCommand { get; set; }
        private void GuidesAddItem(object SelectedItem)
        {
            DialogParameters param = new DialogParameters();
            switch (GuidesMenuSelectedItem.Id) 
            {
                case "enterprises":
                    param.Add("Item", SelectedItem);
                    _currentDialogService.ShowDialog("GuidesEnterprises", param, r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            GuidesDataItems = CurrentSession.GetGuidesDataForDataGrid(GuidesMenuSelectedItem.Id);
                        }
                    });
                    break;

                case "posts":
                    param.Add("Item", SelectedItem);
                    _currentDialogService.ShowDialog("GuidesPosts", param, r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            GuidesDataItems = CurrentSession.GetGuidesDataForDataGrid(GuidesMenuSelectedItem.Id);
                        }
                    });
                    break;

                case "workers":
                    param.Add("Item", SelectedItem);
                    _currentDialogService.ShowDialog("GuidesWorkers", param, r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            GuidesDataItems = CurrentSession.GetGuidesDataForDataGrid(GuidesMenuSelectedItem.Id);
                        }
                    });
                    break;
            }
        }

        private void GuidesDeleteItem(object SelectedItem)
        {
        }

        private readonly IDialogService _currentDialogService;

        public DefaultGuidesPageViewModel(IDialogService DialogService)
        {
            _currentDialogService = DialogService;
            GuidesMenuNavigatorSelectedItemCommand = new DelegateCommand<object>(GuidesMenuNavigatorSelectedItem);
            GuidesAddItemCommand = new DelegateCommand<object>(GuidesAddItem);
            GuidesDeleteItemCommand = new DelegateCommand<object>(GuidesDeleteItem);
        }
    }
}