namespace Frapid.Framework
{
    public sealed class OrString : IOrString
    {
        public string Get(string s, string or)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return or;
            }

            return s;
        }
    }
}