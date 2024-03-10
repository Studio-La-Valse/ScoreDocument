using StudioLaValse.ScoreDocument.Drawable.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualBeamGroup : BaseContentWrapper
    {
        private readonly IEnumerable<VisualStem> visualStems;
        private readonly Ruler visualBeamDefinition;
        private readonly double scale;
        private readonly ColorARGB color;
        private readonly IVisualBeamBuilder visualBeamBuilder;

        public VisualBeamGroup(IEnumerable<VisualStem> visualStems, Ruler visualBeamDefinition, double scale, ColorARGB color, IVisualBeamBuilder visualBeamBuilder)
        {
            this.visualStems = visualStems;
            this.visualBeamDefinition = visualBeamDefinition;
            this.scale = scale;
            this.color = color;
            this.visualBeamBuilder = visualBeamBuilder;
        }


        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return visualStems;
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return visualBeamBuilder.Build(visualStems, visualBeamDefinition, scale, color);
        }
    }
}
