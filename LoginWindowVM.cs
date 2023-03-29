using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace TMP_Lab2
{
    internal partial class LoginWindowVM : ObservableObject
    {
        [ObservableProperty]
        private string currentLanguage = InputLanguageManager.Current.CurrentInputLanguage.DisplayName;

        [ObservableProperty]
        private string capsLockStatus = Console.CapsLock ? "Включен Caps Losk!" : "";

        public LoginWindowVM()
        {
            InputLanguageManager.Current.InputLanguageChanged += InputLanguageChangedHandler;
            Keyboard.AddPreviewKeyDownHandler(App.Current.MainWindow, CapsLockStateHandler);
        }

        private void InputLanguageChangedHandler(object sender, InputLanguageEventArgs e)
        {
            CurrentLanguage = InputLanguageManager.Current.CurrentInputLanguage.DisplayName;
        }
        private void CapsLockStateHandler(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли клавиша Caps Lock
            CapsLockStatus = Keyboard.IsKeyToggled(Key.CapsLock) ? "Включен Caps Lock!" : "";
        }
    }
}
