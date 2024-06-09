using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IInstrumentRibbonLayout
    {
        string AbbreviatedName { get; }
        bool Collapsed { get; }
        string DisplayName { get; }
        int NumberOfStaves { get; }
        double Scale { get; }
    }

    public static class InstrumentRibbonLayoutExtensions
    {
        public static string AbbreviateName(this IInstrumentRibbonLayout layout)
        {
            var name = layout.DisplayName;
            return AbbreviateName(name);
        }
        public static string AbbreviateName(this string name)
        {
            return string.IsNullOrWhiteSpace(name) ? "" : name.Length == 1 ? string.Concat(name.AsSpan(0, 1), ".") : string.Concat(name.AsSpan(0, 2), ".");
        }
    }
}