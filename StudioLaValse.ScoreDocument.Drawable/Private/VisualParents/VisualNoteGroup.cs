﻿using StudioLaValse.ScoreDocument.Drawable.Private.Interfaces;
using StudioLaValse.ScoreDocument.Reader;
using StudioLaValse.ScoreDocument.Reader.Extensions;

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
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly IVisualBeamBuilder visualBeamBuilder;
        private readonly ColorARGB colorARGB;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;

        public IMeasureBlockLayout Layout =>
            measureBlock.ReadLayout();
        public double StemThickness =>
            scoreLayoutDictionary.StemLineThickness * Scale;
        public double Scale
        {
            get
            {
                var scale = (measureBlock.Grace ? 0.5 : 1) * scoreScale * instrumentScale;
                return scale;
            }
        }


        public VisualNoteGroup(IMeasureBlockReader measureBlock,
                               IStaffGroupReader staffGroup,
                               IInstrumentMeasureReader instrumentMeasure,
                               IReadOnlyDictionary<Position, double> positionDictionary,
                               double canvasTopStaffGroup,
                               double globalLineSpacing,
                               double scoreScale,
                               double instrumentScale,
                               IVisualNoteFactory noteFactory,
                               IVisualRestFactory restFactory,
                               IVisualBeamBuilder visualBeamBuilder,
                               ColorARGB colorARGB,
                               IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.measureBlock = measureBlock;
            this.staffGroup = staffGroup;
            this.instrumentMeasure = instrumentMeasure;
            this.positionDictionary = positionDictionary;
            this.canvasTopStaffGroup = canvasTopStaffGroup;
            this.globalLineSpacing = globalLineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.visualBeamBuilder = visualBeamBuilder;
            this.colorARGB = colorARGB;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }



        public IEnumerable<BaseContentWrapper> Create()
        {
            var chords = measureBlock.ReadChords().ToArray();
            if (chords.Length == 0)
            {
                yield break;
            }

            var firstNote = GlyphLibrary.NoteHeadBlack;
            var firstChord = chords[0];
            firstNote.Scale = Scale;

            var firstNoteWidth = firstNote.Width;

            var firstStemUp = Layout.StemLength < 0;
            var firstChordCanvasLeft = positionDictionary[firstChord.Position];
            var firstStemOrigin = ConstructStemOrigin(firstChord, staffGroup, canvasTopStaffGroup, firstChordCanvasLeft, firstStemUp, firstNoteWidth);
            var firstStemTip = new XY(firstStemOrigin.X, firstStemOrigin.Y + (Layout.StemLength * Scale));
            var firstStem = new VisualStem(firstStemOrigin, firstStemTip, StemThickness, firstChord, colorARGB);
            var stems = new List<VisualStem>()
            {
                firstStem
            };

            var beamDefinition = new Ruler(firstStemTip, Layout.BeamAngle);
            var groupLength = chords.Select(c => c.RythmicDuration).Sum().Decimal; 
            var isFirstChord = true;
            foreach (var chord in chords)
            {
                var canvasLeft = positionDictionary[chord.Position];

                yield return new VisualChord(chord, canvasLeft, canvasTopStaffGroup, globalLineSpacing, scoreScale, instrumentScale, staffGroup, instrumentMeasure, noteFactory, restFactory, colorARGB, scoreLayoutDictionary);

                if (isFirstChord)
                {
                    isFirstChord = false;
                    continue;
                }
                
                if (chord.ReadNotes().Any(note => note.RythmicDuration.Decimal <= 0.5M))
                {
                    var chordOrigin = ConstructChordOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, true);
                    var stemIntersection = beamDefinition.IntersectVerticalRay(chordOrigin);
                    var stemUp = stemIntersection.Y < chordOrigin.Y;

                    var stemOrigin = ConstructStemOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, stemUp, firstNoteWidth);
                    stemIntersection = beamDefinition.IntersectVerticalRay(stemOrigin);

                    var visualStem = new VisualStem(stemOrigin, stemIntersection, StemThickness, chord, colorARGB);
                    stems.Add(visualStem);
                }
            }

            // prepare for a cross beam group.
            if(stems.Any(s => s.VisuallyUp && stems.Any(s => !s.VisuallyUp)))
            {
                // if the first stem faces upwards
                // all stems that face downward (visually) should be extended the first beam size.
                if (stems.First().VisuallyUp)
                {
                    for (var i = 0; i < stems.Count; i++)
                    {
                        var stem = stems[i];
                        if (stem.VisuallyUp)
                        {
                            continue;
                        }

                        var newEndPoint = stem.End + new XY(0, Layout.BeamThickness * Scale);
                        stems[i] = new VisualStem(stem.Origin, newEndPoint, StemThickness, stem.Chord, colorARGB);
                    }
                }
                // if the first stem faces downwards
                // all stems that face upward (visually) should be extended the first beam size.
                else
                {
                    for (var i = 0; i < stems.Count; i++)
                    {
                        var stem = stems[i];
                        if (!stem.VisuallyUp)
                        {
                            continue;
                        }

                        var newEndPoint = stem.End - new XY(0, Layout.BeamThickness * Scale);
                        stems[i] = new VisualStem(stem.Origin, newEndPoint, StemThickness, stem.Chord, colorARGB);
                    }
                }
            }

            yield return new VisualBeamGroup(stems, beamDefinition, Layout.BeamThickness, Layout.BeamSpacing, Scale, colorARGB, visualBeamBuilder);
        }
        public XY ConstructStemOrigin(IChordReader chord, IStaffGroupReader staffGroup, double staffGroupCanvasTop, double chordCanvasLeft, bool stemUp, double noteWidth)
        {
            var chordOrigin = ConstructChordOrigin(chord, staffGroup, staffGroupCanvasTop, chordCanvasLeft, stemUp);
            var offset = stemUp ?
                (noteWidth / 2) - (StemThickness / 2) :
                (-noteWidth / 2) + (StemThickness / 2);

            return new XY(chordOrigin.X + offset, chordOrigin.Y);
        }
        public XY ConstructChordOrigin(IChordReader chord, IStaffGroupReader staffGroup, double staffGroupCanvasTop, double chordCanvasLeft, bool stemUp)
        {
            var staffGroupLayout = staffGroup.ReadLayout();

            if (!chord.ReadNotes().Any())
            {
                var line = 4;
                var heightOnCanvas = staffGroup.EnumerateStaves().First().HeightFromLineIndex(staffGroupCanvasTop, line, globalLineSpacing, scoreScale, instrumentScale);
                return new XY(chordCanvasLeft, heightOnCanvas);
            }

            var notesFromVisualHighToLow = chord
                .ReadNotes()
                .OrderBy(note => note.ReadLayout().StaffIndex)
                .ThenByDescending(n => n.Pitch.IndexOnKlavier);

            var anchorNote = stemUp ?
                notesFromVisualHighToLow.Last() :
                notesFromVisualHighToLow.First();

            var noteLayout = anchorNote.ReadLayout();
            var noteStaffIndex = noteLayout.StaffIndex;
            var clef = instrumentMeasure.GetClef(noteStaffIndex, anchorNote.Position);
            var staff = staffGroup.EnumerateStaves().ElementAt(noteStaffIndex);
            var heightOriginOnCanvas = staffGroup.HeightOnCanvas(staffGroupCanvasTop, noteStaffIndex, clef.LineIndexAtPitch(anchorNote.Pitch), globalLineSpacing, scoreLayoutDictionary);
            var offsetToNeatlyFitNoteHead = globalLineSpacing * scoreScale * instrumentScale * 0.17;
            heightOriginOnCanvas += stemUp ?
                -offsetToNeatlyFitNoteHead : offsetToNeatlyFitNoteHead;

            var horizontalPositionStart = chordCanvasLeft;
            return new XY(horizontalPositionStart, heightOriginOnCanvas);
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
}
