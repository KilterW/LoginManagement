using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LoginManagement.Common
{
    public class PasswordHelper
    {
        static bool _isUpdating = false;
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password",typeof(string),typeof(PasswordHelper),
                new FrameworkPropertyMetadata("",new PropertyChangedCallback(OnPropertyChanged)));

        public static string GetPassword(DependencyObject d)
        {
            return d.GetValue(PasswordProperty).ToString();
        }

        public static void SetPassword(DependencyObject d,string value)
        {
            d.SetValue(PasswordProperty,value);
        }

        private static void OnPropertyChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
           PasswordBox passwordBox = d as PasswordBox;
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
            if (!_isUpdating)
                passwordBox.Password = e.NewValue?.ToString();
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            _isUpdating = true;
            SetPassword(passwordBox, passwordBox.Password);
            _isUpdating = false;
        }
    }
}
