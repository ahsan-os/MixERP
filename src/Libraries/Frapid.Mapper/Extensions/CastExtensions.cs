using Frapid.Mapper.Helpers;

namespace Frapid.Mapper.Extensions
{
    public static class CastExtensions
    {
        public static T To<T>(this string input)
        {
            var castable = new Castable();
            return castable.To<T>(input);
        }

        public static T To<T>(this string input, T or)
        {
            var castable = new Castable();
            return castable.To(input, or);
        }

        public static T To<T>(this object input)
        {
            if (typeof(T) == typeof(object))
            {
                return (T)input;
            }

            var castable = new Castable();
            return castable.To<T>(input);
        }

        public static T To<T>(this object input, T or)
        {
            var castable = new Castable();
            return castable.To(input, or);
        }
    }
}