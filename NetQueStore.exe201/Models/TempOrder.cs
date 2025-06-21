namespace NetQueStore.exe201.Models;

public class TempOrder
{
    public string OrderNumber { get; set; }
    public long BankId { get; set; }
    public int? UserId { get; set; }
    public string SessionId { get; set; }
    public string GuestEmail { get; set; }
    public string RecipientName { get; set; }
    public string RecipientPhone { get; set; }
    public string RecipientEmail { get; set; }
    public string DeliveryAddress { get; set; }
    public string DeliveryNotes { get; set; }
    public int PaymentMethodId { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderStatus { get; set; }
    public string PaymentStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public Dictionary<int, int> FoodQuantities { get; set; }
    public string TrackingNumber { get; set; }
}