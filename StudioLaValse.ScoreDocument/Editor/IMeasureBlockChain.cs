namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// The interface for a measure block chain primitive.
    /// </summary>
    public interface IMeasureBlockChain
    {
        /// <summary>
        /// The voice of the block chain.
        /// </summary>
        int Voice { get; }
        /// <summary>
        /// Enumerates the measure blocks.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IMeasureBlock> EnumerateBlocks();
    }
}
