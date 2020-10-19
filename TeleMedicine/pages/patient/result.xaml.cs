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
    public partial class result : UserControl
    {
        MainWindow parent;
        patient_frame subparent;
        List<int> id = new List<int>();

        /// <summary>
        /// Load up result history
        /// </summary>
        /// <param name="root"></param>
        /// <param name="_sub"></param>
        public result(MainWindow root, patient_frame _sub)
        {
            parent = root;
            subparent = _sub;
            InitializeComponent();

            DataTable qry = parent.query("select recordid,date_added from record where patientid=" + subparent.PatientId+" order by recordid");
            
            foreach(DataRow rw in qry.Rows)
            {
                id.Add(int.Parse(rw["recordid"].ToString()));
                listBox.Items.Add(rw["date_added"].ToString());
            }

        }


        private void home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            subparent.loadDashboard();
        }

        /// <summary>
        /// Load details of a result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBox.SelectedItem!=null)
            {
                if(listBox.SelectedIndex>-1)
                {
                   load(id[listBox.SelectedIndex]);
                }
            }
        }


        public void load(int id)
        {
            DataTable _qry = parent.query("select comment from record where recordid=" + id);
            if(_qry.Rows.Count>0)
            {
                DataRow rw = _qry.Rows[0];
                commentBox.Text = rw[0].ToString();
            }

            listBox1.Items.Clear();
            listBox2.Items.Clear();

           _qry = parent.query("select details from result where recordid=" + id);
                
            foreach(DataRow rw in _qry.Rows)
            {
                listBox1.Items.Add(rw["details"].ToString());
            }


            _qry = parent.query("select details from prescription where recordid=" + id);

            foreach (DataRow rw in _qry.Rows)
            {
                listBox2.Items.Add(rw["details"].ToString());
            }

        }






    }
}
