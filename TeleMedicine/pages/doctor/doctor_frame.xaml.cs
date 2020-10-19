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

namespace TeleMedicine.pages.doctor
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


    public partial class doctor_frame : UserControl 
    {
        DispatcherTimer timer;
        public MainWindow parent;
        public int DoctorId;
        public int AccountId;
        bool refreshAccountdetails = true;

     
        /// <summary>
        /// Load up main page
        /// </summary>
        /// <param name="root"></param>
        /// <param name="account_id"></param>
        public doctor_frame(MainWindow root,int account_id)
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
            loadDoctorAccount();
            patients newPage = new patients(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.Fade;
            pageTransitionControl.ShowPage(newPage);
        }

        /// <summary>
        /// Load doctor's information
        /// </summary>
        public void loadDoctorAccount()
        {
            DataTable qry = parent.query("select username,(select doctorid from doctor where doctor.login_user_id="+AccountId+") as DiD,(select photoblob from photo where photo.id=login.user_id) as photo from login where user_id=" + AccountId);

            foreach (DataRow rw in qry.Rows)
            {
                userLabel.Content = rw["username"].ToString();
               image1.Source = parent.getImg((byte[])rw["photo"]);
                DoctorId = int.Parse(rw["DiD"].ToString());
            }
        }

        /// <summary>
        /// Indicate connection status to database
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
                if(refreshAccountdetails)
                loadDoctorAccount();  
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


      
        /// <summary>
        /// Logout from account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(MessageBox.Show("Logout from account?","TeleMedicine",MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.Yes)
            {
                parent.signout(AccountId);
            }
        }


        /// <summary>
        /// Fetch a patient's information
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="category"></param>
        public void loadPatientInfo(int patientid,string category)
        {
            profile newPage = new profile(parent, this,patientid,category);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);
        }




        //Navigations

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            settings newPage = new settings(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);
        }

      
        private void searchBut_Click(object sender, RoutedEventArgs e)
        {
            if(!parent.clean(searchBox.Text).Equals(""))
            {
                new searchWindow(this,parent.clean(searchBox.Text)).ShowDialog();
            }
        }

        private void patient_button_Click(object sender, RoutedEventArgs e)
        {
            patients newPage = new patients(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);
        }

        private void appointments_button_Click(object sender, RoutedEventArgs e)
        {
            appointments newPage = new appointments(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);

        }

        private void schedule_button_Click(object sender, RoutedEventArgs e)
        {
            schedule newPage = new schedule(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);
        }

        private void messages_button_Click(object sender, RoutedEventArgs e)
        {
            messages newPage = new messages(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);
        }

        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                searchBut_Click(null, null);
            }
        }

        private void pharmacy_button_Click(object sender, RoutedEventArgs e)
        {
            pharmacy newPage = new pharmacy(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);
        }
    }
}
