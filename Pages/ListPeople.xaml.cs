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
    /// Логика взаимодействия для ListPeople.xaml
    /// </summary>
    public partial class ListPeople : Page
    {
        public ListPeople()
        {
            InitializeComponent();
            DataGridPeople.ItemsSource = TourAgentEntities.GetContext().User.ToList();
        }

        private void DelPeople_Click(object sender, RoutedEventArgs e)
        {
            var UserRemoving = DataGridPeople.SelectedItems.Cast<User>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {UserRemoving.Count()} элементов",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TourAgentEntities.GetContext().User.RemoveRange(UserRemoving);
                    TourAgentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DataGridPeople.ItemsSource = TourAgentEntities.GetContext().Tour.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TourAgentEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataGridPeople.ItemsSource = TourAgentEntities.GetContext().User.ToList();
            }
        }
    }
}
