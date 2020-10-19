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

namespace TeleMedicine.pages.patient
{
    /// <summary>
    /// Interaction logic for dashboard.xaml
    /// </summary>
    public partial class dashboard : UserControl
    {
        MainWindow parent;
        patient_frame subparent;

        DispatcherTimer timer;


        public dashboard(MainWindow root, patient_frame subroot)
        {
            parent = root;
            subparent = subroot;
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Start();
        }


        /// <summary>
        /// Timer to check for new appointments and messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_tick(object sender, EventArgs e)
        {

           DataTable qry= parent.query("select (select count(*) from schedule where convert(date,date)=str_to_date('" + DateTime.Now.ToShortDateString() + "','%m/%d/%Y') and patientid="+subparent.PatientId+"), (select count(*) from messages where reciever_read='No' and reciever_user_id="+subparent.AccountId+")");

            foreach (DataRow rw in qry.Rows)
            {
                if(!rw[0].ToString().Equals("0"))
                   status1.Content = rw[0].ToString()+" Appointment(s) today";
                else
                {
                    status1.Content = "No Appointment(s) today";
                }

                if (!rw[1].ToString().Equals("0"))
                    status2.Content = rw[1].ToString() + " New Message(s)";
                else
                {
                    status2.Content = "No New Message(s)";
                }

            }
            

        }
        private void rect1_MouseEnter(object sender, MouseEventArgs e)
        {
            rect1.Fill = Brushes.WhiteSmoke;
             
        }

        private void rect1_MouseLeave(object sender, MouseEventArgs e)
        {
            rect1.Fill = Brushes.Transparent;
        }

        private void rect2_MouseEnter(object sender, MouseEventArgs e)
        {
            rect2.Fill = Brushes.WhiteSmoke;
        }

        private void rect2_MouseLeave(object sender, MouseEventArgs e)
        {
            rect2.Fill = Brushes.Transparent;
        }

        private void rect3_MouseEnter(object sender, MouseEventArgs e)
        {
            rect3.Fill = Brushes.WhiteSmoke;
        }

        private void rect3_MouseLeave(object sender, MouseEventArgs e)
        {
            rect3.Fill = Brushes.Transparent;
        }

        private void rect4_MouseEnter(object sender, MouseEventArgs e)
        {
            rect4.Fill = Brushes.WhiteSmoke;
        }

        private void rect4_MouseLeave(object sender, MouseEventArgs e)
        {
            rect4.Fill = Brushes.Transparent;
        }

        private void rect5_MouseEnter(object sender, MouseEventArgs e)
        {
            rect5.Fill = Brushes.WhiteSmoke;
        }

        private void rect5_MouseLeave(object sender, MouseEventArgs e)
        {
            rect5.Fill = Brushes.Transparent;
        }

        private void rect6_MouseEnter(object sender, MouseEventArgs e)
        {
            rect6.Fill = Brushes.WhiteSmoke;
        }

        private void rect6_MouseLeave(object sender, MouseEventArgs e)
        {
            rect6.Fill = Brushes.Transparent;
        }

        private void rect1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            subparent.loadAppointment();
        }

        private void rect2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            subparent.loadMessage();
        }

        private void rect3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            subparent.loadResult();
        }

        private void rect4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            subparent.loadBill();
        }

        private void rect5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            subparent.loadPharmacy();
        }

        private void rect6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            subparent.loadSetting();
        }
    }

}
