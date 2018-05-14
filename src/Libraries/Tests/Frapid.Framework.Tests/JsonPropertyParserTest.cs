using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Frapid.Framework.Tests
{
    public sealed class JsonPropertyParserTest
    {
        private const string Json = @"{
                        ""foo"": 100,
                        ""bar"": ""baz"",
                        ""date"":""2001/1/1""
                    }";

        public JsonPropertyParserTest()
        {
            this.JObject = JsonConvert.DeserializeObject<JObject>(Json, JsonHelper.GetJsonSerializerSettings());
        }

        private JObject JObject { get; }

        [Theory]
        [InlineData("foo", (long)100)]
        [InlineData("bar", "baz")]
        public void ShouldParseProperty(string property, object value)
        {
            var parser = new JsonPropertyParser();

            var result = parser.TryGetPropertyValue<object>(this.JObject, property);
            var expected = value;

            Assert.Equal(expected, result);
        }
    }
}