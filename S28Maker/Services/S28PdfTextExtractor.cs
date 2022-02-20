using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace S28Maker.Services
{
    public static class S28PdfTextExtractor
    {
        private static readonly Regex BookNameRegex = new(@"\d{4}\s(.+)");

        public static List<PublicationName> ExtractNames(string pdfText)
        {
            var lines = BookNameRegex.Matches(pdfText);

            return lines.OfType<Match>().Select(CreatePublicationName).ToList();
        }

        private static PublicationName CreatePublicationName(Match match)
        {
            return new PublicationName(match.Groups[1].Value, match.Value);
        }
    }
}
