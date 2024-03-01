using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.ScoreElements;
using System.Diagnostics;

namespace StudioLaValse.ScoreDocument.Drawable.Extensions
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
        public static IEnumerable<(int, Clef)> EnumerateDefaultInstrumentClefs(this IStaffGroup staffGroup, int numberOfStaves)
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
        /// <param name="scoreLayoutDictionary"></param>
        /// <returns></returns>
        public static double HeightFromLineIndex(this IStaff staff, double staffCanvasTop, int line, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            var staffLayout = scoreLayoutDictionary.GetOrDefault(staff);
            return staffCanvasTop + (line * (staffLayout.LineSpacing / 2));
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
        public static double HeightOnCanvas(this IStaffGroup staffGroup, double canvasTopStaffGroup, int staffIndex, int lineIndex, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            var staffCanvasTop = canvasTopStaffGroup;
            var _staff = staffGroup.EnumerateStaves(1).First();

            foreach (var staff in staffGroup.EnumerateStaves(staffIndex))
            {
                var staffLayout = scoreLayoutDictionary.GetOrDefault(staff);
                canvasTopStaffGroup += 4 * staffLayout.LineSpacing;
                canvasTopStaffGroup += staffLayout.DistanceToNext;
                _staff = staff;
            }

            var canvasTop = _staff.HeightFromLineIndex(staffCanvasTop, lineIndex, scoreLayoutDictionary);
            return canvasTop;
        }

        /// <summary>
        /// Calculate the total height of the staff.
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="scoreLayoutDictionary"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaff staff, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            var staffLayout = scoreLayoutDictionary.GetOrDefault(staff);
            var staffHeight = 4 * staffLayout.LineSpacing;

            return staffHeight;
        }


        /// <summary>
        /// Calculate the total height of the staff group.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="scoreLayoutDictionary"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffGroup staffGroup, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            var height = 0d;
            var groupLayout = scoreLayoutDictionary.GetOrDefault(staffGroup);
            if (groupLayout.Collapsed)
            {
                return height;
            }

            if (!groupLayout.Collapsed)
            {
                var lastStaffSpacing = 0d;
                foreach (var staff in staffGroup.EnumerateStaves(groupLayout.NumberOfStaves))
                {
                    var staffHeight = staff.CalculateHeight(scoreLayoutDictionary);
                    height += staffHeight;

                    var staffLayout = staff;
                    lastStaffSpacing = groupLayout.DistanceToNext;
                    height += lastStaffSpacing;
                }

                height -= lastStaffSpacing;
            }

            return height;
        }

        /// <summary>
        /// Calculate the total height of the staff system.
        /// </summary>
        /// <param name="staffSystem"></param>
        /// <param name="scoreLayoutDictionary"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffSystem staffSystem, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            var height = 0d;
            var lastStafGroupSpacing = 0d;
            foreach (var staffGroup in staffSystem.EnumerateStaffGroups())
            {
                var staffGroupLayout = scoreLayoutDictionary.GetOrDefault(staffGroup);
                var staffGroupHeight = staffGroup.CalculateHeight(scoreLayoutDictionary);
                height += staffGroupHeight;

                lastStafGroupSpacing = staffGroupLayout.DistanceToNext;
                height += lastStafGroupSpacing;
            }
            height -= lastStafGroupSpacing;
            return height;
        }
    }
}
