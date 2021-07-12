namespace Common.Interfaces
{
    /// <summary>
    /// Интерфейс для запуска / останова WCF служб. 
    /// </summary>
    public interface IWCFServiceFactory
    {
        /// <summary>
        /// Запуск WCF служб.
        /// </summary>
        void Listen();
        /// <summary>
        /// Останов WCF служб.
        /// </summary>
        void Stop();
    }
}
