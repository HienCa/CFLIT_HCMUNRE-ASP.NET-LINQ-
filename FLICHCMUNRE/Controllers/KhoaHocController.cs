using FLICHCMUNRE.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FLICHCMUNRE.Controllers
{
    [Authorize]
    public class KhoaHocController : Controller
    {
        // GET: KhoaHoc
        [Authorize]
        public ActionResult Index()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            List<AllKhoaHocGocResult> allKH = context.AllKhoaHocGoc().ToList();
            List<AllTheLoaiResult> AllTL = context.AllTheLoai().ToList();
            List<AllHinhThucResult> AllHT = context.AllHinhThuc().ToList();
            ViewData["AllTL"] = AllTL;
            ViewData["AllHT"] = AllHT;
            return View(allKH);
        }
        public ActionResult Details(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            OneKhoaHocCTResult KH = context.OneKhoaHocCT(id).FirstOrDefault();


            return View(KH);
        }
        [Authorize]
        public ActionResult Create()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            try
            {
                if (Request.Form.Count > 0)
                {
                    String MaKH = Request.Form["MaKH"];
                    String TenKH = Request.Form["TenKH"];
                    String gia = Request.Form["Gia"];
                    String tgbd = Request.Form["NgayBD"];
                    String tgkt = Request.Form["NgayKT"];
                    String maloai = Request.Form["MaLoai"];
                    String MaHT = Request.Form["MaHT"];


                 
                    float Gia = float.Parse(gia);
                    


                    context.InsertKhoaHoc(MaKH, TenKH, Gia, Convert.ToDateTime(tgbd), Convert.ToDateTime(tgkt), maloai, MaHT);
                    return RedirectToAction("Index");

                }
            }
            catch (SqlException e)
            {
                ViewBag.Error = e;
                return RedirectToAction("Index");
            }
            
            return View();
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            if (Request.Form.Count > 0)
            {

                String MaKH = Request.Form["MaKH"];
                String TenKH = Request.Form["TenKH"];
                String gia = Request.Form["Gia"];
                String tgbd = Request.Form["TGBD"];
                String tgkt = Request.Form["TGKT"];
                String MaLoai = Request.Form["MaLoai"];
                String MaHT = Request.Form["MaHT"];


                
                float Gia = float.Parse(gia);


                context.UpdateKhoaHoc(id, MaKH, TenKH, Gia, Convert.ToDateTime(tgbd), Convert.ToDateTime(tgkt), MaLoai, MaHT);
                context.SubmitChanges();
                return RedirectToAction("Index");

            }
            OneKhoaHocResult kh = context.OneKhoaHoc(id).FirstOrDefault();
            List<AllTheLoaiResult> AllTL = context.AllTheLoai().ToList();
            List<AllHinhThucResult> AllHT = context.AllHinhThuc().ToList();
            TempData["AllTL"] = AllTL;
            TempData["AllHT"] = AllHT;
            return View(kh);
        }
        public ActionResult Delete(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            context.DeleteKhoaHoc(id);
            return RedirectToAction("Index");


        }
        public ActionResult Search()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            Console.Write(Request.Form.Count.ToString());
            
            List<AllKhoaHocGocResult> allKH = context.AllKhoaHocGoc().ToList();
            ViewData["allKH"] = allKH;
            
            String Search = Request.Form["Search"];
            List<SearchKhoaHocResult> search = context.SearchKhoaHoc(Search).ToList();

            return View(search);

        }
    }
}