namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// The interface for a measure block chain reader.
    /// </summary>
    public interface IMeasureBlockChainReader : IMeasureBlockChain
    {
        /// <summary>
        /// The the blocks in the chain.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IMeasureBlockReader> ReadBlocks();
    }
}
