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

namespace TeleMedicine.pages
{
    /// <summary>
    /// Interaction logic for faq.xaml
    /// </summary>
    public partial class faq : Window
    {
        login parent;
        public faq(login root)
        {
            parent = root;
            InitializeComponent();
        }
    }
}
