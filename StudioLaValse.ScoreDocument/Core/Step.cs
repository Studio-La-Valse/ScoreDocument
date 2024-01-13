namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// C = 0
    /// </summary>
    public class Step
    {
        private static readonly string[] stepNames = new string[]
        {
            "C", "D", "E",  "F", "G", "A", "B",
        };

        public static Step ADoubleFlat =>
            new Step(5, -2);
        public static Step AFlat =>
            new Step(5, -1);
        public static Step A =>
            new Step(5, 0);
        public static Step ASharp =>
            new Step(5, 1);
        public static Step ADoubleSharp =>
            new Step(5, 2);

        public static Step BDoubleFlat =>
            new Step(6, -2);
        public static Step BFlat =>
            new Step(6, -1);
        public static Step B =>
            new Step(6, 0);
        public static Step BSharp =>
            new Step(6, 1);
        public static Step BDoubleSharp =>
            new Step(6, 2);


        public static Step CDoubleFlat =>
            new Step(0, -2);
        public static Step CFlat =>
            new Step(0, -1);
        public static Step C =>
            new Step(0, 0);
        public static Step CSharp =>
            new Step(0, 1);
        public static Step CDoubleSharp =>
            new Step(0, 2);


        public static Step DDoubleFlat =>
            new Step(1, -2);
        public static Step DFlat =>
            new Step(1, -1);
        public static Step D =>
            new Step(1, 0);
        public static Step DSharp =>
            new Step(1, 1);
        public static Step DDoubleSharp =>
            new Step(1, 2);


        public static Step EDoubleFlat =>
            new Step(2, -2);
        public static Step EFlat =>
            new Step(2, -1);
        public static Step E =>
            new Step(2, 0);
        public static Step ESharp =>
            new Step(2, 1);
        public static Step EDoubleSharp =>
            new Step(2, 2);


        public static Step FDoubleFlat =>
            new Step(3, -2);
        public static Step FFlat =>
            new Step(3, -1);
        public static Step F =>
            new Step(3, 0);
        public static Step FSharp =>
            new Step(3, 1);
        public static Step FDoubleSharp =>
            new Step(3, 2);


        public static Step GDoubleFlat =>
            new Step(4, -2);
        public static Step GFlat =>
            new Step(4, -1);
        public static Step G =>
            new Step(4, 0);
        public static Step GSharp =>
            new Step(4, 1);
        public static Step GDoubleSharp =>
            new Step(4, 2);


        public int StepsFromC { get; }
        public int Shifts { get; }


        public int SemiTones
        {
            get
            {
                var value = 0;

                for (int i = 0; i < StepsFromC; i++)
                {
                    var indexInOctave = i % 12;

                    value += indexInOctave == 2 || indexInOctave == 6 ? 1 : 2;
                }

                return (int)MathUtils.UnsignedModulo(value + Shifts, 12);
            }
        }
        public int PositionCircleOfFifths =>
            SemiTones * 7 % 12;
        public Step RelativeMajor =>
            new Scale(this, ScaleStructure.Minor).EnumerateSteps().ElementAt(2);
        public Step RelativeMinor =>
            new Scale(this, ScaleStructure.Major).EnumerateSteps().ElementAt(5);
        public Step Dominant =>
            new Scale(this, ScaleStructure.Major).EnumerateSteps().ElementAt(4);


        public Step(int step, int shifts)
        {
            if (shifts < -3 || shifts > 3)
            {
                throw new ArgumentOutOfRangeException(nameof(shifts));
            }

            StepsFromC = (int)MathUtils.UnsignedModulo(step, 7);
            Shifts = shifts;
        }
        public static Step FromPositionInCircleOfFifths(int position)
        {
            return position switch
            {
                -6 => GFlat,
                -5 => DFlat,
                -4 => AFlat,
                -3 => EFlat,
                -2 => BFlat,
                -1 => F,
                0 => C,
                1 => G,
                2 => D,
                3 => A,
                4 => E,
                5 => B,
                6 => FSharp,
                7 => DFlat,
                8 => AFlat,
                9 => EFlat,
                10 => BFlat,
                11 => F,
                _ => throw new NotImplementedException()
            };
        }



        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj is not Step other)
            {
                return false;
            }

            return Equals(other);
        }
        public override int GetHashCode()
        {
            return new Tuple<int, int>(StepsFromC, Shifts).GetHashCode();
        }

        public bool Equals(Step other)
        {
            if (other is null)
            {
                return false;
            }

            return other.StepsFromC == StepsFromC && other.Shifts == Shifts;
        }
        public bool EqualsOrResembles(Step other)
        {
            if (other is null)
            {
                return false;
            }

            return other.SemiTones == SemiTones;
        }



        public static Step operator +(Step step, Interval interval)
        {
            if (interval.Steps == 0)
            {
                return new Step(step.StepsFromC, step.Shifts + interval.Shifts);
            }

            var scale = new Scale(step, ScaleStructure.Major);
            var element = scale.EnumerateSteps(interval.Steps + 1).ElementAt(interval.Steps);
            return new Step(element.StepsFromC, element.Shifts + interval.Shifts);
        }
        public static bool operator ==(Step step, Step second)
        {
            return step.Equals(second);
        }
        public static bool operator !=(Step step, Step second)
        {
            return !step.Equals(second);
        }

        public override string ToString()
        {
            var shift = Shifts switch
            {
                -3 => "bbb",
                -2 => "bb",
                -1 => "b",
                0 => "",
                1 => "#",
                2 => "##",
                3 => "###",
                _ => throw new NotSupportedException()
            };

            var step = stepNames.ElementAt(StepsFromC);

            return $"{step}{shift}";
        }
    }
}
