using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Source.Model
{
    class Activity
    {
        public string Day { get; set; }
        public int[] Actions { get; set; }

        public Activity()
        {
            Actions = new int[8];
        }

        public void UpdateConsultation(List<Consultation> consultations)
        {
            if (consultations != null || consultations.Count > 0)
            {
                foreach (Consultation c in consultations)
                {
                    if (c.Day == Day)
                    {
                        int start = c.Start.Hours - 9;
                        int end = c.End.Hours - 9;
                        for (int i = start; i < end; i++)
                        {
                            Actions[i] += 1;
                        }
                    }
                }
            }
        }
        public void UpdateClass(List<Class> classes)
        {
            if (classes != null || classes.Count > 0)
            {
                foreach (Class c in classes)
                {
                    if (c.Day == Day)
                    {
                        int start = c.Start.Hours - 9;
                        int end = c.End.Hours - 9;
                        for (int i = start; i < end; i++)
                        {
                            Actions[i] += 2;
                        }
                    }
                }
            }
        }
    }
}
