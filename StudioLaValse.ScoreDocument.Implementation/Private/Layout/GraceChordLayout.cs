﻿using StudioLaValse.ScoreDocument.Models.Base;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal abstract class GraceChordLayout : IGraceChordLayout
    {
        private readonly IGraceGroupLayout graceGroupLayout;
        private readonly Dictionary<PowerOfTwo, BeamType> beamTypes;

        public ReadonlyTemplateProperty<double> SpaceRight => new ReadonlyTemplatePropertyFromFunc<double>(() => graceGroupLayout.ChordSpacing.Value);

        public GraceChordLayout(IGraceGroupLayout graceGroupLayout, Dictionary<PowerOfTwo, BeamType> beamTypes)
        {
            this.graceGroupLayout = graceGroupLayout;
            this.beamTypes = beamTypes;
        }

        public BeamType? ReadBeamType(PowerOfTwo i)
        {
            return beamTypes.TryGetValue(i, out var value) ? value : null;
        }
        public IEnumerable<KeyValuePair<PowerOfTwo, BeamType>> ReadBeamTypes()
        {
            return beamTypes;
        }


        public void ApplyMemento(GraceChordLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }
        }
        public void ApplyMemento(GraceChordLayoutModel? memento)
        {
            ApplyMemento(memento as GraceChordLayoutMembers);
        }

        public void Restore()
        {

        }
    }

    internal class AuthorGraceChordLayout : GraceChordLayout
    {
        public Dictionary<PowerOfTwo, BeamType> BeamTypes { get; }

        public AuthorGraceChordLayout(IGraceGroupLayout graceGroupLayout, Dictionary<PowerOfTwo, BeamType> beamTypes) : base(graceGroupLayout, beamTypes)
        {
            BeamTypes = beamTypes;
        }
    }

    internal class UserGraceChordLayout : GraceChordLayout
    {
        private readonly Guid guid;
        public Guid Guid => guid;

        public UserGraceChordLayout(Guid guid, IGraceGroupLayout graceGroupLayout, Dictionary<PowerOfTwo, BeamType> beamTypes) : base(graceGroupLayout, beamTypes)
        {
            this.guid = guid;
        }
    }
}
