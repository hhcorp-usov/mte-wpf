using mte.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Unity.Injection;

namespace mte.ViewModels
{
    public class SplashScreenFormViewModel : BindableBase
    {
        public DelegateCommand<object> CloseSplashScreenCommand { get; set; }

        private void CloseSplashScreen(object parm)
        {
            App.Current.Windows[0].Close();
        }

        public SplashScreenFormViewModel()
        {
            CloseSplashScreenCommand = new DelegateCommand<object>(CloseSplashScreen);
        }
    }
}
