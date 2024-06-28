using StudioLaValse.ScoreDocument.Private;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioLaValse.ScoreDocument.Extensions.Private;

/// <summary>
/// Extensions for grace type elements.
/// </summary>
internal static class GraceExtensions
{
    /// <summary>
    /// Imply a regular chord from a grace chord.
    /// </summary>
    /// <param name="graceChordReader"></param>
    /// <param name="measureBlockReaderFromGraceGroup"></param>
    /// <returns></returns>
    internal static IChord Imply(this IGraceChord graceChordReader, MeasureBlockReaderFromGraceGroup measureBlockReaderFromGraceGroup)
    {
        return new ChordReaderFromGraceChord(graceChordReader, measureBlockReaderFromGraceGroup);
    }

    /// <summary>
    /// Imply a regular note from a grace note.
    /// </summary>
    /// <param name="noteReader"></param>
    /// <param name="chordReaderFromGraceChord"></param>
    /// <returns></returns>
    internal static INote Imply(this IGraceNote noteReader, ChordReaderFromGraceChord chordReaderFromGraceChord)
    {
        return new NoteReaderFromGraceChord(noteReader, chordReaderFromGraceChord);
    }
}
