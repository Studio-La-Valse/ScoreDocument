//using StudioLaValse.ScoreDocument.Core.Primitives;
//using StudioLaValse.ScoreDocument.Core.Primitives.Extensions;

//namespace StudioLaValse.ScoreDocument.Layout.Extensions
//{
//    /// <summary>
//    /// <see cref="IInstrumentMeasure"/> extensions.
//    /// </summary>
//    public static class RibbonMeasureReaderExtensions
//    {
//        /// <summary>
//        /// Calculates the opening clef of the specified ribbon measure at the specified staff index.
//        /// </summary>
//        /// <param name="ribbonMeasure"></param>
//        /// <param name="staffIndex"></param>
//        /// <param name="scoreLayoutDictionary"></param>
//        /// <returns></returns>
//        public static Clef OpeningClefAtOrDefault<TInstrumentMeasure>(this TInstrumentMeasure ribbonMeasure, int staffIndex, IScoreLayoutProvider scoreLayoutDictionary) where TInstrumentMeasure : IInstrumentMeasure<IMeasureBlockChain, TInstrumentMeasure>, IScoreEntity
//        {
//            InstrumentMeasureLayout layout = scoreLayoutDictionary.InstrumentMeasureLayout(ribbonMeasure);
//            foreach (ClefChange? clefChange in layout.ClefChanges.Where(c => c.Position.Decimal == 0))
//            {
//                if (clefChange.StaffIndex == staffIndex)
//                {
//                    return clefChange.Clef;
//                }
//            }

//            TInstrumentMeasure? previousMeasure = ribbonMeasure;
//            while (previousMeasure.TryReadPrevious(out previousMeasure))
//            {
//                InstrumentMeasureLayout previousLayout = scoreLayoutDictionary.InstrumentMeasureLayout(previousMeasure);
//                foreach (ClefChange? clefChange in previousLayout.ClefChanges.Reverse())
//                {
//                    if (clefChange.StaffIndex == staffIndex)
//                    {
//                        return clefChange.Clef;
//                    }
//                }
//            }

//            Clef spareClef =
//                ribbonMeasure.Instrument.DefaultClefs.ElementAtOrDefault(staffIndex) ??
//                ribbonMeasure.Instrument.DefaultClefs.Last();

//            return spareClef;
//        }

//        /// <summary>
//        /// Calculate the clef at the specified position in the measure.
//        /// </summary>
//        /// <param name="ribbonMeasure"></param>
//        /// <param name="staffIndex"></param>
//        /// <param name="position"></param>
//        /// <param name="scoreLayoutDictionary"></param>
//        /// <returns></returns>
//        public static Clef GetClef<TInstrumentMeasure>(this TInstrumentMeasure ribbonMeasure, int staffIndex, Position position, IScoreLayoutProvider scoreLayoutDictionary) where TInstrumentMeasure : IInstrumentMeasure<IMeasureBlockChain, TInstrumentMeasure>, IScoreEntity
//        {
//            InstrumentMeasureLayout layout = scoreLayoutDictionary.InstrumentMeasureLayout(ribbonMeasure);
//            ClefChange? lastClefChangeInMeasure = layout.ClefChanges
//                .Where(c => c.StaffIndex == staffIndex)
//                .Where(c => c.Position <= position)
//                .OrderByDescending(c => c.Position.Decimal)
//                .FirstOrDefault();
//            return lastClefChangeInMeasure is not null
//                ? lastClefChangeInMeasure.Clef
//                : ribbonMeasure.OpeningClefAtOrDefault(staffIndex, scoreLayoutDictionary);
//        }

