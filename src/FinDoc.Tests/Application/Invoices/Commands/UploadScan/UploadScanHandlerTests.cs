using FinDoc.Application.Invoices.Commands.UploadScan;
using FinDoc.Application.Interfaces;
using FinDoc.Application.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace FinDoc.Tests.Application.Invoices.Commands.UploadScan;

public class UploadScanHandlerTests
{
    [Fact]
    public async Task Should_ReturnExtractedText_When_FileIsValid()
    {
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.Length).Returns(100); // does simulate an empty file

        var mockOcr = new Mock<ITesseractOcrService>();
        mockOcr.Setup(s => s.ExtractTextAsync(It.IsAny<IFormFile>()))
               .ReturnsAsync("Mocked OCR Text");

        var handler = new UploadScanHandler(mockOcr.Object);
        var command = new UploadScanCommand { File = fileMock.Object };

        var result = await handler.Handle(command);

        result.IsSuccess.Should().BeTrue();
        result.Value.Data.Should().NotBeNull();
        result.Value.StatusCode.Should().Be(200);
        result.Value.Data!.ToString().Should().Contain("Mocked OCR Text");
    }

    [Fact]
    public async Task Should_ReturnFail_When_FileIsEmpty()
    {
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.Length).Returns(0);

        var mockOcr = new Mock<ITesseractOcrService>();
        var handler = new UploadScanHandler(mockOcr.Object);

        var command = new UploadScanCommand { File = fileMock.Object };

        var result = await handler.Handle(command);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Message.Contains("empty or null"));
    }
}
