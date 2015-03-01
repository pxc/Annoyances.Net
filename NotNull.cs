using System;
using System.Diagnostics;

namespace Annoyances.Net
{
    /// <summary>
    /// Helper class to specify parameters that shouldn't be null
    /// </summary>
    /// <typeparam name="T">The type of the parameter</typeparam>
    /// <remarks>
    /// This doesn't guarantee that the parameter is non-null.
    /// For example, a user can do this:
    /// <c>NotNull<string> p = null;</string></c>
    /// It just makes it less likely that nulls are passed in by accident
    /// since the easiest way to get one of these objects is to call <see cref="Create"/>.
    /// </remarks>
    public sealed class NotNull<T> where T : class
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
