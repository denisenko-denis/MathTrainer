namespace MathTrainer.Tasks
{
    public class Substract : MathTask
    {
        public Substract(Operation op, Difficulty level) : base(op, level) { }
        protected override bool AreArgsValuesValid(int[] args) => ((args[0] > 0) || (args[1] > 0));
        protected override int Calc() => Args[0] - Args[1];
        protected override bool NeedSortArgs => true;
        public override string TaskToString() => $"{Args[0]} - {Args[1]} = ";
        public override string TaskName => "Вычитание";
    }
}
