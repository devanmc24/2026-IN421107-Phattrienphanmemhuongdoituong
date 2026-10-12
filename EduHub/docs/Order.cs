using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.docs;

public enum OrderStatus { Pending, Paid, Cancelled }

public sealed class Order
{
    private readonly List<OrderItem> _items = new();

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }  // khoá ngoại -> Customer
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<OrderItem> Items => _items;
    public decimal Total => _items.Sum(i => i.Subtotal);

    private Order(Guid id, Guid customerId)
    {
        Id = id;
        CustomerId = customerId;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public static Order Create(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        return new Order(Guid.NewGuid(), customer.Id);
    }

    public void AddItem(DigitalProduct product, int quantity)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Chỉ thêm sản phẩm khi đơn đang chờ thanh toán.");
        _items.Add(OrderItem.Create(Id, product, quantity));
    }

    public void Pay()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Đơn không ở trạng thái chờ thanh toán.");
        if (_items.Count == 0)
            throw new InvalidOperationException("Đơn hàng chưa có sản phẩm.");
        Status = OrderStatus.Paid;
    }

    public void Cancel()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Chỉ huỷ được đơn đang chờ thanh toán.");
        Status = OrderStatus.Cancelled;
    }
}