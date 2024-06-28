namespace StudioLaValse.ScoreDocument.Implementation
{
    public sealed class GraceNote : ScoreElement, IMementoElement<GraceNoteModel>
    {
        public AuthorGraceNoteLayout AuthorLayout { get; }
        public UserGraceNoteLayout UserLayout { get; }
        public Pitch Pitch { get; set; }
        public InstrumentMeasure InstrumentMeasure { get; }

        public GraceNote(InstrumentMeasure instrumentMeasure,
                         AuthorGraceNoteLayout authorGraceNoteLayout,
                         UserGraceNoteLayout userGraceNoteLayout,
                         Pitch pitch,
                         IKeyGenerator<int> keyGenerator,
                         Guid guid) : base(keyGenerator, guid)
        {
            InstrumentMeasure = instrumentMeasure;
            AuthorLayout = authorGraceNoteLayout;
            UserLayout = userGraceNoteLayout;
            Pitch = pitch;
        }

        public GraceNoteModel GetMemento()
        {
            return new GraceNoteModel()
            {
                Id = Guid,
                ForceAccidental = (int)AuthorLayout.ForceAccidental.Value,
                Pitch = Pitch.Convert(),
                StaffIndex = AuthorLayout.StaffIndex,
                Layout = UserLayout.GetMemento()
            };
        }

        public void ApplyMemento(GraceNoteModel memento)
        {
            AuthorLayout.ApplyMemento(memento);
            UserLayout.ApplyMemento(memento.Layout);
            Pitch = memento.Pitch.Convert();
        }
    }
}
