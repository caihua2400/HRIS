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
using HRIS.Source.Control;
using HRIS.Source.Model;

namespace HRIS.Source.View
{
    /// <summary>
    /// Interaction logic for StaffListView.xaml
    /// </summary>
    public partial class StaffListUserControl : UserControl
    {
        public event EventHandler StaffSelected;
        private StaffController _staffController;

        public StaffListUserControl()
        {
            InitializeComponent();
            // Controller instance
            _staffController = Application.Current.FindResource("StaffController") as StaffController;
            // Filter select box
            var empCats = new List<string>()
            {
                "All Staffs",
                "Academic",
                "Technical",
                "Admin",
                "Casual",
            };
            CBCategory.ItemsSource = empCats;
            CBCategory.SelectedIndex = 0;
        }

        private void TBSearchName_Changed(object sender, TextChangedEventArgs e)
        {
            string name = TBSearchName.Text;
            string cat = CBCategory.SelectedValue.ToString();

            _staffController.Filter(name, cat);
        }
        private void CBCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = TBSearchName.Text;
            string cat = CBCategory.SelectedValue.ToString();

            _staffController.Filter(name, cat);
        }
        private void LBStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //bubble the event up to the parent
            if (this.StaffSelected != null)
            {
                if (e.AddedItems.Count > 0)
                {
                    Staff r = (Staff)e.AddedItems[0];
                    _staffController.UpdateConsultations(ref r);
                    _staffController.UpdateUnits(ref r);
                    _staffController.UpdateClasses(ref r);

                    this.StaffSelected(sender, e);
                }
            }
        }
    }
}
