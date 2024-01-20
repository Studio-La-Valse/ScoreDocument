namespace StudioLaValse.ScoreDocument.Private
{
    internal interface ICellFactory<TCell, TColumn, TRow>
    {
        TCell Create(TColumn column, TRow row);
    }
}
