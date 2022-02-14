using System;

namespace Lab_1
{
    /// <summary>
    /// Ваш класс основной записи 
    /// </summary>
    abstract public class AbstractTaskRecord
    {
        public int id; // Должен генерироваться автоматически после записи в хранилище.
                       // 0 обозначает новую, еще не сохраненную запись

        public string description; // Описание
        public string status; // Статус
        public string executor; // Исполнитель
        public DateTime dateEnd; // Дата завершения

        protected string[] filds;
        protected int index;

        protected AbstractTaskRecord()
        {
        }

        protected AbstractTaskRecord(string record)
        {
            filds = record.Split(';');
            this.description = GetNextField();
            this.status = GetNextField();
            this.executor = GetNextField();
            this.dateEnd = DateTime.Parse(GetNextField());
        }

        protected AbstractTaskRecord(string description, string status, string executor, DateTime dateEnd)
        {
            this.description = description;
            this.status = status;
            this.executor = executor;
            this.dateEnd = dateEnd;
        }

        protected string GetNextField()
        {
            return filds[index++];
        }

        public virtual AbstractTaskRecord Clone() => (AbstractTaskRecord)MemberwiseClone(); 

        public override string ToString()
        {
            return $"ИД:{id}; \nОписание:{description}; \nСтатус:{status}; \nИсполнитель:{executor}; \nДата завершения:{dateEnd}; \n";
        }
    }
}
