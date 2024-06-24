using System.Diagnostics;

namespace StudioLaValse.ScoreDocument.Implementation
{
    /// <summary>
    /// The default rebeam strategy.
    /// </summary>
    public class RebeamStrategy
    {
        /// <summary>
        /// Rebeam the chords.
        /// </summary>
        /// <param name="beamEditors"></param>
        /// <exception cref="UnreachableException"></exception>
        public void Rebeam(IEnumerable<IBeamEditor> beamEditors)
        {
            var chords = beamEditors.ToList();
            if (chords.Count == 0)
            {
                return;
            }

            foreach (var chord in chords)
            {
                chord.BeamTypes.Clear();
            }

            var shortestValue = chords.Select(e => e.RythmicDuration.PowerOfTwo.Value).OrderByDescending(e => e).First();

            for (var i = 8; i <= shortestValue; i *= 2)
            {
                for (var j = 0; j < chords.Count; j++)
                {
                    var leftChord = j >= 1 ? chords[j - 1] : null;
                    var middleChord = chords[j];
                    var rightChord = j < chords.Count - 1 ? chords[j + 1] : null;

                    if (middleChord.RythmicDuration.PowerOfTwo < i)
                    {
                        continue;
                    }

                    var receives = leftChord is not null && leftChord.RythmicDuration.PowerOfTwo >= i;
                    var sends = rightChord is not null && rightChord.RythmicDuration.PowerOfTwo >= i;

                    if (leftChord is null && rightChord is null)
                    {
                        middleChord.BeamTypes[i] = BeamType.Flag;
                        continue;
                    }

                    if (receives && sends)
                    {
                        middleChord.BeamTypes[i] = BeamType.Continue;
                        continue;
                    }
                    else if (sends)
                    {
                        middleChord.BeamTypes[i] = BeamType.Start;
                        continue;
                    }
                    else if (receives)
                    {
                        middleChord.BeamTypes[i] = BeamType.End;
                        continue;
                    }
                    else if (leftChord is null)
                    {
                        middleChord.BeamTypes[i] = BeamType.HookStart;
                        continue;
                    }
                    else if (rightChord is null)
                    {
                        middleChord.BeamTypes[i] = BeamType.HookEnd;
                        continue;
                    }
                    else
                    {
                        if (!middleChord.BeamTypes.TryGetValue(i / 2, out var beamUp))
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

                        middleChord.BeamTypes[i] = toAdd;
                        continue;
                    }

                    throw new UnreachableException("Incoherent beaming strategy");
                }
            }
        }
    }
}
