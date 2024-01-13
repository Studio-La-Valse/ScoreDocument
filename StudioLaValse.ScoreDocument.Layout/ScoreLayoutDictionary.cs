using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout
{
    public class ScoreLayoutDictionary : IScoreLayoutDictionary
    {
        private readonly Dictionary<IScoreDocument, IScoreDocumentLayout> scoreDocumentLayoutDictionary = [];

        private readonly Dictionary<INote, IMeasureElementLayout> measureElementLayoutDictionary = [];
        private readonly Dictionary<IChord, IMeasureElementContainerLayout> chordLayoutDictionary = [];
        private readonly Dictionary<IMeasureBlockReader, INoteGroupLayout> chordGroupReaderDictionary = [];

        private readonly Dictionary<IRibbonMeasure, IRibbonMeasureLayout> instrumentMeasureLayoutDictionary = [];
        private readonly Dictionary<IScoreMeasure, IScoreMeasureLayout> scoreMeasureLayoutDictionary = [];
        private readonly Dictionary<IInstrumentRibbon, IInstrumentRibbonLayout> instrumentRibbonLayoutDictionary = [];

        private readonly Dictionary<IStaff, IStaffLayout> staffLayoutDictionary = [];
        private readonly Dictionary<IStaffGroup, IStaffGroupLayout> staffGroupLayoutDictionary = [];
        private readonly Dictionary<IStaffSystem, IStaffSystemLayout> staffSystemLayoutDictionary = [];


        private ScoreLayoutDictionary()
        {

        }


        public static IScoreLayoutDictionary CreateDefault()
        {
            return new ScoreLayoutDictionary();
        }

        
        
        
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
        public void Assign(INote element, IMeasureElementLayout layout)
        {
            measureElementLayoutDictionary[element] = layout;
        }

        public IMeasureElementContainerLayout GetOrCreate(IChord element)
        {
            if (chordLayoutDictionary.TryGetValue(element, out var value))
            {
                return value;
            }

            var layout = new MeasureElementContainerLayout();
            chordLayoutDictionary.Add(element, layout);

            return chordLayoutDictionary[element];
        }
        public void Assign(IChord chord, IMeasureElementContainerLayout layout)
        {
            chordLayoutDictionary[chord] = layout;
        }

        public INoteGroupLayout GetOrCreate(IMeasureBlockReader reader)
        {
            if(chordGroupReaderDictionary.TryGetValue(reader, out var value))
            {
                return value;
            }

            var layout = new NoteGroupLayout();
            chordGroupReaderDictionary[reader] = layout;
            return chordGroupReaderDictionary[reader];
        }
        public void Assign(IMeasureBlockReader chordGroup, INoteGroupLayout layout)
        {
            chordGroupReaderDictionary[chordGroup] = layout;
        }

        public IRibbonMeasureLayout GetOrCreate(IRibbonMeasure element)
        {
            if (instrumentMeasureLayoutDictionary.TryGetValue(element, out var value))
            {
                return value;
            }

            var layout = new RibbonMeasureLayout();
            instrumentMeasureLayoutDictionary.Add(element, layout);

            return instrumentMeasureLayoutDictionary[element];
        }
        public void Assign(IRibbonMeasure ribbonMeasure, IRibbonMeasureLayout layout)
        {
            instrumentMeasureLayoutDictionary[ribbonMeasure] = layout;
        }

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
        public void Assign(IScoreMeasure scoreMeasure, IScoreMeasureLayout layout)
        {
            scoreMeasureLayoutDictionary[scoreMeasure] = layout;
        }

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
        public void Assign(IInstrumentRibbon instrumentRibbon, IInstrumentRibbonLayout layout)
        {
            instrumentRibbonLayoutDictionary[instrumentRibbon] = layout;
        }

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
        public void Assign(IStaff staffReader, IStaffLayout layout)
        {
            staffLayoutDictionary[staffReader] = layout;
        }

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
        public void Assign(IStaffGroup staffGroup, IStaffGroupLayout layout)
        {
            staffGroupLayoutDictionary[staffGroup] = layout;
        }

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
        public void Assign(IStaffSystem staffSystem, IStaffSystemLayout layout)
        {
            staffSystemLayoutDictionary[staffSystem] = layout;
        }

        public IScoreDocumentLayout GetOrCreate(IScoreDocument scoreDocument)
        {
            throw new NotImplementedException();
        }
        public void Assign(IScoreDocument scoreDocument, IScoreDocumentLayout layout)
        {
            scoreDocumentLayoutDictionary[scoreDocument] = layout;
        }
    }
}
