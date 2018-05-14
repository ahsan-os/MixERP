using System.ComponentModel;

namespace Frapid.Framework
{
    public sealed class Castable : ICastable
    {
        public T To<T>(string input)
        {
            var d = default(T);

            if (string.IsNullOrWhiteSpace(input))
            {
                return d;
            }

            var converter = TypeDescriptor.GetConverter(typeof(T));

            try
            {
                return (T) converter.ConvertFromString(input);
            }
            catch
            {
                //swallow
            }

            return d;
        }

        public T To<T>(string input, T or)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return or;
            }

            return this.To<T>(input);
        }

        public T To<T>(object input)
        {
            var d = default(T);

            if (input == null)
            {
                return d;
            }

            return this.To<T>(input.ToString());
        }

        public T To<T>(object input, T or)
        {
            if (input == null)
            {
                return or;
            }

            return this.To<T>(input);
        }
    }
}