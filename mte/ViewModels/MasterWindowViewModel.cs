using mte.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mte.ViewModels
{
    public class MasterWindowViewModel : BindableBase
    {
        public string CurrentSessionUserView
        {
            get { return CurrentSession.GetCurrentUserView(); }
        }

        public MasterWindowViewModel()
        {

        }
    }
}