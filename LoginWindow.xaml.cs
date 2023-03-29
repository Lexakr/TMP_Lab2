﻿using System.IO;
using System.Windows;

namespace TMP_Lab2
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginWindowVM();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = usernameTextBox.Text;
            string password = passwordBox.Password.ToString();

            using (StreamReader reader = new("users.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(' ');
                    string userLogin = data[0];
                    string userPassword = data[1];
                    if ((login == userLogin) && (password == userPassword))
                    {
                        string menuName = data[2];

                        MainWindow window = new(menuName);
                        window.Show();
                        this.Close();

                        return;
                    }
                }
            }
            MessageBox.Show("Неверный логин или пароль");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
