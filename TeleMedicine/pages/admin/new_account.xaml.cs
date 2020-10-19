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
using System.Data;
using System.Windows.Shapes;

namespace TeleMedicine.pages.admin
{
    /// <summary>
    /// Interaction logic for all_account.xaml
    /// </summary>
    
    
    
    public partial class new_account : UserControl
    {
        MainWindow parent;

        public new_account(MainWindow root)
        {
            InitializeComponent();
            parent = root;
            generatePassword();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            generatePassword();
        }

        /// <summary>
        /// Generate ranom password
        /// </summary>
        public void generatePassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            genPass.Content = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
           
        }

        /// <summary>
        /// Check for account creation requirements before saving it in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if(firstnameBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Firstname is required", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
            
            else if(lastnameBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Lastname is required", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (usernameBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Username is required", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (lastnameBox.Text.Length < 3)
            {
                MessageBox.Show("Lastname too short", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (firstnameBox.Text.Length<3)
            {
                MessageBox.Show("Firstname too short", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (usernameBox.Text.Length < 3)
            {
                MessageBox.Show("Username too short", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                submitButton.IsEnabled = false;
                submitButton.Content = "5%";
                usernameBox.Text = parent.clean(usernameBox.Text);

                DataTable verify_tb = parent.query("select user_id from login where username='" + usernameBox.Text.Trim() + "' ");
                if (verify_tb.Rows.Count == 0)
                {

                    string category = "";
                    string hash_gen = security.EncryptStringAES(genPass.Content.ToString().Trim(), parent.cipher_text);
                    category = (radioButton1.IsChecked == true) ? "doctor" : "patient";
                    parent.query("insert login (username,d_password,role,dateadded) values('" + usernameBox.Text.Trim() + "','" + hash_gen + "','" + category + "',now())");
                    submitButton.Content = "30%";
                    DataTable table = parent.query("select user_id from login where username='" + usernameBox.Text.Trim() + "' and d_password='" + hash_gen + "'");
                    if (table.Rows.Count > 0)
                    {
                        submitButton.Content = "90%";
                        lastnameBox.Text = parent.formatname(parent.clean(lastnameBox.Text).ToLower());
                        firstnameBox.Text = parent.formatname(parent.clean(firstnameBox.Text));
                        string id = table.Rows[0][0].ToString();

                        if (category.Equals("patient"))
                        {
                            string code = lastnameBox.Text.Substring(0, 3).ToUpper() + firstnameBox.Text.Substring(0, 2).ToUpper();
                            table = parent.query("select patientid from patient where PCODE like '" + code + "%' ");
                            int count = table.Rows.Count;
                            count++;
                            if (count < 10)
                                code += "00" + count;
                            else if (count < 100)
                                code += "0" + count;
                            else
                                code += count;

                            parent.query("insert  into " + category + " (login_user_id,lastname,firstname,PCODE) values(" + id + ",'" + lastnameBox.Text.Trim() + "','" + firstnameBox.Text.Trim() + "','" + code + "')");

                        }
                        else
                        {
                            parent.query("insert  into " + category + " (login_user_id,lastname,firstname) values(" + id + ",'" + lastnameBox.Text.Trim() + "','" + firstnameBox.Text.Trim() + "')");
                        }
                        usernameBox.Text = lastnameBox.Text = firstnameBox.Text = "";
                        generatePassword();
                        parent.query("insert into photo (id) values(" + id + ")");
                        parent.savePhoto(int.Parse(id), Properties.Resources._default);
                        MessageBox.Show("Account creation completed", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Account creation failed", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                }
                else
                {
                    MessageBox.Show("Username already assigned to another account", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);

                }


                submitButton.Content = "Submit";
                submitButton.IsEnabled = true;
            }


        }

    
    }
}
