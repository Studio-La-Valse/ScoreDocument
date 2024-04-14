using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Represents an instrument measure layout.
    /// </summary>
    public class InstrumentMeasureLayout : ILayoutElement<InstrumentMeasureLayout>
    {
        private readonly HashSet<ClefChange> _changeList = [];
        private readonly InstrumentMeasureStyleTemplate styleTemplate;

        /// <inheritdoc/>
        public IEnumerable<ClefChange> ClefChanges => _changeList;

        /// <summary>
        /// Create a new Instrument measure layout.
        /// </summary>
        public InstrumentMeasureLayout(InstrumentMeasureStyleTemplate styleTemplate)
        {
            this.styleTemplate = styleTemplate;
        }

        /// <inheritdoc/>
        public void AddClefChange(ClefChange clefChange)
        {
            _ = _changeList.Add(clefChange);
        }

        /// <inheritdoc/>
        public void RemoveClefChange(ClefChange clefChange)
        {
            _ = _changeList.Remove(clefChange);
        }

        /// <inheritdoc/>
        public InstrumentMeasureLayout Copy()
        {
            InstrumentMeasureLayout layout = new(styleTemplate);
            foreach (var change in _changeList)
            {
                layout.AddClefChange(change);
            }
            return layout;
        }
    }
}