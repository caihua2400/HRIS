using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using HRIS.Source.Common;

namespace HRIS.Source.Model
{
    class DBAdapter
    {
        // Database connection configuration
        private const string _database = "kit206";
        private const string _username = "kit206";
        private const string _password = "kit206";
        private const string _hostname = "alacritas.cis.utas.edu.au";

        // MySQL connection
        private static MySqlConnection _conn = null;

        // Private constructor
        private DBAdapter()
        {
            string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3};SslMode=none;", _database, _hostname, _username, _password);
            _conn = new MySqlConnection(connectionString);
        }

        // ERDAdapter instance
        private static DBAdapter _instance;
        public static DBAdapter Instance
        {
            get
            {
                if (_instance == null) { _instance = new DBAdapter(); }
                return _instance;
            }
        }

        // QueryReader wrapper
        private void QueryReader(string description, string query, Action<MySqlDataReader> callback)
        {
            MySqlDataReader reader = null;
            try
            {
                if(_conn.State.ToString() != "Open")
                    _conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, _conn);
                reader = cmd.ExecuteReader();
                callback?.Invoke(reader);
            }
            catch (Exception e)
            {
                Utility.ReportError(description, e);
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                if (_conn != null) { _conn.Close(); }
            }
        }

        // Get a list of staff with all information
        public List<Staff> FetchStaffList()
        {
            var list = new List<Staff>();
            QueryReader("Loading all staff", "SELECT * FROM kit206.staff ORDER BY family_name, given_name ASC", (MySqlDataReader r) =>
            {
                while (r.Read())
                {
                    Console.WriteLine(r);
                    list.Add(new Staff
                    {
                        ID = r.GetInt32("id"),
                        GivenName = r.GetString("given_name"),
                        FamilyName = r.GetString("family_name"),
                        Title = r.GetString("title"),
                        PhotoURL = r.GetString("photo"),
                        Campus = r.GetString("campus"),
                        Phone = r.GetString("phone"),
                        Room = r.GetString("room"),
                        Email = r.GetString("email"),
                        Category = r.GetString("category"),
                    });
                }
            });
            return list;
        }

        public Staff FetchStaffByID(int ID)
        {
            var list = new List<Staff>();
            QueryReader("Loading all staff", "SELECT * FROM kit206.staff WHERE id = " + ID, (MySqlDataReader r) =>
            {
                while (r.Read())
                {
                    Console.WriteLine(r);
                    list.Add(new Staff
                    {
                        ID = r.GetInt32("id"),
                        GivenName = r.GetString("given_name"),
                        FamilyName = r.GetString("family_name"),
                        Title = r.GetString("title"),
                        PhotoURL = r.GetString("photo"),
                        Campus = r.GetString("campus"),
                        Phone = r.GetString("phone"),
                        Room = r.GetString("room"),
                        Email = r.GetString("email"),
                        Category = r.GetString("category"),
                    });
                }
            });
            return list[0];
        }

        public List<Consultation> FetchConsultationListByStaff(int ID)
        {
            var list = new List<Consultation>();
            QueryReader("Loading consultations by staff ID", "SELECT * FROM kit206.consultation WHERE staff_id = " + ID, (MySqlDataReader r) =>
            {
                while (r.Read())
                {
                    Console.WriteLine(r);
                    list.Add(new Consultation
                    {
                        Day = r.GetString("day"),
                        Start = r.GetTimeSpan("start"),
                        End = r.GetTimeSpan("end"),
                    });
                }
            });
            return list;
        }

        public List<Unit> FetchUnitListByStaff(Staff staff)
        {
            var list = new List<Unit>();
            QueryReader("Loading units by staff ID", "SELECT * FROM kit206.unit WHERE coordinator = " + staff.ID, (MySqlDataReader r) =>
            {
                while (r.Read())
                {
                    Console.WriteLine(r);
                    list.Add(new Unit
                    {
                        Code = r.GetString("code"),
                        Title = r.GetString("title"),
                        Coordinator = staff.ID,
                        CoordinatorName = staff.ToString(),
                    });
                }
            });
            return list;
        }

        public List<Class> FetchClassListByStaff(int ID)
        {
            var list = new List<Class>();
            QueryReader("Loading classes by staff ID", "SELECT * FROM kit206.class WHERE staff = " + ID, (MySqlDataReader r) =>
            {
                while (r.Read())
                {
                    Console.WriteLine(r);
                    list.Add(new Class
                    {
                        UnitCode = r.GetString("unit_code"),
                        Campus = r.GetString("campus"),
                        Day = r.GetString("day"),
                        Start = r.GetTimeSpan("start"),
                        End = r.GetTimeSpan("end"),
                        Type = r.GetString("type"),
                        Room = r.GetString("room"),
                        StaffID = ID,
                    });
                }
            });
            return list;
        }

        // Get a list of unit with all information
        public List<Unit> FetchUnitList()
        {
            var list = new List<Unit>();
            QueryReader("Loading all unit", "SELECT u.*, concat(s.family_name, ' ', s.given_name,' (',s.title,')') as staff FROM kit206.unit u, kit206.staff s where u.coordinator = s.id ORDER BY u.code, u.title;", (MySqlDataReader r) =>
            {
                while (r.Read())
                {
                    Console.WriteLine(r);
                    list.Add(new Unit
                    {
                        Code = r.GetString("code"),
                        Title = r.GetString("title"),
                        Coordinator = r.GetInt32("coordinator"),
                        CoordinatorName = r.GetString("staff"),
                    });
                }
            });
            return list;
        }
        public List<Class> FetchClassListByUnit(string ID)
        {
            var list = new List<Class>();
            QueryReader("Loading classes by unit Code", "SELECT c.*, concat(s.family_name, ' ', s.given_name,' (',s.title,')') as staff_name FROM kit206.class c, kit206.staff s where c.staff = s.id AND c.unit_code = '" + ID + "'", (MySqlDataReader r) =>
            {
                while (r.Read())
                {
                    Console.WriteLine(r);
                    list.Add(new Class
                    {
                        UnitCode = r.GetString("unit_code"),
                        Campus = r.GetString("campus"),
                        Day = r.GetString("day"),
                        Start = r.GetTimeSpan("start"),
                        End = r.GetTimeSpan("end"),
                        Type = r.GetString("type"),
                        Room = r.GetString("room"),
                        StaffID = r.GetInt32("staff"),
                        StaffName = r.GetString("staff_name"),
                    });
                }
            });
            return list;
        }
    }
}
