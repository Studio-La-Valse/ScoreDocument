using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Core.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents an instrument measure reader.
    /// </summary>
    public interface IInstrumentMeasureReader : IInstrumentMeasure<IMeasureBlockChainReader, IInstrumentMeasureReader>, IUniqueScoreElement
    {

    }
}
