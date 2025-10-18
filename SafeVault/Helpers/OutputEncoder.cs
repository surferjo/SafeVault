using System.Text.Encodings.Web;

namespace SafeVault.Helpers
{
    public static class OutputEncoder
    {
        // Use the built-in HTML encoder to encode any value before embedding in HTML
        public static string HtmlEncode(string input) => HtmlEncoder.Default.Encode(input ?? string.Empty);
    }
}
