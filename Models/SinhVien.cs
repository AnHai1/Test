using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

//(1đ) Tạo Model class SinhVien gồm các thuộc tính: mã sinh viên (số nguyên), họ tên(chuỗi) , ngày sinh(datetime), giới tính(bool), điện thoại(chuỗi), điểm(số nguyên), tên lớp(chuỗi), Email (chuỗi).
//(1đ) Yêu cầu tạo chú thích cho các thuộc tính như sau:
//-Mã sinh viên, Họ tên, ngày sinh, điểm, tên lớp bắt buộc phải nhập
//- Họ tên: yêu cầu độ dài tối thiểu là 2, tối đa là 30 kí tự
//- Ngày sinh yêu cầu chú thích Kiểu dữ liệu Date
//- Điện thoại yêu cầu phải nhập đúng định dạng số điện thoại
//- Điểm phải từ 0 đến 10
//- Email yêu cầu phải nhập đúng định dạng Email
//(1đ) Tạo Controller SinhVienController, trên controller này khởi tạo 1 danh sách sinh viên dùng chung chứa tối thiểu 3 sinh viên với đầy đủ dữ liệu, tạo các action và view cho action để thực hiện các công việc sau:
//(1đ) action index: Hiển thị danh sách sinh viên trong danh sách sinh viên chung lên view index, Sửa view Index sao cho giới tính hiển thị là Nam hoặc Nữ
//(1.5đ) Hiển thị điểm cao nhất lên view index
//(1.5 đ) action edit: để sửa thông tin 1 sinh viên mà người dùng nhập từ view edit
//(1đ) Sửa view edit sao cho tên lớp nhập trên 1 danh sách đổ xuống gồm 3 lớp: Tin15a1, Tin15a2, Tin15a3 (giống điều khiển combobox) bằng cách dùng hàm DropDownListFor()
//(1đ) Tạo 1 action và view: Hiển thị danh sách sinh viên nữ lên 1 view
//(1đ) Trên view index: thêm 1 điều khiển danh sách đổ xuống (giống combobox) để nhập thông tin tìm kiếm:
//+theo tên lớp và kèm theo 1 nút tìm kiếm, kích vào nút submit cho phép hiển thị danh sách các sinh viên có tên lớp chọn trên combobox và hiển thị điểm cao nhất của lớp được chọn
//(1đ) Trên view index: thêm textbox để nhập thông tin tìm kiếm theo họ tên và thêm điều khiển radio giới tính nam và radio giới tính nữ. Sau đó tạo 1 nút submit, khi kích nút này thực hiện đồng thời 2 yêu cầu:  
//+tìm kiếm gần đúng theo họ tên nhập vào
//+ hiển thị điểm thấp nhất của sinh viên có giới tính đã chọn trên radio

namespace OnTapKT_Lan1.Models
{
    public class SinhVien
    {
        [Required(ErrorMessage = "Mã sinh viên bắt buộc phải nhập")]
        public int MaSinhVien { get; set; }

        [Required(ErrorMessage = "Họ tên bắt buộc phải nhập")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Họ tên phải có độ dài từ 2 đến 30 kí tự")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Ngày sinh bắt buộc phải nhập")]
        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không đúng định dạng")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Điểm bắt buộc phải nhập")]
        [Range(0, 10, ErrorMessage = "Điểm phải nằm trong khoảng từ 0 đến 10")]
        public int Diem { get; set; }

        [Required(ErrorMessage = "Tên lớp bắt buộc phải nhập")]
        public string TenLop { get; set; }

        [Required(ErrorMessage = "Giới tính bắt buộc phải nhập")]
        public bool GioiTinh { get; set; }

        [Required(ErrorMessage = "Điện thoại bắt buộc phải nhập")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string DienThoai { get; set; }

        [Required(ErrorMessage = "Email bắt buộc phải nhập")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
    }
}