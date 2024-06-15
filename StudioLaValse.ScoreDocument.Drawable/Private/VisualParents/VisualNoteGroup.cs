﻿using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.Private.Interfaces;
using StudioLaValse.ScoreDocument.Drawable.Scenes;
using StudioLaValse.ScoreDocument.Reader.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal class VisualNoteGroup : BaseContentWrapper
    {
        private readonly IMeasureBlockReader measureBlock;
        private readonly IStaffGroupReader staffGroup;
        private readonly IInstrumentMeasureReader instrumentMeasure;
        private readonly IReadOnlyDictionary<Position, double> positionDictionary;
        private readonly double canvasTopStaffGroup;
        private readonly double globalLineSpacing;
        private readonly double positionSpaceing;
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly IVisualBeamBuilder visualBeamBuilder;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;
        private readonly IVisualNoteGroupFactory visualNoteGroupFactory;
        private readonly IUnitToPixelConverter unitToPixelConverter;

        public IMeasureBlockLayout Layout =>
            measureBlock.ReadLayout();
        public double StemThickness =>
            unitToPixelConverter.UnitsToPixels(scoreLayoutDictionary.StemLineThickness * Scale);
        public double Scale => scoreScale * instrumentScale;


        public VisualNoteGroup(IMeasureBlockReader measureBlock,
                               IStaffGroupReader staffGroup,
                               IInstrumentMeasureReader instrumentMeasure,
                               IReadOnlyDictionary<Position, double> positionDictionary,
                               double canvasTopStaffGroup,
                               double globalLineSpacing,
                               double positionSpaceing,
                               double scoreScale,
                               double instrumentScale,
                               IVisualNoteFactory noteFactory,
                               IVisualRestFactory restFactory,
                               IVisualBeamBuilder visualBeamBuilder,
                               IScoreDocumentLayout scoreLayoutDictionary,
                               IVisualNoteGroupFactory visualNoteGroupFactory,
                               IUnitToPixelConverter unitToPixelConverter)
        {
            this.measureBlock = measureBlock;
            this.staffGroup = staffGroup;
            this.instrumentMeasure = instrumentMeasure;
            this.positionDictionary = positionDictionary;
            this.canvasTopStaffGroup = canvasTopStaffGroup;
            this.globalLineSpacing = globalLineSpacing;
            this.positionSpaceing = positionSpaceing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.visualBeamBuilder = visualBeamBuilder;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            this.visualNoteGroupFactory = visualNoteGroupFactory;
            this.unitToPixelConverter = unitToPixelConverter;
        }



        public IEnumerable<BaseContentWrapper> Create()
        {
            var chords = measureBlock.ReadChords().ToArray();
            if (chords.Length == 0)
            {
                yield break;
            }

            foreach(var chord in CreateVisualChords(chords))
            {
                yield return chord;
            }

            foreach(var stemOrBeam in CreateVisualBeamGroup(chords))
            {
                yield return stemOrBeam;
            }
        }
        public IEnumerable<BaseContentWrapper> CreateVisualChords(IEnumerable<IChordReader> chords)
        {
            foreach (var chord in chords)
            {
                var canvasLeft = positionDictionary[chord.Position];

                yield return new VisualChord(chord,
                    canvasLeft,
                    canvasTopStaffGroup,
                    globalLineSpacing,
                    positionSpaceing,
                    scoreScale,
                    instrumentScale,
                    staffGroup,
                    instrumentMeasure,
                    noteFactory,
                    restFactory,
                    scoreLayoutDictionary, 
                    unitToPixelConverter);

                var _graceGroup = chord.ReadGraceGroup();
                if (_graceGroup is null)
                {
                    continue;
                }

                var gracePositions = CreateDictionary(_graceGroup, canvasLeft);
                var graceGroup = visualNoteGroupFactory.Build(_graceGroup.Cast(), staffGroup, instrumentMeasure, gracePositions, canvasTopStaffGroup, globalLineSpacing, positionSpaceing);
                yield return graceGroup;
            }
        }
        public IEnumerable<BaseContentWrapper> CreateVisualBeamGroup(IEnumerable<IChordReader> chords)
        {
            if (!chords.Any() || chords.First().RythmicDuration.Decimal >= 1)
            {
                yield break;
            }

            var principalNote = GlyphLibrary.NoteHeadBlack;
            principalNote.Scale = Scale;
            var principalStemDirection = measureBlock.ReadLayout().StemDirection;
            var principalChord = PickAChordForStem(chords, principalStemDirection);

            var principalNoteWidth = principalNote.Width;
            var principalStemLength = Layout.StemLength * (principalStemDirection == StemDirection.Down ? -1 : 1);
            var principalStemUp = principalStemLength > 0;
            var principalChordCanvasLeft = positionDictionary[principalChord.Position];
            var principalStemOrigin = ConstructStemOrigin(principalChord, staffGroup, canvasTopStaffGroup, principalChordCanvasLeft, principalStemUp, principalNoteWidth);
            var (highestNote, lowestNote) = ConstructChordCanvasY(principalChord, staffGroup, canvasTopStaffGroup);
            var principalStemTipY = (principalStemUp ? highestNote : lowestNote) - (principalStemLength * Scale);
            var principalStemTip = new XY(principalStemOrigin.X, principalStemTipY);
            var principalStem = new VisualStem(principalStemOrigin, principalStemTip, StemThickness, principalChord, scoreLayoutDictionary);

            var beamDefinition = new Ruler(principalStemTip, -Layout.BeamAngle);
            var stems = chords
                .Select(c => c.Equals(principalChord) ? principalStem : CreateStem(c, principalNoteWidth, beamDefinition))
                .ToArray();

            yield return new VisualBeamGroup(stems, beamDefinition, Layout.BeamThickness, Layout.BeamSpacing, Scale, positionSpaceing, visualBeamBuilder);
        }
        public VisualStem CreateStem(IChordReader chord, double firstNoteWidth, Ruler beamDefinition)
        {
            var canvasLeft = positionDictionary[chord.Position];
            var chordOrigin = ConstructStemOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, true, firstNoteWidth);
            var stemIntersection = beamDefinition.IntersectVerticalRay(chordOrigin);
            var stemUp = stemIntersection.Y < chordOrigin.Y;

            var stemOrigin = ConstructStemOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, stemUp, firstNoteWidth);
            stemIntersection = beamDefinition.IntersectVerticalRay(stemOrigin);

            var visualStem = new VisualStem(stemOrigin, stemIntersection, StemThickness, chord, scoreLayoutDictionary);
            return visualStem;
        }
        public IChordReader PickAChordForStem(IEnumerable<IChordReader> chords, StemDirection stemDirection)
        {
            return stemDirection switch
            {
                StemDirection.Cross => chords.First(),
                StemDirection.Down => chords
                    .OrderByDescending(c => c.ReadNotes().Select(n => n.ReadLayout().StaffIndex).Max())
                    .ThenBy(c => c.ReadNotes().Select(n => n.Pitch.IndexOnKlavier).Max())
                    .First(),
                StemDirection.Up => chords
                    .OrderByDescending(c => c.ReadNotes().Select(n => n.ReadLayout().StaffIndex).Max())
                    .ThenBy(c => c.ReadNotes().Select(n => n.Pitch.IndexOnKlavier).Max())
                    .Last(),
                _ => throw new NotImplementedException()
            };
        }
        public XY ConstructStemOrigin(IChordReader chord, IStaffGroupReader staffGroup, double staffGroupCanvasTop, double chordCanvasLeft, bool stemUp, double noteWidth)
        {
            var (highestNote, lowestNote) = ConstructChordCanvasY(chord, staffGroup, staffGroupCanvasTop);
            var canvasY = stemUp ? lowestNote : highestNote;
            var offset = stemUp ?
                (noteWidth / 2) - (StemThickness / 2) :
                (-noteWidth / 2) + (StemThickness / 2);
            var offsetToNeatlyFitNoteHead = unitToPixelConverter.UnitsToPixels((globalLineSpacing * scoreScale * instrumentScale * 0.17 * (stemUp ? -1 : 1)));
            return new XY(chordCanvasLeft + offset, canvasY + offsetToNeatlyFitNoteHead);
        }
        public (double highestNote, double lowestNote) ConstructChordCanvasY(IChordReader chord, IStaffGroupReader staffGroup, double staffGroupCanvasTop)
        {
            var staffGroupLayout = staffGroup.ReadLayout();

            if (!chord.ReadNotes().Any())
            {
                var line = 4;
                var heightOnCanvas = staffGroupCanvasTop + unitToPixelConverter.UnitsToPixels(staffGroup.EnumerateStaves().First().DistanceFromTop(line, globalLineSpacing, scoreScale, instrumentScale));
                return (heightOnCanvas, heightOnCanvas);
            }

            var lowestNote = chord
                .ReadNotes()
                .OrderByDescending(note => note.ReadLayout().StaffIndex)
                .ThenBy(n => n.Pitch.IndexOnKlavier)
                .First();

            var highestNote = chord
                .ReadNotes()
                .OrderBy(note => note.ReadLayout().StaffIndex)
                .ThenByDescending(n => n.Pitch.IndexOnKlavier)
                .First();

            var lowestNoteCanvasY = CreateNoteCanvasY(lowestNote, staffGroupCanvasTop);
            var highestNoteCanvasY = CreateNoteCanvasY(highestNote, staffGroupCanvasTop);

            return (highestNoteCanvasY, lowestNoteCanvasY);
        }
        public double CreateNoteCanvasY(INoteReader noteReader, double staffGroupCanvasTop)
        {
            var noteLayout = noteReader.ReadLayout();
            var noteStaffIndex = noteLayout.StaffIndex;
            var clef = instrumentMeasure.GetClef(noteStaffIndex, noteReader.Position);
            var lineIndex = clef.LineIndexAtPitch(noteReader.Pitch);
            var heightOriginOnCanvas = staffGroupCanvasTop + unitToPixelConverter.UnitsToPixels(staffGroup.DistanceFromTop(noteStaffIndex, lineIndex, globalLineSpacing, scoreLayoutDictionary));
            return heightOriginOnCanvas;
        }
        public Dictionary<Position, double> CreateDictionary(IGraceGroupReader graceGroup, double target)
        {
            var dictionary = new Dictionary<Position, double>(new PositionComparer());
            var layout = graceGroup.ReadLayout();
            var position = graceGroup.Target;
            foreach(var chord in graceGroup.ReadChords().Reverse())
            {
                target -= layout.ChordSpacing * layout.Scale;
                position -= layout.ChordDuration;
                dictionary.Add(position, target);
            }
            return dictionary;
        }


        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            yield break;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return Create();
        }
    }

    file class PositionComparer : IEqualityComparer<Position>
    {
        public bool Equals(Position? x, Position? y)
        {
            if (x == null || y == null)
            {
                throw new InvalidOperationException();
            }

            return x.Decimal == y.Decimal;
        }

        public int GetHashCode([DisallowNull] Position obj)
        {
            return obj.Decimal.GetHashCode();
        }
    }
}
