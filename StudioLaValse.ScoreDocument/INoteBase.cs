using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// The base interface for notes. 
    /// May be implemented by a regular note and a grace note.
    /// </summary>
    public interface INoteBase : IHasLayout<INoteLayout>, IUniqueScoreElement, IScoreElement
    {
        /// <summary>
        /// The pitch of the note.
        /// </summary>
        Pitch Pitch { get; set; }

        /// <summary>
        /// Move this note to the specified staff index.
        /// </summary>
        /// <param name="staffIndex"></param>
        void SetStaffIndex(int staffIndex);

        /// <summary>
        /// Change the way accidentals are displayed for this note.
        /// </summary>
        /// <param name="display"></param>
        void SetForceAccidental(AccidentalDisplay display);
    }
}
