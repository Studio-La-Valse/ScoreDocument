namespace StudioLaValse.ScoreDocument.Core
{
    public class Interval
    {
        public static Interval DiminishedUnison =>
            new Interval(0, -1);
        public static Interval Unison =>
            new Interval(0, 0);
        public static Interval AugmentedUnison =>
             new Interval(0, 1);

        public static Interval Second =>
            new Interval(1, 0);
        public static Interval AugmentedSecond =>
           new Interval(1, 1);
        public static Interval MinorSecond =>
           new Interval(1, -1);

        public static Interval Third =>
            new Interval(2, 0);
        public static Interval AugmentedThird =>
           new Interval(2, 1);
        public static Interval MinorThird =>
           new Interval(2, -1);

        public static Interval Fourth =>
            new Interval(3, 0);
        public static Interval AugmentedFourth =>
            new Interval(3, 1);
        public static Interval MinorFourth =>
             new Interval(3, -1);

        public static Interval Fifth =>
            new Interval(4, 0);
        public static Interval AugmentedFifth =>
           new Interval(4, 1);
        public static Interval MinorFifth =>
           new Interval(4, -1);

        public static Interval Sixth =>
            new Interval(5, 0);
        public static Interval AugmentedSixth =>
           new Interval(5, 1);
        public static Interval MinorSixth =>
           new Interval(5, -1);

        public static Interval Seventh =>
            new Interval(6, 0);
        public static Interval AugmentedSeventh =>
            new Interval(6, 1);
        public static Interval MinorSeventh =>
             new Interval(6, -1);

        public static Interval Octave =>
            new Interval(7, 0);
        public static Interval AugmentedOctave =>
           new Interval(7, 1);
        public static Interval OctaveDiminished =>
           new Interval(7, -1);

        public int Steps { get; }
        public int Shifts { get; }
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
