namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager;

internal static class TemplatePropertiesExensions
{
    public static TemplateProperty<T> WithCommandManager<T>(this TemplateProperty<T> templateProperty, ICommandManager commandManager)
    {
        return new TemplatePropertyWithCommandManager<T>(templateProperty, commandManager);
    }

    public static TemplateProperty<T> ThenInvalidate<T>(this TemplateProperty<T> templateProperty, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, IUniqueScoreElement toInvalidate)
    {
        return new TemplatePropertyWithInvalidator<T>(templateProperty, notifyEntityChanged, toInvalidate);
    }

    public static TemplateProperty<T> WithRerender<T>(this TemplateProperty<T> templateProperty, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, IUniqueScoreElement toInvalidate, ICommandManager commandManager)
    {
        return templateProperty.ThenInvalidate(notifyEntityChanged, toInvalidate).WithCommandManager(commandManager);
    }
}
