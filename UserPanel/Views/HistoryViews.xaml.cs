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

namespace UserPanel.Views
{
    /// <summary>
    /// Interaction logic for HistoryViews.xaml
    /// </summary>
    public partial class HistoryViews : UserControl
    {
        public HistoryViews()
        {
            InitializeComponent();
        }
        public neise neise { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Listt.Items.Add(neise);
        }
    }
}
