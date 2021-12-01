using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserPanel.Commands;
using UserPanel.Models;
using UserPanel.Services;
using UserPanel.Stores;

namespace UserPanel.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class RegisterViewModel : BaseViewModel
    {

        public List<User> Users { get; set; } = new List<User>(0);

        public ICommand NavigateLoginCommand { get; set; }

        public ICommand RegisterCommand { get; set; }

        public ICommand BankCardCommand { get; set; }


        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Gmail { get; set; }

        public string Adress { get; set; }

        public Card card { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string ConfirmPass { get; set; }

        public bool RegButtonIsEnanble { get; set; } = BankCardViewModel.RegButtonIsEnable;

        

        public RegisterViewModel(NavigationStore NV)
        {

            BankCardCommand = new UpdateViewCommand<BankCardViewModel>(NV, () => new BankCardViewModel(NV));
            NavigateLoginCommand = new UpdateViewCommand<LoginViewModel>(NV, () => new LoginViewModel(NV));
            RegisterCommand = new RelayCommand(Register);
            Users = JsonSaveService<List<User>>.Load("Users");
            if (Users == null)
                Users = new List<User>(0);
            
        }


        public void Register(Object obj)
        {

            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(ConfirmPass) && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname) && !string.IsNullOrWhiteSpace(Gmail) && !string.IsNullOrWhiteSpace(Phone) && !string.IsNullOrWhiteSpace(Adress))
            {
                var str = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                Regex regex = new Regex(str);
                var PhoneControl = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
                Regex regex1 = new Regex(PhoneControl);
                string Namecontrol = "[0-9]";
                Regex regex2 = new Regex(Namecontrol);
                Match match = regex.Match(Gmail);//GmailControl

                Match match1 = regex1.Match(Phone);//PhoneControl

                Match match2 = regex2.Match(Name);

                Match match3 = regex2.Match(Surname);
                if (match.Success) { goto a; }
                else MessageBox.Show("Please Enter true Gmail Address");
                a:
                if (match1.Success) { goto b; }
                else MessageBox.Show("Please Enter true PhoneNumber");
                b:
                if (match2.Success || match3.Success) { MessageBox.Show("Please Enter true Name And Surname"); }
                else goto c;
                c:
                if (match.Success && match1.Success && match2.Success != true && match3.Success != true)
                {
                    if (Password.Length >= 6)
                    {
                        if (Password == ConfirmPass)
                        {
                            var usr = Users.Find(u => u.Username == Username);

                            if (usr == null)
                            {
                                Users.Add(new User(Username, Password, Name, Surname, Gmail, Phone, Adress));
                                JsonSaveService<object>.Save(Users, "Users");
                                card = BankCardViewModel.usercard;
                                MessageBox.Show("You'r Good Choose Successfully Registration");
                            }
                            else MessageBox.Show("We have a user with this name");
                        }
                        else MessageBox.Show("Please Enter Olready true Password");
                    }
                    else MessageBox.Show("Please Enter Minumum Six Element");
                }
            }
            else MessageBox.Show("Fill in the blanks");
        }

    }
}
