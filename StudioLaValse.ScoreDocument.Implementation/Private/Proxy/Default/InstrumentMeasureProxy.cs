using StudioLaValse.ScoreDocument.Implementation.Private;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Models;
using StudioLaValse.ScoreDocument.Models.Base;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default
{
    internal class InstrumentMeasureProxy : IInstrumentMeasure
    {
        private readonly InstrumentMeasure instrumentMeasure;
        private readonly ILayoutSelector layoutSelector;

        public IInstrumentMeasureLayout Layout => layoutSelector.InstrumentMeasureLayout(instrumentMeasure);

        public int MeasureIndex => instrumentMeasure.MeasureIndex;

        public int RibbonIndex => instrumentMeasure.RibbonIndex;

        public TimeSignature TimeSignature => instrumentMeasure.TimeSignature;

        public Instrument Instrument => instrumentMeasure.Instrument;

        public int Id => instrumentMeasure.Id;

        public ReadonlyTemplateProperty<double> PaddingLeft => Layout.PaddingLeft;

        public ReadonlyTemplateProperty<double> PaddingRight => Layout.PaddingRight;

        public ReadonlyTemplateProperty<KeySignature> KeySignature => Layout.KeySignature;

        public TemplateProperty<double?> PaddingBottom => Layout.PaddingBottom;

        public TemplateProperty<Visibility> Visibility => Layout.Visibility;

        public TemplateProperty<int?> NumberOfStaves => Layout.NumberOfStaves;

        public InstrumentMeasureProxy(InstrumentMeasure source, ILayoutSelector layoutSelector)
        {
            this.instrumentMeasure = source;
            this.layoutSelector = layoutSelector;
        }



        public IEnumerable<ClefChange> EnumerateClefChanges() => Layout.EnumerateClefChanges();

        public void AddVoice(int voice)
        {
            instrumentMeasure.AddVoice(voice);
        }

        public void RemoveVoice(int voice)
        {
            instrumentMeasure.RemoveVoice(voice);
        }

        public void Clear()
        {
            instrumentMeasure.Clear();
        }

        public void AddClefChange(ClefChange clefChange)
        {
            Layout.AddClefChange(clefChange);
        }

        public void RemoveClefChange(ClefChange clefChange)
        {
            Layout.RemoveClefChange(clefChange);
        }

        public void ClearClefChanges()
        {
            Layout.ClearClefChanges();
        }


        public void RequestPaddingBottom(int staffIndex, double? paddingBottom = null)
        {
            Layout.RequestPaddingBottom(staffIndex, paddingBottom);
        }

        public double? GetPaddingBottom(int staffIndex)
        {
            return Layout.GetPaddingBottom(staffIndex);
        }

        public IMeasureBlockChain ReadBlockChainAt(int voice)
        {
            return instrumentMeasure.GetBlockChainOrThrowCore(voice).Proxy(layoutSelector);
        }

        public bool TryReadPrevious([NotNullWhen(true)] out IInstrumentMeasure? previous)
        {
            _ = instrumentMeasure.TryReadPrevious(out var _previous);
            previous = _previous?.Proxy(layoutSelector);
            return previous != null;
        }

        public bool TryReadNext([NotNullWhen(true)] out IInstrumentMeasure? next)
        {
            instrumentMeasure.TryReadNext(out var _next);
            next = _next?.Proxy(layoutSelector);
            return next != null;
        }

        public IEnumerable<int> ReadVoices()
        {
            return instrumentMeasure.EnumerateVoices();
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadVoices().Select(ReadBlockChainAt).SelectMany(e => e.ReadBlocks());
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return other is not null && other.Id == Id;
        }
    }
}
