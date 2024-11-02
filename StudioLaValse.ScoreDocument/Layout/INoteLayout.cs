using StudioLaValse.ScoreDocument.Models.Classes;

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
        /// Define the scale of the note. Inherited from containing <see cref="IMeasureBlock"/>
        /// </summary>
        ReadonlyTemplateProperty<double> Scale { get; }

        /// <summary>
        /// Set the staff index of the note.
        /// </summary>
        TemplateProperty<int> StaffIndex { get; }

        /// <summary>
        /// The color of the note.
        /// </summary>
        TemplateProperty<ColorARGBClass> Color { get; }
    }
}