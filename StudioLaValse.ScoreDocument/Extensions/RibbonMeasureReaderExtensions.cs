using StudioLaValse.ScoreDocument.Private;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Extensions
{
    public static class RibbonMeasureReaderExtensions
    {
        public static Clef OpeningClefAtOrDefault(this IRibbonMeasureReader ribbonMeasure, int staffIndex)
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
