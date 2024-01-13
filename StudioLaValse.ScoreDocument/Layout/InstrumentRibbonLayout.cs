namespace StudioLaValse.ScoreDocument.Layout
{
    public class InstrumentRibbonLayout : IInstrumentRibbonLayout
    {
        public string AbbreviatedName { get; }
        public bool Collapsed { get; }
        public string DisplayName { get; }
        public int NumberOfStaves { get; }

        public InstrumentRibbonLayout(string name, string abbreviatedname, int numberOfStaves, bool collapsed = false)
        {
            DisplayName = name;
            AbbreviatedName = abbreviatedname;
            NumberOfStaves = numberOfStaves;
            Collapsed = collapsed;
        }

        public InstrumentRibbonLayout(string name, int numberOfStaves, bool collapsed = false) : this(name, CreateDefaultNickName(name), numberOfStaves, collapsed)
        {

        }

        public InstrumentRibbonLayout(Instrument instrument, bool collapsed = false) : this(instrument.Name, instrument.NumberOfStaves, collapsed)
        {

        }

        public InstrumentRibbonLayout(Instrument instrument, string nickname, bool collapsed = false) : this(instrument.Name, nickname, instrument.NumberOfStaves, collapsed)
        {

        }

        public static string CreateDefaultNickName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "";
            }
            else if (name.Length == 1)
            {
                return string.Concat(name.AsSpan(0, 1), ".");
            }
            else
            {
                return string.Concat(name.AsSpan(0, 2), ".");
            }
        }

        public IInstrumentRibbonLayout Copy()
        {
            return new InstrumentRibbonLayout(DisplayName, AbbreviatedName, NumberOfStaves, Collapsed);
        }
    }
}