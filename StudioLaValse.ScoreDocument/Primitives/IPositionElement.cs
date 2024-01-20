namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a 
    /// </summary>
    public interface IPositionElement : IUniqueScoreElement
    {
        /// <summary>
        /// Specifies whether the element is a grace element or not.
        /// </summary>
        bool Grace { get; }
        /// <summary>
        /// The position of the element in the measure.
        /// </summary>
        Position Position { get; }
        /// <summary>
        /// The rythmically valid duration of the element.
        /// </summary>
        RythmicDuration RythmicDuration { get; }
        /// <summary>
        /// The tuplet information of the position element.
        /// </summary>
        Tuplet Tuplet { get; }
    }
}
