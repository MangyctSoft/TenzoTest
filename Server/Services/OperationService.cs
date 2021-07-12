using Common.Interfaces.Services;
using System;
using System.ServiceModel;

namespace Server.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class OperationService : IOperationService
    {
        public void Start(string userName)
        {
            Console.WriteLine($"Пользователь {userName} начал запись.");
        }

        public void Stop(string userName)
        {
            Console.WriteLine($"Пользователь {userName} завершил запись.");
        }
    }
}
