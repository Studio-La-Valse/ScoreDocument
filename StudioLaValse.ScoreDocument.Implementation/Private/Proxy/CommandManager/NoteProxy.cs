using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;
using StudioLaValse.ScoreDocument.Models;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager;

internal class NoteProxy(Note source, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector) : INote
{
    private readonly Note source = source;
    private readonly ICommandManager commandManager = commandManager;
    private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged = notifyEntityChanged;
    private readonly ILayoutSelector layoutSelector = layoutSelector;

    public INoteLayout Layout => layoutSelector.NoteLayout(source);



    public Pitch Pitch
    {
        get => source.Pitch;
        set
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<Note, NoteMemento>(source, s => s.Pitch = value).ThenInvalidate(notifyEntityChanged, source.HostMeasure);
            transaction.Enqueue(command);
        }
    }

    public Position Position => source.Position;

    public RythmicDuration RythmicDuration => source.RythmicDuration;

    public Tuplet Tuplet => source.Tuplet;

    public int Id => source.Id;

    public Guid Guid => source.Guid;


    public TemplateProperty<AccidentalDisplay> ForceAccidental => Layout.ForceAccidental.WithRerender(notifyEntityChanged, source.HostMeasure, commandManager);
    public TemplateProperty<double> Scale => Layout.Scale.WithRerender(notifyEntityChanged, source.HostMeasure, commandManager);
    public TemplateProperty<int> StaffIndex => Layout.StaffIndex.WithRerender(notifyEntityChanged, source.HostMeasure.HostMeasure.HostDocument, commandManager);
    public TemplateProperty<double> XOffset => Layout.XOffset.WithRerender(notifyEntityChanged, source.HostMeasure, commandManager);


    public IEnumerable<IScoreElement> EnumerateChildren()
    {
        yield break;
    }

    public bool Equals(IUniqueScoreElement? other)
    {
        if (other is null)
        {
            return false;
        }

        return other.Id == Id;
    }
}
