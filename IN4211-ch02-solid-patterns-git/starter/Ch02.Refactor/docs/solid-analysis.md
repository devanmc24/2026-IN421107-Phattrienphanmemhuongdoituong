# Phân tích vi phạm SOLID trong `ClubManager`

| Vị trí | Nguyên tắc | Vi phạm và hậu quả |
|---|---|---|
| `CreateClub` — kiểm tra tên, lưu CLB, ghi log và gửi thông báo | SRP | Một method có nhiều lý do để thay đổi; thay quy tắc tên hoặc phương thức thông báo buộc sửa chung một lớp. |
| `AddMemberToClub` — quản lý thành viên, kiểm tra giới hạn, ghi log và gửi thông báo | SRP | Các trách nhiệm nghiệp vụ và hạ tầng bị trộn, khó kiểm thử và thay đổi độc lập. |
| `CreateClub` và `AddMemberToClub` — điều kiện Gmail/Yahoo, gửi Slack cố định | OCP | Thêm kênh thông báo mới cần sửa các method hiện tại, làm tăng nguy cơ regression. |
| `CreateClub`, `AddMemberToClub`, `CreateEvent`, `PrintClubReport` — gọi `File.AppendAllText`/`Console.WriteLine` trực tiếp | DIP | Logic cấp cao phụ thuộc API hạ tầng cụ thể; khó thay logger và khó cô lập kiểm thử. |
| Trạng thái `_clubs`, `_members` lưu trực tiếp trong `ClubManager` | DIP | Nghiệp vụ phụ thuộc cấu trúc lưu trữ cụ thể, khó thay bằng repository/database. |
| `PrintClubReport` định dạng/in dữ liệu ngay trong quản lý nghiệp vụ | SRP, OCP | Thêm định dạng báo cáo buộc thay đổi lớp quản lý và có thể ảnh hưởng các chức năng không liên quan. |
| `CreateEvent` tự lặp thành viên và gửi email | SRP, OCP | Lớp tạo sự kiện biết chi tiết người nhận/cách thông báo; thêm listener mới đòi sửa lớp này. |
| `ClubManager` kết hợp thao tác CLB, thành viên, sự kiện, báo cáo, thông báo và log | ISP, SRP | Client phải phụ thuộc vào một lớp lớn chứa nhiều chức năng không cần dùng; thay đổi ở một chức năng gây ảnh hưởng rộng. |

## Cách refactor

- Tách nghiệp vụ theo `ClubService`, `EventService` và `ReportService`; đưa truy cập dữ liệu qua các repository interface nhỏ.
- Dùng `IAppLogger` và `INotificationService` để đảo chiều phụ thuộc hạ tầng.
- Dùng `NotificationServiceFactory`, `LoggingClubRepository`, `IReportStrategy` và `IEventObserver` cho Factory, Decorator, Strategy và Observer.
- Đưa quy tắc tên CLB, sức chứa, đăng ký và check-in về các entity domain.
