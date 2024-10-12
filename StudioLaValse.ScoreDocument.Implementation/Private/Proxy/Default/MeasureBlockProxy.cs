using StudioLaValse.ScoreDocument.Implementation.Private;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Models;
using StudioLaValse.ScoreDocument.Models.Base;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default
{
    internal class MeasureBlockProxy(MeasureBlock source, ILayoutSelector layoutSelector) : IMeasureBlock
    {
        private readonly MeasureBlock source = source;
        private readonly ILayoutSelector layoutSelector = layoutSelector;


        public InstrumentMeasure HostMeasure => source.RibbonMeasure;

        public IMeasureBlockLayout Layout => layoutSelector.MeasureBlockLayout(source);



        public RythmicDuration RythmicDuration => source.RythmicDuration;

        public Position Position => source.Position;

        public Tuplet Tuplet => source.Tuplet;

        public int Id => source.Id;



        public TemplateProperty<StemDirection> StemDirection => Layout.StemDirection;

        public TemplateProperty<double> StemLength => Layout.StemLength;

        public TemplateProperty<double> BeamAngle => Layout.BeamAngle;

        public ReadonlyTemplateProperty<double> BeamThickness => Layout.BeamThickness;

        public ReadonlyTemplateProperty<double> BeamSpacing => Layout.BeamSpacing;

        public TemplateProperty<double> Scale => Layout.Scale;


        public void AppendChord(RythmicDuration rythmicDuration, params Pitch[] pitches)
        {
            source.AppendChord(rythmicDuration, rebeam: true, pitches);
        }

        public void Splice(int index)
        {
            source.Splice(index);
        }

        public void Clear()
        {
            source.Clear();
        }

        public bool TryReadNext([NotNullWhen(true)] out IMeasureBlock? right)
        {
            right = null;
            if (source.TryReadNext(out var _right))
            {
                right = _right.Proxy(layoutSelector);
            }
            return right is not null;
        }

        public bool TryReadPrevious([NotNullWhen(true)] out IMeasureBlock? previous)
        {
            previous = null;
            if (source.TryReadNext(out var _prev))
            {
                previous = _prev.Proxy(layoutSelector);
            }
            return previous is not null;
        }

        public IEnumerable<IChord> ReadChords()
        {
            return source.GetChordsCore().Select(e => e.Proxy(layoutSelector));
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadChords();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Id == Id;
        }
    }
}
