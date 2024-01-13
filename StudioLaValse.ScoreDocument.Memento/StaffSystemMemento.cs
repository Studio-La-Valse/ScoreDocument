namespace StudioLaValse.ScoreDocument.Memento
{
    public class StaffSystemMemento
    {
        public required IStaffSystemLayout Layout { get; init; }
        public required IEnumerable<StaffGroupMemento> StaffGroups { get; init; }
    }
}
