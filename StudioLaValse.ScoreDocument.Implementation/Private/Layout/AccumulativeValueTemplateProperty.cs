namespace StudioLaValse.ScoreDocument.Implementation.Private.Layout
{
    internal class AccumulativeValueTemplateProperty<T> : TemplateProperty<T> where T: struct
    {
        private readonly Func<T> defaultGetter;
        private readonly Func<T> parentValue;
        private readonly Func<T, T, T> accumulator;

        public override bool FieldIsSet => Field is not null;

        public T? Field { get; set; }

        public override T Value
        {
            get
            {
                var _thisValue = Field ?? defaultGetter();
                var _parentValue = parentValue();
                var accumulative = accumulator(_thisValue, _parentValue);
                return accumulative;
            }
            set
            {
                Field = value;
            }
        }

        public AccumulativeValueTemplateProperty(Func<T> defaultGetter, Func<T> parentValue, Func<T, T, T> accumulator)
        {
            this.defaultGetter = defaultGetter;
            this.parentValue = parentValue;
            this.accumulator = accumulator;
        }

        public override void Reset()
        {
            Field = null;
        }
    }
}
