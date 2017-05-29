using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Final.Models;

namespace Final.Controllers
{
    public class AdminController : Controller
    { 
        CoSoDuLieu csdl = new CoSoDuLieu();
        // GET: Admin/Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuanLyBaiHoc(IFormCollection collection)
        {
            int code = Convert.ToInt16(collection["code"]);
            if (code == 1)
            {
                var result = new List<object>();
                for (int i = 1; i < 5; i++)
                {
                    dynamic temp = (from chuong in csdl.Chuongs
                        where chuong.LopId == i
                        orderby chuong.Order ascending 
                        select new
                        {
                            id = chuong.ChuongId
                        }).ToList();
                    result.Add(temp);
                }
                return Json(result);
            }
            if (code == 2)
            {
                var idChuong = Convert.ToInt16(collection["idChuong"]);
                var result = (from chuong in csdl.Chuongs
                    where chuong.ChuongId == idChuong
                    orderby chuong.Order ascending
                    select new
                    {
                        ten = chuong.Ten,
                        idListBaiHoc = (from baiHoc in csdl.BaiHocs
                                       where baiHoc.ChuongId == chuong.ChuongId
                                       orderby baiHoc.Order ascending
                                       select new
                                       {
                                           id = baiHoc.BaiHocId
                                       }).ToList()
                    }).First();
                return Json(result);
            }
            if (code == 3)
            {
                var idBaiHoc = Convert.ToInt16(collection["idBaiHoc"]);
                var result = (from baiHoc in csdl.BaiHocs
                    where baiHoc.BaiHocId == idBaiHoc
                    orderby baiHoc.Order ascending
                    select new
                    {
                        ten = baiHoc.Ten,
                        idListBaiTap = (from baiTap in csdl.BaiTaps
                                        where baiTap.BaiHocId == baiHoc.BaiHocId
                                        orderby baiTap.BaiTapId
                                        select new {
                                            id = baiTap.BaiTapId
                                        }).ToList(),
                    }).First();
                return Json(result);
            }
            if (code == 4)
            {
                var idBaiHoc = Convert.ToInt16(collection["idBaiHoc"]);
                var result = (from baiHoc in csdl.BaiHocs
                              where baiHoc.BaiHocId == idBaiHoc
                              orderby baiHoc.Order ascending
                              select new
                              {
                                  noidung = baiHoc.NoiDung
                              }).First();
                return Json(result);
            }

            if (code == 5)
            {
                var idLop = Convert.ToInt16(collection["idLop"]);
                
                var newOrder = 0;

                if (csdl.Chuongs.Any())
                {
                    newOrder = (from chuong in csdl.Chuongs
                        orderby chuong.Order descending
                        select chuong.Order).First() + 1;
                }
                
                var newChuong = new Chuong()
                {
                    Ten = collection["ten"],
                    LopId = idLop,
                    Order = newOrder,
                };

                csdl.Chuongs.Add(newChuong);
                csdl.SaveChanges();

                return Json(newChuong.ChuongId);
            }

            if (code == 6)
            {
                var idChuong = Convert.ToInt16(collection["idChuong"]);
                csdl.Chuongs.Remove(csdl.Chuongs.First(c => c.ChuongId == idChuong));
                csdl.SaveChanges();
                return Json("Deleted");
            }

            if (code == 7)
            {
                var idChuong = Convert.ToInt16(collection["idChuong"]);
                
                var newOrder = 0;

                if (csdl.BaiHocs.Any())
                {
                    newOrder = (from baiHoc in csdl.BaiHocs
                                orderby baiHoc.Order descending
                                select baiHoc.Order).First() + 1;
                }
                
                var newBaiHoc = new BaiHoc()
                {
                    Ten = collection["ten"],
                    ChuongId = idChuong,
                    Order = newOrder,
                    NoiDung = "Chưa có nội dung",
                };
                csdl.BaiHocs.Add(newBaiHoc);
                csdl.SaveChanges();
                return Json(newBaiHoc.BaiHocId);
            }

            if (code == 8)
            {
                var idBaiHoc = Convert.ToInt16(collection["idBaiHoc"]);
                csdl.BaiHocs.Remove(csdl.BaiHocs.First(b => b.BaiHocId == idBaiHoc));
                csdl.SaveChanges();
                return Json("Deleted");
            }

            if (code == 9)
            {
                var idBaiHoc = Convert.ToInt16(collection["idBaiHoc"]);
                var idChuong = Convert.ToInt16(collection["idChuong"]);
                var updateBaiHoc = csdl.BaiHocs.First(b => b.BaiHocId == idBaiHoc);
                if (updateBaiHoc != null) updateBaiHoc.NoiDung = collection["noiDungHtml"];
                csdl.SaveChanges();
                return Json("Updated");
            }

            return null;
        }

