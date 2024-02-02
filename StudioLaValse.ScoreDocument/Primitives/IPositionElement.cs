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
        /// The rythmically valid duration of the element. This is the total unaltered duration of the block.
        /// </summary>
        RythmicDuration RythmicDuration { get; }
        /// <summary>
        /// The tuplet information of the position element. The actual duration of each of the elements contained in this block will change based on this tuplet.
        /// </summary>
        Tuplet Tuplet { get; }
    }
}