//        /// <summary>
//        /// Get the accidental for the pitch at the specified staff index and position of the instrument measure.
//        /// </summary>
//        /// <param name="ribbonMeasure"></param>
//        /// <param name="Pitch"></param>
//        /// <param name="position"></param>
//        /// <param name="staffIndex"></param>
//        /// <param name="scoreLayoutDictionary"></param>
//        /// <returns></returns>
//        public static Accidental? GetAccidental<TInstrumentMeasure, TBlockChain, TBlock, TChord, TNote>(this TInstrumentMeasure ribbonMeasure, Pitch Pitch, Position position, int staffIndex, IScoreLayoutProvider scoreLayoutDictionary) where TInstrumentMeasure : IInstrumentMeasure<IMeasureBlockChain<TBlock>, TInstrumentMeasure>, IScoreEntity
//                                                                                                                                                                                                                                           where TBlockChain : IMeasureBlockChain<TBlock>
//                                                                                                                                                                                                                                           where TBlock : IMeasureBlock<TChord>
//                                                                                                                                                                                                                                           where TChord : IChord<TNote>
//                                                                                                                                                                                                                                           where TNote : INote, IScoreEntity
//        {
//            TNote[] precedingNotes = ribbonMeasure
//                .ReadPrecedingChordsInMeasure<TInstrumentMeasure, TBlockChain, TBlock, TChord>(position)
//                .SelectMany(e => e.ReadNotes())
//                .ToArray();
//            bool precedingNotesWithSamePitch = precedingNotes
//                .Where(n => scoreLayoutDictionary.NoteLayout(n).StaffIndex == staffIndex)
//                .Where(n => n.Pitch.Octave == Pitch.Octave)
//                .Where(n => n.Pitch.StepValue == Pitch.StepValue)
//                .Where(n => n.Pitch.Shift == Pitch.Shift)
//                .Any();
//            if (precedingNotesWithSamePitch)
//            {
//                return null;
//            }

//            //todo: check if note on same line should have natural
//            //example An a natural should have natural if A flat came before
//            //first attempt:
//            bool precedingNotesSameLineDifferentShift = precedingNotes
//                .Where(n => scoreLayoutDictionary.NoteLayout(n).StaffIndex == staffIndex)
//                .Where(n => n.Pitch.Shift != 0)
//                .Where(n => n.Pitch.StepValue == Pitch.StepValue)
//                .Where(n => n.Pitch.Octave == Pitch.Octave)
//                .Any();
//            if (precedingNotesSameLineDifferentShift)
//            {
//                return (Accidental)Pitch.Shift;
//            }

//            KeySignature keySignature = ribbonMeasure.KeySignature;
//            Accidental? systemSays = keySignature.GetAccidentalForPitch(Pitch.Step);
//            return systemSays;
//        }

//        /// <summary>
//        /// Read the chords in the measure that precede the specified position.
//        /// </summary>
//        /// <param name="ribbonMeasure"></param>
//        /// <param name="position"></param>
//        /// <returns></returns>
//        public static IEnumerable<TChord> ReadPrecedingChordsInMeasure<TInstrumentMeasure, TBlockChain, TBlock, TChord>(this TInstrumentMeasure ribbonMeasure, Position position) where TInstrumentMeasure : IInstrumentMeasure<IMeasureBlockChain<TBlock>, TInstrumentMeasure>, IScoreEntity
//                                                                                                                                                                                  where TBlockChain : IMeasureBlockChain<TBlock>
//                                                                                                                                                                                  where TBlock : IMeasureBlock<TChord>
//                                                                                                                                                                                  where TChord : IChord
//        {
//            foreach (int voice in ribbonMeasure.ReadVoices())
//            {
//                IMeasureBlockChain<TBlock> blocks = ribbonMeasure.ReadBlockChainAt(voice);
//                IEnumerable<TChord> chords = blocks.ReadBlocks().SelectMany(b => b.ReadChords());
//                foreach (TChord? chord in chords)
//                {
//                    if (chord.PositionEnd().Decimal > position.Decimal)
//                    {
//                        break;
//                    }

//                    yield return chord;
//                }
//            }
//        }
//    }

