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

namespace TeleMedicine.pages.admin
{
    /// <summary>
    /// Interaction logic for admin_all_accounts.xaml
    /// </summary>

    public class AccountHolder
    {
        public int ID { set; get; }
        public BitmapImage photo { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public string category { get; set; }
 
  }


    public partial class admin_frame : UserControl 
    {
        DispatcherTimer timer;
        public MainWindow parent;
        public int AdminId;
        bool refreshAccountdetails = true;

     
        public admin_frame(MainWindow root,int id)
        {
            parent = root;
            AdminId = id;
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
            statusText.Foreground = Brushes.Yellow;
            statusCircle.Fill = Brushes.Yellow;
            statusText.Content = "Connecting to database";
            loadAdminAccount();
            all_account newPage = new all_account(parent,this);
            pageTransitionControl.TransitionType = PageTransitionType.Fade;
            pageTransitionControl.ShowPage(newPage);
        }

        /// <summary>
        /// Load account information
        /// </summary>
        public void loadAdminAccount()
        {
            DataTable qry = parent.query("select username,(select photoblob from photo where photo.id=login.user_id) as photo from login where user_id=" + AdminId);

            foreach (DataRow rw in qry.Rows)
            {
                userLabel.Content = rw["username"].ToString();
               image1.Source = parent.getImg((byte[])rw["photo"]);
              
            }
        }

        /// <summary>
        /// Indicate database connection status
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
                loadAdminAccount();  
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

        //navigations

        private void new_button_Click(object sender, RoutedEventArgs e)
        {
            new_account newPage = new new_account(parent);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);
        }

        private void all_account_Click(object sender, RoutedEventArgs e)
        {
            all_account newPage = new all_account(parent,this);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);
        }

        private void doctor_account_Click(object sender, RoutedEventArgs e)
        {
            doctor_account newPage = new doctor_account(parent,this);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);
        }

        private void parent_account_Click(object sender, RoutedEventArgs e)
        {
            patient_account newPage = new patient_account(parent,this);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);

        }


        private void pharmacy_Click(object sender, RoutedEventArgs e)
        {
            pharmacy newPage = new pharmacy(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);
        }


        private void settings_Click(object sender, RoutedEventArgs e)
        {
            settings newPage = new settings(parent, this);
            pageTransitionControl.TransitionType = PageTransitionType.SlideAndFade;
            pageTransitionControl.ShowPage(newPage);
        }


        /// <summary>
        /// Delete  account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        public void DeleteAccount(int id,string category)
        {
            if(category.Equals("Patient"))
            {
               DataTable qry= parent.query("select patientid from patient where login_user_id="+id);
               if(qry.Rows.Count>0)
                {
                    DataRow rw = qry.Rows[0];
                    string pid=(rw[0].ToString());


                    parent.query("delete from bills where patientid=" + pid);
                     parent.query("delete from messages where sender_user_id=" + id);
                    parent.query("delete from messages where reciever_user_id=" + id);
                    parent.query("delete from message_thread where started_by_user_id=" + id);
                    parent.query("delete from message_thread where delete_user_id=" + id);

                    DataTable qry2 = parent.query("select recordid from record where patientid =" + pid);
                    foreach(DataRow rw2 in qry2.Rows)
                    {
                        parent.query("delete from prescription where recordid=" + rw2[0]);
                        parent.query("delete from result where recordid=" + rw2[0]);
                        parent.query("delete from record where recordid=" + rw2[0]);
 
                    }

                    parent.query("delete from schedule where patientid=" + pid);
                    parent.query("delete from photo where id=" + id);
                    parent.query("delete from cart where login_user_id=" + id);
                     parent.query("delete from patient where patientid=" + pid);
                    parent.query("delete from login where user_id=" + id);



                }
            }

            if (category.Equals("Doctor"))
            {

                DataTable qry = parent.query("select doctorid from doctor where login_user_id=" + id);
                if (qry.Rows.Count > 0)
                {
                    DataRow rw = qry.Rows[0];
                    string did = (rw[0].ToString());

                    parent.query("delete from bills where doctorid=" + did);
                     parent.query("delete from messages where sender_user_id=" + id);
                    parent.query("delete from messages where reciever_user_id=" + id);
                    parent.query("delete from message_thread where started_by_user_id=" + id);
                    parent.query("delete from message_thread where delete_user_id=" + id);


                    DataTable qry2 = parent.query("select recordid from record where doctorid =" + did);
                    foreach (DataRow rw2 in qry2.Rows)
                    {
                        parent.query("delete from prescription where recordid=" + rw2[0]);
                        parent.query("delete from result where recordid=" + rw2[0]);
                        parent.query("delete from record where recordid=" + rw2[0]);

                    }

                    parent.query("delete from schedule where doctorid=" + did);
                    parent.query("delete from photo where id=" + id);
                    parent.query("delete from doctor where doctorid=" + did);
                    parent.query("delete from login where user_id=" + id);


                }

            }

           
        }




        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(MessageBox.Show("Logout from account?","TeleMedicine",MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.Yes)
            {
                parent.signout(AdminId);
            }
        }

    }
}
