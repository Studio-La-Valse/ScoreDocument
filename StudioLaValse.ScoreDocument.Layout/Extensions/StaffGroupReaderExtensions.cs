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
            int index = 0;
            foreach (IStaffReader staff in staffGroup.EnumerateStaves(numberOfStaves))
            {
                Clef clef =
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
        /// <param name="lineSpacing"></param>
        /// <returns></returns>
        public static double HeightFromLineIndex(this IStaffReader staff, double staffCanvasTop, int line, double lineSpacing)
        {
            return staffCanvasTop + line * (lineSpacing / 2);
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
        public static double HeightOnCanvas(this IStaffGroupReader staffGroup, double canvasTopStaffGroup, int staffIndex, int lineIndex, IScoreDocumentLayout scoreLayoutDictionary)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(staffIndex, nameof(staffIndex));

            var lineSpacing = scoreLayoutDictionary.StaffGroupLayout(staffGroup).LineSpacing;
            foreach (IStaffReader staff in staffGroup.EnumerateStaves(staffIndex))
            {
                StaffLayout staffLayout = scoreLayoutDictionary.StaffLayout(staff);
                canvasTopStaffGroup += staff.CalculateHeight(lineSpacing);
                canvasTopStaffGroup += staffLayout.DistanceToNext;
            }

            var _staff = staffGroup.EnumerateStaves(staffIndex + 1).ElementAt(staffIndex);
            double canvasTop = _staff.HeightFromLineIndex(canvasTopStaffGroup, lineIndex, lineSpacing);
            return canvasTop;
        }

        /// <summary>
        /// Calculate the total height of the staff.
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="lineSpacing"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffReader staff, double lineSpacing)
        {
            double staffHeight = 4 * lineSpacing;

            return staffHeight;
        }


        /// <summary>
        /// Calculate the total height of the staff group.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="scoreLayoutDictionary"></param>
        /// <returns></returns>
        public static double CalculateHeight(this IStaffGroupReader staffGroup, IScoreDocumentLayout scoreLayoutDictionary)
        {
            double height = 0d;
            StaffGroupLayout groupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);
            var lineSpacing = groupLayout.LineSpacing;
            if (groupLayout.Collapsed)
            {
                return height + groupLayout.DistanceToNext;
            }

            double lastStaffSpacing = 0d;
            foreach (IStaffReader staff in staffGroup.EnumerateStaves(groupLayout.NumberOfStaves))
            {
                double staffHeight = staff.CalculateHeight(lineSpacing);
                double staffSpacing = scoreLayoutDictionary.StaffLayout(staff).DistanceToNext;
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
        public static double CalculateHeight(this IStaffSystemReader staffSystem, IScoreDocumentLayout scoreLayoutDictionary)
        {
            double height = 0d;
            double lastStafGroupSpacing = 0d;
            foreach (IStaffGroupReader staffGroup in staffSystem.EnumerateStaffGroups())
            {
                StaffGroupLayout staffGroupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);
                double staffGroupHeight = staffGroup.CalculateHeight(scoreLayoutDictionary);
                height += staffGroupHeight;

                lastStafGroupSpacing = staffGroupLayout.DistanceToNext;
                height += lastStafGroupSpacing;
            }
            height -= lastStafGroupSpacing;
            return height;
        }
    }
}
