using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FLICHCMUNRE.Models;
namespace FLICHCMUNRE.Controllers
{
    public class DangKyThiController : Controller
    {
        // GET: DangKyThi
        
        HCMUNREDataContext context = new HCMUNREDataContext();
        public ActionResult Index()
        {
            List<AllChiTietDangKyThiResult> ctdkkh = context.AllChiTietDangKyThi().ToList();
            TempData["AllKH"] = context.AllKhoaHocGoc().ToList();
            return View(ctdkkh);
        }
        public ActionResult Details(int id)
        {
            OneChiTietDangKyThiFullResult ctdkkh = context.OneChiTietDangKyThiFull(id).FirstOrDefault();

            return View(ctdkkh);
        }
        public ActionResult Search()
        {
            String Search = Request.Form["Search"];
            List<SearchChiTietDangKyThiResult> ctdkkh = context.SearchChiTietDangKyThi(Search).ToList();
            TempData["AllKH"] = context.AllKhoaHocGoc().ToList();
            return View(ctdkkh);


        }
    }
}