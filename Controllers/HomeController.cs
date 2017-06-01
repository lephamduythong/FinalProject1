using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Final.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Final.Controllers
{
    public class HomeController : Controller
    {
        CoSoDuLieu csdl = new CoSoDuLieu();
        // GET: Home
        public IActionResult Index()
        {
            ViewBag.ListToanVuiMoiNgay = csdl.ToanVuiMoiNgays
                .OrderBy(t => t.Order)
                .Take(2)
                .ToList();

            return View("Index");
        }
        public IActionResult BaiHoc(int lopId = 0)
        {
            if (lopId <= 0)
                return RedirectToAction("Index");

            var danhSachChuong = csdl.Chuongs
                .Where(c => c.LopId == lopId).ToList()
                .Select(c => new Chuong()
                {
                    ChuongId = c.ChuongId,
                    Ten = c.Ten,
                    BaiHocs = csdl.BaiHocs
                        .Where(b => b.ChuongId == c.ChuongId)
                        .ToList()
                        .Select(b => new BaiHoc()
                        {
                            BaiHocId = b.BaiHocId,
                            Ten = b.Ten
                        })
                        .ToList()
                }).ToList();

            ViewBag.LopId = lopId;
            return View(danhSachChuong);
        }

        public IActionResult BaiHocChiTiet(int baiHocId = 0, int lopId = 0)
        {
            if (HttpContext.Session.GetInt32("HocSinhId") == null)
            {
                return View("LoginRequirement");
            }

            if (baiHocId <= 0 || lopId <= 0)
                return RedirectToAction("Index");

            var baiHoc = csdl.BaiHocs
            .Include(b => b.BaiTaps)
            .First(b => b.BaiHocId == baiHocId);

            if (baiHoc == null)
                return RedirectToAction("Index");

            ViewBag.LopId = lopId;
            ViewBag.BaiHocId = baiHocId;

            baiHoc.NoiDung = System.Net.WebUtility.HtmlDecode(baiHoc.NoiDung);

            return View(baiHoc);
        }
        
        public IActionResult BaiTap(int baiHocId = 0, int lopId = 0)
        {
            var list = csdl.BaiTaps.Where(b => b.BaiHocId == baiHocId).ToList();
            
            var findBaiHoc = csdl.BaiHocs.First(b => b.BaiHocId == baiHocId);

            ViewBag.TenBaiHoc = findBaiHoc.Ten;
            ViewBag.LopId = lopId;
            ViewBag.BaiHocId = baiHocId;
            
            return View(list);
        }

        public IActionResult BaiTapChiTiet(int baiTapId = 0)
        {
            if (HttpContext.Session.GetInt32("HocSinhId") == null)
            {
                return View("LoginRequirement");
            }

            var result = csdl.BaiTaps
                .First(b => b.BaiTapId == baiTapId);
               
            return View(result);
        }

        public IActionResult ToanVuiMoiNgay()
        {
            var danhSach = csdl.ToanVuiMoiNgays
                .Where(t => t.Hide == 0)
                .OrderBy(t => t.Order)
                .Take(10)
                .ToList();
               
            return View(danhSach);
        }

        public IActionResult ToanVuiMoiNgayChiTiet(int toanVuiMoiNgayId)
        {
            var toanVuiMoiNgayChiTiet = csdl.ToanVuiMoiNgays.Find(toanVuiMoiNgayId);
            return View(toanVuiMoiNgayChiTiet);
        }

        // -------------------------- Static pages
        public IActionResult GioiThieu()
        {
            return View();
        }

        public IActionResult HuongDan()
        {
            return View();
        }
    }
}