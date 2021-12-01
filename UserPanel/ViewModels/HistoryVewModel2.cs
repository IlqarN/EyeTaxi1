using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPanel.ViewModels
{
    public class HistoryVewModel2:BaseViewModel
    {
        public string From { get; set; }

        public string To { get; set; }

        public string date{ get; set; }

        public string Price { get; set; }
        public HistoryVewModel2(string from,string to,string date, string price)
        {
            From = from;
            To = to;
            this.date = date;
            Price = price;
        }
        public HistoryVewModel2()
        {

        }



    }
}
