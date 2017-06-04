using System;
using System.Linq;
using Final.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    public class AjaxController : Controller
    {
        CoSoDuLieu csdl = new CoSoDuLieu();
        [HttpPost]
        public ActionResult Comment(IFormCollection collection)
        {
            var code = Convert.ToInt16(collection["code"]);
            var idBaiHoc = Convert.ToInt16(collection["idBaiHoc"]);

            if (code == 1)
            {
                var list = (from binhLuan in csdl.BinhLuans
                            where binhLuan.BaiHocId == idBaiHoc
                            orderby binhLuan.Order descending
                            select new
                            {
                                id = binhLuan.BinhLuanId
                            }).Take(10).ToList();
                return Json(list);
            }

            if (code == 2)
            {
                var idBinhLuan = Convert.ToInt16(collection["idBinhLuan"]);
                var result = (from binhLuan in csdl.BinhLuans
                    where binhLuan.BinhLuanId == idBinhLuan
                    select new
                    {
                        tacGia = binhLuan.TacGia,
                        noiDung = binhLuan.NoiDung,
                        hinh = binhLuan.Hinh,
                    }).First();
                           
                return Json(result);
            }

            if (code == 3)
            {
                int newOrder = 1;

                if (csdl.BinhLuans.Any())
                     newOrder = (from binhLuan in csdl.BinhLuans
                                orderby binhLuan.Order descending
                                select binhLuan.Order).First() + 1;
                
                BinhLuan newBinhLuan = new BinhLuan()
                {
                    TacGia = collection["tacGia"],
                    NoiDung = collection["noiDung"],
                    BaiHocId = idBaiHoc,
                    Hinh = collection["hinh"],
                    Order = newOrder
                };
                csdl.BinhLuans.Add(newBinhLuan);
                csdl.SaveChanges();
                return Json(newBinhLuan.BinhLuanId);
            }

            return null;
        }

        [HttpPost]
        public ActionResult Excercise(IFormCollection collection)
        {
            var code = Convert.ToInt16(collection["code"]);
           
            if (code == 1)
            {
                var idBaiTap = Convert.ToInt16(collection["idBaiTap"]);
                var result = from cauHoi in csdl.CauHois
                    where cauHoi.BaiTapId == idBaiTap
                    select new
                    {
                        id = cauHoi.CauHoiId,
                        noiDung = cauHoi.NoiDung,
                        cauTraLoi1 = cauHoi.CauTraLoi1,
                        cauTraLoi2 = cauHoi.CauTraLoi2,
                        cauTraLoi3 = cauHoi.CauTraLoi3,
                        cauTraLoi4 = cauHoi.CauTraLoi4,
                    };

                return Json(result);
            }

            if (code == 2)
            {
                // listIdQuestion, choosingAnswer
                var listIdQuestion = collection["listIdQuestion[]"].ToString().Split(',');
                var choosingAnswer = collection["choosingAnswer[]"].ToString().Split(',');
                var idHocSinh = Convert.ToInt16(collection["idHocSinh"]);
                var idBaiTap = Convert.ToInt16(collection["idBaiTap"]);
                int numOfRightAnswer = 0;
                for (int i = 0; i < listIdQuestion.Length; i++)
                {
                    var findQuestion = csdl.CauHois
                    .First(c => c.CauHoiId == Convert.ToInt16(listIdQuestion[i]));
                    if (findQuestion != null && choosingAnswer[i].Equals(findQuestion.CauDung.ToString()))
                        numOfRightAnswer++;
                }
                double score = ((double) numOfRightAnswer / (double) listIdQuestion.Length) * 100;

                if (csdl.HocSinhBaiTaps.Any(h => h.HocSinhId == idHocSinh && h.BaiTapId == idBaiTap))
                {
                    // Update
                    var updateObj = csdl.HocSinhBaiTaps.First(h => h.HocSinhId == idHocSinh && h.BaiTapId == idBaiTap);
                    updateObj.HoanThanh = (int) score;
                    csdl.SaveChanges();
                    return Json(score);
                }

                var scoreRow = new HocSinhBaiTap()
                {
                    HocSinhId = idHocSinh,
                    BaiTapId = idBaiTap,
                    HoanThanh = (int) score
                };

                csdl.HocSinhBaiTaps.Add(scoreRow);
                csdl.SaveChanges();

                return Json(score);
            }

            return null;
        }

    }
}