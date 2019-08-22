using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Source.Model
{
    class Class
    {
        public string UnitCode { get; set; }
        public int StaffID { get; set; }
        public string StaffName { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Day { get; set; }
        public string Campus { get; set; }
        public string Type { get; set; }
        public string Room { get; set; }
    }
}
