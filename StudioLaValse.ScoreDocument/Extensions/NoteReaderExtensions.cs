namespace StudioLaValse.ScoreDocument.Extensions
{
    /// <summary>
    /// Extensions to the <see cref="INoteReader"/> interface.
    /// </summary>
    public static class NoteReaderExtensions
    {
        /// <summary>
        /// Read all chords in the measure that appear before the specified note reader.
        /// </summary>
        /// <param name="noteReader"></param>
        /// <returns></returns>
        public static IEnumerable<IChordReader> ReadPrecedingChordsInMeasure(this INoteReader noteReader)
        {
            var ribbonMeasure = noteReader
                .ReadContext()
                .ReadContext();

            foreach (var voice in ribbonMeasure.EnumerateVoices())
            {
                var blocks = ribbonMeasure.ReadBlocks(voice);
                var chords = blocks.SelectMany(b => b.ReadChords());
                foreach (var chord in chords)
                {
                    if (chord.PositionEnd().Decimal > noteReader.Position.Decimal)
                    {
                        break;
                    }

                    yield return chord;
                }
            }
        }

        /// <summary>
        /// Calculates the appropriate accidental for the note. Takes into account the layout of the note.
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static Accidental? GetAccidental(this INoteReader note)
        {
            var forceAccidental = note.ReadLayout().ForceAccidental;
            if (forceAccidental != AccidentalDisplay.Default)
            {
                if (forceAccidental == AccidentalDisplay.ForceOn)
                {
                    return (Accidental)note.Pitch.Shift;
                }
                else
                {
                    return null;
                }
            }

            var precedingNotesWithSamePitch = note.ReadPrecedingChordsInMeasure()
                .SelectMany(e => e.ReadNotes())
                .Where(n => n.Pitch.Octave == note.Pitch.Octave)
                .Where(n => n.Pitch.StepValue == note.Pitch.StepValue)
                .Where(n => n.Pitch.Shift == note.Pitch.Shift)
                .Any();
            if (precedingNotesWithSamePitch)
            {
                return null;
            }

            //todo: check if note on same line should have natural
            //example An a natural should have natural if A flat came before
            //first attempt:
            var precedingNotesSameLineDifferentShift = note.ReadPrecedingChordsInMeasure()
                .SelectMany(e => e.ReadNotes())
                .Where(n => n.Pitch.Shift != 0)
                .Where(n => n.Pitch.StepValue == note.Pitch.StepValue)
                .Where(n => n.Pitch.Octave == note.Pitch.Octave)
                .Any();
            if (precedingNotesSameLineDifferentShift)
            {
                return (Accidental)note.Pitch.Shift;
            }

            var keySignature = note.ReadContext().ReadContext().ReadMeasureContext().ReadLayout().KeySignature;
            var systemSays = keySignature.GetAccidentalForPitch(note.Pitch.Step);
            return systemSays;
        }

        /// <summary>
        /// Gets the clef for the note. Takes into account the staff index specified in the layout of the ntoe.
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static Clef GetClef(this INoteReader note)
        {
            var hostMeasure = note.ReadContext().ReadContext();
            var hostMeasureLayout = hostMeasure.ReadLayout();
            var noteLayout = note.ReadLayout();
            var staffIndex = noteLayout.StaffIndex;

            var lastClefChangeInMeasure = hostMeasureLayout.ClefChanges
                .Where(c => c.StaffIndex == staffIndex)
                .Where(c => c.Position <= note.Position)
                .OrderByDescending(c => c.Position.Decimal)
                .FirstOrDefault();
            if (lastClefChangeInMeasure is not null)
            {
                return lastClefChangeInMeasure.Clef;
            }

            return hostMeasure.OpeningClefAtOrDefault(staffIndex);
        }
    }
}
