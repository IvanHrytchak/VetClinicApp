using System;
using System.Globalization;
using System.Text;

namespace VetClinicApp
{
    internal class Program
    {
        const int Capacity = 100;

        static int[] cardNumbers = new int[Capacity];
        static string[] petNames = new string[Capacity];
        static int[] speciesCodes = new int[Capacity];
        static int[] ages = new int[Capacity];
        static string[] ownerLastNames = new string[Capacity];
        static DateTime[] visitDates = new DateTime[Capacity];
        static string[] diagnoses = new string[Capacity];
        static double[] costs = new double[Capacity];

        static int count = 0;
        static int nextId = 1;

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
                    case "1":
                        ShowAllVisits();
                        Pause();
                        break;
                    case "3":
                        ShowAbout();
                        Pause();
                        break;
                    case "4":
                        GetTabulationParameters(out double start, out double end, out double step);
                        Console.WriteLine();
                        PrintCostTable(start, end, step);
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

        static void ShowMenu()
        {
            Console.WriteLine("=== Ветеринарна клініка ===");
            Console.WriteLine("1 - Показати всі записи");
            Console.WriteLine("3 - Довідка про програму та сутність");
            Console.WriteLine("4 - Таблиця вартості прийому (табулювання)");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey(true);
            Console.Clear();
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

        static void ShowAllVisits()
        {
            if (count == 0)
            {
                Console.WriteLine("Записів поки немає.");
                return;
            }

            Console.WriteLine($"{"№",4} | {"Кличка",-12} | {"Код виду",8} | {"Вік",4} | {"Власник",-14} | {"Дата",10} | {"Діагноз",-18} | {"Вартість",10}");
            Console.WriteLine(new string('-', 98));

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{cardNumbers[i],4} | {petNames[i],-12} | {speciesCodes[i],8} | {ages[i],4} | {ownerLastNames[i],-14} | {visitDates[i]:dd.MM.yyyy} | {diagnoses[i],-18} | {costs[i],10:F2}");
            }

            Console.WriteLine($"\nЗаписів: {count}.");
        }

        static void ShowAbout()
        {
            Console.WriteLine("Тема: Ветеринарна клініка (Варіант 6)");
            Console.WriteLine("Сутності: Користувач · Прийом · Вид (довідник)");
            Console.WriteLine("Поля сутності «Прийом»: номер картки, кличка, код виду, вік, власник, дата, діагноз, вартість.");
        }

        static void GetTabulationParameters(out double start, out double end, out double step)
        {
            start = ReadDouble("Введіть початкову вагу тварини (0,5 - 50 кг): ", 0.5, 50.0);
            end = ReadDouble($"Введіть кінцеву вагу ({start} - 100 кг): ", start, 100.0);
            step = ReadDouble("Введіть крок зміни ваги (0,1 - 20 кг): ", 0.1, 20.0);
        }

        static double CalculateCost(double weight)
        {
            return 250 + weight * 35.5 + Math.Sqrt(weight) * 10;
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
    }
}