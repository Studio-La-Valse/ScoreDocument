﻿using StudioLaValse.ScoreDocument.Models.Base;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Implementation.Layout
{
    public abstract class InstrumentMeasureLayout
    {
        public abstract NullableTemplateProperty<int> _NumberOfStaves { get; }
        public abstract NullableTemplateProperty<double> _PaddingBottom { get; }
        public abstract ValueTemplateProperty<bool> _Collapsed { get; }
        public abstract HashSet<ClefChange> _ClefChanges { get; }
        public abstract HashSet<ClefChange> _IgnoredClefChanges { get; }
        public abstract Dictionary<int, double> _PaddingBottomForStaves { get; }


        public double? PaddingBottom
        {
            get => _PaddingBottom.Value;
            set => _PaddingBottom.Value = value;
        }
        public bool Collapsed
        {
            get => _Collapsed.Value;
            set => _Collapsed.Value = value;
        }
        public int? NumberOfStaves
        {
            get => _NumberOfStaves.Value;
            set => _NumberOfStaves.Value = value;
        }

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
    }

    public class AuthorInstrumentMeasureLayout : InstrumentMeasureLayout, IInstrumentMeasureLayout, ILayout<InstrumentMeasureLayoutMembers>
    {
        private readonly ScoreMeasure scoreMeasure;


        public override Dictionary<int, double> _PaddingBottomForStaves { get; } = [];
        public override HashSet<ClefChange> _ClefChanges { get; } = [];
        public override HashSet<ClefChange> _IgnoredClefChanges { get; } = [];
        public override NullableTemplateProperty<int> _NumberOfStaves { get; }
        public override NullableTemplateProperty<double> _PaddingBottom { get; }
        public override ValueTemplateProperty<bool> _Collapsed { get; }


        public KeySignature KeySignature => scoreMeasure.AuthorLayout.KeySignature;
        public IEnumerable<ClefChange> ClefChanges => _ClefChanges;


        public AuthorInstrumentMeasureLayout(Instrument instrument, ScoreMeasure scoreMeasure)
        {
            this.scoreMeasure = scoreMeasure;

            _NumberOfStaves = new NullableTemplateProperty<int>(() => null);
            _Collapsed = new ValueTemplateProperty<bool>(() => false);
            _PaddingBottom = new NullableTemplateProperty<double>(() => null);
        }

        public void AddClefChange(ClefChange clefChange)
        {
            _ = _ClefChanges.Add(clefChange);
        }

        public void RemoveClefChange(ClefChange clefChange)
        {
            _ = _ClefChanges.Remove(clefChange);
        }

        public void RequestPaddingBottom(int staffIndex, double paddingBottom)
        {
            _PaddingBottomForStaves[staffIndex] = paddingBottom;
        }

        public double? GetPaddingBottom(int staffIndex)
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
        public override ValueTemplateProperty<bool> _Collapsed { get; }


        public Guid Id { get; }



        public IEnumerable<ClefChange> ClefChanges => _ClefChanges.Concat(layout.ClefChanges.Where(c => !_IgnoredClefChanges.Contains(c)));
        public KeySignature KeySignature => layout.KeySignature;


        public UserInstrumentMeasureLayout(AuthorInstrumentMeasureLayout layout, Guid id)
        {
            this.layout = layout;
            Id = id;

            _NumberOfStaves = new NullableTemplateProperty<int>(() => layout.NumberOfStaves);
            _PaddingBottom = new NullableTemplateProperty<double>(() => layout.PaddingBottom);
            _Collapsed = new ValueTemplateProperty<bool>(() => layout.Collapsed);
            paddingBottomForStavesSource = layout._PaddingBottomForStaves;
        }

        public void AddClefChange(ClefChange clefChange)
        {
            _ = _ClefChanges.Add(clefChange);
        }

        public void RemoveClefChange(ClefChange clefChange)
        {
            _ = _ClefChanges.Remove(clefChange);
            _IgnoredClefChanges.Add(clefChange);
        }

        public void RequestPaddingBottom(int staffIndex, double paddingBottom)
        {
            _PaddingBottomForStaves[staffIndex] = paddingBottom;
        }

        public double? GetPaddingBottom(int staffIndex)
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