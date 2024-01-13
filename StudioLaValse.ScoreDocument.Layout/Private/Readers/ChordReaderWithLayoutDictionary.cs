using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Collections;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class ChordReaderWithLayoutDictionary : IChordReader
    {
        private readonly IChordReader chordReader;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public ChordReaderWithLayoutDictionary(IChordReader chordReader, IScoreLayoutDictionary layoutDictionary)
        {
            this.chordReader = chordReader;
            this.layoutDictionary = layoutDictionary;
        }

        public int Voice => chordReader.Voice;

        public bool Grace => chordReader.Grace;

        public Position Position => chordReader.Position;

        public RythmicDuration RythmicDuration => chordReader.RythmicDuration;

        public Tuplet Tuplet => chordReader.Tuplet;

        public int Id => chordReader.Id;

        public int IndexInBlock => chordReader.IndexInBlock;

        public IEnumerable<INote> EnumerateNotes()
        {
            return chordReader.EnumerateNotes();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return chordReader.Equals(other);
        }

        public IRibbonMeasureReader ReadContext()
        {
            return chordReader.ReadContext().UseLayout(layoutDictionary);
        }

        public IMeasureElementContainerLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(chordReader);
        }

        public IChordReader? ReadNext()
        {
            return chordReader.ReadNext()?.UseLayout(layoutDictionary);
        }

        public IEnumerable<INoteReader> ReadNotes()
        {
            return chordReader.ReadNotes().Select(e => e.UseLayout(layoutDictionary));
        }

        public IChordReader? ReadPrevious()
        {
            return chordReader.ReadPrevious()?.UseLayout(layoutDictionary);
        }
    }
}
