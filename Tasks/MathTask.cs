using System;
using System.Diagnostics;

namespace MathTrainer
{
    public abstract class MathTask
    {
        protected int[] Args;
        public Operation Operation { get; }
        protected string Answer;
        protected readonly Difficulty Level;

        protected MathTask(Operation op, Difficulty level)
        {
            this.Operation = op;
            this.Level = level;
        }
        protected abstract bool AreArgsValuesValid(int[] args);
        protected virtual int ArgsCount { get; } = 2;
        protected virtual void ArgsPostProcessing(int[] args) { }
        protected abstract int Calc();
        public bool CheckAnswer(string value)
        {
            Answer = value;
            return IsCorrect;
        }
        public void GenerateArgs(int step, Random rnd)
        {
            var args = new int[ArgsCount];
            var limit = GetArgsLimit(step);
            bool ok;
            do
            {
                for (var i = 0; i < ArgsCount; i++)
                {
                    args[i] = rnd.Next(limit);
                }

                ok = AreArgsValuesValid(args);
            } while (!ok);

            ArgsPostProcessing(args);

            SetArgs(args);
        }
        protected virtual int GetArgsLimit(int step) => Math.Min(100, 5 + step * 5);
        public virtual bool IsCorrect
        {
            get
            {
                try
                {
                    var v = Convert.ToInt32(Answer);
                    return v == Calc();
                }
                catch
                {
                    return false;
                }
            }
        }
        public virtual bool IsValidAnswer(string value) => Int32.TryParse(value, out _);
        public abstract string TaskToString();
        protected void SetArgs(params int[] args)
        {
            if (NeedSortArgs)
            {
                Array.Sort(args, (x, y) => -x.CompareTo(y));
            }
            Args = args;
        }
        private Stopwatch _stopwatch = new Stopwatch();
        public void StartTimer() => _stopwatch.Start();
        public void StopTimer() => _stopwatch.Stop();
        public long Elapsed => _stopwatch.ElapsedMilliseconds;
        public override int GetHashCode()
        {
            var s = 0d;
            for (var i = 0; i < Args.Length; i++)
            {
                s += Args[i] * Math.Pow(10, i * 2);
            }

            return ((int)Operation) * 123456789 + (int)Math.Round(s);
        }
        public abstract string TaskName { get; }
        protected virtual bool NeedSortArgs => false;
    }
}