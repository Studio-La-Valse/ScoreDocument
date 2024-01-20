namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// <see cref="IInstrumentMeasureReader"/> extensions.
    /// </summary>
    public static class RibbonMeasureReaderExtensions
    {
        /// <summary>
        /// Calculates the opening clef of the specified ribbon measure at the specified staff index.
        /// </summary>
        /// <param name="ribbonMeasure"></param>
        /// <param name="staffIndex"></param>
        /// <returns></returns>
        public static Clef OpeningClefAtOrDefault(this IInstrumentMeasureReader ribbonMeasure, int staffIndex)
        {
            foreach (var clefChange in ribbonMeasure.ReadLayout().ClefChanges.Where(c => c.Position.Decimal == 0))
            {
                if (clefChange.StaffIndex == staffIndex)
                {
                    return clefChange.Clef;
                }
            }

            var previousMeasure = ribbonMeasure;
            while (previousMeasure.TryReadPrevious(out previousMeasure))
            {
                foreach (var clefChange in previousMeasure.ReadLayout().ClefChanges.Reverse())
                {
                    if (clefChange.StaffIndex == staffIndex)
                    {
                        return clefChange.Clef;
                    }
                }
            }

            var spareClef =
                ribbonMeasure.Instrument.DefaultClefs.ElementAtOrDefault(staffIndex) ??
                ribbonMeasure.Instrument.DefaultClefs.Last();

            return spareClef;
        }
    }
}
