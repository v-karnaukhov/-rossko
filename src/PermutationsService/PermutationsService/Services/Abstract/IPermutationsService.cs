using System.Collections.Generic;
using System.Threading.Tasks;
using PermutationsService.Web.DataAccess.Entities;

namespace PermutationsService.Services.Abstract
{
    /// <summary>
    /// Описывает сервис получения перестановок содержимого элемента.
    /// </summary>
    public interface IPermutationsService
    {
        ///// <summary>
        ///// Получает все варианты перестановок содержимого элемента.
        ///// </summary>
        ///// <param name="element">
        /////     Целевой элемент.
        ///// </param>
        ///// <returns>
        ///// Возвращает инициализированный экземпляр класса <seealso cref="PermutationEntry"/>
        ///// или null в случае ошибки.
        ///// </returns>
        //Task<PermutationEntry> GetPermutations(string element);

        /// <summary>
        /// Получает все варианты перестановок содержимого элементов массива.
        /// </summary>
        /// <param name="elements">
        ///     Целевой массив элементов.
        /// </param>
        /// <returns>
        /// Возвращает перечисление инициализированных экземпляров класса <seealso cref="PermutationEntry"/>.
        /// </returns>
        Task<IEnumerable<PermutationEntry>> GetPermutations(string[] elements);

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
