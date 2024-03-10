﻿using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml.Private
{
    internal static class XElementExtensions
    {
        public static double ToDoubleOrThrow(this string s)
        {
            return !double.TryParse(s, out double result) ? throw new Exception() : result;
        }
        public static int ToIntOrThrow(this string s)
        {
            return !int.TryParse(s, out int result) ? throw new Exception() : result;
        }
        public static int? ToIntOrNull(this string s)
        {
            return s is null ? null : !int.TryParse(s, out int result) ? throw new Exception() : result;
        }

        public static IEnumerable<int> ExtractVoices(this XElement measure)
        {
            return measure
                .Descendants()
                .Select(ExtractVoice)
                .OfType<int>()
                .Distinct();
        }

        public static IEnumerable<XElement> ExtractElements(this XElement measure, int voice)
        {
            return measure
                .Descendants()
                .Where(IsNoteOrForwardOrBackup)
                .SkipWhile(e =>
                {
                    int? _voice = e.ExtractVoice();
                    return _voice is null || _voice.Value < voice;
                })
                .TakeWhile(e =>
                {
                    int? _voice = e.ExtractVoice();
                    return _voice == voice || _voice is null;
                });
        }

        public static int? ExtractVoice(this XElement element)
        {
            return element
                .Descendants()
                .SingleOrDefault(d => d.Name == "voice")?.Value.ToIntOrNull();
        }

        public static bool IsNoteOrForwardOrBackup(this XElement element)
        {
            return element.Name == "note" || element.Name == "forward" || element.Name == "backup";
        }

        public static PowerOfTwo FromTypeString(this string @string)
        {
            return @string switch
            {
                "maxima" => throw new NotSupportedException("A maxima is not yet supported."),
                "breve" => throw new NotSupportedException("A breve is not yet supported."),
                "whole" => new PowerOfTwo(0),
                "half" => new PowerOfTwo(1),
                "quarter" => new PowerOfTwo(2),
                "eighth" => new PowerOfTwo(3),
                "16th" => new PowerOfTwo(4),
                "32nd" => new PowerOfTwo(5),
                "64th" => new PowerOfTwo(6),
                "128th" => new PowerOfTwo(7),
                "256th" => new PowerOfTwo(8),
                "512th" => new PowerOfTwo(9),
                "1024th" => new PowerOfTwo(10),
                _ => throw new NotSupportedException($"{@string} is not a recognized rythmic duration or power of two."),
            };
        }

        public static Pitch ParsePitch(this XElement measureElement)
        {
            string step = measureElement.Descendants().Single(d => d.Name == "step").Value;
            string octave = measureElement.Descendants().Single(d => d.Name == "octave").Value;
            string? alter = measureElement.Descendants().SingleOrDefault(d => d.Name == "alter")?.Value;

            int octaveInt = octave.ToIntOrThrow();

            Step _step = step.ToLower() switch
            {
                "c" => Step.C,
                "d" => Step.D,
                "e" => Step.E,
                "f" => Step.F,
                "g" => Step.G,
                "a" => Step.A,
                "b" => Step.B,
                _ => throw new NotSupportedException()
            };

            int alterInt = alter is null ? 0 : alter.ToIntOrThrow();

            Step stepAlter = new(_step.StepsFromC, alterInt);

            return new Pitch(stepAlter, octaveInt);
        }
    }
}