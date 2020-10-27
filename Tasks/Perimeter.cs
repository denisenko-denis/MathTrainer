using System;

namespace MathTrainer.Tasks
{
    public class Perimeter : MathTask
    {
        public Perimeter(Operation op, Difficulty level) : base(op, level) { }
        protected override bool AreArgsValuesValid(int[] args) => ((args[0] > 0) && (args[1] > 0));
        protected override int Calc() => (Args[0] + Args[1]) * 2;
        protected override int GetArgsLimit(int step) => 10;
        public override string TaskName => "Периметр";
        public override string TaskToString()
        {
            var cUnits = new string[] { "мм", "см", "дм", "м", "км" };
            var unit = cUnits[DateTime.Now.Second % cUnits.Length];
            if (Args[0] == Args[1])
                return $"Какова длина периметра квадрата со стороной {Args[0]} {unit}? ";
            else
                return $"Какова длина периметра прямоугольника со сторонами {Args[0]} {unit} и {Args[1]} {unit}? ";
        }
    }
}