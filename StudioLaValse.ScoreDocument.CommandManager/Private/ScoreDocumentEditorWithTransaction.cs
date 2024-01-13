using System.Diagnostics;

namespace StudioLaValse.ScoreDocument.CommandManager.Private
{
    internal class ScoreDocumentEditorWithTransaction : IScoreDocumentEditor
    {
        private readonly IScoreDocumentEditor score;
        private readonly ICommandManager commandManager;


        public int Id => score.Id;
        public int NumberOfMeasures => score.NumberOfMeasures;
        public int NumberOfInstruments => score.NumberOfInstruments;


        public ScoreDocumentEditorWithTransaction(IScoreDocumentEditor score, ICommandManager commandManager)
        {
            this.score = score;
            this.commandManager = commandManager;
        }



        public void AddInstrumentRibbon(Instrument instrument)
        {
            if (!commandManager.TryGetOpenTransaction(out var transaction))
            {
                throw new InvalidOperationException("No transaction open.");
            }

            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    score.AddInstrumentRibbon(instrument);
                },
                () =>
                {
                    var instrumentToRemove = EditInstrumentRibbons().Last().IndexInScore;
                    score.RemoveInstrumentRibbon(instrumentToRemove);
                }));
        }
        public void RemoveInstrumentRibbon(int indexInScore)
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            ScoreDocumentMemento? memento = null;

            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = score.GetMemento();
                    score.RemoveInstrumentRibbon(indexInScore);
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new UnreachableException("No memento set for this element, cannot undo operation.");
                    }

                    score.ApplyMemento(memento);
                }));
        }


        public void AppendScoreMeasure(TimeSignature? timeSignature = null)
        {
            if (!commandManager.TryGetOpenTransaction(out var transaction))
            {
                throw new InvalidOperationException("No transaction open.");
            }

            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    score.AppendScoreMeasure(timeSignature);
                },
                () =>
                {
                    var measureToRemove = score.NumberOfMeasures - 1;
                    score.RemoveScoreMeasure(measureToRemove);
                }));
        }
        public void InsertScoreMeasure(int index, TimeSignature? timeSignature = null)
        {
            if (!commandManager.TryGetOpenTransaction(out var transaction))
            {
                throw new InvalidOperationException("No transaction open.");
            }

            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    score.InsertScoreMeasure(index, timeSignature);
                },
                () =>
                {
                    score.RemoveScoreMeasure(index);
                }));
        }
        public void RemoveScoreMeasure(int index)
        {
            if (!commandManager.TryGetOpenTransaction(out var transaction))
            {
                throw new InvalidOperationException("No transaction open.");
            }

            ScoreMeasureMemento? memento = null;

            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    var scoreMeasure = EditScoreMeasure(index);
                    if (scoreMeasure is null)
                    {
                        throw new InvalidOperationException("Score measure at index not found!");
                    }

                    memento = scoreMeasure.GetMemento();
                    score.RemoveScoreMeasure(index);
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new UnreachableException("No memento set for this element, cannot undo operation.");
                    }

                    score.InsertScoreMeasure(index);

                    var scoreMeasure = score.EditScoreMeasure(index);
                    scoreMeasure.ApplyMemento(memento);
                }));
        }



        public IEnumerable<IInstrumentRibbonEditor> EditInstrumentRibbons()
        {
            return score.EditInstrumentRibbons().Select(e => e.UseTransaction(commandManager));
        }
        public IEnumerable<IScoreMeasureEditor> EditScoreMeasures()
        {
            return score.EditScoreMeasures().Select(e => e.UseTransaction(commandManager));
        }
        public IEnumerable<IStaffSystemEditor> EditStaffSystems()
        {
            return score.EditStaffSystems().Select(e => e.UseTransaction(commandManager));
        }



        public IEnumerable<IInstrumentRibbon> EnumerateInstrumentRibbons()
        {
            return score.EnumerateInstrumentRibbons();
        }
        public IEnumerable<IScoreMeasure> EnumerateScoreMeasures()
        {
            return score.EnumerateScoreMeasures();
        }


        public IScoreMeasureEditor EditScoreMeasure(int indexInScore)
        {
            return score.EditScoreMeasure(indexInScore).UseTransaction(commandManager);
        }
        public IInstrumentRibbonEditor EditInstrumentRibbon(int indexInScore)
        {
            return score.EditInstrumentRibbon(indexInScore).UseTransaction(commandManager);
        }



        public void ApplyLayout(IScoreDocumentLayout layout)
        {
            if (!commandManager.TryGetOpenTransaction(out var transaction))
            {
                throw new InvalidOperationException("No transaction open.");
            }

            IScoreDocumentLayout? oldLayout = null;

            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    oldLayout = score.ReadLayout().Copy();
                    score.ApplyLayout(layout);
                },
                () =>
                {
                    if (oldLayout is null)
                    {
                        throw new Exception("Memento not recorded!");
                    }

                    score.ApplyLayout(oldLayout);
                }));
        }
        public IScoreDocumentLayout ReadLayout()
        {
            return score.ReadLayout();
        }



        public bool Equals(IUniqueScoreElement? other)
        {
            return score.Equals(other);
        }

        public void Clear()
        {
            var transaction = commandManager.ThrowIfNoTransactionOpen();

            ScoreDocumentMemento? memento = null;

            transaction.Enqueue(new SimpleCommand(
                () =>
                {
                    memento = score.GetMemento();
                    score.Clear();
                },
                () =>
                {
                    if (memento is null)
                    {
                        throw new UnreachableException("No memento set for this element, cannot undo operation.");
                    }

                    score.ApplyMemento(memento);
                }));
        }
    }
}
