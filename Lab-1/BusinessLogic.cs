using System;
using System.Collections.Generic;

namespace Lab1ConsoleApp
{
    public class BusinessLogic
    {
        List<string> allowStatuses = new List<string>()
        {
            "Новая",
            "В работе",
            "Решена",
            "Закрыта",
        };
        int minDesriptionLength = 20;

        IDataSource dataSource;

        public BusinessLogic(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }     

        public AbstractTaskRecord Save(AbstractTaskRecord record)
        {
            if (allowStatuses.Contains(record.status) == false)
            {
                throw new ArgumentException($"Статус должен быть один из следующих {string.Join(",", allowStatuses)}, но передан {record.status}");
            }

            if(record.description.Length <= minDesriptionLength)
            {
                throw new ArgumentException($"Длина описания должна быть больше {minDesriptionLength}, но описание имеет длину {record.description.Length}");
            }

            return dataSource.Save(record);
        }

        public AbstractTaskRecord Get(int id)
        {
            return dataSource.Get(id);
        }

        public bool Delete(int id)
        {
            return dataSource.Delete(id);
        }

        public List<AbstractTaskRecord> GetAll()
        {
            var records = dataSource.GetAll();
            records.Sort(RecordCompare);
            return records;
        }

        private int RecordCompare(AbstractTaskRecord a, AbstractTaskRecord b)
        {
            var statusCompare = a.status.CompareTo(b.status);
            if (statusCompare != 0)
                return statusCompare;
            return a.executor.CompareTo(b.executor);
        }
    }
}
