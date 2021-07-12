using ClientWpf.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClientWpf.ViewModels
{
    public class NavigatorViewModel : ViewModelBase
    {
        private bool _startEnabled;
        public bool StartEnabled { get => _startEnabled; set => Set(ref _startEnabled, value); }

        private bool _stopEnabled;
        public bool StopEnabled { get => _stopEnabled; set => Set(ref _stopEnabled, value); }

        private bool _isRunCapture;
        public bool IsRunCapture { get => _isRunCapture; set => Set(ref _isRunCapture, value); }

        private string _titleButton;
        public string TitleButton { get => _titleButton; set => Set(ref _titleButton, value); }

        private string _userName;
        public string UserName { get => _userName; set => Set(ref _userName, value); }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        private readonly OperationServiceClient _operationServiceClient;

        public NavigatorViewModel()
        {
            _operationServiceClient = new OperationServiceClient();
            TitleButton = "Вход";

            StartCommand = new RelayCommand(StartRecord);
            StopCommand = new RelayCommand(StopRecord);

        }

        private void StopRecord()
        {
            _operationServiceClient.Stop(UserName);
            IsRunCapture = false;
            ToggleCaptureButton();
        }

        private void StartRecord()
        {
            _operationServiceClient.Start(UserName);
            IsRunCapture = true;
            ToggleCaptureButton();
        }

        private void ToggleCaptureButton()
        {
            StartEnabled = StartEnabled ? false : true;
            StopEnabled = StopEnabled ? false : true;
        }
    }
}
