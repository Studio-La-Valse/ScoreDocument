namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// An interface that defines a chord that has beam information.
    /// May be implemented by a chord layout or grace chord layout.
    /// </summary>
    public interface IHasBeamGroup
    {
        /// <summary>
        /// Reads the beam types of this chord.
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<PowerOfTwo, BeamType>> ReadBeamTypes();

        /// <summary>
        /// Reads the beam type at the requested rythmic duration.
        /// For example, the second chord in a beamgroup of 3/16 and 1/16th will return:
        /// <see cref="BeamType.End"/> at 1/8th and <see cref="BeamType.HookEnd"/> at 1/16th duration.
        /// </summary>
        /// <returns></returns>
        BeamType? ReadBeamType(PowerOfTwo i);
    }
}