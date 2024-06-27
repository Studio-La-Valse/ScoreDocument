namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// <see cref="IInstrumentMeasure"/> extensions.
    /// </summary>
    public static class RibbonMeasureReaderExtensions
    {
        /// <summary>
        /// Read all the chords that precede the position in this measure.
        /// </summary>
        /// <param name="ribbonMeasure"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static IEnumerable<IChord> ReadUntil(this IInstrumentMeasure ribbonMeasure, Position position)
        {
            foreach (var voice in ribbonMeasure.ReadVoices())
            {
                var blocks = ribbonMeasure.ReadBlockChainAt(voice);
                var chords = blocks.ReadBlocks().SelectMany(b => b.ReadChords());
                foreach (var chord in chords)
                {
                    if (chord.PositionEnd().Decimal > position.Decimal)
                    {
                        break;
                    }

                    yield return chord;
                }
            }
        }

        /// <summary>
        /// Read all the chords that precede the position in this measure.
        /// </summary>
        /// <param name="ribbonMeasure"></param>
        /// <param name="position"></param>
        /// <param name="voice"></param>
        /// <returns></returns>
        public static IEnumerable<IChord> ReadUntil(this IInstrumentMeasure ribbonMeasure, Position position, int voice)
        {
            var blocks = ribbonMeasure.ReadBlockChainAt(voice);
            var chords = blocks.ReadBlocks().SelectMany(b => b.ReadChords());
            foreach (var chord in chords)
            {
                if (chord.PositionEnd().Decimal > position.Decimal)
                {
                    break;
                }

                yield return chord;
            }
        }

        /// <summary>
        /// Read all notes in the instrument measure.
        /// </summary>
        /// <param name="instrumentMeasureReader"></param>
        /// <returns></returns>
        public static IEnumerable<INote> ReadNotes(this IInstrumentMeasure instrumentMeasureReader)
        {
            foreach (var chord in instrumentMeasureReader.ReadChords())
            {
                foreach (var note in chord.ReadNotes())
                {
                    yield return note;
                }
            }
        }

        /// <summary>
        /// Read all notes with the specified voice in the instrument measure.
        /// </summary>
        /// <param name="instrumentMeasureReader"></param>
        /// <param name="voice"></param>
        /// <returns></returns>
        public static IEnumerable<INote> ReadNotes(this IInstrumentMeasure instrumentMeasureReader, int voice)
        {
            foreach (var chord in instrumentMeasureReader.ReadChords(voice))
            {
                foreach (var note in chord.ReadNotes())
                {
                    yield return note;
                }
            }
        }


        /// <summary>
        /// Read all chords in the instrument measure.
        /// </summary>
        /// <param name="instrumentMeasureReader"></param>
        /// <returns></returns>
        public static IEnumerable<IChord> ReadChords(this IInstrumentMeasure instrumentMeasureReader)
        {
            foreach (var voice in instrumentMeasureReader.ReadVoices())
            {
                foreach (var block in instrumentMeasureReader.ReadBlockChainAt(voice).ReadBlocks())
                {
                    foreach (var chord in block.ReadChords())
                    {
                        yield return chord;
                    }
                }
            }
        }

        /// <summary>
        /// Read all chords with the specified voice in the instrument measure.
        /// </summary>
        /// <param name="instrumentMeasureReader"></param>
        /// <param name="voice"></param>
        /// <returns></returns>
        public static IEnumerable<IChord> ReadChords(this IInstrumentMeasure instrumentMeasureReader, int voice)
        {
            foreach (var block in instrumentMeasureReader.ReadBlockChainAt(voice).ReadBlocks())
            {
                foreach (var chord in block.ReadChords())
                {
                    yield return chord;
                }
            }
        }

        /// <summary>
        /// Calculates the opening clef of the specified ribbon measure at the specified staff index.
        /// </summary>
        /// <param name="ribbonMeasure"></param>
        /// <param name="staffIndex"></param>
        /// <returns></returns>
        public static Clef OpeningClefAtOrDefault(this IInstrumentMeasure ribbonMeasure, int staffIndex)
        {
            foreach (var clefChange in ribbonMeasure.ClefChanges.Where(c => c.Position.Decimal == 0))
            {
                if (clefChange.StaffIndex == staffIndex)
                {
                    return clefChange.Clef;
                }
            }

            var previousMeasure = ribbonMeasure;
            while (previousMeasure.TryReadPrevious(out previousMeasure))
            {
                foreach (var clefChange in previousMeasure.ClefChanges.Reverse())
                {
                    if (clefChange.StaffIndex == staffIndex)
                    {
                        return clefChange.Clef;
                    }
                }
            }

            var spareClef =
                ribbonMeasure.Instrument.DefaultClefs.ElementAtOrDefault(staffIndex) ??
                ribbonMeasure.Instrument.DefaultClefs.Last();

            return spareClef;
        }

        /// <summary>
        /// Calculate the clef at the specified position in the measure.
        /// </summary>
        /// <param name="ribbonMeasure"></param>
        /// <param name="staffIndex"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Clef GetClef(this IInstrumentMeasure ribbonMeasure, int staffIndex, Position position)
        {
            var lastClefChangeInMeasure = ribbonMeasure.ClefChanges
                .Where(c => c.StaffIndex == staffIndex)
                .Where(c => c.Position <= position)
                .OrderByDescending(c => c.Position.Decimal)
                .FirstOrDefault();
            return lastClefChangeInMeasure is not null
                ? lastClefChangeInMeasure.Clef
                : ribbonMeasure.OpeningClefAtOrDefault(staffIndex);
        }

        /// <summary>
        /// Get the accidental for the pitch at the specified staff index and position of the instrument measure.
        /// </summary>
        /// <param name="ribbonMeasure"></param>
        /// <param name="Pitch"></param>
        /// <param name="position"></param>
        /// <param name="staffIndex"></param>
        /// <returns></returns>
        public static Accidental? GetAccidental(this IInstrumentMeasure ribbonMeasure, Pitch Pitch, Position position, int staffIndex)
        {
            var precedingNotes = ribbonMeasure
                .ReadUntil(position)
                .SelectMany(e => e.ReadNotes())
                .ToArray();
            var precedingNotesWithSamePitch = precedingNotes
                .Where(n => n.StaffIndex == staffIndex)
                .Where(n => n.Pitch.Octave == Pitch.Octave)
                .Where(n => n.Pitch.StepValue == Pitch.StepValue)
                .Where(n => n.Pitch.Shift == Pitch.Shift)
                .Any();
            if (precedingNotesWithSamePitch)
            {
                return null;
            }

            //todo: check if note on same line should have natural
            //example An a natural should have natural if A flat came before
            //first attempt:
            var precedingNotesSameLineDifferentShift = precedingNotes
                .Where(n => n.StaffIndex == staffIndex)
                .Where(n => n.Pitch.Shift != 0)
                .Where(n => n.Pitch.StepValue == Pitch.StepValue)
                .Where(n => n.Pitch.Octave == Pitch.Octave)
                .Any();
            if (precedingNotesSameLineDifferentShift)
            {
                return (Accidental)Pitch.Shift;
            }

            var keySignature = ribbonMeasure.KeySignature;
            var systemSays = keySignature.GetAccidentalForPitch(Pitch.Step);
            return systemSays;
        }
    }
}
