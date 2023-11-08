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
    /// Логика взаимодействия для ListTours.xaml
    /// </summary>
    public partial class ListTours : Page
    {
        public ListTours()
        {
            InitializeComponent();
            DataGridTours.ItemsSource = TourAgentEntities.GetContext().Tour.ToList();
            if (Manager._currentUser == null)
            {
                btnAdd.IsEnabled = false;
                btnAdd.ToolTip = "У вас нет прав";
                btnDelete.IsEnabled = false;
                btnDelete.ToolTip = "У вас нет прав";
            }
            else
            {
                try
                {
                    switch (Manager._currentUser.RoleID)
                    {
                        case 1:
                            btnAdd.IsEnabled = true;
                            btnDelete.IsEnabled = true;
                            break;
                        case 2:
                            btnAdd.IsEnabled = false;
                            btnAdd.ToolTip = "У вас нет прав";
                            btnDelete.IsEnabled = true;
                            break;
                        case 3:
                            btnAdd.IsEnabled = true;
                            btnDelete.IsEnabled = false;
                            btnDelete.ToolTip = "У вас нет прав";
                            break;
                        default:
                            btnAdd.IsEnabled = false;
                            btnAdd.ToolTip = "У вас нет прав";
                            btnDelete.IsEnabled = false;
                            btnDelete.ToolTip = "У вас нет прав";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка" + ex.Message.ToString() + "Критическая ошибка приложения", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TourAgentEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataGridTours.ItemsSource = TourAgentEntities.GetContext().Tour.ToList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.Navigate(new AddTourList(null));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var tovarRemoving = DataGridTours.SelectedItems.Cast<Tour>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {tovarRemoving.Count()} элементов",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TourAgentEntities.GetContext().Tour.RemoveRange(tovarRemoving);
                    TourAgentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DataGridTours.ItemsSource = TourAgentEntities.GetContext().Tour.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnEditTovar_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.Navigate(new AddTourList((sender as Button).DataContext as Tour));
        }

        private void ListHotels_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.Navigate(new ListHotels());
        }

        private void HotelsComments_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.Navigate(new CommentsHotels());
        }
    }
}
