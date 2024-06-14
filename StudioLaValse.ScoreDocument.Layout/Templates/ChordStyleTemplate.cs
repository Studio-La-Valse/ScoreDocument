namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class ChordStyleTemplate
    {
        public required double SpaceRight { get; set; }

        public static ChordStyleTemplate Create()
        {
            return new ChordStyleTemplate()
            {
                SpaceRight = 1
            };
        }

        public void Apply(ChordStyleTemplate styleTemplate)
        {
            SpaceRight = styleTemplate.SpaceRight;
        }
    }
}
