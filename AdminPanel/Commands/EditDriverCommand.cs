using AdminPanel.Stores;
using AdminPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminPanel.Commands
{
    class EditDriverCommand<TViewModel> : ICommand where TViewModel : BaseViewModel
    {
        private readonly DriverEditViewModel _DriverEditViewModel;
        private readonly DriverStores _DriverStore;
        private readonly NavigationStore _NavigationStore;

        private readonly Func<TViewModel> _CreateViewModel;


        public EditDriverCommand(DriverEditViewModel driverEditViewModel, DriverStores driverStore, NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _DriverEditViewModel = driverEditViewModel;
            _DriverStore = driverStore;
            _NavigationStore = navigationStore;
            _CreateViewModel = createViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _DriverStore.Edit(_DriverEditViewModel.Driver);
            _NavigationStore.SelectedViewModel = _CreateViewModel();
        }
    }
}
