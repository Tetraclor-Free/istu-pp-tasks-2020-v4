using System;

namespace Lab1ConsoleApp
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
            return $"{base.ToString()}Заказчик:{customer}\n";
        }
    }
}
