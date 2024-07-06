using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private;

internal class ChordReaderFromGraceChord : IChord
{
    private readonly IGraceChord graceChordReader;
    private readonly MeasureBlockReaderFromGraceGroup graceGroup;

    public Position Position
    {
        get
        {
            var nRemaining = graceGroup.Length - graceChordReader.IndexInGroup;
            var distanceToTarget = graceGroup.ChordDuration * nRemaining;
            var position = graceGroup.Target - distanceToTarget;
            return position;
        }
    }

    public RythmicDuration RythmicDuration => graceGroup.ChordDuration;

    public Tuplet Tuplet => graceGroup.Tuplet;

    public int Id => graceChordReader.Id;

    public TemplateProperty<double> XOffset => new TemplatePropertyFromValue<double>(0);

    public TemplateProperty<double> SpaceRight => new TemplatePropertyFromReadonlyTemplateProperty<double>(graceChordReader.SpaceRight);


    public ChordReaderFromGraceChord(IGraceChord graceChordReader, MeasureBlockReaderFromGraceGroup measureBlockReaderFromGraceGroup)
    {
        this.graceChordReader = graceChordReader;
        graceGroup = measureBlockReaderFromGraceGroup;
    }

    public IEnumerable<IScoreElement> EnumerateChildren()
    {
        return graceChordReader.EnumerateChildren();
    }

    public IGraceGroup? ReadGraceGroup()
    {
        return null;
    }

    public IEnumerable<INote> ReadNotes()
    {
        return graceChordReader.ReadNotes().Select(n => new NoteReaderFromGraceChord(n, this));
    }

    public void Grace(RythmicDuration rythmicDuration, params Pitch[] pitches)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<PowerOfTwo, BeamType>> ReadBeamTypes()
    {
        return graceChordReader.ReadBeamTypes();
    }

    public BeamType? ReadBeamType(PowerOfTwo i)
    {
        return graceChordReader.ReadBeamType(i);
    }

    public void Clear()
    {
        graceChordReader.Clear();
    }

    public void Add(params Pitch[] pitches)
    {
        graceChordReader.Add(pitches);
    }

    public void Set(params Pitch[] pitches)
    {
        graceChordReader.Set(pitches);
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
