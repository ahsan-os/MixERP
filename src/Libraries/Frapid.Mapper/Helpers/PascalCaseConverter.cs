namespace Frapid.Mapper.Helpers
{
    public sealed class PascalCaseConverter
    {
        public TitleCaseConverter TitleCaseConverter { get; set; }
        public PascalCaseConverter(TitleCaseConverter titleCaseConverter)
        {
            this.TitleCaseConverter = titleCaseConverter;
        }

        public string Convert(string name)
        {
            name = name.Replace("_", " ");
            name = this.TitleCaseConverter.Convert(name);

            return name.Replace(" ", "");
        }
    }
}