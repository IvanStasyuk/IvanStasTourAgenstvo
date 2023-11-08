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

namespace IvanStasTourAgenstvo.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autorisation.xaml
    /// </summary>
    public partial class Autorisation : Page
    {
        public Autorisation()
        {
            InitializeComponent();
        }

        private void Regbtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.Navigate(new Registration());
        }

        private void Vhod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var UserObj = TourAgentEntities.GetContext().User.FirstOrDefault(x => x.UserLogin == Login.Text && x.UserPassword == Password.Text);
                if (UserObj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (CaptchaInput.Text == "")
                {
                    MessageBox.Show("Введите капчу", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (CaptchaInput.Text == "ABCDEF")
                {
                    MessageBox.Show("Капча верна", "Авторизация успешна", MessageBoxButton.OK, MessageBoxImage.Information);
                    Manager._currentUser = UserObj;

                    switch (UserObj.RoleID)
                    {
                        case 1:
                            MessageBox.Show("Здравствуй, Администратор " + UserObj.UserName + "!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager.MyFrame.Navigate(new ListTours());
                            break;
                        case 2:
                            MessageBox.Show("Здравствуй, Менеджер " + UserObj.UserName + "!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager.MyFrame.Navigate(new ListTours());
                            break;
                        case 3:
                            MessageBox.Show("Здравствуй, Клиент " + UserObj.UserName + "!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager.MyFrame.Navigate(new ListTours());
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены " + UserObj.UserName + "!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager.MyFrame.Navigate(new ListTours());
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString() + "Критическая ошибка приложения", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
