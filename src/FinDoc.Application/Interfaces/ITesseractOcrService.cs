
namespace FinDoc.Application.Interfaces;

using Microsoft.AspNetCore.Http;

public interface ITesseractOcrService
{
    Task<string> ExtractTextAsync(IFormFile file);
}
