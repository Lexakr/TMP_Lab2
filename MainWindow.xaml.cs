using System.Windows;
using WpfApp2;

namespace TMP_Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string filename)
        {
            InitializeComponent();

            MenuCreator newMenu = new(filename);
            mainMenu.ItemsSource = newMenu.CreatedMenu.Items;
        }
    }
}

