namespace StudioLaValse.ScoreDocument.Layout
{
    public class InstrumentMeasureLayout : IInstrumentMeasureLayout
    {
        private readonly HashSet<ClefChange> _changeList = [];
        public IEnumerable<ClefChange> ClefChanges => _changeList;

        public InstrumentMeasureLayout()
        {

        }

        public void AddClefChange(ClefChange clefChange)
        {
            _changeList.Add(clefChange);
        }


        public void RemoveClefChange(ClefChange clefChange)
        {
            _changeList.Remove(clefChange);
        }

        public IInstrumentMeasureLayout Copy()
        {
            var layout = new InstrumentMeasureLayout();
            foreach (var change in _changeList)
            {
                layout.AddClefChange(change);
            }
            return layout;
        }
    }
}