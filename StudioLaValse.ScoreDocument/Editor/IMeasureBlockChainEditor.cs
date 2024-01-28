namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// The interface for a measure block chain editor.
    /// </summary>
    public interface IMeasureBlockChainEditor : IMeasureBlockChain
    {
        /// <summary>
        /// Clear all content from the block chain.
        /// </summary>
        void Clear();
        /// <summary>
        /// Edit the blocks in the measure.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IMeasureBlockEditor> EditBlocks();
        /// <summary>
        /// Divide the block chain in intervals with the specified stepsizes. 
        /// Note that the sum of the steps has to be equal to the numinator of the parent measure.
        /// For example, if the containing time signature is 7/8, then valid stepsizes are: (2, 3, 2), (1, 1, 2, 3), etc. 
        /// Note that each of the steps will be evaluated to be relatively equal to a valid rythmic duration. 
        /// For example in a 7/8 time signature, a stepsize of 5 cannot be resolve, because 5/8ths cannot be resolved to a valid rythmic duration.
        /// If this block chain already has blocks that contain measure elements, an exception will be thrown.
        /// </summary>
        /// <param name="steps"></param>
        void Divide(params int[] steps);
        /// <summary>
        /// Divide the block chain into equal lenght segments. Note that all the rules of <see cref="Divide(int[])"/> will be taken into account.
        /// </summary>
        /// <param name="number"></param>
        void DivideEqual(int number);
        /// <summary>
        /// Prepend a block to the measure blocks.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="grace"></param>
        void Prepend(RythmicDuration duration, bool grace);
        /// <summary>
        /// Appends a measure block after the blocks.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="grace"></param>
        void Append(RythmicDuration duration, bool grace);
        /// <summary>
        /// Inserts a measure block at the specified position.
        /// Of no block in the block chains starts at this position, an exception will be thrown.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="duration"></param>
        /// <param name="grace"></param>
        void Insert(Position position, RythmicDuration duration, bool grace);
    }
}
