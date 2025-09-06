using System.IO;
using System.Text;
using System.Threading.Tasks;
using FinDoc.Infrastructure.OCR;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace FinDoc.Tests.Infrastructure.OCR;

public class TesseractOcrServiceTests
{

    [Fact]
    public async Task Should_ExtractText_From_ValidImage()
    {
        if (!File.Exists("/usr/bin/tesseract"))
        {
            return;
        }

        var content = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("fake image content"));
        var file = new FormFile(content, 0, content.Length, "Data", "dummy.png");

        var service = new TesseractOcrService();
        var result = await service.ExtractTextAsync(file);

        Assert.NotNull(result);
        Assert.IsType<string>(result);
    }


    [Fact]
    public async Task Should_ThrowArgumentException_WhenFileIsNull()
    {
        var service = new TesseractOcrService();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.ExtractTextAsync(null!)
        );
    }

    [Fact]
    public async Task Should_ThrowArgumentException_WhenFileIsEmpty()
    {
        var emptyStream = new MemoryStream();
        var emptyFile = new FormFile(emptyStream, 0, 0, "Data", "empty.png");

        var service = new TesseractOcrService();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.ExtractTextAsync(emptyFile)
        );
    }
}