//    /// <summary>
//    /// Extensions to staff (-group and -system) readers.
//    /// </summary>
//    public static class StaffGroupReaderExtensions
//    {
//        /// <summary>
//        /// Enumerates the default opening clefs.
//        /// </summary>
//        /// <param name="staffGroup"></param>
//        /// <param name="numberOfStaves"></param>
//        /// <returns></returns>
//        public static IEnumerable<(int, Clef)> EnumerateDefaultInstrumentClefs<TStaffGroup, TStaff>(this TStaffGroup staffGroup, int numberOfStaves) where TStaffGroup : IStaffGroup<TStaff>
//                                                                                                                                                     where TStaff : IStaff
//        {
//            int index = 0;
//            foreach (TStaff staff in staffGroup.EnumerateStaves(numberOfStaves))
//            {
//                Clef clef =
//                    staffGroup.Instrument.DefaultClefs.ElementAtOrDefault(staff.IndexInStaffGroup) ??
//                    staffGroup.Instrument.DefaultClefs.Last();

//                yield return (index, clef);
//                index++;
//            }
//        }

//        /// <summary>
//        /// Calculates the height of a line, assuming the staff starts at the specified canvas top.
//        /// </summary>
//        /// <param name="staff"></param>
//        /// <param name="staffCanvasTop"></param>
//        /// <param name="line"></param>
//        /// <param name="lineSpacing"></param>
//        /// <returns></returns>
//        public static double HeightFromLineIndex<TStaff>(this TStaff staff, double staffCanvasTop, int line, double lineSpacing) where TStaff : IStaff, IScoreEntity
//        {
//            return staffCanvasTop + (line * (lineSpacing / 2));
//        }



//        /// <summary>
//        /// Calculates the height of a note in a staff group, assuming the staff group starts a the specified canvas top.
//        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the staff index specified in the note layout is not present in the staff group.
//        /// </summary>
//        /// <param name="staffGroup"></param>
//        /// <param name="canvasTopStaffGroup"></param>
//        /// <param name="staffIndex"></param>
//        /// <param name="lineIndex"></param>
//        /// <param name="scoreLayoutDictionary"></param>
//        /// <returns></returns>
//        /// <exception cref="ArgumentOutOfRangeException"></exception>
//        public static double HeightOnCanvas<TStaffGroup, TStaff>(this TStaffGroup staffGroup, double canvasTopStaffGroup, int staffIndex, int lineIndex, IScoreLayoutProvider scoreLayoutDictionary) where TStaffGroup : IStaffGroup<TStaff>, IScoreEntity
//                                                                                                                                                                                                     where TStaff : IStaff, IScoreEntity
//        {
//            double staffCanvasTop = canvasTopStaffGroup;
//            TStaff _staff = staffGroup.EnumerateStaves(1).First();
//            var lineSpacing = scoreLayoutDictionary.StaffGroupLayout(staffGroup).LineSpacing.Value;
//            foreach (TStaff staff in staffGroup.EnumerateStaves(staffIndex))
//            {
//                StaffLayout staffLayout = scoreLayoutDictionary.StaffLayout(staff);
//                canvasTopStaffGroup += 4 * lineSpacing;
//                canvasTopStaffGroup += staffLayout.DistanceToNext.Value;
//                _staff = staff;
//            }

//            double canvasTop = _staff.HeightFromLineIndex(staffCanvasTop, lineIndex, lineSpacing);
//            return canvasTop;
//        }

//        /// <summary>
//        /// Calculate the total height of the staff.
//        /// </summary>
//        /// <param name="staff"></param>
//        /// <param name="scoreLayoutDictionary"></param>
//        /// <returns></returns>
//        public static double CalculateHeight<TStaff>(this TStaff staff, double lineSpacing) where TStaff : IStaff, IScoreEntity
//        {
//            double staffHeight = 4 * lineSpacing;

//            return staffHeight;
//        }


//        /// <summary>
//        /// Calculate the total height of the staff group.
//        /// </summary>
//        /// <param name="staffGroup"></param>
//        /// <param name="scoreLayoutDictionary"></param>
//        /// <returns></returns>
//        public static double CalculateHeight<TStaffGroup, TStaff>(this TStaffGroup staffGroup, IScoreLayoutProvider scoreLayoutDictionary) where TStaffGroup : IStaffGroup<TStaff>, IScoreEntity
//                                                                                                                                           where TStaff : IStaff, IScoreEntity
//        {
//            double height = 0d;
//            StaffGroupLayout groupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);
//            if (groupLayout.Collapsed)
//            {
//                return height;
//            }

