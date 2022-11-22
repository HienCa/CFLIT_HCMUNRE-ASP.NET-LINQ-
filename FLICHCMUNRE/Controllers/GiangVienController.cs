using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
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
        [Authorize]
        public ActionResult Details(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            OneGiangVienResult KH = context.OneGiangVien(id).FirstOrDefault();


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

        [Authorize]
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
        [Authorize]
        public ActionResult Delete(int id)
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            context.DeleteGiangVien(id);
            return RedirectToAction("Index");


        }
        [Authorize]
        public ActionResult Search()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            Console.Write(Request.Form.Count.ToString());
           
                String Search = Request.Form["Search"];
                List<SearchGiangVienResult> all = context.SearchGiangVien(Search).ToList();


 

            return View(all);


        }
        [Authorize]
        public ActionResult ExportToCSV()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            List<AllGiangVienResult> all = context.AllGiangVien().ToList();

            var builder = new StringBuilder();
            builder.AppendLine("MSSX,Họ Tên, CCCD, EMAIL, Số Điện Thoại, Ngày Sinh, Nơi Sinh, Trình Độ, Giới Tính");
            foreach (var gv in all)
            {
                builder.AppendLine($"{gv.MAGV}, {gv.HOTEN}, {gv.CCCD}, {gv.EMAIL}, {gv.SDT}, {gv.NGAYSINH}, {gv.NOISINH}, {gv.TRINHDO}, {gv.GIOITINH}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text / csv", "lecturers.csv");


        }
        [Authorize]
        public ActionResult ExportToExcel()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            List<AllGiangVienResult> all = context.AllGiangVien().ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "MaGV";
                worksheet.Cell(currentRow, 2).Value = "Họ Tên";
                worksheet.Cell(currentRow, 3).Value = "CCCD";
                worksheet.Cell(currentRow, 4).Value = "EMAIL";
                worksheet.Cell(currentRow, 5).Value = "Số Điện Thoại";
                worksheet.Cell(currentRow, 6).Value = "Ngày Sinh";
                worksheet.Cell(currentRow, 7).Value = "Nơi Sinh";
                worksheet.Cell(currentRow, 8).Value = "Trình Độ";
                worksheet.Cell(currentRow, 9).Value = "Giới Tính";
                foreach (var gv in all)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = gv.MAGV;
                    worksheet.Cell(currentRow, 2).Value = gv.HOTEN;
                    worksheet.Cell(currentRow, 3).Value = gv.CCCD;
                    worksheet.Cell(currentRow, 4).Value = gv.EMAIL;
                    worksheet.Cell(currentRow, 5).Value = gv.SDT;
                    worksheet.Cell(currentRow, 6).Value = gv.NGAYSINH;
                    worksheet.Cell(currentRow, 7).Value = gv.NOISINH;
                    worksheet.Cell(currentRow, 8).Value = gv.TRINHDO;
                    worksheet.Cell(currentRow, 9).Value = gv.GIOITINH;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "lecturers.xlsx");
                }
            }



        }
        [Authorize]
        public ActionResult NhapDiem()
        {
            HCMUNREDataContext context = new HCMUNREDataContext();
            ViewBag.Message = "Your contact page.";
            List<AllGiangVienResult> GV = context.AllGiangVien().ToList();
            return View();
        }
    }
}