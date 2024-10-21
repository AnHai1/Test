using OnTapKT_Lan1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnTapKT_Lan1.Controllers
{
    public class SinhVienController : Controller
    {
        // Danh sách sinh viên
        public static List<SinhVien> sinhViens = new List<SinhVien>
        {
            new SinhVien { MaSinhVien = 1, HoTen = "Nguyen Van A", NgaySinh = new DateTime(2000, 5, 12), GioiTinh = true, Diem = 9, DienThoai = "0123456789", Email = "a@example.com", TenLop = "Tin15a1" },
            new SinhVien { MaSinhVien = 2, HoTen = "Tran Thi B", NgaySinh = new DateTime(2001, 4, 22), GioiTinh = false, Diem = 8, DienThoai = "0987654321", Email = "b@example.com", TenLop = "Tin15a2" },
            new SinhVien { MaSinhVien = 3, HoTen = "Le Van C", NgaySinh = new DateTime(2000, 8, 17), GioiTinh = true, Diem = 10, DienThoai = "0345678901", Email = "c@example.com", TenLop = "Tin15a3" }
        };

        // Danh sách lớp học
        private List<string> LopList = new List<string> { "Tin15a1", "Tin15a2", "Tin15a3" };

        // Action Index - Hiển thị danh sách sinh viên và tìm kiếm
        public ActionResult Index(string searchLop, string searchHoTen, bool? searchGioiTinh)
        {
            var sinhVienList = sinhViens.AsQueryable();

            if (!string.IsNullOrEmpty(searchLop))
            {
                sinhVienList = sinhVienList.Where(s => s.TenLop == searchLop);
            }

            if (!string.IsNullOrEmpty(searchHoTen))
            {
                sinhVienList = sinhVienList.Where(s => s.HoTen.Contains(searchHoTen));
            }

            if (searchGioiTinh.HasValue)
            {
                sinhVienList = sinhVienList.Where(s => s.GioiTinh == searchGioiTinh);
            }

            ViewBag.DiemCaoNhat = sinhVienList.Max(s => s.Diem);

            if (searchGioiTinh.HasValue)
            {
                ViewBag.DiemThapNhat = sinhVienList.Where(s => s.GioiTinh == searchGioiTinh).Min(s => s.Diem);
            }

            ViewBag.LopList = new SelectList(LopList);

            return View(sinhVienList.ToList());
        }

        // Action Create - Tạo sinh viên
        public ActionResult Create()
        {
            ViewBag.LopList = new SelectList(LopList);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                sinhVien.MaSinhVien = sinhViens.Max(s => s.MaSinhVien) + 1;
                sinhViens.Add(sinhVien);
                return RedirectToAction("Index");
            }

            ViewBag.LopList = new SelectList(LopList);
            return View(sinhVien);
        }

        // Action Edit - Sửa thông tin sinh viên
        public ActionResult Edit(int id)
        {
            var sinhVien = sinhViens.FirstOrDefault(s => s.MaSinhVien == id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }

            ViewBag.LopList = new SelectList(LopList, sinhVien.TenLop);
            return View(sinhVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SinhVien sinhVien)
        {
            var existingSinhVien = sinhViens.FirstOrDefault(s => s.MaSinhVien == sinhVien.MaSinhVien);
            if (existingSinhVien != null && ModelState.IsValid)
            {
                existingSinhVien.HoTen = sinhVien.HoTen;
                existingSinhVien.NgaySinh = sinhVien.NgaySinh;
                existingSinhVien.GioiTinh = sinhVien.GioiTinh;
                existingSinhVien.Diem = sinhVien.Diem;
                existingSinhVien.DienThoai = sinhVien.DienThoai;
                existingSinhVien.Email = sinhVien.Email;
                existingSinhVien.TenLop = sinhVien.TenLop;

                return RedirectToAction("Index");
            }

            ViewBag.LopList = new SelectList(LopList, sinhVien.TenLop);
            return View(sinhVien);
        }

        // Action Details - Xem chi tiết sinh viên
        public ActionResult Details(int id)
        {
            var sinhVien = sinhViens.FirstOrDefault(s => s.MaSinhVien == id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // Action Delete - Xóa sinh viên
        public ActionResult Delete(int id)
        {
            var sinhVien = sinhViens.FirstOrDefault(s => s.MaSinhVien == id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sinhVien = sinhViens.FirstOrDefault(s => s.MaSinhVien == id);
            if (sinhVien != null)
            {
                sinhViens.Remove(sinhVien);
            }
            return RedirectToAction("Index");
        }

        // Action - Hiển thị danh sách sinh viên nữ
        public ActionResult DanhSachNu()
        {
            var sinhVienNu = sinhViens.Where(s => s.GioiTinh == false).ToList();
            return View(sinhVienNu);
        }
    }
}
