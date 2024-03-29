﻿using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Defines a score entity that can be persisted outside of the lifecycle of the application.
    /// </summary>
    public interface IScoreEntity : IScoreElement
    {
        /// <summary>
        /// The Guid of the entity.
        /// </summary>
        Guid Guid { get; }
    }
}