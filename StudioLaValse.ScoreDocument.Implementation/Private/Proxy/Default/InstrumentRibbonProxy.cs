using StudioLaValse.ScoreDocument.Implementation.Private;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default
{
    internal class InstrumentRibbonProxy : IInstrumentRibbon
    {
        private readonly InstrumentRibbon source;
        private readonly ILayoutSelector layoutSelector;

        public int IndexInScore => source.IndexInScore;

        public Instrument Instrument => source.Instrument;

        public int Id => source.Id;

        public IInstrumentRibbonLayout Layout => layoutSelector.InstrumentRibbonLayout(source);

        public TemplateProperty<string> DisplayName => Layout.DisplayName;

        public TemplateProperty<string> AbbreviatedName => Layout.DisplayName;

        public TemplateProperty<bool> Collapsed => Layout.Collapsed;

        public TemplateProperty<int> NumberOfStaves => Layout.NumberOfStaves;

        public TemplateProperty<double> Scale => Layout.Scale;



        public InstrumentRibbonProxy(InstrumentRibbon source, ILayoutSelector layoutSelector)
        {
            this.source = source;
            this.layoutSelector = layoutSelector;
        }




        public IInstrumentMeasure ReadMeasure(int measureIndex)
        {
            return source.GetMeasureCore(measureIndex).Proxy(layoutSelector);
        }

        public IEnumerable<IInstrumentMeasure> ReadMeasures()
        {
            return source.EnumerateMeasuresCore().Select(e => e.Proxy(layoutSelector));
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadMeasures();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return other is not null && other.Id == Id;
        }
    }
}
