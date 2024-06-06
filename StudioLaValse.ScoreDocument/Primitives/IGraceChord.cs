namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a grace chord.
    /// </summary>
    public interface IGraceChord : IChord, IScoreElement
    {

    }

    /// <summary>
    /// Represents a grace chord.
    /// </summary>
    /// <typeparam name="TNote"></typeparam>
    public interface IGraceChord<TNote> : IGraceChord, IChord<TNote>
        where TNote : IGraceNote
    {

    }

    /// <summary>
    /// Represents a grace chord.
    /// </summary>
    /// <typeparam name="TNote"></typeparam>
    /// /// <typeparam name="TGrace"></typeparam>
    public interface IGraceChord<TNote, TGrace> : IGraceChord<TNote>, IChord<TNote, TGrace>
        where TNote : IGraceNote
        where TGrace : IGraceGroup
    {

    }
}