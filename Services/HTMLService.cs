using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Services
{
    using HtmlAgilityPack;
    using System.Text.RegularExpressions;
    using System.Xml;

    public static class HTMLService
    {
        public static string ToPlainText(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var text = HtmlEntity.DeEntitize(doc.DocumentNode.InnerText);

            // Clean spacing & line breaks
            text = Regex.Replace(text, @"\r\n|\r|\n", "\n");
            text = Regex.Replace(text, @"[ \t]+", " ");

            return text.Trim();
        }
    }

}
