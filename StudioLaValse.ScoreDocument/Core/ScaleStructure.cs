namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical scale structure.
    /// </summary>
    public class ScaleStructure
    {
        /// <summary>
        /// Represents a major scale.
        /// </summary>
        public static ScaleStructure Major =>
            new ScaleStructure(         //f##
                Interval.Second,        //->g##
                Interval.Second,        //->a##
                Interval.MinorSecond,   //->b#
                Interval.Second,        //->c##
                Interval.Second,        //->d##
                Interval.Second,        //->e##
                Interval.MinorSecond);  //->f##
        /// <summary>
        /// Represents a minor scale.
        /// </summary>
        public static ScaleStructure Minor =>
            new ScaleStructure(         //f##
                Interval.Second,        //->g##
                Interval.MinorSecond,   //->a#
                Interval.Second,        //->b#
                Interval.Second,        //->c##
                Interval.MinorSecond,   //->d#
                Interval.Second,        //->e#
                Interval.Second);       //->f##
        /// <summary>
        /// Represents a chromatic scale.
        /// </summary>
        public static ScaleStructure Chromatic =>
            new ScaleStructure(             //c
                Interval.AugmentedUnison,   //c#
                Interval.MinorSecond,       //d
                Interval.MinorSecond,       //eb
                Interval.AugmentedUnison,   //e                       
                Interval.MinorSecond,       //f
                Interval.AugmentedUnison,   //f#
                Interval.MinorSecond,       //g
                Interval.AugmentedUnison,   //g#
                Interval.MinorSecond,       //a
                Interval.MinorSecond,       //bb
                Interval.AugmentedUnison,   //b
                Interval.MinorSecond);      //c


        /// <summary>
        /// Enumerates the invals in the scale.
        /// </summary>
        public IEnumerable<Interval> Intervals { get; }
        /// <summary>
        /// The number of intervals in the scale.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Construct a scale structure from the specified steps.
        /// </summary>
        /// <param name="steps"></param>
        public ScaleStructure(params Interval[] steps)
        {
            Intervals = steps;
            Length = steps.Length;

        }
    }
}
