﻿using StudioLaValse.ScoreDocument.Drawable.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualBeamGroup : BaseContentWrapper
    {
        private readonly VisualStem[] visualStems;
        private readonly Ruler visualBeamDefinition;
        private readonly double beamThickness;
        private readonly double beamSpacing;
        private readonly double scale;
        private readonly double hooksize;
        private readonly ColorARGB color;
        private readonly IVisualBeamBuilder visualBeamBuilder;

        public VisualBeamGroup(VisualStem[] visualStems, Ruler visualBeamDefinition, double beamThickness, double beamSpacing, double scale, double hooksize, ColorARGB color, IVisualBeamBuilder visualBeamBuilder)
        {
            this.visualStems = visualStems;
            this.visualBeamDefinition = visualBeamDefinition;
            this.beamThickness = beamThickness;
            this.beamSpacing = beamSpacing;
            this.scale = scale;
            this.hooksize = hooksize;
            this.color = color;
            this.visualBeamBuilder = visualBeamBuilder;
        }


        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return visualStems;
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return visualBeamBuilder.Build(visualStems, visualBeamDefinition, beamThickness, beamSpacing, scale, hooksize, color);
        }
    }
}
