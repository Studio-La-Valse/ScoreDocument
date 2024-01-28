using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The default implementation of the score layout dictionary.
    /// </summary>
    public class ScoreLayoutDictionary : IScoreLayoutDictionary
    {
        private readonly Dictionary<IScoreDocument, IScoreDocumentLayout> scoreDocumentLayoutDictionary = [];

        private readonly Dictionary<INote, IMeasureElementLayout> measureElementLayoutDictionary = [];
        private readonly Dictionary<IChord, IChordLayout> chordLayoutDictionary = [];
        private readonly Dictionary<IMeasureBlock, INoteGroupLayout> chordGroupReaderDictionary = [];

        private readonly Dictionary<IInstrumentMeasure, IInstrumentMeasureLayout> instrumentMeasureLayoutDictionary = [];
        private readonly Dictionary<IScoreMeasure, IScoreMeasureLayout> scoreMeasureLayoutDictionary = [];
        private readonly Dictionary<IInstrumentRibbon, IInstrumentRibbonLayout> instrumentRibbonLayoutDictionary = [];

        private readonly Dictionary<IStaff, IStaffLayout> staffLayoutDictionary = [];
        private readonly Dictionary<IStaffGroup, IStaffGroupLayout> staffGroupLayoutDictionary = [];
        private readonly Dictionary<IStaffSystem, IStaffSystemLayout> staffSystemLayoutDictionary = [];


        private ScoreLayoutDictionary()
        {

        }

        /// <summary>
        /// Create the default implementation.
        /// </summary>
        /// <returns></returns>
        public static IScoreLayoutDictionary CreateDefault()
        {
            return new ScoreLayoutDictionary();
        }



        /// <inheritdoc/>
        public IMeasureElementLayout GetOrCreate(INote element)
        {
            if (measureElementLayoutDictionary.TryGetValue(element, out var value))
            {
                return value;
            }

            var layout = new MeasureElementLayout();
            measureElementLayoutDictionary.Add(element, layout);

            return measureElementLayoutDictionary[element];
        }
        /// <inheritdoc/>
        public void Assign(INote element, IMeasureElementLayout layout)
        {
            measureElementLayoutDictionary[element] = layout;
        }


        /// <inheritdoc/>
        public IChordLayout GetOrCreate(IChord element)
        {
            if (chordLayoutDictionary.TryGetValue(element, out var value))
            {
                return value;
            }

            var layout = new ChordLayout();
            chordLayoutDictionary.Add(element, layout);

            return chordLayoutDictionary[element];
        }
        /// <inheritdoc/>
        public void Assign(IChord chord, IChordLayout layout)
        {
            chordLayoutDictionary[chord] = layout;
        }


        /// <inheritdoc/>
        public INoteGroupLayout GetOrCreate(IMeasureBlock reader)
        {
            if (chordGroupReaderDictionary.TryGetValue(reader, out var value))
            {
                return value;
            }

            var layout = new NoteGroupLayout();
            chordGroupReaderDictionary[reader] = layout;
            return chordGroupReaderDictionary[reader];
        }
        /// <inheritdoc/>
        public void Assign(IMeasureBlock chordGroup, INoteGroupLayout layout)
        {
            chordGroupReaderDictionary[chordGroup] = layout;
        }


        /// <inheritdoc/>
        public IInstrumentMeasureLayout GetOrCreate(IInstrumentMeasure element)
        {
            if (instrumentMeasureLayoutDictionary.TryGetValue(element, out var value))
            {
                return value;
            }

            var layout = new InstrumentMeasureLayout();
            instrumentMeasureLayoutDictionary.Add(element, layout);

            return instrumentMeasureLayoutDictionary[element];
        }
        /// <inheritdoc/>
        public void Assign(IInstrumentMeasure ribbonMeasure, IInstrumentMeasureLayout layout)
        {
            instrumentMeasureLayoutDictionary[ribbonMeasure] = layout;
        }


        /// <inheritdoc/>
        public IScoreMeasureLayout GetOrCreate(IScoreMeasure element)
        {
            if (scoreMeasureLayoutDictionary.TryGetValue(element, out var value))
            {
                return value;
            }

            var layout = new ScoreMeasureLayout();

            scoreMeasureLayoutDictionary.Add(element, layout);
            return scoreMeasureLayoutDictionary[element];
        }
        /// <inheritdoc/>
        public void Assign(IScoreMeasure scoreMeasure, IScoreMeasureLayout layout)
        {
            scoreMeasureLayoutDictionary[scoreMeasure] = layout;
        }


        /// <inheritdoc/>
        public IInstrumentRibbonLayout GetOrCreate(IInstrumentRibbon element)
        {
            if (instrumentRibbonLayoutDictionary.TryGetValue(element, out var value))
            {
                return value;
            }

            var instrument = element.Instrument;
            var layout = new InstrumentRibbonLayout(instrument);

            instrumentRibbonLayoutDictionary.Add(element, layout);
            return instrumentRibbonLayoutDictionary[element];
        }
        /// <inheritdoc/>
        public void Assign(IInstrumentRibbon instrumentRibbon, IInstrumentRibbonLayout layout)
        {
            instrumentRibbonLayoutDictionary[instrumentRibbon] = layout;
        }


        /// <inheritdoc/>
        public IStaffLayout GetOrCreate(IStaff element)
        {
            if (staffLayoutDictionary.TryGetValue(element, out var layout))
            {
                return layout;
            }

            layout = new StaffLayout();
            staffLayoutDictionary.Add(element, layout);
            return staffLayoutDictionary[element];
        }
        /// <inheritdoc/>
        public void Assign(IStaff staffReader, IStaffLayout layout)
        {
            staffLayoutDictionary[staffReader] = layout;
        }


        /// <inheritdoc/>
        public IStaffGroupLayout GetOrCreate(IStaffGroup element)
        {
            if (staffGroupLayoutDictionary.TryGetValue(element, out var layout))
            {
                return layout;
            }

            layout = new StaffGroupLayout(element.Instrument);
            staffGroupLayoutDictionary.Add(element, layout);
            return staffGroupLayoutDictionary[element];
        }
        /// <inheritdoc/>
        public void Assign(IStaffGroup staffGroup, IStaffGroupLayout layout)
        {
            staffGroupLayoutDictionary[staffGroup] = layout;
        }


        /// <inheritdoc/>
        public IStaffSystemLayout GetOrCreate(IStaffSystem element)
        {
            if (staffSystemLayoutDictionary.TryGetValue(element, out var layout))
            {
                return layout;
            }

            layout = new StaffSystemLayout();
            staffSystemLayoutDictionary.Add(element, layout);
            return staffSystemLayoutDictionary[element];
        }
        /// <inheritdoc/>
        public void Assign(IStaffSystem staffSystem, IStaffSystemLayout layout)
        {
            staffSystemLayoutDictionary[staffSystem] = layout;
        }


        /// <inheritdoc/>
        public IScoreDocumentLayout GetOrCreate(IScoreDocument scoreDocument)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc/>
        public void Assign(IScoreDocument scoreDocument, IScoreDocumentLayout layout)
        {
            scoreDocumentLayoutDictionary[scoreDocument] = layout;
        }
    }
}
