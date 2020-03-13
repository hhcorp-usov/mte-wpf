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
    public class GuidesPostsViewModel : BindableBase, IDialogAware
    {
        private int _postsId;

        private string _postsName;
        public string PostsName
        {
            get { return _postsName; }
            set { SetProperty(ref (_postsName), value); }
        }

        public DelegateCommand<object> CloseGuidesPopupCommand { get; set; }
        private void CloseGuidesPopup(object Parameters)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        public DelegateCommand<object> ApplyGuidesPopupCommand { get; set; }
        private void ApplyGuidesPopup(object Parameters)
        {
            if (_postsId > 0)
            {
                var Item = CurrentSession._dbContext.Posts.Where(w => w.Id == _postsId).FirstOrDefault();
                Item.Name = PostsName;
                CurrentSession._dbContext.Entry(Item).State = EntityState.Modified;
            }
            else
            {
                Posts Item = new Posts()
                {
                    Name = _postsName
                };
                CurrentSession._dbContext.Posts.Add(Item);
            }
            CurrentSession._dbContext.SaveChanges();
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed() { }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.Count > 0)
            {
                var value = parameters.GetValue<Posts>("Item");
                if (value != null)
                {
                    _postsId = value.Id;
                    PostsName = value.Name;
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

        public GuidesPostsViewModel(IRegionManager RegionManager)
        {
            Title = "Должность";
            ApplyGuidesPopupCommand = new DelegateCommand<object>(ApplyGuidesPopup);
            CloseGuidesPopupCommand = new DelegateCommand<object>(CloseGuidesPopup);
        }
    }
}
