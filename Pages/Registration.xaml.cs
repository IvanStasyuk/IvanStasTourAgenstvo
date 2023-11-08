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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (TourAgentEntities.GetContext().User.Count(y => y.UserLogin == LoginBox.Text) > 0)
            {
                MessageBox.Show("Пользователь уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                User userBDJ = new User()
                {
                    UserLogin = LoginBox.Text,
                    UserPassword = PasswordBox.Text,
                    UserSurname = FamiliaBox.Text,
                    UserName = NameBox.Text,
                    UserPatronymic = PatronymicBox.Text,
                    RoleID = int.Parse(RolePeople.Text)
                };
                TourAgentEntities.GetContext().User.Add(userBDJ);
                TourAgentEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные добавлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
