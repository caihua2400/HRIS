using HRIS.Source.Control;
using HRIS.Source.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    public partial class StaffDetailUserControl : UserControl
    {
        private UnitController _unitController;
        public StaffDetailUserControl()
        {
            InitializeComponent();
            _unitController = Application.Current.FindResource("UnitController") as UnitController;
        }

        public void OnHyperlinkClick(object sender, RoutedEventArgs e)
        {
            TextBlock row = sender as TextBlock;
            // Fetch Unit information
            Unit u = row.DataContext as Unit;
            _unitController.UpdateClasses(ref u);

            // User control
            UnitDetailUserControl uduc = new UnitDetailUserControl();
            uduc.DataContext = u;

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

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                DGActivity.Visibility = Visibility.Visible;
                ColorPanel.Visibility = Visibility.Visible;
            } catch
            {

            }
            
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            DGActivity.Visibility = Visibility.Hidden;
            ColorPanel.Visibility = Visibility.Hidden;
        }
    }
}
