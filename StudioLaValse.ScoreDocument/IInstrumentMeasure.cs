using StudioLaValse.ScoreDocument.Layout;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents an instrument measure editor.
    /// </summary>
    public interface IInstrumentMeasure : IHasLayout<IInstrumentMeasureLayout>, IScoreElement, IUniqueScoreElement
    {
        /// <summary>
        /// The measure index of the host instrument ribbon. 
        /// This value is equal to the measure index of the host score measure.
        /// </summary>
        int MeasureIndex { get; }
        /// <summary>
        /// The index of the instrument ribbon in the score.
        /// </summary>
        int RibbonIndex { get; }
        /// <summary>
        /// The instrument of the measure.
        /// </summary>
        Instrument Instrument { get; }
        /// <summary>
        /// The time signature of the measure.
        /// </summary>
        TimeSignature TimeSignature { get; }
        /// <summary>
        /// Enumemerate the voices in the measure.
        /// </summary>
        /// <returns></returns>
        IEnumerable<int> ReadVoices();

        /// <summary>
        /// Read the blockchain for the specified voice.
        /// </summary>
        /// <param name="voice"></param>
        /// <returns></returns>
        IMeasureBlockChain ReadBlockChainAt(int voice);

        /// <summary>
        /// Tries to read the previous instrument measure.
        /// </summary>
        /// <param name="previous"></param>
        /// <returns><see langword="true"/> if it exists.</returns>
        bool TryReadPrevious([NotNullWhen(true)] out IInstrumentMeasure? previous);
        /// <summary>
        /// Tries to read the next instrument measure.
        /// </summary>
        /// <param name="next"></param>
        /// <returns><see langword="true"/> if it exists.</returns>
        bool TryReadNext([NotNullWhen(true)] out IInstrumentMeasure? next);

        /// <summary>
        /// Clears the content of the instrument measure.
        /// </summary>
        void Clear();
        /// <summary>
        /// Clears the content of one voice in the measure.
        /// </summary>
        /// <param name="voice"></param>
        void RemoveVoice(int voice);
        /// <summary>
        /// Adds a voice to the measure. Does nothing if the voice already exists.
        /// </summary>
        /// <param name="voice"></param>
        void AddVoice(int voice);


        /// <summary>
        /// Add a clefchange to this instrument measure.
        /// </summary>
        /// <param name="clefChange"></param>
        void AddClefChange(ClefChange clefChange);
        /// <summary>
        /// Remove a clef change from the instrument measure.
        /// </summary>
        /// <param name="clefChange"></param>
        void RemoveClefChange(ClefChange clefChange);


        /// <summary>
        /// Request a padding at the bottom of this score measure. 
        /// If no other measures in the staff group have made a request, the provided value will be used in the staff group.
        /// If other measures have made a request, the highest value will be used.
        /// When the specified value is null, the request is removed from the staff group.
        /// </summary>
        /// <param name="staffIndex"></param>
        /// <param name="paddingBottom"></param>
        void RequestPaddingBottom(int staffIndex, double? paddingBottom = null);
        /// <summary>
        /// Request the staff group containing this measure to be collapsed. 
        /// </summary>
        /// <param name="collapse"></param>
        void RequestCollapsed(bool collapse);
        /// <summary>
        /// Request the staff group containing to display the specified amount of saves.
        /// If no other instrument measures have made the same request, this value will be displayed.
        /// If other instrument measures have made this request, the highest value will be used.
        /// If no requests have been made in the same staff group, the default amount of staves will be displayed.
        /// Specify null to restore the requested number of staves.
        /// </summary>
        /// <param name="numberOfStaves"></param>
        void RequestNumberOfStaves(int? numberOfStaves = null);
    }
}
