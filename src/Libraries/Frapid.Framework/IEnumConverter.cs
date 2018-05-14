namespace Frapid.Framework
{
    public interface IEnumConverter
    {
        T ToEnum<T>(string value, T defaultValue) where T : struct;
    }
}