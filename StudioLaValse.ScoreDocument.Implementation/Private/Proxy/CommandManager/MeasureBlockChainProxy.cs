using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager
{
    internal class MeasureBlockChainProxy : IMeasureBlockChain
    {
        private readonly MeasureBlockChain source;
        private readonly ICommandManager commandManager;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;
        private readonly ILayoutSelector layoutSelector;

        public TimeSignature TimeSignature => source.TimeSignature;



        public MeasureBlockChainProxy(MeasureBlockChain source, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            this.source = source;
            this.commandManager = commandManager;
            this.notifyEntityChanged = notifyEntityChanged;
            this.layoutSelector = layoutSelector;
        }




        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new ParentMementoCommand<MeasureBlockChain, InstrumentMeasure, InstrumentMeasureMemento>(source.RibbonMeasure, source, s => s.Clear()).ThenInvalidate(notifyEntityChanged, source.RibbonMeasure);
            transaction.Enqueue(command);
        }

        public void Divide(params int[] steps)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new ParentMementoCommand<MeasureBlockChain, InstrumentMeasure, InstrumentMeasureMemento>(source.RibbonMeasure, source, s => s.Divide(steps)).ThenInvalidate(notifyEntityChanged, source.RibbonMeasure);
            transaction.Enqueue(command);
        }

        public void Divide(params RythmicDuration[] steps)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new ParentMementoCommand<MeasureBlockChain, InstrumentMeasure, InstrumentMeasureMemento>(source.RibbonMeasure, source, s => s.Divide(steps)).ThenInvalidate(notifyEntityChanged, source.RibbonMeasure);
            transaction.Enqueue(command);
        }

        public void DivideEqual(int number)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new ParentMementoCommand<MeasureBlockChain, InstrumentMeasure, InstrumentMeasureMemento>(source.RibbonMeasure, source, s => s.DivideEqual(number)).ThenInvalidate(notifyEntityChanged, source.RibbonMeasure);
            transaction.Enqueue(command);
        }

        public IEnumerable<IMeasureBlock> ReadBlocks()
        {
            return source.GetBlocksCore().Select(e => e.Proxy(commandManager, notifyEntityChanged, layoutSelector));
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadBlocks();
        }
    }
}
