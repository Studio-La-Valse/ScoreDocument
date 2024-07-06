using StudioLaValse.ScoreDocument.Implementation.Private;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Models;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default;

internal class NoteProxy(Note source, ILayoutSelector layoutSelector) : INote
{
    private readonly Note source = source;
    private readonly ILayoutSelector layoutSelector = layoutSelector;

    public INoteLayout Layout => layoutSelector.NoteLayout(source);



    public Pitch Pitch
    {
        get => source.Pitch;
        set
        {
            source.Pitch = value;
        }
    }

    public Position Position => source.Position;

    public RythmicDuration RythmicDuration => source.RythmicDuration;

    public Tuplet Tuplet => source.Tuplet;

    public int Id => source.Id;



    public TemplateProperty<AccidentalDisplay> ForceAccidental => Layout.ForceAccidental;
    public TemplateProperty<double> Scale => Layout.Scale;
    public TemplateProperty<int> StaffIndex => Layout.StaffIndex;
    public TemplateProperty<double> XOffset => Layout.XOffset;


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
