namespace MathTrainer.Tasks
{
    public class Add : MathTask
    {
        public Add(Operation op, Difficulty level) : base(op, level) { }
        protected override bool AreArgsValuesValid(int[] args) => ((args[0] > 0) || (args[1] > 0));
        protected override int Calc() => Args[0] + Args[1];
        public override string TaskName => "Сложение";
        public override string TaskToString() => $"{Args[0]} + {Args[1]} = ";
    }
}
