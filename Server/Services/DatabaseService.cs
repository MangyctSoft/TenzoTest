using Common.Dto;
using Common.Interfaces.Repositories;
using Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;

namespace Server.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class DatabaseService : IDatabaseService
    {
        private readonly IDataRepository _dataRepository;

        public DatabaseService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IEnumerable<PositionDto> GetPositionWithPaging(int pageNumber, int pageSize, DateTime startDate, DateTime endDate, string[] events) => _dataRepository.GetPositionWithPaging(pageNumber, pageSize, startDate, endDate, events);

        public int GetTotalRows() => _dataRepository.GetTotalRows();
                
        public void SetPosition(PositionDto position) => _dataRepository.SetPosition(position);

    }
}
