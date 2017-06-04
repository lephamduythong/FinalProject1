using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

using Final.Models;
using Newtonsoft.Json;

namespace Final.Controllers
{
    public class HocSinhAuthenticationController : Controller
    {
        CoSoDuLieu csdl = new CoSoDuLieu();
        
        // GET: User/Authentication
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            HttpContext.Session.Clear();

            if (csdl.HocSinhs.Any(h => h.Email.Equals(email) && h.Password.Equals(password)))
            {
                var hocSinhFound = csdl.HocSinhs.First(h => h.Email.Equals(email) && h.Password.Equals(password));
                List<string> hocSinhInfo = new List<string>();
                
                hocSinhInfo.Add(hocSinhFound.HocSinhId.ToString());
                hocSinhInfo.Add(hocSinhFound.HoTen);
                hocSinhInfo.Add(hocSinhFound.Hinh);
                
                HttpContext.Session.SetInt32("HocSinhId", hocSinhFound.HocSinhId);
                HttpContext.Session.SetString("HocSinhHoTen", hocSinhFound.HoTen);
                HttpContext.Session.SetString("HocSinhHinh", hocSinhFound.Hinh);
                HttpContext.Session.SetString("HocSinhNgaySinh", hocSinhFound.NgaySinh.ToString("dd/MM/yyyy"));
                HttpContext.Session.SetString("HocSinhEmail", hocSinhFound.Email);
                

                var hetHan = csdl.HocPhis
                    .Where(h => h.HocSinhId == hocSinhFound.HocSinhId)
                    .OrderByDescending(h => h.NgayHetHan)
                    .First();

                HttpContext.Session.SetString("NgayHetHan", hetHan.NgayHetHan.ToString());
                
                // HttpContext.Session.Set<List<string>>("HocSinh", hocSinhInfo); // HocSinhId
                return RedirectToAction("Index", "Home");   
            }   
            else
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu";
                return View("Index");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(HocSinh hocSinh, string confirmPassword)
        {
            HttpContext.Session.Clear();

            var tuoi = (DateTime.Now.Year - hocSinh.NgaySinh.Year);
            
            if (csdl.HocSinhs.Any(h => h.Email.Equals(hocSinh.Email)))
            {
                ViewData["DuplicateEmailError"] = "Email đã được sử dụng";
                return View(hocSinh);
            }
        
            if (confirmPassword == null)
            {
                ViewData["ConfirmPasswordError"] = "Mật khẩu nhập lại không được bỏ trống";
                return View(hocSinh);
            }

            if (ModelState.IsValid)
            {
                if (DateTime.Compare(hocSinh.NgaySinh, DateTime.Now.AddYears(-5)) == 1 || tuoi > 100)
                {
                    ViewData["NgaySinhError"] = "Chỉ chấp nhận học sinh từ 5 tuổi trở lên và 100 tuổi trở xuống hoặc ngày sinh không được vượt quá hiện tại";
                    return View(hocSinh);
                }
                if (!hocSinh.Password.Equals(confirmPassword))
                {
                    ViewData["ConfirmPasswordError"] = "Mật khẩu nhập lại không khớp";
                    return View(hocSinh);
                }
                csdl.HocSinhs.Add(hocSinh);
                csdl.SaveChanges();
                return View("RegisterSucessfull");
            }

            
            else  if (!hocSinh.Password.Equals(confirmPassword))
            {
                ViewData["ConfirmPasswordError"] = "Mật khẩu nhập lại không khớp";
            }

            if (DateTime.Compare(hocSinh.NgaySinh, DateTime.Now.AddYears(-5)) == 1 || tuoi > 100)
            {
                ViewData["NgaySinhError"] = "Chỉ chấp nhận học sinh từ 5 tuổi trở lên và 100 tuổi trở xuống hoặc ngày sinh không được vượt quá hiện tại";
                return View(hocSinh);
            }

            return View(hocSinh);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","HocSinhAuthentication");
        }

        [HttpGet]
        public IActionResult EditInformation() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditInformation(string HoTen, string NgaySinh, string Hinh) 
        {
            if (String.IsNullOrWhiteSpace(HoTen) || String.IsNullOrWhiteSpace(NgaySinh))
            {
                ViewData["Error"] = "Thông tin thay đổi không được bỏ trống";
                return View();
            }

            int hocSinhId = (int)HttpContext.Session.GetInt32("HocSinhId");
            var foundHocSinh = csdl.HocSinhs.First(h => h.HocSinhId == hocSinhId);
            foundHocSinh.HoTen = HoTen;
            foundHocSinh.NgaySinh = DateTime.Parse(NgaySinh);
            foundHocSinh.Hinh = Hinh;
            csdl.SaveChanges();

            HttpContext.Session.SetString("HocSinhHoTen", foundHocSinh.HoTen);
            HttpContext.Session.SetString("HocSinhHinh", foundHocSinh.Hinh);
            HttpContext.Session.SetString("HocSinhNgaySinh", foundHocSinh.NgaySinh.ToString("dd/MM/yyyy"));

            return RedirectToAction("Index","Home");
        }

    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session,string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}