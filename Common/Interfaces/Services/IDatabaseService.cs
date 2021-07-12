using Common.Dto;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Interfaces.Services
{
    /// <summary>
    /// WCF контракт для работы с таблицей "позиции мыши".
    /// </summary>
    [ServiceContract]
    public interface IDatabaseService
    {
        /// <summary>
        /// Записать данные курсора мыши.
        /// </summary>
        /// <param name="position">Сущность с данными курсора мыши.</param>
        [OperationContract]
        void SetPosition(PositionDto position);
        /// <summary>
        /// Получить данные курсора мыши постронично с фильтром по дате и событию.
        /// </summary>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="pageSize">Чисто строк.</param>
        /// <param name="startDate">Начальная дата.</param>
        /// <param name="endDate">Конечная дата.</param>
        /// <param name="events">Массив наименований событий.</param>
        /// <returns></returns>
        [OperationContract]        
        IEnumerable<PositionDto> GetPositionWithPaging(int pageNumber, int pageSize, DateTime startDate, DateTime endDate, string[] events);
        /// <summary>
        /// Получить общее количество строк в таблице с позициями мыши.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetTotalRows();
    }
}
