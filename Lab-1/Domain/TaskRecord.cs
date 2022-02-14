using System;

namespace  Lab_1

{
    public class TaskRecord : AbstractTaskRecord
    {
        public TaskRecord()
        {
        }

        public TaskRecord(string record) : base(record)
        {
        }

        public TaskRecord(string description, string status, string executor, DateTime dateEnd) : base(description, status, executor, dateEnd)
        {
        }

        public override string ToString()
        {
            return "Задача:\n" + base.ToString();
        }
    }
}
