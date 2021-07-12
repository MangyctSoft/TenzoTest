using System.ServiceModel;

namespace Common.Interfaces.Services
{
    /// <summary>
    /// WCF контракт для общих операций.
    /// </summary>
    [ServiceContract]
    public interface IOperationService
    {
        /// <summary>
        /// Начать запись значений координат указателя мыши.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        [OperationContract]
        void Start(string userName);
        /// <summary>
        /// Остановить запись значений координат указателя мыши.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        [OperationContract]
        void Stop(string userName);
    }
}
