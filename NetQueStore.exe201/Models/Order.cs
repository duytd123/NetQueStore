using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = null!;

    public int? UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Tax { get; set; }

    public decimal ShippingFee { get; set; }

    public decimal Discount { get; set; }

    public int? CouponId { get; set; }

    public decimal GiftWrappingFee { get; set; }

    public decimal TotalAmount { get; set; }

    public string RecipientName { get; set; } = null!;

    public string RecipientPhone { get; set; } = null!;

    public string? RecipientEmail { get; set; }

    public int ProvinceId { get; set; }

    public int DistrictId { get; set; }

    public int WardId { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public string? DeliveryNotes { get; set; }

    public int PaymentMethodId { get; set; }

    public int? BankId { get; set; }

    public string? TransactionId { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string OrderStatus { get; set; } = null!;

    public string? ShippingProvider { get; set; }

    public string? ShippingMethod { get; set; }

    public string? TrackingNumber { get; set; }

    public DateTime? EstimatedDelivery { get; set; }

    public DateTime? ActualDelivery { get; set; }

    public string? Notes { get; set; }

    public string? AdminNotes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Bank? Bank { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual Province Province { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual Ward Ward { get; set; } = null!;
}
