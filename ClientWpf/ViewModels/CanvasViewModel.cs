using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWpf.ViewModels
{
    public class CanvasViewModel : ViewModelBase
    {
        private bool _isChecked;
        public bool IsChecked { get => _isChecked; set => Set(ref _isChecked, value); }

        private Common.Dto.UserDto _user;
        public Common.Dto.UserDto User { get => _user; set => Set(ref _user, value); }
    }
}
