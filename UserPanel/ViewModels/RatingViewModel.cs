using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserPanel.Commands;
using UserPanel.Stores;

namespace UserPanel.ViewModels
{
    class RatingViewModel:BaseViewModel
    {

        public ICommand CancelCommand { get; set; }

        public ICommand OkCommand { get; set; }

        public RatingViewModel(NavigationStore store)
        {
            CancelCommand = new UpdateViewCommand<NewMapViewModel>(store, () => new NewMapViewModel(store));
            OkCommand = new UpdateViewCommand<NewMapViewModel>(store, () => new NewMapViewModel(store));

        }
    }
}
