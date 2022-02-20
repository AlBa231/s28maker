using S28Maker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace S28Maker.Tests
{
    public class S28DocumentParserTests
    {
        [Fact]
        public void TestParsePublicationNames()
        {
            var text = "";

            var publicationNames = S28PdfTextExtractor.ExtractNames(text);
        }
    }
}
