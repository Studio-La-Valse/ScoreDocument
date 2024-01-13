using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioLaValse.ScoreDocument.Layout.Private.Editors
{
    internal class ScoreDocumentEditorWithLayoutDictionary : IScoreDocumentEditor
    {
        private readonly IScoreDocumentEditor source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public ScoreDocumentEditorWithLayoutDictionary(IScoreDocumentEditor source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public int NumberOfMeasures => source.NumberOfMeasures;

        public int NumberOfInstruments => source.NumberOfInstruments;

        public int Id => source.Id;

        public void AddInstrumentRibbon(Instrument instrument)
        {
            source.AddInstrumentRibbon(instrument);
        }

        public void AppendScoreMeasure(TimeSignature? timeSignature = null)
        {
            source.AppendScoreMeasure(timeSignature);
        }

        public void ApplyLayout(IScoreDocumentLayout layout)
        {
            layoutDictionary.Assign(source, layout);
        }

        public void Clear()
        {
            source.Clear();
        }

        public IInstrumentRibbonEditor EditInstrumentRibbon(int indexInScore)
        {
            return source.EditInstrumentRibbon(indexInScore).UseLayout(layoutDictionary);
        }

        public IEnumerable<IInstrumentRibbonEditor> EditInstrumentRibbons()
        {
            return source.EditInstrumentRibbons().Select(e => e.UseLayout(layoutDictionary));
        }

        public IScoreMeasureEditor EditScoreMeasure(int indexInScore)
        {
            return source.EditScoreMeasure(indexInScore).UseLayout(layoutDictionary);
        }

        public IEnumerable<IScoreMeasureEditor> EditScoreMeasures()
        {
            return source.EditScoreMeasures().Select(e => e.UseLayout(layoutDictionary));
        }

        public IEnumerable<IStaffSystemEditor> EditStaffSystems()
        {
            if (!EditScoreMeasures().Any())
            {
                yield break;
            }

            var firstMeasure = EditScoreMeasures().First();
            if (!firstMeasure.ReadLayout().IsNewSystem)
            {
                throw new Exception("The first measure of the score must have a new system in it's layout supplied before the score can enumerate the staff systems.");
            }

            foreach (var measure in EditScoreMeasures())
            {
                if (measure.ReadLayout().IsNewSystem)
                {
                    yield return measure.EditStaffSystemOrigin();
                }
            }
        }

        public IEnumerable<IInstrumentRibbon> EnumerateInstrumentRibbons()
        {
            return source.EnumerateInstrumentRibbons();
        }

        public IEnumerable<IScoreMeasure> EnumerateScoreMeasures()
        {
            return source.EnumerateScoreMeasures();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public void InsertScoreMeasure(int index, TimeSignature? timeSignature = null)
        {
            source.InsertScoreMeasure(index, timeSignature);
        }

        public IScoreDocumentLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(source);
        }

        public void RemoveInstrumentRibbon(int elementId)
        {
            source.RemoveInstrumentRibbon(elementId);
        }

        public void RemoveScoreMeasure(int elementId)
        {
            source.RemoveScoreMeasure(elementId);
        }
    }
}
