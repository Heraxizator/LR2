namespace ConsoleApp1
{
    public class Program
    {
        static IList<Exception> Exceptions = new List<Exception>();

        class SolutionNotFoundException : Exception
        {
            public SolutionNotFoundException() { }

            public SolutionNotFoundException(string message) : base(message) { }

            public SolutionNotFoundException(string message, Exception inner) : base(message, inner) { }
        }

        static List<string> GetStrings(string path)
        {
            List<string> list = new();

            try
            {
                StreamReader sr = new(path);

                string? line = sr.ReadLine();

                while (line != null)
                {
                    list.Add(line);
                    line = sr.ReadLine();
                }

                sr.Close();
            }

            catch (FileNotFoundException fnfe)
            {
                Exceptions.Add(fnfe);
            }

            catch (IOException oie)
            {
                Exceptions.Add(oie);
            }

            catch (ArgumentException ae)
            {
                Exceptions.Add(ae);
            }

            catch (Exception e)
            {
                Exceptions.Add(e);
            }

            return list;
        }

        static (int, int, int) GetValues(string row)
        {
            int[] values = row.Split(" ").Select(int.Parse).ToArray();

            return values.Length >= 3 ? (values[0], values[1], values[2]) : (-1, -1, -1);
        }

        static void GetSolutions((int, int, int) values)
        {
            int a = values.Item1;

            int b = values.Item2;

            int c = values.Item3;

            int d = (b * b) - (4 * a * c);

            try
            {
                if (d > 0)
                {
                    double x1 = (-b + Math.Sqrt(d)) / 2;
                    double x2 = (-b - Math.Sqrt(d)) / 2;
                    Console.WriteLine($"D > 0 => x1 = {x1}; x2 = {x2}");
                }

                else if (d == 0)
                {
                    double x = (-b + Math.Sqrt(d)) / 2;
                    Console.WriteLine($"D = 0 => x = {x}");
                    throw new SolutionNotFoundException("Не удалось найти второе решение квадратного уравнения");
                }

                else
                {
                    Console.WriteLine($"D < 0 => Нет корней");
                    throw new SolutionNotFoundException("Не удалось найти оба решения квадратного уравнения");
                }
            }

            catch (SolutionNotFoundException snfe)
            {
                Exceptions.Add(snfe);
            }

            catch (Exception e)
            {
                Exceptions.Add(e);
            }
        }

        static void Main(string[] args)
        {
            string path = "G:\\Дисциплины\\Тестирование ПО\\ЛР2\\file.txt";

            try
            {
                List<string> items = GetStrings(path);

                foreach (var item in items)
                {
                    Console.WriteLine("Входные значения: " + item);

                    (int, int, int) values = GetValues(item);

                    GetSolutions(values);

                    Console.WriteLine('\n');
                }
            }

            catch (Exception e)
            {
                Exceptions.Add(e);
            }

            finally
            {
                if (Exceptions.Any())
                {
                    Console.WriteLine("В программе произошли следующие ошибки: ");

                    foreach(var item in Exceptions)
                    {
                        Console.WriteLine(item.Message);
                    }
                }

                else
                {
                    Console.WriteLine("Программа выполнилась без ошибок");
                }
            }
        }
    }
}