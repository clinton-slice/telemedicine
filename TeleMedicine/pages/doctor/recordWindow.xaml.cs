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
    /// Interaction logic for recordWindow.xaml
    /// </summary>
    public partial class recordWindow : Window
    {
        MainWindow parent;
        profile subparent;
        string operate;
        int _id;
        int update_patient_id;
        string doc_name;


        /// <summary>
        /// Get information of patient
        /// </summary>
        /// <param name="root"></param>
        /// <param name="sub"></param>
        /// <param name="id"></param>
        /// <param name="operation"></param>
        public recordWindow(MainWindow root, profile sub,int id,string operation)
        {
            parent = root;
            subparent = sub;
            _id = id;
            operate = operation;
            InitializeComponent();
            if(operation.Equals("new"))
            {
                DataTable qry = root.query("select  (select concat(lastname,' ',firstname) as patient from patient where patientid="+id+"),(select concat(lastname,' ',firstname) from doctor where doctorid="+subparent.subparent.DoctorId+ "),(select photoblob from photo where id=(select login_user_id from patient where patientid="+id+" ) ) as photo");
                if (qry.Rows.Count > 0)
                {
                    DataRow rw = qry.Rows[0];
                    patientName.Content = rw[0];
                    doctorLabel.Content = "Doctor: "+rw[1];
                    doc_name = rw[1].ToString();
                    dateLabel.Content = "Date: "+DateTime.Now.ToLongDateString();
                    profilePix.Source = parent.getImg((byte[])rw["photo"]);
                }
            }


            if (operation.Equals("update"))
            {
                DataTable qry = root.query("select patientid,comment,date_added, (select concat(lastname,' ',firstname) as patient from patient where patientid=record.patientid) as patient,(select concat(lastname,' ',firstname) from doctor where doctorid=record.doctorid) as doctor,(select photoblob from photo where id=(select login_user_id from patient where patientid=record.patientid ) ) as photo from record where recordid=" + id);
                if (qry.Rows.Count > 0)
                {
                    DataRow rw = qry.Rows[0];
                    patientName.Content = rw["patient"];
                    doctorLabel.Content = "Doctor: " + rw["doctor"];
                    doc_name = rw["doctor"].ToString();
                    dateLabel.Content = "Date: " + DateTime.Parse(rw["date_added"].ToString()).ToLongDateString();
                    profilePix.Source = parent.getImg((byte[])rw["photo"]);
                    update_patient_id = int.Parse(rw["patientid"].ToString());
                    commentBox.AppendText(rw["comment"].ToString());
                }


                  qry = root.query("select details from result where recordid=" + id);
                  
                foreach(DataRow rw in qry.Rows)
                {
                    resultList.Items.Add(rw["details"].ToString());
                }


                qry = root.query("select details from prescription where recordid=" + id);

                foreach (DataRow rw in qry.Rows)
                {
                    presList.Items.Add(rw["details"].ToString());
                }



            }

        }

        /// <summary>
        /// Save changes/ add record to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if(operate.Equals("new"))
            {
                string comment = "";
                TextRange range = new TextRange(commentBox.Document.ContentStart, commentBox.Document.ContentEnd);
                comment = range.Text;
               bool result= parent.addRecordInformation(_id, subparent.subparent.DoctorId, comment);

                if(result)
                {

                 
                    int id = -1;
                    DataTable qry = parent.query("select max(recordid) from record where patientid="+_id);
                    if(qry.Rows.Count>0)
                    {
                        DataRow rw = qry.Rows[0];
                        id = int.Parse(rw[0].ToString());
                    }

                    foreach (string itm in resultList.Items)
                    {
                        parent.addResultInformation(id,(itm));
                    }

                    foreach (string itm in presList.Items)
                    {
                        parent.addPrescriptionInformation(id, (itm));
                    }


                    string caption ="";

                    if (comment.Length > 20)
                        caption = comment.Substring(0, 15) + "...";
                    else
                        caption = comment;


                     subparent.addToList(id, doc_name, caption, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                 

                    MessageBox.Show("Record added successfully", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);


                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to save new record", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }






            if (operate.Equals("update"))
            {
                string comment = "";
                TextRange range = new TextRange(commentBox.Document.ContentStart, commentBox.Document.ContentEnd);
                comment = range.Text;
                bool result = parent.updateRecordInformation(_id, update_patient_id, subparent.subparent.DoctorId, comment);

                if (result)
                {
 
                     parent.query("delete from result where recordid=" + _id);
                    parent.query("delete from prescription where recordid=" + _id);


                    foreach (string itm in resultList.Items)
                    {
                        parent.addResultInformation(_id, (itm));
                    }

                    foreach (string itm in presList.Items)
                    {
                        parent.addPrescriptionInformation(_id, (itm));
                    }


                   
                    MessageBox.Show("Record updated successfully", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);


                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update record", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }


 


        }

        private void addResult_Click(object sender, RoutedEventArgs e)
        {
            if (!new TextRange(resultBox.Document.ContentStart, resultBox.Document.ContentEnd).Text.Trim().Equals(""))
            {
                resultList.Items.Add(new TextRange(resultBox.Document.ContentStart, resultBox.Document.ContentEnd).Text);
                resultBox.Document.Blocks.Clear();
            }
        }

        private void deleteResult_Click(object sender, RoutedEventArgs e)
        {
            if(resultList.SelectedIndex>-1)
            {
                resultList.Items.Remove(resultList.SelectedItem);
            }
        }

        private void addPres_Click(object sender, RoutedEventArgs e)
        {
            if (!new TextRange(presBox.Document.ContentStart, presBox.Document.ContentEnd).Text.Trim().Equals(""))
            {
                presList.Items.Add(new TextRange(presBox.Document.ContentStart, presBox.Document.ContentEnd).Text);
                presBox.Document.Blocks.Clear();
            }
        }

        private void deletePres_Click(object sender, RoutedEventArgs e)
        {
            if (presList.SelectedIndex > -1)
            {
                presList.Items.Remove(presList.SelectedItem);
            }
        }







    }
}
