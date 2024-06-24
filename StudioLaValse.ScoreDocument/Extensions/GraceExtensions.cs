using StudioLaValse.ScoreDocument.Private;

namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// Extensions for grace type elements.
    /// </summary>
    public static class GraceExtensions
    {
        /// <summary>
        /// Cast a grace group reader to a measure block reader.
        /// </summary>
        /// <param name="graceGroupReader"></param>
        /// <returns></returns>
        public static IMeasureBlock Cast(this IGraceGroup graceGroupReader)
        {
            return new MeasureBlockReaderFromGraceGroup(graceGroupReader);
        }

        internal static IChord Cast(this IGraceChord graceChordReader, MeasureBlockReaderFromGraceGroup measureBlockReaderFromGraceGroup)
        {
            return new ChordReaderFromGraceChord(graceChordReader, measureBlockReaderFromGraceGroup);
        }

        internal static INote Cast(this IGraceNote noteReader, ChordReaderFromGraceChord chordReaderFromGraceChord)
        {
            return new NoteReaderFromGraceChord(noteReader, chordReaderFromGraceChord);
        }

        /// <summary>
        /// Create a tuplet from a grace group.
        /// </summary>
        /// <param name="graceGroupReader"></param>
        /// <returns></returns>
        public static Tuplet CreateTuplet(this IGraceGroup graceGroupReader)
        {
            var fallback = RythmicDuration.QuarterNote;
            return new(graceGroupReader.ImplyRythmicDuration(fallback), graceGroupReader.ReadChords().Select(c => graceGroupReader.ReadLayout().ChordDuration).ToArray());
        }

        /// <summary>
        /// Imply a rythmic duration from a grace group reader.
        /// If none can be created, a fallback will be used.
        /// </summary>
        /// <param name="graceGroupReader"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public static RythmicDuration ImplyRythmicDuration(this IGraceGroup graceGroupReader, RythmicDuration fallback)
        {
            if (RythmicDuration.TryConstruct(graceGroupReader.ImplyDuration(), out var rythmicDuration))
            {
                return rythmicDuration;
            }

            return fallback;
        }

        /// <summary>
        /// Imply a duration from a grace group.
        /// </summary>
        /// <param name="graceGroupReader"></param>
        /// <returns></returns>
        public static Duration ImplyDuration(this IGraceGroup graceGroupReader)
        {
            var duration = graceGroupReader.ReadLayout().ChordDuration * graceGroupReader.ReadChords().Count();
            return duration;
        }
    }
}
