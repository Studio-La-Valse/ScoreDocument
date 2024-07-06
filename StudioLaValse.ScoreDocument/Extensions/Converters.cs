using StudioLaValse.ScoreDocument.Models.Classes;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// Converts model classes to core types.
    /// </summary>
    public static class Converters
    {
        /// <summary>
        /// Converts an instrument class to a core instrument.
        /// </summary>
        /// <param name="instrument"></param>
        /// <returns></returns>
        public static Instrument Convert(this InstrumentClass instrument)
        {
            var _instrument = Instrument.CreateCustom(instrument.Name, instrument.Clefs.Select(Convert).ToArray());
            return _instrument;
        }
        /// <summary>
        /// Converts a core instrument to an instrument model.
        /// </summary>
        /// <param name="instrument"></param>
        /// <returns></returns>
        public static InstrumentClass Convert(this Instrument instrument)
        {
            var _instrument = new InstrumentClass()
            {
                Clefs = instrument.DefaultClefs.Select(Convert).ToList(),
                Name = instrument.Name,
            };
            return _instrument;
        }
        /// <summary>
        /// Converts a rythmic duration model to a rythmic duration.
        /// </summary>
        /// <param name="rythmicDuration"></param>
        /// <returns></returns>
        public static RythmicDuration Convert(this RythmicDurationClass rythmicDuration)
        {
            var _rythmicDuration = new RythmicDuration(rythmicDuration.PowerOfTwo, rythmicDuration.Dots);
            return _rythmicDuration;
        }
        /// <summary>
        /// Converts a rythmic duration to a model.
        /// </summary>
        /// <param name="rythmicDuration"></param>
        /// <returns></returns>
        public static RythmicDurationClass Convert(this RythmicDuration rythmicDuration)
        {
            var _rythmicDuration = new RythmicDurationClass()
            {
                PowerOfTwo = rythmicDuration.PowerOfTwo,
                Dots = rythmicDuration.Dots
            };
            return _rythmicDuration;
        }
        /// <summary>
        /// Converts a time signature model to a time signature.
        /// </summary>
        /// <param name="timeSignature"></param>
        /// <returns></returns>
        public static TimeSignature Convert(this TimeSignatureClass timeSignature)
        {
            var _timeSignature = new TimeSignature(timeSignature.Numerator, timeSignature.Denominator);
            return _timeSignature;
        }
        /// <summary>
        /// Converts a time signature to a time signature model.
        /// </summary>
        /// <param name="timeSignature"></param>
        /// <returns></returns>
        public static TimeSignatureClass Convert(this TimeSignature timeSignature)
        {
            var _timeSignature = new TimeSignatureClass()
            {
                Denominator = timeSignature.Denominator,
                Numerator = timeSignature.Numerator,
            };
            return _timeSignature;
        }
        /// <summary>
        /// Converts a key signature model to a key signature.
        /// </summary>
        /// <param name="keySignature"></param>
        /// <returns></returns>
        public static KeySignature Convert(this KeySignatureClass keySignature)
        {
            var step = keySignature.Step.Convert();
            var _keySignature = new KeySignature(step, keySignature.Major ? MajorOrMinor.Major : MajorOrMinor.Minor);
            return _keySignature;
        }
        /// <summary>
        /// Converts a key signature to a key signature model.
        /// </summary>
        /// <param name="keySignature"></param>
        /// <returns></returns>
        public static KeySignatureClass Convert(this KeySignature keySignature)
        {
            var step = keySignature.Origin.Convert();
            var major = keySignature.MajorOrMinor == MajorOrMinor.Major ? true : false;
            var _keySignature = new KeySignatureClass()
            {
                Major = major,
                Step = step,
            };
            return _keySignature;
        }
        /// <summary>
        /// Converts a string to a clef.
        /// </summary>
        /// <param name="clef"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
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
        /// <summary>
        /// Converts a clef to a string.
        /// </summary>
        /// <param name="clef"></param>
        /// <returns></returns>
        public static string Convert(this Clef clef)
        {
            return clef.Name.ToString();
        }
        /// <summary>
        /// Converts a clef change model to a clef change.
        /// </summary>
        /// <param name="clefChange"></param>
        /// <returns></returns>
        public static ClefChange Convert(this ClefChangeClass clefChange)
        {
            var _clefChange = new ClefChange(clefChange.Clef.Convert(), clefChange.StaffIndex, clefChange.Position.Convert());
            return _clefChange;
        }
        /// <summary>
        /// Converts a clef change to a clef change model.
        /// </summary>
        /// <param name="clefChange"></param>
        /// <returns></returns>
        public static ClefChangeClass Convert(this ClefChange clefChange)
        {
            var _clefChange = new ClefChangeClass()
            {
                Clef = clefChange.Clef.Convert(),
                Position = clefChange.Position.Convert(),
                StaffIndex = clefChange.StaffIndex
            };
            return _clefChange;
        }

        /// <summary>
        /// Converts a step model to a step.
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public static Step Convert(this StepClass step)
        {
            var _step = new Step(step.StepsFromC, step.Shifts);
            return _step;
        }
        /// <summary>
        /// Converts a step to a step model.
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public static StepClass Convert(this Step step)
        {
            var _step = new StepClass()
            {
                StepsFromC = step.StepsFromC,
                Shifts = step.Shifts
            };
            return _step;
        }
        /// <summary>
        /// Converts a pitch model to a pich.
        /// </summary>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public static Pitch Convert(this PitchClass pitch)
        {
            var step = pitch.Step.Convert();
            var _pitch = new Pitch(step, pitch.Octave);
            return _pitch;
        }
        /// <summary>
        /// Converts a pitch to a pitch model.
        /// </summary>
        /// <param name="pitch"></param>
        /// <returns></returns>
        public static PitchClass Convert(this Pitch pitch)
        {
            var _pitch = new PitchClass()
            {
                Octave = pitch.Octave,
                Step = pitch.Step.Convert()
            };
            return _pitch;
        }
        /// <summary>
        /// Converts a position model to a position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Position Convert(this PositionClass position)
        {
            var _position = new Position(position.Numerator, position.Denominator);
            return _position;
        }
        /// <summary>
        /// Converts a position to a position model.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static PositionClass Convert(this Position position)
        {
            var _position = new PositionClass()
            {
                Denominator = position.Denominator,
                Numerator = position.Numerator,
            };
            return _position;
        }
        /// <summary>
        /// Converts a color argb model to a color.
        /// </summary>
        /// <param name="Color"></param>
        /// <returns></returns>
        public static ColorARGB Convert(this ColorARGBClass Color)
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
        /// <summary>
        /// Converts a color to a color model.
        /// </summary>
        /// <param name="Color"></param>
        /// <returns></returns>
        public static ColorARGBClass Convert(this ColorARGB Color)
        {
            var _color = new ColorARGBClass()
            {
                A = Color.A,
                B = Color.B,
                G = Color.G,
                R = Color.R,
            };
            return _color;
        }

        /// <summary>
        /// Converts an integer to a beamtype.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static BeamType ConvertBeam(this int i)
        {
            return (BeamType)i;
        }
        /// <summary>
        /// Converts a beamtype to an integer.
        /// </summary>
        /// <param name="beamType"></param>
        /// <returns></returns>
        public static int ConvertBeam(this BeamType beamType)
        {
            return (int)beamType;
        }
        /// <summary>
        /// Converts an integer to a accidental display type.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static AccidentalDisplay ConvertAccidental(this int i)
        {
            return (AccidentalDisplay)i;
        }
        /// <summary>
        /// Converts an accidental to an integer.
        /// </summary>
        /// <param name="accidental"></param>
        /// <returns></returns>
        public static int ConvertAccidental(this AccidentalDisplay accidental)
        {
            return (int)accidental;
        }
        /// <summary>
        /// Converts an integer to a stem direction.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static StemDirection ConvertStemDirection(this int i)
        {
            return (StemDirection)i;
        }
        /// <summary>
        /// Converts a stem direciton to an integer.
        /// </summary>
        /// <param name="accidental"></param>
        /// <returns></returns>
        public static int ConvertStemDirection(this StemDirection accidental)
        {
            return (int)accidental;
        }
    }
}
