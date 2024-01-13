namespace StudioLaValse.ScoreDocument.Core
{
    public class ChordStructure
    {
        public static ChordStructure Major =>
            new ChordStructure(
                Interval.Unison,
                Interval.Third,
                Interval.Fifth);

        public static ChordStructure Minor =>
            new ChordStructure(
                Interval.Unison,
                Interval.MinorThird,
                Interval.Fifth);



        public IEnumerable<Interval> Intervals { get; }


        public ChordStructure(params Interval[] intervals)
        {
            Intervals = intervals;
        }
    }
}
