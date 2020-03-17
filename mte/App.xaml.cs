using MahApps.Metro.Controls.Dialogs;
using mteModels.Models;
using mte.ViewModels;
using mte.Views;
using Prism;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Converters;

namespace mte
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // show splash
            SplashScreenForm splashScreenForm = new SplashScreenForm();
            splashScreenForm.Show();

            Task.Factory.StartNew(() =>
            {
                // empty
                splashScreenForm.SplashScreenText("Инициализация ...");
                System.Threading.Thread.Sleep(500);

                // db connect
                splashScreenForm.SplashScreenText("Соединение с базой данных ...");
                SessionsHelper.DatabaseConnect();

                /*
                // login form
                splashScreenForm.SplashScreenText("Идентификация пользователя ...");
                splashScreenForm.SplashScreenProgress(false);
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    LoginFormViewModel loginFormViewModel = new LoginFormViewModel();
                    LoginForm loginForm = new LoginForm();
                    loginForm.Owner = splashScreenForm;
                    loginForm.DataContext = loginFormViewModel;
                    loginFormViewModel.OnRequestClose += (s, ee) => loginForm.Close();
                    var r = loginForm.ShowDialog();
                });
                */

                SessionsHelper.CurrentUser = SessionsHelper.GetUsersList().First();

                if (SessionsHelper.CurrentUser != null)
                {
                    // empty
                    splashScreenForm.SplashScreenText("Применение параметров ...");
                    splashScreenForm.SplashScreenProgress(true);
                    System.Threading.Thread.Sleep(500);

                    this.Dispatcher.Invoke(() =>
                    {
                        base.OnStartup(e);
                        splashScreenForm.Close();
                    });
                }
                else Process.GetCurrentProcess().Kill();                    
            });
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MasterWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<mteGuides.mteGuidesModule>();
        }
    }
}
