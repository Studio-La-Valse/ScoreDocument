﻿using StudioLaValse.ScoreDocument.Core;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace StudioLaValse.ScoreDocument.MusicXml.Private
{
    internal static class XElementExtensions
    {
        public static double ToDoubleOrThrow(this string s)
        {
            return !double.TryParse(s, out var result) ? throw new Exception() : result;
        }
        public static int ToIntOrThrow(this string s)
        {
            return !int.TryParse(s, out var result) ? throw new Exception() : result;
        }
        public static int? ToIntOrNull(this string s)
        {
            return s is null ? null : !int.TryParse(s, out var result) ? throw new Exception() : result;
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
                    var _voice = e.ExtractVoice();
                    return _voice is null || _voice.Value < voice;
                })
                .TakeWhile(e =>
                {
                    var _voice = e.ExtractVoice();
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

        public static bool IsNoteOrRest(this XElement element)
        {
            return element.Name == "note";
        }

        public static int? StaffIndex(this XElement element)
        {
            return element.Descendants().SingleOrDefault(d => d.Name == "staff")?.Value.ToIntOrThrow() - 1;
        }

        public static IEnumerable<string> GetBeams(this XElement element)
        {
            var beams = element.Descendants().Where(a => a.Name == "beam").Select(e => e.Value);
            return beams;
        }

        public static bool IsNoteOrForward(this XElement element)
        {
            return element.Name == "note" || element.Name == "forward";
        }

        public static bool IsBackward(this XElement element)
        {
            return element.Name == "backup";
        }

        public static bool IsGrace(this XElement element)
        {
            return element.Descendants().Any(d => d.Name == "grace");
        }

        public static bool IsChord(this XElement element)
        {
            return element.Element("chord") != null;
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

        public static bool TryParsePitch(this XElement element, out Pitch pitch)
        {
            pitch = default;

            var step = element.Descendants().FirstOrDefault(d => d.Name == "step")?.Value;
            if(step == null)
            {
                return false;
            }

            var octave = element.Descendants().FirstOrDefault(d => d.Name == "octave")?.Value;
            if(octave == null)
            {
                return false;
            }

            var alter = element.Descendants().FirstOrDefault(d => d.Name == "alter")?.Value;
            var alterInt = alter is null ? 0 : alter.ToIntOrThrow();

            var octaveInt =  0;
            if(int.TryParse(octave, out var result))
            {
                octaveInt = result;
            }
            else
            {
                return false;
            }

            var _step = step.ToLower() switch
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

            Step stepAlter = new(_step.StepsFromC, alterInt);

            pitch = new Pitch(stepAlter, octaveInt);
            return true;
        }

        public static Pitch ParsePitch(this XElement measureElement)
        {
            if(measureElement.TryParsePitch(out var pitch))
            {
                return pitch;
            }

            throw new InvalidOperationException($"XEelemnt {measureElement} does not soom to have a pitch.");
        }
    }
}