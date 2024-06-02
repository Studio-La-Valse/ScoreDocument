namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class ChordStyleTemplate
    {
        public required double SpaceRight { get; set; }

        public static ChordStyleTemplate Create()
        {
            return new ChordStyleTemplate()
            {
                SpaceRight = 12
            };
        }
    }
}
