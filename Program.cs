using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR15
{
    internal class Program
    {
        public struct Astrology
        {
            public string zodiac;
            public string planet;
            public string stone;
            public DateTime period_from;
            public DateTime period_to;

            public Astrology(string zodiac)
            {
                this.zodiac = zodiac;
                planet = null;
                stone = null;
                period_from = new DateTime();
                period_to = new DateTime();
            }

            /// <summary>
            /// Добавить информацию об астрологии
            /// </summary>
            public bool AddInfo()
            {
                bool success = true;
                try
                {
                    Console.Write("\tпланета -> ");
                    planet = Console.ReadLine();

                    Console.Write("\tкамень -> ");
                    stone = Console.ReadLine();

                    Console.Write("\tпериод с (например 02.10) -> ");
                    period_from = GetPeriod(Console.ReadLine());

                    Console.Write("\tпериод до (например 15.11) -> ");
                    period_to = GetPeriod(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    success = false;
                    ShowError(ex.Message);
                }

                return success;
            }

            /// <summary>
            /// Показать информацию об астрологии
            /// </summary>
            public void ShowInfo()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tпланета -> " + planet);
                Console.WriteLine("\tкамень -> " + stone);
                Console.WriteLine("\tпериод с -> " + GetTimeFormat(period_from.Day) + "." + GetTimeFormat(period_from.Month));
                Console.WriteLine("\tпериод до -> " + GetTimeFormat(period_to.Day) + "." + GetTimeFormat(period_to.Month));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// Показать линию
        /// </summary>
        static void ShowLine()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("----------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Показать успешное сообщение
        /// </summary>
        static void ShowSuccess(string message)
        {
            ShowLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Показать сообщение с ошибкой
        /// </summary>
        static void ShowError(string message)
        {
            ShowLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Получить специальный формат времени
        /// </summary>
        static string GetTimeFormat(int time)
        {
            return (time < 9) ? "0" + time : Convert.ToString(time);
        }

        /// <summary>
        /// Получить период
        /// </summary>
        static DateTime GetPeriod(string text)
        {
            string[] date = text.Split('.');

            if (date.Length != 2)
                throw new Exception("Неверный формат периода!");

            int day = Convert.ToInt32(date[0]);
            int month = Convert.ToInt32(date[1]);
            int year = DateTime.Now.Year;

            return new DateTime(year, month, day);
        }

        static void Main(string[] args)
        {
            Console.Title = "Практическая работа №15";

            // Массив (список)
            List<Astrology> astrologyes = new List<Astrology> { };

            while (true)
            {
                ShowLine();
                try
                {
                    Console.WriteLine("1. Добавить знаки зодиака");
                    Console.WriteLine("2. Поиск знака зодиака");
                    Console.WriteLine("3. Вывести всё");
                    Console.WriteLine("4. Удалить всё");
                    Console.WriteLine("5. Выход");
                    Console.Write("-> ");

                    int menu = Convert.ToInt32(Console.ReadLine());
                    switch (menu)
                    {
                        // Добавить знаки зодиака
                        case 1:
                            ShowLine();
                            Console.WriteLine("Введите \"end\" для завершения");
                            ShowLine();
                            while (true)
                            {
                                Console.Write("Введите знак зодиака: ");
                                string zodiac = Console.ReadLine();

                                if (zodiac == "end")
                                    break;

                                // Создание астрологии
                                Astrology item = new Astrology(zodiac);

                                // Добавление данных
                                if (item.AddInfo())
                                {
                                    astrologyes.Add(item);
                                    ShowSuccess("\tЗодиак успешно добавлен!");
                                }
                                ShowLine();
                            }
                            break;
                        // Поиск знака зодиака
                        case 2:
                            ShowLine();
                            Console.Write("Поиск знака зодиака: ");
                            string search = Console.ReadLine();

                            bool success = false;
                            foreach (var astrology in astrologyes)
                            {
                                if (search == astrology.zodiac)
                                {
                                    astrology.ShowInfo();
                                    success = true;
                                    break;
                                }
                            }
                            if (success == false)
                                ShowError("Знак зодиака не найден!");
                            break;
                        // Вывести всё
                        case 3:
                            if (astrologyes.Count <= 0)
                                ShowError("Нет знаков зодиака!");
                            foreach (var astrology in astrologyes)
                            {
                                ShowLine();
                                Console.WriteLine("Зодиак " + astrology.zodiac + ":");
                                astrology.ShowInfo();
                            }
                            break;
                        // Удалить всё
                        case 4:
                            if (astrologyes.Count > 0)
                            {
                                astrologyes.Clear();
                                ShowSuccess("Все знаки зодиака успешно удалены!");
                            }
                            else
                                ShowError("Нет знаков зодиака!");
                            break;
                        // Выход
                        case 5:
                            Environment.Exit(0);
                            break;
                        default:
                            ShowError("Вы ввели недопустимое значение!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }

                ShowLine();
                Console.Write("Нажмите любую клавишу для возврата в меню... ");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}

