using Lab_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public class BusinessLogicWithReport : BusinessLogic
    {
        public BusinessLogicWithReport(IDataSource dataSource) : base(dataSource)
        {
        }

        public string MakeReport(DateTime from_, DateTime to)
        {
            var result = GetAll()
                .Where(v => from_ < v.dateEnd && v.dateEnd < to)
                .GroupBy(v => v.status)
                .Select(v => (v.Key, v.Count()));

            return $"Группировка задач по статусам выполения по дате от {from_} до {to}:\n"
                + string.Join("\n", result);
        }
    }
}
