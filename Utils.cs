using System;

namespace MathTrainer
{
    public static class Utils
    {
        public static string Capitalize(this string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }

            return value[0].ToString().ToUpper() + value.Substring(1);
        }

        public static string ToStr(this Difficulty level)
        {
            var res = "";
            switch (level)
            {
                case Difficulty.Unknown:
                    res = "неизвестный уровень";
                    break;
                case Difficulty.Easy:
                    res = "лёгкий уровень";
                    break;
                case Difficulty.Medium:
                    res = "средний уровень";
                    break;
                case Difficulty.High:
                    res = "высокий уровень";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }

            return res;
        }
    }
}
