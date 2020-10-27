using System;
using System.Text.RegularExpressions;

namespace MathTrainer
{
    public abstract class TwoArgsTask : MathTask
    {
        public TwoArgsTask(Operation op) : base(op) { }
        public virtual bool NeedSwapArgs { get; } = false;
        public override void SetArgs(params int[] args)
        {
            this.Args = args;

            if (NeedSwapArgs)
            {
                SwapArgs();
            }
        }
        public void SwapArgs()
        {
            if (arg2 > arg1)
            {
                var tmp = arg1;
                arg1 = arg2;
                arg2 = tmp;
            }
        }
    }
}