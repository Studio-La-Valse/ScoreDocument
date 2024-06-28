using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Templates;

namespace StudioLaValse.ScoreDocument.Models
{
    public static class Converters
    {
        public static Instrument Convert(this Classes.InstrumentClass instrument)
        {
            var _instrument = Instrument.CreateCustom(instrument.Name, instrument.Clefs.Select(Convert).ToArray());
            return _instrument;
        }
        public static Classes.InstrumentClass Convert(this Instrument instrument)
        {
            var _instrument = new Classes.InstrumentClass()
            {
                Clefs = instrument.DefaultClefs.Select(Convert).ToList(),
                Name = instrument.Name,
            };
            return _instrument;
        }

        public static RythmicDuration Convert(this Classes.RythmicDurationClass rythmicDuration)
        {
            var _rythmicDuration = new RythmicDuration(rythmicDuration.PowerOfTwo, rythmicDuration.Dots);
            return _rythmicDuration;
        }
        public static Classes.RythmicDurationClass Convert(this RythmicDuration rythmicDuration)
        {
            var _rythmicDuration = new Classes.RythmicDurationClass()
            {
                PowerOfTwo = rythmicDuration.PowerOfTwo,
                Dots = rythmicDuration.Dots
            };
            return _rythmicDuration;
        }

        public static TimeSignature Convert(this Classes.TimeSignatureClass timeSignature)
        {
            var _timeSignature = new TimeSignature(timeSignature.Numerator, timeSignature.Denominator);
            return _timeSignature;
        }
        public static Classes.TimeSignatureClass Convert(this TimeSignature timeSignature)
        {
            var _timeSignature = new Classes.TimeSignatureClass()
            {
                Denominator = timeSignature.Denominator,
                Numerator = timeSignature.Numerator,
            };
            return _timeSignature;
        }

        public static KeySignature Convert(this Classes.KeySignatureClass keySignature)
        {
            var step = keySignature.Step.Convert();
            var _keySignature = new KeySignature(step, keySignature.Major ? MajorOrMinor.Major : MajorOrMinor.Minor);
            return _keySignature;
        }
        public static Classes.KeySignatureClass Convert(this KeySignature keySignature)
        {
            var step = keySignature.Origin.Convert();
            var major = keySignature.MajorOrMinor == MajorOrMinor.Major ? true : false;
            var _keySignature = new Classes.KeySignatureClass()
            {
                Major = major,
                Step = step,
            };
            return _keySignature;
        }

        public static Clef Convert(this string clef)
        {
            var _clef = clef.ToLower() switch
            {
                "treble" => Clef.Treble,
                "soprano" => Clef.Soprano,
                "mezzosoprano" => Clef.MezzoSoprano,
                "alto" => Clef.Alto,
                "tenor" => Clef.Tenor,
                "baritone" => Clef.Baritone,
                "bass" => Clef.Bass,
                _ => throw new NotImplementedException()
            };
            return _clef;
        }
        public static string Convert(this Clef clef)
        {
            return clef.Name.ToString();
        }


        public static ClefChange Convert(this Classes.ClefChangeClass clefChange)
        {
            var _clefChange = new ClefChange(clefChange.Clef.Convert(), clefChange.StaffIndex, clefChange.Position.Convert());
            return _clefChange;
        }
        public static Classes.ClefChangeClass Convert(this ClefChange clefChange)
        {
            var _clefChange = new Classes.ClefChangeClass()
            {
                Clef = clefChange.Clef.Convert(),
                Position = clefChange.Position.Convert(),
                StaffIndex = clefChange.StaffIndex
            };
            return _clefChange;
        }


        public static Step Convert(this Classes.StepClass step)
        {
            var _step = new Step(step.StepsFromC, step.Shifts);
            return _step;
        }
        public static Classes.StepClass Convert(this Step step)
        {
            var _step = new Classes.StepClass()
            {
                StepsFromC = step.StepsFromC,
                Shifts = step.Shifts
            };
            return _step;
        }

        public static Pitch Convert(this Classes.PitchClass pitch)
        {
            var step = pitch.Step.Convert();
            var _pitch = new Pitch(step, pitch.Octave);
            return _pitch;
        }
        public static Classes.PitchClass Convert(this Pitch pitch)
        {
            var _pitch = new Classes.PitchClass()
            {
                Octave = pitch.Octave,
                Step = pitch.Step.Convert()
            };
            return _pitch;
        }

        public static Position Convert(this Classes.PositionClass position)
        {
            var _position = new Position(position.Numerator, position.Denominator);
            return _position;
        }
        public static Classes.PositionClass Convert(this Position position)
        {
            var _position = new Classes.PositionClass()
            {
                Denominator = position.Denominator,
                Numerator = position.Numerator,
            };
            return _position;
        }

        public static ColorARGB Convert(this Classes.ColorARGBClass Color)
        {
            var _color = new ColorARGB()
            {
                A = Color.A,
                B = Color.B,
                G = Color.G,
                R = Color.R,
            };
            return _color;
        }
        public static Classes.ColorARGBClass Convert(this ColorARGB Color)
        {
            var _color = new Classes.ColorARGBClass()
            {
                A = Color.A,
                B = Color.B,
                G = Color.G,
                R = Color.R,
            };
            return _color;
        }


        public static BeamType ConvertBeam(this int i)
        {
            return (BeamType)i;
        }
        public static int ConvertBeam(this BeamType beamType)
        {
            return (int)beamType;
        }
        public static AccidentalDisplay ConvertAccidental(this int i)
        {
            return (AccidentalDisplay)i;
        }
        public static int ConvertAccidental(this AccidentalDisplay accidental)
        {
            return (int)accidental;
        }
    }
}
