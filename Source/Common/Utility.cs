using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HRIS.Source.Common
{
    public static class Utility
    {
        private static bool reportingErrors = true;

        public static void ReportError(string msg, Exception e)
        {
            if (reportingErrors)
            {
                MessageBox.Show("An error occurred while " + msg + ". Try again later.\n\nError Details:\n" + e,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static int CheckDay(String day)
        {
            Dictionary<String, int> mp = new Dictionary<String, int>();

            mp.Add("Sunday", 1);
            mp.Add("Monday", 2);
            mp.Add("Tuesday", 3);
            mp.Add("Wednesday", 4);
            mp.Add("Thrusday", 5);
            mp.Add("Friday", 6);
            mp.Add("Saturday", 7);

            return mp[day];
        }
    }
}
