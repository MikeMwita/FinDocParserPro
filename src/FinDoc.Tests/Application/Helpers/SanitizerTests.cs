using FinDoc.Application.Common;
using Xunit;

public class SanitizerTests
{
    [Fact]
    public void Should_Mask_CreditCardNumbers()
    {
        var input = "My card number is 1234 5678 9012 3456";
        var result = Sanitizer.Sanitize(input);
        Assert.DoesNotContain("1234", result);
        Assert.Contains("**** **** **** ****", result);
    }

    [Fact]
    public void Should_Mask_SSN()
    {
        var input = "SSN is 123-45-6789";
        var result = Sanitizer.Sanitize(input);
        Assert.Equal("***-**-****", result.Trim().Split(' ')[2]);
    }

    [Fact]
    public void Should_Mask_Email()
    {
        var input = "Contact me at evans@example.com";
        var result = Sanitizer.Sanitize(input);
        Assert.Contains("[REDACTED EMAIL]", result);
    }
}
