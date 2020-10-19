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
using System.Windows.Shapes;
using System.Data;

namespace TeleMedicine.pages.doctor
{
    /// <summary>
    /// Interaction logic for searchWindow.xaml
    /// </summary>
    public partial class searchWindow : Window
    {
        doctor_frame subparent;

        public class SearchHolder
        {
            public int ID { set; get; }
             public string match { get; set; }
            public string fullname { get; set; }
            public string content { get; set; }

        }

        public searchWindow(doctor_frame subroot, string text)
        {
            subparent = subroot;
            InitializeComponent();
            this.Title = "Searching for " + text;
            search(text);
        }




        /// <summary>
        /// Find match in database with entered keyword
        /// </summary>
        /// <param name="text"></param>
        public void search(string text)
        {
            string _query = "select patientid,concat(lastname,' ',firstname) as fullname,'Patient ID' as category, PCODE as content from patient where PCODE like '" + text + "%' union "
                          + "select patientid, concat(lastname,' ', firstname) as fullname,'Lastname' as category, lastname as content from patient where lastname like  '" + text + "%' union "
                          + "select patientid,concat(lastname, ' ', firstname) as fullname,'Firstname' as category, firstname as content from patient where firstname like '" + text + "%' union "
                          + "select patientid,concat(lastname, ' ', firstname) as fullname,'Sex' as category, sex as content from patient where sex like  '" + text + "%' union "
                          + "select patientid,concat(lastname, ' ', firstname) as fullname,'Email' as category, email as content from patient where email like  '" + text + "%' union "
                          + "select patientid,concat(lastname, ' ', firstname) as fullname,'Phone' as category, phoneno as content from patient where phoneno like  '" + text + "%' "
                          + "order by fullname";

            DataTable qry = subparent.parent.query(_query);
            foreach (DataRow rw in qry.Rows)
            {
                SearchHolder search = new SearchHolder();
                search.ID = int.Parse(rw["patientid"].ToString());
                search.content = rw["content"].ToString();
                search.fullname = rw["fullname"].ToString();
                search.match = rw["category"].ToString();


                listView.Items.Add(search);
            }
            label.Content = "Result: " + qry.Rows.Count + " found";


        }


        /// <summary>
        /// Load profile of patient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void loadprofile(object sender,MouseButtonEventArgs e)
        {
            ListViewItem itm = sender as ListViewItem;
            SearchHolder search = itm.Content as SearchHolder;
            subparent.loadPatientInfo(search.ID,"profile");
            this.Close();
        }


    }
}
