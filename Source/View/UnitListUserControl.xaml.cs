using HRIS.Source.Control;
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
using HRIS.Source.Model;

namespace HRIS.Source.View
{
    /// <summary>
    /// Interaction logic for UnitListUserControl.xaml
    /// </summary>
    public partial class UnitListUserControl : UserControl
    {
        private UnitController _unitController;
        public event EventHandler UnitSelected;
        public UnitListUserControl()
        {
            InitializeComponent();
            _unitController = Application.Current.FindResource("UnitController") as UnitController;
        }

        private void TBSearchName_Changed(object sender, TextChangedEventArgs e)
        {
            string name = TBSearchName.Text;

            _unitController.Filter(name);
        }
        private void LBUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.UnitSelected != null)
            {
                if (e.AddedItems.Count > 0)
                {
                    Unit r = (Unit)e.AddedItems[0];
                    _unitController.UpdateClasses(ref r);
                    this.UnitSelected(sender, e);
                }
            }
        }
    }
}
