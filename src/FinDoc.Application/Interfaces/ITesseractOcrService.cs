
namespace FinDoc.Application.Interfaces;

public interface ITesseractOcrService
{
    Task<string>ExtractText(IFormFile file);
}