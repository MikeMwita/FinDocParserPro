
namespace FinDoc.Application.Interfaces;

using Microsoft.AspNetCore.Http;

namespace FinDoc.Application.Interfaces;

public interface ITesseractOcrService
{
    Task<string> ExtractTextAsync(IFormFile file);
}
