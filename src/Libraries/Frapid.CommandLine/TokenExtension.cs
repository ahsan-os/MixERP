namespace frapid
{
    public static class TokenExtension
    {
        public static string GetTokenOn(this string token, int index)
        {
            var tokens = token.Split(' ');
            if (tokens.Length > index)
            {
                return tokens[index];
            }

            return string.Empty;
        }

        public static int CountTokens(this string token)
        {
            var tokens = token.Split(' ');
            return tokens.Length;
        }
    }
}