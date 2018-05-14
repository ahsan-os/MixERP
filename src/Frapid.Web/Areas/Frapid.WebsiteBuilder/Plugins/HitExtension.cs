namespace Frapid.WebsiteBuilder.Plugins
{
    public static class HitHelper
    {
        private const string Template = @"<script>
                                $(document).ready(function($)
                                        {
                                            (function() { $.post(document.location + ""/hit""); })();
                                        });
                                </script>";

        public static string Add(string html)
        {
            return html + Template;
        }
    }
}