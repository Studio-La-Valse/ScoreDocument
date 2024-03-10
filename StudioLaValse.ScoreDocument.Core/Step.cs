using StudioLaValse.ScoreDocument.Core.Private;

namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical step.
    /// </summary>
    public readonly struct Step : IEquatable<Step>
    {
        private static readonly string[] stepNames =
        [
            "C",
            "D",
            "E",
            "F",
            "G",
            "A",
            "B",
        ];

        /// <summary>
        /// 
        /// </summary>
        public static Step ADoubleFlat =>
            new(5, -2);
        /// <summary>
        /// 
        /// </summary>
        public static Step AFlat =>
            new(5, -1);
        /// <summary>
        /// 
        /// </summary>
        public static Step A =>
            new(5, 0);
        /// <summary>
        /// 
        /// </summary>
        public static Step ASharp =>
            new(5, 1);
        /// <summary>
        /// 
        /// </summary>
        public static Step ADoubleSharp =>
            new(5, 2);

        /// <summary>
        /// 
        /// </summary>
        public static Step BDoubleFlat =>
            new(6, -2);
        /// <summary>
        /// 
        /// </summary>
        public static Step BFlat =>
            new(6, -1);
        /// <summary>
        /// 
        /// </summary>
        public static Step B =>
            new(6, 0);
        /// <summary>
        /// 
        /// </summary>
        public static Step BSharp =>
            new(6, 1);
        /// <summary>
        /// 
        /// </summary>
        public static Step BDoubleSharp =>
            new(6, 2);

        /// <summary>
        /// 
        /// </summary>
        public static Step CDoubleFlat =>
            new(0, -2);
        /// <summary>
        /// 
        /// </summary>
        public static Step CFlat =>
            new(0, -1);
        /// <summary>
        /// 
        /// </summary>
        public static Step C =>
            new(0, 0);
        /// <summary>
        /// 
        /// </summary>
        public static Step CSharp =>
            new(0, 1);
        /// <summary>
        /// 
        /// </summary>
        public static Step CDoubleSharp =>
            new(0, 2);

        /// <summary>
        /// 
        /// </summary>
        public static Step DDoubleFlat =>
            new(1, -2);
        /// <summary>
        /// 
        /// </summary>
        public static Step DFlat =>
            new(1, -1);
        /// <summary>
        /// 
        /// </summary>
        public static Step D =>
            new(1, 0);
        /// <summary>
        /// 
        /// </summary>
        public static Step DSharp =>
            new(1, 1);
        /// <summary>
        /// 
        /// </summary>
        public static Step DDoubleSharp =>
            new(1, 2);


        /// <summary>
        /// 
        /// </summary>
        public static Step EDoubleFlat =>
            new(2, -2);
        /// <summary>
        /// 
        /// </summary>
        public static Step EFlat =>
            new(2, -1);
        /// <summary>
        /// 
        /// </summary>
        public static Step E =>
            new(2, 0);
        /// <summary>
        /// 
        /// </summary>
        public static Step ESharp =>
            new(2, 1);
        /// <summary>
        /// 
        /// </summary>
        public static Step EDoubleSharp =>
            new(2, 2);


        /// <summary>
        /// 
        /// </summary>
        public static Step FDoubleFlat =>
            new(3, -2);
        /// <summary>
        /// 
        /// </summary>
        public static Step FFlat =>
            new(3, -1);
        /// <summary>
        /// 
        /// </summary>
        public static Step F =>
            new(3, 0);
        /// <summary>
        /// 
        /// </summary>
        public static Step FSharp =>
            new(3, 1);
        /// <summary>
        /// 
        /// </summary>
        public static Step FDoubleSharp =>
            new(3, 2);

        /// <summary>
        /// 
        /// </summary>
        public static Step GDoubleFlat =>
            new(4, -2);
        /// <summary>
        /// 
        /// </summary>
        public static Step GFlat =>
            new(4, -1);
        /// <summary>
        /// 
        /// </summary>
        public static Step G =>
            new(4, 0);
        /// <summary>
        /// 
        /// </summary>
        public static Step GSharp =>
            new(4, 1);
        /// <summary>
        /// 
        /// </summary>
        public static Step GDoubleSharp =>
            new(4, 2);



        /// <summary>
        /// The steps taken from c.
        /// </summary>
        public int StepsFromC { get; }
        /// <summary>
        /// The number of shifts, after taking the specified amount of steps.
        /// </summary>
        public int Shifts { get; }



        /// <summary>
        /// Calculates the number of semitones in the step.
        /// </summary>
        public int SemiTones
        {
            get
            {
                int value = 0;

                for (int i = 0; i < StepsFromC; i++)
                {
                    int indexInOctave = i % 12;

                    value += indexInOctave is 2 or 6 ? 1 : 2;
                }

                return MathUtils.UnsignedModulo(value + Shifts, 12);
            }
        }
        /// <summary>
        /// The index in the circle of fifths, for the major scale.
        /// </summary>
        public int PositionCircleOfFifths =>
            SemiTones * 7 % 12;
        /// <summary>
        /// Calculates the relative major step. Assumes a minor scale origin for this step.
        /// For example, the relative major for an 'a' step would be 'c', because the relative major of a minor is c major.
        /// </summary>
        public Step RelativeMajor =>
            new Scale(this, ScaleStructure.Minor).EnumerateSteps().ElementAt(2);
        /// <summary>
        /// Calculates the relative minor step. Assumes a major scale origin for this step.
        /// For example, the relative major for an 'c' step would be 'a', because the relative minor of c major is a minor.
        /// </summary>
        public Step RelativeMinor =>
            new Scale(this, ScaleStructure.Major).EnumerateSteps().ElementAt(5);
        /// <summary>
        /// Calculates the dominant of this step.
        /// </summary>
        public Step Dominant =>
            new Scale(this, ScaleStructure.Major).EnumerateSteps().ElementAt(4);



        /// <summary>
        /// Construct a step from the number of steps from c, and the number of shifts. 
        /// If the number of steps is smaller than 0, an exeption will be thrown.
        /// If the number of shifts is smaller than -2 or larger than 2, an exception will be thrown.
        /// </summary>
        /// <param name="step"></param>
        /// <param name="shifts"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Step(int step, int shifts)
        {
            if (shifts is < (-2) or > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(shifts));
            }

            if (step < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(step));
            }

            StepsFromC = step % 7;
            Shifts = shifts;
        }


        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is not null && obj is Step other && Equals(other);
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return new Tuple<int, int>(StepsFromC, Shifts).GetHashCode();
        }
        /// <inheritdoc/>
        public bool Equals(Step other)
        {
            return other.StepsFromC == StepsFromC && other.Shifts == Shifts;
        }
        /// <summary>
        /// Determines whether the other step resembles this instance.
        /// For example, an a flat resembles a g sharp.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool EqualsOrResembles(Step other)
        {
            return other.SemiTones == SemiTones;
        }

        /// <summary>
        /// Moves this step along the circle of fifths for the given amount of steps. 
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        public Step MoveAlongCircleOfFifths(int steps)
        {
            Step step = this;
            for (int i = 0; i < steps; i++)
            {
                step += Interval.Fifth;
            }

            return step;
        }

        /// <inheritdoc/>
        public static Step operator +(Step step, Interval interval)
        {
            if (interval.Steps == 0)
            {
                return new Step(step.StepsFromC, step.Shifts + interval.Shifts);
            }

            Scale scale = new(step, ScaleStructure.Major);
            Step element = scale.EnumerateSteps(interval.Steps + 1).ElementAt(interval.Steps);
            return new Step(element.StepsFromC, element.Shifts + interval.Shifts);
        }
        /// <inheritdoc/>
        public static bool operator ==(Step step, Step second)
        {
            return step.Equals(second);
        }
        /// <inheritdoc/>
        public static bool operator !=(Step step, Step second)
        {
            return !step.Equals(second);
        }
        /// <inheritdoc/>
        public override string ToString()
        {
            string shift = Shifts switch
            {
                -2 => "bb",
                -1 => "b",
                0 => "",
                1 => "#",
                2 => "##",
                _ => throw new NotSupportedException()
            };

            string step = stepNames.ElementAt(StepsFromC);

            return $"{step}{shift}";
        }
    }
}
