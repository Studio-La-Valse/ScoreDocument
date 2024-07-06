using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private
{
    internal class Note : ScoreElement, IMementoElement<NoteMemento>
    {
        private readonly Chord container;


        public Pitch Pitch { get; set; }
        public AuthorNoteLayout AuthorLayout { get; }
        public UserNoteLayout UserLayout { get; set; }

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



        public NoteModel GetModel()
        {
            return new NoteModel
            {
                Pitch = Pitch.Convert(),
                Id = Guid,
                ForceAccidental = (int?)AuthorLayout._ForceAccidental.Field,
                Scale = AuthorLayout._Scale.Field,
                StaffIndex = AuthorLayout._StaffIndex.Field,
                XOffset = AuthorLayout._XOffset.Field,
            };
        }

        public NoteLayoutModel GetLayoutModel()
        {
            return new NoteLayoutModel()
            {
                Id = UserLayout.Id,
                NoteId = Guid,
                ForceAccidental = (int?)UserLayout._ForceAccidental.Field,
                Scale = UserLayout._Scale.Field,
                StaffIndex = UserLayout._StaffIndex.Field,
                XOffset = UserLayout._XOffset.Field
            };
        }

        public NoteMemento GetMemento()
        {
            return new NoteMemento
            {
                Pitch = Pitch.Convert(),
                Layout = GetLayoutModel(),
                Id = Guid,
                ForceAccidental = (int?)AuthorLayout._ForceAccidental.Field,
                Scale = AuthorLayout._Scale.Field,
                StaffIndex = AuthorLayout._StaffIndex.Field,
                XOffset = AuthorLayout._XOffset.Field,
            };
        }

        public void ApplyMemento(NoteMemento memento)
        {
            AuthorLayout.ApplyMemento(memento);
            Pitch = memento.Pitch.Convert();

            var noteLayoutModel = memento.Layout;
            UserLayout = new UserNoteLayout(noteLayoutModel.Id, AuthorLayout);
            UserLayout.ApplyMemento(noteLayoutModel);
        }
    }
}