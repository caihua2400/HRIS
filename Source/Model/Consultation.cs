using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Source.Model
{
    class Consultation
    {
        public int StaffID { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Day { get; set; }
    }
}
