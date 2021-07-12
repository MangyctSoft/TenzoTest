using Common.Dto;
using Common.Interfaces.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Server.Repositories
{
    public class MySqlDataRepository : DataRepositoryBase, IDataRepository
    {       
        public MySqlDataRepository(IDbConnection dbConnection): base(dbConnection)
        {
            Open();
        }

        protected override void Open()
        {
            Console.Write($"Подключение к базе данных [{_dbConnection.Database}]...");
            base.Open();
            Console.WriteLine("Успех.");
        }

        protected override void Close()
        {
            Console.Write($"Отключение от базы данных [{_dbConnection.Database}]...");
            base.Close();
            Console.WriteLine("Успех.");
        }

        public IEnumerable<PositionDto> GetPositionWithPaging(int pageNumber, int pageSize, DateTime startDate, DateTime endDate, string[] events)
        {
            if (IsNotOpen)
            {
                throw new Exception();           
            }
            return _dbConnection.Query<PositionDto>(
                sql:"CALL sp_getPositonsWithPaging (@PageNumber , @PageSize, @StartDate, @EndDate, @Events)",
                new { PageNumber = pageNumber, PageSize = pageSize, StartDate = startDate, EndDate = endDate, Events = string.Join("|", events) });
        }

        public void SetPosition(PositionDto position)
        {
            if (IsNotOpen)
            {
                throw new Exception();
            }
            _dbConnection.Execute(
                sql:"CALL sp_setPositon (@UserId , @CreatedAt, @Event, @Points)", 
                new { UserId = position.UserId, CreatedAt = position.CreatedAt, Event = position.Event, Points = position.Points });
        }

        public int GetTotalRows()
        {
            if (IsNotOpen)
            {
                throw new Exception();
            }
            return _dbConnection.Query<int>(sql: "CALL sp_getTotalRows").First();
        }
    }
}
