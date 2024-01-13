namespace StudioLaValse.ScoreDocument.Core
{

    public class Instrument
    {
        private readonly Clef[] defaultClefs;

        public static Instrument Default => new("Default", 1, [Clef.Treble]);
        public static Instrument Violin => new("Violin", 1, [Clef.Treble]);
        public static Instrument Piano => new("Piano", 2, [Clef.Treble, Clef.Bass]);
        public static Instrument Organ => new("Organ", 3, [Clef.Treble, Clef.Bass, Clef.Bass]);



        public string Name { get; }
        public int NumberOfStaves { get; }


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
        public static Instrument TryGetFromName(string name)
        {
            return name switch
            {
                "Violin" => Violin,
                "Piano" => Piano,
                "Organ" => Organ,
                _ => Default,
            };
        }
    }
}
