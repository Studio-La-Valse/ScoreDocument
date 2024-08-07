﻿using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual note factory.
    /// </summary>
    public class VisualNoteFactory : IVisualNoteFactory
    {
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IScoreDocument scoreDocumentLayout;
        private readonly IUnitToPixelConverter unitToPixelConverter;
        private readonly IGlyphLibrary glyphLibrary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="scoreDocumentLayout"></param>
        /// <param name="unitToPixelConverter"></param>
        /// <param name="glyphLibrary"></param>
        public VisualNoteFactory(ISelection<IUniqueScoreElement> selection, IScoreDocument scoreDocumentLayout, IUnitToPixelConverter unitToPixelConverter, IGlyphLibrary glyphLibrary)
        {
            this.selection = selection;
            this.scoreDocumentLayout = scoreDocumentLayout;
            this.unitToPixelConverter = unitToPixelConverter;
            this.glyphLibrary = glyphLibrary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Build(INote note, double canvasLeft, double canvasTop, double lineSpacing, double scoreScale, double instrumentScale, bool offsetDots, Accidental? accidental)
        {
            var noteScale = note.Scale;
            return new VisualNote(note, canvasLeft, canvasTop, lineSpacing, scoreScale, instrumentScale, noteScale, offsetDots, accidental, glyphLibrary, scoreDocumentLayout, selection, unitToPixelConverter);
        }
    }
}
