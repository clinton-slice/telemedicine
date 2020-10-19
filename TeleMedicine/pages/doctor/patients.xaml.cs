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
using System.Windows.Threading;

namespace TeleMedicine.pages.doctor
{
    /// <summary>
    /// Interaction logic for patients.xaml
    /// </summary>
    public partial class patients : UserControl
    {

        MainWindow parent;
        doctor_frame subparent;
        DispatcherTimer timer;

        public patients(MainWindow root, doctor_frame _sub)
        {
            parent = root;
            subparent = _sub;
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
        }



        /// <summary>
        /// Load up filter options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_tick(object sender, EventArgs e)
        {
            filtercomboBox.Items.Add("Show All");

            for (char _ind = 'A'; _ind <= 'Z'; _ind++)
            {
                filtercomboBox.Items.Add("Begins with '" + _ind + "'");
            }

            filtercomboBox.SelectedIndex = 0;


            timer.Stop();
        }


        public void load()
        {
            listView.Items.Clear();
            DataTable qry = parent.query("select patientid,pcode,email,lastname,firstname,sex,(select photoblob from photo where login_user_id=photo.id) as photo from patient");
            foreach (DataRow rw in qry.Rows)
            {
                PatientHolder patient = new PatientHolder();
                patient.ID = int.Parse(rw["patientid"].ToString());
                patient.email = rw["email"].ToString();
                patient.pcode = rw["pcode"].ToString();
                patient.fullname = rw["lastname"].ToString() + " " + rw["firstname"].ToString();
                patient.sex = rw["sex"].ToString();
                patient.photo = parent.getImg((byte[])rw["photo"]);
                listView.Items.Add(patient);
            }
            resultLabel.Content = "Result: " + qry.Rows.Count + " found";
        }


        /// <summary>
        /// Load patients whose's surname starts with selected alphabet
        /// </summary>
        /// <param name="_index"></param>
        public void load(string _index)
        {
            listView.Items.Clear();
            DataTable qry = parent.query("select patientid,pcode,email,lastname,firstname,sex,(select photoblob from photo where login_user_id=photo.id) as photo from patient where lastname like '" + _index + "%' ");
            foreach (DataRow rw in qry.Rows)
            {
                PatientHolder patient = new PatientHolder();
                patient.ID = int.Parse(rw["patientid"].ToString());
                patient.email = rw["email"].ToString();
                patient.pcode = rw["pcode"].ToString();
                patient.fullname = rw["lastname"].ToString() + " " + rw["firstname"].ToString();
                patient.sex = rw["sex"].ToString();
                patient.photo = parent.getImg((byte[])rw["photo"]);
                listView.Items.Add(patient);
            }

            resultLabel.Content = "Result: " + qry.Rows.Count + " found";
        }



        private void filtercomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filtercomboBox.SelectedIndex > 0)
            {
                int offset = 1;
                for (char _ind = 'A'; _ind <= 'Z'; _ind++, offset++)
                {
                    if (filtercomboBox.SelectedIndex == offset)
                    {
                        load(_ind.ToString());
                        break;
                    }
                }
            }
            else
            {
                load();
            }
        }


        /// <summary>
        /// Load up page to view patient profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewPatient(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
           PatientHolder account = b.CommandParameter as PatientHolder;
            subparent.loadPatientInfo(account.ID, "profile");
          }

 







    }
}
