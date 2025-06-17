namespace FinDoc.Domain.Entities;

public class Invoice
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string?  InvoiceNumber {get; set}
    public DateTime? InvoiceDate {get;set}
    public decimal?  TotalAmount {get;set}
    public string? SupplierName {get;set}
    public string? RawText {get;set} =  string.Empty;
}