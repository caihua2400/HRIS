using HRIS.Source.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Source.Model
{
    class Staff
    {
        public int ID { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Name { get { return GivenName + " " + FamilyName; } }
        public string Title { get; set; }
        public string Campus { get; set; }
        public string Phone { get; set; }
        public string Room { get; set; }
        public string Email { get; set; }
        public string PhotoURL { get; set; }
        public string Category { get; set; }
        public List<Consultation> Consultations { get; set; }
        public List<Unit> Units { get; set; }
        public List<Class> Classes { get; set; }

        public string Status
        {
            get
            {
                DateTime now = DateTime.Now;
                if (Classes != null || Classes.Count > 0)
                {
                    var teaching = from c in Classes
                                   where c.Day == now.DayOfWeek.ToString() && (c.Start <= now.TimeOfDay && c.End >= now.TimeOfDay)
                                   select c;
                    if (teaching.Count() > 0)
                        return "Teaching";
                }
                if (Consultations != null || Consultations.Count > 0)
                {
                    var consulting = from c in Consultations
                                     where c.Day == now.DayOfWeek.ToString() && (c.Start <= now.TimeOfDay && c.End >= now.TimeOfDay)
                                   select c;
                    if (consulting.Count() > 0)
                        return "Consulting";
                }
                return "Free";
            }
        }
        public bool VerifyCategory(string filter)
        {
            if (filter == "All Staffs")
                return true;
            else
                return Category.ToLower() == filter.ToLower();
        }
        public override string ToString()
        {
            return FamilyName + ", " + GivenName + " (" + Title + ")";
        }

        public List<Activity> Activities
        {
            get
            {
                List<Activity> array = new List<Activity>(5);
                string[] days = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
               
                for (int i = 0; i < 5; i++)
                {
                    Activity a = new Activity();
                    a.Day = days[i];
                    a.UpdateConsultation(Consultations);
                    a.UpdateClass(Classes);
                    array.Add(a);
                }
                return array;
            }
        }
    }
}
