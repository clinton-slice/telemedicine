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
    /// Interaction logic for profile.xaml
    /// </summary>
    public partial class profile : UserControl
    {
        MainWindow parent;
        public doctor_frame subparent;
        int patientid;
        string doc_name = "";

        public class RecordHolder
        {
            public int record_id { set; get; }
             public string record_doctor { get; set; }
            public string record_comment { get; set; }
            public string record_date { get; set; }
         
        }

        public class bills
        {
            public string amount { set; get; }
            public string doctor { set; get; }
            public string date { get; set; }
        }


        /// <summary>
        /// Load all information of a patient
        /// </summary>
        /// <param name="root"></param>
        /// <param name="_sub"></param>
        /// <param name="id"></param>
        /// <param name="category"></param>
        public profile(MainWindow root, doctor_frame _sub, int id, string category)
        {
            parent = root;
            patientid = id;
            subparent = _sub;
            InitializeComponent();
            
         

            DataTable qry = parent.query("select *,(select photoblob from photo where login_user_id=photo.id) as photo from patient where patientid="+id);
            foreach(DataRow rw in qry.Rows)
            {
                proPix.Source = parent.getImg((byte[])rw["photo"]);
                fullname.Content = rw["lastname"] + " " + rw["firstname"];
                patientCode.Content = rw["PCODE"].ToString();
                email.Content = rw["email"].ToString();

                lastnameBlock.Text = rw["lastname"].ToString();
                firstnameBlock.Text = rw["firstname"].ToString();
                phoneBlock.Text = rw["phoneno"].ToString();
                genderBlock.Text = rw["sex"].ToString();
                heightBlock.Text = rw["height"].ToString();
                weightBlock.Text = rw["weight"].ToString();
                addressBlock.Text = rw["address"].ToString();
                bloodgroupBlock.Text = rw["bloodgroup"].ToString();
                genotypeBlock.Text = rw["genotype"].ToString();
                allergyBlock.Text = rw["allergies"].ToString();
            }


           qry = parent.query("select scheduleid,patientid, status as attended, schedule.date as schedule, date_added as created from schedule where patientid=" + patientid+" order by schedule desc");
            foreach (DataRow rw in qry.Rows)
            {
                schedule.ScheduleHolder schedule = new schedule.ScheduleHolder();
                schedule.ID = int.Parse(rw["scheduleid"].ToString());
                schedule.PatientID = int.Parse(rw["patientid"].ToString());
                schedule.attended = (rw["attended"].ToString());
                schedule.schedule = (rw["schedule"].ToString());
                schedule.created = (rw["created"].ToString());
                comboBox.Items.Add(rw["schedule"].ToString());

                listView0.Items.Add(schedule);
            }

            qry = parent.query("select concat(lastname,' ',firstname) from doctor where doctorid="+subparent.DoctorId);
              if(qry.Rows.Count>0)
               {
                DataRow row = qry.Rows[0];
                doc_name = row[0].ToString();
               }

            qry = parent.query("select *,(select concat(lastname,' ',firstname) from doctor where doctor.doctorid=bills.doctorid) as doctor from bills where patientid=" + patientid);
            foreach (DataRow rw in qry.Rows)
            {
                bills _bill = new bills();
                _bill.amount = rw["amount"].ToString();
                _bill.date = rw["date"].ToString();
                _bill.doctor = rw["doctor"].ToString();

                listView1.Items.Add(_bill);
            }

            if (category.Equals("appointment"))
            {
                tabControl.SelectedIndex = 4;
            }

            qry = parent.query("select *,(select concat(lastname,' ',firstname) from doctor where doctor.doctorid=record.doctorid) as doctor from record where patientid=" + id);
            foreach (DataRow rw in qry.Rows)
            {

                 id = int.Parse(rw["recordid"].ToString());

                string caption = "";
                string comment = rw["comment"].ToString();
                if (comment.Length > 20)
                    caption = comment.Substring(0, 15) + "...";
                else
                    caption = comment;

               addToList(id, rw["doctor"].ToString(), caption, rw["date_added"].ToString());



            }

            recordCount.Content = qry.Rows.Count + " record(s)";

            }


        /// <summary>
        /// Initiate adding a new record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctor"></param>
        /// <param name="comment"></param>
        /// <param name="date"></param>
        public void addToList(int id,string doctor,string comment,string date)
        {

            RecordHolder _record = new RecordHolder();
            _record.record_id = id;
            _record.record_doctor = doctor;
            _record.record_comment = comment;
            _record.record_date = date;

            recordList.Items.Add(_record);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            new recordWindow(parent,this,patientid,"new").ShowDialog();
        }

        private void recordList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Edit a record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recordList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (recordList.SelectedIndex > -1)
            {
                RecordHolder holder = (RecordHolder)(sender as ListView).SelectedItem;
                new recordWindow(parent, this, holder.record_id, "update").ShowDialog();
            }
        }

        /// <summary>
        /// Save changes made to schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex > -1)
            {
              
                foreach (schedule.ScheduleHolder itm in listView0.Items)
                {
                    
                    if (itm.schedule.Equals(comboBox.Items[comboBox.SelectedIndex]))
                    {
                         itm.attended = "Yes";
                        string time = parent.convertTo24HoursFormat(itm.schedule.Split(new Char[] { ' ' })[1]+" "+ itm.schedule.Split(new Char[] { ' ' })[2]);
                        parent.query("update schedule set status='Yes' where date= '"+ parent.reverseDateFormat(itm.schedule) +" "+time+ "' and patientid="+patientid);
                          listView0.Items.Refresh();
                        break;
                    }

                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex > -1)
            {
                foreach (schedule.ScheduleHolder itm in listView0.Items)
                {
                    if (itm.schedule.Equals(comboBox.Items[comboBox.SelectedIndex]))
                    {
                        itm.attended = "No";
                        string time = parent.convertTo24HoursFormat(itm.schedule.Split(new Char[] { ' ' })[1] + " " + itm.schedule.Split(new Char[] { ' ' })[2]);
                        parent.query("update schedule set status='No' where date='" + parent.reverseDateFormat(itm.schedule) + " " + time + "' and patientid=" + patientid);

                        listView0.Items.Refresh();
                        break;
                    }

                }
            }
        }

        /// <summary>
        /// Add bill information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if(amountBox.Text.Trim().Length>0)
            {
                if (parent.Dbstate.Equals("Open"))
                {
                    parent.query("insert into bills (amount,date,patientid,doctorid) values('" + parent.clean(amountBox.Text) + "',now()," + patientid + ","+subparent.DoctorId+")");

                    bills _bill = new bills();
                    _bill.amount = amountBox.Text.Trim();
                    _bill.date = DateTime.Now.ToLocalTime().ToString();
                    _bill.doctor = doc_name;

                    listView1.Items.Add(_bill);
                    MessageBox.Show("Bill added", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
