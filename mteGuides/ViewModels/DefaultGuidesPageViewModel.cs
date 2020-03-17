using mteModels.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace mteGuides.ViewModels
{
    public class DefaultGuidesPageViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<DataGridColumn> _dataGridColumns;
        public ObservableCollection<DataGridColumn> DataGridColumns
        {
            get { return _dataGridColumns; }
            set { SetProperty(ref (_dataGridColumns), value); }
        }

        private MenuNavigatorItem _guidesMenuSelectedItem;
        public MenuNavigatorItem GuidesMenuSelectedItem
        {
            get { return _guidesMenuSelectedItem; }
            set { SetProperty(ref (_guidesMenuSelectedItem), value); }
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
                DataGridColumns = SessionsHelper.GetDataGridGuidesColumns(_currentMenuItem.Id);
                GuidesDataItems = SessionsHelper.GetDataGridGuidesItems(_currentMenuItem.Id);
                GuidesName = _currentMenuItem.Text;
            }
        }

        private object _guidesDataSelectedItems;
        public object GuidesDataSelectedItems
        {
            get { return _guidesDataSelectedItems; }
            set { SetProperty(ref (_guidesDataSelectedItems), value); }
        }

        public DelegateCommand GuidesAddItemCommand { get; set; }
        public DelegateCommand<object> GuidesEditItemCommand { get; set; }
        public DelegateCommand<object> GuidesDeleteItemCommand { get; set; }
        
        private void GuidesAddItem()
        {
            DialogParameters param = new DialogParameters();
            switch (GuidesMenuSelectedItem.Id) 
            {
                case GuidesElements.Enterprises:
                    _currentDialogService.ShowDialog("GuidesEnterprises", param, r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            GuidesDataItems = SessionsHelper.GetDataGridGuidesItems(GuidesMenuSelectedItem.Id);
                        }
                    });
                    break;

                case GuidesElements.Posts:
                    _currentDialogService.ShowDialog("GuidesPosts", param, r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            GuidesDataItems = SessionsHelper.GetDataGridGuidesItems(GuidesMenuSelectedItem.Id);
                        }
                    });
                    break;

                case GuidesElements.Workers:
                    _currentDialogService.ShowDialog("GuidesWorkers", param, r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            GuidesDataItems = SessionsHelper.GetDataGridGuidesItems(GuidesMenuSelectedItem.Id);
                        }
                    });
                    break;
            }
        }

        private void GuidesEditItem(object SelectedItem)
        {
            DialogParameters param = new DialogParameters();
            switch (GuidesMenuSelectedItem.Id)
            {
                case GuidesElements.Enterprises:
                    param.Add("Item", SelectedItem);
                    _currentDialogService.ShowDialog("GuidesEnterprises", param, r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            GuidesDataItems = SessionsHelper.GetDataGridGuidesItems(GuidesMenuSelectedItem.Id);
                        }
                    });
                    break;

                case GuidesElements.Posts:
                    param.Add("Item", SelectedItem);
                    _currentDialogService.ShowDialog("GuidesPosts", param, r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            GuidesDataItems = SessionsHelper.GetDataGridGuidesItems(GuidesMenuSelectedItem.Id);
                        }
                    });
                    break;

                case GuidesElements.Workers:
                    param.Add("Item", SelectedItem);
                    _currentDialogService.ShowDialog("GuidesWorkers", param, r =>
                    {
                        if (r.Result == ButtonResult.OK)
                        {
                            GuidesDataItems = SessionsHelper.GetDataGridGuidesItems(GuidesMenuSelectedItem.Id);
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
            GuidesAddItemCommand = new DelegateCommand(GuidesAddItem);
            GuidesEditItemCommand = new DelegateCommand<object>(GuidesEditItem);
            GuidesDeleteItemCommand = new DelegateCommand<object>(GuidesDeleteItem);
        }
    }
}