using Microsoft.AspNetCore.Http;

namespace FinDoc.Application.Invoices.Commands.UploadScan;

public class UploadScanCommand
{
    public IFormFile File { get; set; } = null!;
}
