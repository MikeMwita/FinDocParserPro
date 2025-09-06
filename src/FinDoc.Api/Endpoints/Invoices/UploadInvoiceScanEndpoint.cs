using FinDoc.Application.Invoices.Commands.UploadScan;

namespace FinDoc.Api.Endpoints.Invoices;

public static class UploadInvoiceScanEndpoint
{
    public static void MapUploadScan(this WebApplication app)
    {
        app.MapPost("/invoices/upload", async (
            IFormFile file,
            UploadScanHandler handler
        ) =>
        {
            var result = await handler.Handle(new UploadScanCommand { File = file });
            return Results.Json(result.Value, statusCode: result.Value.StatusCode);
        })
        .WithName("UploadInvoiceScan")
        .WithTags("Invoices")
        .Accepts<IFormFile>("multipart/form-data")
        .Produces<FinDoc.Application.Common.HttpDataResponse>(StatusCodes.Status200OK)
        .Produces<FinDoc.Application.Common.HttpDataResponse>(StatusCodes.Status400BadRequest)
        .WithOpenApi(op =>
        {
            op.Summary = "Upload a scanned invoice image or PDF";
            op.Description = "Accepts a scanned financial document and returns extracted text using OCR.";
            return op;
        });
    }
}