//            if (!groupLayout.Collapsed)
//            {
//                double lastStaffSpacing = 0d;
//                foreach (TStaff staff in staffGroup.EnumerateStaves(groupLayout.NumberOfStaves.Value))
//                {
//                    double staffHeight = staff.CalculateHeight<TStaff>(scoreLayoutDictionary);
//                    height += staffHeight;

//                    TStaff staffLayout = staff;
//                    lastStaffSpacing = groupLayout.DistanceToNext.Value;
//                    height += lastStaffSpacing;
//                }

//                height -= lastStaffSpacing;
//            }

//            return height;
//        }

//        /// <summary>
//        /// Calculate the total height of the staff system.
//        /// </summary>
//        /// <param name="staffSystem"></param>
//        /// <param name="scoreLayoutDictionary"></param>
//        /// <returns></returns>
//        public static double CalculateHeight<TStaffSystem, TStaffGroup, TStaff>(this TStaffSystem staffSystem, IScoreLayoutProvider scoreLayoutDictionary) where TStaffSystem : IStaffSystem<TStaffGroup>, IScoreEntity
//                                                                                                                                                           where TStaffGroup : IStaffGroup<TStaff>, IScoreEntity
//                                                                                                                                                           where TStaff : IStaff, IScoreEntity
//        {
//            double height = 0d;
//            double lastStafGroupSpacing = 0d;
//            foreach (TStaffGroup staffGroup in staffSystem.EnumerateStaffGroups())
//            {
//                StaffGroupLayout staffGroupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);
//                double staffGroupHeight = staffGroup.CalculateHeight<TStaffGroup, TStaff>(scoreLayoutDictionary);
//                height += staffGroupHeight;

//                lastStafGroupSpacing = staffGroupLayout.DistanceToNext;
//                height += lastStafGroupSpacing;
//            }
//            height -= lastStafGroupSpacing;
//            return height;
//        }
//    }
//}


using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument.Layout.Extensions;

/// <summary>
/// Extensions to staff (-group and -system) readers.
/// </summary>
public static class StaffGroupReaderExtensions
{
    /// <summary>
    /// Enumerates the default opening clefs.
    /// </summary>
    /// <param name="staffGroup"></param>
    /// <param name="numberOfStaves"></param>
    /// <returns></returns>
    public static IEnumerable<(int, Clef)> EnumerateDefaultInstrumentClefs<TStaffGroupReader>(this TStaffGroupReader staffGroup, int numberOfStaves) where TStaffGroupReader : IStaffGroup<IStaff>
    {
        int index = 0;
        foreach (var staff in staffGroup.EnumerateStaves(numberOfStaves))
        {
            Clef clef =
                staffGroup.Instrument.DefaultClefs.ElementAtOrDefault(staff.IndexInStaffGroup) ??
                staffGroup.Instrument.DefaultClefs.Last();

            yield return (index, clef);
            index++;
        }
    }

    /// <summary>
    /// Calculates the height of a line, assuming the staff starts at the specified canvas top.
    /// </summary>
    /// <param name="staff"></param>
    /// <param name="staffCanvasTop"></param>
    /// <param name="line"></param>
    /// <param name="lineSpacing"></param>
    /// <returns></returns>
    public static double HeightFromLineIndex<TStaffReader>(this TStaffReader staff, double staffCanvasTop, int line, double lineSpacing)
    {
        return staffCanvasTop + (line * (lineSpacing / 2));
    }



