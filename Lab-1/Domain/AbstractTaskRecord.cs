using System;

namespace Lab1ConsoleApp
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

        protected AbstractTaskRecord(string record)
        {
            filds = record.Split(';');
            this.description = filds[0];
            this.status = filds[1];
            this.executor = filds[2];
            this.dateEnd = DateTime.Parse(filds[3]);
            index = 3;
        }

        protected AbstractTaskRecord(string description, string status, string executor, DateTime dateEnd)
        {
            this.description = description;
            this.status = status;
            this.executor = executor;
            this.dateEnd = dateEnd;
        }

        public virtual AbstractTaskRecord Clone() => (AbstractTaskRecord)MemberwiseClone(); 

        public override string ToString()
        {
            return $"ИД:{id}\nОписание:{description}\nСтатус:{status}\nИсполнитель:{executor}\nДата завершения:{dateEnd}\n";
        }
    }

    public class MyTaskRecord : AbstractTaskRecord
    {
        public MyTaskRecord(string record) : base(record)
        {
        }

        public MyTaskRecord(string description, string status, string executor, DateTime dateEnd) : base(description, status, executor, dateEnd)
        {
        }
    }
}
