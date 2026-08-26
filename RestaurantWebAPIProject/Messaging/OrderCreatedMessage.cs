namespace RestaurantWebAPIProject.Messaging
{
    public class OrderCreatedMessage
    {
        public int OrderId { get; set; }
        public int TableNumber { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
