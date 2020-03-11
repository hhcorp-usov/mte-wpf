using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Threading;

namespace mte.Views
{
    /// <summary>
    /// Interaction logic for SplashScreenForm.xaml
    /// </summary>
    public partial class SplashScreenForm : MetroWindow
    {
        public SplashScreenForm()
        {
            InitializeComponent();
            //
            SplashScreenProgressRing.IsActive = true;
            SplashScreenTextBlock.Text = "";
        }

        public void SplashScreenText(string Text)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)delegate () {
                SplashScreenTextBlock.Text += "\n" + Text;
            });
        }

        public void SplashScreenProgress(bool Active) {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)delegate () {
                SplashScreenProgressRing.IsActive = Active;
            });
        }
    }
}
