using StudioLaValse.ScoreDocument.Reader.Private;

namespace StudioLaValse.ScoreDocument.Reader.Extensions
{
    public static class GraceExtensions
    {
        public static IMeasureBlockReader Cast(this IGraceGroupReader graceGroupReader)
        {
            return new MeasureBlockReaderFromGraceGroup(graceGroupReader);
        }

        internal static IChordReader Cast(this IGraceChordReader graceChordReader, MeasureBlockReaderFromGraceGroup measureBlockReaderFromGraceGroup)
        {
            return new ChordReaderFromGraceChord(graceChordReader, measureBlockReaderFromGraceGroup);
        }

        internal static INoteReader Cast(this IGraceNoteReader noteReader, ChordReaderFromGraceChord chordReaderFromGraceChord)
        {
            return new NoteReaderFromGraceChord(noteReader, chordReaderFromGraceChord);
        }

        public static Tuplet CreateTuplet(this IGraceGroupReader graceGroupReader)
        {
            var fallback = RythmicDuration.QuarterNote;
            return new(graceGroupReader.CreateImpliedRythmicDuration(fallback), graceGroupReader.ReadChords().Select(c => graceGroupReader.ReadLayout().ChordDuration).ToArray());
        }

        public static RythmicDuration CreateImpliedRythmicDuration(this IGraceGroupReader graceGroupReader, RythmicDuration fallback)
        {
            if (RythmicDuration.TryConstruct(graceGroupReader.CreateImpliedDuration(), out var rythmicDuration))
            {
                return rythmicDuration;
            }

            return fallback;
        }

        public static Duration CreateImpliedDuration(this IGraceGroupReader graceGroupReader)
        {
            var duration = graceGroupReader.ReadLayout().ChordDuration * graceGroupReader.ReadChords().Count();
            return duration;
        }
    }
}
