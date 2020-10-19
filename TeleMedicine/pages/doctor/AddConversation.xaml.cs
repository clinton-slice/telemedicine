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
    /// Interaction logic for AddConversation.xaml
    /// </summary>
    public partial class AddConversation : Window
    {

        MainWindow parent;
        messages subparent;
        doctor_frame parent2;
        List<string> patients = new List<string>();

        /// <summary>
        /// Get list of all patients
        /// </summary>
        /// <param name="_root"></param>
        /// <param name="_root2"></param>
        /// <param name="_sub"></param>
        public AddConversation(MainWindow _root,doctor_frame _root2, messages _sub)
        {
            parent = _root;
            subparent = _sub;
            parent2 = _root2;
            InitializeComponent();
            DataTable qry = parent.query("select login_user_id, concat(lastname,' ',firstname) as name from patient");
            foreach(DataRow rw in qry.Rows)
            {
                patients.Add(rw["login_user_id"].ToString());
                patientList.Items.Add(rw["name"].ToString());
            }
        }


        /// <summary>
        /// Resume/start conversation with patient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if(patientList.SelectedIndex>-1)
            {
                string id = patients[patientList.SelectedIndex];

                DataTable qry = parent.query("select thread_id from messages where sender_user_id="+parent2.AccountId+ " and reciever_user_id=" + id + " or sender_user_id=" + id + " and reciever_user_id=" + parent2.AccountId + " group by thread_id desc limit 1");
                if (qry.Rows.Count > 0)
                {
                    DataRow row = qry.Rows[0];

                    foreach (StackPanel sp in subparent.conversationlist.Items)
                    {

                        if (sp.Name.Equals("Thread_" + row[0]))
                        {
                            subparent.conversationlist.SelectedItem = sp;
                            this.Close();
                        }
                    }

                }

                else
                {
                    parent.query("insert into message_thread (started_by_user_id) values(" + parent2.AccountId + ")");
                    int newThread = 0;
                    qry = parent.query("select thread_id from message_thread where started_by_user_id=" + parent2.AccountId + " and thread_id=(select max(thread_id) from message_thread)");
                    if (qry.Rows.Count > 0)
                    {
                        DataRow row = qry.Rows[0];
                        newThread = int.Parse(row[0].ToString());
                    }

                    if (newThread > 0)
                    {

                        qry = parent.query("select (select concat(lastname,' ',firstname) from patient where login_user_id=" + id + ") as name,(select photoblob from photo where id=" + id + ") as photo ");

                        foreach (DataRow rw in qry.Rows)
                        {

                            StackPanel _mSp = new StackPanel();
                            _mSp.Orientation = Orientation.Horizontal;
                            _mSp.Height = 60;
                            _mSp.Name = "Thread_" + newThread;

                            Image profilePix = new Image();
                            profilePix.Source = parent.getImg((byte[])rw["photo"]);
                            profilePix.Stretch = Stretch.UniformToFill;
                            profilePix.Width = 50;
                            profilePix.Height = 50;

                            _mSp.Children.Add(profilePix);


                            StackPanel _cSp = new StackPanel();
                            _cSp.Orientation = Orientation.Vertical;

                            Label name = new Label();
                            name.Content = "  " + rw["name"].ToString();
                            _cSp.Children.Add(name);


                            Label caption = new Label();
                            _cSp.Children.Add(caption);

                            _mSp.Children.Add(_cSp);
                            subparent.reciever_id = int.Parse(id);
                            subparent.refreshconversationlist(_mSp);
                            subparent.conversationlist.SelectedItem = _mSp;

                        }

                    }
                }
                this.Close();
            }
        }
    }
}
