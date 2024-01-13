namespace StudioLaValse.ScoreDocument.Private
{
    public interface ICellFactory<TCell, TColumn, TRow>
    {
        TCell Create(TColumn column, TRow row);
    }
}
