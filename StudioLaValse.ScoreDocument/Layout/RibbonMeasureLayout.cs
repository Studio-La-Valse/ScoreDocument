namespace StudioLaValse.ScoreDocument.Layout
{
    public class RibbonMeasureLayout : IRibbonMeasureLayout
    {
        private readonly HashSet<ClefChange> _changeList = [];
        public IEnumerable<ClefChange> ClefChanges => _changeList;

        public RibbonMeasureLayout()
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

        public IRibbonMeasureLayout Copy()
        {
            var layout = new RibbonMeasureLayout(); 
            foreach(var change in _changeList)
            {
                layout.AddClefChange(change);
            }
            return layout;
        }
    }
}