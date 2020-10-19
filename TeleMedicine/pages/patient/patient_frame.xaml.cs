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
using System.Windows.Threading;
using System.Data;
using TeleMedTransition;

namespace TeleMedicine.pages.patient
{
    /// <summary>
    /// Interaction logic for admin_all_accounts.xaml
    /// </summary>

    public class PatientHolder
    {
        public int ID { set; get; }
        public BitmapImage photo { get; set; }
        public string pcode { get; set; }
        public string email { get; set; }
        public string fullname { get; set; }
        public string sex { get; set; }
 
  }


    public partial class patient_frame : UserControl
    {
        DispatcherTimer timer;
        public MainWindow parent;
        public int PatientId;
        public int AccountId;
        bool refreshAccountdetails = true;


        public patient_frame(MainWindow root, int account_id)
        {
            parent = root;
            AccountId = account_id;
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
            statusText.Foreground = Brushes.Yellow;
            statusCircle.Fill = Brushes.Yellow;
            statusText.Content = "Connecting to database";
            loadPatientAccount();

            loadDashboard();
        }

        /// <summary>
        /// Load basic information of patient
        /// </summary>
        public void loadPatientAccount()
        {
            DataTable qry = parent.query("select username,(select PCODE from patient where patient.login_user_id=" + AccountId + ") as PCODE,(select patientid from patient where patient.login_user_id=" + AccountId + ") as PiD,(select photoblob from photo where photo.id=login.user_id) as photo from login where user_id=" + AccountId);
             foreach (DataRow rw in qry.Rows)
            {
                userLabel.Content = rw["username"].ToString();
                pcode.Content = rw["PCODE"].ToString();
                image1.Source = parent.getImg((byte[])rw["photo"]);
                PatientId = int.Parse(rw["PiD"].ToString());
            }
        }

        /// <summary>
        /// Indicate status of connection to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_tick(object sender, EventArgs e)
        {

            if (parent.Dbstate.Equals("Open"))
            {
                statusText.Foreground = Brushes.White;
                statusCircle.Fill = Brushes.LightGreen;
                statusText.Content = "Connected to database";
                if (refreshAccountdetails)
                    loadPatientAccount();
            }
            else if (parent.Dbstate.Equals("Connecting"))
            {
                statusText.Foreground = Brushes.Yellow;
                statusCircle.Fill = Brushes.Yellow;
                statusText.Content = "Connecting to database";
                refreshAccountdetails = true;
            }
            else
            {
                statusText.Foreground = Brushes.Gold;
                statusCircle.Fill = Brushes.DarkGray;
                statusText.Content = "Disconnected from database";
                refreshAccountdetails = true;
            }


        }

        //Navigation from main page
        public void loadDashboard()
        {
            dashboard newPage = new dashboard(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.GrowAndFade;
            pageTransitionControl.ShowPage(newPage);
        }

        public void loadAppointment()
        {
            appointment newPage = new appointment(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.Fade;
            pageTransitionControl.ShowPage(newPage);
        }

        public void loadMessage()
        {
            messages newPage = new messages(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.Fade;
            pageTransitionControl.ShowPage(newPage);
        }

        public void loadResult()
        {
            result newPage = new result(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.Fade;
            pageTransitionControl.ShowPage(newPage);
        }

        public void loadBill()
        {
            bill newPage = new bill(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.Fade;
            pageTransitionControl.ShowPage(newPage);
        }

        public void loadPharmacy()
        {
            pharmacy newPage = new pharmacy(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.Fade;
            pageTransitionControl.ShowPage(newPage);
        }

        public void loadSetting()
        {
            settings newPage = new settings(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.Fade;
            pageTransitionControl.ShowPage(newPage);
        }




        /// <summary>
        /// Logout from current logged account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Logout from account?", "TeleMedicine", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                parent.signout(AccountId);
            }
        }










    }
}
