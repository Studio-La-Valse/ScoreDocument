using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout.Extensions
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
            var index = 0;
            foreach (var staff in staffGroup.EnumerateStaves(numberOfStaves))
            {
                var clef =
                    staffGroup.Instrument.DefaultClefs.ElementAtOrDefault(staff.IndexInStaffGroup) ??
                    staffGroup.Instrument.DefaultClefs.Last();

                yield return (index, clef);
                index++;
            }
        }

        /// <summary>
        /// Calculates the height of a line, assuming the staff starts at the specified canvas top.
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="staffCanvasTop"></param>
        /// <param name="line"></param>
        /// <param name="globalLineSpacing"></param>
        /// <returns></returns>
        public static double HeightFromLineIndex(this IStaffReader staff, double staffCanvasTop, int line, double globalLineSpacing, double scoreScale, double instrumentScale)
        {
            return staffCanvasTop + (line * (globalLineSpacing * (scoreScale * instrumentScale) / 2));
        }

        /// <summary>
        /// Calculate the total height of the staff.
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="globalLineSpacing"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffReader staff, double globalLineSpacing, double scoreScale, double instrumentScale)
        {
            var staffHeight = 4 * (globalLineSpacing * (scoreScale * instrumentScale));

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
        /// <param name="scoreLayoutDictionary"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static double HeightOnCanvas(this IStaffGroupReader staffGroup, double canvasTopStaffGroup, int staffIndex, int lineIndex, double globalLineSpacing, IScoreDocumentLayout scoreLayoutDictionary)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(staffIndex, nameof(staffIndex));

            var scoreLayout = scoreLayoutDictionary.DocumentLayout();
            var scoreScale = scoreLayout.Scale;
            var instrumentScale = 1d;
            if (scoreLayout.InstrumentScales.TryGetValue(staffGroup.Instrument, out var value))
            {
                instrumentScale = value;
            }

            foreach (var staff in staffGroup.EnumerateStaves(staffIndex))
            {
                var staffLayout = scoreLayoutDictionary.StaffLayout(staff);
                canvasTopStaffGroup += staff.CalculateHeight(globalLineSpacing, scoreScale, instrumentScale);
                canvasTopStaffGroup += staffLayout.DistanceToNext;
            }

            var _staff = staffGroup.EnumerateStaves(staffIndex + 1).ElementAt(staffIndex);
            var canvasTop = _staff.HeightFromLineIndex(canvasTopStaffGroup, lineIndex, globalLineSpacing, scoreScale, instrumentScale);
            return canvasTop;
        }



        /// <summary>
        /// Calculate the total height of the staff group.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="scoreLayoutDictionary"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffGroupReader staffGroup, double globalLineSpacing, IScoreDocumentLayout scoreLayoutDictionary)
        {
            var height = 0d;
            var groupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);
            if (groupLayout.Collapsed)
            {
                return height;
            }

            var scoreLayout = scoreLayoutDictionary.DocumentLayout();
            var scoreScale = scoreLayout.Scale;
            var instrumentScale = 1d;
            if (scoreLayout.InstrumentScales.TryGetValue(staffGroup.Instrument, out var value))
            {
                instrumentScale = value;
            }

            var lastStaffSpacing = 0d;
            foreach (var staff in staffGroup.EnumerateStaves(groupLayout.NumberOfStaves))
            {
                var staffHeight = staff.CalculateHeight(globalLineSpacing, scoreScale, instrumentScale);
                var staffSpacing = scoreLayoutDictionary.StaffLayout(staff).DistanceToNext;
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
        /// <param name="scoreLayoutDictionary"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffSystemReader staffSystem, double lineSpacing, IScoreDocumentLayout scoreLayoutDictionary)
        {
            var height = 0d;
            var lastStafGroupSpacing = 0d;
            foreach (var staffGroup in staffSystem.EnumerateStaffGroups())
            {
                var staffGroupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);
                var staffGroupHeight = staffGroup.CalculateHeight(lineSpacing, scoreLayoutDictionary);
                height += staffGroupHeight;

                lastStafGroupSpacing = staffGroupLayout.DistanceToNext;
                height += lastStafGroupSpacing;
            }
            height -= lastStafGroupSpacing;
            return height;
        }
    }
}
