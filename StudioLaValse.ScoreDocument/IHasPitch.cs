namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// The base interface for notes. 
    /// May be implemented by a regular note and a grace note.
    /// </summary>
    public interface IHasPitch 
    {
        /// <summary>
        /// The pitch of the note.
        /// </summary>
        Pitch Pitch { get; set; }
    }
}
