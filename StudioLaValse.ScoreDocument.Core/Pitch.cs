namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical pitch.
    /// </summary>
    public readonly struct Pitch : IEquatable<Pitch>
    {
        private static readonly Dictionary<string, int> midiIndexForOctave0 = new()
        {
            {"C", 12}, {"D", 14}, {"E", 16}, {"F", 17}, {"G", 19}, {"A", 21}, {"B", 23}
        };
        private static readonly Dictionary<int, decimal> frequenciesOfBottomOctavePiano = new()
        {
            {0, 27.5M },
            {1, 29.135M },
            {2, 30.868M },
            {3, 32.703M },
            {4, 34.648M },
            {5, 36.708M },
            {6, 38.891M },
            {7, 41.204M },
            {8, 43.654M },
            {9, 46.249M },
            {10, 48.999M },
            {11, 51.913M }
        };


        /// <summary>
        /// The step of the pitch.
        /// </summary>
        public Step Step { get; }
        /// <summary>
        /// The octave of the pitch.
        /// </summary>
        public int Octave { get; }

        /// <summary>
        /// Calculates the index of the octave on a default piano keyboard.
        /// </summary>
        public int OctaveOnClavier
        {
            get
            {
                var octaveOnClavier = 0;

                var mod = IndexOnKlavier;

                while (mod > 11)
                {
                    octaveOnClavier++;

                    mod -= 12;
                }

                return octaveOnClavier;
            }
        }
        /// <summary>
        /// The number of steps, relative from <see cref="Step.C"/>.
        /// </summary>
        public int StepValue =>
            Step.StepsFromC;
        /// <summary>
        /// The number of chromatic shifts after the steps.
        /// </summary>
        public int Shift =>
            Step.Shifts;
        /// <summary>
        /// Calculates the integer value as a midi note.
        /// </summary>
        public int IntValueAsMidiNote
        {
            get
            {
                var indexOfPitchAtOctave0 = midiIndexForOctave0
                    .ElementAt(Step.StepsFromC).Value;

                var pitchInCorrectOctave = indexOfPitchAtOctave0 + (Octave * 12);

                var pitchAfterShiftCorrection = pitchInCorrectOctave + Shift;

                return pitchAfterShiftCorrection < 21
                    ? throw new ArgumentOutOfRangeException(nameof(pitchAfterShiftCorrection))
                    : pitchAfterShiftCorrection;
            }
        }
        /// <summary>
        /// Calculates the index on a piano keyboard, where A0 has index 0.
        /// </summary>
        public int IndexOnKlavier
        {
            get
            {
                var a0 = midiIndexForOctave0["A"];
                return IntValueAsMidiNote - a0;
            }
        }
        /// <summary>
        /// Estimates the frequency of the pitch.
        /// </summary>
        public decimal Frequency
        {
            get
            {
                var frequency = frequenciesOfBottomOctavePiano[IndexOnKlavier % 12];

                for (var i = 0; i < OctaveOnClavier; i++)
                {
                    frequency *= 2;
                }

                return frequency;
            }
        }


        /// <summary>
        /// Construct a default pitch. 
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the specified octave is smaller than 0 or greater than 8.
        /// </summary>
        /// <param name="step"></param>
        /// <param name="octave"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Pitch(Step step, int octave)
        {
            if (octave is < 0 or > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(octave));
            }

            Octave = octave;

            Step = step;
        }



        /// <inheritdoc/>
        public override string ToString()
        {
            var shift = Step.Shifts switch
            {
                -2 => "bb",
                -1 => "b",
                0 => "",
                1 => "#",
                2 => "##",
                _ => throw new NotSupportedException()
            };

            return $"{midiIndexForOctave0.ElementAt(Step.StepsFromC).Key}{shift}{Octave}";
        }

        /// <inheritdoc/>
        public static Pitch operator +(Pitch pitch, Interval interval)
        {
            var step = pitch.Step;
            var newStep = step + interval;
            var octave = pitch.Octave;
            if (interval.SemiTones + step.SemiTones >= 12)
            {
                octave++;
            }
            else if (interval.SemiTones + step.SemiTones < 0)
            {
                octave--;
            }

            return new Pitch(newStep, octave);
        }

        /// <inheritdoc/>
        public static bool operator ==(Pitch pitch, Pitch other)
        {
            return pitch.Step == other.Step && pitch.Octave == other.Octave;
        }

        /// <inheritdoc/>
        public static bool operator !=(Pitch pitch, Pitch other)
        {
            return pitch.Step != other.Step || pitch.Octave != other.Octave;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is Pitch pitch && Equals(pitch);
        }

        /// <inheritdoc/>
        public bool Equals(Pitch other)
        {
            return other.Step == Step && other.Octave == Octave;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return new Tuple<Step, int>(Step, Octave).GetHashCode();
        }
    }
}
