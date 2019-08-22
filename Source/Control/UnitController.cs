using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Source.Model;

namespace HRIS.Source.Control
{
    class UnitController
    {
        private List<Unit> _units;

        private ObservableCollection<Unit> _viewableUnits;
        public ObservableCollection<Unit> GetUnitList()
        {
            return _viewableUnits;
        }
        public UnitController()
        {
            _units = DBAdapter.Instance.FetchUnitList();
            _viewableUnits = new ObservableCollection<Unit>(_units);
        }
        public void Filter(string name)
        {
            var selected = from Unit r in _units
                           where (r.Code.ToLower().Contains(name.ToLower()) || r.Title.ToLower().Contains(name.ToLower())) 
                           select r;

            // Clear viewable list
            _viewableUnits.Clear();

            // Bind to viewable list
            selected.ToList().ForEach(_viewableUnits.Add);
        }
        public void UpdateClasses(ref Unit unit)
        {
            if (unit.Classes == null)
                unit.Classes = DBAdapter.Instance.FetchClassListByUnit(unit.Code);
        }
    }
}
