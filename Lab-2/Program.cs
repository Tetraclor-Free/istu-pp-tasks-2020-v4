using Lab_1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_1.ConsoleInterface;

namespace Lab_2
{
    public class Program
    {
        public static void Main(string[] args)
        {

            File.Delete(@"../../../data.bin");

            IDataSource dataSource = null;
            try
            {
                dataSource = new FileDataSource(@"../../../data.bin",
                    new Dictionary<byte, Func<AbstractTaskRecord>>()
                    {
                        [1] = (() => new TaskRecord()),
                        [2] = (() => new TaskWithCustomerRecord()),
                        [3] = (() => new TaskWithMoneyRecord())
                    });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            dataSource.Save(new TaskWithCustomerRecord("Это описание точно длиннее 20 символов", "Закрыта", "Me", DateTime.Now, "Customer"));
            dataSource.Save(new TaskRecord("Починить крышу на бане в дачнм поселке", "Новая", "Влад", DateTime.Now.AddDays(10)));
            dataSource.Save(new TaskWithMoneyRecord("Сделать выданный срочный заказ", "Новая", "Влад", DateTime.Now.AddDays(5), 2000000));

            var businessLogic = new BusinessLogicWithReport(dataSource);

            var extMenuList = new List<MenuItem>()
            {
                new MenuItem("Сформировать отчет", "6", v => MakeReportConsole(v, businessLogic))
            };

            new ConsoleInterface().Start(businessLogic, extMenuList);
        }

        static void MakeReportConsole(ConsoleInterface consoleInterface, BusinessLogicWithReport businessLogic)
        {
            var from_ = consoleInterface.GetUserInput<DateTime>("Ввод даты от:", "ошибка при наборе даты");
            var to = consoleInterface.GetUserInput<DateTime>("Ввод даты по:", "ошибка при наборе даты");

            var report = businessLogic.MakeReport(from_, to);

            Console.WriteLine(report);
        }
    }


}
