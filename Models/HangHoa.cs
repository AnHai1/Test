using System;
using System.ComponentModel.DataAnnotations;

//(1 đ) Tạo Model class Hàng Hóa gồm các thuộc tính: mã hàng hóa(số nguyên), tên hàng hóa(chuỗi), ngày
//sản xuất(datetime), đơn giá(số nguyên), số lượng(số nguyên), thành tiền(số nguyên), loại hàng(chuỗi).
//(1đ) Yêu cầu tạo chú thích cho các thuộc tính như sau:
//Mã hàng hóa, Tên hàng hóa, đơn giá, số lượng, loại hàng bắt buộc phải nhập
//Thành tiền không hiển thị điều khiển cho người dùng nhập (vì thành tiền sẽ được tính = đơn giá
//* số lượng)
//- Tên hàng hóa: yêu cầu độ dài tối thiểu là 2, tối đa là 30 kí tự
//Ngày sản xuất yêu cầu chú thích Kiểu dữ liệu Date
//(1đ) Tạo Controller HangHoaController, trên controller này khởi tạo 1 danh sách Hàng hóa dùng chung
//chứa tối thiểu 3 Hàng Hóa với đầy đủ dữ liệu, tạo các action và view cho action để thực hiện các công
//việc sau:
//+(1 đ) action index: Hiển thị danh sách hàng hóa trong danh sách hàng hóa chung lên view index
//+ (2 đ) action create: để thêm thông tin 1 hàng hóa mà người dùng nhập từ view create
//+ (2 đ) action delete: để xóa thông tin 1 hàng hóa trong danh sách hàng hóa chung từ view index
//+ (2 đ) action detail: để hiển thị thông tin chi tiết của 1 hàng hóa trên view Detail
//+ (2 đ) action edit : để sửa thông tin 1 hàng hóa mà người dùng trước đó đã nhập từ view create
//+ trên view index:
//(0.5đ) -thêm 1 điều khiển để nhập thông tin tìm kiếm theo đơn giá và thêm điều khiển để nhập loại
//hàng hóa lên một điều khiển đổ xuống(giống combobox)
//Sau đó tạo 1 nút submit, khi kích nút này thực hiện đồng thời 2 yêu cầu dưới:
//(1.5đ) -hiển thị danh sách các mặt hàng có giá nhỏ hơn giá nhập vào
//(1.5đ) -hiển thị tổng thành tiền các hàng hóa của loại hàng hóa được nhập vào
//(1.5đ) -hiển thị 2 hàng hóa có đơn giá lớn nhất của loại hàng hóa được nhập vào
//(1.5đ) -tìm kiếm và hiển thị hàng hóa có loại hàng là điện tử 
//(1.5đ) -tính tổng lương của các hàng hóa của 1 loại hàng với tên hàng hóa nhập vào textbox
//(1.5đ) - Tìm kiếm và hiển thị hàng hóa của 1 loại hàng với tên hàng hóa nhập vào textbox

namespace OnTapKT_Lan1.Models
{
    public class HangHoa
    {
        [Required(ErrorMessage = "Mã hàng hóa là bắt buộc")]
        public int MaHangHoa { get; set; }

        [Required(ErrorMessage = "Tên hàng hóa là bắt buộc")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Tên hàng hóa phải có độ dài từ 2 đến 30 ký tự")]
        public string TenHangHoa { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgaySanXuat { get; set; }

        [Required(ErrorMessage = "Đơn giá là bắt buộc")]
        public int DonGia { get; set; }

        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        public int SoLuong { get; set; }

        [ScaffoldColumn(false)] // Không hiển thị trường này trong view
        public int ThanhTien
        {
            get { return DonGia * SoLuong; }
        }

        //// Tính lương từ doanh thu bán hàng (5% của thành tiền)
        //public decimal Luong
        //{
        //    get { return ThanhTien * 0.05m; }
        //}

        [Required(ErrorMessage = "Loại hàng là bắt buộc")]
        public string LoaiHang { get; set; }
    }
}