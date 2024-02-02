using StudioLaValse.ScoreDocument.Core;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class RibbonMeasureEditorWithCommandManager : IInstrumentMeasureEditor
    {
        private readonly IInstrumentMeasureEditor source;
        private readonly ICommandManager commandManager;

        public RibbonMeasureEditorWithCommandManager(IInstrumentMeasureEditor source, ICommandManager commandManager)
        {
            this.source = source;
            this.commandManager = commandManager;
        }

        public int MeasureIndex => source.MeasureIndex;

        public int RibbonIndex => source.RibbonIndex;

        public TimeSignature TimeSignature => source.TimeSignature;

        public Instrument Instrument => source.Instrument;

        public int Id => source.Id;

        public void AddVoice(int voice)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            InstrumentMeasureMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento();
                    source.AddVoice(voice);
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new Exception("Memento not recorded.");
                    }

                    source.ApplyMemento(memento);
                }));
        }

        public void RemoveVoice(int voice)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            InstrumentMeasureMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento();
                    source.RemoveVoice(voice);
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new Exception("Memento not recorded.");
                    }

                    source.ApplyMemento(memento);
                }));
        }

        public IEnumerable<int> EnumerateVoices()
        {
            return source.EnumerateVoices();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public void ApplyLayout(IInstrumentMeasureLayout layout)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            IInstrumentMeasureLayout? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.ReadLayout();
                    source.ApplyLayout(layout);
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new Exception("Memento not recorded.");
                    }

                    source.ApplyLayout(memento);
                }));
        }

        public IInstrumentMeasureLayout ReadLayout()
        {
            return source.ReadLayout();
        }

        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            InstrumentMeasureMemento? memento = null;
            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = source.GetMemento();
                    source.Clear();
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new Exception("Memento not recorded.");
                    }

                    source.ApplyMemento(memento);
                }));
        }

        public IMeasureBlockChainEditor EditBlockChainAt(int voice)
        {
            return source.EditBlockChainAt(voice).UseTransaction(commandManager);
        }

        public IMeasureBlockChain BlockChainAt(int voice)
        {
            return source.BlockChainAt(voice);
        }
    }
}
