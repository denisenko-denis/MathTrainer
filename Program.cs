using System;
using System.Diagnostics;

namespace MathTrainer
{
    class Program
    {
        const int Duration = 60;
        private static string name = "";
        static void Main(string[] args)
        {
            Console.WriteLine("Привет!");
            Console.WriteLine("Меня зовут Вася, я твой тренер по математике.");
            Console.Write("Напиши, как тебя зовут, и нажми Enter: ");

            do
            {
                name = Console.ReadLine();
            } while (String.IsNullOrEmpty(name));

            name = name.Capitalize();

            Console.WriteLine();
            Console.WriteLine($"Отлично, {name}!");

            ConsoleKeyInfo key;

            Console.WriteLine("Выбери уровень сложности, на котором хочешь заниматься:");
            Console.WriteLine($"1) {Difficulty.Easy.ToStr().PadRight(15)}: простые примеры на сложение, вычитание, умножение и деление");
            Console.WriteLine($"2) {Difficulty.Medium.ToStr().PadRight(15)}: то же, но примеры сложнее, плюс простые числа и периметр");
            //Console.WriteLine($"3) {Difficulty.High.ToStr().PadRight(15)}: примеры ещё сложнее, некоторые с отрицательными числами");
            Console.WriteLine($"3) {Difficulty.High.ToStr().PadRight(15)}: примеры ещё сложнее");
            do
            {
                key = Console.ReadKey();
            } while ((key.KeyChar != '1') && (key.KeyChar != '2') && (key.KeyChar != '3'));

            var level = Difficulty.Unknown;
            switch (key.KeyChar)
            {
                case '1': level = Difficulty.Easy; break;
                case '2': level = Difficulty.Medium; break;
                case '3': level = Difficulty.High; break;
                default: level = Difficulty.Medium; break;
            }

            Console.WriteLine();
            Console.WriteLine("Выбран " + level.ToStr());
            LogLine("", false);
            LogLine("Выбран " + level.ToStr());

            var tg = new TaskGenerator(level);

            do
            {
                LogLine("-----------------------------------", false);
                LogLine($"Решает {name}:");

                Console.WriteLine();
                Console.WriteLine("На экране будут появляться примеры, нужно вводить ответ и нажимать Enter, чтобы перейти к следующему примеру.");
                Console.WriteLine($"На всё будет {Duration} сек. Успей как можно больше!");
                Console.WriteLine("Нажми Enter, когда будешь готов решать задачки.");
                Console.ReadLine();

                PlayRound(tg);

                Console.WriteLine();
                Console.WriteLine($"{name}, здорово!");
                Console.WriteLine("Занимайся каждый день по 10 минут - и твоя математика всегда будет супер!");
                Console.WriteLine("Нажми 1, если хочешь продолжить, или любую другую кнопку, если больше не хочешь заниматься: ");
                key = Console.ReadKey();
            } while (key.KeyChar == '1');
        }

        private static void PlayRound(TaskGenerator tg)
        {
            tg.Reset();
            var sw = new Stopwatch();
            sw.Start();
            string res;

            while (sw.Elapsed < TimeSpan.FromSeconds(Duration))
            {
                var t = tg.Next();
                Console.Write(t.TaskToString());
                Log(t.TaskToString());
                t.StartTimer();

                var answer = "";
                do
                {
                    answer = Console.ReadLine();
                } while (!t.IsValidAnswer(answer));

                t.StopTimer();

                LogLine(answer, false);

                res = t.CheckAnswer(answer) ? "Верно!" : "";
                Console.WriteLine(res);
                LogLine(res);

                Console.WriteLine();
            }

            sw.Stop();

            Console.WriteLine();
            res = tg.GetResults();
            LogLine(res);
            LogLine("-----------------------------------", false);
            Console.WriteLine(res);
        }


        private static string LogFileName => @"c:\MathTrainer.txt";

        private static void LogLine(string value, bool date = true) => Log(value + "\r\n", date);

        private static void Log(string value, bool date = true)
        {
            if (date)
            {
                value = $"{DateTime.Now}\t" + value;
            }

            try
            {
                System.IO.File.AppendAllText(LogFileName, value);
            }
            catch
            {
                // ignored
            }
        }
    }
}
