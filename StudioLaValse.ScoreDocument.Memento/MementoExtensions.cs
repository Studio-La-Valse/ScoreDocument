namespace StudioLaValse.ScoreDocument.Memento
{
    /// <summary>
    /// All extensions related to the memento namespace.
    /// </summary>
    public static class MementoExtensions
    {
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="scoreDocument"></param>
        /// <returns></returns>
        public static ScoreDocumentMemento GetMemento(this IScoreDocumentReader scoreDocument)
        {
            return new ScoreDocumentMemento
            {
                Layout = scoreDocument.ReadLayout().Copy(),
                InstrumentRibbons = scoreDocument.ReadInstrumentRibbons().Select(e => e.GetMemento()).ToList(),
                ScoreMeasures = scoreDocument.ReadScoreMeasures().Select(e => e.GetMemento()).ToList()
            };
        }
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="scoreDocument"></param>
        /// <returns></returns>
        public static ScoreDocumentMemento GetMemento(this IScoreDocumentEditor scoreDocument)
        {
            return new ScoreDocumentMemento
            {
                Layout = scoreDocument.ReadLayout().Copy(),
                InstrumentRibbons = scoreDocument.EditInstrumentRibbons().Select(e => e.GetMemento()).ToList(),
                ScoreMeasures = scoreDocument.EditScoreMeasures().Select(e => e.GetMemento()).ToList()
            };
        }
        /// <summary>
        /// Apply the memento to the specified element.
        /// </summary>
        /// <param name="scoreDocument"></param>
        /// <param name="memento"></param>
        public static void ApplyMemento(this IScoreDocumentEditor scoreDocument, ScoreDocumentMemento memento)
        {
            scoreDocument.Clear();

            scoreDocument.ApplyLayout(memento.Layout);

            foreach (var instrument in memento.InstrumentRibbons)
            {
                scoreDocument.AddInstrumentRibbon(instrument.Instrument);
            }

            foreach (var scoreMeasureMemento in memento.ScoreMeasures)
            {
                scoreDocument.AppendScoreMeasure(scoreMeasureMemento.TimeSignature);
                var scoreMeasure = scoreDocument.EditScoreMeasures().Last();
                scoreMeasure.ApplyMemento(scoreMeasureMemento);
            }
        }


        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="instrumentRibbon"></param>
        /// <returns></returns>
        public static InstrumentRibbonMemento GetMemento(this IInstrumentRibbonReader instrumentRibbon)
        {
            return new InstrumentRibbonMemento
            {
                Layout = instrumentRibbon.ReadLayout().Copy(),
                Measures = instrumentRibbon.ReadMeasures().Select(e => e.GetMemento()).ToList(),
                Instrument = instrumentRibbon.Instrument
            };
        }
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="instrumentRibbon"></param>
        /// <returns></returns>
        public static InstrumentRibbonMemento GetMemento(this IInstrumentRibbonEditor instrumentRibbon)
        {
            return new InstrumentRibbonMemento
            {
                Layout = instrumentRibbon.ReadLayout().Copy(),
                Measures = instrumentRibbon.EditMeasures().Select(e => e.GetMemento()).ToList(),
                Instrument = instrumentRibbon.Instrument
            };
        }
        /// <summary>
        /// Apply the memento to the specified element.
        /// </summary>
        /// <param name="instrumentRibbon"></param>
        /// <param name="memento"></param>
        public static void ApplyMemento(this IInstrumentRibbonEditor instrumentRibbon, InstrumentRibbonMemento memento)
        {
            instrumentRibbon.ApplyLayout(memento.Layout);

            foreach (var measureMemento in memento.Measures)
            {
                var measure = instrumentRibbon.EditMeasure(measureMemento.MeasureIndex);
                measure.ApplyMemento(measureMemento);
            }
        }


        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="scoreMeasure"></param>
        /// <returns></returns>
        public static ScoreMeasureMemento GetMemento(this IScoreMeasureReader scoreMeasure)
        {
            return new ScoreMeasureMemento
            {
                Measures = scoreMeasure.ReadMeasures().Select(e => e.GetMemento()).ToList(),
                Layout = scoreMeasure.ReadLayout().Copy(),
                TimeSignature = scoreMeasure.TimeSignature,
                StaffSystem = scoreMeasure.ReadStaffSystemOrigin().GetMemento()
            };
        }
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="scoreMeasure"></param>
        /// <returns></returns>
        public static ScoreMeasureMemento GetMemento(this IScoreMeasureEditor scoreMeasure)
        {
            return new ScoreMeasureMemento
            {
                Measures = scoreMeasure.EditMeasures().Select(e => e.GetMemento()).ToList(),
                Layout = scoreMeasure.ReadLayout().Copy(),
                TimeSignature = scoreMeasure.TimeSignature,
                StaffSystem = scoreMeasure.EditStaffSystemOrigin().GetMemento()
            };
        }
        /// <summary>
        /// Apply the memento to the specified element.
        /// </summary>
        /// <param name="scoreMeasure"></param>
        /// <param name="memento"></param>
        public static void ApplyMemento(this IScoreMeasureEditor scoreMeasure, ScoreMeasureMemento memento)
        {
            scoreMeasure.ApplyLayout(memento.Layout);
            scoreMeasure.EditStaffSystemOrigin().ApplyMemento(memento.StaffSystem);

            foreach (var measureMemento in memento.Measures)
            {
                var measure = scoreMeasure.EditMeasure(measureMemento.RibbonIndex);
                measure.ApplyMemento(measureMemento);
            }
        }


        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="ribbonMeasure"></param>
        /// <returns></returns>
        public static InstrumentMeasureMemento GetMemento(this IInstrumentMeasureReader ribbonMeasure)
        {
            return new InstrumentMeasureMemento
            {
                Layout = ribbonMeasure.ReadLayout().Copy(),
                MeasureIndex = ribbonMeasure.MeasureIndex,
                RibbonIndex = ribbonMeasure.RibbonIndex,
                VoiceGroups = ribbonMeasure
                    .EnumerateVoices()
                    .Select(v => ribbonMeasure.ReadBlockChainAt(v).GetMemento())
                    .ToList()
            };
        }
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="ribbonMeasure"></param>
        /// <returns></returns>
        public static InstrumentMeasureMemento GetMemento(this IInstrumentMeasureEditor ribbonMeasure)
        {
            return new InstrumentMeasureMemento
            {
                Layout = ribbonMeasure.ReadLayout().Copy(),
                MeasureIndex = ribbonMeasure.MeasureIndex,
                RibbonIndex = ribbonMeasure.RibbonIndex,
                VoiceGroups = ribbonMeasure
                    .EnumerateVoices()
                    .Select(v => ribbonMeasure.EditBlockChainAt(v).GetMemento())
                    .ToList()
            };
        }
        /// <summary>
        /// Apply the memento to the specified element.
        /// </summary>
        /// <param name="ribbonMeasure"></param>
        /// <param name="memento"></param>
        public static void ApplyMemento(this IInstrumentMeasureEditor ribbonMeasure, InstrumentMeasureMemento memento)
        {
            ribbonMeasure.Clear();
            ribbonMeasure.ApplyLayout(memento.Layout);
            foreach (var voiceGroup in memento.VoiceGroups)
            {
                var blockChain = ribbonMeasure.EditBlockChainAt(voiceGroup.Voice);
                blockChain.ApplyMemento(voiceGroup);
            }
        }


        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="blockChain"></param>
        /// <returns></returns>
        public static RibbonMeasureVoiceMemento GetMemento(this IMeasureBlockChainEditor blockChain)
        {
            return new RibbonMeasureVoiceMemento
            {
                Voice = blockChain.Voice,
                MeasureBlocks = blockChain.EditBlocks().Select(b => b.GetMemento()).ToList()
            };
        }
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="blockChain"></param>
        /// <returns></returns>
        public static RibbonMeasureVoiceMemento GetMemento(this IMeasureBlockChainReader blockChain)
        {
            return new RibbonMeasureVoiceMemento
            {
                Voice = blockChain.Voice,
                MeasureBlocks = blockChain.ReadBlocks().Select(b => b.GetMemento()).ToList()
            };
        }
        /// <summary>
        /// Apply the memento to the specified element.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="memento"></param>
        public static void ApplyMemento(this IMeasureBlockChainEditor editor, RibbonMeasureVoiceMemento memento)
        {
            editor.Clear();
            foreach (var block in memento.MeasureBlocks)
            {
                var duration = block.Duration;
                editor.Append(duration, block.Grace);

                var chord = editor.EditBlocks().Last();
                chord.ApplyMemento(block);
            }
        }



        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="chordGroup"></param>
        /// <returns></returns>
        public static MeasureBlockMemento GetMemento(this IMeasureBlockReader chordGroup)
        {
            return new MeasureBlockMemento
            {
                Chords = chordGroup.ReadChords().Select(c => c.GetMemento()).ToList(),
                Layout = chordGroup.ReadLayout(),
                Duration = chordGroup.RythmicDuration,
                Grace = chordGroup.Grace
            };
        }
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="chordGroup"></param>
        /// <returns></returns>
        public static MeasureBlockMemento GetMemento(this IMeasureBlockEditor chordGroup)
        {
            return new MeasureBlockMemento
            {
                Chords = chordGroup.EditChords().Select(c => c.GetMemento()).ToList(),
                Layout = chordGroup.ReadLayout(),
                Duration = chordGroup.RythmicDuration,
                Grace = chordGroup.Grace
            };
        }
        /// <summary>
        /// Apply the memento to the specified element.
        /// </summary>
        /// <param name="chordGroup"></param>
        /// <param name="memento"></param>
        public static void ApplyMemento(this IMeasureBlockEditor chordGroup, MeasureBlockMemento memento)
        {
            chordGroup.Clear();
            chordGroup.ApplyLayout(memento.Layout);
            foreach (var chordMemento in memento.Chords)
            {
                chordGroup.AppendChord(chordMemento.RythmicDuration);
                var chord = chordGroup.EditChords().Last();
                chord.ApplyMemento(chordMemento);
            }
        }


        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="chord"></param>
        /// <returns></returns>
        public static ChordMemento GetMemento(this IChordReader chord)
        {
            return new ChordMemento
            {
                Notes = chord.ReadNotes().Select(n => n.GetMemento()).ToList(),
                Layout = chord.ReadLayout(),
                RythmicDuration = chord.RythmicDuration
            };
        }
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="chord"></param>
        /// <returns></returns>
        public static ChordMemento GetMemento(this IChordEditor chord)
        {
            return new ChordMemento
            {
                Notes = chord.EditNotes().Select(n => n.GetMemento()).ToList(),
                Layout = chord.ReadLayout(),
                RythmicDuration = chord.RythmicDuration
            };
        }
        /// <summary>
        /// Apply the memento to the specified element.
        /// </summary>
        /// <param name="chord"></param>
        /// <param name="chordMemento"></param>
        public static void ApplyMemento(this IChordEditor chord, ChordMemento chordMemento)
        {
            chord.Clear();
            chord.ApplyLayout(chordMemento.Layout);
            foreach (var noteMemento in chordMemento.Notes)
            {
                chord.Add(noteMemento.Pitch);
                var note = chord.EditNotes().Last();
                note.ApplyMemento(noteMemento);
            }
        }


        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static NoteMemento GetMemento(this INoteReader note)
        {
            return new NoteMemento
            {
                Pitch = note.Pitch,
                Layout = note.ReadLayout()
            };
        }
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static NoteMemento GetMemento(this INoteEditor note)
        {
            return new NoteMemento
            {
                Pitch = note.Pitch,
                Layout = note.ReadLayout()
            };
        }
        /// <summary>
        /// Apply the memento to the specified element.
        /// </summary>
        /// <param name="note"></param>
        /// <param name="noteMemento"></param>
        public static void ApplyMemento(this INoteEditor note, NoteMemento noteMemento)
        {
            note.ApplyLayout(noteMemento.Layout);
            note.Pitch = noteMemento.Pitch;
        }


        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="staffSystem"></param>
        /// <returns></returns>
        public static StaffSystemMemento GetMemento(this IStaffSystemReader staffSystem)
        {
            return new StaffSystemMemento()
            {
                Layout = staffSystem.ReadLayout().Copy(),
                StaffGroups = staffSystem.ReadStaffGroups().Select(g => g.GetMemento()).ToArray()
            };
        }
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="staffSystem"></param>
        /// <returns></returns>
        public static StaffSystemMemento GetMemento(this IStaffSystemEditor staffSystem)
        {
            return new StaffSystemMemento()
            {
                Layout = staffSystem.ReadLayout().Copy(),
                StaffGroups = staffSystem.EditStaffGroups().Select(g => g.GetMemento()).ToArray()
            };
        }
        /// <summary>
        /// Apply the memento to the specified element.
        /// </summary>
        /// <param name="staffSystem"></param>
        /// <param name="memento"></param>
        public static void ApplyMemento(this IStaffSystemEditor staffSystem, StaffSystemMemento memento)
        {
            staffSystem.ApplyLayout(memento.Layout);
            foreach (var staffGroupMemento in memento.StaffGroups)
            {
                var staffGroup = staffSystem.EditStaffGroup(staffGroupMemento.IndexInScore);
                staffGroup.ApplyMemento(staffGroupMemento);
            }
        }


        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <returns></returns>
        public static StaffGroupMemento GetMemento(this IStaffGroupReader staffGroup)
        {
            return new StaffGroupMemento()
            {
                IndexInScore = staffGroup.ReadContext().IndexInScore,
                Layout = staffGroup.ReadLayout().Copy(),
                Staves = staffGroup.ReadStaves().Select(e => e.GetMemento()).ToArray()
            };
        }
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <returns></returns>
        public static StaffGroupMemento GetMemento(this IStaffGroupEditor staffGroup)
        {
            return new StaffGroupMemento()
            {
                IndexInScore = staffGroup.IndexInScore,
                Layout = staffGroup.ReadLayout().Copy(),
                Staves = staffGroup.EditStaves().Select(e => e.GetMemento()).ToArray()
            };
        }
        /// <summary>
        /// Apply the memento to the specified element.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="memento"></param>
        public static void ApplyMemento(this IStaffGroupEditor staffGroup, StaffGroupMemento memento)
        {
            staffGroup.ApplyLayout(memento.Layout);
            foreach (var staffMemento in memento.Staves)
            {
                var staff = staffGroup.EditStaff(staffMemento.IndexInStaffGroup);
                staff.ApplyMemento(staffMemento);
            }
        }


        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="staffReader"></param>
        /// <returns></returns>
        public static StaffMemento GetMemento(this IStaffReader staffReader)
        {
            return new StaffMemento()
            {
                Layout = staffReader.ReadLayout().Copy(),
                IndexInStaffGroup = staffReader.IndexInStaffGroup
            };
        }
        /// <summary>
        /// Get the memento for the specified element.
        /// </summary>
        /// <param name="staffReader"></param>
        /// <returns></returns>
        public static StaffMemento GetMemento(this IStaffEditor staffReader)
        {
            return new StaffMemento()
            {
                Layout = staffReader.ReadLayout().Copy(),
                IndexInStaffGroup = staffReader.IndexInStaffGroup
            };
        }
        /// <summary>
        /// Apply the memento to the specified element.
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="memento"></param>
        public static void ApplyMemento(this IStaffEditor staff, StaffMemento memento)
        {
            staff.ApplyLayout(memento.Layout);
        }
    }
}
