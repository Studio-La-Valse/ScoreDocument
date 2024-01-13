namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Allows any valid musical notated element duration, including dotted rythms, excluding tuplet notations
    /// </summary>
    public class RythmicDuration : Duration
    {
        public int Dots { get; }
        public PowerOfTwo PowerOfTwo { get; }




        public RythmicDuration(PowerOfTwo powerOfTwo, int dots = 0) : base(IntPow(2, (uint)(dots + 1)) - 1, powerOfTwo * IntPow(2, (uint)dots))
        {
            PowerOfTwo = powerOfTwo;
            Dots = dots;
        }


        private static int IntPow(int x, uint pow)
        {
            int ret = 1;
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


        public Fraction ToActualDuration(Tuplet tupletInformation)
        {
            if (tupletInformation.IsRedundant)
            {
                return this;
            }

            // lets say this tuplet contains 15 1/16ths in the space of 3 1/8ths;
            //          3/8
            var tupletActualLength = tupletInformation.TargetLength;
            Fraction result;
            if (Denominator <= tupletInformation.TargetLength.Denominator)
            {
                // a base rythmic duration of 3/8ths now means 6 1/16ths of 15 1/16ths, so 6/15ths of 3 eights => (6/15) * (3/8) => (18/120) 

                //       2                                            16              8
                var modeFraction = tupletInformation.SourceLength.Denominator / Denominator;

                //        6                   3               2
                var beatsInContainer = Numinator * modeFraction;

                result = new Fraction(
                    //      6                       3
                    beatsInContainer * tupletActualLength.Numinator,
                    //                      15                                          8
                    tupletInformation.SourceLength.Numinator * tupletActualLength.Denominator);
            }
            else
            {
                // a base rythmic duration of 3 1/32ths now means 3 1/32ths of 30 1/32ths, so 3/30ths of 3 eights => (3/30) * (3/8) => (9/240) 

                //       2               32                   16
                var modeFraction = Denominator / tupletInformation.SourceLength.Denominator;

                //        30                                 15                         2
                var beatsInContainer = tupletInformation.SourceLength.Numinator * modeFraction;

                result = new Fraction(
                    //      3                   3
                    Numinator * tupletActualLength.Numinator,
                    //       30                     8
                    beatsInContainer * tupletActualLength.Denominator);
            }
            return result.Simplify();
        }


        public RythmicDuration HalfDuration() =>
            new RythmicDuration(new PowerOfTwo(PowerOfTwo.Power + 1), Dots);




        public override string ToString()
        {
            var start = $"1 / {PowerOfTwo.Value}";

            for (int i = 0; i < Dots; i++)
            {
                start += "dot ";
            }

            return start;
        }
    }
}
