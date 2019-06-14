using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;
using static WebApplication1.Common.General;

namespace WebApplication1.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        private WebApplication1Entities1 db = new WebApplication1Entities1();
        public class IndexBanner
        {
            public int id { get; set; }
            public string tenbanner { get; set; }
            public string urlbanner { get; set; }
            public string motabanner { get; set; }
            public string ghichu { get; set; }
            public Nullable<System.DateTime> ngaytao { get; set; }
            public Nullable<System.DateTime> ngaycapnhat { get; set; }
            public Nullable<int> nguoitao { get; set; }
            public Nullable<bool> hienthi { get; set; }
            public Nullable<int> thutuhien { get; set; }
            public Nullable<bool> daxoa { get; set; }
            public Nullable<int> loaibanner { get; set; }

            public virtual admin_account admin_account { get; set; }
            public virtual loaibanner loaibanner1 { get; set; }
        }

        public class IndexThanhVien
        {
            public int id { get; set; }
            public string tenthanhvien { get; set; }
            public string motasoluoc { get; set; }
            public string chitiet { get; set; }
            public string urlhinhanh { get; set; }
            public Nullable<System.DateTime> ngaytao { get; set; }
            public Nullable<System.DateTime> ngaycapnhat { get; set; }
            public Nullable<int> nguoitao { get; set; }
            public Nullable<bool> daxoa { get; set; }
            public string ghichu { get; set; }
            public Nullable<int> nhomthanhvien { get; set; }
            public Nullable<bool> hienthi { get; set; }

            public virtual admin_account admin_account { get; set; }
            public virtual admin_nhomthanhvien admin_nhomthanhvien { get; set; }
        }

        public class ToIndexPage
        {
            public List<string> arrBanner { get; set; }
            public List<admin_thanhvien> IndexThanhVien { get; set; }
            public string thongtincodinh { get; set; }
        }
        public ActionResult Index()
        {
            var zz = Thread.CurrentThread.CurrentCulture;

            //var _lang = lang;
            //if (_lang != null)
            //{
            //    if(_lang == "en-US")
            //    {
            //        Session["CurrentCulture"] = "en-US";
            //        Thread.CurrentThread.CurrentCulture = new CultureInfo(_lang);
            //        Thread.CurrentThread.CurrentUICulture = new CultureInfo(_lang);
            //        var zz = Resources.Resource.gioithieu;
            //    }
            //    else
            //    {
            //        Session["CurrentCulture"] = "vi-VN";
            //        Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            //        Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");
            //        var zz = Resources.Resource.gioithieu;
            //    }
            //}

            admin_thongtincodinh arradmin_thongtincodinh = db.admin_thongtincodinh.Find(-1);
            var arr_imgBanner = db.banners.Where(s => s.loaibanner1.id == (int)LoaiBannerEnum.Trangchu && s.hienthi == true)
                .OrderBy(s => s.thutuhien).Select(s=>s.urlbanner).ToList();
            //ViewBag.arrBanner = arr_imgBanner;

            List<admin_thanhvien> arrThanhvien = db.admin_thanhvien.Where(s => s.daxoa != true && s.hienthi == true).OrderBy(s=>s.thutuhienthi).ToList();
            //return View(arradmin_thongtincodinh);

            // View Model
            ToIndexPage indexpage = new ToIndexPage();
            indexpage.arrBanner = arr_imgBanner;
            indexpage.IndexThanhVien= arrThanhvien;
            indexpage.thongtincodinh = arradmin_thongtincodinh.baiviettrangchu;
            if (Session["CurrentCulture"] != null)
            {
                if(Session["CurrentCulture"].ToString() == "en-US")
                {
                    indexpage.thongtincodinh = arradmin_thongtincodinh.baiviettrangchu_en;
                }
            }

            return View(indexpage);
        }

        public ActionResult About()
        {
            List<admin_gioithieu> arr = db.admin_gioithieu.Where(s => s.daxoa != true && s.hienthi == true)
                .OrderBy(s=>s.thutuhienthi).ToList();
            var arr_imgBanner = db.banners.Where(s => s.loaibanner1.id == (int)LoaiBannerEnum.Gioithieu && s.hienthi == true)
                .OrderBy(s => s.thutuhien).Select(s => s.urlbanner).FirstOrDefault();
            ViewBag.arrBanner = "../../"+arr_imgBanner;
            return View(arr);
        }

        public ActionResult Contact()
        {
            List<admin_baivietlienhe> arr = db.admin_baivietlienhe.Where(s => s.daxoa != true && s.hienthi == true)
                   .OrderBy(s => s.thutuhienthi).ToList();
            var arr_imgBanner = db.banners.Where(s => s.loaibanner1.id == (int)LoaiBannerEnum.Lienhe && s.hienthi == true)
                .OrderBy(s => s.thutuhien).Select(s => s.urlbanner).FirstOrDefault();
            ViewBag.arrBanner = "../../" + arr_imgBanner;

            admin_thongtincodinh ggmap = db.admin_thongtincodinh.FirstOrDefault();
            ViewBag.ggmap = ggmap.diachigmap;
            ViewBag.thongtincodinh_diachi = ggmap.diachi;
            ViewBag.thongtincodinh_dienthoai = ggmap.dienthoai;
            ViewBag.thongtincodinh_mail = ggmap.mail;

            ViewBag._website = ggmap.website;

            if (Session["CurrentCulture"] != null)
            {
                if (Session["CurrentCulture"].ToString() == "en-US")
                {
                    ViewBag.thongtincodinh_diachi = ggmap.diachi_en;
                }
            }

            return View(arr);
        }
    }
}