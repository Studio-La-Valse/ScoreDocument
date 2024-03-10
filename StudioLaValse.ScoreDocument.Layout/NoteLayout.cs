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

        public int StaffIndex { get; set; }
        public double XOffset { get; set; }

        public TemplateProperty<AccidentalDisplay> ForceAccidental { get; set; }
        public TemplateProperty<double> Scale { get; set; }


        public NoteLayout(NoteStyleTemplate styleTemplate, bool grace)
        {
            this.styleTemplate = styleTemplate;
            this.grace = grace;
            StaffIndex = 0;
            XOffset = 0;
            ForceAccidental = new TemplateProperty<AccidentalDisplay>(() => this.styleTemplate.AccidentalDisplay);
            Scale = new TemplateProperty<double>(() => this.styleTemplate.Scale * (grace ? 0.5 : 1));
        }

        /// <inheritdoc/>
        public NoteLayout Copy()
        {
            var copy = new NoteLayout(styleTemplate, grace)
            {
                StaffIndex = StaffIndex,
                XOffset = XOffset,
            };
            copy.ForceAccidental.Field = ForceAccidental.Field;
            copy.Scale.Field = Scale.Field;
            return copy;
        }
    }
}