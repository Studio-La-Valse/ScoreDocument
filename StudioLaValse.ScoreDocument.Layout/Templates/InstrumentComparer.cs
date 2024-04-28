using StudioLaValse.ScoreDocument.Core;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class InstrumentComparer : IEqualityComparer<Instrument>
    {
        public bool Equals(Instrument? x, Instrument? y)
        {
            if (x is null) return false;
            if (y is null) return false;

            if (ReferenceEquals(x, y)) return true;

            return x.Name.Equals(y.Name);
        }

        public int GetHashCode([DisallowNull] Instrument obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
