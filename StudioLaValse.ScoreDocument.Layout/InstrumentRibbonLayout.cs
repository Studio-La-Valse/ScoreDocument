using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of an instrument ribbon.
    /// </summary>
    public class InstrumentRibbonLayout : ILayoutElement<InstrumentRibbonLayout>
    {
        private readonly InstrumentRibbonStyleTemplate styleTemplate;
        private readonly IInstrumentRibbon instrumentRibbon;
        private readonly NullableTemplateProperty<string> abbreviatedName;
        private NullableTemplateProperty<string> displayName;
        private TemplateProperty<int> numberOfStaves;

        public string AbbreviatedName { get => abbreviatedName.Value; set => abbreviatedName.Value = value; }
        public string DisplayName { get => displayName.Value; set => displayName.Value = value; }
        public int NumberOfStaves { get => numberOfStaves.Value; set => numberOfStaves.Value = value; }


        public bool Collapsed { get; set; }




        /// <summary>
        /// Create a new instrument ribbon layout.
        /// </summary>
        /// <param name="styleTemplate"></param>
        /// <param name="instrumentRibbon"></param>
        public InstrumentRibbonLayout(InstrumentRibbonStyleTemplate styleTemplate, IInstrumentRibbon instrumentRibbon)
        {
            this.styleTemplate = styleTemplate;
            this.instrumentRibbon = instrumentRibbon;

            displayName = new NullableTemplateProperty<string>(() => this.instrumentRibbon.Instrument.Name);
            abbreviatedName = new NullableTemplateProperty<string>(() => CreateDefaultNickName(displayName.Value));
            numberOfStaves = new TemplateProperty<int>(() => instrumentRibbon.Instrument.NumberOfStaves);
            Collapsed = false;
        }

        /// <summary>
        /// Create the default nickname.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreateDefaultNickName(string name)
        {
            return string.IsNullOrWhiteSpace(name)
                ? ""
                : name.Length == 1 ? string.Concat(name.AsSpan(0, 1), ".") : string.Concat(name.AsSpan(0, 2), ".");
        }

        /// <inheritdoc/>
        public InstrumentRibbonLayout Copy()
        {
            var copy = new InstrumentRibbonLayout(styleTemplate, instrumentRibbon);
            copy.abbreviatedName.Field = abbreviatedName.Field;
            copy.displayName.Field = displayName.Field;
            copy.numberOfStaves.Field = numberOfStaves.Field;
            copy.Collapsed = Collapsed;
            return copy;
        }
    }
}