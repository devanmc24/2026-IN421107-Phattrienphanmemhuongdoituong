using EduHub.docs;

// 1. Tạo khách hàng và sản phẩm
var customer = Customer.Create("Nguyễn Văn A", "a@example.com");
var course = DigitalProduct.Create("Khoá học C# cơ bản", 299_000m, "https://shop.example/dl/csharp");
var ebook = DigitalProduct.Create("Ebook Clean Code", 99_000m, "https://shop.example/dl/cleancode");

// 2. Tạo đơn và thêm sản phẩm
var order = Order.Create(customer);
order.AddItem(course, 1);
order.AddItem(ebook, 2);
Console.WriteLine($"Đơn {order.Id} - trạng thái: {order.Status} - tổng: {order.Total:N0} đ");

// 3. Thanh toán
order.Pay();
Console.WriteLine($"Sau thanh toán: {order.Status}");

// 4. Phát hành license cho từng dòng đơn hàng
var licenses = order.Items.Select(i => License.Create(i, validDays: 365)).ToList();

// 5. Báo cáo
Console.WriteLine("\n===== BÁO CÁO ĐƠN HÀNG =====");
Console.WriteLine($"Khách hàng: {customer.Name} <{customer.Email}>");
foreach (var item in order.Items)
{
    var lic = licenses.First(l => l.OrderItemId == item.Id);
    Console.WriteLine($"- {item.ProductName} x{item.Quantity} = {item.Subtotal:N0} đ | Key: {lic.Key} | Hạn: {lic.ExpiredAt:dd/MM/yyyy}");
}
Console.WriteLine($"TỔNG CỘNG: {order.Total:N0} đ");

// 6. Minh hoạ kiểm tra dữ liệu
try { DigitalProduct.Create("Sai giá", -5m, "https://x"); }
catch (ArgumentException ex) { Console.WriteLine($"\nLỗi dữ liệu (đúng dự kiến): {ex.Message}"); }
try { order.AddItem(course, 1); }
catch (InvalidOperationException ex) { Console.WriteLine($"Lỗi nghiệp vụ (đúng dự kiến): {ex.Message}"); }