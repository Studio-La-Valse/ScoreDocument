namespace StudioLaValse.ScoreDocument.Memento
{
    public class StaffMemento
    {
        public required int IndexInStaffGroup { get; init; }
        public required IStaffLayout Layout { get; init; }
    }
}
