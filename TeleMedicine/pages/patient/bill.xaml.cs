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
    /// Interaction logic for result.xaml
    /// </summary>
    public partial class bill : UserControl
    {
        MainWindow parent;
        patient_frame subparent;


        public class BillHolder
        {
            public int ID { set; get; }
            public string amount { get; set; }
            public string date { get; set; }
   
        }

        /// <summary>
        /// Load up all bill history
        /// </summary>
        /// <param name="root"></param>
        /// <param name="_sub"></param>
        public bill(MainWindow root, patient_frame _sub)
        {
            parent = root;
            subparent = _sub;
            InitializeComponent();

            DataTable tab = parent.query("select amount,date from bills where patientid="+subparent.PatientId+" order by bill_id");
            int Count = 0;
            foreach (DataRow rw in tab.Rows)
            {
                Count++;
                BillHolder bill = new BillHolder();
                bill.ID = Count;
                bill.amount = rw["amount"].ToString();
                bill.date = rw["date"].ToString();

                listView1.Items.Add(bill);
            }

        }


        /// <summary>
        /// Return to main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            subparent.loadDashboard();
        }


    }
}
