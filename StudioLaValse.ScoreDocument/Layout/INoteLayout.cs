namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Represents a note layout.
    /// </summary>
    public interface INoteLayout 
    {
        /// <summary>
        /// Force a type of accidental.
        /// </summary>
        TemplateProperty<AccidentalDisplay> ForceAccidental { get; }

        /// <summary>
        /// Define the scale of the note. Default 1.
        /// </summary>
        TemplateProperty<double> Scale { get; }

        /// <summary>
        /// Set the staff index of the note.
        /// </summary>
        TemplateProperty<int> StaffIndex { get; }

        /// <summary>
        /// Set the horizontal offset of the note.
        /// </summary>
        TemplateProperty<double> XOffset { get; }
    }
}