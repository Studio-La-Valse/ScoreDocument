using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Core
{
    public class PowerOfTwo
    {
        public uint Power { get; }

        public int Value
        {
            get
            {
                var value = 1;

                for (int i = 0; i < Power; i++)
                {
                    value *= 2;
                }

                return value;
            }
        }


        public PowerOfTwo(uint power)
        {
            if (power < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(power));
            }

            Power = power;
        }

        public static bool TryCreate(int value, [NotNullWhen(true)] out PowerOfTwo? powerOfTwo)
        {
            powerOfTwo = null;

            var _val = 1;

            uint power = 0;

            while (_val <= value)
            {
                //2 pow 0 = 1;
                //2 pow 1 = 2;
                //2 pow 2 = 4;
                //2 pow 3 = 8;

                if (_val == value)
                {
                    powerOfTwo = new PowerOfTwo(power);
                    return true;
                }

                _val *= 2;
                power++;
            }

            return false;
        }
        public bool TryDivideByTwo([NotNullWhen(true)] out PowerOfTwo? half)
        {
            half = null;

            if (Power <= 0)
            {
                return false;
            }

            half = new PowerOfTwo(Power - 1);

            return true;
        }

        public PowerOfTwo Double()
        {
            return new PowerOfTwo(Power + 1);
        }

        public static implicit operator int(PowerOfTwo p) => p.Value;
        public static implicit operator PowerOfTwo(int i) => FromInt(i) ?? throw new InvalidCastException();

        public static PowerOfTwo? FromInt(int i)
        {
            return TryCreate(i, out var result) ? result : default;
        }

        public override string ToString()
        {
            return $"2 ^ {Power} = {Value}";
        }
    }
}
