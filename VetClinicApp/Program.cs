using System;
using System.Text;

namespace VetClinicApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;

            while (running)
            {
                Console.WriteLine("=== Ветеринарна клініка ===");
                Console.WriteLine("1 - Довідка про програму");
                Console.WriteLine("2 - Таблиця вартості прийому");
                Console.WriteLine("3 - Завантаження лікарів за тиждень");
                Console.WriteLine("0 - Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAbout();
                        Pause();
                        break;
                    case "2":
                        ShowCostTable();
                        Pause();
                        break;
                    case "3":
                        ShowVisitsTable();
                        Pause();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Невірний пункт меню.");
                        Pause();
                        break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey(true);
            Console.Clear();
        }

        static void ShowAbout()
        {
            Console.WriteLine("Тема: Ветеринарна клініка");
            Console.WriteLine("Головна сутність: Прийом у лікаря");
            Console.WriteLine("Поля запису:");
            Console.WriteLine("1. id - номер прийому");
            Console.WriteLine("2. petName - кличка тварини");
            Console.WriteLine("3. species - вид тварини");
            Console.WriteLine("4. age - вік тварини");
            Console.WriteLine("5. owner - власник");
            Console.WriteLine("6. visitDate - дата прийому");
            Console.WriteLine("7. cost - вартість у грн");
        }

        static void ShowCostTable()
        {
            Console.WriteLine("Розрахунок вартості огляду залежно від ваги тварини:");
            Console.WriteLine();

            double startWeight = 1.5;
            double endWeight = 15.0;
            double step = 1.5;

            Console.WriteLine($"{"Вага (кг)",10} | {"Вартість (грн)",15}");
            Console.WriteLine("----------------------------");

            for (double w = startWeight; w <= endWeight + 1e-9; w += step)
            {
                double price = 250 + w * 35.5 + Math.Sqrt(w) * 10;
                Console.WriteLine($"{w,10:F2} | {price,15:F2}");
            }
        }

        static void ShowVisitsTable()
        {
            Console.Write("Введіть кількість лікарів: ");
            if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0)
            {
                Console.WriteLine("Помилка: введіть число більше 0.");
                return;
            }

            Console.Write("Введіть кількість днів: ");
            if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0)
            {
                Console.WriteLine("Помилка: введіть число більше 0.");
                return;
            }

            Console.WriteLine();

            int[] dayTotals = new int[cols];
            int totalAll = 0;

            Console.Write($"{"Лікар",-12}");
            for (int c = 1; c <= cols; c++)
            {
                Console.Write($"{"День " + c,8}");
            }
            Console.WriteLine($"{"Разом",9}");

            for (int r = 1; r <= rows; r++)
            {
                Console.Write($"{"Лікар " + r,-12}");
                int rowSum = 0;

                for (int c = 1; c <= cols; c++)
                {
                    int visits = (r * 2 + c) % 5 + 2;
                    Console.Write($"{visits,8}");

                    rowSum += visits;
                    dayTotals[c - 1] += visits;
                }

                Console.WriteLine($"{rowSum,9}");
                totalAll += rowSum;
            }

            Console.Write($"{"Загалом",-12}");
            for (int c = 0; c < cols; c++)
            {
                Console.Write($"{dayTotals[c],8}");
            }
            Console.WriteLine($"{totalAll,9}");
        }
    }
}