using ClientWpf.Controls;
using ClientWpf.Pages;
using ClientWpf.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FatherViewModel _fatherViewModel;

        public MainWindow()
        {
            InitializeComponent();

            _fatherViewModel = new FatherViewModel();

            CommandBindings.Add(new CommandBinding(ClientCommands.ShowResultPage, ShowResultPage));
            CommandBindings.Add(new CommandBinding(ClientCommands.ShowLoginPage, ShowLoginPage));

            DataContext = _fatherViewModel;
        }

        private void Login(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShowResultPage(object sender, ExecutedRoutedEventArgs e) => SetContent(_fatherViewModel.TableCoordinatesPage);

        private void ShowLoginPage(object sender, ExecutedRoutedEventArgs e) => SetContent(_fatherViewModel.AuthenticationPage);

        private void SetContent(Page page) => _fatherViewModel.CurrentPage = page;
        
    }
}
