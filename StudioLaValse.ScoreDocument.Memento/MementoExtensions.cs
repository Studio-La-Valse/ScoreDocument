namespace StudioLaValse.ScoreDocument.Memento
{
    public static class MementoExtensions
    {
        public static ScoreDocumentMemento GetMemento(this IScoreDocumentReader scoreDocument)
        {
            return new ScoreDocumentMemento
            {
                Layout = scoreDocument.ReadLayout().Copy(),
                InstrumentRibbons = scoreDocument.ReadInstrumentRibbons().Select(e => e.GetMemento()).ToList(),
                ScoreMeasures = scoreDocument.ReadScoreMeasures().Select(e => e.GetMemento()).ToList()
            };
        }
        public static ScoreDocumentMemento GetMemento(this IScoreDocumentEditor scoreDocument)
        {
            return new ScoreDocumentMemento
            {
                Layout = scoreDocument.ReadLayout().Copy(),
                InstrumentRibbons = scoreDocument.EditInstrumentRibbons().Select(e => e.GetMemento()).ToList(),
                ScoreMeasures = scoreDocument.EditScoreMeasures().Select(e => e.GetMemento()).ToList()
            };
        }
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


        public static InstrumentRibbonMemento GetMemento(this IInstrumentRibbonReader instrumentRibbon)
        {
            return new InstrumentRibbonMemento
            {
                Layout = instrumentRibbon.ReadLayout().Copy(),
                Measures = instrumentRibbon.ReadMeasures().Select(e => e.GetMemento()).ToList(),
                Instrument = instrumentRibbon.Instrument
            };
        }
        public static InstrumentRibbonMemento GetMemento(this IInstrumentRibbonEditor instrumentRibbon)
        {
            return new InstrumentRibbonMemento
            {
                Layout = instrumentRibbon.ReadLayout().Copy(),
                Measures = instrumentRibbon.EditMeasures().Select(e => e.GetMemento()).ToList(),
                Instrument = instrumentRibbon.Instrument
            };
        }
        public static void ApplyMemento(this IInstrumentRibbonEditor instrumentRibbon, InstrumentRibbonMemento memento)
        {
            instrumentRibbon.ApplyLayout(memento.Layout);

            foreach (var measureMemento in memento.Measures)
            {
                var measure = instrumentRibbon.EditMeasure(measureMemento.MeasureIndex);
                measure.ApplyMemento(measureMemento);
            }
        }



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



        public static InstrumentMeasureMemento GetMemento(this IRibbonMeasureReader ribbonMeasure)
        {
            return new InstrumentMeasureMemento
            {
                Layout = ribbonMeasure.ReadLayout().Copy(),
                MeasureIndex = ribbonMeasure.MeasureIndex,
                RibbonIndex = ribbonMeasure.RibbonIndex,
                VoiceGroups = ribbonMeasure
                    .EnumerateVoices()
                    .Select(v =>
                    {
                        return new RibbonMeasureVoiceMemento
                        {
                            Voice = v,
                            ChordGroups = ribbonMeasure.ReadBlocks(v).Select(b => b.GetMemento()).ToList()
                        };
                    })
                    .ToList()
            };
        }
        public static InstrumentMeasureMemento GetMemento(this IRibbonMeasureEditor ribbonMeasure)
        {
            return new InstrumentMeasureMemento
            {
                Layout = ribbonMeasure.ReadLayout().Copy(),
                MeasureIndex = ribbonMeasure.MeasureIndex,
                RibbonIndex = ribbonMeasure.RibbonIndex,
                VoiceGroups = ribbonMeasure
                    .EnumerateVoices()
                    .Select(v =>
                    {
                        return new RibbonMeasureVoiceMemento
                        {
                            Voice = v,
                            ChordGroups = ribbonMeasure.EditBlocks(v).Select(b => b.GetMemento()).ToList()
                        };
                    })
                    .ToList()
            };
        }
        public static void ApplyMemento(this IRibbonMeasureEditor ribbonMeasure, InstrumentMeasureMemento memento)
        {
            ribbonMeasure.Clear();
            ribbonMeasure.ApplyLayout(memento.Layout);
            foreach (var voiceGroup in memento.VoiceGroups)
            {
                ribbonMeasure.ApplyMemento(voiceGroup);
            }
        }



        public static RibbonMeasureVoiceMemento GetMemento(this IRibbonMeasureReader ribbonMeasure, int voice)
        {
            return new RibbonMeasureVoiceMemento
            {
                Voice = voice,
                ChordGroups = ribbonMeasure.ReadBlocks(voice).Select(e => e.GetMemento()).ToList()
            };
        }
        public static RibbonMeasureVoiceMemento GetMemento(this IRibbonMeasureEditor ribbonMeasure, int voice)
        {
            return new RibbonMeasureVoiceMemento
            {
                Voice = voice,
                ChordGroups = ribbonMeasure.EditBlocks(voice).Select(e => e.GetMemento()).ToList()
            };
        }
        public static void ApplyMemento(this IRibbonMeasureEditor ribbonMeasure, RibbonMeasureVoiceMemento memento)
        {
            ribbonMeasure.ClearVoice(memento.Voice);
            ribbonMeasure.AddVoice(memento.Voice);
            foreach (var chordGroupMemento in memento.ChordGroups)
            {
                ribbonMeasure.AppendBlock(memento.Voice, chordGroupMemento.Duration, chordGroupMemento.Grace);
                var measureBlock = ribbonMeasure.EditBlocks(memento.Voice).Last();
                measureBlock.ApplyMemento(chordGroupMemento);
                measureBlock.Rebeam();
            }
        }



        public static ChordGroupMemento GetMemento(this IMeasureBlockReader chordGroup)
        {
            return new ChordGroupMemento
            {
                Chords = chordGroup.ReadChords().Select(c => c.GetMemento()).ToList(),
                Layout = chordGroup.ReadLayout(),
                Duration = chordGroup.Duration,
                Grace = chordGroup.Grace
            };
        }
        public static ChordGroupMemento GetMemento(this IMeasureBlockEditor chordGroup)
        {
            return new ChordGroupMemento
            {
                Chords = chordGroup.EditChords().Select(c => c.GetMemento()).ToList(),
                Layout = chordGroup.ReadLayout(),
                Duration = chordGroup.Duration,
                Grace = chordGroup.Grace
            };
        }
        public static void ApplyMemento(this IMeasureBlockEditor chordGroup, ChordGroupMemento memento)
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



        public static ChordMemento GetMemento(this IChordReader chord)
        {
            return new ChordMemento
            {
                Notes = chord.ReadNotes().Select(n => n.GetMemento()).ToList(),
                Layout = chord.ReadLayout(),
                RythmicDuration = chord.RythmicDuration
            };
        }
        public static ChordMemento GetMemento(this IChordEditor chord)
        {
            return new ChordMemento
            {
                Notes = chord.EditNotes().Select(n => n.GetMemento()).ToList(),
                Layout = chord.ReadLayout(),
                RythmicDuration = chord.RythmicDuration
            };
        }
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



        public static NoteMemento GetMemento(this INoteReader note)
        {
            return new NoteMemento
            {
                Pitch = note.Pitch,
                Layout = note.ReadLayout()
            };
        }
        public static NoteMemento GetMemento(this INoteEditor note)
        {
            return new NoteMemento
            {
                Pitch = note.Pitch,
                Layout = note.ReadLayout()
            };
        }
        public static void ApplyMemento(this INoteEditor note, NoteMemento noteMemento)
        {
            note.ApplyLayout(noteMemento.Layout);
            note.Pitch = noteMemento.Pitch;
        }



        public static StaffSystemMemento GetMemento(this IStaffSystemReader staffSystem)
        {
            return new StaffSystemMemento()
            {
                Layout = staffSystem.ReadLayout().Copy(),
                StaffGroups = staffSystem.ReadStaffGroups().Select(g => g.GetMemento()).ToArray()
            };
        }
        public static StaffSystemMemento GetMemento(this IStaffSystemEditor staffSystem)
        {
            return new StaffSystemMemento()
            {
                Layout = staffSystem.ReadLayout().Copy(),
                StaffGroups = staffSystem.EditStaffGroups().Select(g => g.GetMemento()).ToArray()
            };
        }
        public static void ApplyMemento(this IStaffSystemEditor staffSystem, StaffSystemMemento memento)
        {
            staffSystem.ApplyLayout(memento.Layout);
            foreach (var staffGroupMemento in memento.StaffGroups)
            {
                var staffGroup = staffSystem.EditStaffGroup(staffGroupMemento.IndexInScore);
                staffGroup.ApplyMemento(staffGroupMemento);
            }
        }



        public static StaffGroupMemento GetMemento(this IStaffGroupReader staffGroup)
        {
            return new StaffGroupMemento()
            {
                IndexInScore = staffGroup.ReadContext().IndexInScore,
                Layout = staffGroup.ReadLayout().Copy(),
                Staves = staffGroup.ReadStaves().Select(e => e.GetMemento()).ToArray()
            };
        }
        public static StaffGroupMemento GetMemento(this IStaffGroupEditor staffGroup)
        {
            return new StaffGroupMemento()
            {
                IndexInScore = staffGroup.IndexInScore,
                Layout = staffGroup.ReadLayout().Copy(),
                Staves = staffGroup.EditStaves().Select(e => e.GetMemento()).ToArray()
            };
        }
        public static void ApplyMemento(this IStaffGroupEditor staffGroup, StaffGroupMemento memento)
        {
            staffGroup.ApplyLayout(memento.Layout);
            foreach (var staffMemento in memento.Staves)
            {
                var staff = staffGroup.EditStaff(staffMemento.IndexInStaffGroup);
                staff.ApplyMemento(staffMemento);
            }
        }



        public static StaffMemento GetMemento(this IStaffReader staffReader)
        {
            return new StaffMemento()
            {
                Layout = staffReader.ReadLayout().Copy(),
                IndexInStaffGroup = staffReader.IndexInStaffGroup
            };
        }
        public static StaffMemento GetMemento(this IStaffEditor staffReader)
        {
            return new StaffMemento()
            {
                Layout = staffReader.ReadLayout().Copy(),
                IndexInStaffGroup = staffReader.IndexInStaffGroup
            };
        }
        public static void ApplyMemento(this IStaffEditor staff, StaffMemento memento)
        {
            staff.ApplyLayout(memento.Layout);
        }
    }
}
