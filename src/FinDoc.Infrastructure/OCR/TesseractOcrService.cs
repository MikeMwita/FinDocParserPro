using FinDoc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Tesseract;

namespace FinDoc.Infrastructure.OCR;

public class TesseractOcrService : ITesseractOcrService
{
    public async Task<string> ExtractTextAsync(IFormFile file)
    {
        if (file == null)
            throw new ArgumentException("File cannot be null", nameof(file));

        if (file.Length == 0)
            throw new ArgumentException("File is empty", nameof(file));

        var tempPath = Path.GetTempFileName();

        try
        {
            await using (var stream = File.Create(tempPath))
            {
                await file.CopyToAsync(stream);
            }

            using var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            using var img = Pix.LoadFromFile(tempPath);
            using var page = engine.Process(img);

            return page.GetText();
        }
        finally
        {
            if (File.Exists(tempPath))
                File.Delete(tempPath); // cleanup the  temp file
        }
    }
}
