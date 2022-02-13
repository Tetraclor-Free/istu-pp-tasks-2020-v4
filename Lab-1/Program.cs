using System;

namespace Lab1ConsoleApp
{
    class Program
    {
        static BusinessLogic logic;

        static void Main(string[] args)
        {
            IDataSource dataSource = new MemoryDataSource();
            logic = new BusinessLogic(dataSource);

            logic.Save(new TaskWithCustomerRecord("Это описание точно длиннее 20 символов", "Закрыта", "Me", DateTime.Now, "Customer"));
            logic.Save(new TaskRecord("Починить крышу на бане в дачнм поселке", "Новая", "Влад", DateTime.Now.AddDays(10)));
            logic.Save(new TaskWithMoneyRecord("Сделать выданный срочный заказ", "Новая", "Влад", DateTime.Now.AddDays(5), 2000000));

            bool exit = false;
            while (!exit)
            {
                PrintMenu();
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        AddRecord();
                        break;
                    case "2":
                        PrintList();
                        break;
                    case "3":
                        ChangeRecord();
                        break;
                    case "4":
                        DeleteRecord();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }

        static void PrintMenu()
        {
            var menu = @"
1) Добавить запись
2) Просмотреть записи 
3) Изменить запись 
4) Удалить запись 
5) Выход. 
";
            Console.Write(menu);
        }

        static void AddRecord()
        {
            var ans = GetNumberAnswer("Выбрать подвид записи: \n1)Обычная задача\n2)Задача от заказчика\n3)Задача с наградой\n4)Выход", 4);
            if (ans == -1 || ans == 4) return;

            if (AddOrUpdateFromConsole(ans) == false)
                AddRecord();
        }

        static bool AddOrUpdateFromConsole(int recordType, int id = 0)
        {
            Console.Write("Введите через точку с запятой значения следующих полей: Описание;Статус;Исполнитель;Дата заверешения;");
            AbstractTaskRecord record = null;
            try
            {
                if (recordType == 1)
                {
                    Console.WriteLine();
                    record = new TaskRecord(Console.ReadLine());
                }
                if (recordType == 2)
                {
                    Console.WriteLine("Заказчик;");
                    record = new TaskWithCustomerRecord(Console.ReadLine());
                }
                if (recordType == 3)
                {
                    Console.WriteLine("Награда;");
                    record = new TaskWithMoneyRecord(Console.ReadLine());
                }
                record.id = id;
                Console.WriteLine($"Добавлена запись:\n{logic.Save(record)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка заполнения данных: " + ex.Message);
                return false;
            }

            return true;
        }

        static void PrintList()
        {
            var records = logic.GetAll();
            if(records.Count == 0)
            {
                Console.WriteLine("Записей нет");
                return;
            }
            var str = string.Join("\n", records);
            Console.WriteLine(str);
        }

        static void ChangeRecord()
        {
            var id = GetNumberAnswer("Изменение записи, введите идентификатор записи", int.MaxValue);
            if (id == -1) return;
            var record = logic.Get(id);
            if (record == null)
            {
                Console.WriteLine("По переданному идентификатору запись не найдена");
                return;
            }
            Console.WriteLine($"Изменение записи:{record}");
            if (record is TaskRecord) AddOrUpdateFromConsole(1, id);
            if (record is TaskWithCustomerRecord) AddOrUpdateFromConsole(2, id);
            if (record is TaskWithMoneyRecord) AddOrUpdateFromConsole(3, id);
        }

        static void DeleteRecord()
        {
            var id = GetNumberAnswer("Удаление записи, введите идентификатор записи", int.MaxValue);
            if (id == -1) return;
            var record = logic.Get(id);
            if (record == null)
            {
                Console.WriteLine("По переданному идентификатору запись не найдена");
                return;
            }
            var ans = GetNumberAnswer($"Вы точно желаете удалить эту запись? 1.Да  2.Нет\n{record}", 2);
            if (ans == -1) return;
            if (ans == 2) return;
            logic.Delete(id);
        }

        static int GetNumberAnswer(string message, int maxNum)
        {
            Console.WriteLine(message);

            var ans = Console.ReadLine();

            if(int.TryParse(ans, out int result) && maxNum >= result && result > 0)
            {
                return result;
            }
            else
            {
                Console.WriteLine("Неизвестная команда");
                return -1;
            }
        }
    }
}
