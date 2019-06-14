using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;
using static WebApplication1.Common.General;

namespace WebApplication1.Controllers
{
    public class MemberController : BaseController
    {
        private WebApplication1Entities1 db = new WebApplication1Entities1();
        private static int _size = 8;
        // GET: Member
        public ActionResult Index(int? page)
        {
            ViewBag.haspagination = false;
            ViewBag.pickpage = (int)0;
            ViewBag.pages = (int)0;
            int _page = 0;
            var arradmin_thanhvien = db.admin_thanhvien.Where(s => s.daxoa != true && s.hienthi == true);
            List<admin_thanhvien> result = arradmin_thanhvien.ToList();
            if (arradmin_thanhvien.Count() > _size)
            {
                ViewBag.haspagination = true;
                if (arradmin_thanhvien.Count() % _size > 0)
                {
                    ViewBag.pages = (arradmin_thanhvien.Count() / _size) + 1;
                }
                else
                {
                    ViewBag.pages = (arradmin_thanhvien.Count() / _size);
                }
            }
            result = arradmin_thanhvien.OrderByDescending(s => s.ngaytao).Skip(_page * _size).Take(_size).ToList();
            if (page != null)
            {
                _page = Convert.ToInt32(page);
                result = arradmin_thanhvien.OrderByDescending(s => s.ngaytao).Skip((_page - 1) * _size).Take(_size).ToList();
                ViewBag.pickpage = page;
            }

            var arr_imgBanner = db.banners.Where(s => s.loaibanner1.id == (int)LoaiBannerEnum.Thanhvien && s.hienthi == true)
                .OrderBy(s => s.thutuhien).Select(s => s.urlbanner).FirstOrDefault();
            ViewBag.arrBanner = "../.." + arr_imgBanner;
            result = result.OrderBy(s => s.thutuhienthi).ToList();
            return View(result);
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_thanhvien admin_thanhvien = db.admin_thanhvien.Find(id);
            if (admin_thanhvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.hinhdaidien = @"../.." + admin_thanhvien.urlhinhanh;

            var arr_imgBanner = db.banners.Where(s => s.loaibanner1.id == (int)LoaiBannerEnum.Thanhvien && s.hienthi == true)
                .OrderBy(s => s.thutuhien).Select(s => s.urlbanner).FirstOrDefault();
            ViewBag.arrBanner = "../.." + arr_imgBanner;
            ViewBag.motasoluoc = admin_thanhvien.motasoluoc;
            return View(admin_thanhvien);
        }
    }
}