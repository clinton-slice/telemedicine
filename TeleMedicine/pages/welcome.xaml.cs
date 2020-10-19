using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace TeleMedicine.pages
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class welcome : UserControl
    {
        MainWindow parent;
        int user_id;
        string role = "";
        /// <summary>
        /// Fetch basic information of new user
        /// </summary>
        /// <param name="root"></param>
        /// <param name="id"></param>
        public welcome(MainWindow root, int id)
        {
            parent = root;
            user_id = id;
            InitializeComponent();
            DataTable qry = parent.query("select role,(select lastname from doctor where doctor.login_user_id=login.user_id) as lastname,(select firstname from doctor where doctor.login_user_id=login.user_id) as firstname from login where user_id=" + user_id + " and (select count(*) from doctor where doctor.login_user_id =" + user_id + ")>0 union select role,(select lastname from patient where patient.login_user_id=login.user_id) as lastname,(select firstname from patient where patient.login_user_id=login.user_id) as firstname from login where user_id=" + user_id + " and (select count(*) from patient where patient.login_user_id =" + user_id+")>0");
            foreach (DataRow rw in qry.Rows)
            {
               role=(rw["role"].ToString());
                name.Content = (rw["lastname"].ToString()) + " " + (rw["firstname"].ToString());
            }
        }

        /// <summary>
        /// Confirm password of new user before saving it in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void proceedbutton_Click(object sender, RoutedEventArgs e)
        {
            if(passbox.Password.Trim().Length>0 && cpassbox.Password.Trim().Length>0)
            {
                if(passbox.Password.Equals(cpassbox.Password))
                {
                    string hash_gen = security.EncryptStringAES(cpassbox.Password.ToString().Trim(), parent.cipher_text);
                    if(parent.Dbstate.Equals("Open"))
                    {
                        parent.query("update login set d_password='', password='"+hash_gen+"' where user_id="+user_id);
                        MessageBox.Show("Registration Completed", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                        parent.loadAccount(role, user_id);
                    }
                    else
                    {
                        MessageBox.Show("Database disconnected", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Hand);

                    }
                }
                else
                {
                    MessageBox.Show("Password mismatch", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Hand);

                }
            }
            else
            {
                MessageBox.Show("Incomplete details","TeleMedicine",MessageBoxButton.OK,MessageBoxImage.Hand);
            }


        }
    }
}
