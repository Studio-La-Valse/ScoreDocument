namespace StudioLaValse.ScoreDocument.Core
{
    public class KeySignature : IEquatable<KeySignature>
    {
        public Step Origin { get; }
        public MajorOrMinor MajorOrMinor { get; }

        public bool DefaultFlats =>
            IndexInCircleOfFifths > 6;
        public int IndexInCircleOfFifths =>
            MajorOrMinor == MajorOrMinor.Major ?
                Origin.PositionCircleOfFifths :
                Origin.RelativeMajor.PositionCircleOfFifths;
        public Scale Scale =>
            MajorOrMinor == MajorOrMinor.Major ?
            new Scale(Origin, ScaleStructure.Major) :
            new Scale(Origin, ScaleStructure.Minor);




        public KeySignature(Step step, MajorOrMinor majorOrMinor)
        {
            Origin = step;
            MajorOrMinor = majorOrMinor;
        }




        public IEnumerable<int> EnumerateSharpLines(Clef targetClef)
        {
            return EnumerateSharps()
                .Select(accidental =>
                {
                    var pitch = new Pitch(accidental, 5);
                    var line = targetClef.LineIndexAtPitch(pitch);
                    while(line < targetClef.TopMostSharpLine)
                    {
                        line += 7;
                    }

                    line %= 10;

                    return line;
                });
        }
        
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

        public IEnumerable<Step> EnumerateFlats()
        {
            var accidental = Step.BFlat;
            for (int i = 0; i < NumberOfFlats(); i++)
            {
                yield return accidental;
                accidental += Interval.Fourth;
            }
        }

        public IEnumerable<Step> EnumerateSharps()
        {
            var accidental = Step.FSharp;
            for (int i = 0; i < NumberOfSharps(); i++)
            {
                yield return accidental;
                accidental += Interval.Fifth;
            }
        }

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

        public bool IsAccidentalRedundant(Step pitch)
        {
            var scale = new Scale(Origin, ScaleStructure.Major);
            return scale.Contains(pitch);
        }

        public int NumberOfSharps()
        {
            return IndexInCircleOfFifths % 12;
        }

        public int NumberOfFlats()
        {
            return (12 - IndexInCircleOfFifths) % 12;
        }

        public bool Equals(KeySignature? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Origin.Equals(Origin);
        }

        public static bool operator == (KeySignature? left, KeySignature? right)
        {
            if(left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator != (KeySignature? left, KeySignature? right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as KeySignature);
        }

        public override int GetHashCode()
        {
            return new Tuple<Step, MajorOrMinor>(Origin, MajorOrMinor).GetHashCode();
        }
    }
}
