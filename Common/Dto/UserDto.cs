using System.Runtime.Serialization;

namespace Common.Dto
{
    /// <summary>
    /// Таблица пользователей.
    /// </summary>
    [DataContract]
    public class UserDto
    {
        /// <summary>
        /// Ид.
        /// </summary>
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Имя.
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Телефон.
        /// </summary>
        [DataMember]
        public string Phone { get; set; }
        /// <summary>
        /// Электронная почта.
        /// </summary>
        [DataMember]
        public string Email { get; set; }
    }
}
