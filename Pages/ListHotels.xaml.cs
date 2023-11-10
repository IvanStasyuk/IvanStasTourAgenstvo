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
    /// Логика взаимодействия для ListHotels.xaml
    /// </summary>
    public partial class ListHotels : Page
    {
        public ListHotels()
        {
            InitializeComponent();
            DataGridHotels.ItemsSource = TourAgentEntities.GetContext().Hotel.ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TourAgentEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataGridHotels.ItemsSource = TourAgentEntities.GetContext().Hotel.ToList();
            }
        }

        private void btnEditTovar_Click(object sender, RoutedEventArgs e)
        {
            Manager.MyFrame.Navigate(new AddHotelList((sender as Button).DataContext as Hotel));
        }

        private void btnDelHotel_Click(object sender, RoutedEventArgs e)
        {
            var HotelRemoving = DataGridHotels.SelectedItems.Cast<Hotel>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {HotelRemoving.Count()} элементов",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TourAgentEntities.GetContext().Hotel.RemoveRange(HotelRemoving);
                    TourAgentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DataGridHotels.ItemsSource = TourAgentEntities.GetContext().Tour.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
