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
    /// Логика взаимодействия для AddHotelList.xaml
    /// </summary>
    public partial class AddHotelList : Page
    {
        private Hotel _currentHotel = new Hotel();
        public AddHotelList(Hotel selectedHotel)
        {
            InitializeComponent();
            if (selectedHotel != null)
                _currentHotel = selectedHotel;
            DataContext = _currentHotel;
        }

        private void SavebtnOrder_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(NameHotel.Text))
                errors.AppendLine("Укажите название отеля");
            if (string.IsNullOrEmpty(CountOfStarsHotel.Text))
                errors.AppendLine("Укажите количество звёзд для отеля");
            if (string.IsNullOrEmpty(CountryCodeHotel.Text))
                errors.AppendLine("Укажите код страны для отеля");
            if (string.IsNullOrEmpty(DescriptionHotel.Text))
                errors.AppendLine("Укажите описание отеля");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentHotel.Id > 0)
            {
                TourAgentEntities.GetContext().Hotel.Add(_currentHotel);
            }
            try
            {
                TourAgentEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MyFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
