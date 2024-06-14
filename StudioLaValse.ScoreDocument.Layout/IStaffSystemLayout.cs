namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IStaffSystemLayout 
    {
        /// <summary>
        /// Space below the staff system in mm. Not affected by score scale.
        /// </summary>
        double PaddingBottom { get; }
    }
}