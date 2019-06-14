using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;
using static WebApplication1.Common.General;

namespace WebApplication1.Controllers
{
    public class GalleryController : BaseController
    {
        private WebApplication1Entities1 db = new WebApplication1Entities1();
        private static int _size = 8;
        // GET: Gallery
        public ActionResult Index(int? page)
        {
            ViewBag.haspagination = false;
            ViewBag.pickpage = (int)0;
            ViewBag.pages = (int)0;
            int _page = 0;
            var arradmin_loaihinhanhn = db.admin_loaihinhanh.Where(s => s.daxoa != true && s.hienthi==true);
            List<admin_loaihinhanh> result = arradmin_loaihinhanhn.ToList();
            if (arradmin_loaihinhanhn.Count() > _size)
            {
                ViewBag.haspagination = true;
                if (arradmin_loaihinhanhn.Count() % _size > 0)
                {
                    ViewBag.pages = (arradmin_loaihinhanhn.Count() / _size) + 1;
                }
                else
                {
                    ViewBag.pages = (arradmin_loaihinhanhn.Count() / _size);
                }
            }
            result = arradmin_loaihinhanhn.OrderByDescending(s => s.ngaytao).Skip(_page * _size).Take(_size).ToList();
            if (page != null)
            {
                _page = Convert.ToInt32(page);
                result = arradmin_loaihinhanhn.OrderByDescending(s => s.ngaytao).Skip((_page - 1) * _size).Take(_size).ToList();
                ViewBag.pickpage = page;
            }

            var arr_imgBanner = db.banners.Where(s => s.loaibanner1.id == (int)LoaiBannerEnum.Hinhanh && s.hienthi == true)
                .OrderBy(s => s.thutuhien).Select(s => s.urlbanner).FirstOrDefault();
            ViewBag.arrBanner = "../.." + arr_imgBanner;

            return View(result);
        }
        public ActionResult Detail(int? id)
        {
            var imageFiles = db.admin_hinhanh.Where(s => s.loaihinhanh == id).OrderBy(s=>s.thutuhien).ToList();
            List<string> lstImg = new List<string>();
            foreach (var item in imageFiles)
            {
                lstImg.Add(item.urlhinhanh);
            }
            ViewBag._lstImg = lstImg;
            var loaihinhanh_= db.admin_loaihinhanh.Find(id);
            ViewBag.tenalbum = loaihinhanh_.tenloaihinhanh;
            if(Session["CurrentCulture"] != null)
            {
                if (Session["CurrentCulture"].ToString() =="en-US")
                {
                    ViewBag.tenalbum = loaihinhanh_.tenloaihinhanh_en;
                }
                else
                {
                    ViewBag.tenalbum = loaihinhanh_.tenloaihinhanh;
                }
            }
            var arr_imgBanner = db.banners.Where(s => s.loaibanner1.id == (int)LoaiBannerEnum.Hinhanh && s.hienthi == true)
                .OrderBy(s => s.thutuhien).Select(s => s.urlbanner).FirstOrDefault();
            ViewBag.arrBanner = "../.." + arr_imgBanner;
            return View();
        }
    }
}