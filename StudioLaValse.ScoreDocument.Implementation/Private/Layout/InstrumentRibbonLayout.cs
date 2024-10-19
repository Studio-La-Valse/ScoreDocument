using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal abstract class InstrumentRibbonLayout : IInstrumentRibbonLayout
    {
        public abstract ReferenceTemplateProperty<string> _AbbreviatedName { get; }
        public abstract ReferenceTemplateProperty<string> _DisplayName { get; }
        public abstract ValueTemplateProperty<int> _NumberOfStaves { get; }
        public abstract ValueTemplateProperty<Visibility> _Collapsed { get; }
        public abstract ValueTemplateProperty<double> _Scale { get; }


        public TemplateProperty<string> DisplayName => _DisplayName;
        public TemplateProperty<string> AbbreviatedName => _AbbreviatedName;
        public TemplateProperty<Visibility> Visibility => _Collapsed;
        public TemplateProperty<int> NumberOfStaves => _NumberOfStaves;


        public TemplateProperty<double> Scale { get; }


        public InstrumentRibbonLayout(UserScoreDocumentLayout userScoreDocumentLayout)
        {
            double defaultScaleGetter() => _Scale.Value;
            double parentScaleGetter() => userScoreDocumentLayout.Scale.Value;
            double scaleAccumulator(double first, double second) => first * second;
            Scale = new AccumulativeValueTemplateProperty<double>(defaultScaleGetter, parentScaleGetter, scaleAccumulator);
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
            _Collapsed.Field = memento.Visibility?.ConvertVisibility();
            _Scale.Field = memento.Scale;
        }
        public void ApplyMemento(InstrumentRibbonLayoutModel? memento)
        {
            ApplyMemento(memento as InstrumentRibbonLayoutMembers);
        }
    }

    internal class AuthorInstrumentRibbonLayout : InstrumentRibbonLayout
    {
        public override ReferenceTemplateProperty<string> _AbbreviatedName { get; }
        public override ReferenceTemplateProperty<string> _DisplayName { get; }
        public override ValueTemplateProperty<int> _NumberOfStaves { get; }
        public override ValueTemplateProperty<Visibility> _Collapsed { get; }
        public override ValueTemplateProperty<double> _Scale { get; }


        public AuthorInstrumentRibbonLayout(Instrument instrument, ScoreDocumentStyleTemplate scoreDocumentStyleTemplate, Guid instrumentRibbonId, UserScoreDocumentLayout userScoreDocumentLayout) : base(userScoreDocumentLayout)
        {
            _DisplayName = new ReferenceTemplateProperty<string>(() => instrument.Name);
            _NumberOfStaves = new ValueTemplateProperty<int>(() => instrument.NumberOfStaves);
            _Collapsed = new ValueTemplateProperty<Visibility>(() => StudioLaValse.ScoreDocument.Layout.Visibility.Visible);
            _AbbreviatedName = new ReferenceTemplateProperty<string>(() => _DisplayName.Value.AbbreviateName());
            _Scale = new ValueTemplateProperty<double>(() => scoreDocumentStyleTemplate.InstrumentScales.TryGetValue(instrumentRibbonId, out var value) ? value : 1);
        }

        public InstrumentRibbonLayoutMembers GetMemento()
        {
            return new InstrumentRibbonLayoutMembers()
            {
                AbbreviatedName = _AbbreviatedName.Field,
                DisplayName = _DisplayName.Field,
                NumberOfStaves = _NumberOfStaves.Field,
                Visibility = _Collapsed.Field?.ConvertVisibility(),
                Scale = _Scale.Field
            };
        }
    }

    internal class UserInstrumentRibbonLayout : InstrumentRibbonLayout
    {
        public Guid Id { get; }
        public override ReferenceTemplateProperty<string> _AbbreviatedName { get; }
        public override ReferenceTemplateProperty<string> _DisplayName { get; }
        public override ValueTemplateProperty<int> _NumberOfStaves { get; }
        public override ValueTemplateProperty<Visibility> _Collapsed { get; }
        public override ValueTemplateProperty<double> _Scale { get; }

        public UserInstrumentRibbonLayout(AuthorInstrumentRibbonLayout layout, Guid id, UserScoreDocumentLayout userScoreDocumentLayout) : base(userScoreDocumentLayout)
        {
            Id = id;
            _DisplayName = new ReferenceTemplateProperty<string>(() => layout._DisplayName);
            _NumberOfStaves = new ValueTemplateProperty<int>(() => layout._NumberOfStaves);
            _Collapsed = new ValueTemplateProperty<Visibility>(() => layout._Collapsed);
            _AbbreviatedName = new ReferenceTemplateProperty<string>(() => layout._AbbreviatedName);
            _Scale = new ValueTemplateProperty<double>(() => layout._Scale);
        }
    }
}