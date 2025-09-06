using FinDoc.Application.Invoices.Commands.UploadScan;
using FinDoc.Application.Interfaces;
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
        fileMock.Setup(f => f.Length).Returns(100); // simulating a  non-empty file

        var ocrMock = new Mock<ITesseractOcrService>();
        ocrMock.Setup(s => s.ExtractTextAsync(It.IsAny<IFormFile>()))
               .ReturnsAsync("Mocked OCR Text");

        var handler = new UploadScanHandler(ocrMock.Object);
        var command = new UploadScanCommand { File = fileMock.Object };

        // Act
        var result = await handler.Handle(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Data.Should().NotBeNull();
        result.Value.Data!.ToString().Should().Contain("Mocked OCR Text");
        result.Value.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Should_ReturnFail_When_FileIsEmpty()
    {
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.Length).Returns(0);

        var ocrMock = new Mock<ITesseractOcrService>();
        var handler = new UploadScanHandler(ocrMock.Object);

        var command = new UploadScanCommand { File = fileMock.Object };

        var result = await handler.Handle(command);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Message.Contains("empty or null"));
    }

    [Fact]
    public async Task Should_ReturnFail_When_FileIsNull()
    {
        var ocrMock = new Mock<ITesseractOcrService>();
        var handler = new UploadScanHandler(ocrMock.Object);

        var command = new UploadScanCommand { File = null };

        var result = await handler.Handle(command);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().Contain(e => e.Message.Contains("empty or null"));
    }
}
