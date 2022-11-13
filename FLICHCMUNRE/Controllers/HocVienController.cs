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
    public class HocVienController : Controller
    {
        // GET: HocVien
        [Authorize]
        public ActionResult Index()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            List<AllHocVienResult> HV = context.AllHocVien().ToList();

            return View(HV);
        }
        public ActionResult Details(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            OneHocVienResult KH = context.OneHocVien(id).FirstOrDefault();


            return View(KH);
        }
        public ActionResult Create()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            try
            {
                if (Request.Form.Count > 0)
                {
                    String MaHV = Request.Form["MaHV"];
                    String CCCD = Request.Form["CCCD"];
                    String HoTen = Request.Form["HoTen"];
                    String Email = Request.Form["Email"];
                    String SDT = Request.Form["SDT"];
                    String NgaySinh = Request.Form["NgaySinh"];
                    String NoiSinh = Request.Form["NoiSinh"];
                    String GioiTinh = Request.Form["GioiTinh"];
                    String Lop = Request.Form["Lop"];

                    if (MaHV.Length ==0 && CCCD.Length == 0 && HoTen.Length == 0 && SDT.Length == 0 && NoiSinh.Length == 0)
                    {
                        TempData["CheckMaHV"] = "Vui lòng cung cấp MaHV hợp lệ!";
                        TempData["CheckCCCD"] = "Vui lòng cung cấp CCCD hợp lệ!";
                        TempData["CheckHoTen"] = "Vui lòng thông tin hợp lệ!";
                        TempData["CheckSDT"] = "Vui lòng thông tin hợp lệ!"; 
                        TempData["CheckNoiSinh"] = "Vui lòng thông tin hợp lệ!";

                    }
                    else if (MaHV.Length < 8 || MaHV.Length > 20)
                    {
                        TempData["CheckMaHV"] = "Vui lòng cung cấp MaHV hợp lệ!";

                    }
                    else if (CCCD.Length < 9 || CCCD.Length > 12)
                    {
                        TempData["CheckCCCD"] = "Vui lòng cung cấp CCCD hợp lệ!";

                    }
                    else if (HoTen.Length < 4 || HoTen.Length > 200)
                    {
                        TempData["CheckHoTen"] = "Vui lòng thông tin hợp lệ!";

                    }
                    else if (SDT.Length < 9 || HoTen.Length > 50)
                    {
                        TempData["CheckSDT"] = "Vui lòng thông tin hợp lệ!";

                    }
                    else if (NoiSinh.Length < 4 || NoiSinh.Length > 200)
                    {
                        TempData["CheckNoiSinh"] = "Vui lòng thông tin hợp lệ!";

                    }
                    else
                    {
                        context.InsertHocVien(MaHV, HoTen, Email, CCCD, SDT, GioiTinh, NoiSinh, Convert.ToDateTime(NgaySinh), Lop);
                        context.InsertTaiKhoan(MaHV, "tnmt12345", "hocvien");
                    }
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


        public ActionResult Edit(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            if (Request.Form.Count > 0)
            {

                String MaHV = Request.Form["MaHV"];
                String CCCD = Request.Form["CCCD"];
                String HoTen = Request.Form["HoTen"];
                String Email = Request.Form["Email"];
                String SDT = Request.Form["SDT"];
                String NgaySinh = Request.Form["NgaySinh"];
                String NoiSinh = Request.Form["NoiSinh"];
                String GioiTinh = Request.Form["GioiTinh"];
                String Lop = Request.Form["Lop"];

                if (MaHV.Length == 0 && CCCD.Length == 0 && HoTen.Length == 0 && SDT.Length == 0 && NoiSinh.Length == 0)
                {
                    TempData["CheckMaHV"] = "Vui lòng cung cấp MaHV hợp lệ!";
                    TempData["CheckCCCD"] = "Vui lòng cung cấp CCCD hợp lệ!";
                    TempData["CheckHoTen"] = "Vui lòng thông tin hợp lệ!";
                    TempData["CheckSDT"] = "Vui lòng thông tin hợp lệ!";
                    TempData["CheckNoiSinh"] = "Vui lòng thông tin hợp lệ!";

                }
                else if (MaHV.Length < 8 || MaHV.Length > 20)
                {
                    ViewBag["CheckMaHV"] = "Vui lòng cung cấp MaHV hợp lệ!";

                }
                else if (CCCD.Length < 9 || CCCD.Length > 12)
                {
                    ViewBag["CheckCCCD"] = "Vui lòng cung cấp CCCD hợp lệ!";

                }
                else if (HoTen.Length < 4 || HoTen.Length > 200)
                {
                    ViewBag["CheckHoTen"] = "Vui lòng thông tin hợp lệ!";

                }
                else if (SDT.Length < 9 || HoTen.Length > 50)
                {
                    ViewBag["CheckSDT"] = "Vui lòng thông tin hợp lệ!";

                }
                else if (NoiSinh.Length < 4 || NoiSinh.Length > 200)
                {
                    ViewBag["CheckNoiSinh"] = "Vui lòng thông tin hợp lệ!";

                }
                else
                {
                    context.UpdateHocVien(id, MaHV, HoTen, Email, CCCD, SDT, GioiTinh, NoiSinh, Convert.ToDateTime(NgaySinh), Lop);

                    context.SubmitChanges();
                }
                return RedirectToAction("Index");

            }
            OneHocVienResult kh = context.OneHocVien(id).FirstOrDefault();

            return View(kh);
        }
        public ActionResult Delete(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            context.DeleteHocVien(id);
            return RedirectToAction("Index");


        }

        public ActionResult Search()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            Console.Write(Request.Form.Count.ToString());

            String Search = Request.Form["Search"];
            List<SearchHocVienResult> all = context.SearchHocVien(Search).ToList();


            return View(all);


        }
    }
}