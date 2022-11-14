using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FLICHCMUNRE.Controllers;
using FLICHCMUNRE.Models;

namespace FLICHCMUNRE.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            List<AllKhoaHocResult> KH = context.AllKhoaHoc().ToList();
            return View(KH);
        }
        public ActionResult DangKyHoc()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            try
            {
                if (Request.Form.Count > 0)
                {


                    String MaKH = Request.Form["MaKH"];
                    String MaHV = Request.Form["MaHV"];
                    String Ca = Request.Form["Ca"];

                    DateTime d = DateTime.Now;
                    DateTime NgayDK = d;
                    String AnhCK = "";


                    HttpPostedFileBase file = Request.Files["AnhCK"];
                    if (file != null && file.FileName != "")
                    {
                        String serverPath = HttpContext.Server.MapPath("~/imgs");
                        String filePath = serverPath + "/" + file.FileName;
                        file.SaveAs(filePath);
                        AnhCK = file.FileName;
                    }


                    if (MaHV.Length < 9 || MaHV.Length > 20)
                    {
                        TempData["CheckMaHV"] = "Vui lòng cung cấp MSSV hợp lệ!";
                        RedirectToAction("DangkyHoc");

                    }
                    else
                    {
                        context.InsertChiTietDangKyHoc(MaKH, MaHV, AnhCK, NgayDK);
                        context.InsertTaiKhoan(MaHV, "tnmt12345", "hocvien");

                        context.SubmitChanges();
                        TempData["Success1"] = "Đăng Ký Thành Công";

                    }


                    RedirectToAction("DangkyHoc");
                }
            }
            catch (SqlException e)
            {
                TempData["e"] = "Loi Cu Phap" + e;
                RedirectToAction("DangkyHoc");
            }



            ViewBag.Message = "Your contact page.";

            List<AllKhoaHocGocResult> AllKH = context.AllKhoaHocGoc().ToList();
            List<AllCaResult> AllCa = context.AllCa().ToList();
            List<AllHinhThucResult> AllHT = context.AllHinhThuc().ToList();
            List<AllTheLoaiResult> AllTL = context.AllTheLoai().ToList();

            ViewData["AllKH"] = AllKH;
            ViewData["AllCa"] = AllCa;
            ViewData["AllHT"] = AllHT;
            ViewData["AllTL"] = AllTL;


            return View();
        }



        public ActionResult DangKyHocNotHCMUNE()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            try
            {
                if (Request.Form.Count > 0)
                {
                    String CCCD = Request.Form["CCCD"];
                    String HoTen = Request.Form["HoTen"];
                    String Email = Request.Form["Email"];
                    String SDT = Request.Form["SDT"];
                    String NgaySinh = Request.Form["NgaySinh"];
                    String NoiSinh = Request.Form["NoiSinh"];
                    String GioiTinh = Request.Form["GioiTinh"];
                    String Lop = "";
                    String Ca = Request.Form["Ca"];
                    String MaKH = Request.Form["MaKH"];


                    DateTime d = DateTime.Now;
                    DateTime NgayDK = d;


                    String AnhCK = "";
                    HttpPostedFileBase file = Request.Files["AnhCK"];
                    if (file != null && file.FileName != "")
                    {
                        String serverPath = HttpContext.Server.MapPath("~/imgs");
                        String filePath = serverPath + "/" + file.FileName;
                        file.SaveAs(filePath);
                        AnhCK = file.FileName;
                    }
                    if (CCCD.Length == 0 && HoTen.Length == 0 && SDT.Length == 0 && NoiSinh.Length == 0)
                    {
                        TempData["CheckMaHV"] = "Vui lòng cung cấp MaHV hợp lệ!";
                        TempData["CheckCCCD"] = "Vui lòng cung cấp CCCD hợp lệ!";
                        TempData["CheckHoTen"] = "Vui lòng thông tin hợp lệ!";
                        TempData["CheckSDT"] = "Vui lòng thông tin hợp lệ!";
                        TempData["CheckNoiSinh"] = "Vui lòng thông tin hợp lệ!";

                    }
                    if (CCCD.Length < 9 || CCCD.Length > 12)
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
                        context.InsertHocVien(CCCD, HoTen, Email, CCCD, SDT, GioiTinh, NoiSinh, Convert.ToDateTime(NgaySinh), Lop);

                        context.InsertChiTietDangKyHoc(MaKH, CCCD, AnhCK, NgayDK);

                        context.InsertTaiKhoan(CCCD, "tnmt12345", "hocvien");
                        context.SubmitChanges();
                        TempData["Success1"] = "Đăng Ký Thành Công";

                    }

                    RedirectToAction("DangkyHoc");
                }
                else
                {
                    RedirectToAction("DangkyHoc");
                }
            }
            catch (SqlException e)
            {
                ViewBag.Error = "Loi Cu Phap" + e;
                RedirectToAction("DangkyHoc");
            }



         

            List<AllKhoaHocGocResult> AllKH = context.AllKhoaHocGoc().ToList();
            List<AllCaResult> AllCa = context.AllCa().ToList();
            List<AllHinhThucResult> AllHT = context.AllHinhThuc().ToList();
            List<AllTheLoaiResult> AllTL = context.AllTheLoai().ToList();

            ViewData["AllKH"] = AllKH;
            ViewData["AllCa"] = AllCa;
            ViewData["AllHT"] = AllHT;
            ViewData["AllTL"] = AllTL;


            return RedirectToAction("DangkyHoc");
        }



        public ActionResult DangKyThi()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            try
            {
                if (Request.Form.Count > 0)
                {
                    String CCCD = Request.Form["CCCD"];
                    String HoTen = Request.Form["HoTen"];
                    String Email = Request.Form["Email"];
                    String SDT = Request.Form["SDT"];
                    String NgaySinh = Request.Form["NgaySinh"];
                    String NoiSinh = Request.Form["NoiSinh"];
                    String GioiTinh = "";
                    String Lop = "";
                    String MaKH = Request.Form["MaKH"];


                    DateTime d = DateTime.Now;
                    DateTime NgayDK = d;


                    String AnhCK = "";
                    HttpPostedFileBase file = Request.Files["AnhCK"];
                    if (file != null && file.FileName != "")
                    {
                        String serverPath = HttpContext.Server.MapPath("~/imgs");
                        String filePath = serverPath + "/" + file.FileName;
                        file.SaveAs(filePath);
                        AnhCK = file.FileName;
                    }
                    if (CCCD.Length == 0 && HoTen.Length == 0 && SDT.Length == 0 && NoiSinh.Length == 0)
                    {
                        TempData["CheckMaHV"] = "Vui lòng cung cấp MaHV hợp lệ!";
                        TempData["CheckCCCD"] = "Vui lòng cung cấp CCCD hợp lệ!";
                        TempData["CheckHoTen"] = "Vui lòng thông tin hợp lệ!";
                        TempData["CheckSDT"] = "Vui lòng thông tin hợp lệ!";
                        TempData["CheckNoiSinh"] = "Vui lòng thông tin hợp lệ!";

                    }
                    if (CCCD.Length < 9 || CCCD.Length > 12)
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
                        context.InsertHocVien(CCCD, HoTen, Email, CCCD, SDT, GioiTinh, NoiSinh, Convert.ToDateTime(NgaySinh), Lop);

                        context.InsertChiTietDangKyThi(MaKH, CCCD, AnhCK, NgayDK);

                        context.InsertTaiKhoan(CCCD, "tnmt12345", "hocvien");
                        context.SubmitChanges();
                        TempData["Success1"] = "Đăng Ký Thành Công";

                    }

                    RedirectToAction("DangKyThi");
                }
                else
                {
                    RedirectToAction("DangKyThi");
                }
            }
            catch (SqlException e)
            {
                ViewBag.Error = "Loi Cu Phap" + e;
                RedirectToAction("DangKyThi");
            }

            List<AllKhoaHocGocResult> AllKH = context.AllKhoaHocGoc().ToList();

            ViewData["AllKH"] = AllKH;
            return View();
        }

        public ActionResult TraCuuDiem()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult HuongDan()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DangXuat()
        {
            ViewBag.Message = "Your contact page.";
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap", "Home");
        }
        [ValidateInput(false)]
        public ActionResult DangNhap()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            if (Request.Form.Count > 0)
            {
                String TK = Request.Form["TK"];
                String MK = Request.Form["MK"];
                //QLTK qltk = new QLTK();
                //qltk.TK = TK;
                //qltk.MK = MK;
                OneTaiKhoanLogInResult otk = context.OneTaiKhoanLogIn(TK, MK).FirstOrDefault();
                if (otk != null)
                {
                    FormsAuthentication.SetAuthCookie(TK, false);
                    if (otk.VAITRO.Equals("admin"))
                        return RedirectToAction("QLAdmin", "TaiKhoan");
                    else if (otk.VAITRO.Equals("quanly"))
                        return RedirectToAction("QLQuanLy", "TaiKhoan");
                    else if (otk.VAITRO.Equals("hocvien"))
                        return RedirectToAction("QLTKHV", "TaiKhoan");
                    else if (otk.VAITRO.Equals("giangvien"))
                        return RedirectToAction("QLTKGV", "TaiKhoan");
                }
                else
                {
                    TempData["TK"] = TK;
                    TempData["MK"] = MK;
                }


            }
            return View();
        }

        public ActionResult SearchKH()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            Console.Write(Request.Form.Count.ToString());
            List<AllKhoaHocGocResult> kh = context.AllKhoaHocGoc().ToList();


            ViewData["kh"] = kh;
            String Search = Request.Form["Search"];
            List<SearchKhoaHocResult> search = context.SearchKhoaHoc(Search).ToList();

            return View(search);

        }

    }
}