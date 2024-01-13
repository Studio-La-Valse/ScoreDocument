namespace StudioLaValse.ScoreDocument.Extensions
{
    public static class StaffGroupReaderExtensions
    {
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

        public static double HeightFromLineIndex(this IStaffReader staff, double staffCanvasTop, int line) =>
            staffCanvasTop + (line * (staff.ReadLayout().LineSpacing / 2));

        public static double HeightInStaffGruop(this IStaffGroupReader staffGroup, int staffIndex, double canvasTopStaffGroup)
        {
            foreach (var staff in staffGroup.ReadStaves())
            {
                if (staff.IndexInStaffGroup == staffIndex)
                {
                    return canvasTopStaffGroup;
                }

                canvasTopStaffGroup += 4 * staff.ReadLayout().LineSpacing;
                canvasTopStaffGroup += staff.ReadLayout().DistanceToNext;
            }

            throw new ArgumentOutOfRangeException($"Requested staff index {staffIndex} not in staffgroup with {staffGroup.EnumerateStaves().Count()} staves.");
        }

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
                    var staffHeight = 4 * staff.ReadLayout().LineSpacing;
                    height += staffHeight;

                    var staffLayout = staff.ReadLayout();
                    lastStaffSpacing = staffLayout.DistanceToNext;
                    height += lastStaffSpacing;
                }

                height -= lastStaffSpacing;
            }

            return height;
        }

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
