using Common.Interfaces;
using Common.Interfaces.Repositories;
using MySql.Data.MySqlClient;
using Server.Factories;
using Server.Repositories;
using SimpleInjector;
using System;
using System.Data;
using System.ServiceModel;

namespace Server
{
    class Program
    {
        static readonly Container container;

        static readonly string databaseName = "TenzoTest";
        static readonly string connectionString = "server=localhost;port=3306;database=" + databaseName + ";uid=root;pwd=example";

        static Program()
        {            
            container = new Container();
            container.Register(typeof(IDbConnection), () => new MySqlConnection(connectionString), Lifestyle.Singleton);
            container.Register<IDataRepository, MySqlDataRepository>(Lifestyle.Singleton);
            container.Register<IAuthenticationRepository, AuthenticationRepository>(Lifestyle.Singleton);
            container.Register<IWCFServiceFactory, WCFServiceFactory>();
            container.Verify();
        }

        static void Main(string[] args)
        {
            var factory = container.GetInstance<IWCFServiceFactory>();            
            
            try
            {
                factory.Listen();
                Console.WriteLine("СЕРВЕР запущен.");
                Console.WriteLine("Нажми <Enter> для завершения работы СЕРВЕРА.");
                Console.WriteLine();
                Console.ReadLine();
                factory.Stop();
                container.Dispose();

            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                factory.Stop();
                container.Dispose();
            }

            Console.WriteLine("Нажми <Enter> для выхода.");
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
