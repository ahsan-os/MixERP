namespace Google
{
    public sealed class Config
    {
        /// <summary>
        /// Customize maps with your own content and imagery. Ref: https://developers.google.com/maps/documentation/javascript/?hl=en_US
        /// </summary>
        public string MapsJavascriptApiKey { get; set; }

        /// <summary>
        /// Add up-to-date information about millions of locations to your service. Ref: https://developers.google.com/places/web-service/?hl=en_US
        /// </summary>
        public string PlacesApiWebServiceKey { get; set; }
    }
}