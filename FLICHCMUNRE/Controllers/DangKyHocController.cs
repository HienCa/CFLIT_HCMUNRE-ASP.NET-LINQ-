using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FLICHCMUNRE.Models;
namespace FLICHCMUNRE.Controllers
{
    [Authorize]
    public class DangKyHocController : Controller
    {
        // GET: DangKyHoc
        HCMUNREDataContext context = new HCMUNREDataContext();
        public ActionResult Index()
        {
            List<AllChiTietDangKyHocResult> ctdkkh = context.AllChiTietDangKyHoc().ToList();
            TempData["AllKH"] = context.AllKhoaHocGoc().ToList();
            return View(ctdkkh);
        }
        public ActionResult Details(int id)
        {
            OneChiTietDangKyHocFullResult ctdkkh = context.OneChiTietDangKyHocFull(id).FirstOrDefault();

            return View(ctdkkh);
        }
        public ActionResult Search()
        {
            String Search = Request.Form["Search"];
            List<SearchChiTietDangKyHocFULLResult> ctdkkh = context.SearchChiTietDangKyHocFULL(Search).ToList();
            TempData["AllKH"] = context.AllKhoaHocGoc().ToList();
            return View(ctdkkh);


        }
    }
}