using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace TeleMedicine.pages.doctor
{
    /// <summary>
    /// Interaction logic for schedule.xaml
    /// </summary>
    public partial class schedule : UserControl
    {
        MainWindow parent;
        doctor_frame subparent;
        List<string> ids = new List<string>();

        public class ScheduleHolder
        {
            public int ID { set; get; }
            public int PatientID { set; get; }
            public string patient_name { get; set; }
            public string patient_code { get; set; }
            public string attended { get; set; }
            public string schedule { get; set; }
            public string created { get; set; }

        }



        public schedule(MainWindow root, doctor_frame _sub)
        {
            parent = root;
            subparent = _sub;
            InitializeComponent();

            for (int num = 1; num <= 12; num++)
            {
                if (num < 10)
                    comboBox1.Items.Add("0" + num);
                else
                    comboBox1.Items.Add(num);
            }

            for (int num= 0; num < 60; num++)
            {
                if (num < 10)
                    comboBox2.Items.Add("0" + num);
                else
                    comboBox2.Items.Add(num);
            }

            comboBox3.Items.Add("AM");
            comboBox3.Items.Add("PM");

            comboBox1.SelectedIndex = comboBox2.SelectedIndex = comboBox3.SelectedIndex = 0;

            DataTable table = parent.query("select patientid,concat(lastname,' ',firstname) as name from patient order by lastname");
            foreach(DataRow rw in table.Rows)
            {
                ids.Add(rw["patientid"].ToString());
                comboBox0.Items.Add(rw["name"].ToString());
            }


            calendar_SelectedDatesChanged(null, null);
        }


        public void loadprofile(object sender, MouseButtonEventArgs e)
        {
            ListViewItem itm = sender as ListViewItem;
            ScheduleHolder schedule = itm.Content as ScheduleHolder;
            subparent.loadPatientInfo(schedule.PatientID, "appointment");
        }

        

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDatesCollection collection = calendar.SelectedDates;

            string patch = "";
            if (collection.Count > 0)
                patch = " where ";
            foreach(DateTime dt in collection)
            {
                
                patch+= "doctorid="+subparent.DoctorId+" and convert(date,date)=str_to_date('"+ dt.ToShortDateString() + "','%m/%d/%Y')" ;

                if(collection.IndexOf(dt)<(collection.Count-1))
                { patch += " or "; }
            }

            listView0.Items.Clear();
           DataTable qry = parent.query("select scheduleid,patientid,(select PCODE from patient where patient.patientid=schedule.patientid) as pcode, (select concat(lastname,' ',firstname) from patient where patient.patientid=schedule.patientid) as name, status as attended, schedule.date as schedule, date_added as created from schedule "+patch);
            foreach (DataRow rw in qry.Rows)
            {
                ScheduleHolder schedule = new ScheduleHolder();
                schedule.ID = int.Parse(rw["scheduleid"].ToString());
                schedule.PatientID = int.Parse(rw["patientid"].ToString());
                schedule.patient_code = (rw["pcode"].ToString());
                schedule.patient_name = (rw["name"].ToString());
                schedule.attended = (rw["attended"].ToString());
                schedule.schedule = (rw["schedule"].ToString());
                schedule.created = (rw["created"].ToString());

                listView0.Items.Add(schedule);
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox0.SelectedIndex < 0)
            {
                MessageBox.Show("Select a patient", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (datepicker.SelectedDate.ToString().Equals(""))
            {
                MessageBox.Show("Select schedule date", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {

                string id = ids[comboBox0.SelectedIndex];
                string time = parent.reverseDateFormat(datepicker.SelectedDate.ToString()) + " " + parent.convertTo24HoursFormat(comboBox1.Items[comboBox1.SelectedIndex].ToString() + ":" + comboBox2.Items[comboBox2.SelectedIndex].ToString() + " " + comboBox3.Items[comboBox3.SelectedIndex].ToString());
                if (parent.Dbstate.Equals("Open"))
                {
                    parent.query("insert into schedule (patientid,doctorid,date) values(" + id + "," + subparent.DoctorId + ",'" + time + "')");
                    MessageBox.Show("Schedule Added successfully", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }
    }
}
