using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// Extensions for instrument ribbon layout.
    /// </summary>
    public static class InstrumentRibbonLayoutExtensions
    {
        /// <summary>
        /// Abbreviate the default name.
        /// </summary>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static string AbbreviateName(this IInstrumentRibbonLayout layout)
        {
            var name = layout.DisplayName;
            return name.AbbreviateName();
        }

        /// <summary>
        /// Abbreviate the default name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string AbbreviateName(this string name)
        {
            return string.IsNullOrWhiteSpace(name) ? "" : name.Length == 1 ? string.Concat(name.AsSpan(0, 1), ".") : string.Concat(name.AsSpan(0, 2), ".");
        }
    }
}