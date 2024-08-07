﻿namespace StudioLaValse.ScoreDocument.Core.Private
{
    internal static class MathUtils
    {
        public static IList<double> Map(IList<double> list, double newMin, double newMax)
        {
            var listMin = list.Min();
            var listMax = list.Max();

            if (listMax - listMin == 0)
            {
                return list;
            }

            List<double> newList = [];

            foreach (var value in list)
            {
                var newValue = Map(value, listMin, listMax, newMin, newMax);

                newList.Add((float)newValue);
            }

            return newList;
        }
        public static IList<float> Map(IList<float> list, float newMin, float newMax)
        {
            var listMin = list.Min();
            var listMax = list.Max();

            if (listMax - listMin == 0)
            {
                return list;
            }

            List<float> newList = [];

            foreach (var value in list)
            {
                var newValue = Map(value, listMin, listMax, newMin, newMax);

                newList.Add((float)newValue);
            }

            return newList;
        }
        public static double[] SubSampleValues(IEnumerable<double> values, int newLength)
        {
            if (!values.Any())
            {
                return new double[0];
            }

            var originalCount = values.Count();

            if (newLength == originalCount)
            {
                return values.ToArray();
            }

            var newArray = new double[newLength];

            for (var i = 0; i < newLength; i++)
            {
                var indexInOriginalCollection = (int)Math.Floor(Map(i, 0d, newLength, 0, originalCount));
                newArray[i] = values.ElementAt(indexInOriginalCollection);
            }

            return newArray;
        }
        public static float[] SubSampleValues(IEnumerable<float> values, int newLength)
        {
            if (!values.Any())
            {
                return new float[0];
            }

            var originalCount = values.Count();

            if (newLength == originalCount)
            {
                return values.ToArray();
            }

            var newArray = new float[newLength];

            for (var i = 0; i < newLength; i++)
            {
                var indexInOriginalCollection = (int)Math.Floor(Map(i, 0d, newLength, 0, originalCount));
                newArray[i] = values.ElementAt(indexInOriginalCollection);
            }

            return newArray;
        }
        public static double Map(double value, double minStart, double maxStart, double minEnd, double maxEnd)
        {
            var fraction = maxStart - minStart;

            return fraction == 0 ? throw new ArgumentOutOfRangeException() : minEnd + ((maxEnd - minEnd) * ((value - minStart) / fraction));
        }
        public static decimal Map(decimal value, decimal minStart, decimal maxStart, decimal minEnd, decimal maxEnd)
        {
            var fraction = maxStart - minStart;

            return fraction == 0 ? throw new ArgumentOutOfRangeException() : minEnd + ((maxEnd - minEnd) * ((value - minStart) / fraction));
        }
        public static double Clamp(double value, double minValue, double maxValue)
        {
            var trueMin = Math.Min(minValue, maxValue);
            var trueMax = Math.Max(minValue, maxValue);

            return Math.Clamp(value, trueMin, trueMax);
        }
        public static decimal Clamp(decimal value, decimal minValue, decimal maxValue)
        {
            var trueMin = Math.Min(minValue, maxValue);

            var trueMax = Math.Max(minValue, maxValue);

            return Math.Clamp(value, trueMin, trueMax);
        }
        public static int Clamp(int value, int minValue, int maxValue)
        {
            var trueMin = Math.Min(minValue, maxValue);

            var trueMax = Math.Max(minValue, maxValue);

            return Math.Clamp(value, trueMin, trueMax);
        }
        public static double ForcePositiveModulo(double value, double max)
        {
            while (value < 0)
            {
                value += max;
            }

            value %= max;

            return value;
        }
        public static int UnsignedModulo(int value, int max)
        {
            if (max <= 0)
            {
                throw new Exception("The max value needs to be greater than or equal to one.");
            }

            while (value < 0)
            {
                value += max;
            }

            var unsignedValue = value % max;

            return unsignedValue;
        }
        public static bool IsAlmostEqualTo(this double a, double b, double epsilon = 0.001)
        {
            return Math.Abs(a - b) < epsilon;
        }

        public static int GCD(this int[] numbers)
        {
            return numbers.Aggregate(GCD);
        }

        public static int GCD(this int a, int b)
        {
            return (a == 0 || b == 0) ? a | b : GCD(Math.Min(a, b), Math.Max(a, b) % Math.Min(a, b));
        }
    }
}
