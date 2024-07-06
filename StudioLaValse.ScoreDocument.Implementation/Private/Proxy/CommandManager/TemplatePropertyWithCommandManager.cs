namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager;

internal class TemplatePropertyWithCommandManager<T> : TemplateProperty<T>
{
    private readonly TemplateProperty<T> original;
    private readonly ICommandManager commandManager;

    public TemplatePropertyWithCommandManager(TemplateProperty<T> original, ICommandManager commandManager)
    {
        this.original = original;
        this.commandManager = commandManager;
    }

    public override T Value
    {
        get => original.Value;
        set
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var valueIsSet = FieldIsSet;
            var oldValue = original.Value;
            var command = new SimpleCommand(() =>
            {
                original.Value = value;
            },
            () =>
            {
                if (valueIsSet)
                {
                    original.Value = oldValue;
                }
                else
                {
                    original.Reset();
                }
            });
            transaction.Enqueue(command);
        }
    }

    public override bool FieldIsSet => original.FieldIsSet;

    public override void Reset()
    {
        if (!FieldIsSet)
        {
            return;
        }

        var transaction = commandManager.ThrowIfNoTransactionOpen();
        var oldValue = original.Value;
        var command = new SimpleCommand(original.Reset, () => original.Value = oldValue);
        transaction.Enqueue(command);
    }
}
