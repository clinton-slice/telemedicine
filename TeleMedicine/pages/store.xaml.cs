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

namespace TeleMedicine.pages
{
    /// <summary>
    /// Interaction logic for store.xaml
    /// </summary>
    public partial class store : Window
    {
        login parent;
        /// <summary>
        /// Fetch list of drugs available in pharmacy
        /// </summary>
        /// <param name="root"></param>
        public store(login root)
        {
            parent = root;
            InitializeComponent();

            DataTable qry = parent.parent.query("select * from drugs order by drug_name asc");
            listDrugs(qry);
        }



        /// <summary>
        /// List out all drugs on a listbox
        /// </summary>
        /// <param name="tab"></param>
        public void listDrugs(DataTable tab)
        {
            foreach (DataRow rw in tab.Rows)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;


                Image Pix = new Image();
                Pix.Source = parent.parent.getImg((byte[])rw["photo"]);
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
                name.Width = 250;

                Label cost = new Label();
                cost.Content = rw["cost"].ToString();
                cost.FontWeight = FontWeights.Bold;
                cost.Width = 100;

          
                panel3.Children.Add(name);
                panel3.Children.Add(cost);
           
                panel2.Children.Add(panel3);
                TextBlock description = new TextBlock();
                description.TextWrapping = TextWrapping.Wrap;
                description.Width = 300;
                description.Text = rw["description"].ToString();
                panel2.Children.Add(description);


                panel.Children.Add(panel2);
                drugList.Items.Add(panel);
            }
        }

        private void drugList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Login to your account to purchase this drug","TeleMedicine",MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
}
