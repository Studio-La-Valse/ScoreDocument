﻿using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <inheritdoc/>
    public class VisualPageFactory : IVisualPageFactory
    {
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;

        /// <inheritdoc/>
        public VisualPageFactory(IVisualStaffSystemFactory staffSystemContentFactory, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IPageReader page, double canvasLeft, double canvasTop)
        {
            var lineSpacing = GlyphLibrary.LineSpacing;
            var visualPage = new VisualPage(page, canvasLeft, canvasTop, lineSpacing, staffSystemContentFactory, scoreLayoutDictionary);
            return visualPage;
        }
    }
}