namespace ECommerce_API.Models
{
    public class Order
    {
        public long Id { get; set; }
        public string OrderRef { get; set; } = string.Empty; //test
        public DateTime OrderDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
