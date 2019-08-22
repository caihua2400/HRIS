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

namespace HRIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.StaffList.StaffSelected += new EventHandler(StaffList_StaffSelected);
            this.UnitList.UnitSelected += new EventHandler(UnitList_StaffSelected);
        }
        public void StaffList_StaffSelected(object sender, EventArgs e)
        {
            //your code here
            // StaffDetail.Clear();
            // Change Detail context
            StaffDetail.DataContext = ((SelectionChangedEventArgs)e).AddedItems[0];
        }
        public void UnitList_StaffSelected(object sender, EventArgs e)
        {
            //your code here
            // StaffDetail.Clear();
            // Change Detail context
            UnitDetail.DataContext = ((SelectionChangedEventArgs)e).AddedItems[0];
        }
    }
}
