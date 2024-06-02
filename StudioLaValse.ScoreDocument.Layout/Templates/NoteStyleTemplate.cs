namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class NoteStyleTemplate
    {
        public required double Scale { get; set; }
        public required AccidentalDisplay AccidentalDisplay { get; set; }

        public static NoteStyleTemplate Create()
        {
            return new NoteStyleTemplate()
            {
                Scale = 1,
                AccidentalDisplay = AccidentalDisplay.Default
            };
        }
        public void Apply(NoteStyleTemplate template)
        {
            Scale = template.Scale;
            AccidentalDisplay = template.AccidentalDisplay;
        }
    }
}
