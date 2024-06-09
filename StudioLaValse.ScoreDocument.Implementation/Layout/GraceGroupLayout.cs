﻿using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.Templates;
using StudioLaValse.ScoreDocument.Models.Base;
using System;

namespace StudioLaValse.ScoreDocument.Implementation
{
    public abstract class GraceGroupLayout 
    {
        protected abstract ValueTemplateProperty<bool> _OccupySpace { get; }
        protected abstract ValueTemplateProperty<double> _ChordSpacing { get; }
        protected abstract ReferenceTemplateProperty<RythmicDuration> _ChordDuration { get; }
        protected abstract ValueTemplateProperty<double> _Scale { get; }
        protected abstract ValueTemplateProperty<double> _StemLength { get; }
        protected abstract ValueTemplateProperty<double> _BeamAngle { get; }
        protected abstract ValueTemplateProperty<double> _BeamThickness { get; }
        protected abstract ValueTemplateProperty<double> _BeamSpacing { get; }

        public bool OccupySpace
        {
            get => _OccupySpace.Value;
            set => _OccupySpace.Value = value;
        }
        public double ChordSpacing
        {
            get => _ChordSpacing.Value;
            set => _ChordSpacing.Value = value;
        }
        public RythmicDuration ChordDuration
        {
            get => _ChordDuration.Value;
            set => _ChordDuration.Value = value;
        }
        public double Scale
        {
            get => _Scale.Value;
        }
        public double StemLength
        {
            get => _StemLength.Value;
            set => _StemLength.Value = value;
        }
        public double BeamAngle
        {
            get => _BeamAngle.Value;
            set => _BeamAngle.Value = value;
        }
        public double BeamThickness
        {
            get => _BeamThickness.Value;
        }
        public double BeamSpacing
        {
            get => _BeamSpacing.Value;
        }


        public void ApplyMemento(GraceGroupLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            _OccupySpace.Field = memento.OccupySpace;
            _ChordDuration.Field = memento.ChordDuration?.Convert();
            _ChordSpacing.Field = memento.ChordSpacing;
            _BeamAngle.Field = memento.BeamAngle;
            _StemLength.Field = memento.StemLength;
        }
        public void ApplyMemento(GraceGroupLayoutModel? memento)
        {
            ApplyMemento(memento as GraceGroupLayoutMembers);
        }
        public void Restore()
        {
            _OccupySpace.Reset();
            _ChordSpacing.Reset();
            _ChordDuration.Reset();
            _Scale.Reset();
            _StemLength.Reset();
            _BeamAngle.Reset();
            _BeamThickness.Reset();
            _BeamSpacing.Reset();
        }
    }

    public class AuthorGraceGroupLayout : GraceGroupLayout, IGraceGroupLayout, ILayout<GraceGroupLayoutMembers>
    {
        protected override ValueTemplateProperty<bool> _OccupySpace { get; }
        protected override ValueTemplateProperty<double> _ChordSpacing { get; }
        protected override ReferenceTemplateProperty<RythmicDuration> _ChordDuration { get; }
        protected override ValueTemplateProperty<double> _Scale { get; }
        protected override ValueTemplateProperty<double> _StemLength { get; }
        protected override ValueTemplateProperty<double> _BeamAngle { get; }
        protected override ValueTemplateProperty<double> _BeamThickness { get; }
        protected override ValueTemplateProperty<double> _BeamSpacing { get; }

        public AuthorGraceGroupLayout(GraceGroupStyleTemplate graceGroupStyleTemplate)
        {          
            _OccupySpace = new ValueTemplateProperty<bool>(() => graceGroupStyleTemplate.OccupySpace);
            _ChordSpacing = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.ChordSpaceRight);
            _ChordDuration = new ReferenceTemplateProperty<RythmicDuration>(() => new(graceGroupStyleTemplate.ChordDuration));
            _Scale = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.Scale);
            _StemLength = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.BeamAngle);
            _BeamThickness = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.BeamThickness);
            _BeamSpacing = new ValueTemplateProperty<double>(() => graceGroupStyleTemplate.BeamSpacing);
        }

        public GraceGroupLayoutMembers GetMemento()
        {
            return new GraceGroupLayoutMembers()
            {
                BeamAngle = _BeamAngle.Field,
                ChordDuration = _ChordDuration.Field?.Convert(),
                ChordSpacing = _ChordSpacing.Field,
                OccupySpace = _OccupySpace.Field,
                StemLength = _StemLength.Field,
            };
        }
    }

    public class UserGraceGroupLayout : GraceGroupLayout, IGraceGroupLayout
    {
        private readonly Guid guid;


        protected override ValueTemplateProperty<bool> _OccupySpace { get; }
        protected override ValueTemplateProperty<double> _ChordSpacing { get; }
        protected override ReferenceTemplateProperty<RythmicDuration> _ChordDuration { get; }
        protected override ValueTemplateProperty<double> _Scale { get; }
        protected override ValueTemplateProperty<double> _StemLength { get; }
        protected override ValueTemplateProperty<double> _BeamAngle { get; }
        protected override ValueTemplateProperty<double> _BeamThickness { get; }
        protected override ValueTemplateProperty<double> _BeamSpacing { get; }

        public UserGraceGroupLayout(AuthorGraceGroupLayout authorGraceGroupLayout, Guid guid)
        {
            this.guid = guid;

            _OccupySpace = new ValueTemplateProperty<bool>(() => authorGraceGroupLayout.OccupySpace);
            _ChordSpacing = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.ChordSpacing);
            _ChordDuration = new ReferenceTemplateProperty<RythmicDuration>(() => authorGraceGroupLayout.ChordDuration);
            _Scale = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.Scale);
            _StemLength = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.StemLength);
            _BeamAngle = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.BeamAngle);
            _BeamThickness = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.BeamThickness);
            _BeamSpacing = new ValueTemplateProperty<double>(() => authorGraceGroupLayout.BeamSpacing);
        }

        public GraceGroupLayoutModel GetMemento()
        {
            return new GraceGroupLayoutModel()
            {
                Id = guid,
                BeamAngle = _BeamAngle.Field,
                ChordDuration = _ChordDuration.Field?.Convert(),
                ChordSpacing = _ChordSpacing.Field,
                OccupySpace = _OccupySpace.Field,
                StemLength = _StemLength.Field,
            };
        }
    }
}
