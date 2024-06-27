using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Templates
{
    /// <summary>
    /// A note style template.
    /// </summary>
    public class NoteStyleTemplate
    {
        /// <summary>
        /// The global scale of notes in the score.
        /// </summary>
        public required double Scale { get; set; }

        /// <summary>
        /// The way accidentals are displayed.
        /// </summary>
        public required AccidentalDisplay AccidentalDisplay { get; set; }

        /// <summary>
        /// Create a default note style template.
        /// </summary>
        /// <returns></returns>
        public static NoteStyleTemplate Create()
        {
            return new NoteStyleTemplate()
            {
                Scale = 1,
                AccidentalDisplay = AccidentalDisplay.Default
            };
        }

        /// <summary>
        /// Apply another style template.
        /// </summary>
        /// <param name="template"></param>
        public void Apply(NoteStyleTemplate template)
        {
            Scale = template.Scale;
            AccidentalDisplay = template.AccidentalDisplay;
        }
    }
}
