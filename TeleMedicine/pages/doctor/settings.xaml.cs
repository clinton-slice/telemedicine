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
 

namespace TeleMedicine.pages.doctor
{
    /// <summary>
    /// Interaction logic for all_account.xaml
    /// </summary>
    
    
    
    public partial class settings : UserControl
    {
        MainWindow parent;
        doctor_frame subparent;

        public settings(MainWindow root, doctor_frame _sub)
        {
            InitializeComponent();
            parent = root;
            subparent = _sub;
            profilePix.Source = subparent.image1.Source;

            loadAccountInfo();

        }

        /// <summary>
        /// Confirm/Change of account password
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

        public void loadAccountInfo()
        {
            DataTable qry = parent.query("select concat(lastname,' ',firstname) as name,specialization from doctor where doctorid="+subparent.DoctorId);
            if(qry.Rows.Count>0)
            {
                DataRow row = qry.Rows[0];
                fullnameLabel.Content = row[0].ToString();
                specializationLabel.Text = row[1].ToString();
            }
        }


        /// <summary>
        /// Upload new profile picture
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
                            subparent.loadDoctorAccount();
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

        private void specializationBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextRange tr = new TextRange(specializationBox.Document.ContentStart, specializationBox.Document.ContentEnd);

            if(tr.Text.Length>=300 || e.Key==Key.Space || e.Key==Key.Enter )
            {
                e.Handled = true;
                return;
            }

        }

        /// <summary>
        /// Update profile information of doctor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(lastnameBox.Text.Equals("") || firstnameBox.Text.Equals(""))
            {
                MessageBox.Show("Incomplete Details","TeleMedicine",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                TextRange tr = new TextRange(specializationBox.Document.ContentStart, specializationBox.Document.ContentEnd);
               bool result= parent.updateDoctorInformation(subparent.DoctorId,lastnameBox.Text,firstnameBox.Text,tr.Text);

             
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
    }
}
