namespace PermutationsService.Web.DataAccess.Entities
{
    /// <summary>
    /// Описывает сущность информации о конкретном элементе, для которого были расчитаны все варианты перестановок.
    /// </summary>
    public class PermutationEntry
    {
        /// <summary>
        /// Уникальный идентификатор строки в БД.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Непосредственно целевой элемент, для которого рассчитываются варианты перестановок.
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// Уникальный ключ, по которому определяется уникальность элемента.
        /// </summary>
        /// <remarks>
        /// Используется хеш-функция.
        /// </remarks>
        public string UniqueKey { get; set; }

        /// <summary>
        /// Все полученные варианты перестановок элемента.
        /// </summary>
        public string ResultString { get; set; }

        /// <summary>
        /// Количество полученных для элемента перестановок.
        /// </summary>
        public long ResultCount { get; set; }

        /// <summary>
        /// Количество затраченного на рассчеты время.
        /// </summary>
        public string SpendedTime { get; set; }
    }
}
