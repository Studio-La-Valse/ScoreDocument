namespace StudioLaValse.ScoreDocument.Core.Primitives
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
    }

    /// <inheritdoc/>
    public interface IMeasureBlockChain<TMeasureBlock> : IMeasureBlockChain where TMeasureBlock : IMeasureBlock
    {

        /// <summary>
        /// Enumerates the measure blocks.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TMeasureBlock> ReadBlocks();
    }
}
