using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private
{
    internal sealed class GraceNote : ScoreElement, IMementoElement<GraceNoteMemento>
    {
        private readonly ScoreDocumentStyleTemplate scoreDocumentStyleTemplate;
        private readonly GraceGroup graceGroup;

        public AuthorGraceNoteLayout AuthorLayout { get; }
        public UserGraceNoteLayout UserLayout { get; set; }
        public Pitch Pitch { get; set; }
        public InstrumentMeasure InstrumentMeasure { get; }

        public GraceNote(InstrumentMeasure instrumentMeasure,
                         AuthorGraceNoteLayout authorGraceNoteLayout,
                         UserGraceNoteLayout userGraceNoteLayout,
                         ScoreDocumentStyleTemplate scoreDocumentStyleTemplate,
                         GraceGroup graceGroup,
                         Pitch pitch,
                         IKeyGenerator<int> keyGenerator,
                         Guid guid) : base(keyGenerator, guid)
        {
            InstrumentMeasure = instrumentMeasure;
            AuthorLayout = authorGraceNoteLayout;
            UserLayout = userGraceNoteLayout;
            Pitch = pitch;

            this.scoreDocumentStyleTemplate = scoreDocumentStyleTemplate;
            this.graceGroup = graceGroup;
        }

        public GraceNoteModel GetModel()
        {
            return new GraceNoteModel()
            {
                Id = Guid,
                Pitch = Pitch.Convert(),
                ForceAccidental = (int?)AuthorLayout._ForceAccidental.Field,
                StaffIndex = AuthorLayout._StaffIndex.Field,
                Color = AuthorLayout._Color.Field?.Convert()
            };
        }
        public GraceNoteLayoutModel GetLayoutModel()
        {
            return new GraceNoteLayoutModel()
            {
                Id = UserLayout.Guid,
                GraceNoteId = Guid,
                ForceAccidental = (int?)UserLayout._ForceAccidental.Field,
                StaffIndex = UserLayout._StaffIndex.Field,
                Color = AuthorLayout._Color.Field?.Convert()
            };
        }
        public GraceNoteMemento GetMemento()
        {
            return new GraceNoteMemento()
            {
                Id = Guid,
                Layout = GetLayoutModel(),
                Pitch = Pitch.Convert(),
                ForceAccidental = (int?)AuthorLayout._ForceAccidental.Field,
                StaffIndex = AuthorLayout._StaffIndex.Field,
                Color = AuthorLayout._Color.Field?.Convert()
            };
        }
        public void ApplyMemento(GraceNoteMemento memento)
        {
            AuthorLayout.ApplyMemento(memento);
            Pitch = memento.Pitch.Convert();

            var layout = memento.Layout;
            UserLayout = new UserGraceNoteLayout(layout.Id, graceGroup.UserLayout, AuthorLayout);
            UserLayout.ApplyMemento(layout);
        }
    }
}
