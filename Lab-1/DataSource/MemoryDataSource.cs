using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1ConsoleApp
{
    /// <summary>
    /// Хранение записей в оперативной памяти
    /// ! Для предотвращения изменения записей извне,
    /// все методы, возвращающие записи должны возвращать
    /// их копии
    /// </summary>
    public class MemoryDataSource : IDataSource
    {
        private Dictionary<int, AbstractTaskRecord> records = new Dictionary<int, AbstractTaskRecord>();
        private int currentId = 1;

        public AbstractTaskRecord Save(AbstractTaskRecord record)
        {
            var cloned = record.Clone();

            if (cloned.id == 0) 
                cloned.id = currentId++;

            records[cloned.id] = cloned;

            return cloned.Clone();
        }

        public AbstractTaskRecord Get(int id)
        {
            return records.TryGetValue(id, out AbstractTaskRecord record) ? record.Clone() : null;
        }

        public bool Delete(int id)
        {
            return records.Remove(id); 
        }

        public List<AbstractTaskRecord> GetAll()
        {
            return records.Select(v => v.Value.Clone()).ToList();
        }
    }
}
