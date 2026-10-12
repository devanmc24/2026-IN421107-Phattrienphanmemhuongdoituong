using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.docs;

public sealed class DigitalProduct
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string DownloadUrl { get; private set; }

    private DigitalProduct(Guid id, string name, decimal price, string downloadUrl)
    {
        Id = id;
        Name = name;
        Price = price;
        DownloadUrl = downloadUrl;
    }

    public static DigitalProduct Create(string name, decimal price, string downloadUrl)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên sản phẩm không được rỗng.", nameof(name));
        if (price <= 0)
            throw new ArgumentException("Giá phải lớn hơn 0.", nameof(price));
        if (string.IsNullOrWhiteSpace(downloadUrl))
            throw new ArgumentException("Link tải không được rỗng.", nameof(downloadUrl));

        return new DigitalProduct(Guid.NewGuid(), name.Trim(), price, downloadUrl.Trim());
    }
}
