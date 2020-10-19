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
    /// Interaction logic for appointments.xaml
    /// </summary>
    public partial class appointments : UserControl
    {
        MainWindow parent;
        doctor_frame subparent;


        public class AppointmentHolder
        {
            public int ID { set; get; }
            public int PatientID { set; get; }
            public string patient_name { get; set; }
            public string patient_code { get; set; }
            public string attended { get; set; }
            public string schedule { get; set; }
            public string created { get; set; }

        }

        /// <summary>
        /// Initialize, load up all apointments
        /// </summary>
        /// <param name="root"></param>
        /// <param name="_sub"></param>
        public appointments(MainWindow root, doctor_frame _sub)
        {
            parent = root;
            subparent = _sub;
            InitializeComponent();
            loadAllAppointment();
        }


        /// <summary>
        /// Load profile of patient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void loadprofile(object sender, MouseButtonEventArgs e)
        {
            ListViewItem itm = sender as ListViewItem;
            AppointmentHolder appointment = itm.Content as AppointmentHolder;
            subparent.loadPatientInfo(appointment.PatientID,"appointment");
        }



        public void loadAllAppointment()
        {

            DataTable qry = parent.query("select scheduleid,patientid,(select PCODE from patient where patient.patientid=schedule.patientid) as pcode, (select concat(lastname,' ',firstname) from patient where patient.patientid=schedule.patientid) as name, status as attended, schedule.date as schedule, date_added as created from schedule where doctorid=" + subparent.DoctorId);
            foreach (DataRow rw in qry.Rows)
            {
                AppointmentHolder appointment = new AppointmentHolder();
                appointment.ID = int.Parse(rw["scheduleid"].ToString());
                appointment.PatientID = int.Parse(rw["patientid"].ToString());
                appointment.patient_code = (rw["pcode"].ToString());
                appointment.patient_name = (rw["name"].ToString());
                appointment.attended = (rw["attended"].ToString());
                appointment.schedule = (rw["schedule"].ToString());
                appointment.created = (rw["created"].ToString());

                listView0.Items.Add(appointment);
            }

            loadTodayAppointment();
        }



        /// <summary>
        /// Get today's appointments
        /// </summary>
        public void loadTodayAppointment()
        {

            DataTable qry = parent.query("select scheduleid,patientid,(select PCODE from patient where patient.patientid=schedule.patientid) as pcode, (select concat(lastname,' ',firstname) from patient where patient.patientid=schedule.patientid) as name, status as attended, schedule.date as schedule, date_added as created from schedule where doctorid="+subparent.DoctorId+ " and extract(day from date)=extract(day from now())  and   extract(Month from date)=extract(month from now())  and extract(year from date)=extract(year from now())");
            foreach (DataRow rw in qry.Rows)
            {
                AppointmentHolder appointment = new AppointmentHolder();
                appointment.ID = int.Parse(rw["scheduleid"].ToString());
                appointment.PatientID = int.Parse(rw["patientid"].ToString());
                appointment.patient_code= (rw["pcode"].ToString());
                appointment.patient_name = (rw["name"].ToString());
                appointment.attended = (rw["attended"].ToString());
                appointment.schedule = (rw["schedule"].ToString());
                appointment.created = (rw["created"].ToString());

                listView1.Items.Add(appointment);
            }

            loadTomorrowAppointment();
        }


        /// <summary>
        /// Get tomorrow's appointments
        /// </summary>
        public void loadTomorrowAppointment()
        {

            DataTable qry = parent.query("select scheduleid,patientid,(select PCODE from patient where patient.patientid=schedule.patientid) as pcode, (select concat(lastname,' ',firstname) from patient where patient.patientid=schedule.patientid) as name, status as attended, schedule.date as schedule, date_added as created from schedule where doctorid=" + subparent.DoctorId+ " and extract(day from date)=extract(day from date_add(now(),INTERVAL 1 DAY))  and   extract(Month from date)=extract(month from date_add(now(),INTERVAL 1 DAY))  and extract(year from date)=extract(year from date_add(now(),INTERVAL 1 DAY))");
            foreach (DataRow rw in qry.Rows)
            {
                AppointmentHolder appointment = new AppointmentHolder();
                appointment.ID = int.Parse(rw["scheduleid"].ToString());
                appointment.PatientID = int.Parse(rw["patientid"].ToString());
                appointment.patient_code = (rw["pcode"].ToString());
                appointment.patient_name = (rw["name"].ToString());
                appointment.attended = (rw["attended"].ToString());
                appointment.schedule = (rw["schedule"].ToString());
                appointment.created = (rw["created"].ToString());

                listView2.Items.Add(appointment);
            }

            loadMissedAppointment();
        }



        /// <summary>
        /// Get appointments that didnt hold
        /// </summary>
        public void loadMissedAppointment()
        {

            DataTable qry = parent.query("select scheduleid,patientid,(select PCODE from patient where patient.patientid=schedule.patientid) as pcode, (select concat(lastname,' ',firstname) from patient where patient.patientid=schedule.patientid) as name, status as attended, schedule.date as schedule, date_added as created from schedule where doctorid=" + subparent.DoctorId + " and date < now() and status='No'");
            foreach (DataRow rw in qry.Rows)
            {
                AppointmentHolder appointment = new AppointmentHolder();
                appointment.ID = int.Parse(rw["scheduleid"].ToString());
                appointment.PatientID = int.Parse(rw["patientid"].ToString());
                appointment.patient_code = (rw["pcode"].ToString());
                appointment.patient_name = (rw["name"].ToString());
                appointment.attended = (rw["attended"].ToString());
                appointment.schedule = (rw["schedule"].ToString());
                appointment.created = (rw["created"].ToString());

                listView3.Items.Add(appointment);
            }

            loadAttendedAppointment();
        }



        /// <summary>
        /// get appointments that held
        /// </summary>
        public void loadAttendedAppointment()
        {

            DataTable qry = parent.query("select scheduleid,patientid,(select PCODE from patient where patient.patientid=schedule.patientid) as pcode, (select concat(lastname,' ',firstname) from patient where patient.patientid=schedule.patientid) as name, status as attended, schedule.date as schedule, date_added as created from schedule where doctorid=" + subparent.DoctorId + " and date < now() and status='Yes'");
            foreach (DataRow rw in qry.Rows)
            {
                AppointmentHolder appointment = new AppointmentHolder();
                appointment.ID = int.Parse(rw["scheduleid"].ToString());
                appointment.PatientID = int.Parse(rw["patientid"].ToString());
                appointment.patient_code = (rw["pcode"].ToString());
                appointment.patient_name = (rw["name"].ToString());
                appointment.attended = (rw["attended"].ToString());
                appointment.schedule = (rw["schedule"].ToString());
                appointment.created = (rw["created"].ToString());

                listView4.Items.Add(appointment);
            }

           
        }













    }
}
