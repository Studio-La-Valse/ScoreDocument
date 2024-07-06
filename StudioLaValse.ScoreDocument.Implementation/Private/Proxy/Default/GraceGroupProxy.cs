using StudioLaValse.ScoreDocument.Implementation.Private;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.Default
{
    internal class GraceGroupProxy : IGraceGroup
    {
        private readonly GraceGroup graceGroup;
        private readonly ILayoutSelector layoutSelector;


        public IGraceGroupLayout Layout => layoutSelector.GraceGroupLayout(graceGroup);


        public int Id => graceGroup.Id;

        public int Length => graceGroup.Length;

        public Position Target => graceGroup.Target;



        public TemplateProperty<RythmicDuration> BlockDuration => Layout.ChordDuration;

        public TemplateProperty<bool> OccupySpace => Layout.OccupySpace;

        public TemplateProperty<double> ChordSpacing => Layout.ChordSpacing;

        public TemplateProperty<RythmicDuration> ChordDuration => Layout.ChordDuration;

        public TemplateProperty<double> Scale => Layout.Scale;

        public TemplateProperty<StemDirection> StemDirection => Layout.StemDirection;

        public TemplateProperty<double> StemLength => Layout.StemLength;

        public TemplateProperty<double> BeamAngle => Layout.BeamAngle;

        public ReadonlyTemplateProperty<double> BeamThickness => Layout.BeamThickness;

        public ReadonlyTemplateProperty<double> BeamSpacing => Layout.BeamSpacing;



        public GraceGroupProxy(GraceGroup graceGroup, ILayoutSelector layoutSelector)
        {
            this.graceGroup = graceGroup;
            this.layoutSelector = layoutSelector;
        }



        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadChords();
        }

        public IEnumerable<IGraceChord> ReadChords()
        {
            return graceGroup.Chords.Select(c => c.Proxy(layoutSelector));
        }

        public void Splice(int index)
        {
            graceGroup.Splice(index);
        }

        public void AppendChord(params Pitch[] pitches)
        {
            graceGroup.Append(pitches);
        }

        public void Clear()
        {
            graceGroup.Clear();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return other is not null && other.Id == Id;
        }
    }
}
