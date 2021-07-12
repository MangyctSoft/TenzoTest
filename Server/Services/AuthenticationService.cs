using Common.Dto;
using Common.Interfaces.Repositories;
using Common.Interfaces.Services;
using System;
using System.ServiceModel;

namespace Server.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository  _authenticationRepository;
        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public UserDto Login(string phone)
        {
            var response = _authenticationRepository.Login(phone);
            if (response is null)
            {
                Console.WriteLine($"Пользователь с номером {phone} не найден.");
            }
            else
            {
                Console.WriteLine($"Пользователь {response.Name} выполнил в ход.");
            }
            return response;
        }

        public void Logout(int id, string userName)
        {
            _authenticationRepository.Logout(id, userName);
            Console.WriteLine($"Пользователь {userName} вышел.");
        }
    }
}
