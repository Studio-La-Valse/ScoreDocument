using System.Diagnostics;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager
{
    internal class MementoCommand<TEntity, TMemento> : BaseCommand where TEntity : IMementoElement<TMemento>
    {
        private readonly TEntity entity;
        private readonly Action<TEntity> action;
        private TMemento? memento = default;


        public MementoCommand(TEntity entity, Action<TEntity> action)
        {
            this.entity = entity;
            this.action = action;
        }

        public override void Do()
        {
            memento = entity.GetMemento();
            action(entity);
        }

        public override void Undo()
        {
            if (memento is null)
            {
                throw new UnreachableException("Memento not recorded; Undo() method called before do() method.");
            }

            entity.ApplyMemento(memento);
        }
    }
}
