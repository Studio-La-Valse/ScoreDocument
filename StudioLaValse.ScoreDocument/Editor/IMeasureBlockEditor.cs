namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IMeasureBlockEditor : IMeasureBlock
    {
        IEnumerable<IChordEditor> EditChords();



        void Clear();
        void AppendChord(RythmicDuration rythmicDuration);
        void Splice(int index);


        void ApplyLayout(INoteGroupLayout layout);
        INoteGroupLayout ReadLayout();


        void Rebeam();
    }
}
