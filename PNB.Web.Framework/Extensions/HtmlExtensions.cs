using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using PNB.Service.sLocalization;
using PNB.Web.Framework.Events;
using PNB.Web.Framework.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;

namespace PNB.Web.Framework.Extensions
{
    public static class HtmlExtensions
    {
         
        
        public static IHtmlContent LocalizedEditor<T, TLocalizedModelLocal>(this IHtmlHelper<T> helper,
             string name,
             Func<int, HelperResult> localizedTemplate,
             Func<T, HelperResult> standardTemplate,
           
             bool ignoreIfSeveralStores = false, string cssClass = "")
             where T : ILocalizedModel<TLocalizedModelLocal>
             where TLocalizedModelLocal : ILocalizedLocaleModel
        {
            var localizationSupported = helper.ViewData.Model.Locales.Count > 1;
       
            if (localizationSupported)
            {
                var tabStrip = new StringBuilder();
                var cssClassWithSpace = !String.IsNullOrEmpty(cssClass) ? " " + cssClass : null;
                tabStrip.AppendLine($"<div id=\"{name}\" class=\"nav-tabs-custom nav-tabs-localized-fields{cssClassWithSpace}\">");

                //render input contains selected tab name
                var tabNameToSelect = GetSelectedTabName(helper, name);
                var selectedTabInput = new TagBuilder("input");
                selectedTabInput.Attributes.Add("type", "hidden");
                selectedTabInput.Attributes.Add("id", $"selected-tab-name-{name}");
                selectedTabInput.Attributes.Add("name", $"selected-tab-name-{name}");
                selectedTabInput.Attributes.Add("value", tabNameToSelect);
                tabStrip.AppendLine(selectedTabInput.RenderHtmlContent());

                tabStrip.AppendLine("<ul class=\"nav nav-tabs\">");

                //default tab
                var standardTabName = $"{name}-standard-tab";
                var standardTabSelected = string.IsNullOrEmpty(tabNameToSelect) || standardTabName == tabNameToSelect;
                tabStrip.AppendLine(string.Format("<li{0}>", standardTabSelected ? " class=\"active\"" : null));
                tabStrip.AppendLine($"<a data-tab-name=\"{standardTabName}\" href=\"#{standardTabName}\" data-toggle=\"tab\">{"Tiêu chuẩn"}</a>");
                tabStrip.AppendLine("</li>");
                IServiceCollection services = new ServiceCollection();

                 services.AddTransient<IUrlHelperFactory, UrlHelperFactory>();
                services.AddTransient<ILanguageService, LanguageService>();

               var serviceProvider = services.BuildServiceProvider();
            var languageService = serviceProvider.GetService<ILanguageService>();
                var urlHelper = serviceProvider.GetService<IUrlHelperFactory>().GetUrlHelper(helper.ViewContext); ;



                foreach (var locale in helper.ViewData.Model.Locales)
                {
                    //languages
                    var language = languageService.GetLanguageById(locale.LanguageId);
                    if (language == null)
                        throw new Exception("Language cannot be loaded");

                    var localizedTabName = $"{name}-{language.Id}-tab";
                    tabStrip.AppendLine(string.Format("<li{0}>", localizedTabName == tabNameToSelect ? " class=\"active\"" : null));
                    var iconUrl = urlHelper.Content("~/images/flags/" + language.FlagImageFileName);
                    tabStrip.AppendLine($"<a data-tab-name=\"{localizedTabName}\" href=\"#{localizedTabName}\" data-toggle=\"tab\"><img alt='' src='{iconUrl}'>{WebUtility.HtmlEncode(language.Name)}</a>");

                    tabStrip.AppendLine("</li>");
                }
                tabStrip.AppendLine("</ul>");

                //default tab
                tabStrip.AppendLine("<div class=\"tab-content\">");
                tabStrip.AppendLine(string.Format("<div class=\"tab-pane{0}\" id=\"{1}\">", standardTabSelected ? " active" : null, standardTabName));
                tabStrip.AppendLine(standardTemplate(helper.ViewData.Model).ToHtmlString());
                tabStrip.AppendLine("</div>");

                for (var i = 0; i < helper.ViewData.Model.Locales.Count; i++)
                {
                    //languages
                    var language = languageService.GetLanguageById(helper.ViewData.Model.Locales[i].LanguageId);
                    if (language == null)
                        throw new Exception("Language cannot be loaded");

                    var localizedTabName = $"{name}-{language.Id}-tab";
                    tabStrip.AppendLine(string.Format("<div class=\"tab-pane{0}\" id=\"{1}\">", localizedTabName == tabNameToSelect ? " active" : null, localizedTabName));
                    tabStrip.AppendLine(localizedTemplate(i).ToHtmlString());
                    tabStrip.AppendLine("</div>");
                }
                tabStrip.AppendLine("</div>");
                tabStrip.AppendLine("</div>");

                //render tabs script
                var script = new TagBuilder("script");
                script.InnerHtml.AppendHtml("$(document).ready(function () {bindBootstrapTabSelectEvent('" + name + "', 'selected-tab-name-" + name + "');});");
                tabStrip.AppendLine(script.RenderHtmlContent());

                return new HtmlString(tabStrip.ToString());
            }
            else
            {
                return new HtmlString(standardTemplate(helper.ViewData.Model).RenderHtmlContent());
            }
        }

