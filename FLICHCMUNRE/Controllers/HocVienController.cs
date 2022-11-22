using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FLICHCMUNRE.Models;
using ClosedXML.Excel;
using System.IO;

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
        [Authorize]
        public ActionResult Details(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            OneHocVienResult KH = context.OneHocVien(id).FirstOrDefault();


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

        [Authorize]
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
        [Authorize]
        public ActionResult EditPersonal(int id)
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
                return RedirectToAction("TinhTrangDangKyHoc","HocVien");

            }
            OneHocVienResult kh = context.OneHocVien(id).FirstOrDefault();

            return View(kh);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            context.DeleteHocVien(id);
            return RedirectToAction("Index");


        }
        [Authorize]
        public ActionResult Search()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            Console.Write(Request.Form.Count.ToString());

            String Search = Request.Form["Search"];
            List<SearchHocVienResult> all = context.SearchHocVien(Search).ToList();


            return View(all);


        }
        [Authorize]
        public ActionResult ExportToCSV()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            List<AllHocVienResult> all = context.AllHocVien().ToList();

            var builder = new StringBuilder();
            builder.AppendLine("MSSX,Họ Tên, CCCD, EMAIL, Số Điện Thoại, Ngày Sinh, Nơi Sinh, Lớp, Giới Tính");
            foreach (var hv in all)
            {
                builder.AppendLine($"{hv.MAHV}, {hv.HOTEN}, {hv.CCCD}, {hv.EMAIL}, {hv.SDT}, {hv.NGAYSINH}, {hv.NOISINH}, {hv.LOP}, {hv.GIOITINH}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text / csv", "students.csv");
            

        }
        [Authorize]
        public ActionResult ExportToExcel()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            List<AllHocVienResult> all = context.AllHocVien().ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "MSSV";
                worksheet.Cell(currentRow, 2).Value = "Họ Tên";
                worksheet.Cell(currentRow, 3).Value = "CCCD";
                worksheet.Cell(currentRow, 4).Value = "EMAIL";
                worksheet.Cell(currentRow, 5).Value = "Số Điện Thoại";
                worksheet.Cell(currentRow, 6).Value = "Ngày Sinh";
                worksheet.Cell(currentRow, 7).Value = "Nơi Sinh";
                worksheet.Cell(currentRow, 8).Value = "Lớp";
                worksheet.Cell(currentRow, 9).Value = "Giới Tính";
                foreach (var hv in all)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = hv.MAHV;
                    worksheet.Cell(currentRow, 2).Value = hv.HOTEN;
                    worksheet.Cell(currentRow, 3).Value = hv.CCCD;
                    worksheet.Cell(currentRow, 4).Value = hv.EMAIL;
                    worksheet.Cell(currentRow, 5).Value = hv.SDT;
                    worksheet.Cell(currentRow, 6).Value = hv.NGAYSINH;
                    worksheet.Cell(currentRow, 7).Value = hv.NOISINH;
                    worksheet.Cell(currentRow, 8).Value = hv.LOP;
                    worksheet.Cell(currentRow, 9).Value = hv.GIOITINH;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "students.xlsx");
                }
            }



        }
        [Authorize]
        public ActionResult TinhTrangDangKyHoc()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            TempData["DanhSachDangKy"]  = context.DanhSachDaDangKyKhoaHoc(User.Identity.Name).ToList();
            HocVienInfoResult hv = context.HocVienInfo(User.Identity.Name).FirstOrDefault();
            return View(hv);
        }
        [Authorize]
        public ActionResult TinhTrangDangKyThi()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            TempData["DanhSachDangKyThi"] = context.DanhSachDaDangKyKhoaThi(User.Identity.Name).ToList();
            HocVienInfoResult hv = context.HocVienInfo(User.Identity.Name).FirstOrDefault();
            return View(hv);
        }

    }
}