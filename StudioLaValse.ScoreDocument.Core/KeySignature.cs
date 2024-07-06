namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical key signature.
    /// </summary>
    public readonly struct KeySignature : IEquatable<KeySignature>
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
        /// Please consider the difference between major and minor positions. 
        /// The minor position is equal to the relative major position.
        /// In other words, the position for C Major (0), is equal to the position of A Minor (also 0).
        /// The position for G Major (1), is equal to the position of E Minor (also 1), etcetera.
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
                    Pitch pitch = new(accidental, 5);
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
            Pitch highestPitchOnTrebleClef = new(Step.EFlat, 5);

            return EnumerateFlats()
                .Select(accidental =>
                {
                    Pitch pitch = new(accidental, 5);
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
            for (var i = 0; i < NumberOfFlats(); i++)
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
            for (var i = 0; i < NumberOfSharps(); i++)
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
            return IsAccidentalRedundant(pitch) ? null : (Accidental)pitch.Shifts;
        }

        /// <summary>
        /// Returns true if the step is sharpened or flattened by the key signature.
        /// </summary>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public bool IsAccidentalRedundant(Step pitch)
        {
            Scale scale = new(Origin, ScaleStructure.Major);
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
        public readonly bool Equals(KeySignature other)
        {
            return other.MajorOrMinor == MajorOrMinor
                ? other.Origin.Equals(Origin)
                : MajorOrMinor == MajorOrMinor.Minor
                    ? other.Origin.RelativeMinor.Equals(Origin)
                    : other.Origin.RelativeMajor.Equals(Origin);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is KeySignature signature && Equals(signature);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return new Tuple<Step, MajorOrMinor>(Origin, MajorOrMinor.Minor).GetHashCode();
        }

        /// <inheritdoc/>
        public static bool operator ==(KeySignature left, KeySignature right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(KeySignature left, KeySignature right)
        {
            return !(left == right);
        }
    }
}
