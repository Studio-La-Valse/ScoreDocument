﻿namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory interface for creating a visual staff system.
    /// </summary>
    public interface IVisualStaffSystemFactory
    {
        /// <summary>
        /// Create the visual staff system.
        /// </summary>
        /// <param name="staffSystem"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="canvasTop"></param>
        /// <param name="length"></param>
        /// <param name="lineSpacing"></param>
        /// <returns></returns>
        BaseContentWrapper CreateContent(IStaffSystem staffSystem, double canvasLeft, double canvasTop, double length, double lineSpacing);
    }
}
