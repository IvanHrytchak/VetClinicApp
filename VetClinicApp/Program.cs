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

            // Довідник "Вид тварини"
            int[] speciesCodes = { 1, 2, 3, 4 };
            string[] speciesNames = { "Кіт", "Собака", "Папуга", "Хом'як" };

            // Масиви даних сутності "Прийом"
            int[] cardNumbers = { 101, 102, 103, 104, 105, 106 };
            string[] petNames = { "Мурчик", "Рекс", "Барсик", "Кеша", "Жук", "Сніжок" };
            int[] visitSpeciesCodes = { 1, 2, 1, 3, 2, 1 };
            int[] ages = { 3, 5, 2, 1, 8, 4 };
            string[] ownerLastNames = { "Шевченко", "Коваленко", "Бойко", "Ткаченко", "Мельник", "Кравчук" };
            DateTime[] visitDates =
            {
                new DateTime(2026, 11, 10),
                new DateTime(2026, 11, 11),
                new DateTime(2026, 11, 12),
                new DateTime(2026, 11, 12),
                new DateTime(2026, 11, 14),
                new DateTime(2026, 11, 15)
            };
            string[] diagnoses = { "Вакцинація", "Отит", "Перелом лапи", "Огляд", "Чистка зубів", "Алергія" };
            double[] costs = { 450.0, 800.0, 1500.0, 300.0, 1100.0, 650.0 };

            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAbout();
                        Pause();
                        break;
                    case "2":
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
            Console.WriteLine("1 - Довідка про програму та сутності");
            Console.WriteLine("2 - Таблиця вартості прийому");
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

        static void ShowAbout()
        {
            Console.WriteLine("Тема: Ветеринарна клініка");
            Console.WriteLine("Сутності: Користувач · Прийом · Вид (довідник)");
            Console.WriteLine("Поля сутності «Прийом»:");
            Console.WriteLine("1. int номер картки");
            Console.WriteLine("2. string кличка тварини");
            Console.WriteLine("3. int код — вид, з довідника");
            Console.WriteLine("4. int вік");
            Console.WriteLine("5. string прізвище власника");
            Console.WriteLine("6. DateTime дата прийому");
            Console.WriteLine("7. string діагноз");
            Console.WriteLine("8. double вартість");
            Console.WriteLine("Підсумкова величина: кількість прийомів за видами тварин");
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