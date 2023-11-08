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
    }
}
