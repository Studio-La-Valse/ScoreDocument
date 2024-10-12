using StudioLaValse.ScoreDocument.GlyphLibrary;
using StudioLaValse.ScoreDocument.Private;

namespace StudioLaValse.ScoreDocument.Extensions
{

    /// <summary>
    /// Extensions to staff (-group and -system) readers.
    /// </summary>
    public static class StaffGroupReaderExtensions
    {
        /// <summary>
        /// Enumerate the staff systems on this page with their respective height on the page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="startValue"></param>
        /// <returns></returns>
        public static IEnumerable<(IStaffSystem, double)> EnumerateFromTop(this IPage page, double startValue)
        {
            foreach (var staffSystem in page.EnumerateStaffSystems())
            {
                var staffSystemHeight = staffSystem.CalculateHeight();
                yield return (staffSystem, startValue);

                startValue += staffSystemHeight;

                var lastStafGroupSpacing = staffSystem.PaddingBottom * staffSystem.Scale;
                startValue += lastStafGroupSpacing;
            }
        }

        /// <summary>
        /// Calculates the height of a note in a staff group, assuming the staff group starts a the specified canvas top.
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the staff index specified in the note layout is not present in the staff group.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="staffIndex"></param>
        /// <param name="lineIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static double DistanceFromTop(this IStaffGroup staffGroup, int staffIndex, int lineIndex)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(staffIndex, nameof(staffIndex));

            var instrumentScale = staffGroup.InstrumentRibbon.Scale;
            var canvasTopStaffGroup = 0d;

            foreach (var staff in staffGroup.EnumerateStaves().Take(staffIndex))
            {
                canvasTopStaffGroup += staff.CalculateHeight();
                canvasTopStaffGroup += staff.DistanceToNext * staff.Scale;
            }

            var _staff = staffGroup.EnumerateStaves().ElementAt(staffIndex);
            var canvasTop = canvasTopStaffGroup + _staff.DistanceFromTop(lineIndex);
            return canvasTop;
        }


        /// <summary>
        /// Calculate the total height of the staff system.
        /// </summary>
        /// <param name="staffSystem"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffSystem staffSystem)
        {
            if (!staffSystem.EnumerateStaffGroups().Any())
            {
                return 0;
            }

            var (lastStaffGruop, lastHeight) = staffSystem.EnumerateFromTop(0).Last();

            var lastStaffGroupHeight = lastStaffGruop.CalculateHeight();
            lastHeight += lastStaffGroupHeight;

            return lastHeight;
        }

        /// <summary>
        /// Enumerate the staff groups with each corresponding height measured from the starting top value.
        /// </summary>
        /// <param name="staffSystem"></param>
        /// <param name="startValue"></param>
        /// <returns></returns>
        public static IEnumerable<(IStaffGroup, double)> EnumerateFromTop(this IStaffSystem staffSystem, double startValue)
        {
            foreach (var staffGroup in staffSystem.EnumerateStaffGroups())
            {
                var staffGroupHeight = staffGroup.CalculateHeight();
                yield return (staffGroup, startValue);

                startValue += staffGroupHeight;

                var lastStafGroupSpacing = staffGroup.DistanceToNext * staffGroup.Scale;
                startValue += lastStafGroupSpacing;
            }
        }


        /// <summary>
        /// Calculate the total height of the staff group.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffGroup staffGroup)
        {
            var number = staffGroup.NumberOfStaves;
            if (number == 0)
            {
                return 0;
            }
            var (lastStaff, lastHeight) = staffGroup.EnumerateFromTop(0, number).Last();

            var lastStaffHeight = lastStaff.CalculateHeight();
            lastHeight += lastStaffHeight;

            return lastHeight;
        }

        /// <summary>
        /// Enumerate the staves with each corresponding height measured from the starting top value.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="startValue"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static IEnumerable<(IStaff, double)> EnumerateFromTop(this IStaffGroup staffGroup, double startValue, int number)
        {
            if (number == 0)
            {
                yield break;
            }

            foreach (var staff in staffGroup.EnumerateStaves(number))
            {
                var staffHeight = staff.CalculateHeight();
                yield return (staff, startValue);

                startValue += staffHeight;

                var lastStaffSpacing = staff.DistanceToNext * staff.Scale;
                startValue += lastStaffSpacing;
            }
        }

        /// <summary>
        /// Enumerate the staves with each corresponding height measured from the starting top value.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="startValue"></param>
        /// <returns></returns>
        public static IEnumerable<(IStaff, double)> EnumerateFromTop(this IStaffGroup staffGroup, double startValue)
        {
            var number = staffGroup.NumberOfStaves;
            return staffGroup.EnumerateFromTop(startValue, number);
        }

        /// <summary>
        /// Calculates the height of a line, assuming the staff starts at the specified canvas top.
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static double DistanceFromTop(this IStaff staff, int line)
        {
            return line * (Glyph.LineSpacing * staff.Scale / 2);
        }

        /// <summary>
        /// Calculate the total height of the staff.
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaff staff)
        {
            var staffHeight = 4 * Glyph.LineSpacing * staff.Scale;

            return staffHeight;
        }

        /// <summary>
        /// Enumerates the default opening clefs.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="numberOfStaves"></param>
        /// <returns></returns>
        public static IEnumerable<(int, Clef)> EnumerateDefaultInstrumentClefs(this IStaffGroup staffGroup, int numberOfStaves)
        {
            for (var i = 0; i < numberOfStaves; i++)
            {
                var clef =
                    staffGroup.Instrument.DefaultClefs.ElementAtOrDefault(i) ??
                    staffGroup.Instrument.DefaultClefs.Last();

                yield return (i, clef);
            }
        }
    }
}