        [HttpPost]
        public ActionResult QuanLyBaiTap(IFormCollection collection)
        {
            int code = Convert.ToInt16(collection["code"]);
            if (code == 1)
            {
                var idBaiTap = Convert.ToInt16(collection["idBaiTap"]);
                var result = (from baiTap in csdl.BaiTaps
                    where baiTap.BaiTapId == idBaiTap
                    select new
                    {
                        ten = baiTap.Ten,
                        idListCauHoi = (from cauHoi in csdl.CauHois
                            where cauHoi.BaiTapId == baiTap.BaiTapId
                            select new
                            {
                                id = cauHoi.CauHoiId
                            }).ToList()
                    }).First();
                return Json(result);
            }

            if (code == 2)
            {
                var idCauHoi = Convert.ToInt16(collection["idCauHoi"]);
                var result = (from cauHoi in csdl.CauHois
                    where cauHoi.CauHoiId == idCauHoi
                    select new
                    {
                        noiDung = cauHoi.NoiDung,
                        cauTraLoi1 = cauHoi.CauTraLoi1,
                        cauTraLoi2 = cauHoi.CauTraLoi2,
                        cauTraLoi3 = cauHoi.CauTraLoi3,
                        cauTraLoi4 = cauHoi.CauTraLoi4,
                        cauDung = cauHoi.CauDung
                    }).First();
                return Json(result);
            }

            if (code == 3)
            {
                var idCauHoi = Convert.ToInt16(collection["idCauHoi"]);
                var cauDung = Convert.ToInt16(collection["cauDung"]);

                var result = csdl.CauHois.FirstOrDefault(x => x.CauHoiId == idCauHoi);
                if (result != null)
                {
                    result.CauDung = cauDung;
                    csdl.SaveChanges();
                }
                return Json("Updated");
            } 

            if (code == 4)
            {
                var idBaiHoc = Convert.ToInt16(collection["idBaiHoc"]);

                int newOrder = 1;

                if (csdl.BaiTaps.Any()) {
                     newOrder = (from baiTap in csdl.BaiTaps
                                where baiTap.BaiHocId == idBaiHoc
                                orderby baiTap.Order descending
                                select baiTap.Order).First() + 1;
                }

                var tenBaiTap = collection["tenBaiTap"];
                var newBaiTap = new BaiTap()
                {
                    Ten = tenBaiTap,
                    BaiHocId = idBaiHoc,
                    Order = newOrder,
                    Hide = 0
                };
                csdl.BaiTaps.Add(newBaiTap);
                csdl.SaveChanges();
                return Json(newBaiTap.BaiTapId);
            }

            if (code == 5)
            {
                var idBaiTap = Convert.ToInt16(collection["idBaiTap"]);
                var baiTapToDelete = csdl.BaiTaps.First(b => b.BaiTapId == idBaiTap);
                if (baiTapToDelete != null) csdl.BaiTaps.Remove(baiTapToDelete);
                csdl.SaveChanges();
                return Json("Deleted");
            }

            if (code == 6)
            {
                var idBaiTap = Convert.ToInt16(collection["idBaiTap"]);
                var newCauHoi = new CauHoi()
                {
                    NoiDung = "Chưa có nội dung",
                    CauTraLoi1 = "Chưa có câu trả lời 1",
                    CauTraLoi2 = "Chưa có câu trả lời 2",
                    CauTraLoi3 = "Chưa có câu trả lời 3",
                    CauTraLoi4 = "Chưa có câu trả lời 4",
                    BaiTapId = idBaiTap,
                    CauDung = 1
                };
                csdl.CauHois.Add(newCauHoi);
                csdl.SaveChanges();
                return Json(newCauHoi.CauHoiId);
            }

            if (code == 7)
            {
                var idCauHoi = Convert.ToInt16(collection["idCauHoi"]);
                var cauDung = Convert.ToInt16(collection["cauDung"]);
                var changeCauHoi = csdl.CauHois.First(c => c.CauHoiId == idCauHoi);
                switch (cauDung)
                {
                    case 1:
                        if (changeCauHoi != null) changeCauHoi.CauTraLoi1 = collection["noiDungCauTraLoi"];
                        break;
                    case 2:
                        if (changeCauHoi != null) changeCauHoi.CauTraLoi2 = collection["noiDungCauTraLoi"];
                        break;
                    case 3:
                        if (changeCauHoi != null) changeCauHoi.CauTraLoi3 = collection["noiDungCauTraLoi"];
                        break;
                    case 4:
                        if (changeCauHoi != null) changeCauHoi.CauTraLoi4 = collection["noiDungCauTraLoi"];
                        break;
                }
                csdl.SaveChanges();
                return Json("Updated");
            }

            if (code == 8)
            {
                var idCauHoi = Convert.ToInt16(collection["idCauHoi"]);
                var noiDungHtml = collection["noiDungHtml"];
                var changeCauHoi = csdl.CauHois.First(c => c.CauHoiId == idCauHoi);
                if (changeCauHoi != null) changeCauHoi.NoiDung = noiDungHtml;
                csdl.SaveChanges();
                return Json("Updated");
            }

            if (code == 9)
            {
                var idCauHoi = Convert.ToInt16(collection["idCauHoi"]);
                csdl.CauHois.Remove(csdl.CauHois.First(c => c.CauHoiId == idCauHoi));
                csdl.SaveChanges();
                return Json("Deleted");
            }

            return null;
        }
    }
}