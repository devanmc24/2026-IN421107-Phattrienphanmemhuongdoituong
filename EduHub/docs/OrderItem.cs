using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.docs;

public sealed class OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }     // khoá ngoại -> Order
    public Guid ProductId { get; private set; }   // khoá ngoại -> DigitalProduct
    public string ProductName { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public decimal Subtotal => UnitPrice * Quantity;

    private OrderItem(Guid id, Guid orderId, DigitalProduct product, int quantity)
    {
        Id = id;
        OrderId = orderId;
        ProductId = product.Id;
        ProductName = product.Name;
        UnitPrice = product.Price;
        Quantity = quantity;
    }

    public static OrderItem Create(Guid orderId, DigitalProduct product, int quantity)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (orderId == Guid.Empty)
            throw new ArgumentException("OrderId không hợp lệ.", nameof(orderId));
        if (quantity < 1)
            throw new ArgumentException("Số lượng phải ≥ 1.", nameof(quantity));

        return new OrderItem(Guid.NewGuid(), orderId, product, quantity);
    }
}