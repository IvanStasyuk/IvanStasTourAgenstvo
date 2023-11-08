using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IvanStasTourAgenstvo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.MyFrame = MyFrame;
            Manager.MyFrame.Navigate(new Pages.Autorisation());
        }

        private void MyFrame_ContentRendered(object sender, EventArgs e)
        {
            if (Manager.MyFrame.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
                btnVhodGost.Visibility = Visibility.Hidden;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
                btnVhodGost.Visibility = Visibility.Visible;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.GoBack();
        }

        private void btnVhodGost_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добро пожаловать, гость!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            Manager.MyFrame.Navigate(new Pages.ListTours());
        }
    }
}
