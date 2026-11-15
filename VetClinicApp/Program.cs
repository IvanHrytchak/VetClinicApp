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
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowAbout(); Pause(); break;
                    case "2": RunCostTabulation(); Pause(); break;
                    case "3": ShowVisitsTable(); Pause(); break;
                    case "0": running = false; break;
                    default:
                        Console.WriteLine("Невірний пункт меню.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("=== Ветеринарна клініка ===");
            Console.WriteLine("1 - Довідка про програму");
            Console.WriteLine("2 - Таблиця вартості прийому");
            Console.WriteLine("3 - Завантаження лікарів за тиждень");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey(true);
            Console.Clear();
        }

        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"Помилка: введіть ціле число від {min} до {max}.");
            }
        }

        static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value) && value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"Помилка: введіть число від {min} до {max}.");
            }
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

        static double CalculateCost(double weight)
        {
            return 250 + weight * 35.5 + Math.Sqrt(weight) * 10;
        }

        static void RunCostTabulation()
        {
            double start = ReadDouble("Введіть початкову вагу тварини (0,5 - 50 кг): ", 0.5, 50.0);
            double end = ReadDouble($"Введіть кінцеву вагу ({start} - 100 кг): ", start, 100.0);
            double step = ReadDouble("Введіть крок зміни ваги (0,1 - 20 кг): ", 0.1, 20.0);

            Console.WriteLine();
            PrintCostTable(start, end, step);
        }

        static void PrintCostTable(double startWeight, double endWeight, double step)
        {
            Console.WriteLine($"{"Вага (кг)",10} | {"Вартість (грн)",15}");
            Console.WriteLine("----------------------------");

            for (double w = startWeight; w <= endWeight + 1e-9; w += step)
            {
                double price = CalculateCost(w);
                Console.WriteLine($"{w,10:F2} | {price,15:F2}");
            }
        }

        static void ShowVisitsTable()
        {
            int rows = ReadInt("Введіть кількість лікарів (1-20): ", 1, 20);
            int cols = ReadInt("Введіть кількість днів (1-7): ", 1, 7);

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