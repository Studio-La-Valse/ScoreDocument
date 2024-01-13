namespace StudioLaValse.ScoreDocument.Core
{
    public class Pitch
    {
        private static readonly Dictionary<string, int> midiIndexForOctave0 = new Dictionary<string, int>
        {
            {"C", 12}, {"D", 14}, {"E", 16}, {"F", 17}, {"G", 19}, {"A", 21}, {"B", 23}
        };
        private static readonly Dictionary<int, decimal> frequenciesOfBottomOctavePiano = new Dictionary<int, decimal>
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



        public Step Step { get; }
        public int Octave { get; }

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
        public int StepValue =>
            Step.StepsFromC;
        public int Shift =>
            Step.Shifts;
        public int IntValueAsMidiNote
        {
            get
            {
                var indexOfPitchAtOctave0 = midiIndexForOctave0
                    .ElementAt(Step.StepsFromC).Value;

                var pitchInCorrectOctave = indexOfPitchAtOctave0 + Octave * 12;

                var pitchAfterShiftCorrection = pitchInCorrectOctave + Shift;

                if (pitchAfterShiftCorrection < 21)
                    throw new ArgumentOutOfRangeException(nameof(pitchAfterShiftCorrection));

                return pitchAfterShiftCorrection;
            }
        }
        public int IndexOnKlavier
        {
            get
            {
                var a0 = midiIndexForOctave0["A"];
                return IntValueAsMidiNote - a0;
            }
        }
        public decimal Frequency
        {
            get
            {
                var frequency = frequenciesOfBottomOctavePiano[IndexOnKlavier % 12];

                for (int i = 0; i < OctaveOnClavier; i++)
                {
                    frequency *= 2;
                }

                return frequency;
            }
        }



        public Pitch(Step step, int octave)
        {
            if (octave < 0 || octave > 8)
                throw new ArgumentOutOfRangeException(nameof(octave));

            Octave = octave;

            Step = step;
        }




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

        public static bool operator ==(Pitch pitch, Pitch other)
        {
            return pitch.Step == other.Step && pitch.Octave == other.Octave;
        }

        public static bool operator !=(Pitch pitch, Pitch other)
        {
            return pitch.Step != other.Step || pitch.Octave != other.Octave;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (obj is Pitch pitch)
            {
                return Equals(pitch);
            }

            return false;
        }

        public bool Equals(Pitch other)
        {
            if (other is null)
            {
                return false;
            }

            return other == this;
        }

        public override int GetHashCode()
        {
            return new Tuple<Step, int>(Step, Octave).GetHashCode();
        }
    }
}
