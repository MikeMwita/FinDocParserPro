
using FinDoc.Application.Invoices.Commands.UploadScan;

namespace FinDoc.Api.Endpoints.Invoices;

public static class UploadInvoiceScanEndpoint
{
    public static void MapUploadScan(this IEndpointRouteBuilder app)
    {
        app.MapPost("/invoices/upload", async (
            IFormFile file,
            UploadScanHandler handler
        ) =>
        {
            var result = await handler.Handle(new UploadScanCommand { File = file });
            return Results.Json(result.Value, statusCode: result.Value.StatusCode);
        });
    }
}
