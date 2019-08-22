using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Source.Model;
using HRIS.Source.Common;

namespace HRIS.Source.Control
{
    class StaffController
    {
        private List<Staff> _staffs;

        private ObservableCollection<Staff> _viewableStaffs;
        public ObservableCollection<Staff> GetStaffList()
        {
            return _viewableStaffs;
        }
        public StaffController()
        {
            _staffs = DBAdapter.Instance.FetchStaffList();
            _viewableStaffs = new ObservableCollection<Staff>(_staffs);
        }

        //filter by name and by category, using LINQ
        public void Filter(string name, string category)
        {
            var selected = from Staff r in _staffs
                           where (r.FamilyName.ToLower().Contains(name.ToLower()) || r.GivenName.ToLower().Contains(name.ToLower())) && r.VerifyCategory(category)
                           select r;

            // Clear viewable list
            _viewableStaffs.Clear();

            // Bind to viewable list
            selected.ToList().ForEach(_viewableStaffs.Add);
        }

        public void UpdateConsultations(ref Staff staff)
        {
            if(staff.Consultations == null)
                staff.Consultations = DBAdapter.Instance.FetchConsultationListByStaff(staff.ID);
        }

        public void UpdateUnits(ref Staff staff)
        {
            if (staff.Units == null)
                staff.Units = DBAdapter.Instance.FetchUnitListByStaff(staff);
        }

        public void UpdateClasses(ref Staff staff)
        {
            if (staff.Classes == null)
                staff.Classes = DBAdapter.Instance.FetchClassListByStaff(staff.ID);
        }
    }
}
