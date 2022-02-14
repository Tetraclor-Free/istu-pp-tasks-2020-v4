using System;

namespace Lab_1
{
    public class TaskWithMoneyRecord : AbstractTaskRecord
    {
        public uint money;

        public TaskWithMoneyRecord()
        {
        }

        public TaskWithMoneyRecord(string record) : base(record)
        {
            money = uint.Parse(GetNextField());
        }

        public TaskWithMoneyRecord(string description, string status, string executor, DateTime dateEnd, uint money) : base(description, status, executor, dateEnd)
        {
            this.money = money;
        }

        public override string ToString()
        {
            return $"Задача с оплатой:\n{base.ToString()}Награда:{money}\n";
        }
    }
}
