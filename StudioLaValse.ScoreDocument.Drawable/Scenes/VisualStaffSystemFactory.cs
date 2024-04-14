﻿namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual staff system factory.
    /// </summary>
    public class VisualStaffSystemFactory : IVisualStaffSystemFactory
    {
        private readonly IVisualSystemMeasureFactory systemMeasureFactory;
        private readonly ISelection<IUniqueScoreElement> selection;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="systemMeasureFactory"></param>
        /// <param name="selection"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public VisualStaffSystemFactory(IVisualSystemMeasureFactory systemMeasureFactory, ISelection<IUniqueScoreElement> selection, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.systemMeasureFactory = systemMeasureFactory;
            this.selection = selection;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IStaffSystemReader staffSystem, double canvasLeft, double canvasTop, double length, double lineSpacing, ColorARGB color)
        {
            return new VisualStaffSystem(staffSystem, canvasLeft, canvasTop, length, lineSpacing, systemMeasureFactory, color, selection, scoreLayoutDictionary);
        }
    }
}