        public static string GetSelectedPanelName(this IHtmlHelper helper)
        {
            //keep this method synchronized with
            //"SaveSelectedPanelName" method of \Area\Admin\Controllers\BaseAdminController.cs
            var tabName = string.Empty;
            const string dataKey = "nop.selected-panel-name";

            if (helper.ViewData.ContainsKey(dataKey))
                tabName = helper.ViewData[dataKey].ToString();

            if (helper.ViewContext.TempData.ContainsKey(dataKey))
                tabName = helper.ViewContext.TempData[dataKey].ToString();

            return tabName;
        }


        public static string GetSelectedTabName(this IHtmlHelper helper, string dataKeyPrefix = null)
        {
            //keep this method synchronized with
            //"SaveSelectedTab" method of \Area\Admin\Controllers\BaseAdminController.cs
            var tabName = string.Empty;
            var dataKey = "nop.selected-tab-name";
            if (!string.IsNullOrEmpty(dataKeyPrefix))
                dataKey += $"-{dataKeyPrefix}";

            if (helper.ViewData.ContainsKey(dataKey))
                tabName = helper.ViewData[dataKey].ToString();

            if (helper.ViewContext.TempData.ContainsKey(dataKey))
                tabName = helper.ViewContext.TempData[dataKey].ToString();

            return tabName;
        }

        public static IHtmlContent TabContentByURL(this AdminTabStripCreated eventMessage, string tabId, string tabName, string url)
        {
            return new HtmlString($@"
                <script>
                    $(document).ready(function() {{
                        $('<li><a data-tab-name='{tabId}' data-toggle='tab' href='#{tabId}'>{tabName}</a></li>').appendTo('#{eventMessage.TabStripName} .nav-tabs:first');
                        $.get('{url}', function(result) {{
                            $(`<div class='tab-pane' id='{tabId}'>` + result + `</div>`).appendTo('#{eventMessage.TabStripName} .tab-content:first');
                        }});
                    }});
                </script>");
        }

        public static IHtmlContent TabContentByModel(this AdminTabStripCreated eventMessage, string tabId, string tabName, string contentModel)
        {
            return new HtmlString($@"
                <script>
                    $(document).ready(function() {{
                        $(`<li><a data-tab-name='{tabId}' data-toggle='tab' href='#{tabId}'>{tabName}</a></li>`).appendTo('#{eventMessage.TabStripName} .nav-tabs:first');
                        $(`<div class='tab-pane' id='{tabId}'>{contentModel}</div>`).appendTo('#{eventMessage.TabStripName} .tab-content:first');
                    }});
                </script>");
        }

        public static IHtmlContent Hint(this IHtmlHelper helper, string value)
        {
            //create tag builder
            var builder = new TagBuilder("div");
            builder.MergeAttribute("title", value);
            builder.MergeAttribute("class", "ico-help");
            builder.MergeAttribute("data-toggle", "tooltip");
            var icon = new StringBuilder();
            icon.Append("<i class='fa fa-question-circle'></i>");
            builder.InnerHtml.AppendHtml(icon.ToString());
            //render tag
            return new HtmlString(builder.ToHtmlString());
        }

        #region Common extensions

        /// <summary>
        /// Convert IHtmlContent to string
        /// </summary>
        /// <param name="htmlContent">HTML content</param>
        /// <returns>Result</returns>
        public static string RenderHtmlContent(this IHtmlContent htmlContent)
        {
            using (var writer = new StringWriter())
            {
                htmlContent.WriteTo(writer, HtmlEncoder.Default);
                var htmlOutput = writer.ToString();
                return htmlOutput;
            }
        }

        /// <summary>
        /// Convert IHtmlContent to string
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <returns>String</returns>
        public static string ToHtmlString(this IHtmlContent tag)
        {
            using (var writer = new StringWriter())
            {
                tag.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }

        #endregion
    
    }
}
