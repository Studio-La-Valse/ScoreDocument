namespace StudioLaValse.ScoreDocument.Core
{
    public class ScaleStructure
    {
        public static ScaleStructure Major =>
            new ScaleStructure(         //f##
                Interval.Second,        //->g##
                Interval.Second,        //->a##
                Interval.MinorSecond,   //->b#
                Interval.Second,        //->c##
                Interval.Second,        //->d##
                Interval.Second,        //->e##
                Interval.MinorSecond);  //->f##
        public static ScaleStructure Minor =>
            new ScaleStructure(         //f##
                Interval.Second,        //->g##
                Interval.MinorSecond,   //->a#
                Interval.Second,        //->b#
                Interval.Second,        //->c##
                Interval.MinorSecond,   //->d#
                Interval.Second,        //->e#
                Interval.Second);       //->f##
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



        public IEnumerable<Interval> Intervals { get; }
        public int Length { get; }

        public ScaleStructure(params Interval[] steps)
        {
            Intervals = steps;
            Length = steps.Length;

        }
    }
}
