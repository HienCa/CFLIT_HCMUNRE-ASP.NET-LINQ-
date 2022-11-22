using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FLICHCMUNRE.Models;
namespace FLICHCMUNRE.Controllers
{
    [Authorize]
    public class LopHocController : Controller
    {
        // GET: LopHoc
        [Authorize]
        public ActionResult Index()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            List<AllLopHocResult> lophoc = context.AllLopHoc().ToList();
            ViewData["allKH"] = context.AllKhoaHoc().ToList();
            return View(lophoc);
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            OneLopHocResult lh = context.OneLopHoc(id).FirstOrDefault();


            return View(lh);
        }
        [Authorize]
        public ActionResult Create()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            try
            {
                if (Request.Form.Count > 0)
                {
                    String MaLop = Request.Form["MaLop"];
                    String TenLop = Request.Form["TenLop"];
                    String SoLuong = Request.Form["SoLuong"];
                    String SoPhong = Request.Form["SoPhong"];
                    String MaKH = Request.Form["MaKH"];

                    context.InsertLopHoc(MaLop, TenLop, Convert.ToInt32(SoLuong), SoPhong, MaKH);
                    return RedirectToAction("Index");

                }
            }

            catch (SqlException e)
            {
                ViewBag.Error = "Mã đã tồn tại, vui lòng chọn mã khác."+e;
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

                String MaLoai = Request.Form["MaLop"];
                String TenLoai = Request.Form["Tenlop"];
                String SoLuong = Request.Form["SoLuong"];
                String SoPhong = Request.Form["SoPhong"];
                String MaKH = Request.Form["MaKH"];

                context.UpdateLopHoc(id, MaLoai, TenLoai, Convert.ToInt32(SoLuong), SoPhong, MaKH);
                context.SubmitChanges();
                return RedirectToAction("Index");

            }
            OneLopHocResult tk = context.OneLopHoc(id).FirstOrDefault();
            TempData["allKH"] = context.AllKhoaHoc().ToList();
            return View(tk);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            context.DeleteLopHoc(id);
            return RedirectToAction("Index");


        }
        [Authorize]
        public ActionResult Search()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            Console.Write(Request.Form.Count.ToString());
            ViewData["allKH"] = context.AllKhoaHoc().ToList();
            String Search = Request.Form["Search"];
            List<SearchLopHocResult> search = context.SearchLopHoc(Search).ToList();

            return View(search);

        }
    }
}