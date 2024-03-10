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


        public NullableTemplateProperty<string> AbbreviatedName { get; }
        public NullableTemplateProperty<string> DisplayName { get; set; }
        public TemplateProperty<int> NumberOfStaves { get; set; }


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

            DisplayName = new NullableTemplateProperty<string>(() => this.instrumentRibbon.Instrument.Name);
            AbbreviatedName = new NullableTemplateProperty<string>(() => CreateDefaultNickName(this.DisplayName.Value));
            NumberOfStaves = new TemplateProperty<int>(() => instrumentRibbon.Instrument.NumberOfStaves);
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
            copy.AbbreviatedName.Field = AbbreviatedName.Field;
            copy.DisplayName.Field = DisplayName.Field;
            copy.NumberOfStaves.Field = NumberOfStaves.Field;
            copy.Collapsed = Collapsed;
            return copy;
        }
    }
}