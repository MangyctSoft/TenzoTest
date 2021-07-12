using System.Windows.Input;

namespace ClientWpf.Controls
{
    public static class ClientCommands
    {
        public static RoutedUICommand ShowResultPage = new RoutedUICommand("ShowResultPage", "ShowResultPage", typeof(ClientCommands));
        public static RoutedUICommand ShowLoginPage = new RoutedUICommand("ShowLoginPage", "ShowLoginPage", typeof(ClientCommands));
    }
}
