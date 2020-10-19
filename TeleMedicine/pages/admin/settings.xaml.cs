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
 

namespace TeleMedicine.pages.admin
{
    /// <summary>
    /// Interaction logic for all_account.xaml
    /// </summary>
    
    
    
    public partial class settings : UserControl
    {
        MainWindow parent;
        admin_frame subparent;

        public settings(MainWindow root, admin_frame _sub)
        {
            InitializeComponent();
            parent = root;
            subparent = _sub;
            profilePix.Source = subparent.image1.Source;
        }

        /// <summary>
        /// Update account password 
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
                bool changed = parent.changepassword(subparent.AdminId, c_pass.Password, n_pass.Password);
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
        /// upload new account photo
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
                        bool result = parent.savePhoto(subparent.AdminId, img);
                        if (result)
                        {
                            profilePix.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(img.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                            MessageBox.Show("Image upload completed", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                            subparent.loadAdminAccount();
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
    }
}
