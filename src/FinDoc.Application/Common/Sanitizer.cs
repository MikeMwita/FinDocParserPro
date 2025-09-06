using System.Text.RegularExpressions;

namespace FinDoc.Application.Common;

public static class Sanitizer
{
    public static string Sanitize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        input = Regex.Replace(input, @"\b(?:\d[ -]*?){13,16}\b", "**** **** **** ****");

        input = Regex.Replace(input, @"\b\d{3}-\d{2}-\d{4}\b", "***-**-****");

        input = Regex.Replace(input, @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b", "[REDACTED EMAIL]");

        return input;
    }
}
