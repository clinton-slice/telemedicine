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

namespace TeleMedicine.pages.admin
{
    /// <summary>
    /// Interaction logic for all_account.xaml
    /// </summary>
    public partial class doctor_account : UserControl
    {
        MainWindow parent;
        admin_frame subparent;
        DispatcherTimer timer;

        public doctor_account(MainWindow root, admin_frame _sub)
        {
            parent = root;
            subparent = _sub;
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
        }


        /// <summary>
        /// Load up filter options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_tick(object sender, EventArgs e)
        {
            filtercomboBox.Items.Add("Show All");

            for (char _ind = 'A'; _ind <= 'Z'; _ind++)
            {
                filtercomboBox.Items.Add("Begins with '" + _ind + "'");
            }

            filtercomboBox.SelectedIndex = 0;


            timer.Stop();
        }


        public void load()
        {
            listView.Items.Clear();
            DataTable qry = parent.query("select user_id,'Doctor' as category,username,(select firstname from doctor where doctor.login_user_id=login.user_id) as firstname,(select lastname from doctor where doctor.login_user_id=login.user_id) as lastname, (select photoblob from photo where login.user_id=photo.id) as photo  from login where (select count(*) from doctor where login_user_id=login.user_id)>0 order by lastname ");
            foreach (DataRow rw in qry.Rows)
            {
                AccountHolder account = new AccountHolder();
                account.ID = int.Parse(rw["user_id"].ToString());
                account.category = rw["category"].ToString();
                account.fullname = rw["lastname"].ToString() + " " + rw["firstname"].ToString();
                account.username = rw["username"].ToString();
                account.photo = parent.getImg((byte[])rw["photo"]);
                listView.Items.Add(account);
            }
            resultLabel.Content = "Result: " + qry.Rows.Count + " found";
        }


        /// <summary>
        /// Load basic information of doctors whose lastname starts with select letter
        /// </summary>
        /// <param name="_index"></param>

        public void load(string _index)
        {
            listView.Items.Clear();
            DataTable qry = parent.query("select user_id,'Doctor' as category,username,(select firstname from doctor where doctor.login_user_id=login.user_id) as firstname,(select lastname from doctor where doctor.login_user_id=login.user_id) as lastname, (select photoblob from photo where login.user_id=photo.id) as photo  from login where (select count(*) from doctor where login_user_id=login.user_id and lastname like '" + _index + "%')>0 order by lastname  ");

            foreach (DataRow rw in qry.Rows)
            {
                AccountHolder account = new AccountHolder();
                account.ID = int.Parse(rw["user_id"].ToString());
                account.category = rw["category"].ToString();
                account.fullname = rw["lastname"].ToString() + " " + rw["firstname"].ToString();
                account.username = rw["username"].ToString();
                account.photo = parent.getImg((byte[])rw["photo"]);
                listView.Items.Add(account);
            }
            resultLabel.Content = "Result: " + qry.Rows.Count + " found";
        }



        private void filtercomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filtercomboBox.SelectedIndex > 0)
            {
                int offset = 1;
                for (char _ind = 'A'; _ind <= 'Z'; _ind++, offset++)
                {
                    if (filtercomboBox.SelectedIndex == offset)
                    {
                        load(_ind.ToString());
                        break;
                    }
                }
            }
            else
            {
                load();
            }
        }


        /// <summary>
        /// Delete doctor's account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCategory(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            AccountHolder account = b.CommandParameter as AccountHolder;
            if (MessageBox.Show("Delete " + account.fullname + "'s account?\nNote: All information about this account and relationship with patients would be deleted", "TeleMedicine", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            { subparent.DeleteAccount(account.ID, account.category); listView.Items.Remove(account); }
        }


        /// <summary>
        /// Reset doctor's account password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetCategory(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            AccountHolder account = b.CommandParameter as AccountHolder;
            if (MessageBox.Show("Reset " + account.fullname + "'s account password?", "TeleMedicine", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                if (parent.Dbstate.Equals("Open"))
                {
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    var random = new Random();
                    string gen_pass = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
                    string hash_gen = security.EncryptStringAES(gen_pass, parent.cipher_text);
                    parent.query("update login set password='', d_password='" + hash_gen + "' where user_id=" + account.ID);
                    MessageBox.Show("New default password for " + account.fullname + "\n" + gen_pass, "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Database disconnected", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Hand);

                }

            }
        }


    }
}
