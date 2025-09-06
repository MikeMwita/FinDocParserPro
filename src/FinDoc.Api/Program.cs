
using FinDoc.Application.Invoices.Commands.UploadScan;
using FinDoc.Application.Interfaces;
using FinDoc.Infrastructure.OCR;
using FinDoc.Api.Endpoints.Invoices;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<UploadScanHandler>();
builder.Services.AddScoped<ITesseractOcrService, TesseractOcrService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapUploadScan();
app.Run();
