using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical instrument.
    /// </summary>
    public class Instrument
    {
        private readonly Clef[] defaultClefs;

        /// <summary>
        /// A violin.
        /// </summary>
        public static Instrument Violin => new("Violin", [Clef.Treble]);
        /// <summary>
        /// A piano.
        /// </summary>
        public static Instrument Piano => new("Piano", [Clef.Treble, Clef.Bass]);
        /// <summary>
        /// An organ.
        /// </summary>
        public static Instrument Organ => new("Organ", [Clef.Treble, Clef.Bass, Clef.Bass]);


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




        private Instrument(string name, Clef[] defaultClefs)
        {
            if (defaultClefs.Length == 0)
            {
                throw new Exception("An instrument needs at least 1 staff.");
            }

            this.defaultClefs = defaultClefs;

            Name = name;
            NumberOfStaves = defaultClefs.Length;
        }


        /// <summary>
        /// Tries to get the instrument with the specified name. Returns true if the instrument is a known instrument.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instrument"></param>
        /// <returns></returns>
        public static bool TryGetFromName(string name, [NotNullWhen(true)] out Instrument? instrument)
        {
            instrument = name.ToLowerInvariant() switch
            {
                "violin" => Violin,
                "piano" => Piano,
                "organ" => Organ,
                _ => null
            };

            return instrument is not null;
        }

        /// <summary>
        /// Create a custom instrument from a collection of clefs. Each clef will be the default clef for one staff of the instrument.
        /// Therefore, the instrument will have a number of staves that is equal to the amount of supplied clefs. At least one clef is required.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="clefs"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Instrument CreateCustom(string name, params Clef[] clefs)
        {
            return clefs.Length == 0
                ? throw new InvalidOperationException("Please provide at least one clef.")
                : new Instrument(name, clefs);
        }

        ///<inheritdoc/>
        public override string ToString()
        {
            return Name;
        }
    }
}
