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
        public IActionResult Register(HocSinh hocSinh)
        {
            if (ModelState.IsValid)
            {
                csdl.HocSinhs.Add(hocSinh);
                csdl.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(hocSinh);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","HocSinhAuthentication");
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