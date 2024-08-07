﻿using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual staff system factory.
    /// </summary>
    public class VisualStaffSystemFactory : IVisualStaffSystemFactory
    {
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly IScoreDocument scoreLayoutDictionary;
        private readonly IUnitToPixelConverter unitToPixelConverter;
        private readonly IGlyphLibrary glyphLibrary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="systemMeasureFactory"></param>
        /// <param name="scoreLayoutDictionary"></param>
        /// <param name="unitToPixelConverter"></param>
        /// <param name="glyphLibrary"></param>
        public VisualStaffSystemFactory(IVisualSystemMeasureFactory systemMeasureFactory, IScoreDocument scoreLayoutDictionary, IUnitToPixelConverter unitToPixelConverter, IGlyphLibrary glyphLibrary)
        {
            this.systemMeasureFactory = systemMeasureFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.unitToPixelConverter = unitToPixelConverter;
            this.glyphLibrary = glyphLibrary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IStaffSystem staffSystem, double canvasLeft, double canvasTop, double length, double lineSpacing)
        {
            return new VisualStaffSystem(staffSystem, canvasLeft, canvasTop, length, lineSpacing, glyphLibrary, systemMeasureFactory, scoreLayoutDictionary, unitToPixelConverter);
        }
    }
}
