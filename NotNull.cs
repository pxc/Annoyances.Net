using System;
using System.Diagnostics;

namespace Annoyances.Net
{
    public class NotNull<T> where T : class
    {
        public T Value { get; private set; }

        private NotNull(T value)
        {
            Debug.Assert(value != null);
            Value = value;
        }

        public static NotNull<T> Create(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            return new NotNull<T>(value);
        }

        public static implicit operator T(NotNull<T> value)
        {
            return value.Value;
        }
    }
}
