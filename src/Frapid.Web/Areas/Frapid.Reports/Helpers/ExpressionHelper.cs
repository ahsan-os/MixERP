using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Frapid.ApplicationState.Cache;
using Frapid.ApplicationState.CacheFactory;
using Frapid.Framework.Extensions;
using Frapid.i18n;
using Frapid.Reports.Engine.Model;

namespace Frapid.Reports.Helpers
{
    public static class ExpressionHelper
    {
        public static string ParseDataSource(string expression, List<DataSource> dataSources)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

            if (dataSources == null)
            {
                return null;
            }

            foreach (var match in Regex.Matches(expression, "{.*?}"))
            {
                string word = match.ToString();

                if (word.StartsWith("{DataSource", StringComparison.OrdinalIgnoreCase))
                {
                    int index = word.Split('.').First().Replace("{DataSource[", "").Replace("]", "").To<int>();
                    string column = word.Split('.').Last().Replace("}", "");

                    var dataSource = dataSources.FirstOrDefault(x => x.Index.Equals(index));

                    if (dataSource?.Data?.Rows.Count > 0)
                    {
                        if (dataSource.Data.Columns.Contains(column))
                        {
                            string value = FormattingHelper.GetFormattedValue(dataSource.Data.Rows[0][column]);
                            expression = expression.Replace(word, value);
                        }
                    }
                    else
                    {
                        expression = expression.Replace(word, string.Empty);
                    }
                }
            }

            return expression;
        }

        private static string GetDictionaryValue(string tenant, string key)
        {
            string globalLoginId = HttpContext.Current.User.Identity.Name;

            if (!string.IsNullOrWhiteSpace(globalLoginId))
            {
                string cacheKey = tenant + "/dictionary/" + globalLoginId;

                var cache = new DefaultCacheFactory();
                var dictionary = cache.Get<Dictionary<string, object>>(cacheKey);

                if (dictionary != null && dictionary.ContainsKey(key))
                {
                    var value = dictionary?[key];

                    if (value != null)
                    {
                        return value.ToString();
                    }
                }
            }

            return string.Empty;
        }

        private static string GetLogo()
        {
            return AppUsers.GetCurrent().Logo.Or("/Static/images/logo-sm.png");
        }

        public static string GetCurrentDomainName()
        {
            if (HttpContext.Current == null)
            {
                return string.Empty;
            }

            string url = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host;

            if (HttpContext.Current.Request.Url.Port != 80)
            {
                url += ":" + HttpContext.Current.Request.Url.Port.ToString(CultureInfo.InvariantCulture);
            }

            return url;
        }


