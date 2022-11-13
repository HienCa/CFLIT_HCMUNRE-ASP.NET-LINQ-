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
    public class TheLoaiController : Controller
    {
        // GET: TheLoai
        [Authorize]
        public ActionResult Index()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            List<AllTheLoaiResult> allTL = context.AllTheLoai().ToList();
            return View(allTL);
        }
        public ActionResult Details(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            OneTheLoaiResult TL = context.OneTheLoai(id).FirstOrDefault();


            return View(TL);
        }
        public ActionResult Create()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();

            try
            {
                if (Request.Form.Count > 0)
                {
                    String MaLoai = Request.Form["MaLoai"];
                    String TenLoai = Request.Form["TenLoai"];

                    context.InsertTheLoai(MaLoai, TenLoai);
                    return RedirectToAction("Index");

                }
            }
            
            catch (SqlException e)
            {
                ViewBag.Error = "Mã đã tồn tại, vui lòng chọn mã khác.";
                return RedirectToAction("Index");
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            if (Request.Form.Count > 0)
            {

                String MaLoai = Request.Form["MaLoai"];
                String TenLoai = Request.Form["TenLoai"];


                context.UpdateTheLoai(id, MaLoai, TenLoai);
                context.SubmitChanges();
                return RedirectToAction("Index");

            }
            OneTheLoaiResult tk = context.OneTheLoai(id).FirstOrDefault();

            return View(tk);
        }
        public ActionResult Delete(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            context.DeleteTheLoai(id);
            return RedirectToAction("Index");


        }
        public ActionResult Search()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            Console.Write(Request.Form.Count.ToString());

            String Search = Request.Form["Search"];
            List<SearchTheLoaiResult> search = context.SearchTheLoai(Search).ToList();

            return View(search);

        }
        public ActionResult I()

        {
            HCMUNREDataContext context = new HCMUNREDataContext();

            List<THELOAI> t = context.THELOAIs.ToList();
            return View(t);
        }

        public ActionResult De(int id)

        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            THELOAI tl = context.THELOAIs.FirstOrDefault(x => x.ID == id);
            return View(tl);
        }

        public ActionResult Cr()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();

            if (Request.Form.Count > 0)
            {
                String MaLoai = Request.Form["MaLoai"];
                String TenLoai = Request.Form["TenLoai"];
                THELOAI tl = new THELOAI();
                tl.TENLOAI = TenLoai;
                tl.MALOAI = MaLoai;
                context.THELOAIs.InsertOnSubmit(tl);
                context.SubmitChanges();
                
            }
            return View();
        }

        public ActionResult Del(int id)

        {
            HCMUNREDataContext context = new HCMUNREDataContext();

            if (Request.Form.Count > 0)
            {
                THELOAI tl = context.THELOAIs.FirstOrDefault(x => x.ID == id );
                if(tl != null)
                {
                    context.THELOAIs.DeleteOnSubmit(tl);
                    context.SubmitChanges();
                }

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Ed(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();

            if (Request.Form.Count > 0)
            {
                String MaLoai = Request.Form["MaLoai"];
                String TenLoai = Request.Form["TenLoai"];

                THELOAI tl = context.THELOAIs.FirstOrDefault(x => x.ID == id);
                tl.TENLOAI = TenLoai;
                tl.MALOAI = MaLoai;
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}