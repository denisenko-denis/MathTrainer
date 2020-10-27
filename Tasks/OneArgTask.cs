using System;

namespace MathTrainer
{
    public abstract class OneArgTask : MathTask
    {
        public OneArgTask(Operation op) : base(op) { }
        public override void SetArgs(params int[] args) => this.arg1 = args[0];
    }
}