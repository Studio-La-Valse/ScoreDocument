using StudioLaValse.ScoreDocument.Implementation.Private;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default
{
    internal class GraceNoteProxy : IGraceNote
    {
        private readonly GraceNote graceNote;
        private readonly ILayoutSelector layoutSelector;


        public IGraceNoteLayout Layout => layoutSelector.GraceNoteLayout(graceNote);

        public int Id => graceNote.Id;

        public ReadonlyTemplateProperty<double> Scale => Layout.Scale;

        public ReadonlyTemplateProperty<double> XOffset => Layout.XOffset;

        public Pitch Pitch
        {
            get => graceNote.Pitch;
            set
            {
                graceNote.Pitch = value;
            }
        }

        public TemplateProperty<AccidentalDisplay> ForceAccidental => Layout.ForceAccidental;

        public TemplateProperty<int> StaffIndex => Layout.StaffIndex;

        public TemplateProperty<ColorARGB> Color => Layout.Color;



        public GraceNoteProxy(GraceNote graceNote, ILayoutSelector layoutSelector)
        {
            this.graceNote = graceNote;
            this.layoutSelector = layoutSelector;
        }


        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            yield break;
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Id == Id;
        }
    }
}
