using FinDoc.Infrastructure.OCR;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace FinDoc.Tests.Infrastructure.OCR;

public class TesseractOcrServiceTests
{
    [Fact]
    public async Task Should_ExtractText_From_ValidImage()
    {
        var service = new TesseractOcrService();
        var fileMock = new Mock<IFormFile>();

        // TODO : a placeholder for later use

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.ExtractTextAsync(null!)
        );
    }
}
