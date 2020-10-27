using System;

namespace MathTrainer.Tasks
{
    public class Prime : MathTask
    {
        public Prime(Operation op, Difficulty level) : base(op, level) { }
        protected override bool AreArgsValuesValid(int[] args) => (args[0] > 1) && (args[0] % 2 != 0);
        protected override int ArgsCount { get; } = 1;
        protected override int Calc()
        {
            var limit = (int)Math.Truncate(Math.Sqrt(Args[0]));
            var isPrime = true;
            var d = 2;
            while (d <= limit)
            {
                if (Args[0] % d == 0)
                {
                    isPrime = false;
                    break;
                }

                d++;
            }

            return Convert.ToInt32(isPrime);
        }
        public override bool IsCorrect => Answer.ToLower() == (Calc() == 1 ? "y" : "n");
        public override bool IsValidAnswer(string value) => (value.ToLower() == "y") || (value.ToLower() == "n");
        public override string TaskToString() => $"{Args[0]} простое число (\"Y\" простое, \"N\" составное)? ";
        public override string TaskName => "Простые числа";
    }
}