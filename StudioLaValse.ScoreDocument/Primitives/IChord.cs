using StudioLaValse.ScoreDocument.Core;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// A chord-like note container with a preceding grace group.
    /// </summary>
    /// <typeparam name="TNote"></typeparam>
    /// <typeparam name="TGraceGroup"></typeparam>
    public interface IChord<TNote, TGraceGroup> : INoteContainer<TNote, TGraceGroup>, IPositionElement, IUniqueScoreElement
    {
        
    }
}
