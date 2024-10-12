using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Represents a note layout.
    /// </summary>
    public interface IGraceNoteLayout : ILayout
    {
        /// <summary>
        /// Force a type of accidental.
        /// </summary>
        TemplateProperty<AccidentalDisplay> ForceAccidental { get; }

        /// <summary>
        /// Define the scale of the note. Default 1.
        /// </summary>
        ReadonlyTemplateProperty<double> Scale { get; }

        /// <summary>
        /// Set the staff index of the note.
        /// </summary>
        TemplateProperty<int> StaffIndex { get; }

        /// <summary>
        /// Set the horizontal offset of the note.
        /// </summary>
        ReadonlyTemplateProperty<double> XOffset { get; }

        /// <summary>
        /// The color of the note.
        /// </summary>
        TemplateProperty<ColorARGB> Color { get; }
    }
}