namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager
{
    internal static class MementoCommandExtensions
    {
        public static BaseCommand ThenInvalidate<TEntity>(this BaseCommand command, INotifyEntityChanged<TEntity> entityChanged, TEntity entity) where TEntity : IUniqueScoreElement
        {
            return new CommandWithRerender<TEntity>(command, entityChanged, entity);
        }
    }
}
