using FinDoc.Domain.Entities;
using Xunit;

namespace FinDoc.Tests.Domain.Entities;

public class InvoiceTests
{
    [Fact]
    public void Should_Initialize_With_Id_And_EmptyRawText()
    {
        var invoice = new Invoice();

        Assert.NotEqual(Guid.Empty, invoice.Id);
        Assert.Equal(string.Empty, invoice.RawText);
        Assert.Null(invoice.InvoiceNumber);
        Assert.Null(invoice.InvoiceDate);
        Assert.Null(invoice.TotalAmount);
        Assert.Null(invoice.SupplierName);
    }
}
