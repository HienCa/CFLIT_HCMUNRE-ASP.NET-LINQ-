using FLICHCMUNRE.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FLICHCMUNRE.Controllers
{
    [Authorize]
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        [Authorize]
        public ActionResult Index()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            List<AllTaiKhoanResult> allTK = context.AllTaiKhoan().ToList();
            return View(allTK);
        }
        [Authorize]
        public ActionResult QLAdmin()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            List<AllTaiKhoanResult> allTK = context.AllTaiKhoan().ToList();


            return View(allTK);
        }
        [Authorize]
        public ActionResult QLQuanLy()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";

            //get cookie User.Identity.Name
            OneTaiKhoanPersonalResult one = context.OneTaiKhoanPersonal(User.Identity.Name).FirstOrDefault();

            return View(one);
        }
        [Authorize]
        public ActionResult QLTKHV()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";

            //get cookie User.Identity.Name
            OneTaiKhoanPersonalResult one = context.OneTaiKhoanPersonal(User.Identity.Name).FirstOrDefault();

            return View(one);
        }
        [Authorize]
        public ActionResult UpdateTKHV(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            if (Request.Form.Count > 0)
            {

                String TK = Request.Form["TK"];
                String MK = Request.Form["MK"];
                String VT = Request.Form["VAITRO"];


                context.UpdateTaiKhoan(id, TK, MK, VT);
                context.SubmitChanges();
                return RedirectToAction("QLTKHV");

            }
            OneTaiKhoanResult tk = context.OneTaiKhoan(id).FirstOrDefault();

            return View(tk);
        }


        [Authorize]
        public ActionResult QLTKGV()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";

            //get cookie User.Identity.Name
            OneTaiKhoanPersonalResult one = context.OneTaiKhoanPersonal(User.Identity.Name).FirstOrDefault();

            return View(one);
        }

        [Authorize]
        public ActionResult UpdateTKGV(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            if (Request.Form.Count > 0)
            {

                String TK = Request.Form["TK"];
                String MK = Request.Form["MK"];
                String VT = Request.Form["VAITRO"];


                context.UpdateTaiKhoan(id, TK, MK, VT);
                context.SubmitChanges();
                return RedirectToAction("QLTKGV");

            }
            OneTaiKhoanResult tk = context.OneTaiKhoan(id).FirstOrDefault();

            return View(tk);
        }
        [Authorize]
        public ActionResult UpdateTKQL(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            if (Request.Form.Count > 0)
            {

                String TK = Request.Form["TK"];
                String MK = Request.Form["MK"];
                String VT = Request.Form["VAITRO"];


                context.UpdateTaiKhoan(id, TK, MK, VT);
                context.SubmitChanges();
                return RedirectToAction("QLQuanLy");

            }
            OneTaiKhoanResult tk = context.OneTaiKhoan(id).FirstOrDefault();

            return View(tk);
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            OneTaiKhoanResult TK = context.OneTaiKhoan(id).FirstOrDefault();


            return View(TK);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            if (Request.Form.Count > 0)
            {

                String TK = Request.Form["TK"];
                String MK = Request.Form["MK"];
                String VT = Request.Form["VAITRO"];


                context.UpdateTaiKhoan(id, TK, MK, VT);
                context.SubmitChanges();
                return RedirectToAction("QLAdmin");

            }
            OneTaiKhoanResult tk = context.OneTaiKhoan(id).FirstOrDefault();

            return View(tk);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            context.DeleteTaiKhoan(id);
            return RedirectToAction("QLAdmin");


        }
        [Authorize]
        public ActionResult Create()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();

            try
            {
                if (Request.Form.Count > 0)
                {
                    String TK = Request.Form["TK"];
                    String MK = Request.Form["MK"];
                    String VT = Request.Form["vaitro"];
                    QLTK qltk = new QLTK();
                    qltk.TK = TK;
                    qltk.MK = MK;
                    qltk.VAITRO = VT;

                    context.InsertTaiKhoan(TK, MK, VT);
                    return RedirectToAction("QLAdmin");

                }
            }

            catch (SqlException e)
            {
                ViewBag.Error = "Tài khoản tồn tại."+e;
                return RedirectToAction("Index");
            }
            return View();
        }




        [Authorize]
        public ActionResult Search()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            Console.Write(Request.Form.Count.ToString());

            String Search = Request.Form["Search"];
            List<SearchTaiKhoanResult> search = context.SearchTaiKhoan(Search).ToList();

            return View(search);

        }

        

    }
}