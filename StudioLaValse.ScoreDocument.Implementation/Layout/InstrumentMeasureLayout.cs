using StudioLaValse.ScoreDocument.Models.Base;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class InstrumentMeasureLayout : IInstrumentMeasureLayout
    {
        public abstract NullableTemplateProperty<int> _NumberOfStaves { get; }
        public abstract NullableTemplateProperty<double> _PaddingBottom { get; }
        public abstract NullableTemplateProperty<bool> _Collapsed { get; }
        public abstract HashSet<ClefChange> _ClefChanges { get; }
        public abstract HashSet<ClefChange> _IgnoredClefChanges { get; }
        public abstract Dictionary<int, double> _PaddingBottomForStaves { get; }



        public ReadonlyTemplateProperty<KeySignature> KeySignature { get; }
        public TemplateProperty<double?> PaddingBottom => _PaddingBottom;
        public TemplateProperty<bool?> Collapsed => _Collapsed;
        public TemplateProperty<int?> NumberOfStaves => _NumberOfStaves;




        protected InstrumentMeasureLayout(ScoreMeasure scoreMeasure)
        {
            KeySignature = new ReadonlyTemplatePropertyFromFunc<KeySignature>(() => scoreMeasure.UserLayout.KeySignature);
        }


        public abstract IEnumerable<ClefChange> EnumerateClefChanges();


        public void Restore()
        {
            _ClefChanges.Clear();
            _PaddingBottomForStaves.Clear();
            _IgnoredClefChanges.Clear();
            _NumberOfStaves.Reset();
            _PaddingBottom.Reset();
            _Collapsed.Reset();
        }

        public void ApplyMemento(InstrumentMeasureLayoutMembers? memento)
        {
            Restore();
            if (memento is null)
            {
                return;
            }

            foreach (var clefChange in memento.ClefChanges ?? [])
            {
                _ClefChanges.Add(clefChange.Convert());
            }

            foreach (var kv in memento.StaffPaddingBottom ?? [])
            {
                _PaddingBottomForStaves.Add(kv.Key, kv.Value);
            }

            _NumberOfStaves.Field = memento.NumberOfStaves;
            _PaddingBottom.Field = memento.PaddingBottom;
            _Collapsed.Field = memento.Collapsed;
        }
        public void ApplyMemento(InstrumentMeasureLayoutModel? memento)
        {
            ApplyMemento(memento as InstrumentMeasureLayoutMembers);

            if (memento is null)
            {
                return;
            }

            foreach (var clefChange in memento.IgnoredClefChanges ?? [])
            {
                _IgnoredClefChanges.Add(clefChange.Convert());
            }
        }

        public void ClearClefChanges()
        {
            _ClefChanges.Clear();
        }

        public abstract void AddClefChange(ClefChange clefChange);

        public abstract void RemoveClefChange(ClefChange clefChange);


        public abstract double? GetPaddingBottom(int staffIndex);

        public abstract void RequestPaddingBottom(int staffIndex, double? paddingBottom = null);
    }

    public class AuthorInstrumentMeasureLayout : InstrumentMeasureLayout, ILayout<InstrumentMeasureLayoutMembers>
    {
        public override Dictionary<int, double> _PaddingBottomForStaves { get; } = [];
        public override HashSet<ClefChange> _ClefChanges { get; } = [];
        public override HashSet<ClefChange> _IgnoredClefChanges { get; } = [];
        public override NullableTemplateProperty<int> _NumberOfStaves { get; }
        public override NullableTemplateProperty<double> _PaddingBottom { get; }
        public override NullableTemplateProperty<bool> _Collapsed { get; }


        public override IEnumerable<ClefChange> EnumerateClefChanges() => _ClefChanges;


        public AuthorInstrumentMeasureLayout(ScoreMeasure scoreMeasure) : base(scoreMeasure)
        {
            _NumberOfStaves = new NullableTemplateProperty<int>(() => null);
            _Collapsed = new NullableTemplateProperty<bool>(() => false);
            _PaddingBottom = new NullableTemplateProperty<double>(() => null);
        }

        public override void AddClefChange(ClefChange clefChange)
        {
            _ = _ClefChanges.Add(clefChange);
        }

        public override void RemoveClefChange(ClefChange clefChange)
        {
            _ = _ClefChanges.Remove(clefChange);
        }

        public override void RequestPaddingBottom(int staffIndex, double? paddingBottom)
        {
            if(paddingBottom is null)
            {
                _PaddingBottomForStaves.Remove(staffIndex);
                return;
            }
            _PaddingBottomForStaves[staffIndex] = paddingBottom.Value;
        }

        public override double? GetPaddingBottom(int staffIndex)
        {
            if (_PaddingBottomForStaves.TryGetValue(staffIndex, out var paddingBottom))
            {
                return paddingBottom;
            }

            return null;
        }

        public InstrumentMeasureLayoutMembers GetMemento()
        {
            var clefChangeDictionary = new HashSet<ClefChange>();
            foreach (var clefChange in _ClefChanges)
            {
                clefChangeDictionary.Add(clefChange);
            }

            var paddingBottomStavesDictionary = new Dictionary<int, double>();
            foreach (var kv in _PaddingBottomForStaves)
            {
                paddingBottomStavesDictionary.Add(kv.Key, kv.Value);
            }

            return new InstrumentMeasureLayoutMembers()
            {
                ClefChanges = _ClefChanges.Select(e => e.Convert()).ToList(),
                StaffPaddingBottom = paddingBottomStavesDictionary,
                NumberOfStaves = _NumberOfStaves.Field,
                PaddingBottom = PaddingBottom,
                Collapsed = Collapsed
            };
        }
    }

    public class UserInstrumentMeasureLayout : InstrumentMeasureLayout, IInstrumentMeasureLayout, ILayout<InstrumentMeasureLayoutModel>
    {
        private readonly AuthorInstrumentMeasureLayout layout;
        private readonly Dictionary<int, double> paddingBottomForStavesSource;


        public override HashSet<ClefChange> _IgnoredClefChanges { get; } = [];
        public override Dictionary<int, double> _PaddingBottomForStaves { get; } = [];
        public override HashSet<ClefChange> _ClefChanges { get; } = [];
        public override NullableTemplateProperty<int> _NumberOfStaves { get; }
        public override NullableTemplateProperty<double> _PaddingBottom { get; }
        public override NullableTemplateProperty<bool> _Collapsed { get; }


        public Guid Id { get; }



        public override IEnumerable<ClefChange> EnumerateClefChanges() => _ClefChanges.Concat(layout.EnumerateClefChanges().Where(c => !_IgnoredClefChanges.Contains(c)));



        public UserInstrumentMeasureLayout(AuthorInstrumentMeasureLayout layout, Guid id, ScoreMeasure scoreMeasure) : base(scoreMeasure)
        {
            this.layout = layout;

            Id = id;

            _NumberOfStaves = new NullableTemplateProperty<int>(() => layout.NumberOfStaves);
            _PaddingBottom = new NullableTemplateProperty<double>(() => layout.PaddingBottom);
            _Collapsed = new NullableTemplateProperty<bool>(() => layout.Collapsed);
            paddingBottomForStavesSource = layout._PaddingBottomForStaves;
        }

        public override void AddClefChange(ClefChange clefChange)
        {
            _ = _ClefChanges.Add(clefChange);
        }

        public override void RemoveClefChange(ClefChange clefChange)
        {
            _ = _ClefChanges.Remove(clefChange);
            _IgnoredClefChanges.Add(clefChange);
        }

        public override void RequestPaddingBottom(int staffIndex, double? paddingBottom)
        {
            if(paddingBottom is null)
            {
                _PaddingBottomForStaves.Remove(staffIndex);
                return;
            }
            _PaddingBottomForStaves[staffIndex] = paddingBottom.Value;
        }

        public override double? GetPaddingBottom(int staffIndex)
        {
            if (paddingBottomForStavesSource.TryGetValue(staffIndex, out var _paddingBottom))
            {
                return _paddingBottom;
            }

            if (_PaddingBottomForStaves.TryGetValue(staffIndex, out var __paddingBottom))
            {
                return __paddingBottom;
            }

            return null;
        }


        public InstrumentMeasureLayoutModel GetMemento()
        {
            var clefChangeDictionary = new HashSet<ClefChange>();
            foreach (var clefChange in _ClefChanges)
            {
                clefChangeDictionary.Add(clefChange);
            }

            var paddingBottomStavesDictionary = new Dictionary<int, double>();
            foreach (var kv in _PaddingBottomForStaves)
            {
                paddingBottomStavesDictionary.Add(kv.Key, kv.Value);
            }

            return new InstrumentMeasureLayoutModel()
            {
                Id = Id,
                ClefChanges = _ClefChanges.Select(e => e.Convert()).ToList(),
                IgnoredClefChanges = _IgnoredClefChanges.Select(e => e.Convert()).ToList(),
                StaffPaddingBottom = paddingBottomStavesDictionary,
                NumberOfStaves = _NumberOfStaves.Field,
                PaddingBottom = PaddingBottom,
                Collapsed = Collapsed
            };
        }
    }
}