        public static string ParseExpression(string tenant, string expression, List<DataSource> dataSources, ParameterInfo info)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return string.Empty;
            }


            string logo = GetLogo();

            if (!string.IsNullOrWhiteSpace(logo))
            {
                //Or else logo will not be exported into excel.
                expression = expression.Replace("{LogoPath}", GetCurrentDomainName() + VirtualPathUtility.ToAbsolute(logo));
            }

            expression = expression.Replace("{PrintDate}", DateTime.Now.ToString(CultureManager.GetCurrentUiCulture()));

            foreach (var match in Regex.Matches(expression, "{.*?}"))
            {
                string word = match.ToString();

                if (word.StartsWith("{Meta.", StringComparison.OrdinalIgnoreCase))
                {
                    string sessionKey = RemoveBraces(word);
                    sessionKey = sessionKey.Replace("Meta.", "");
                    sessionKey = sessionKey.Trim();

                    string value = GetDictionaryValue(tenant, sessionKey);

                    expression = expression.Replace(word, value);
                }
                else if (word.StartsWith("{Query.", StringComparison.OrdinalIgnoreCase))
                {
                    string res = RemoveBraces(word);
                    var resource = res.Split('.');

                    string key = resource[1];

                    var parameter = info.Parameters.FirstOrDefault(x => x.Name.ToLower().Equals(key.ToLower()));

                    if (parameter != null)
                    {
                        string value = FormattingHelper.GetFormattedValue(parameter.Value);

                        var datasourceParameter = info.DataSourceParameters.FirstOrDefault(x => x.Name.Replace("@", "").ToLower().Equals(parameter.Name.ToLower()));

                        if (datasourceParameter != null)
                        {
                            string type = datasourceParameter.Type;
                            value = DataSourceParameterHelper.CastValue(value, type).ToString();
                        }

                        expression = expression.Replace(word, value);
                    }
                }
                else if (word.StartsWith("{i18n.", StringComparison.OrdinalIgnoreCase))
                {
                    string res = RemoveBraces(word);
                    var resource = res.Split('.');

                    string key = resource[1];

                    expression = expression.Replace(word, LocalizationHelper.Localize(key, false));
                }
                else if (word.StartsWith("{DataSource", StringComparison.OrdinalIgnoreCase) && word.ToLower(CultureInfo.InvariantCulture).Contains("runningtotalfieldvalue"))
                {
                    if (dataSources == null)
                    {
                        return null;
                    }

                    string res = RemoveBraces(word);
                    var resource = res.Split('.');

                    int dataSourceIndex = resource[0].ToLower(CultureInfo.InvariantCulture)
                            .Replace("datasource", "")
                            .Replace("[", "")
                            .Replace("]", "").To<int>();
                    int index = resource[1].ToLower(CultureInfo.InvariantCulture)
                            .Replace("runningtotalfieldvalue", "")
                            .Replace("[", "")
                            .Replace("]", "").To<int>();

                    if (dataSourceIndex >= 0 && index >= 0)
                    {
                        var dataSource = dataSources.FirstOrDefault(x => x.Index.Equals(dataSourceIndex));

                        if (dataSource?.Data != null)
                        {
                            expression = expression.Replace(word,
                                GetSum(dataSource.Data, index)
                                    .ToString(CultureInfo.InvariantCulture));
                        }
                    }
                }
                //else if (word.StartsWith("{Barcode", StringComparison.OrdinalIgnoreCase))
                //{
                //string res = RemoveBraces(word).Replace("Barcode(", "").Replace(")", "");
                //string barCodeValue = res;

                //    if (res.StartsWith("DataSource"))
                //    {
                //        barCodeValue = ParseDataSource("{" + res + "}", dataTableCollection);
                //    }

                //    string barCodeFormat = ConfigurationHelper.GetReportParameter("BarCodeFormat");
                //    string barCodeDisplayValue = ConfigurationHelper.GetReportParameter("BarCodeDisplayValue");
                //    string barCodeFontSize = ConfigurationHelper.GetReportParameter("BarCodeFontSize");
                //    string barCodeWidth = ConfigurationHelper.GetReportParameter("BarCodeWidth");
                //    string barCodeHeight = ConfigurationHelper.GetReportParameter("BarCodeHeight");
                //    string barCodeQuite = ConfigurationHelper.GetReportParameter("BarCodeQuite");
                //    string barCodeFont = ConfigurationHelper.GetReportParameter("BarCodeFont");
                //    string barCodeTextAlign = ConfigurationHelper.GetReportParameter("BarCodeTextAlign");
                //    string barCodeBackgroundColor = ConfigurationHelper.GetReportParameter("BarCodeBackgroundColor");
                //    string barCodeLineColor = ConfigurationHelper.GetReportParameter("BarCodeLineColor");

                //    string imageSource =
                //        "<img class='reportEngineBarCode' data-barcodevalue='{0}' alt='{0}' value='{0}' data-barcodeformat='{1}' data-barcodedisplayvalue='{2}' data-barcodefontsize='{3}' data-barcodewidth='{4}' data-barcodeheight='{5}' data-barcodefont='{6}' data-barcodetextalign='{7}' data-barcodebackgroundcolor='{8}' data-barcodelinecolor='{9}' data-barcodequite={10} />";
                //    imageSource = string.Format(CultureInfo.InvariantCulture, imageSource, barCodeValue,
                //        barCodeFormat, barCodeDisplayValue, barCodeFontSize, barCodeWidth, barCodeHeight,
                //        barCodeFont, barCodeTextAlign, barCodeBackgroundColor, barCodeLineColor, barCodeQuite);
                //    expression = expression.Replace(word, imageSource).ToString(CultureInfo.InvariantCulture);
                //}
                //else if (word.StartsWith("{QRCode", StringComparison.OrdinalIgnoreCase))
                //{
                //    string res = RemoveBraces(word).Replace("QRCode(", "").Replace(")", "");
                //    string qrCodeValue = res;

                //    if (res.StartsWith("DataSource"))
                //    {
                //        qrCodeValue = ParseDataSource("{" + res + "}", dataTableCollection);
                //    }

                //    string qrCodeRender = ConfigurationHelper.GetReportParameter("QRCodeRender");
                //    string qrCodeBackgroundColor = ConfigurationHelper.GetReportParameter("QRCodeBackgroundColor");
                //    string qrCodeForegroundColor = ConfigurationHelper.GetReportParameter("QRCodeForegroundColor");
                //    string qrCodeWidth = ConfigurationHelper.GetReportParameter("QRCodeWidth");
                //    string qrCodeHeight = ConfigurationHelper.GetReportParameter("QRCodeHeight");
                //    string qrCodeTypeNumber = ConfigurationHelper.GetReportParameter("QRCodeTypeNumber");

                //    string qrCodeDiv =
                //        "<div class='reportEngineQRCode' data-qrcodevalue={0} data-qrcoderender='{1}' data-qrcodebackgroundcolor='{2}' data-qrcodeforegroundcolor='{3}' data-qrcodewidth='{4}' data-qrcodeheight='{5}' data-qrcodetypenumber='{6}'></div>";
                //    qrCodeDiv = string.Format(CultureInfo.InvariantCulture, qrCodeDiv, qrCodeValue, qrCodeRender,
                //        qrCodeBackgroundColor, qrCodeForegroundColor, qrCodeWidth, qrCodeHeight, qrCodeTypeNumber);
                //    expression = expression.Replace(word, qrCodeDiv).ToString(CultureInfo.InvariantCulture);
                //
                //}
            }
            return expression;
        }

        public static string RemoveBraces(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return string.Empty;
            }

            return expression.Replace("{", "").Replace("}", "");
        }

        public static decimal GetSum(DataTable table, int index)
        {
            if (table != null && table.Rows.Count > 0)
            {
                string expression = "SUM(" + table.Columns[index].ColumnName + ")";
                return table.Compute(expression, "").To<decimal>();
            }

            return 0;
        }
    }
}