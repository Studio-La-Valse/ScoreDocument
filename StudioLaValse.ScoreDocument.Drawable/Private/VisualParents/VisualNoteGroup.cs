using StudioLaValse.ScoreDocument.GlyphLibrary;
using StudioLaValse.ScoreDocument.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal class VisualNoteGroup : BaseContentWrapper
    {
        private readonly IMeasureBlock measureBlock;
        private readonly IStaffGroup staffGroup;
        private readonly IInstrumentMeasure instrumentMeasure;
        private readonly IReadOnlyDictionary<Position, double> positionDictionary;
        private readonly double canvasTopStaffGroup;
        private readonly IGlyphLibrary glyphLibrary;
        private readonly IVisualNoteFactory noteFactory;
        private readonly IVisualRestFactory restFactory;
        private readonly IVisualNoteGroupFactory visualNoteGroupFactory;

        public IMeasureBlock Layout =>
            measureBlock;
        public double StemThickness(IChord chord) =>
            chord.StemLineThickness * Scale;
        public double Scale => measureBlock.Scale;


        public VisualNoteGroup(IMeasureBlock measureBlock,
                               IStaffGroup staffGroup,
                               IInstrumentMeasure instrumentMeasure,
                               IReadOnlyDictionary<Position, double> positionDictionary,
                               double canvasTopStaffGroup,
                               IGlyphLibrary glyphLibrary,
                               IVisualNoteFactory noteFactory,
                               IVisualRestFactory restFactory,
                               IVisualNoteGroupFactory visualNoteGroupFactory)
        {
            this.measureBlock = measureBlock;
            this.staffGroup = staffGroup;
            this.instrumentMeasure = instrumentMeasure;
            this.positionDictionary = positionDictionary;
            this.canvasTopStaffGroup = canvasTopStaffGroup;
            this.glyphLibrary = glyphLibrary;
            this.noteFactory = noteFactory;
            this.restFactory = restFactory;
            this.visualNoteGroupFactory = visualNoteGroupFactory;
        }



        public IEnumerable<BaseContentWrapper> Create()
        {
            var chords = measureBlock.ReadChords().ToArray();
            if (chords.Length == 0)
            {
                yield break;
            }
            var (ruler, pincipalStem) = CreateRuler(chords);
            var stems = new List<VisualStem>();

            foreach(var (chord, stem) in CreateVisualChords(chords, ruler))
            {
                yield return chord;
                if (stem is not null)
                {
                    yield return stem;
                    stems.Add(stem);
                }
            }

            var hookSize = chords.Select(c => c.SpaceRight.Value).Average() / 2;
            var beamGroup = CreateVisualBeamGroup(chords, stems.ToArray(), ruler, hookSize);
            yield return beamGroup;

            foreach (var chord in chords)
            {
                var _graceGroup = chord.ReadGraceGroup();
                if (_graceGroup is null)
                {
                    continue;
                }

                var canvasLeft = positionDictionary[chord.Position];
                yield return CreateGraceGroup(_graceGroup, canvasLeft);
            }
        }
        public (Ruler ruler, VisualStem principalStem) CreateRuler(IEnumerable<IChord> chords)
        {
            var principalNote = glyphLibrary.NoteHeadBlack(Scale);
            var principalStemDirection = measureBlock.StemDirection.Value;
            var principalChord = PickPrincipleChord(chords, principalStemDirection);

            var principalNoteWidth = principalNote.Width();
            var principalStemLength = Layout.StemLength * (principalStemDirection == StemDirection.Down ? -1 : 1);
            var principalStemUp = principalStemLength > 0;
            var principalChordCanvasLeft = positionDictionary[principalChord.Position];
            var principalStemOrigin = ConstructStemOrigin(principalChord, staffGroup, canvasTopStaffGroup, principalChordCanvasLeft, principalStemUp, principalNoteWidth);
            var (highestNote, lowestNote) = ConstructChordCanvasY(principalChord, staffGroup, canvasTopStaffGroup);
            var principalStemTipY = (principalStemUp ? highestNote : lowestNote) - (principalStemLength * Scale);
            var principalStemTip = new XY(principalStemOrigin.X, principalStemTipY);
            var principalStem = new VisualStem(principalStemOrigin, principalStemTip, StemThickness(principalChord), principalChord);

            var beamDefinition = new Ruler(principalStemTip, -Layout.BeamAngle, principalStem);
            return (beamDefinition, principalStem);
        }
        public IEnumerable<(BaseContentWrapper, VisualStem?)> CreateVisualChords(IEnumerable<IChord> chords, Ruler ruler)
        {
            foreach (var chord in chords)
            {
                var canvasLeft = positionDictionary[chord.Position];
                var drawDirection = true;
                VisualStem? visualStem = null;

                if(chord.RythmicDuration.Decimal < 1)
                {
                    var noteWidth = glyphLibrary.NoteHeadBlack(Scale).Width();
                    visualStem = ruler.PrincipalStem.Chord.Equals(chord) ?
                        ruler.PrincipalStem :
                        CreateStem(chord, noteWidth, ruler);

                    drawDirection = visualStem.VisuallyUp;
                }

                var visualChord = new VisualChord(chord,
                    canvasLeft,
                    canvasTopStaffGroup,
                    staffGroup,
                    instrumentMeasure,
                    noteFactory,
                    restFactory,
                    glyphLibrary,
                    drawDirection);

                yield return (visualChord, visualStem);
            }
        }
        public BaseContentWrapper CreateGraceGroup(IGraceGroup graceGroup, double hostCanvasLeft)
        {
            var gracePositions = CreateDictionary(graceGroup, hostCanvasLeft);
            var visualGraceGroup = visualNoteGroupFactory.Build(graceGroup.Imply(), staffGroup, instrumentMeasure, gracePositions, canvasTopStaffGroup);
            return visualGraceGroup;
        }
        

        public BaseContentWrapper CreateVisualBeamGroup(IEnumerable<IChord> chords, VisualStem[] visualStems, Ruler beamDefinition, double hookSize)
        {
            var color = staffGroup.Color.Value.FromPrimitive();
            return new VisualBeamGroup(
                visualStems,
                beamDefinition,
                Layout.BeamThickness,
                Layout.BeamSpacing,
                Scale,
                hookSize,
                color,
                glyphLibrary
            );
        }
        public VisualStem CreateStem(IChord chord, double firstNoteWidth, Ruler beamDefinition)
        {
            var canvasLeft = positionDictionary[chord.Position];
            var chordOrigin = ConstructStemOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, true, firstNoteWidth);
            var stemIntersection = beamDefinition.IntersectVerticalRay(chordOrigin);
            var stemUp = stemIntersection.Y < chordOrigin.Y;

            var stemOrigin = ConstructStemOrigin(chord, staffGroup, canvasTopStaffGroup, canvasLeft, stemUp, firstNoteWidth);
            stemIntersection = beamDefinition.IntersectVerticalRay(stemOrigin);

            var visualStem = new VisualStem(stemOrigin, stemIntersection, StemThickness(chord), chord);
            return visualStem;
        }
        public IChord PickPrincipleChord(IEnumerable<IChord> chords, StemDirection stemDirection)
        {
            return stemDirection switch
            {
                StemDirection.Cross => chords.First(),
                StemDirection.Down => chords
                    .OrderByDescending(c => c.ReadNotes().Select(n => n.StaffIndex.Value).Max())
                    .ThenBy(c => c.ReadNotes().Select(n => n.Pitch.IndexOnKlavier).Max())
                    .First(),
                StemDirection.Up => chords
                    .OrderByDescending(c => c.ReadNotes().Select(n => n.StaffIndex.Value).Max())
                    .ThenBy(c => c.ReadNotes().Select(n => n.Pitch.IndexOnKlavier).Max())
                    .Last(),
                _ => throw new NotImplementedException()
            };
        }
        public XY ConstructStemOrigin(IChord chord, IStaffGroup staffGroup, double staffGroupCanvasTop, double chordCanvasLeft, bool stemUp, double noteWidth)
        {
            var (highestNote, lowestNote) = ConstructChordCanvasY(chord, staffGroup, staffGroupCanvasTop);
            var canvasY = stemUp ? lowestNote : highestNote;
            var offset = stemUp ?
                (noteWidth / 2) - (StemThickness(chord) / 2) :
                (-noteWidth / 2) + (StemThickness(chord) / 2);
            var offsetToNeatlyFitNoteHead = Glyph.LineSpacing * Scale * 0.17 * (stemUp ? -1 : 1);
            return new XY(chordCanvasLeft + offset, canvasY + offsetToNeatlyFitNoteHead);
        }
        public (double highestNote, double lowestNote) ConstructChordCanvasY(IChord chord, IStaffGroup staffGroup, double staffGroupCanvasTop)
        {
            var staffGroupLayout = staffGroup;

            if (!chord.ReadNotes().Any())
            {
                var line = 4;
                var heightOnCanvas = staffGroupCanvasTop + staffGroup.EnumerateStaves().First().DistanceFromTop(line);
                return (heightOnCanvas, heightOnCanvas);
            }

            var lowestNote = chord
                .ReadNotes()
                .OrderByDescending(note => note.StaffIndex.Value)
                .ThenBy(n => n.Pitch.IndexOnKlavier)
                .First();

            var highestNote = chord
                .ReadNotes()
                .OrderBy(note => note.StaffIndex.Value)
                .ThenByDescending(n => n.Pitch.IndexOnKlavier)
                .First();

            var lowestNoteCanvasY = CreateNoteCanvasY(lowestNote, staffGroupCanvasTop);
            var highestNoteCanvasY = CreateNoteCanvasY(highestNote, staffGroupCanvasTop);

            return (highestNoteCanvasY, lowestNoteCanvasY);
        }
        public double CreateNoteCanvasY(INote noteReader, double staffGroupCanvasTop)
        {
            var noteLayout = noteReader;
            var noteStaffIndex = noteLayout.StaffIndex;
            var clef = instrumentMeasure.GetClef(noteStaffIndex, noteReader.Position);
            var lineIndex = clef.LineIndexAtPitch(noteReader.Pitch);
            var heightOriginOnCanvas = staffGroupCanvasTop + staffGroup.DistanceFromTop(noteStaffIndex, lineIndex);
            return heightOriginOnCanvas;
        }
        public Dictionary<Position, double> CreateDictionary(IGraceGroup graceGroup, double target)
        {
            var dictionary = new Dictionary<Position, double>(new PositionComparer());
            var layout = graceGroup;
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
