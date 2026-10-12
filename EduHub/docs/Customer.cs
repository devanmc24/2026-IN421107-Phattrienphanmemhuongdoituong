using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.docs;

public sealed class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    private Customer(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public static Customer Create(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên khách hàng không được rỗng.", nameof(name));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ.", nameof(email));

        return new Customer(Guid.NewGuid(), name.Trim(), email.Trim());
    }
}
