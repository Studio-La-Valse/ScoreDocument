using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Reader;
using StudioLaValse.ScoreDocument.Reader.Extensions;
using StudioLaValse.ScoreDocument.Reader.Private;

namespace StudioLaValse.ScoreDocument.Reader.Extensions
{
    /// <summary>
    /// Extensions to staff (-group and -system) readers.
    /// </summary>
    public static class StaffGroupReaderExtensions
    {
        /// <summary>
        /// Enumerates the default opening clefs.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="numberOfStaves"></param>
        /// <returns></returns>
        public static IEnumerable<(int, Clef)> EnumerateDefaultInstrumentClefs(this IStaffGroupReader staffGroup, int numberOfStaves)
        {
            for (var i = 0; i < numberOfStaves; i++)
            {
                var clef =
                    staffGroup.Instrument.DefaultClefs.ElementAtOrDefault(i) ??
                    staffGroup.Instrument.DefaultClefs.Last();

                yield return (i, clef);
            }
        }

        /// <summary>
        /// Calculates the height of a line, assuming the staff starts at the specified canvas top.
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="staffCanvasTop"></param>
        /// <param name="line"></param>
        /// <param name="globalLineSpacing"></param>
        /// <param name="scoreScale"></param>
        /// <param name="instrumentScale"></param>
        /// <returns></returns>
        public static double DistanceFromTop(this IStaffReader staff, int line, double globalLineSpacing, double scoreScale, double instrumentScale)
        {
            return line * (globalLineSpacing * (scoreScale * instrumentScale) / 2);
        }

        /// <summary>
        /// Calculate the total height of the staff.
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="globalLineSpacing"></param>
        /// <param name="scoreScale"></param>
        /// <param name="instrumentScale"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffReader staff, double globalLineSpacing, double scoreScale, double instrumentScale)
        {
            var staffHeight = 4 * globalLineSpacing * scoreScale * instrumentScale;

            return staffHeight;
        }


        /// <summary>
        /// Calculates the height of a note in a staff group, assuming the staff group starts a the specified canvas top.
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the staff index specified in the note layout is not present in the staff group.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="canvasTopStaffGroup"></param>
        /// <param name="staffIndex"></param>
        /// <param name="lineIndex"></param>
        /// <param name="globalLineSpacing"></param>
        /// <param name="scoreLayout"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static double DistanceFromTop(this IStaffGroupReader staffGroup, int staffIndex, int lineIndex, double globalLineSpacing, IScoreDocumentLayout scoreLayout)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(staffIndex, nameof(staffIndex));

            var scoreScale = scoreLayout.Scale;
            var instrumentScale = staffGroup.InstrumentRibbon.ReadLayout().Scale;
            var canvasTopStaffGroup = 0d;

            foreach (var staff in staffGroup.EnumerateStaves().Take(staffIndex))
            {
                var staffLayout = staff.ReadLayout();
                canvasTopStaffGroup += staff.CalculateHeight(globalLineSpacing, scoreScale, instrumentScale);
                canvasTopStaffGroup += staffLayout.DistanceToNext * scoreScale;
            }

            var _staff = staffGroup.EnumerateStaves().ElementAt(staffIndex);
            var canvasTop = canvasTopStaffGroup + _staff.DistanceFromTop(lineIndex, globalLineSpacing, scoreScale, instrumentScale);
            return canvasTop;
        }



        /// <summary>
        /// Calculate the total height of the staff group.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="globalLineSpacing"></param>
        /// <param name="scoreLayout"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffGroupReader staffGroup, double globalLineSpacing, IScoreDocumentLayout scoreLayout)
        {
            var height = 0d;
            var groupLayout = staffGroup.ReadLayout();
            if (groupLayout.Collapsed)
            {
                return height;
            }

            var scoreScale = scoreLayout.Scale;
            var instrumentScale = staffGroup.InstrumentRibbon.ReadLayout().Scale;

            var lastStaffSpacing = 0d;
            foreach (var staff in staffGroup.EnumerateStaves())
            {
                var staffHeight = staff.CalculateHeight(globalLineSpacing, scoreScale, instrumentScale);
                var staffSpacing = staff.ReadLayout().DistanceToNext * scoreScale;
                height += staffHeight;
                height += staffSpacing;
                lastStaffSpacing = staffSpacing;
            }

            height -= lastStaffSpacing;

            return height;
        }

        /// <summary>
        /// Calculate the total height of the staff system.
        /// </summary>
        /// <param name="staffSystem"></param>
        /// <param name="lineSpacing"></param>
        /// <param name="scoreLayoutDictionary"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffSystemReader staffSystem, double lineSpacing, IScoreDocumentLayout scoreLayoutDictionary)
        {
            var height = 0d;
            var lastStafGroupSpacing = 0d;
            var scoreScale = scoreLayoutDictionary.Scale;
            foreach (var staffGroup in staffSystem.EnumerateStaffGroups())
            {
                var staffGroupLayout = staffGroup.ReadLayout();
                var staffGroupHeight = staffGroup.CalculateHeight(lineSpacing, scoreLayoutDictionary);
                height += staffGroupHeight;

                lastStafGroupSpacing = staffGroupLayout.DistanceToNext * scoreScale;
                height += lastStafGroupSpacing;
            }
            height -= lastStafGroupSpacing;
            return height;
        }
    }
}
