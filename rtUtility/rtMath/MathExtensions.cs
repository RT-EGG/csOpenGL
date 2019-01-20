// System
using System;

namespace rtUtility.rtMath
{
    public static class TMathExtension
    {
        const float con_FloatThreshold = 1.0e-6f;
        const float con_DoubleThreshold = 1.0e-10f;

        public static bool IsZero(this float aValue, float aThreshould = con_FloatThreshold)
        {
            return Math.Abs(aValue) <= aThreshould;
        }

        public static bool IsZero(this double aValue, double aThreshould = con_DoubleThreshold)
        {
            return Math.Abs(aValue) <= aThreshould;
        }

        public static bool AlmostEqual(this float aValue, float aValue2, float aThreshould = con_FloatThreshold)
        {
            return (aValue - aValue2).IsZero(aThreshould);
        }

        public static bool AlmostEqual(this double aValue, double aValue2, double aThreshould = con_DoubleThreshold)
        {
            return (aValue - aValue2).IsZero(aThreshould);
        }

        public static T Min<T>(T aValue1, T aValue2) where T : IComparable
        {
            return (aValue1.CompareTo(aValue2) < 0) ? aValue1 : aValue2;
        }

        public static T Max<T>(T aValue1, T aValue2) where T : IComparable
        {
            return (aValue1.CompareTo(aValue2) > 0) ? aValue1 : aValue2;
        }

        public static T Clamp<T>(this T aValue, T aMin, T aMax) where T : IComparable
        {
            return Min(aMax, Max(aMin, aValue));
        }

        public static bool InRange<T>(this T aValue, T aMin, T aMax) where T : IComparable
        {
            return (aValue.CompareTo(aMin) >= 0) && (aValue.CompareTo(aMax) <= 0);
        }

        public static int Round(this float aValue)
        {
            return (int)(Math.Truncate(aValue + 0.5f));
        }

        public static int Round(this double aValue)
        {
            return (int)(Math.Truncate(aValue + 0.5));
        }

        public static int Round(this decimal aValue)
        {
            return (int)(Math.Truncate(aValue + 0.5m));
        }

        public static float Modulate(this float aValue, float aMin, float aMax)
        {
            return (float)Modulate((double)aValue, (double)aMin, (double)aMax);
        }

        public static double Modulate(this double aValue, double aMin, double aMax)
        {
            if (aValue.AlmostEqual(aMin))
                return aMin;
            if (aValue.AlmostEqual(aMax))
                return aMax;

            double times = (aValue - aMin) / (aMax - aMin);
            double s = times - (int)(times);
            double result = s * (aMax - aMin);

            if (s <= 0.0)
                result = aMax + result;
            else
                result = aMin + result;

            result = Math.Min(aMax, Math.Max(aMin, result));
            return result;
        }

        public static int Pow(this int aValue, uint aExp)
        {
            if (aValue == 0)
                return 0;

            int result = 1;
            for (uint i = 0; i < aExp; ++i) {
                result *= aValue;
            }

            return result;
        }

        public static uint Pow(this uint aValue, uint aExp)
        {
            if (aValue == 0)
                return 0;

            uint result = 1;
            if (aExp >= 0) {
                for (int i = 0; i < aExp; ++i) {
                    result *= aValue;
                }
            }

            return result;
        }

        public static uint UpToPowerOfTwo(this uint aValue)
        {
            switch (aValue) {
                case 0: return 0;
                case 1: return 1;
                default:
                    if (aValue < 0)
                        return 0;

                    return (uint)Math.Pow(2.0, Math.Ceiling(Math.Log((int)aValue, 2.0))).Round();
            }
        }

        public static uint DownToPowerOfTwo(this uint aValue)
        {
            switch (aValue) {
                case 0: return 0;
                case 1: return 1;
                default:
                    if (aValue < 0)
                        return 0;

                    return (uint)Math.Pow(2.0, Math.Truncate(Math.Log((int)aValue, 2.0))).Round();
            }
        }

        public static double RadToDeg(this double aRadian)
        {
            const double con_RtD = 180.0 / Math.PI;
            return aRadian * con_RtD;
        }

        public static double DegToRad(this double aDegree)
        {
            const double con_DtR = Math.PI / 180.0;
            return aDegree * con_DtR;
        }

        public static int MaxIndex<T>(this T[] aValues) where T : IComparable
        {
            if (aValues.Length == 0)
                return -1;

            int result = 0;
            for (int i = 1; i < aValues.Length; ++i) {
                if (aValues[result].CompareTo(aValues[i]) < 0)
                    result = i;
            }
            return result;
        }

        public static int MinIndex<T>(this T[] aValues) where T : IComparable
        {
            if (aValues.Length == 0)
                return -1;

            int result = 0;
            for (int i = 1; i < aValues.Length; ++i) {
                if (aValues[result].CompareTo(aValues[i]) > 0)
                    result = i;
            }
            return result;
        }
    }
}
