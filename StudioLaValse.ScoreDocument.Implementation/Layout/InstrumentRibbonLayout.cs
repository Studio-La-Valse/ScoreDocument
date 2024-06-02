using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class InstrumentRibbonLayout
    {
        public abstract ReferenceTemplateProperty<string> _AbbreviatedName { get; }
        public abstract ReferenceTemplateProperty<string> _DisplayName { get; }
        public abstract ValueTemplateProperty<int> _NumberOfStaves { get; }
        public abstract ValueTemplateProperty<bool> _Collapsed { get; }
        public abstract ValueTemplateProperty<double> _Scale { get; }


        public string AbbreviatedName { get => _AbbreviatedName.Value; set => _AbbreviatedName.Value = value; }
        public string DisplayName { get => _DisplayName.Value; set => _DisplayName.Value = value; }
        public int NumberOfStaves { get => _NumberOfStaves.Value; set => _NumberOfStaves.Value = value; }
        public bool Collapsed { get => _Collapsed.Value; set => _Collapsed.Value = value; }
        public string Name { get => _DisplayName.Value; set => _DisplayName.Value = value; }
        public double Scale { get => _Scale.Value; set => _Scale.Value = value; }

        public InstrumentRibbonLayout()
        {

        }

        public void Restore()
        {
            _AbbreviatedName.Reset();
            _DisplayName.Reset();
            _NumberOfStaves.Reset();
            _Collapsed.Reset();
            _Scale.Reset();
        }

        public void ApplyMemento(InstrumentRibbonLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _AbbreviatedName.Field = memento.AbbreviatedName;
            _DisplayName.Field = memento.DisplayName;
            _NumberOfStaves.Field = memento.NumberOfStaves;
            _Collapsed.Field = memento.Collapsed;
            _Scale.Field = memento.Scale;
        }
        public void ApplyMemento(InstrumentRibbonLayoutModel? memento)
        {
            ApplyMemento(memento as InstrumentRibbonLayoutMembers);
        }
    }

    public class AuthorInstrumentRibbonLayout : InstrumentRibbonLayout, IInstrumentRibbonLayout, ILayout<InstrumentRibbonLayoutMembers>
    {
        public override ReferenceTemplateProperty<string> _AbbreviatedName { get; }
        public override ReferenceTemplateProperty<string> _DisplayName { get; }
        public override ValueTemplateProperty<int> _NumberOfStaves { get; }
        public override ValueTemplateProperty<bool> _Collapsed { get; }
        public override ValueTemplateProperty<double> _Scale { get; }


        public AuthorInstrumentRibbonLayout(Instrument instrument, ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, Guid instrumentRibbonId)
        {
            _DisplayName = new ReferenceTemplateProperty<string>(() => instrument.Name);
            _NumberOfStaves = new ValueTemplateProperty<int>(() => instrument.NumberOfStaves);
            _Collapsed = new ValueTemplateProperty<bool>(() => false);
            _AbbreviatedName = new ReferenceTemplateProperty<string>(_DisplayName.Value.AbbreviateName);
            _Scale = new ValueTemplateProperty<double>(() => scoreDocumentStyleTemplate.InstrumentScales.TryGetValue(instrumentRibbonId, out var value) ? value : 1);
        }

        public InstrumentRibbonLayoutMembers GetMemento()
        {
            return new InstrumentRibbonLayoutMembers()
            {
                AbbreviatedName = _AbbreviatedName.Field,
                DisplayName = _DisplayName.Field,
                NumberOfStaves = _NumberOfStaves.Field,
                Collapsed = Collapsed,
                Scale = Scale
            };
        }
    }

    public class SecondaryInstrumentRibbonLayout : InstrumentRibbonLayout, IInstrumentRibbonLayout, ILayout<InstrumentRibbonLayoutModel>
    {
        public Guid Id { get; }
        public override ReferenceTemplateProperty<string> _AbbreviatedName { get; }
        public override ReferenceTemplateProperty<string> _DisplayName { get; }
        public override ValueTemplateProperty<int> _NumberOfStaves { get; }
        public override ValueTemplateProperty<bool> _Collapsed { get; }
        public override ValueTemplateProperty<double> _Scale { get; }

        public SecondaryInstrumentRibbonLayout(AuthorInstrumentRibbonLayout layout, Guid id)
        {
            Id = id;
            _DisplayName = new ReferenceTemplateProperty<string>(() => layout.Name);
            _NumberOfStaves = new ValueTemplateProperty<int>(() => layout.NumberOfStaves);
            _Collapsed = new ValueTemplateProperty<bool>(() => layout.Collapsed);
            _AbbreviatedName = new ReferenceTemplateProperty<string>(() => layout.AbbreviatedName);
            _Scale = new ValueTemplateProperty<double>(() => layout.Scale);

        }

        public InstrumentRibbonLayoutModel GetMemento()
        {
            return new InstrumentRibbonLayoutModel()
            {
                Id = Id,
                AbbreviatedName = _AbbreviatedName.Field,
                DisplayName = _DisplayName.Field,
                NumberOfStaves = _NumberOfStaves.Field,
                Collapsed = Collapsed,
                Scale = Scale
            };
        }
    }
}