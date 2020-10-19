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
using System.IO;
using System.Data;
using Microsoft.Win32;
 


namespace TeleMedicine.pages.admin
{
    /// <summary>
    /// Interaction logic for pharmacy.xaml
    /// </summary>
    public partial class pharmacy : UserControl
    {
        MainWindow parent;
        admin_frame subparent;
        System.Drawing.Bitmap img=null;
        int edit_id = 0;
        byte [] edit_img=null;

        public pharmacy(MainWindow root, admin_frame _sub)
        {
            parent = root;
            subparent = _sub;
            InitializeComponent();
            DataTable qry = parent.query("select * from drugs order by drug_name asc");
            listDrugs(qry);
        }

        /// <summary>
        /// Display list of drugs
        /// </summary>
        /// <param name="tab"></param>
        public void listDrugs(DataTable tab)
        {
            foreach (DataRow rw in tab.Rows)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Name = "sp_" + rw["drug_id"].ToString();

                Image Pix = new Image();
                Pix.Source = parent.getImg((byte[])rw["photo"]);
                Pix.Stretch = Stretch.UniformToFill;
                Pix.Width = 70;
                Pix.Height = 70;

               
                Label space = new Label();
                space.Width = 5;
                panel.Children.Add(space);

                panel.Children.Add(Pix);

                Label space2 = new Label();
                space2.Width = 20;
                panel.Children.Add(space2);

                StackPanel panel2 = new StackPanel();
                panel2.Orientation = Orientation.Vertical;

                StackPanel panel3 = new StackPanel();
                panel3.Orientation = Orientation.Horizontal;
                Label name = new Label();
                name.Content = rw["drug_name"].ToString();
                name.FontWeight = FontWeights.Bold;
                name.Width = 300;

                Label cost = new Label();
                cost.Content = rw["cost"].ToString();
                cost.FontWeight = FontWeights.Bold;

                panel3.Children.Add(name);
                panel3.Children.Add(cost);

                panel2.Children.Add(panel3);
                TextBlock description = new TextBlock();
                description.TextWrapping = TextWrapping.Wrap;
                description.Width = 500;
                description.Text =rw["description"].ToString();
                panel2.Children.Add(description);


                Label space3 = new Label();
                space3.Width = 5;
                panel2.Children.Add(space3);


                StackPanel panel4 = new StackPanel();
                panel4.Orientation = Orientation.Horizontal;


                Button button = new Button();
                button.Name = "edit";
                button.Name = "edit_" + rw["drug_id"].ToString();
                button.Content = "Edit";
                button.Click += EditButton_Click;
                button.Background = Brushes.Transparent;
                panel4.Children.Add(button);


                Label space4 = new Label();
                space4.Width = 5;
                panel4.Children.Add(space4);


                Button button2 = new Button();
                button2.Name = "del_"+rw["drug_id"].ToString(); 
                button2.Content = "Delete";
                button2.Click += DelButton_Click; 
                button2.Background = Brushes.Transparent;
                panel4.Children.Add(button2);

                panel2.Children.Add(panel4);

                panel.Children.Add(panel2);
                
                drugList.Items.Add(panel);
            }
        }



        /// <summary>
        /// Edit  drug information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;

            string[] _splited = bt.Name.Split(new Char[] { '_' });

            if (MessageBox.Show("Edit Selected Drug?", "TeleMedicine", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DataTable qry = parent.query("select * from drugs where drug_id=" + _splited[1]);

                foreach (DataRow rw in qry.Rows)
                {
                    edit_id = Int32.Parse(rw["drug_id"].ToString());
                    nameBox.Text = rw["drug_name"].ToString();
                    costBox.Text = rw["cost"].ToString();
                    desBox.Document.Blocks.Clear();
                    desBox.AppendText(rw["description"].ToString());
                    BitmapImage db_img = parent.getImg((byte[])rw["photo"]);
                    edit_img = ((byte[])rw["photo"]);
                    image1.Source = db_img;
                    saveButton.Content = "Update";
                }

            }
        }




        /// <summary>
        /// Delete a drug from the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;

            string [] _splited = bt.Name.Split(new Char[] { '_' });

