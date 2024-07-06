using StudioLaValse.ScoreDocument.Core.Private;

namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Allows any valid musical notated element duration, including dotted rythms, excluding tuplet notations
    /// </summary>
    public class RythmicDuration : Duration, IEquatable<RythmicDuration>
    {
        /// <summary>
        /// A default quarter note.
        /// </summary>
        public static readonly RythmicDuration QuarterNote = new (4);

        /// <summary>
        /// The number of dots.
        /// </summary>
        public int Dots { get; }
        /// <summary>
        /// The denominator of the duration. For example 8 in a one/eigth note. The numerator of a rythmic duration is always 1.
        /// </summary>
        public PowerOfTwo PowerOfTwo { get; }



        /// <summary>
        /// Constructs a rythmic duration from a power of two and the number of dots.
        /// </summary>
        /// <param name="powerOfTwo">The power of two for the duration, for example 8 in the case of a 1/8th duration.</param>
        /// <param name="dots">The number of dots.</param>
        public RythmicDuration(PowerOfTwo powerOfTwo, int dots = 0) : base(IntPow(2, dots + 1) - 1, powerOfTwo * IntPow(2, dots))
        {
            ArgumentOutOfRangeException.ThrowIfNegative(dots);

            PowerOfTwo = powerOfTwo;
            Dots = dots;
        }


        private static int IntPow(int x, int pow)
        {
            var ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                {
                    ret *= x;
                }

                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        /// <summary>
        /// Tries to construct a rythmic duration from a duration.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static bool TryConstruct(Fraction duration, out RythmicDuration output)
        {
            output = null!;

            if (duration.Numerator == 0)
            {
                return false;
            }

            //required
            //todo: allow breve note which is a 2/1 note (double of a whole)
            //for now a whole note (one over two to the power 0) is the longest note allowed)
            if (duration.Numerator > duration.Denominator)
            {
                return false;
            }

            if (!PowerOfTwo.TryCreate(duration.Denominator, out var durationDenominatorAsPowerOfTwo))
            {
                return false;
            }

            if (PowerOfTwo.TryCreate(duration.Numerator, out var beatsAsPower))
            {
                //eg: 8/16 => 1/2
                //eg: 64/256 => 1/4
                var gcd = durationDenominatorAsPowerOfTwo.Value.GCD(beatsAsPower.Value);

                var smallerPower = durationDenominatorAsPowerOfTwo.Value / gcd;

                if (!PowerOfTwo.TryCreate(smallerPower, out var _power))
                {
                    return false;
                }

                output = new RythmicDuration(_power, 0);
                return true;
            }

            //valid: 3, 7, 15, 31, etc
            if (!PowerOfTwo.TryCreate(duration.Numerator + 1, out var durationNuminatorPlusOneAsPowerOfTwo))
            {
                return false;
            }

            //consider (3 / 8)                                                  = 1 / 4 with one dot
            //consider (7 / 32 => 3 / 16)                                       = 1 / 8 with two dots
            //consider (15 / 32 =>  7 / 16 => 3 / 8)                            = 1 / 4 with 3 dots
            //consider (63 / 256 => 31 / 128 => 15 / 64 => 7 / 32 => 3 / 16)    = 1 / 8 with 5 dots
            //consider (7 / 64 => 3 / 32)                                       = 1 / 16 with 2 dots
            var dots = 0;
            while (durationNuminatorPlusOneAsPowerOfTwo.Value > 2)
            {
                if (!durationDenominatorAsPowerOfTwo.TryDivideByTwo(out durationDenominatorAsPowerOfTwo))
                {
                    return false;
                }

                if (!durationNuminatorPlusOneAsPowerOfTwo.TryDivideByTwo(out durationNuminatorPlusOneAsPowerOfTwo))
                {
                    return false;
                }

                dots++;
            }

            output = new RythmicDuration(durationDenominatorAsPowerOfTwo, dots);
            return true;
        }


        /// <summary>
        /// Halves the rythmic duration.
        /// </summary>
        /// <returns></returns>
        public RythmicDuration HalfDuration()
        {
            return new RythmicDuration(new PowerOfTwo(PowerOfTwo.Power + 1), Dots);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var start = $"1 / {PowerOfTwo.Value}";

            for (var i = 0; i < Dots; i++)
            {
                start += "dot ";
            }

            return start;
        }

        /// <inheritdoc/>
        public bool Equals(RythmicDuration? other)
        {
            return other != null && other.PowerOfTwo.Equals(PowerOfTwo) && other.Dots == Dots;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return new Tuple<int, PowerOfTwo>(Dots, PowerOfTwo).GetHashCode();
        }
    }
}
