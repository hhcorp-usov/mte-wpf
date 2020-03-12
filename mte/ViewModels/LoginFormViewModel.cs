using mteModels.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mte.ViewModels
{
    public class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return values.ToArray();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("No two way conversion, one way binding only.");
        }
    }

    public class LoginFormViewModel : BindableBase, INotifyPropertyChanged
    {
        public event EventHandler OnRequestClose;

        public string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }

        private ObservableCollection<Users> _usersList;
        public ObservableCollection<Users> UsersList
        {
            get { return _usersList; }
            set { SetProperty(ref _usersList, value); }
        }

        public DelegateCommand<object[]> ButtonLoginCommand { get; set; }
        public DelegateCommand<object> ButtonCancelCommand { get; set; }

        private void ButtonLogin(object[] parameter)
        {
            string pwd_etalon = (parameter[0] as Users).Password;
            string pwd_input = SHA512((parameter[1] as PasswordBox).Password + (parameter[0] as Users).Login);
            if (pwd_etalon == pwd_input & parameter[0] is Users)
            {
                CurrentSession.CurrentUser = (parameter[0] as Users);
                OnRequestClose(this, new EventArgs());
            }
        }

        private void ButtonCancel(object parm)
        {
            CurrentSession.CurrentUser = null;
            OnRequestClose(this, new EventArgs());
        }

        public LoginFormViewModel()
        {
            ButtonLoginCommand = new DelegateCommand<object[]>(ButtonLogin);
            ButtonCancelCommand = new DelegateCommand<object>(ButtonCancel);

            UsersList = new ObservableCollection<Users>();
            UsersList.AddRange(CurrentSession.GetUsersList());
        }
    }
}