    /// <summary>
    /// Calculates the height of a note in a staff group, assuming the staff group starts a the specified canvas top.
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the staff index specified in the note layout is not present in the staff group.
    /// </summary>
    /// <param name="staffGroup"></param>
    /// <param name="canvasTopStaffGroup"></param>
    /// <param name="staffIndex"></param>
    /// <param name="lineIndex"></param>
    /// <param name="scoreLayoutDictionary"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static double HeightOnCanvas<TStaffGroupReader, TStaff>(this TStaffGroupReader staffGroup, double canvasTopStaffGroup, int staffIndex, int lineIndex, IScoreLayoutProvider scoreLayoutDictionary) where TStaffGroupReader : IStaffGroup<TStaff>, IScoreEntity
                                                                                                                                                                                                             where TStaff : IStaff, IScoreEntity
    {
        ArgumentOutOfRangeException.ThrowIfNegative(staffIndex, nameof(staffIndex));

        var lineSpacing = scoreLayoutDictionary.StaffGroupLayout(staffGroup).LineSpacing.Value;
        foreach (var staff in staffGroup.EnumerateStaves(staffIndex))
        {
            StaffLayout staffLayout = scoreLayoutDictionary.StaffLayout(staff);
            canvasTopStaffGroup += staff.CalculateHeight(lineSpacing);
            canvasTopStaffGroup += staffLayout.DistanceToNext.Value;
        }

        var _staff = staffGroup.EnumerateStaves(staffIndex + 1).ElementAt(staffIndex);
        double canvasTop = _staff.HeightFromLineIndex(canvasTopStaffGroup, lineIndex, lineSpacing);
        return canvasTop;
    }

    /// <summary>
    /// Calculate the total height of the staff.
    /// </summary>
    /// <param name="staff"></param>
    /// <param name="lineSpacing"></param>
    /// <returns></returns>
    public static double CalculateHeight(this IStaff staff, double lineSpacing)
    {
        double staffHeight = 4 * lineSpacing;

        return staffHeight;
    }


    /// <summary>
    /// Calculate the total height of the staff group.
    /// </summary>
    /// <param name="staffGroup"></param>
    /// <param name="scoreLayoutDictionary"></param>
    /// <returns></returns>
    public static double CalculateHeight<TStaffGroupReader, TStaff>(this TStaffGroupReader staffGroup, IScoreLayoutProvider scoreLayoutDictionary) where TStaffGroupReader : IStaffGroup<TStaff>, IScoreEntity
                                                                                                                                                   where TStaff : IStaff, IScoreEntity
    {
        double height = 0d;
        StaffGroupLayout groupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);
        var lineSpacing = groupLayout.LineSpacing.Value;
        if (groupLayout.Collapsed)
        {
            return height + groupLayout.DistanceToNext.Value;
        }

        double lastStaffSpacing = 0d;
        foreach (var staff in staffGroup.EnumerateStaves(groupLayout.NumberOfStaves.Value))
        {
            double staffHeight = staff.CalculateHeight(lineSpacing);
            double staffSpacing = scoreLayoutDictionary.StaffLayout(staff).DistanceToNext.Value;
            height += staffHeight;
            height += staffSpacing;
            lastStaffSpacing = staffSpacing;
        }

        height -= lastStaffSpacing;

        return height;
    }

    /// <summary>
    /// Calculate the total height of the staff system.
    /// </summary>
    /// <param name="staffSystem"></param>
    /// <param name="scoreLayoutDictionary"></param>
    /// <returns></returns>
    public static double CalculateHeight<TStaffSystemReader, TStaffGroupReader, TStaff>(this TStaffSystemReader staffSystem, IScoreLayoutProvider scoreLayoutDictionary) where TStaffSystemReader : IStaffSystem<TStaffGroupReader>, IScoreEntity
                                                                                                                                                                         where TStaffGroupReader : IStaffGroup<TStaff>, IScoreEntity
                                                                                                                                                                         where TStaff : IStaff, IScoreEntity
    {
        double height = 0d;
        double lastStafGroupSpacing = 0d;
        foreach (var staffGroup in staffSystem.EnumerateStaffGroups())
        {
            StaffGroupLayout staffGroupLayout = scoreLayoutDictionary.StaffGroupLayout(staffGroup);
            double staffGroupHeight = staffGroup.CalculateHeight<TStaffGroupReader, TStaff>(scoreLayoutDictionary);
            height += staffGroupHeight;

            lastStafGroupSpacing = staffGroupLayout.DistanceToNext.Value;
            height += lastStafGroupSpacing;
        }
        height -= lastStafGroupSpacing;
        return height;
    }
}