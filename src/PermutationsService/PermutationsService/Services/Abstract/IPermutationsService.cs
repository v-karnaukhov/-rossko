using PermutationsService.Data.ServicesData;

namespace PermutationsService.Services.Abstract
{
    /// <summary>
    /// Описывает сервис получения перестановок содержимого элемента.
    /// </summary>
    public interface IPermutationsService
    {
        /// <summary>
        /// Получает все варианты перестановок содержимого элемента.
        /// </summary>
        /// <param name="element">
        /// Целевой элемент.
        /// </param>
        /// <returns>
        /// Возвращает инициализированный экземпляр класса <seealso cref="PermutationEntry"/>
        /// или null в случае ошибки.
        /// </returns>
        PermutationEntry GetPermutations(string element);

        /// <summary>
        /// Рассчитывает уникальный ключ для элемента.
        /// </summary>
        /// <param name="element">
        /// Элемент для которого нужно получить уникальный ключ.
        /// </param>
        /// <returns>
        /// Возвращает сгенерированный уникальный ключ элемента.
        /// </returns>
        string GetUniqueKeyByValue(string element);
    }
}
