using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    /// <summary>
    /// Интерфейс для реализации хранилища записей
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// Сохранение записи в хранилище.
        /// Если у записи id == 0, то значит выполняется добавление новой записи,
        /// для нее нужно сгенерировать id (порядковый номер). Иначе - обновление записи
        /// </summary>
        /// <param name="record">Добавляемая или обновляемая запись</param>
        /// <returns>Запись из хранилищая с id</returns>
        AbstractTaskRecord Save(AbstractTaskRecord record);
        /// <summary>
        /// Возвращает одну запись из хранилища по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns>Найденная запись или null, если записи с таким id нет</returns>
        AbstractTaskRecord Get(int id);
        /// <summary>
        /// Удаляет одну запись из хранилища по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns>true, если запись успешно удалена</returns>
        bool Delete(int id);
        /// <summary>
        /// Возвращает все записи из хранилища
        /// </summary>
        /// <returns>Все записи</returns>
        List<AbstractTaskRecord> GetAll();
    }
}
