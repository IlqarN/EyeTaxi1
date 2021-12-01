using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserPanel.Commands;
using UserPanel.Models;
using UserPanel.Stores;

namespace UserPanel.ViewModels
{
    public class BankCardViewModel:BaseViewModel
    {
        public ICommand Back { get; set; }

        public ICommand Next { get; set; }

        public RelayCommand Nextfunc { get; set; }

        public string Pan { get; set; }

        public string Pin { get; set; }

        public string Cvc { get; set; }

        public string Year { get; set; }

        public string Month { get; set; }


        public static Card usercard { get; set; }

        public static bool RegButtonIsEnable { get; set; } = true;


        public BankCardViewModel(NavigationStore navigation)
        {
            
            Back = new UpdateViewCommand<RegisterViewModel>(navigation, () => new RegisterViewModel(navigation));
            Nextfunc = new RelayCommand(NextFunc);
            Next = new UpdateViewCommand<RegisterViewModel>(navigation, () => new RegisterViewModel(navigation));
        }

        public void NextFunc(Object obj)
        {
            //if(string.IsNullOrEmpty(Pan)&&string.IsNullOrEmpty(Pin)&& string.IsNullOrEmpty(Cvc) && string.IsNullOrEmpty(Month.ToString()) && string.IsNullOrEmpty(Year.ToString()))
            //{
                int mont = Int32.Parse(Month);
                if(Pin.Length!=4 || Pan.Length!=16 || Cvc.Length!=3 || Year!= "2021")
                {
                    if(mont <0 || mont > 12)
                    {
                        System.Windows.Forms.MessageBox.Show("Please Enter True Information");
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Added Bank Card successfully");
                    usercard = new Card(Pin,Pan,Year,Month,Cvc);
                    RegButtonIsEnable = true;
                    Next.Execute(obj);
                }
            //}
            //else System.Windows.Forms.MessageBox.Show("Bosluqu doldurun");
        }

    }
}
