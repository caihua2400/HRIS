using HRIS.Source.Control;
using HRIS.Source.Model;
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

namespace HRIS.Source.View
{
    /// <summary>
    /// Interaction logic for StaffDetailUserControl.xaml
    /// </summary>
    public partial class UnitDetailUserControl : UserControl
    {
        private StaffController _staffController;
        public UnitDetailUserControl()
        {
            InitializeComponent();
            _staffController = Application.Current.FindResource("StaffController") as StaffController;
            var empCats = new List<string>()
            {
                "All Campuses",
                "Hobart",
                "Launceston",
            };
            CBCategory.ItemsSource = empCats;
            CBCategory.SelectedIndex = 0;
        }

        private void CBCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
            {
                var data = ((Unit)DataContext).Classes;
                if (CBCategory.SelectedIndex == 0)
                    DGClasses.ItemsSource = data;
                else
                    DGClasses.ItemsSource = data.Where(x => x.Campus == CBCategory.SelectedItem.ToString());
            }
        }
        public void OnHyperlinkClick(object sender, RoutedEventArgs e)
        {
            TextBlock row = sender as TextBlock;
            // Fetch Unit information
            Class u = row.DataContext as Class;
            Staff r = DBAdapter.Instance.FetchStaffByID(u.StaffID);
            _staffController.UpdateConsultations(ref r);
            _staffController.UpdateUnits(ref r);
            _staffController.UpdateClasses(ref r);

            // User control
            StaffDetailUserControl uduc = new StaffDetailUserControl();
            uduc.DataContext = r;

            Window window = new Window
            {
                Title = u.ToString(),
                Owner = Application.Current.MainWindow,
                Content = uduc,
                Height = 500,
                Width = 700,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }
    }
}
