using MathTrainer.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathTrainer
{
    public enum Difficulty { Unknown = 0, Easy = 1, Medium = 2, High = 3 }
    public enum Operation
    {
        Unknown = 0, Add = 1, Substract = 2, Multiply = 3, Divide = 4, Prime = 5,
        Perimeter = 6,
        Max = Perimeter
    }

    class TaskGenerator
    {
        private readonly Random _random = new Random();
        private int _step = 0;
        private readonly List<MathTask> _tasks = new List<MathTask>();
        private readonly HashSet<int> hs = new HashSet<int>();
        private readonly Difficulty level;

        public TaskGenerator(Difficulty level)
        {
            this.level = level;
        }

        private Operation GetNextOperationType()
        {
            var opLimit = Operation.Divide;
            if (level > Difficulty.Easy)
            {
                opLimit = Operation.Max;
            }

            //var opLimit = (_step <= 3 ? Operation.Substract : Operation.Max);
            //var op = (Operation)(_random.Next((int)opLimit) + 1);

            var op = (Operation)(_random.Next((int)opLimit) + 1);
            return op;
        }

        public MathTask Next()
        {
            _step++;

            MathTask t = null;

            do
            {
                var op = GetNextOperationType();

                switch (op)
                {
                    case Operation.Add: t = new Add(op, level); break;
                    case Operation.Substract: t = new Substract(op, level); break;
                    case Operation.Multiply: t = new Mult(op, level); break;
                    case Operation.Divide: t = new Divide(op, level); break;
                    case Operation.Prime: t = new Prime(op, level); break;
                    case Operation.Perimeter: t = new Perimeter(op, level); break;
                }

                t.GenerateArgs(_step, _random);
            } while (hs.Contains(t.GetHashCode()));

            _tasks.Add(t);
            hs.Add(t.GetHashCode());

            return t;
        }

        public string GetResults()
        {
            var correctAnswers = _tasks.Sum(x => x.IsCorrect ? 1 : 0);
            var pc = 100d * correctAnswers / _tasks.Count;

            var res = "Что-то слабенько сегодня...";
            if (correctAnswers == _tasks.Count) res = "Идеальный результат!";
            else if (pc >= 90) res = "Вообще молодец!";
            else if (pc >= 80) res = "Молодец!";
            else if (pc >= 70) res = "Неплохо!";
            else if (pc >= 50) res = "Больше половины решено правильно!";

            var sb = new StringBuilder();
            sb.AppendLine($"{res} Правильно {correctAnswers} из {_tasks.Count}");
            sb.AppendLine();

            var ln = _tasks.Max(x => x.TaskName.Length);
            sb.AppendLine("Тип задачи".PadRight(ln) + " | Кол-во | Ср время, сек | Ошибок");
            sb.AppendLine(new string('-', 50));

            _tasks
                .GroupBy(x => new { x.Operation, x.TaskName })
                .Select(x => new
                {
                    op = x.Key.Operation,
                    taskName = x.Key.TaskName,
                    count = x.Count(),
                    avgTime = (double)(x.Average(y => y.Elapsed) / 1000),
                    mistakes = x.Count(y => !y.IsCorrect)
                })
                .OrderBy(x => x.op)
                .ToList()
                .ForEach(x => sb.AppendLine(
                    $"{x.taskName.PadRight(ln)} | {x.count.ToString().PadRight(6)} | {Math.Round(x.avgTime, 1).ToString().PadRight(13)} | {x.mistakes}"));

            return sb.ToString();
        }

        public void Reset()
        {
            _step = 0;
            _tasks.Clear();
            hs.Clear();
        }
    }
}
