using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientWpf.ViewModels
{
    public class AuthenticationViewModel : ViewModelBase
    {
        private bool _isLogin;
        public bool IsLogin { get => _isLogin; set => Set(ref _isLogin, value); }

        private Common.Dto.UserDto _user;
        public Common.Dto.UserDto User { get => _user; set => Set(ref _user, value); }

        private string _phone;
        public string Phone { get => _phone; set => Set(ref _phone, value); }

        private string _loginMessage;
        public string LoginMessage { get => _loginMessage; set => Set(ref _loginMessage, value); }

        public ICommand LoginCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }

        private readonly AuthenticationServiceClient _authenticationServiceClient;

        public AuthenticationViewModel()
        {
            _authenticationServiceClient = new AuthenticationServiceClient();

            LoginCommand = new RelayCommand(Login);
            LogoutCommand = new RelayCommand(Logout);
        }

        private void Logout()
        {
            _authenticationServiceClient.Logout(User.Id, User.Name);
            ToggleAuthentication(null);
        }

        private void Login()
        {
            try
            {
                var resonse = _authenticationServiceClient.Login(Phone);
                if (resonse is null)
                {
                    LoginMessage = "Пользователь не найден";
                    return;
                }
                ToggleAuthentication(resonse);
            }
            catch (Exception ex)
            {
                LoginMessage = ex.Message;
                return;
            }
        }

        private void ToggleAuthentication(Common.Dto.UserDto user)
        {
            IsLogin = IsLogin ? false : true;
            User = user;
            Phone = "";
        }
    }
}
