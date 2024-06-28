using StudioLaValse.ScoreDocument.Models.Base;
using StudioLaValse.ScoreDocument.Templates;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class InstrumentRibbonLayout : IInstrumentRibbonLayout
    {
        public abstract ReferenceTemplateProperty<string> _AbbreviatedName { get; }
        public abstract ReferenceTemplateProperty<string> _DisplayName { get; }
        public abstract ValueTemplateProperty<int> _NumberOfStaves { get; }
        public abstract ValueTemplateProperty<bool> _Collapsed { get; }
        public abstract ValueTemplateProperty<double> _Scale { get; }


        public TemplateProperty<string> DisplayName => _DisplayName;
        public TemplateProperty<string> AbbreviatedName => _AbbreviatedName;
        public TemplateProperty<bool> Collapsed => _Collapsed;
        public TemplateProperty<int> NumberOfStaves => _NumberOfStaves;
        public TemplateProperty<double> Scale => _Scale;


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

        public void ResetDisplayName()
        {
            _DisplayName.Reset();
        }

        public void ResetAbbreviatedName()
        {
            _AbbreviatedName.Reset();
        }

        public void ResetCollapsed()
        {
            _Collapsed.Reset();
        }

        public void ResetNumberOfStaves()
        {
            _NumberOfStaves.Reset();
        }

        public void ResetScale()
        {
            _Scale.Reset();
        }
    }

    public class AuthorInstrumentRibbonLayout : InstrumentRibbonLayout, ILayout<InstrumentRibbonLayoutMembers>
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
            _DisplayName = new ReferenceTemplateProperty<string>(() => layout.DisplayName);
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