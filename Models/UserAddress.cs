namespace ECommerce_API.Models
{
    public class UserAddress
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public string Village { get; set; } = string.Empty;
        public int TalukId { get; set; }
        public string Pincode { get; set; } = string.Empty;
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
