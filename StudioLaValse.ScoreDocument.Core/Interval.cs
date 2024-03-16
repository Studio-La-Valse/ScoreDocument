namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical interval.
    /// </summary>
    public readonly struct Interval
    {
        /// <summary>
        /// A diminished unison.
        /// </summary>
        public static Interval DiminishedUnison =>
            new(0, -1);
        /// <summary>
        /// A unison.
        /// </summary>
        public static Interval Unison =>
            new(0, 0);
        /// <summary>
        /// An augmented unison.
        /// </summary>
        public static Interval AugmentedUnison =>
             new(0, 1);

        /// <summary>
        /// A second.
        /// </summary>
        public static Interval Second =>
            new(1, 0);
        /// <summary>
        /// An augmented second.
        /// </summary>
        public static Interval AugmentedSecond =>
           new(1, 1);
        /// <summary>
        /// A minor second.
        /// </summary>
        public static Interval MinorSecond =>
           new(1, -1);

        /// <summary>
        /// A third.
        /// </summary>
        public static Interval Third =>
            new(2, 0);
        /// <summary>
        /// An augmented third.
        /// </summary>
        public static Interval AugmentedThird =>
           new(2, 1);
        /// <summary>
        /// A minor third.
        /// </summary>
        public static Interval MinorThird =>
           new(2, -1);

        /// <summary>
        /// A fourth.
        /// </summary>
        public static Interval Fourth =>
            new(3, 0);
        /// <summary>
        /// An augmented fourth.
        /// </summary>
        public static Interval AugmentedFourth =>
            new(3, 1);
        /// <summary>
        /// A minor fourth.
        /// </summary>
        public static Interval MinorFourth =>
             new(3, -1);

        /// <summary>
        /// A fifth.
        /// </summary>
        public static Interval Fifth =>
            new(4, 0);
        /// <summary>
        /// An augmented fifth.
        /// </summary>
        public static Interval AugmentedFifth =>
           new(4, 1);
        /// <summary>
        /// A minor fifth.
        /// </summary>
        public static Interval MinorFifth =>
           new(4, -1);

        /// <summary>
        /// A sixth.
        /// </summary>
        public static Interval Sixth =>
            new(5, 0);
        /// <summary>
        /// An augmented sixth.
        /// </summary>
        public static Interval AugmentedSixth =>
           new(5, 1);
        /// <summary>
        /// A minor sixth.
        /// </summary>
        public static Interval MinorSixth =>
           new(5, -1);

        /// <summary>
        /// A seventh.
        /// </summary>
        public static Interval Seventh =>
            new(6, 0);
        /// <summary>
        /// An augmented seveth.
        /// </summary>
        public static Interval AugmentedSeventh =>
            new(6, 1);
        /// <summary>
        /// A minor seventh.
        /// </summary>
        public static Interval MinorSeventh =>
             new(6, -1);

        /// <summary>
        /// An octave.
        /// </summary>
        public static Interval Octave =>
            new(7, 0);
        /// <summary>
        /// An augmented octave.
        /// </summary>
        public static Interval AugmentedOctave =>
           new(7, 1);
        /// <summary>
        /// A dimished octave.
        /// </summary>
        public static Interval OctaveDiminished =>
           new(7, -1);



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
                int chromaticDistance = 0;

                //C D E F G A B etc
                for (int i = 0; i < Steps; i++)
                {
                    int indexInOctave = i % 7;

                    chromaticDistance += indexInOctave is 2 or 6 ? 1 : 2;
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