            if(MessageBox.Show("Delete Selected Drug?", "TeleMedicine", MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.Yes)
            {
                parent.query("delete from drugs where drug_id="+_splited[1]);

                foreach(StackPanel sp in drugList.Items)
                {
                    if(sp.Name.Equals("sp_"+_splited[1]))
                    {
                        drugList.Items.Remove(sp);
                        break;
                    }
                }
                
            }

            

        }

       
        /// <summary>
        /// Upload drug image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "JPG Files|*.jpg|JPEG Files|*.jpeg | PNG Files|*.png|GIF Files|*.gif";
            if (dialog.ShowDialog() == true)
            {

                uploadButton.IsEnabled = false;
                string filename = (dialog.FileName);
                if (new System.IO.FileInfo(filename).Length <= 184320)
                {
                   
                    try
                    {
                        img = new System.Drawing.Bitmap(filename);
                    }
                    catch (Exception) { MessageBox.Show("Failed to read file", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error); }

                    if (img != null)
                    {
                            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                             edit_img= (byte[])converter.ConvertTo(img, typeof(byte[]));
                            image1.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(img.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                            MessageBox.Show("Image upload completed", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                       
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
        /// Add/update drug information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange tr = new TextRange(desBox.Document.ContentStart, desBox.Document.ContentEnd);



            if (!saveButton.Content.Equals("Update"))
            {

                if (!nameBox.Text.Trim().Equals("") && !costBox.Text.Trim().Equals("") && img != null)
                {
                    int id = parent.saveDrug(nameBox.Text.Trim(), costBox.Text.Trim(), tr.Text, img);

                    if (id > 0)
                    {
                        addToList(id);
                        MessageBox.Show("Drug Added Successfully", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                        img = null;
                        nameBox.Text = costBox.Text = "";
                        image1.Source = null;
                        desBox.Document.Blocks.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Incomplete Details", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {

                if (!nameBox.Text.Trim().Equals("") && !costBox.Text.Trim().Equals("") && edit_id > 0 && edit_img != null && parent.updateDrug(edit_id, nameBox.Text.Trim(), costBox.Text.Trim(), tr.Text, edit_img))
                {
                    updateList(edit_id);
                    MessageBox.Show("Drug updated Successfully", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                    img = null;
                    nameBox.Text = costBox.Text = "";
                    image1.Source = null;
                    desBox.Document.Blocks.Clear();
                    saveButton.Content = "Save";
                }

                else
                {
                    MessageBox.Show("Incomplete Details", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

            img = null;
            edit_img = null;

        }




        /// <summary>
        /// Add drug to list
        /// </summary>
        /// <param name="id"></param>
        public void addToList(int id)
        {
            DataTable qry = parent.query("select * from drugs where drug_id=" + id);

            foreach (DataRow rw in qry.Rows)
            {

                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Name = "sp_" + rw["drug_id"].ToString();

                Image Pix = new Image();
                Pix.Source = parent.getImg((byte[])rw["photo"]);
                Pix.Stretch = Stretch.UniformToFill;
                Pix.Width = 70;
                Pix.Height = 70;


                Label space = new Label();
                space.Width = 5;
                panel.Children.Add(space);

                panel.Children.Add(Pix);

                Label space2 = new Label();
                space2.Width = 20;
                panel.Children.Add(space2);

                StackPanel panel2 = new StackPanel();
                panel2.Orientation = Orientation.Vertical;

                StackPanel panel3 = new StackPanel();
                panel3.Orientation = Orientation.Horizontal;
                Label name = new Label();
                name.Content = rw["drug_name"].ToString();
                name.FontWeight = FontWeights.Bold;
                name.Width = 300;

                Label cost = new Label();
                cost.Content = rw["cost"].ToString();
                cost.FontWeight = FontWeights.Bold;

                panel3.Children.Add(name);
                panel3.Children.Add(cost);

                panel2.Children.Add(panel3);
                TextBlock description = new TextBlock();
                description.TextWrapping = TextWrapping.Wrap;
                description.Width = 500;
                description.Text = rw["description"].ToString();
                panel2.Children.Add(description);


                Label space3 = new Label();
                space3.Width = 5;
                panel2.Children.Add(space3);


                StackPanel panel4 = new StackPanel();
                panel4.Orientation = Orientation.Horizontal;


                Button button = new Button();
                button.Name = "edit";
                button.Name = "edit_" + rw["drug_id"].ToString();
                button.Content = "Edit";
                button.Click += EditButton_Click;
                button.Background = Brushes.Transparent;
                panel4.Children.Add(button);



                Label space4 = new Label();
                space4.Width = 5;
                panel4.Children.Add(space4);


                Button button2 = new Button();
                button2.Name = "del_" + rw["drug_id"].ToString();
                button2.Content = "Delete";
                button2.Click += DelButton_Click;
                button2.Background = Brushes.Transparent;
                panel4.Children.Add(button2);

                panel2.Children.Add(panel4);

                panel.Children.Add(panel2);

                drugList.Items.Add(panel);

                drugList.ScrollIntoView(panel);
            }
        }

        /// <summary>
        /// Update list of drugs displayed
        /// </summary>
        /// <param name="id"></param>
        public void updateList(int id)
        {
            drugList.Items.Clear();
            DataTable qry = parent.query("select * from drugs order by drug_name asc");
            listDrugs(qry);

            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.Name = "sp_" + id;

            drugList.ScrollIntoView(panel);
           

        }







    }
}
