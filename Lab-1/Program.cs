using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataSource dataSource = new MemoryDataSource();

            dataSource.Save(new TaskWithCustomerRecord("Это описание точно длиннее 20 символов", "Закрыта", "Me", DateTime.Now, "Customer"));
            dataSource.Save(new TaskRecord("Починить крышу на бане в дачнм поселке", "Новая", "Влад", DateTime.Now.AddDays(10)));
            dataSource.Save(new TaskWithMoneyRecord("Сделать выданный срочный заказ", "Новая", "Влад", DateTime.Now.AddDays(5), 2000000));

            new ConsoleInterface().Start(new BusinessLogic(dataSource));
        }
    }

    public class ConsoleInterface
    {
        public BusinessLogic logic;
        public bool exit = false;

        public class MenuItem
        {
            public string Message;
            public string Id;
            public Action<ConsoleInterface> Action;

            public MenuItem(string message, string id, Action<ConsoleInterface> action)
            {
                Message = message;
                Id = id;
                Action = action;
            }
        }

        public void Start(BusinessLogic businessLogic, List<MenuItem> extMenuItems = null)
        {
            logic = businessLogic;
            extMenuItems = extMenuItems ?? new List<MenuItem>();

            var defaultMenuItems = new List<MenuItem>
            {
                new MenuItem("Добавить запись", "1", v => AddRecord()),
                new MenuItem("Просмотреть записи", "2", v => PrintList()),
                new MenuItem("Изменить запись", "3", v => ChangeRecord()),
                new MenuItem("Удалить запись", "4", v => DeleteRecord()),
                new MenuItem("Выход", "5", v => v.exit = true)
            };

            var allMenuItems = defaultMenuItems.Union(extMenuItems).ToList();

            var menuActions = new Dictionary<string, MenuItem>();

            foreach (var menuItem in allMenuItems)
            {
                menuActions[menuItem.Id] = menuItem;
            }

            exit = false;
            while (!exit)
            {
                PrintMenu(allMenuItems);
                string command = Console.ReadLine();

                if (menuActions.ContainsKey(command))
                {
                    menuActions[command].Action(this);
                }
                else
                {
                    Console.WriteLine("Неизвестная команда");
                }
            }
        }

        void PrintMenu(List<MenuItem> menuItems)
        {
            var menu = "";
            foreach(var menuItem in menuItems)
            {
                menu += $"{menuItem.Id}) {menuItem.Message}\n";
            }
            Console.Write(menu);
        }

        void AddRecord()
        {
            var ans = GetNumberAnswer("Выбрать подвид записи: \n1)Обычная задача\n2)Задача от заказчика\n3)Задача с наградой\n4)Выход", 4);
            if (ans == -1 || ans == 4) return;

            if (AddOrUpdateFromConsole(ans) == false)
                AddRecord();
        }

        bool AddOrUpdateFromConsole(int recordType, int id = 0)
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

        void PrintList()
        {
            var records = logic.GetAll();
            if (records.Count == 0)
            {
                Console.WriteLine("Записей нет");
                return;
            }
            var str = string.Join("\n", records);
            Console.WriteLine(str);
        }

        void ChangeRecord()
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

        void DeleteRecord()
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

        public int GetNumberAnswer(string message, int maxNum)
        {
            return GetUserInput<int>(message, "Неверный ввод целого положительног очисла", v => maxNum >= v && v > 0);
        }

        public bool ReadInput<T>(out T result)
        {
            try
            {
                result = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                return true;
            }
            catch (Exception ex)
            {
                result = default;
                return false;
            }
        }

        public T GetUserInput<T>(string message, string messageIfError, Func<T, bool> check = null, bool defultIfError = true)
        {
            check ??= (v => true);

            while (true)
            {
                Console.WriteLine(message);

                if (ReadInput(out T value) && check(value))
                    return value;

                Console.WriteLine(messageIfError);
                if (defultIfError)
                    return default;
            }
        }
    }
}
