using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Input;

namespace TMP_Lab2
{
    /// <summary>
    /// View Model для LoginWindow.cs.
    /// </summary>
    internal partial class LoginWindowVM : ObservableObject
    {
        /// <summary>
        /// Текущий язык ввода.
        /// </summary>
        [ObservableProperty]
        private string currentLanguage = InputLanguageManager.Current.CurrentInputLanguage.DisplayName;

        /// <summary>
        /// Статус нажатой клавиши Caps Lock.
        /// </summary>
        [ObservableProperty]
        private string capsLockStatus = Console.CapsLock ? "Включен Caps Losk!" : "";

        public LoginWindowVM()
        {
            // Подписываемся на события изменения языка ввода и нажатия клавиши Caps Lock.
            InputLanguageManager.Current.InputLanguageChanged += InputLanguageChangedHandler;
            Keyboard.AddPreviewKeyDownHandler(App.Current.MainWindow, CapsLockStateHandler);
        }

        private void InputLanguageChangedHandler(object sender, InputLanguageEventArgs e)
        {
            CurrentLanguage = InputLanguageManager.Current.CurrentInputLanguage.DisplayName;
        }
        private void CapsLockStateHandler(object sender, KeyEventArgs e)
        {
            CapsLockStatus = Keyboard.IsKeyToggled(Key.CapsLock) ? "Включен Caps Lock!" : "";
        }
    }
}
