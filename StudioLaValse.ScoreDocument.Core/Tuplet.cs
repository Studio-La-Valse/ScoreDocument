namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical tuplet.
    /// </summary>
    public class Tuplet
    {
        private readonly RythmicDuration[] _durations;

        /// <summary>
        /// The length of the unchanged sum of the duration of the content of this tuplet.
        /// </summary>
        public Duration SourceLength => _durations.Sum();
        /// <summary>
        /// The actual musical duration of this tuplet.
        /// </summary>
        public Duration TargetLength { get; }
        /// <summary>
        /// Calculates whether the content duration of the tuplet is equal to the actual duration.
        /// </summary>
        public bool IsRedundant =>
            SourceLength.Decimal == TargetLength.Decimal;


        /// <summary>
        /// Construct a tuplet from a target duration and a set of rythmic durations.
        /// </summary>
        /// <param name="targetLength"></param>
        /// <param name="durations"></param>
        public Tuplet(Duration targetLength, params RythmicDuration[] durations)
        {
            _durations = [.. durations];
            TargetLength = targetLength;
        }

        /// <summary>
        /// Transform the rythmic duration to an actual duration for the specified tuplet.
        /// </summary>
        /// <param name="rythmicDuration"></param>
        /// <returns></returns>
        public Fraction ToActualDuration(RythmicDuration rythmicDuration)
        {
            if (IsRedundant)
            {
                return rythmicDuration;
            }

            //// lets say this tuplet contains 15 1/16ths in the space of 3 1/8ths;
            ////          3/8
            //Fraction result;
            //if (rythmicDuration.Denominator <= TargetLength.Denominator)
            //{
            //    // a base rythmic duration of 3/8ths now means 6 1/16ths of 15 1/16ths, so 6/15ths of 3 eights => (6/15) * (3/8) => (18/120) 

            //    //       2                      16                      8
            //    var modeFraction = SourceLength.Denominator / rythmicDuration.Denominator;

            //    //        6                     3                       2
            //    var beatsInContainer = rythmicDuration.Numinator * modeFraction;

            //    result = new Fraction(
            //        //      6                       3
            //        beatsInContainer * TargetLength.Numinator,
            //        //      15                                  8
            //        SourceLength.Numinator * TargetLength.Denominator);
            //}
            //else
            //{
            //    // a base rythmic duration of 3 1/32ths now means 3 1/32ths of 30 1/32ths, so 3/30ths of 3 eights => (3/30) * (3/8) => (9/240) 

            //    //       2                  32                                 16
            //    var modeFraction = rythmicDuration.Denominator / SourceLength.Denominator;

            //    //        30                    15                  2
            //    var beatsInContainer = SourceLength.Numinator * modeFraction;

            //    result = new Fraction(
            //        //      3                               3
            //        rythmicDuration.Numinator * TargetLength.Numinator,
            //        //       30                     8
            //        beatsInContainer * TargetLength.Denominator);
            //}
            //return result.Simplify();

            var denom = rythmicDuration.Denominator * TargetLength.Denominator;
            denom *= SourceLength.Numinator;

            var num = rythmicDuration.Numinator * TargetLength.Numinator;
            num *= SourceLength.Denominator;

            var fraction = new Fraction(num, denom).Simplify();
            return fraction;
        }
    }
}
