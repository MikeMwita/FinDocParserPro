#  FinDocParserPro

A modular, scalable C# toolkit for reading and intelligently extracting data from financial documents such as invoices, credit notes, and receipts using OCR and pattern-based field parsing.

Built with modern Clean Architecture, CQRS pattern, and a testable, extensible design — this tool empowers businesses, auditors, and developers to streamline document data workflows.

---

##  Purpose

**FinDocParserPro** solves the problem of manual data entry from financial documents by providing:
- Automated text extraction using OCR (Tesseract)
- Smart pattern recognition for key fields (amount, dates, invoice numbers)
- Clean API endpoints for managing document input/output
- Structured exports (CSV/JSON) or integration hooks for external systems

---

##  Architecture Overview

This solution follows **[Clean Architecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)** and enforces:
- Separation of concerns between API, Application, Domain, and Infrastructure
- CQRS-based command/query pattern without over-complicating the flow
- Explicit and consistent error/result handling (FluentResults)

## Key Patterns and Decisions

### CQRS (without [MediatR](https://github.com/jbogard/MediatR))
Command and Query handlers are kept separate.
Handlers are directly injected into Minimal API endpoints , improving performance and clarity without the need for `IMediator`.

```csharp
app.MapPost("/invoices", async (
    CreateInvoiceCommand command,
    CreateInvoiceHandler handler
) => await handler.Handle(command));
```

### FluentResults for Return Types
All command/query handlers return a Result<T> (from FluentResults) instead of throwing exceptions.

### [Result Pattern Object](https://www.forevolve.com/en/articles/2018/03/19/operation-result/)
We use a shared wrapper (HttpDataResponse) for consistent and testable HTTP response shapes:

```csharp
public class HttpDataResponse
{
    public object? Data { get; set; }
    public List<string>? Errors { get; set; }
    public int StatusCode { get; set; }

    public static HttpDataResponse Success(object data, int code = 200) =>
        new() { Data = data, StatusCode = code };

    public static HttpDataResponse Fail(List<string> errors, int code = 400) =>
        new() { Errors = errors, StatusCode = code };
}
```

### MediatR
We intentionally avoid MediatR initially for simplicity. After  the project scales up, MediatR can be plugged in with minimal changes for advanced command pipeline behaviors.



### Features
- [ ] Upload PDF/PNG/JPEG scans of financial documents
- [ ] Extract text using Tesseract OCR
- [ ] Regex-based field extraction (e.g., invoice number, date, total)
- [ ] Export as CSV/JSON
- [ ] Minimal API endpoints with Swagger
- [ ] Fully testable command/query layers