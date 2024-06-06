namespace StudioLaValse.ScoreDocument.Implementation
{
    public class Note : ScoreElement, IMementoElement<NoteModel>
    {
        private readonly Chord container;


        public Pitch Pitch { get; set; }
        public AuthorNoteLayout AuthorLayout { get; }
        public UserNoteLayout UserLayout { get; }

        public InstrumentMeasure HostMeasure =>
            container.HostMeasure;
        public Position Position =>
            container.Position;
        public RythmicDuration RythmicDuration =>
            container.RythmicDuration;
        public Tuplet Tuplet =>
            container.Tuplet;


        internal Note(Pitch pitch,
                      Chord container,
                      AuthorNoteLayout layout,
                      UserNoteLayout secondaryLayout,
                      IKeyGenerator<int> keyGenerator,
                      Guid guid) : base(keyGenerator, guid)
        {
            this.container = container;

            Pitch = pitch;
            AuthorLayout = layout;
            UserLayout = secondaryLayout;
        }



        public NoteModel GetMemento()
        {
            return new NoteModel
            {
                Pitch = Pitch.Convert(),
                Id = Guid,
                Layout = UserLayout.GetMemento(),
                ForceAccidental = AuthorLayout._ForceAccidental.Field.HasValue ? (int)AuthorLayout._ForceAccidental.Field.Value : null,
                Scale = AuthorLayout._Scale.Field,
                StaffIndex = AuthorLayout._StaffIndex.Field,
                XOffset = AuthorLayout._XOffset.Field,
            };
        }
        public void ApplyMemento(NoteModel memento)
        {
            AuthorLayout.ApplyMemento(memento);
            UserLayout.ApplyMemento(memento.Layout);

            Pitch = memento.Pitch.Convert();
        }
    }
}