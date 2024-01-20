using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class MeasureBlock : ScoreElement, IMeasureBlockReader, IMeasureBlockEditor
    {
        private readonly List<MeasureElementContainer> containers;
        private readonly MeasureBlockChain host;
        private readonly bool grace;
        private readonly IKeyGenerator<int> keyGenerator;
        private readonly Duration duration;
        private INoteGroupLayout layout;



        public int Voice =>
            host.Voice;
        public Position Position
        {
            get
            {
                var index = host.IndexOfOrThrow(this);

                var position = new Position(0, 4);

                foreach (var block in host.Blocks.Take(index))
                {
                    if (block.Grace)
                    {
                        continue;
                    }

                    position += block.Duration;
                }

                return position;
            }
        }
        public InstrumentMeasure RibbonMeasure =>
            host.RibbonMeasure;
        public IEnumerable<MeasureElementContainer> Containers =>
            containers;
        public bool Grace =>
            grace;
        public Duration Duration =>
            duration;
        public Tuplet Tuplet
        {
            get
            {
                var groupLength = Containers.Select(e => e.RythmicDuration).ToArray();
                return new Tuplet(Duration, groupLength);
            }
        }

        public MeasureBlock(Duration duration, MeasureBlockChain host, bool grace, IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {
            this.host = host;
            this.grace = grace;
            this.keyGenerator = keyGenerator;
            this.duration = duration;
            containers = new List<MeasureElementContainer>();
            layout = new NoteGroupLayout();
        }



        public MeasureElementContainer? ContainerRight(MeasureElementContainer elementContainer)
        {
            var index = IndexOfOrThrow(elementContainer);
            if (index - 1 < containers.Count)
            {
                return containers[index + 1];
            }

            return null;
        }
        public MeasureElementContainer? ContainerLeft(MeasureElementContainer elementContainer)
        {
            var index = IndexOfOrThrow(elementContainer);
            if (index > 0)
            {
                return containers[index - 1];
            }

            return null;
        }


        public int IndexOfOrThrow(MeasureElementContainer container)
        {
            var index = containers.IndexOf(container);

            if (index == -1)
            {
                throw new Exception("Measure element container does not exist in this measure block");
            }

            return index;
        }
        public bool ContainsPosition(Position position)
        {
            return (Position + duration).Decimal >= position.Decimal && Position.Decimal < position.Decimal;
        }


        public IEnumerable<IChord> EnumerateChords()
        {
            return containers;
        }
        public IEnumerable<IChordReader> ReadChords()
        {
            return containers;
        }
        public IEnumerable<IChordEditor> EditChords()
        {
            return containers;
        }
        public void AppendChord(RythmicDuration rythmicDuration)
        {
            containers.Add(new MeasureElementContainer(this, rythmicDuration, keyGenerator));
        }
        public void Splice(int index)
        {
            containers.RemoveAt(index);
        }


        public bool TryReadNext([NotNullWhen(true)] out IMeasureBlockReader? right)
        {
            right = null;

            try
            {
                right = host.BlockRight(this);
                return right is not null;
            }
            catch
            {

            }

            return false;
        }
        public bool TryReadPrevious([NotNullWhen(true)] out IMeasureBlockReader? previous)
        {
            previous = null;

            try
            {
                previous = host.BlockLeft(this);
                return previous is not null;
            }
            catch
            {

            }

            return false;
        }


        public void Rebeam()
        {
            for (int i = 8; i <= 64; i *= 2)
            {
                var duration = 1M / i;

                for (int j = 0; j < containers.Count; j++)
                {
                    var leftChord = j >= 1 ? containers[j - 1] : null;
                    var middleChord = containers[j];
                    var rightChord = j < containers.Count - 1 ? containers[j + 1] : null;
                    var middleChordBeams = middleChord.ReadLayout().Beams;
                    middleChordBeams.Clear();

                    if (middleChord.RythmicDuration.PowerOfTwo < 1 / duration)
                    {
                        continue;
                    }

                    var receives = leftChord is not null && leftChord.RythmicDuration.PowerOfTwo >= 1 / duration;
                    var sends = rightChord is not null && rightChord.RythmicDuration.PowerOfTwo >= 1 / duration;

                    if (leftChord is null && rightChord is null)
                    {
                        middleChordBeams.Add(i, BeamType.Flag);
                        continue;
                    }

                    if (receives && sends)
                    {
                        middleChordBeams.Add(i, BeamType.Continue);
                        continue;
                    }
                    else if (sends)
                    {
                        middleChordBeams.Add(i, BeamType.Start);
                        continue;
                    }
                    else if (receives)
                    {
                        middleChordBeams.Add(i, BeamType.End);
                        continue;
                    }
                    else if (leftChord is null)
                    {
                        middleChordBeams.Add(i, BeamType.HookStart);
                        continue;
                    }
                    else if (rightChord is null)
                    {
                        middleChordBeams.Add(i, BeamType.HookEnd);
                        continue;
                    }
                    else
                    {
                        if (!middleChordBeams.TryGetValue(i / 2, out var beamUp))
                        {
                            throw new UnreachableException("Incoherent beaming strategy");
                        }

                        var toAdd = beamUp switch
                        {
                            BeamType.Continue => BeamType.HookStart,
                            BeamType.End => BeamType.HookEnd,
                            BeamType.HookEnd => BeamType.HookEnd,
                            BeamType.HookStart => BeamType.HookStart,
                            BeamType.Start => BeamType.HookStart,
                            _ => throw new UnreachableException("Incoherent beaming strategy")
                        };

                        middleChordBeams.Add(i, toAdd);
                        continue;
                    }

                    throw new UnreachableException("Incoherent beaming strategy");
                }
            }
        }


        public INoteGroupLayout ReadLayout()
        {
            return layout;
        }
        public void ApplyLayout(INoteGroupLayout layout)
        {
            this.layout = layout;
        }

        public void Clear()
        {
            containers.Clear();
        }
    }
}
