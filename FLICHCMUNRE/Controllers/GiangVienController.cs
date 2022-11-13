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
    public class GiangVienController : Controller
    {
        // GET: GiangVien
        [Authorize]
        public ActionResult Index()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            List<AllGiangVienResult> GV = context.AllGiangVien().ToList();
            return View(GV);
        }

        public ActionResult Details(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            OneGiangVienResult KH = context.OneGiangVien(id).FirstOrDefault();


            return View(KH);
        }
        public ActionResult Create()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            try
            {
                if (Request.Form.Count > 0)
                {
                    String MaGV = Request.Form["MaGV"];
                    String CCCD = Request.Form["CCCD"];
                    String HoTen = Request.Form["HoTen"];
                    String Email = Request.Form["Email"];
                    String SDT = Request.Form["SDT"];
                    String NgaySinh = Request.Form["NgaySinh"];
                    String NoiSinh = Request.Form["NoiSinh"];
                    String GioiTinh = Request.Form["GioiTinh"];
                    String TrinhDo = Request.Form["TrinhDo"];
                    String Isforeign = Request.Form["Isforeign"];

                    if (MaGV.Length == 0 && CCCD.Length == 0 && HoTen.Length == 0 && SDT.Length == 0 && NoiSinh.Length == 0 && TrinhDo.Length == 0)
                    {
                        TempData["CheckMaHV"] = "Vui lòng cung cấp MaHV hợp lệ!";
                        TempData["CheckCCCD"] = "Vui lòng cung cấp CCCD hợp lệ!";
                        TempData["CheckHoTen"] = "Vui lòng thông tin hợp lệ!";
                        TempData["CheckSDT"] = "Vui lòng thông tin hợp lệ!";
                        TempData["CheckTrinhDo"] = "Vui lòng thông tin hợp lệ!";
                        TempData["CheckNoiSinh"] = "Vui lòng thông tin hợp lệ!";

                    }
                    else if (MaGV.Length < 8 || MaGV.Length > 20)
                    {
                        TempData["CheckMaGV"] = "Vui lòng cung cấp MaGV hợp lệ!";

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
                    else if (TrinhDo.Length < 4 || TrinhDo.Length > 200)
                    {
                        TempData["CheckTrinhDo"] = "Vui lòng thông tin hợp lệ!";

                    }
                    else
                    {
                        context.InsertGiangVien(MaGV, HoTen, Email, CCCD, SDT, GioiTinh, NoiSinh, Convert.ToDateTime(NgaySinh), TrinhDo, Convert.ToInt32(Isforeign));

                        context.InsertTaiKhoan(MaGV, "hcmunre", "giangvien");
                    }
                    return RedirectToAction("Index");

                }
            }
            catch (SqlException e)
            {
                TempData["e"] = e;
                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult Edit(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            if (Request.Form.Count > 0)
            {

                String MaGV = Request.Form["MaGV"];
                String CCCD = Request.Form["CCCD"];
                String HoTen = Request.Form["HoTen"];
                String Email = Request.Form["Email"];
                String SDT = Request.Form["SDT"];
                String NgaySinh = Request.Form["NgaySinh"];
                String NoiSinh = Request.Form["NoiSinh"];
                String GioiTinh = Request.Form["GioiTinh"];
                String TrinhDo = Request.Form["TrinhDo"];
                String Isforeign = Request.Form["Isforeign"];

                if (MaGV.Length < 8 || MaGV.Length > 20)
                {
                    ViewBag["CheckMaGV"] = "Vui lòng cung cấp MaGV hợp lệ!";

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
                else if (TrinhDo.Length < 4 || TrinhDo.Length > 200)
                {
                    ViewBag["CheckTrinhDo"] = "Vui lòng thông tin hợp lệ!";

                }
                else
                {
                    context.UpdateGiangVien(id, MaGV, HoTen, Email, CCCD, SDT, GioiTinh, NoiSinh, Convert.ToDateTime(NgaySinh), TrinhDo, Convert.ToInt32(Isforeign));

                    context.SubmitChanges();
                }
                return RedirectToAction("Index");

            }
            OneGiangVienResult gv = context.OneGiangVien(id).FirstOrDefault();

            return View(gv);
        }
        public ActionResult Delete(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            context.DeleteGiangVien(id);
            return RedirectToAction("Index");


        }

        public ActionResult Search()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            Console.Write(Request.Form.Count.ToString());
           
                String Search = Request.Form["Search"];
                List<SearchGiangVienResult> all = context.SearchGiangVien(Search).ToList();


 

            return View(all);


        }
    }
}