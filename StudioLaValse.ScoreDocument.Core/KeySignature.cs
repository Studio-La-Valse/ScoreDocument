namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical key signature.
    /// </summary>
    public class KeySignature : IEquatable<KeySignature>
    {
        /// <summary>
        /// The origin of the key signature.
        /// </summary>
        public Step Origin { get; }
        /// <summary>
        /// Whether the key signature is major or minor.
        /// </summary>
        public MajorOrMinor MajorOrMinor { get; }

        /// <summary>
        /// Whether the keysignature should by default be displayed using flats instead of sharps.
        /// </summary>
        public bool DefaultFlats =>
            IndexInCircleOfFifths > 6;
        /// <summary>
        /// The index in the circle of fifths.
        /// </summary>
        public int IndexInCircleOfFifths =>
            MajorOrMinor == MajorOrMinor.Major ?
                Origin.PositionCircleOfFifths :
                Origin.RelativeMajor.PositionCircleOfFifths;
        /// <summary>
        /// Construct a default scale from this key signature.
        /// </summary>
        public Scale Scale =>
            MajorOrMinor == MajorOrMinor.Major ?
            new Scale(Origin, ScaleStructure.Major) :
            new Scale(Origin, ScaleStructure.Minor);



        /// <summary>
        /// Construct a key signature from its origin and major or minor.
        /// </summary>
        /// <param name="step"></param>
        /// <param name="majorOrMinor"></param>
        public KeySignature(Step step, MajorOrMinor majorOrMinor)
        {
            Origin = step;
            MajorOrMinor = majorOrMinor;
        }



        /// <summary>
        /// Enumerate the lines on which sharps are displayed for the specified clef.
        /// </summary>
        /// <param name="targetClef"></param>
        /// <returns></returns>
        public IEnumerable<int> EnumerateSharpLines(Clef targetClef)
        {
            return EnumerateSharps()
                .Select(accidental =>
                {
                    var pitch = new Pitch(accidental, 5);
                    var line = targetClef.LineIndexAtPitch(pitch);
                    while (line < targetClef.TopMostSharpLine)
                    {
                        line += 7;
                    }

                    line %= 10;

                    return line;
                });
        }

        /// <summary>
        /// Enumerate the lines on which flats are displayed for the specified clef.
        /// </summary>
        /// <param name="targetClef"></param>
        /// <returns></returns>
        public IEnumerable<int> EnumerateFlatLines(Clef targetClef)
        {
            var highestPitchOnTrebleClef = new Pitch(Step.EFlat, 5);

            return EnumerateFlats()
                .Select(accidental =>
                {
                    var pitch = new Pitch(accidental, 5);
                    var line = targetClef.LineIndexAtPitch(pitch);
                    while (line < targetClef.TopMostFlatLine)
                    {
                        line += 7;
                    }

                    line %= 10;

                    return line;
                });
        }

        /// <summary>
        /// Enumerate the steps that are flattened by the key signature.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Step> EnumerateFlats()
        {
            var accidental = Step.BFlat;
            for (int i = 0; i < NumberOfFlats(); i++)
            {
                yield return accidental;
                accidental += Interval.Fourth;
            }
        }

        /// <summary>
        /// Enumerate the steps that are sharpened by the key signature.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Step> EnumerateSharps()
        {
            var accidental = Step.FSharp;
            for (int i = 0; i < NumberOfSharps(); i++)
            {
                yield return accidental;
                accidental += Interval.Fifth;
            }
        }

        /// <summary>
        /// For the specified step, return null if the step is sharpened or flattened by the keysignature, otherwise the accidental represented by the step's pitch.
        /// </summary>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public Accidental? GetAccidentalForPitch(Step pitch)
        {
            if (IsAccidentalRedundant(pitch))
            {
                return null;
            }
            else
            {
                return (Accidental)pitch.Shifts;
            }
        }

        /// <summary>
        /// Returns true if the step is sharpened or flattened by the key signature.
        /// </summary>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public bool IsAccidentalRedundant(Step pitch)
        {
            var scale = new Scale(Origin, ScaleStructure.Major);
            return scale.Contains(pitch);
        }

        /// <summary>
        /// Calculate the number of sharps in the key signature.
        /// </summary>
        /// <returns></returns>
        public int NumberOfSharps()
        {
            return IndexInCircleOfFifths % 12;
        }

        /// <summary>
        /// Calculate the number of flats in the key signature.
        /// </summary>
        /// <returns></returns>
        public int NumberOfFlats()
        {
            return (12 - IndexInCircleOfFifths) % 12;
        }

        /// <inheritdoc/>
        public bool Equals(KeySignature? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Origin.Equals(Origin);
        }

        /// <summary>
        /// Determines whether the two key signatures are equal by their properties.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(KeySignature? left, KeySignature? right)
        {
            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the two key signatures are different by their properties.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(KeySignature? left, KeySignature? right)
        {
            return !(left == right);
        }
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as KeySignature);
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return new Tuple<Step, MajorOrMinor>(Origin, MajorOrMinor).GetHashCode();
        }
    }
}
