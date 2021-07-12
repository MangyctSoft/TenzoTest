using System;
using System.Runtime.Serialization;

namespace Common.Dto
{
    /// <summary>
    /// Таблица позиций мыши.
    /// </summary>
    [DataContract]
    public class PositionDto
    {
        /// <summary>
        /// Ид пользователя.
        /// </summary>
        [DataMember]
        public int UserId { get; set; }
        /// <summary>
        /// Дата/время создания записи.
        /// </summary>
        [DataMember]        
        public DateTime CreatedAt { get; set; }        
        /// <summary>
        /// Наименование события.
        /// </summary>
        [DataMember]
        public string Event { get; set; }
        /// <summary>
        /// Позиция относительно экрана.
        /// </summary>
        [DataMember]
        public string Points { get; set; }
    }
}
