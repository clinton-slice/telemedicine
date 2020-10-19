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
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.Drawing;
 

namespace TeleMedicine.pages.patient
{
    /// <summary>
    /// Interaction logic for all_account.xaml
    /// </summary>
    
    
    
    public partial class settings : UserControl
    {
        MainWindow parent;
        patient_frame subparent;

        public settings(MainWindow root, patient_frame _sub)
        {
            InitializeComponent();
            parent = root;
            subparent = _sub;
            profilePix.Source = subparent.image1.Source;

            loadAccountInfo();

        }

        /// <summary>
        /// Confirm password match. Change password of user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changePass_Click(object sender, RoutedEventArgs e)
        {
            if (c_pass.Password.Trim().Equals(""))
            { MessageBox.Show("Current password is empty!", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (n_pass.Password.Trim().Equals(""))
            { MessageBox.Show("New password is empty!", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (n_pass.Password.Trim() != c_n_pass.Password.Trim())
            { MessageBox.Show("Password mismatch!", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {

                changePass.IsEnabled = false;
                bool changed = parent.changepassword(subparent.AccountId, c_pass.Password, n_pass.Password);
                changePass.IsEnabled = true;
                changePass.Content = "Change Password";
                if (changed)
                {
                   c_pass.Password= c_n_pass.Password= n_pass.Password = "";
                    MessageBox.Show("Password changed successfully", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Authentication failed! Check your current password or you are offline", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        /// <summary>
        /// Get Account information of user
        /// </summary>
        public void loadAccountInfo()
        {
            DataTable qry = parent.query("select * from patient where patientid="+subparent.PatientId);
            if(qry.Rows.Count>0)
            {
                foreach (DataRow row in qry.Rows)
                {
                    firstnameBox.Text = row["firstname"].ToString();
                    lastnameBox.Text = row["lastname"].ToString();
                    phoneBox.Text = row["phoneno"].ToString();
                    emailBox.Text = row["email"].ToString();
                    weight.Text = row["weight"].ToString();
                    height.Text = row["height"].ToString();
                    blood.Text = row["bloodgroup"].ToString();
                    genotype.Text = row["genotype"].ToString();
                    if (row["sex"].ToString().Equals("Male"))
                    {
                        male.IsChecked = true;
                        female.IsChecked = false;
                    }

                    if (row["sex"].ToString().Equals("Female"))
                    {
                        male.IsChecked = false;
                        female.IsChecked = true;
                    }

                    address.Document.Blocks.Clear();
                    address.AppendText(row["address"].ToString());

                    allergies.Document.Blocks.Clear();
                    allergies.AppendText(row["allergies"].ToString());

                }
            }
        }


        /// <summary>
        /// Upload new profile picture of user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "JPG Files|*.jpg|JPEG Files|*.jpeg | PNG Files|*.png|GIF Files|*.gif";
            if(dialog.ShowDialog()==true)
            {

                uploadButton.IsEnabled = false;
                string filename=(dialog.FileName);
                if (new System.IO.FileInfo(filename).Length <= 184320)
                {
                    Bitmap img = null;
                    try
                    {
                        img = new Bitmap(filename);
                    }
                    catch (Exception) { MessageBox.Show("Failed to read file", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error); }

                    if (img != null)
                    {
                        bool result = parent.savePhoto(subparent.AccountId, img);
                        if (result)
                        {
                            profilePix.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(img.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                            MessageBox.Show("Image upload completed", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                            subparent.loadPatientAccount();
                        }
                        else
                        {
                            MessageBox.Show("Image upload failed. Ensure you are connected", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Image upload failed", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                { MessageBox.Show("Image upload failed. File size limit is 180kb", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error); }
                uploadButton.IsEnabled = true;
            }
        }

      

        /// <summary>
        /// Update information of patient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            string sex = "";

            if (male.IsChecked == true)
                sex = "Male";
            if (female.IsChecked == true)
                sex = "Female";

            if (lastnameBox.Text.Equals("") || firstnameBox.Text.Equals("") || sex.Equals(""))
            {
                MessageBox.Show("Incomplete Details\nLastname, Firstname, Sex is required","TeleMedicine",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                bool result= parent.updatePatientInformation(subparent.PatientId,lastnameBox.Text,firstnameBox.Text,sex,new TextRange(address.Document.ContentStart,address.Document.ContentEnd).Text,emailBox.Text,phoneBox.Text, new TextRange(allergies.Document.ContentStart, allergies.Document.ContentEnd).Text,blood.Text,genotype.Text,height.Text,weight.Text);

             
                if (result)
                {
                    MessageBox.Show("Information updated successfully", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                    loadAccountInfo();
                }
                else
                {
                    MessageBox.Show("Failed to update", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            subparent.loadDashboard();
        }
    }
}
