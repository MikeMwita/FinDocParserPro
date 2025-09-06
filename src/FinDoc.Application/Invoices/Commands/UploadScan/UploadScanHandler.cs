
using FluentResults;
using FinDoc.Application.Interfaces;
using FinDoc.Application.Common;

namespace FinDoc.Application.Invoices.Commands.UploadScan;

public class UploadScanHandler
{
    private readonly ITesseractOcrService _ocr;

    public UploadScanHandler(ITesseractOcrService ocr)
    {
        _ocr = ocr;
    }

    public async Task<Result<HttpDataResponse>> Handle(UploadScanCommand request)
    {
        if (request.File == null || request.File.Length == 0)
            return Result.Fail("Uploaded file is empty or null");

        var extractedText = await _ocr.ExtractTextAsync(request.File);

        var sanitizedText = Sanitizer.Sanitize(extractedText);

        Console.WriteLine("[OCR Raw]: " + extractedText);
        Console.WriteLine("[Sanitized]: " + sanitizedText);

        var response = new HttpDataResponse
        {
            Success = true,
            Data = sanitizedText
        };

        return Result.Ok(response);
    }
