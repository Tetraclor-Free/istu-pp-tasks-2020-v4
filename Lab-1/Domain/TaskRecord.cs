using System;

namespace Lab1ConsoleApp
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
    }
}
