using UglyToad.PdfPig;
using System.Text;

namespace SmartResumeAnalyzer.Services
{
    public static class PdfReaderService
    {
        public static string ExtractText(string filePath)
        {
            var text = new StringBuilder();
            using var document = PdfDocument.Open(filePath);
            foreach (var page in document.GetPages())
            {
                text.AppendLine(page.Text);
            }
            return text.ToString();
        }
    }
}