# 1 - User Stories

## US1 – Khách hàng xem danh mục sản phẩm số 
Là khách hàng, tôi muốn xem danh sách sản phẩm số để chọn mua.
1. Hiển thị tên, giá, mô tả của từng sản phẩm.
2. Sản phẩm đã ngừng bán không xuất hiện trong danh sách.
## US2 – Khách hàng đặt đơn và thanh toán
Là khách hàng, tôi muốn thêm sản phẩm vào đơn và thanh toán.
1. Số lượng mỗi sản phẩm phải ≥ 1, tổng tiền được tính đúng.
2. Đơn chỉ chuyển sang “Đã thanh toán” khi có ít nhất một sản phẩm.
## US3 – Khách hàng nhận sản phẩm sau khi thanh toán
Là khách hàng, tôi muốn nhận link tải hoặc license key ngay sau khi trả tiền.
1. Mỗi dòng đơn hàng đã thanh toán có một license key.
2. Đơn chưa thanh toán thì không được cấp key.
## US4 – Quản trị viên quản lý sản phẩm
Là quản trị viên, tôi muốn thêm và sửa sản phẩm số.
1. Giá phải > 0, tên và link tải không được rỗng.
2. Sản phẩm mới xuất hiện trong danh mục ngay sau khi lưu.
## US5 – Nhân viên hỗ trợ xử lý yêu cầu của khách
Là nhân viên hỗ trợ, tôi muốn tra cứu đơn hàng và huỷ đơn chưa thanh toán.
1. Tra cứu được đơn theo mã đơn hoặc email khách hàng.
2. Chỉ huỷ được đơn đang ở trạng thái “Chờ thanh toán”.

