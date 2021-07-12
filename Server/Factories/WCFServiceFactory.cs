using Common.Interfaces;
using Common.Interfaces.Repositories;
using Common.Interfaces.Services;
using Server.Services;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Server.Factories
{
    public class WCFServiceFactory : IWCFServiceFactory
    {
		private Dictionary<string, ServiceHost> _serviceHosts = new Dictionary<string, ServiceHost>();

		private readonly string _ipAddress = "localhost";
		private readonly string _port = "8000";

		private readonly IDataRepository _dataRepository;
		private readonly IAuthenticationRepository _authenticationRepository;

		public WCFServiceFactory(IDataRepository dataRepository, IAuthenticationRepository authenticationRepository)
        {
			_dataRepository = dataRepository;
			_authenticationRepository = authenticationRepository;
		}
								
		public void Listen()
		{
			try
			{
				AddService<IDatabaseService, DatabaseService>(new DatabaseService(_dataRepository));
				AddService<IAuthenticationService, AuthenticationService>(new AuthenticationService(_authenticationRepository));
				AddService<IOperationService, OperationService>();
			}
			catch (Exception ex)
			{
				Console.WriteLine("WCF services was started with fail - {0}", ex);
				throw ex;
			}
		}

		public void Stop()
		{
			foreach (ServiceHost serviceHost in _serviceHosts.Values)
			{
				serviceHost.Close();
			}
			_serviceHosts.Clear();			
		}

		private void AddService<I, T>(object singletonInstance = null) where T: class, I 
			=> _serviceHosts.Add(typeof(T).Name, ListenService(typeof(I), typeof(T), singletonInstance));		

		private ServiceHost ListenService(Type implementedContract, Type service, object singletonInstance = null) 
			=> ListenService(implementedContract, service, _ipAddress, _port, singletonInstance);
		
		private static ServiceHost ListenService(Type implementedContract, Type service, string ipAddress, string port, object singletonInstance = null)
		{
			string address = string.Concat(new string[]
				{
					"http://",
					ipAddress,
					":",
					port,
					"/",
					service.Name
				});
			ServiceHost serviceHost = singletonInstance == null ? new ServiceHost(service, new Uri(address)) : new ServiceHost(singletonInstance, new Uri(address));

			serviceHost.AddServiceEndpoint(implementedContract, new BasicHttpBinding(), service.Name);

			ServiceMetadataBehavior smb = new ServiceMetadataBehavior { HttpGetEnabled = true };
			serviceHost.Description.Behaviors.Add(smb);

			serviceHost.Open();
			return serviceHost;
		}
	}
}
