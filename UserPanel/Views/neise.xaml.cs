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
using UserPanel.ViewModels;

namespace UserPanel.Views
{
    /// <summary>
    /// Interaction logic for neise.xaml
    /// </summary>
    public partial class neise : UserControl
    {
        public neise()
        {
            InitializeComponent();
        }

        public string From { get; set; }

        public string To { get; set; }

        public string date { get; set; }

        public string Price { get; set; }

        public neise(string from, string to, string date, string price)
        {
            InitializeComponent();
            From = from;
            To = to;
            this.date = date;
            Price = price;
            DataContext = this;
        }

    }
}
