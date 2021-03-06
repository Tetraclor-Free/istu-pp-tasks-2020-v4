using System;

namespace Lab_1
{
    public class TaskWithCustomerRecord : AbstractTaskRecord
    {
        public string customer; // Заказчик

        public TaskWithCustomerRecord()
        {
        }

        public TaskWithCustomerRecord(string record) : base(record)
        {
            customer = GetNextField();
        }

        public TaskWithCustomerRecord(string description, string status, string executor, DateTime dateEnd, string customer) : base(description, status, executor, dateEnd)
        {
            this.customer = customer;
        }

        public override string ToString()
        {
            return $"Задача от заказчика:\n{base.ToString()}Заказчик:{customer}\n";
        }
    }
}
