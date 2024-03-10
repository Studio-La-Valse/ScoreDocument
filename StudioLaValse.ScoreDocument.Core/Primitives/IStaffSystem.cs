namespace StudioLaValse.ScoreDocument.Core.Primitives
{
    /// <summary>
    /// Represents a staff system reader.
    /// </summary>
    public interface IStaffSystem
    {

    }

    /// <summary>
    /// Represents a staff system reader.
    /// </summary>
    public interface IStaffSystem<TStaffGroup> : IStaffSystem where TStaffGroup : IStaffGroup
    {
        /// <summary>
        /// Reads the staffgroups in this staff system.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TStaffGroup> EnumerateStaffGroups();
    }

    /// <summary>
    /// Represents a staff system reader.
    /// </summary>
    public interface IStaffSystem<TScoreMeasure, TStaffGroup> : IStaffSystem<TStaffGroup> where TStaffGroup : IStaffGroup where TScoreMeasure : IScoreMeasure
    {
        /// <summary>
        /// Reads the score measures contained by this staff.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TScoreMeasure> EnumerateMeasures();
    }
}
