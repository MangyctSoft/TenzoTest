using Common.Dto;
using Common.Interfaces.Repositories;
using Dapper;
using System;
using System.Data;
using System.Linq;

namespace Server.Repositories
{
    public class AuthenticationRepository : DataRepositoryBase, IAuthenticationRepository
    {
        public AuthenticationRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            Open();
        }

        public UserDto Login(string phone)
        {
            if (IsNotOpen)
            {
                throw new Exception();
            }
            return _dbConnection.Query<UserDto>(sql: "CALL sp_getUser (@Phone)", new { Phone = phone }).FirstOrDefault();
        }

        public void Logout(int id, string userName)
        {
            return;
        }
    }
}
