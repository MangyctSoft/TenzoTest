using ClientWpf.Pages;
using System.ComponentModel;
using System.Windows.Controls;

namespace ClientWpf.ViewModels
{
    public class FatherViewModel : ViewModelBase
    {
        public NavigatorViewModel NavigatorViewModel { get; private set; }        
        public AuthenticationViewModel AuthenticationViewModel { get; private set; }
        public CanvasViewModel CanvasViewModel { get; private set; }

        private Page _currentPage;
        public Page CurrentPage { get => _currentPage; set => Set(ref _currentPage, value); }

        public TableCoordinatesPage TableCoordinatesPage => new TableCoordinatesPage { DataContext =  new TableViewModel() };
        public AuthenticationPage AuthenticationPage { get; private set; }
        public CanvasPage CanvasPage { get; private set; }

        public FatherViewModel()
        {
            NavigatorViewModel = new NavigatorViewModel();            
            AuthenticationViewModel = new AuthenticationViewModel();
            CanvasViewModel = new CanvasViewModel();
            
            AuthenticationPage = new AuthenticationPage { DataContext = AuthenticationViewModel };
            CanvasPage = new CanvasPage { DataContext = CanvasViewModel };

            CurrentPage = TableCoordinatesPage;

            AuthenticationViewModel.PropertyChanged += AuthenticationViewModelOnPropertyChanged;
            NavigatorViewModel.PropertyChanged += NavigatorViewModelOnPropertyChanged;
        }

        private void NavigatorViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NavigatorViewModel.IsRunCapture))
            {
                CanvasViewModel.IsChecked = NavigatorViewModel.IsRunCapture;
                CurrentPage = NavigatorViewModel.IsRunCapture ? CanvasPage : (Page)AuthenticationPage;
            }
        }

        private void AuthenticationViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AuthenticationViewModel.User))
            {
                CanvasViewModel.User = AuthenticationViewModel.User;
                if (AuthenticationViewModel.User != null)
                {
                    NavigatorViewModel.UserName = AuthenticationViewModel.User.Name;                    
                    NavigatorViewModel.TitleButton = "Выход";
                    NavigatorViewModel.StartEnabled = true;
                    
                }
                else
                {
                    NavigatorViewModel.UserName = "";
                    NavigatorViewModel.TitleButton = "Вход";
                    NavigatorViewModel.StartEnabled = false;
                }
            }
        }
    }
}
