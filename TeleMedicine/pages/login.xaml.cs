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
    public partial class login : UserControl
    {
       public MainWindow parent;
        public login(MainWindow root)
        {
            parent = root;
            InitializeComponent();
        }

        /// <summary>
        /// Authenticate login details. The password is decrypted from database when there is a username match
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginbutton_Click(object sender, RoutedEventArgs e)
        {
            loginbutton.IsEnabled = false;
            failedLab.Visibility = Visibility.Hidden;
            loginbutton.Content = "Authenticating..";
            if (passbox.Password.Trim().Length > 0 && userbox.Text.Trim().Length>0)
            {
                userbox.Text = parent.clean(userbox.Text);
                DataTable qry = parent.query("select user_id,role,password, d_password from login where username='" + userbox.Text + "' limit 1");
                bool match = false;
                bool new_account = false;
                int user_id = 0;
                string role = "";
                string _dipher = "";
                foreach (DataRow rw in qry.Rows)
                {
                    
                    user_id = int.Parse(rw["user_id"].ToString());
                    role = rw["role"].ToString();
                    if (rw["password"].ToString().Trim().Length > 0)
                        _dipher = security.DecryptStringAES(rw["password"].ToString(), parent.cipher_text);
                    else if (rw["d_password"].ToString().Trim().Length > 0)
                    { _dipher = security.DecryptStringAES(rw["d_password"].ToString(), parent.cipher_text); new_account = true; }
                    else
                        _dipher = "";

                    if(_dipher.Equals(passbox.Password.Trim()))
                    {
                        match = true;
                        if (new_account)
                            parent.loadAccount("new",user_id);
                        else
                            parent.loadAccount(role, user_id);

                    }

                }
                if (!match) { failedLab.Visibility = Visibility.Visible; failedLab.Content = "Invalid login details"; }
            }
            loginbutton.Content = "login";
            loginbutton.IsEnabled = true;
            if(!parent.Dbstate.Equals("Open"))
              { failedLab.Visibility = Visibility.Visible; failedLab.Content = "Database disconnected"; }
        }

        private void userbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            failedLab.Visibility = Visibility.Hidden;
        }

        private void passbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            failedLab.Visibility = Visibility.Hidden;
        }

        private void passbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                loginbutton_Click(this, null);
            }
        }

        private void label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new store(this).ShowDialog();
        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new faq(this).ShowDialog();
        }
    }
}
