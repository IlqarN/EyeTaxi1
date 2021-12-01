using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPanel.Models
{
    public class Card
    {
        public string Pin { get; set; }

        public string Pan { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        public string Cvc { get; set; }
   
        public Card(string pin, string pan, string Year,string Month,string cvc)
        {
            Pin = pin;
            Pan = pan;
            Cvc = cvc;
            this.Year = Year;
            this.Month = Month;
        }

    }
}
