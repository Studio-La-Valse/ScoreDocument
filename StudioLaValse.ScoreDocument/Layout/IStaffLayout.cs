﻿namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A staff layout.
    /// </summary>
    public interface IStaffLayout : IScoreElementLayout<IStaffLayout>
    {
        /// <summary>
        /// Specifies how much space is reserved between lines.
        /// </summary>
        double LineSpacing { get; }
        /// <summary>
        /// Specifies how much space is reserved before the next <see cref="IStaff"/> in the <see cref="IStaffGroup"/>.
        /// </summary>
        double DistanceToNext { get; }
    }
}
