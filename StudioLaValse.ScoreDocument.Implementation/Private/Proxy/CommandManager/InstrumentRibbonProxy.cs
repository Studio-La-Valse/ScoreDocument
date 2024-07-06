using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager
{
    internal class InstrumentRibbonProxy : IInstrumentRibbon
    {
        private readonly InstrumentRibbon source;
        private readonly ICommandManager commandManager;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;
        private readonly ILayoutSelector layoutSelector;

        public int IndexInScore => source.IndexInScore;

        public Instrument Instrument => source.Instrument;

        public int Id => source.Id;

        public IInstrumentRibbonLayout Layout => layoutSelector.InstrumentRibbonLayout(source);

        public TemplateProperty<string> DisplayName => Layout.DisplayName.WithRerender(notifyEntityChanged, source.HostScoreDocument, commandManager);

        public TemplateProperty<string> AbbreviatedName => Layout.DisplayName.WithRerender(notifyEntityChanged, source.HostScoreDocument, commandManager);

        public TemplateProperty<bool> Collapsed => Layout.Collapsed.WithRerender(notifyEntityChanged, source.HostScoreDocument, commandManager);

        public TemplateProperty<int> NumberOfStaves => Layout.NumberOfStaves.WithRerender(notifyEntityChanged, source.HostScoreDocument, commandManager);

        public TemplateProperty<double> Scale => Layout.Scale.WithRerender(notifyEntityChanged, source.HostScoreDocument, commandManager);



        public InstrumentRibbonProxy(InstrumentRibbon source, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            this.source = source;
            this.commandManager = commandManager;
            this.notifyEntityChanged = notifyEntityChanged;
            this.layoutSelector = layoutSelector;
        }




        public IInstrumentMeasure ReadMeasure(int measureIndex)
        {
            return source.GetMeasureCore(measureIndex).Proxy(commandManager, notifyEntityChanged, layoutSelector);
        }

        public IEnumerable<IInstrumentMeasure> ReadMeasures()
        {
            return source.EnumerateMeasuresCore().Select(e => e.Proxy(commandManager, notifyEntityChanged, layoutSelector));
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
