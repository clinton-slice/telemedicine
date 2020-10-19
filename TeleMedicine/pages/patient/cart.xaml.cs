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

namespace TeleMedicine.pages.patient
{
    /// <summary>
    /// Interaction logic for cart.xaml
    /// </summary>
    /// 

        public class CartItem
         {
             public int id { set; get; }
             public string cid { set; get; }
             public string drug { set; get; }
             public string cost { set; get; }
  
        }

    public partial class cart : Window
      {
        MainWindow parent;
        patient_frame subparent;
        pharmacy pharmacy;

      

        /// <summary>
        /// Display list of items in cart
        /// </summary>
        /// <param name="_parent"></param>
        /// <param name="_subparent"></param>
        /// <param name="_sub"></param>
        public cart(MainWindow _parent,patient_frame _subparent,pharmacy _sub)
        {
            parent = _parent;
            subparent = _subparent;
            pharmacy = _sub;
            InitializeComponent();

           DataTable qry= parent.query("select * from cart where login_user_id=" + _subparent.AccountId);
            int count = 0;
            foreach(DataRow rw in qry.Rows)
            {
                CartItem item = new CartItem();
                item.cost = rw["cost"].ToString();
                item.drug = rw["drug"].ToString();
                item.cid = rw["cart_id"].ToString();
                count++;
                item.id = count;
                listView.Items.Add(item);
            }
        }

        /// <summary>
        /// Remove item from cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            CartItem itm = b.CommandParameter as CartItem;
            parent.query("delete from cart where cart_id=" + itm.cid);
            listView.Items.Remove(itm);
            pharmacy.checkCart();

        }



        }
}
