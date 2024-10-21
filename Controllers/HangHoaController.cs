using OnTapKT_Lan1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnTapKT_Lan1.Controllers
{
    public class HangHoaController : Controller
    {
        // Danh sách hàng hóa dùng chung
        public static List<HangHoa> hangHoas = new List<HangHoa>
        {
            new HangHoa { MaHangHoa = 1, TenHangHoa = "Sản phẩm A", NgaySanXuat = new DateTime(2022, 1, 10), DonGia = 100000, SoLuong = 10, LoaiHang = "Điện tử" },
            new HangHoa { MaHangHoa = 2, TenHangHoa = "Sản phẩm B", NgaySanXuat = new DateTime(2022, 2, 15), DonGia = 50000, SoLuong = 20, LoaiHang = "Thực phẩm" },
            new HangHoa { MaHangHoa = 3, TenHangHoa = "Sản phẩm C", NgaySanXuat = new DateTime(2022, 3, 20), DonGia = 150000, SoLuong = 5, LoaiHang = "Gia dụng" },
            new HangHoa { MaHangHoa = 4, TenHangHoa = "Sản phẩm D", NgaySanXuat = new DateTime(2022, 4, 10), DonGia = 200000, SoLuong = 7, LoaiHang = "Điện tử" },
            new HangHoa { MaHangHoa = 5, TenHangHoa = "Sản phẩm E", NgaySanXuat = new DateTime(2022, 5, 5), DonGia = 250000, SoLuong = 8, LoaiHang = "Gia dụng" }
        };

        // Action Index - Hiển thị danh sách hàng hóa
        public ActionResult Index(string searchLoaiHang, int? searchDonGia, string loaiHangMaxPrice, string tenHangNhap, int? soLuongNhap)
        {
            var filteredList = hangHoas.AsQueryable();

            if (!string.IsNullOrEmpty(searchLoaiHang))
            {
                filteredList = filteredList.Where(h => h.LoaiHang == searchLoaiHang);
            }

            if (searchDonGia.HasValue)
            {
                filteredList = filteredList.Where(h => h.DonGia < searchDonGia);
            }

            // Tìm kiếm và hiển thị hàng hóa có loại hàng là "Điện tử"
            var hangHoaDienTu = hangHoas.Where(h => h.LoaiHang == "Điện tử").ToList();
            ViewBag.HangHoaDienTu = hangHoaDienTu;

            // Hiển thị 2 hàng hóa có đơn giá lớn nhất của loại hàng hóa được nhập vào
            if (!string.IsNullOrEmpty(loaiHangMaxPrice))
            {
                var top2MaxPrice = hangHoas
                    .Where(h => h.LoaiHang == loaiHangMaxPrice)
                    .OrderByDescending(h => h.DonGia)
                    .Take(2)
                    .ToList();
                ViewBag.Top2MaxPrice = top2MaxPrice;
            }

            // Tính tổng giá trị (thành tiền) của các hàng hóa thuộc loại hàng nhập vào từ Textbox
            if (!string.IsNullOrEmpty(tenHangNhap))
            {
                var tongThanhTien = hangHoas
                    .Where(h => h.LoaiHang == tenHangNhap)
                    .Sum(h => h.ThanhTien);
                ViewBag.TongThanhTien = tongThanhTien;
            }

            // Tìm kiếm hàng hóa dựa trên tên hàng nhập từ textbox
            if (!string.IsNullOrEmpty(tenHangNhap))
            {
                filteredList = filteredList.Where(h => h.TenHangHoa.Contains(tenHangNhap));
            }

            // Tìm kiếm hàng hóa dựa trên số lượng nhập từ textbox
            if (soLuongNhap.HasValue)
            {
                filteredList = filteredList.Where(h => h.SoLuong == soLuongNhap);
            }

            ViewBag.TongThanhTienAll = filteredList.Sum(h => h.ThanhTien);
            // Tính trung bình cộng thành tiền
            var count = filteredList.Count();
            ViewBag.TrungBinhThanhTien = count > 0 ? filteredList.Average(h => h.ThanhTien) : 0;

            return View(filteredList.ToList());
        }

        public ActionResult HangHoaDienTu()
        {
            // Lấy danh sách các hàng hóa có loại "Điện tử"
            var hangHoaDienTu = hangHoas.Where(h => h.LoaiHang == "Điện tử").ToList();

            // Trả về view cùng với danh sách hàng hóa
            return View(hangHoaDienTu);
        }

        // Action Create - Thêm hàng hóa
        public ActionResult Create()
        {
            // Danh sách các loại hàng hóa có thể chọn
            ViewBag.LoaiHangList = new SelectList(new List<string> { "Điện tử", "Thực phẩm", "Gia dụng" });
            return View();
        }

        [HttpPost]
        public ActionResult Create(HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                hangHoa.MaHangHoa = hangHoas.Count + 1;
                hangHoas.Add(hangHoa);
                return RedirectToAction("Index");
            }

            // Nếu ModelState không hợp lệ, gửi lại danh sách Loại hàng cho View
            ViewBag.LoaiHangList = new SelectList(new List<string> { "Điện tử", "Thực phẩm", "Gia dụng" });
            return View(hangHoa);
        }

        // Action Details - Xem chi tiết hàng hóa
        public ActionResult Details(int id)
        {
            var hangHoa = hangHoas.FirstOrDefault(h => h.MaHangHoa == id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        // Action Edit - Sửa thông tin hàng hóa
        public ActionResult Edit(int id)
        {
            var hangHoa = hangHoas.FirstOrDefault(h => h.MaHangHoa == id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        [HttpPost]
        public ActionResult Edit(HangHoa hangHoa)
        {
            var existingHangHoa = hangHoas.FirstOrDefault(h => h.MaHangHoa == hangHoa.MaHangHoa);
            if (existingHangHoa != null && ModelState.IsValid)
            {
                existingHangHoa.TenHangHoa = hangHoa.TenHangHoa;
                existingHangHoa.NgaySanXuat = hangHoa.NgaySanXuat;
                existingHangHoa.DonGia = hangHoa.DonGia;
                existingHangHoa.SoLuong = hangHoa.SoLuong;
                existingHangHoa.LoaiHang = hangHoa.LoaiHang;

                return RedirectToAction("Index");
            }

            return View(hangHoa);
        }

        // Action Delete - Xóa hàng hóa
        public ActionResult Delete(int id)
        {
            var hangHoa = hangHoas.FirstOrDefault(h => h.MaHangHoa == id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var hangHoa = hangHoas.FirstOrDefault(h => h.MaHangHoa == id);
            if (hangHoa != null)
            {
                hangHoas.Remove(hangHoa);
            }
            return RedirectToAction("Index");
        }
    }
}
