using Common.Dto;
using System.ServiceModel;

namespace Common.Interfaces.Services
{
    /// <summary>
    /// WCF контракт для аутентификации пользователей.
    /// </summary>
    [ServiceContract]
    public interface IAuthenticationService
    {
        /// <summary>
        /// Метод для входа пользователя по номеру телефона.
        /// </summary>
        /// <param name="phone">Номер телефон.</param>
        /// <returns></returns>
        [OperationContract]
        UserDto Login(string phone);
        /// <summary>
        /// Выход пользователя.
        /// </summary>
        /// <param name="id">Ид пользователя.</param>
        /// <param name="userName">Имя пользователя.</param>
        [OperationContract]
        void Logout(int id, string userName);
    }
}
