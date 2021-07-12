using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ClientWpf.ViewModels
{
    public class TableViewModel : ViewModelBase
    {
        private const int PageSize = 15;

        private int _pageNumber;
        public int PageNumber { get => _pageNumber; set => Set(ref _pageNumber, value); }

        private int _pageIndex;
        public int PageIndex { get => _pageIndex + 1; set => Set(ref _pageIndex, value / PageSize); }

        private int _pageEnd;
        public int PageEnd { get => _pageEnd; set => Set(ref _pageEnd, value + PageSize); }

        private int _totalRows;
        public int TotalRows { get => _totalRows; set => Set(ref _totalRows, value); }      

        private DateTime _startDate;
        public DateTime StartDate { get => _startDate; set => Set(ref _startDate, value); }

        private DateTime _endDate;
        public DateTime EndDate { get => _endDate; set => Set(ref _endDate, value); }

        private bool _checkedMove;
        public bool CheckedMove { get => _checkedMove; set => Set(ref _checkedMove, value); }

        private bool _checkedLeft;
        public bool CheckedLeft { get => _checkedLeft; set => Set(ref _checkedLeft, value); }

        private bool _checkedMiddle;
        public bool CheckedMiddle { get => _checkedMiddle; set => Set(ref _checkedMiddle, value); }

        private bool _checkedRight;
        public bool CheckedRight { get => _checkedRight; set => Set(ref _checkedRight, value); }


        private IEnumerable<Common.Dto.PositionDto> _positionTable;
        public IEnumerable<Common.Dto.PositionDto> PositionTable { get => _positionTable; set => Set(ref _positionTable, value); }


        public ICommand ToFirstPageCommand { get; private set; }
        public ICommand PreviousPageCommand { get; private set; }
        public ICommand NextPageCommand { get; private set; }
        public ICommand ToEndPageCommand { get; private set; }

        public ICommand ApplyDateFilterCommand { get; private set; }
        public ICommand ApplyEventFilterCommand { get; private set; }
        public ICommand RefreshDateFilterCommand { get; private set; }
        public ICommand RefreshEventFilterCommand { get; private set; }

        private Action GoZeroPage => () => GoPage(PageEnd = PageNumber = 0);

        public TableViewModel()
        {
            ToFirstPageCommand = new RelayCommand(GoZeroPage);
            PreviousPageCommand = new RelayCommand(() => GoPage(PageEnd = PageNumber -= PageSize));
            NextPageCommand = new RelayCommand(() => GoPage(PageEnd = PageNumber += PageSize), () => PageNumber < TotalRows);
            ToEndPageCommand = new RelayCommand(() => GoPage(PageEnd = PageNumber = TotalRows - PageSize), () => PageNumber != TotalRows);

            ApplyDateFilterCommand = new RelayCommand(GoZeroPage);
            ApplyEventFilterCommand = new RelayCommand(GoZeroPage);
            RefreshDateFilterCommand = new RelayCommand(() => { RefreshDateFilter(); GoZeroPage(); });
            RefreshEventFilterCommand = new RelayCommand(() => { RefreshEventFilter(); GoZeroPage(); });

            RefreshEventFilter();
            RefreshDateFilter();

            GoZeroPage();
        }

        private void RefreshEventFilter()
        {
            CheckedMove = true;
            CheckedLeft = true;
            CheckedMiddle = true;
            CheckedRight = true;            
        }

        private void RefreshDateFilter()
        {
            StartDate = DateTime.Now.AddDays(-1);
            EndDate = DateTime.Now;           
        }

        private void GoPage(int pageNaumber)
        {
            PageIndex = pageNaumber;
            ShowPosition(pageNaumber);
        }

        private void ShowPosition(int pageNaumber)
        {
            var databaseServiceClient = new DatabaseServiceClient();
            TotalRows = databaseServiceClient.GetTotalRows();
            PositionTable = databaseServiceClient.GetPositionWithPaging(pageNaumber, PageSize, StartDate, EndDate, GetEventsString());
        }

        private string[] GetEventsString()
        {
            List<string> result = new List<string>();
            if (CheckedMove) { result.Add("Move"); }
            if (CheckedLeft) { result.Add("Left"); }
            if (CheckedMiddle) { result.Add("Middle"); }
            if (CheckedRight) { result.Add("Right"); }
            return result.ToArray();
        }
    }
}
