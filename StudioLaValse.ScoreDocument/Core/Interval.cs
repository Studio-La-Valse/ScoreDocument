namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical interval.
    /// </summary>
    public class Interval
    {
        /// <summary>
        /// A diminished unison.
        /// </summary>
        public static Interval DiminishedUnison =>
            new Interval(0, -1);
        /// <summary>
        /// A unison.
        /// </summary>
        public static Interval Unison =>
            new Interval(0, 0);
        /// <summary>
        /// An augmented unison.
        /// </summary>
        public static Interval AugmentedUnison =>
             new Interval(0, 1);

        /// <summary>
        /// A second.
        /// </summary>
        public static Interval Second =>
            new Interval(1, 0);
        /// <summary>
        /// An augmented second.
        /// </summary>
        public static Interval AugmentedSecond =>
           new Interval(1, 1);
        /// <summary>
        /// A minor second.
        /// </summary>
        public static Interval MinorSecond =>
           new Interval(1, -1);

        /// <summary>
        /// A third.
        /// </summary>
        public static Interval Third =>
            new Interval(2, 0);
        /// <summary>
        /// An augmented third.
        /// </summary>
        public static Interval AugmentedThird =>
           new Interval(2, 1);
        /// <summary>
        /// A minor third.
        /// </summary>
        public static Interval MinorThird =>
           new Interval(2, -1);

        /// <summary>
        /// A fourth.
        /// </summary>
        public static Interval Fourth =>
            new Interval(3, 0);
        /// <summary>
        /// An augmented fourth.
        /// </summary>
        public static Interval AugmentedFourth =>
            new Interval(3, 1);
        /// <summary>
        /// A minor fourth.
        /// </summary>
        public static Interval MinorFourth =>
             new Interval(3, -1);

        /// <summary>
        /// A fifth.
        /// </summary>
        public static Interval Fifth =>
            new Interval(4, 0);
        /// <summary>
        /// An augmented fifth.
        /// </summary>
        public static Interval AugmentedFifth =>
           new Interval(4, 1);
        /// <summary>
        /// A minor fifth.
        /// </summary>
        public static Interval MinorFifth =>
           new Interval(4, -1);

        /// <summary>
        /// A sixth.
        /// </summary>
        public static Interval Sixth =>
            new Interval(5, 0);
        /// <summary>
        /// An augmented sixth.
        /// </summary>
        public static Interval AugmentedSixth =>
           new Interval(5, 1);
        /// <summary>
        /// A minor sixth.
        /// </summary>
        public static Interval MinorSixth =>
           new Interval(5, -1);

        /// <summary>
        /// A seventh.
        /// </summary>
        public static Interval Seventh =>
            new Interval(6, 0);
        /// <summary>
        /// An augmented seveth.
        /// </summary>
        public static Interval AugmentedSeventh =>
            new Interval(6, 1);
        /// <summary>
        /// A minor seventh.
        /// </summary>
        public static Interval MinorSeventh =>
             new Interval(6, -1);

        /// <summary>
        /// An octave.
        /// </summary>
        public static Interval Octave =>
            new Interval(7, 0);
        /// <summary>
        /// An augmented octave.
        /// </summary>
        public static Interval AugmentedOctave =>
           new Interval(7, 1);
        /// <summary>
        /// A dimished octave.
        /// </summary>
        public static Interval OctaveDiminished =>
           new Interval(7, -1);

        /// <summary>
        /// The number of steps in the interval.
        /// </summary>
        public int Steps { get; }
        /// <summary>
        /// The number of shifts in the interval, applied after the number of steps.
        /// </summary>
        public int Shifts { get; }
        /// <summary>
        /// Calculates the number of semitones in the interval.
        /// </summary>
        public int SemiTones
        {
            get
            {
                var chromaticDistance = 0;

                //C D E F G A B etc
                for (int i = 0; i < Steps; i++)
                {
                    var indexInOctave = i % 7;

                    chromaticDistance += indexInOctave == 2 || indexInOctave == 6 ? 1 : 2;
                }

                chromaticDistance += Shifts;

                return chromaticDistance;
            }
        }


        private Interval(int stepsFromC, int shift)
        {
            Steps = stepsFromC;
            Shifts = shift;
        }
    }
}
