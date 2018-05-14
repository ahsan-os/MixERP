namespace Frapid.Framework
{
    public interface ICastable
    {
        T To<T>(string input);
        T To<T>(object input);
        T To<T>(string input, T or);
        T To<T>(object input, T or);
    }
}