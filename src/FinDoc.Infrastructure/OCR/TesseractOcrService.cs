using FinDoc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Tesseract;

namespace FinDoc.Infrastructure.OCR;

public class TesseractOcrService : ITesseractOcrService
{
    public async Task<string> ExtractTextAsync(IFormFile file)
    {
        var tempPath = Path.GetTempFileName();

        await using (var stream = File.Create(tempPath))
        {
            await file.CopyToAsync(stream);
        }

        using var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
        using var img = Pix.LoadFromFile(tempPath);
        using var page = engine.Process(img);

        return page.GetText();
    }
}
