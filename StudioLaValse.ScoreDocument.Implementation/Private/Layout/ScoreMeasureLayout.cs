using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout;

internal abstract class ScoreMeasureLayout : IScoreMeasureLayout
{
    public abstract ValueTemplateProperty<KeySignature> _KeySignature { get; }
    public abstract NullableTemplateProperty<double> _PaddingBottom { get; }


    public ReadonlyTemplateProperty<double> PaddingLeft { get; }
    public ReadonlyTemplateProperty<double> PaddingRight { get; }


    public TemplateProperty<KeySignature> KeySignature => _KeySignature;
    public TemplateProperty<double?> PaddingBottom => _PaddingBottom;


    protected ScoreMeasureLayout(ScoreMeasureStyleTemplate scoreMeasureStyleTemplate)
    {
        PaddingLeft = new ReadonlyTemplatePropertyFromFunc<double>(() => scoreMeasureStyleTemplate.PaddingLeft);
        PaddingRight = new ReadonlyTemplatePropertyFromFunc<double>(() => scoreMeasureStyleTemplate.PaddingRight);
    }

    public void Restore()
    {
        _KeySignature.Reset();
        _PaddingBottom.Reset();
    }

    public void ApplyMemento(ScoreMeasureLayoutModel? memento)
    {
        ApplyMemento(memento as ScoreMeasureLayoutMembers);
    }
    public void ApplyMemento(ScoreMeasureLayoutMembers? memento)
    {
        Restore();
        if (memento is null)
        {
            return;
        }

        _KeySignature.Field = memento.KeySignature?.Convert();
        _PaddingBottom.Field = memento.PaddingBottom;
    }
}

internal class AuthorScoreMeasureLayout : ScoreMeasureLayout
{
    public override ValueTemplateProperty<KeySignature> _KeySignature { get; }
    public override NullableTemplateProperty<double> _PaddingBottom { get; }

    internal AuthorScoreMeasureLayout(ScoreMeasureStyleTemplate scoreMeasureStyleTemplate) : base(scoreMeasureStyleTemplate)
    {
        _KeySignature = new ValueTemplateProperty<KeySignature>(() => new KeySignature(new Step(0, 0), MajorOrMinor.Major));

        _PaddingBottom = new NullableTemplateProperty<double>(() => null);
    }

    public ScoreMeasureLayoutMembers GetMemento()
    {
        return new ScoreMeasureLayoutMembers()
        {
            KeySignature = _KeySignature.Field?.Convert(),
            PaddingBottom = _PaddingBottom.Field,
        };
    }
}

internal class UserScoreMeasureLayout : ScoreMeasureLayout
{
    private readonly Guid id;

    public override ValueTemplateProperty<KeySignature> _KeySignature { get; }
    public override NullableTemplateProperty<double> _PaddingBottom { get; }


    public Guid Id => id;



    public UserScoreMeasureLayout(Guid id, AuthorScoreMeasureLayout primaryScoreMeasureLayout, ScoreMeasureStyleTemplate scoreMeasureStyleTemplate) : base(scoreMeasureStyleTemplate)
    {
        this.id = id;

        _KeySignature = new ValueTemplateProperty<KeySignature>(() => primaryScoreMeasureLayout.KeySignature);
        _PaddingBottom = new NullableTemplateProperty<double>(() => primaryScoreMeasureLayout.PaddingBottom);
    }
}