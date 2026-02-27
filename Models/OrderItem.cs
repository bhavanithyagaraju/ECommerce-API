namespace ECommerce_API.Models
{
    public class OrderItem
    {
      public long Id { get; set; }
      public long OrderId { get; set; }
      public long ProductId { get; set; }
      public long CreatedBy { get; set; }
      public DateTime CreatedDate { get; set; }
      public long? UpdatedBy { get; set; }
      public DateTime? UpdatedDate { get; set; }
    }
}
