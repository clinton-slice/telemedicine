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
using System.Data;

namespace TeleMedicine.pages.patient
{
    /// <summary>
    /// Interaction logic for result.xaml
    /// </summary>
    public partial class pharmacy : UserControl
    {
        MainWindow parent;
        patient_frame subparent;
        int cart_items = 0;


        /// <summary>
        /// Get list of all drugs
        /// </summary>
        /// <param name="root"></param>
        /// <param name="_sub"></param>
        public pharmacy(MainWindow root, patient_frame _sub)
        {
            parent = root;
            subparent = _sub;
            InitializeComponent();
        
           DataTable qry = parent.query("select * from drugs order by drug_name asc");
            listDrugs(qry);
            checkCart();
        }


        /// <summary>
        /// Check if user has previously added items to cart
        /// </summary>
        public void checkCart()
        {
            DataTable qry = parent.query("select * from cart where login_user_id=" + subparent.AccountId);
            if (qry.Rows.Count > 0)
            {
                cartAmount.Content = qry.Rows.Count + " item(s) in cart";
                cart_items = qry.Rows.Count;
            }
            else
            {
                cart_items = 0;
                cartAmount.Content = "Cart is empty!";
            }
        }


        /// <summary>
        /// Display list of all drugs on a listbox
        /// </summary>
        /// <param name="tab"></param>
        public void listDrugs(DataTable tab)
        {
            foreach (DataRow rw in tab.Rows)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;


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
                name.Width = 270;

                Label cost = new Label();
                cost.Content = rw["cost"].ToString();
                cost.FontWeight = FontWeights.Bold;
                cost.Width = 100;

                Button add = new Button();
                add.Name = "add_" + rw["drug_id"].ToString();
                add.Content = " Add to Cart ";
                add.Background = Brushes.White;
                add.Click += AddButton_Click;

                panel3.Children.Add(name);
                panel3.Children.Add(cost);
                panel3.Children.Add(add);

                panel2.Children.Add(panel3);
                TextBlock description = new TextBlock();
                description.TextWrapping = TextWrapping.Wrap;
                description.Width = 400;
                description.Text = rw["description"].ToString();
                panel2.Children.Add(description);


                panel.Children.Add(panel2);
                drugList.Items.Add(panel);
            }
        }


        /// <summary>
        /// Add selected drug to cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            string [] _split  = bt.Name.Split(new Char[] { '_' });
            DataTable qry = parent.query("select * from drugs where drug_id="+_split[1]);
            if(qry.Rows.Count>0)
            {
                DataRow rw = qry.Rows[0];
                parent.query("insert into cart (login_user_id,drug,cost) values("+subparent.AccountId+",'"+rw[1]+ "','" + rw[2] + "')");
                MessageBox.Show("Item added to cart!", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                checkCart();
            }
            else
            {
                MessageBox.Show("Item not available!", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        /// <summary>
        /// Return to main oage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            subparent.loadDashboard();
        }

        /// <summary>
        /// Check out cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (cart_items == 0)
            {
                MessageBox.Show("Cart is empty!", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (MessageBox.Show("Do you want to checkout " + cart_items + " item(s)?", "TeleMedicine", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    parent.query("delete from cart where login_user_id=" + subparent.AccountId);
                    MessageBox.Show("Items Checked out", "TeleMedicine", MessageBoxButton.OK, MessageBoxImage.Information);
                    checkCart();
                }
            }
        }

        /// <summary>
        /// Open cart list dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new cart(parent, subparent, this).ShowDialog();
        }
    }
}
