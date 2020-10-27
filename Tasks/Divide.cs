namespace MathTrainer.Tasks
{
    public class Divide : MathTask
    {
        public Divide(Operation op, Difficulty level) : base(op, level) { }

        protected override bool AreArgsValuesValid(int[] args)
        {
            var min = (Level > Difficulty.Easy ? 1 : 0);
            var ok = ((args[0] > min) && (args[1] > min));

            return ok && (Level < Difficulty.High || ((args[0] <= 10) || (args[1] <= 10)));
        }

        protected override void ArgsPostProcessing(int[] args) => args[0] *= args[1];
        protected override int Calc() => Args[0] / Args[1];
        protected override int GetArgsLimit(int step) => (Level < Difficulty.High ? 10 : 20);
        protected override bool NeedSortArgs => true;
        public override string TaskName => "Деление";
        public override string TaskToString() => $"{Args[0]} / {Args[1]} = ";
    }
}