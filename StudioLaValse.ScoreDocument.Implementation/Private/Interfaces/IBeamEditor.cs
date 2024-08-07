﻿namespace StudioLaValse.ScoreDocument.Implementation.Private.Interfaces
{
    /// <summary>
    /// An interface defining an element that can edit a chord's beams.
    /// </summary>
    internal interface IBeamEditor
    {
        /// <summary>
        /// The rythmic duration of the chord.
        /// </summary>
        RythmicDuration RythmicDuration { get; }
        /// <summary>
        /// The beamtypes of the chord.
        /// </summary>
        Dictionary<PowerOfTwo, BeamType> BeamTypes { get; }
    }
}
