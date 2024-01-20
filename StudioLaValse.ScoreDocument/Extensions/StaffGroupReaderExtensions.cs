namespace StudioLaValse.ScoreDocument.Extensions
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
        /// <returns></returns>
        public static IEnumerable<Clef> EnumerateDefaultInstrumentClefs(this IStaffGroupReader staffGroup)
        {
            foreach (var staff in staffGroup.EnumerateStaves())
            {
                var clef =
                    staffGroup.Instrument.DefaultClefs.ElementAtOrDefault(staff.IndexInStaffGroup) ??
                    staffGroup.Instrument.DefaultClefs.Last();

                yield return clef;
            }
        }

        /// <summary>
        /// Calculates the height of a line, assuming the staff starts at the specified canvas top.
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="staffCanvasTop"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static double HeightFromLineIndex(this IStaffReader staff, double staffCanvasTop, int line) =>
            staffCanvasTop + (line * (staff.ReadLayout().LineSpacing / 2));


        /// <summary>
        /// Calculates the height of a note in a staff group, assuming the staff group starts a the specified canvas top.
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the staff index specified in the note layout is not present in the staff group.
        /// </summary>
        /// <param name="noteReader"></param>
        /// <param name="staffGroup"></param>
        /// <param name="canvasTopStaffGroup"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static double HeightOnCanvas(this INoteReader noteReader, IStaffGroupReader staffGroup, double canvasTopStaffGroup)
        {
            var staffIndex = 0;
            var staffIndexNote = noteReader.ReadLayout().StaffIndex;
            foreach (var staff in staffGroup.ReadStaves())
            {
                if (staffIndexNote == staffIndex)
                {
                    var lineIndex = noteReader.GetClef().LineIndexAtPitch(noteReader.Pitch);
                    var canvasTop = staff.HeightFromLineIndex(canvasTopStaffGroup, lineIndex);
                    return canvasTop;
                }

                staffIndex++;
                canvasTopStaffGroup += 4 * staff.ReadLayout().LineSpacing;
                canvasTopStaffGroup += staff.ReadLayout().DistanceToNext;
            }

            throw new ArgumentOutOfRangeException($"Requested staff index {staffIndexNote} not in staffgroup with {staffGroup.EnumerateStaves().Count()} staves.");
        }

        /// <summary>
        /// Calculate the total height of the staff.
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffReader staff)
        {
            var staffLayout = staff.ReadLayout();
            var staffHeight = 4 * staffLayout.LineSpacing;

            return staffHeight;
        }


        /// <summary>
        /// Calculate the total height of the staff group.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffGroupReader staffGroup)
        {
            var height = 0d;
            if (staffGroup.ReadLayout().Collapsed)
            {
                return height;
            }

            var staffGroupLayout = staffGroup.ReadLayout();

            if (!staffGroupLayout.Collapsed)
            {
                var lastStaffSpacing = 0d;
                foreach (var staff in staffGroup.ReadStaves())
                {
                    var staffHeight = staff.CalculateHeight();
                    height += staffHeight;

                    var staffLayout = staff.ReadLayout();
                    lastStaffSpacing = staffLayout.DistanceToNext;
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
        /// <returns></returns>
        public static double CalculateHeight(this IStaffSystemReader staffSystem)
        {
            var height = 0d;
            var lastStafGroupSpacing = 0d;
            foreach (var staffGroup in staffSystem.ReadStaffGroups())
            {
                var staffGroupHeight = staffGroup.CalculateHeight();
                height += staffGroupHeight;

                lastStafGroupSpacing = staffGroup.ReadLayout().DistanceToNext;
                height += lastStafGroupSpacing;
            }
            height -= lastStafGroupSpacing;
            return height;
        }
    }
}
