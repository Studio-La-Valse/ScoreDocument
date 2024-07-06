using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private.Proxy.CommandManager
{
    internal class GraceGroupProxy : IGraceGroup
    {
        private readonly GraceGroup graceGroup;
        private readonly ICommandManager commandManager;
        private readonly INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged;
        private readonly ILayoutSelector layoutSelector;


        public IGraceGroupLayout Layout => layoutSelector.GraceGroupLayout(graceGroup);


        public int Id => graceGroup.Id;

        public int Length => graceGroup.Length;

        public Position Target => graceGroup.Target;



        public TemplateProperty<RythmicDuration> BlockDuration => Layout.ChordDuration.WithRerender(notifyEntityChanged, graceGroup.HostMeasure, commandManager);

        public TemplateProperty<bool> OccupySpace => Layout.OccupySpace.WithRerender(notifyEntityChanged, graceGroup.HostMeasure, commandManager);

        public TemplateProperty<double> ChordSpacing => Layout.ChordSpacing.WithRerender(notifyEntityChanged, graceGroup.HostMeasure, commandManager);

        public TemplateProperty<RythmicDuration> ChordDuration => Layout.ChordDuration.WithRerender(notifyEntityChanged, graceGroup.HostMeasure, commandManager);

        public TemplateProperty<double> Scale => Layout.Scale.WithRerender(notifyEntityChanged, graceGroup.HostMeasure, commandManager);

        public TemplateProperty<StemDirection> StemDirection => Layout.StemDirection.WithRerender(notifyEntityChanged, graceGroup.HostMeasure, commandManager);

        public TemplateProperty<double> StemLength => Layout.StemLength.WithRerender(notifyEntityChanged, graceGroup.HostMeasure, commandManager);

        public TemplateProperty<double> BeamAngle => Layout.BeamAngle.WithRerender(notifyEntityChanged, graceGroup.HostMeasure, commandManager);

        public ReadonlyTemplateProperty<double> BeamThickness => Layout.BeamThickness;

        public ReadonlyTemplateProperty<double> BeamSpacing => Layout.BeamSpacing;



        public GraceGroupProxy(GraceGroup graceGroup, ICommandManager commandManager, INotifyEntityChanged<IUniqueScoreElement> notifyEntityChanged, ILayoutSelector layoutSelector)
        {
            this.graceGroup = graceGroup;
            this.commandManager = commandManager;
            this.notifyEntityChanged = notifyEntityChanged;
            this.layoutSelector = layoutSelector;
        }



        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return ReadChords();
        }

        public IEnumerable<IGraceChord> ReadChords()
        {
            return graceGroup.Chords.Select(c => c.Proxy(commandManager, notifyEntityChanged, layoutSelector));
        }

        public void Splice(int index)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<GraceGroup, GraceGroupMemento>(graceGroup, (s) => s.Splice(index)).ThenInvalidate(notifyEntityChanged, graceGroup.HostMeasure);
            transaction.Enqueue(command);
        }

        public void AppendChord(params Pitch[] pitches)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<GraceGroup, GraceGroupMemento>(graceGroup, (s) => s.Append(pitches)).ThenInvalidate(notifyEntityChanged, graceGroup.HostMeasure);
            transaction.Enqueue(command);
        }

        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();
            var command = new MementoCommand<GraceGroup, GraceGroupMemento>(graceGroup, (s) => s.Clear()).ThenInvalidate(notifyEntityChanged, graceGroup.HostMeasure);
            transaction.Enqueue(command);
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return other is not null && other.Id == Id;
        }
    }
}
