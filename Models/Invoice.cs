namespace ECommerce_API.Models
{
    public class Invoice
    {
      public int Id { get; set; }
      public long OrderId { get; set; }
      public DateTime InvoiceDate { get; set; }
      public long CreatedBy { get; set; }
      public DateTime CreatedDate { get; set; }
      public long? UpdatedBy { get; set; }
      public DateTime? UpdatedDate { get; set; }
    }
}
