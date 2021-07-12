using ClientWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWpf.Pages
{
    /// <summary>
    /// Interaction logic for CanvasPage.xaml
    /// </summary>
    public partial class CanvasPage : Page
    {
        private const int Deadband = 10;

        private int _count = 0;

        private int _oldX;
        private int OldXWithDeadbandInc { get => _oldX + Deadband; }
        private int OldXWithDeadbandDec { get => _oldX - Deadband; }

        private int _oldY;
        private int OldYWithDeadbandInc { get => _oldY + Deadband; }
        private int OldYWithDeadbandDec { get => _oldY - Deadband; }

        private readonly DatabaseServiceClient _databaseServiceClient;

        private CanvasViewModel CanvasViewModel => DataContext as CanvasViewModel;
        public CanvasPage()
        {
            InitializeComponent();
            _databaseServiceClient = new DatabaseServiceClient();
            
        }

        private void MouseCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsStartTrigger.IsChecked.GetValueOrDefault())
            {
                return;
            }
            var relativePosition = e.GetPosition(this);
            var point = PointToScreen(relativePosition);
            if (OldXWithDeadbandInc < point.X || OldXWithDeadbandDec > point.X)
            {
                _oldX = (int)point.X;
                WritePositionInDatabase(point, "Move");
                EventIncrement();
            }

            if (OldYWithDeadbandInc < point.Y || OldYWithDeadbandDec > point.Y)
            {
                _oldY = (int)point.Y;
                WritePositionInDatabase(point, "Move");
                EventIncrement();
            }
        }

        private void WritePositionInDatabase(Point point, string eventName)
        {
            _databaseServiceClient.SetPosition(new Common.Dto.PositionDto
            {
                UserId = 1,
                CreatedAt = DateTime.Now,
                Event = eventName,
                Points = $"{point.X} x {point.Y}"
            });
        }

        private void EventIncrement()
        {
            _count++;            
            if (_count >= 50)
            {
                _count = 0;
                IsStartTrigger.IsChecked = false;
                var totslRows = _databaseServiceClient.GetTotalRows();
                ShowMessageBox("Результат", $"Ваш результат {totslRows}.\n Отправить на\n(SMS){CanvasViewModel?.User?.Phone}\n(Email){CanvasViewModel?.User?.Email}?");
            }
        }

        private void ShowMessageBox(string caption, string message)
        {            
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
            {                
            }            
            IsStartTrigger.IsChecked = true;
        }

        private void MouseCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsStartTrigger.IsChecked.GetValueOrDefault())
            {
                return;
            }
            var relativePosition = e.GetPosition(this);
            var point = PointToScreen(relativePosition);
            WritePositionInDatabase(point, e.ChangedButton.ToString());
            EventIncrement();
        }
    }
}
