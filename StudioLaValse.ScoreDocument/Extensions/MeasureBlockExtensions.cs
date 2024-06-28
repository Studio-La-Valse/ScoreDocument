namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// Measure block extensions.
    /// </summary>
    public static class MeasureBlockExtensions
    {
        /// <summary>
        /// Read all notes in the measure block.
        /// </summary>
        /// <param name="measureBlock"></param>
        /// <returns></returns>
        public static IEnumerable<INote> ReadNotes(this IMeasureBlock measureBlock)
        {
            foreach(var chord in measureBlock.ReadChords())
            {
                foreach(var note in chord.ReadNotes())
                {
                    yield return note;
                }
            }
        }
    }
}
