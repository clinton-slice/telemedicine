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

namespace TeleMedicine.pages.patient
{
    /// <summary>
    /// Interaction logic for schedule.xaml
    /// </summary>
    public partial class appointment : UserControl
    {
        MainWindow parent;
        patient_frame subparent;
        List<string> ids = new List<string>();

        /// <summary>
        /// Class for schedules
        /// </summary>
        public class ScheduleHolder
        {
            public int ID { set; get; }
             public string doctor_name { get; set; }
            public string specialization { get; set; }
            public string attended { get; set; }
            public string schedule { get; set; }
            public string created { get; set; }


        }



        /// <summary>
        /// Add all schedules for current date to a listview. Trigger calendar date change event
        /// </summary>
        /// <param name="root"></param>
        /// <param name="_sub"></param>
        public appointment(MainWindow root, patient_frame _sub)
        {
            parent = root;
            subparent = _sub;
            InitializeComponent();

            DataTable qry = parent.query("select scheduleid, (select concat(lastname,' ',firstname) from doctor where doctor.doctorid=schedule.doctorid) as name, status as attended, schedule.date as schedule, date_added as created from schedule where convert(date,date)=str_to_date('" + DateTime.Now.ToShortDateString() + "','%m/%d/%Y') and patientid = " + subparent.PatientId);
            foreach (DataRow rw in qry.Rows)
            {
                ScheduleHolder schedule = new ScheduleHolder();
                schedule.ID = int.Parse(rw["scheduleid"].ToString());
                schedule.doctor_name = (rw["name"].ToString());
                schedule.attended = (rw["attended"].ToString());
                schedule.schedule = (rw["schedule"].ToString());
                schedule.created = (rw["created"].ToString());

                listView1.Items.Add(schedule);
            }


            calendar_SelectedDatesChanged(null, null);
        }


       
        

        /// <summary>
        /// Fetch all apointments for selected dates on the calendar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDatesCollection collection = calendar.SelectedDates;

            string patch = "";
            if (collection.Count > 0)
                patch = " where ";
            foreach(DateTime dt in collection)
            {
                
                patch+= "patientid="+subparent.PatientId+" and convert(date,date)=str_to_date('"+ dt.ToShortDateString() + "','%m/%d/%Y')" ;

                if(collection.IndexOf(dt)<(collection.Count-1))
                { patch += " or "; }
            }

            if(patch.Equals(""))
            {
                patch = " where patientid=" + subparent.PatientId;
            }
            

            listView0.Items.Clear();
           DataTable qry = parent.query("select scheduleid, (select concat(lastname,' ',firstname) from doctor where doctor.doctorid=schedule.doctorid) as name,(select specialization from doctor where doctor.doctorid=schedule.doctorid) as specialization, status as attended, schedule.date as schedule, date_added as created from schedule " + patch+" ");
            foreach (DataRow rw in qry.Rows)
            {
                ScheduleHolder schedule = new ScheduleHolder();
                schedule.ID = int.Parse(rw["scheduleid"].ToString());

                schedule.doctor_name = (rw["name"].ToString());
                schedule.specialization= (rw["specialization"].ToString());
                schedule.attended = (rw["attended"].ToString());
                schedule.schedule = (rw["schedule"].ToString());
                schedule.created = (rw["created"].ToString());

                listView0.Items.Add(schedule);
            }

        }

       


        /// <summary>
        /// Return back to main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            subparent.loadDashboard();
        }


    }
}
