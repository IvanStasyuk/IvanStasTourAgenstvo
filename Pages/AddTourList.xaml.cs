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
    /// Логика взаимодействия для AddTourList.xaml
    /// </summary>
    public partial class AddTourList : Page
    {
        private Tour _currentTour = new Tour();
        public AddTourList(Tour selectedTour)
        {
            InitializeComponent();
            if (selectedTour != null)
                _currentTour = selectedTour;
            DataContext = _currentTour;
        }

        private void SavebtnOrder_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(NameTour.Text))
                errors.AppendLine("Укажите имя тура");
            if (string.IsNullOrEmpty(DescriptionTour.Text))
                errors.AppendLine("Укажите описание тура");
            if (string.IsNullOrEmpty(CountryTour.Text))
                errors.AppendLine("Укажите страну тура");
            if (string.IsNullOrEmpty(CountTicket.Text))
                errors.AppendLine("Укажите цену билета");
            if (string.IsNullOrEmpty(PriceTour.Text))
                errors.AppendLine("Укажите цену");
            if (string.IsNullOrEmpty(IsActualTour.Text))
                errors.AppendLine("Укажите актуально ли");
            if (string.IsNullOrEmpty(ImageTour.Text))
                errors.AppendLine("Укажите изображение");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentTour.Id > 0)
            {
                TourAgentEntities.GetContext().Tour.Add(_currentTour);
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
