using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.docs;

public sealed class License
{
    public Guid Id { get; private set; }
    public Guid OrderItemId { get; private set; } // khoá ngoại -> OrderItem
    public string Key { get; private set; }
    public DateTime ExpiredAt { get; private set; }

    private License(Guid id, Guid orderItemId, string key, DateTime expiredAt)
    {
        Id = id;
        OrderItemId = orderItemId;
        Key = key;
        ExpiredAt = expiredAt;
    }

    public static License Create(OrderItem item, int validDays)
    {
        ArgumentNullException.ThrowIfNull(item);
        if (validDays <= 0)
            throw new ArgumentException("Thời hạn phải > 0 ngày.", nameof(validDays));

        var key = Guid.NewGuid().ToString("N")[..16].ToUpperInvariant();
        return new License(Guid.NewGuid(), item.Id, key, DateTime.UtcNow.AddDays(validDays));
    }

    public bool IsValid() => DateTime.UtcNow <= ExpiredAt;
}
