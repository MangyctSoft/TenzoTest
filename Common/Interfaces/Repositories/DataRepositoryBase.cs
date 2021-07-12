using System;
using System.Data;

namespace Common.Interfaces.Repositories
{
    /// <summary>
    /// Базовый класс для работы с провайдеров бд.
    /// </summary>
    public abstract class DataRepositoryBase : IDisposable
    {
        protected readonly IDbConnection _dbConnection;
        protected ConnectionState State => _dbConnection.State;
        protected bool IsNotOpen => State != ConnectionState.Open;

        public DataRepositoryBase(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        protected virtual void Open()
        {
            if (IsNotOpen) 
            {
                _dbConnection.Open();
            }
        }

        protected virtual void Close()
        {
            if (State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }

        public void Dispose()
        {
            Close();
        }
    }
}
