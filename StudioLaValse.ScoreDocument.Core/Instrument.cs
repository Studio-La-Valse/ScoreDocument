namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical instrument.
    /// </summary>
    public class Instrument
    {
        private readonly Clef[] defaultClefs;

        /// <summary>
        /// The default instrument.
        /// </summary>
        public static Instrument Default => new("Default", 1, [Clef.Treble]);
        /// <summary>
        /// A violin.
        /// </summary>
        public static Instrument Violin => new("Violin", 1, [Clef.Treble]);
        /// <summary>
        /// A piano.
        /// </summary>
        public static Instrument Piano => new("Piano", 2, [Clef.Treble, Clef.Bass]);
        /// <summary>
        /// An organ.
        /// </summary>
        public static Instrument Organ => new("Organ", 3, [Clef.Treble, Clef.Bass, Clef.Bass]);


        /// <summary>
        /// The name of the instrument.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The default number of staves of this instrument.
        /// </summary>
        public int NumberOfStaves { get; }

        /// <summary>
        /// The default clefs of this instrument. The first clef is associated with the topmost staff of the default staff system.
        /// </summary>
        public IEnumerable<Clef> DefaultClefs =>
            defaultClefs;




        private Instrument(string name, int staves, Clef[] defaultClefs)
        {
            if (defaultClefs.Length != staves)
                throw new Exception("An equal amount of staves and default clefs is expected.");

            if (staves == 0)
                throw new Exception("An instrument needs at least 1 staff.");

            this.defaultClefs = defaultClefs;

            Name = name;
            NumberOfStaves = staves;
        }


        /// <summary>
        /// Tries to get the instrument with the specified name. If it does not exist, the default instrument is returned.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Instrument TryGetFromName(string name)
        {
            return name.ToLowerInvariant() switch
            {
                "violin" => Violin,
                "piano" => Piano,
                "organ" => Organ,
                _ => Default
            };
        }
    }
}
