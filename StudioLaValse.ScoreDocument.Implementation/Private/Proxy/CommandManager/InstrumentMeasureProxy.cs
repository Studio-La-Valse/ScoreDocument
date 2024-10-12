using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Models;
using StudioLaValse.ScoreDocument.Models.Base;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager
{
    internal class InstrumentMeasureProxy : IInstrumentMeasure
    {
        private readonly InstrumentMeasure instrumentMeasure;
        private readonly ICommandManager commandManager;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;
        private readonly ILayoutSelector layoutSelector;

        public IInstrumentMeasureLayout Layout => layoutSelector.InstrumentMeasureLayout(instrumentMeasure);

        public int MeasureIndex => instrumentMeasure.MeasureIndex;

        public int RibbonIndex => instrumentMeasure.RibbonIndex;

        public TimeSignature TimeSignature => instrumentMeasure.TimeSignature;

        public Instrument Instrument => instrumentMeasure.Instrument;

        public int Id => instrumentMeasure.Id;

        public ReadonlyTemplateProperty<double> PaddingLeft => Layout.PaddingLeft;

        public ReadonlyTemplateProperty<double> PaddingRight => Layout.PaddingRight;

        public ReadonlyTemplateProperty<KeySignature> KeySignature => Layout.KeySignature;

        public TemplateProperty<double?> PaddingBottom => Layout.PaddingBottom.WithRerender(notifyEntityChanged, instrumentMeasure.HostMeasure.HostDocument, commandManager);

        public TemplateProperty<bool?> Collapsed => Layout.Collapsed.WithRerender(notifyEntityChanged, instrumentMeasure.HostMeasure.HostDocument, commandManager);

        public TemplateProperty<int?> NumberOfStaves => Layout.NumberOfStaves.WithRerender(notifyEntityChanged, instrumentMeasure.HostMeasure.HostDocument, commandManager);

        public InstrumentMeasureProxy(InstrumentMeasure source, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            instrumentMeasure = source;
            this.commandManager = commandManager;
            this.notifyEntityChanged = notifyEntityChanged;
            this.layoutSelector = layoutSelector;
        }



        public IEnumerable<ClefChange> EnumerateClefChanges() => Layout.EnumerateClefChanges();

        public void AddVoice(int voice)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<InstrumentMeasure, InstrumentMeasureMemento>(instrumentMeasure, s => s.AddVoice(voice)).ThenInvalidate(notifyEntityChanged, instrumentMeasure);
            transaction.Enqueue(command);
        }

        public void RemoveVoice(int voice)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<InstrumentMeasure, InstrumentMeasureMemento>(instrumentMeasure, s => s.RemoveVoice(voice)).ThenInvalidate(notifyEntityChanged, instrumentMeasure);
            transaction.Enqueue(command);
        }

        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<InstrumentMeasure, InstrumentMeasureMemento>(instrumentMeasure, s => s.Clear()).ThenInvalidate(notifyEntityChanged, instrumentMeasure);
            transaction.Enqueue(command);
        }

        public void AddClefChange(ClefChange clefChange)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            var originalClefChanges = Layout.EnumerateClefChanges().Select(c => new ClefChange(c.Clef, c.StaffIndex, c.Position)).ToArray();

            void doCommand() => Layout.AddClefChange(clefChange);
            void undoCommand()
            {
                Layout.ClearClefChanges();
                foreach (var clefChange in originalClefChanges)
                {
                    Layout.AddClefChange(clefChange);
                }
            }

            var command = new SimpleCommand(doCommand, undoCommand).ThenInvalidate(notifyEntityChanged, instrumentMeasure);
            transaction.Enqueue(command);
        }

        public void RemoveClefChange(ClefChange clefChange)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            var originalClefChanges = Layout.EnumerateClefChanges().Select(c => new ClefChange(c.Clef, c.StaffIndex, c.Position)).ToArray();

            void doCommand() => Layout.RemoveClefChange(clefChange);
            void undoCommand()
            {
                Layout.ClearClefChanges();
                foreach (var clefChange in originalClefChanges)
                {
                    Layout.AddClefChange(clefChange);
                }
            }

            var command = new SimpleCommand(doCommand, undoCommand).ThenInvalidate(notifyEntityChanged, instrumentMeasure);
            transaction.Enqueue(command);
        }

        public void ClearClefChanges()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            var originalClefChanges = Layout.EnumerateClefChanges().Select(c => new ClefChange(c.Clef, c.StaffIndex, c.Position)).ToArray();

            void doCommand() => Layout.ClearClefChanges();
            void undoCommand()
            {
                Layout.ClearClefChanges();
                foreach (var clefChange in originalClefChanges)
                {
                    Layout.AddClefChange(clefChange);
                }
            }

            var command = new SimpleCommand(doCommand, undoCommand).ThenInvalidate(notifyEntityChanged, instrumentMeasure);
            transaction.Enqueue(command);
        }


        public void RequestPaddingBottom(int staffIndex, double? paddingBottom = null)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            var originalValue = GetPaddingBottom(staffIndex);
            void doCommand() => Layout.RequestPaddingBottom(staffIndex, paddingBottom);
            void undoCommand() => Layout.RequestPaddingBottom(staffIndex, originalValue);
            var command = new SimpleCommand(doCommand, undoCommand).ThenInvalidate(notifyEntityChanged, instrumentMeasure);

            transaction.Enqueue(command);
        }

        public double? GetPaddingBottom(int staffIndex)
        {
            return Layout.GetPaddingBottom(staffIndex);
        }

        public IMeasureBlockChain ReadBlockChainAt(int voice)
        {
            return instrumentMeasure.GetBlockChainOrThrowCore(voice).Proxy(commandManager, notifyEntityChanged, layoutSelector);
        }

        public bool TryReadPrevious([NotNullWhen(true)] out IInstrumentMeasure? previous)
        {
            _ = instrumentMeasure.TryReadPrevious(out var _previous);
            previous = _previous?.Proxy(commandManager, notifyEntityChanged, layoutSelector);
            return previous != null;
        }

        public bool TryReadNext([NotNullWhen(true)] out IInstrumentMeasure? next)
        {
            _ = instrumentMeasure.TryReadNext(out var _next);
            next = _next?.Proxy(commandManager, notifyEntityChanged, layoutSelector);
            return next != null;
        }

        public IEnumerable<int> ReadVoices()
        {
            return instrumentMeasure.EnumerateVoices();
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadVoices().Select(ReadBlockChainAt).SelectMany(e => e.ReadBlocks());
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return other is not null && other.Id == Id;
        }
    }
}
