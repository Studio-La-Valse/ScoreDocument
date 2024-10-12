using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager
{
    internal class MeasureBlockProxy(MeasureBlock source, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector) : IMeasureBlock
    {
        private readonly MeasureBlock source = source;
        private readonly ICommandManager commandManager = commandManager;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged = notifyEntityChanged;
        private readonly ILayoutSelector layoutSelector = layoutSelector;


        public InstrumentMeasure HostMeasure => source.RibbonMeasure;

        public IMeasureBlockLayout Layout => layoutSelector.MeasureBlockLayout(source);



        public RythmicDuration RythmicDuration => source.RythmicDuration;

        public Position Position => source.Position;

        public Tuplet Tuplet => source.Tuplet;

        public int Id => source.Id;



        public TemplateProperty<StemDirection> StemDirection => Layout.StemDirection.WithRerender(notifyEntityChanged, HostMeasure, commandManager);

        public TemplateProperty<double> StemLength => Layout.StemLength.WithRerender(notifyEntityChanged, HostMeasure, commandManager);

        public TemplateProperty<double> BeamAngle => Layout.BeamAngle.WithRerender(notifyEntityChanged, HostMeasure, commandManager);

        public ReadonlyTemplateProperty<double> BeamThickness => Layout.BeamThickness;

        public ReadonlyTemplateProperty<double> BeamSpacing => Layout.BeamSpacing;

        public TemplateProperty<double> Scale => Layout.Scale;


        public void AppendChord(RythmicDuration rythmicDuration, params Pitch[] pitches)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<MeasureBlock, MeasureBlockMemento>(source, (s) => s.AppendChord(rythmicDuration, rebeam: true, pitches: pitches)).ThenInvalidate(notifyEntityChanged, HostMeasure);
            transaction.Enqueue(command);
        }

        public void Splice(int index)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<MeasureBlock, MeasureBlockMemento>(source, (s) => s.Splice(index)).ThenInvalidate(notifyEntityChanged, HostMeasure);
            transaction.Enqueue(command);
        }

        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<MeasureBlock, MeasureBlockMemento>(source, (s) => s.Clear()).ThenInvalidate(notifyEntityChanged, HostMeasure);
            transaction.Enqueue(command);
        }

        public bool TryReadNext([NotNullWhen(true)] out IMeasureBlock? right)
        {
            right = null;
            if (source.TryReadNext(out var _right))
            {
                right = _right.Proxy(commandManager, notifyEntityChanged, layoutSelector);
            }
            return right is not null;
        }

        public bool TryReadPrevious([NotNullWhen(true)] out IMeasureBlock? previous)
        {
            previous = null;
            if (source.TryReadNext(out var _prev))
            {
                previous = _prev.Proxy(commandManager, notifyEntityChanged, layoutSelector);
            }
            return previous is not null;
        }

        public IEnumerable<IChord> ReadChords()
        {
            return source.GetChordsCore().Select(e => e.Proxy(commandManager, notifyEntityChanged, layoutSelector));
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadChords();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Id == Id;
        }
    }
}
