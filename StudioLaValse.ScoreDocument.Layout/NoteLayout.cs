using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a note.
    /// </summary>
    public class NoteLayout : ILayoutElement<NoteLayout>
    {
        private readonly NoteStyleTemplate styleTemplate;
        private readonly bool grace;
        private readonly TemplateProperty<AccidentalDisplay> forceAccidental;
        private readonly TemplateProperty<double> scale;

        public int StaffIndex { get; set; }
        public double XOffset { get; set; }

        public AccidentalDisplay ForceAccidental { get => forceAccidental.Value; set => forceAccidental.Value = value; }
        public double Scale { get => scale.Value; set => scale.Value = value; }


        public NoteLayout(NoteStyleTemplate styleTemplate, bool grace)
        {
            this.styleTemplate = styleTemplate;
            this.grace = grace;

            forceAccidental = new TemplateProperty<AccidentalDisplay>(() => this.styleTemplate.AccidentalDisplay);
            scale = new TemplateProperty<double>(() => this.styleTemplate.Scale * (grace ? 0.5 : 1));

            StaffIndex = 0;
            XOffset = 0;
        }

        public NoteLayout Copy()
        {
            var copy = new NoteLayout(styleTemplate, grace)
            {
                StaffIndex = StaffIndex,
                XOffset = XOffset,
            };
            copy.forceAccidental.Field = forceAccidental.Field;
            copy.scale.Field = scale.Field;
            return copy;
        }
    }
}