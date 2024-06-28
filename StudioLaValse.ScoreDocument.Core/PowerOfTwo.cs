using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Reprents a non-zero, non-negative integer that is a power of two.
    /// </summary>
    public class PowerOfTwo : IEquatable<PowerOfTwo>
    {
        /// <summary>
        /// Raise two to this power. This value may never be negative.
        /// </summary>
        public int Power { get; }

        /// <summary>
        /// The actual value when two is raised to the <see cref="Power"/>.
        /// </summary>
        public int Value
        {
            get
            {
                var value = 1;

                for (var i = 0; i < Power; i++)
                {
                    value *= 2;
                }

                return value;
            }
        }

        /// <summary>
        /// Construct a power of two by specifying what number to raise two to.
        /// The power may never be negative.
        /// </summary>
        /// <param name="power"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public PowerOfTwo(int power)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(power);

            Power = power;
        }

        /// <summary>
        /// Try create a power of two from an integer.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="powerOfTwo"></param>
        /// <returns></returns>
        public static bool TryCreate(int value, [NotNullWhen(true)] out PowerOfTwo? powerOfTwo)
        {
            powerOfTwo = null;

            if (value <= 0)
            {
                return false;
            }

            var _val = 1;

            var power = 0;

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
        /// <summary>
        /// Try to divide the power of two by two.
        /// </summary>
        /// <param name="half"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Double the power of two.
        /// </summary>
        /// <returns></returns>
        public PowerOfTwo Double()
        {
            return new PowerOfTwo(Power + 1);
        }

        /// <summary>
        /// Cast the power of two to an integer value.
        /// </summary>
        /// <param name="p"></param>
        public static implicit operator int(PowerOfTwo p)
        {
            return p.Value;
        }

        /// <summary>
        /// Cast the integer value to a power of two. Throws <see cref="InvalidCastException"/> if the number cannot be casted.
        /// </summary>
        /// <param name="i"></param>
        public static implicit operator PowerOfTwo(int i)
        {
            return TryCreate(i, out var result) ? result : throw new InvalidCastException();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"2 ^ {Power} = {Value}";
        }

        /// <inheritdoc/>
        public bool Equals(PowerOfTwo? other)
        {
            return other != null && other.Value == Value;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as PowerOfTwo);
        }
    }

    /// <summary>
    /// The default equality comparer for a power of two.
    /// </summary>
    public class PowerOfTwoEqualityComparer : IEqualityComparer<PowerOfTwo>
    {
        /// <inhertdoc/>
        public bool Equals(PowerOfTwo? x, PowerOfTwo? y)
        {
            return x is not null && x.Equals(y);
        }

        /// <inhertdoc/>
        public int GetHashCode([DisallowNull] PowerOfTwo obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
