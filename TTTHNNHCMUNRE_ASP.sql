CREATE DATABASE TTTHNNHCMUNRE
GO
USE TTTHNNHCMUNRE
GO

CREATE TABLE HOCVIEN
(
	ID INT IDENTITY PRIMARY KEY,
	MAHV VARCHAR(50) UNIQUE,
	HOTEN NVARCHAR(100),
	EMAIL VARCHAR(100),
	CCCD VARCHAR(50),
	SDT VARCHAR(50),
	Pass VARCHAR(50),
	GIOITINH INT,
	NOISINH NVARCHAR(100),
	NGAYSINH DATETIME,
	LOP VARCHAR(50),
	STATUS INT

)

CREATE TABLE QLTK
(
	ID INT IDENTITY PRIMARY KEY,
	TK VARCHAR(50) UNIQUE,
	MK VARCHAR(50),
	VAITRO VARCHAR(50)
)


CREATE TABLE GIANGVIEN
(
	ID INT IDENTITY PRIMARY KEY,
	MAGV VARCHAR(50) UNIQUE,
	HOTEN NVARCHAR(100),
	EMAIL VARCHAR(100),
	CCCD VARCHAR(50),
	SDT VARCHAR(50),
	PASS VARCHAR(50),
	GIOITINH INT,
	NOISINH NVARCHAR(100),
	NGAYSINH DATETIME,
	TRINHDO VARCHAR(50),
	ISFOREIGN INT,
	STATUS INT
)

CREATE TABLE CA
(

	ID INT IDENTITY PRIMARY KEY,
	MACA VARCHAR(50) UNIQUE,
	TENCA NVARCHAR(100),

)

CREATE TABLE HINHTHUC
(
	ID INT IDENTITY PRIMARY KEY,
	MAHT VARCHAR(50) UNIQUE,
	TENHT NVARCHAR(100),

)
CREATE TABLE THELOAI
(
	ID INT IDENTITY PRIMARY KEY,
	MALOAI VARCHAR(50) UNIQUE,
	TENLOAI NVARCHAR(100),

)

CREATE TABLE THI
(

	ID INT IDENTITY PRIMARY KEY,
	MATHI VARCHAR(50) UNIQUE,
	PHONG NVARCHAR(100),
	NGAYTHI DATETIME,
	DIADIEM NVARCHAR(100),

)
CREATE TABLE KETQUA
(
	ID INT IDENTITY PRIMARY KEY,
	MAKQ VARCHAR(50) UNIQUE,
	DIEMTH FLOAT,
	DIEMLT FLOAT

)



CREATE TABLE KHOAHOC
(
	ID INT IDENTITY PRIMARY KEY,
	MAKH VARCHAR(50) UNIQUE,
	TENKH NVARCHAR(100),
	GIA float(25),
	TGBD DATETIME,
	TGKT DATETIME,
	MALOAI VARCHAR(50),
	MAHT VARCHAR(50),


	CONSTRAINT FK_KH_MALOAI FOREIGN KEY(MALOAI) REFERENCES THELOAI(MALOAI),
	CONSTRAINT FK_KH_MAHT FOREIGN KEY(MAHT) REFERENCES HINHTHUC(MAHT)
	
)
CREATE TABLE LOP
(
	ID INT IDENTITY PRIMARY KEY,
	MALOP VARCHAR(50) UNIQUE,
	TENLOP NVARCHAR(100),
	SOLUONG INT,
	SOPHONG VARCHAR(50),
	MAKH VARCHAR(50)
	constraint fl_lop_kh foreign key(MAKH) references khoahoc(MAKH)
)
go
CREATE TABLE CHITIETGIANGDAY
(
	ID INT IDENTITY PRIMARY KEY,
	MALOP VARCHAR(50),
	MAGV VARCHAR(50),
	NGAYBD DATETIME,
	NGAYKT DATETIME,
	VAITRO NVARCHAR(100),
	GHICHU NVARCHAR(250),
	STATUS INT,

	CONSTRAINT FK_CTGD_MALOP FOREIGN KEY(MALOP) REFERENCES LOP(MALOP),
	CONSTRAINT FK_CTGD_MAGV FOREIGN KEY(MAGV) REFERENCES GIANGVIEN(MAGV)
)
CREATE TABLE CHITIETKETQUA
(
	ID INT IDENTITY PRIMARY KEY,
	MAHV VARCHAR(50),
	MATHI VARCHAR(50),
	MAKQ VARCHAR(50),
	MAKH VARCHAR(50),
	STATUS INT,
	CONSTRAINT FK_CTKQ_MAHV FOREIGN KEY(MAHV) REFERENCES HOCVIEN(MAHV),
	CONSTRAINT FK_CTKQ_MAKQ FOREIGN KEY(MAKQ) REFERENCES KETQUA(MAKQ),
	CONSTRAINT FK_CTKQ_MATHI FOREIGN KEY(MATHI) REFERENCES THI(MATHI),
	CONSTRAINT FK_CTKQ_MAKH FOREIGN KEY(MAKH) REFERENCES KHOAHOC(MAKH)
)

CREATE TABLE CHITIETKHOAHOC
(
	ID INT IDENTITY PRIMARY KEY,
	MACA VARCHAR(50),
	MAKH VARCHAR(50),
	MAHV VARCHAR(50),
	STATUS INT,
	CONSTRAINT FK_CTKH_MAHV FOREIGN KEY(MAHV) REFERENCES HOCVIEN(MAHV),
	CONSTRAINT FK_CTKH_MACA FOREIGN KEY(MACA) REFERENCES CA(MACA),
	CONSTRAINT FK_CTKH_MAKH FOREIGN KEY(MAKH) REFERENCES KHOAHOC(MAKH)
)

CREATE TABLE CHITIETDANGKYHOC
(
	ID INT IDENTITY PRIMARY KEY,
	MAKH VARCHAR(50),
	MAHV VARCHAR(50),
	ANHCK VARCHAR(100),
	NGAYDK DATETIME,
	STATUS INT,

	CONSTRAINT FK_CTDKH_MAHV FOREIGN KEY(MAHV) REFERENCES HOCVIEN(MAHV),
	CONSTRAINT FK_CTDKH_MAKH FOREIGN KEY(MAKH) REFERENCES KHOAHOC(MAKH)
)

CREATE TABLE CHITIETDANGKYTHI
(
	ID INT IDENTITY PRIMARY KEY,
	MAKH VARCHAR(50),
	MAHV VARCHAR(50),
	ANHCK VARCHAR(100),
	NGAYDK DATETIME,
	STATUS INT,

	CONSTRAINT FK_CTDKT_MAHV FOREIGN KEY(MAHV) REFERENCES HOCVIEN(MAHV),
	CONSTRAINT FK_CTDKT_MAKH FOREIGN KEY(MAKH) REFERENCES KHOAHOC(MAKH)
)


----Create procedure

go
--Tài Khoản
CREATE PROC AllTaiKhoan
AS
BEGIN
	select * from QLTK
END
go

CREATE PROC SearchTaiKhoan
@SearchTK varchar(50)
AS
BEGIN
	
	select * from QLTK WHERE TK LIKE '%' + @SearchTK + '%'OR MK LIKE '%' + @SearchTK + '%'  OR VAITRO LIKE '%' + @SearchTK +'%' 
END
GO


--ONE
CREATE PROC OneTaiKhoan
@id int
AS
BEGIN
	select * from QLTK WHERE ID = @id
END
GO

CREATE PROC OneTaiKhoanPersonal
@tk varchar(50)
AS
BEGIN
	select * from QLTK WHERE TK = @tk
END
GO


CREATE PROC OneTaiKhoanDN
@tk varchar(50), @mk varchar(50)
AS
BEGIN
	declare  @count int
	select @count= count(*) from QLTK WHERE TK = @tk AND MK = @mk
	if(@count > 1)
		return 1
	else
		return 0
END
GO


CREATE PROC OneTaiKhoanLogIn
@tk varchar(50), @mk varchar(50)
AS
BEGIN
	select * from QLTK WHERE TK = @tk AND MK = @mk
END
GO



--INSERT
CREATE PROC InsertTaiKhoan
@tk varchar(50), @mk nvarchar(50), @vaitro nvarchar(50)
AS
BEGIN
	insert into QLTK(TK, MK, VaiTro) 
	values(@tk, @mk, @vaitro)
END
GO

--vd
DECLARE @tk varchar(50) = 'admin', @mk nvarchar(50) ='admin', @vaitro varchar(50) ='admin'
EXEC InsertTaiKhoan @tk, @mk, @vaitro
GO
DECLARE @tk varchar(50) = 'quanly', @mk nvarchar(50) ='quanly', @vaitro varchar(50) ='quanly'
EXEC InsertTaiKhoan @tk, @mk, @vaitro
GO

--DELETE 
CREATE PROC DeleteTaiKhoan
@id int
AS
BEGIN
	DELETE from QLTK WHERE ID = @id
END
GO



--UPDATE
CREATE PROC UpdateTaiKhoan
@id int, @tk varchar(50), @mk nvarchar(50), @vaitro nvarchar(50)
AS
BEGIN
	 UPDATE QLTK SET TK=@tk, MK=@mk, VaiTro=@vaitro WHERE ID = @id
END
GO
--DECLARE @id int =2, @tk varchar(50) = 'quanly', @mk nvarchar(50) ='quanly', @vaitro varchar(50) ='quanly'
		
--EXEC UpdateTaiKhoan @id, @tk, @mk, @vaitro



--TheLoai
CREATE PROC AllTheLoai
AS
BEGIN
	select * from THELOAI
END
GO


--ONE
CREATE PROC OneTheLoai
@matl int
AS
BEGIN
	select * from THELOAI WHERE ID = @matl
END
GO



--INSERT
CREATE PROC InsertTheLoai
@maloai varchar(50), @tenloai nvarchar(100)
AS
BEGIN
	insert into TheLoai(MaLoai, TenLoai) 
	values(@maloai, @tenloai)
END
GO

--vd
DECLARE @ma varchar(50) = 'TA', @ten nvarchar(100) =N'Tiếng Anh'
EXEC InsertTheLoai @ma, @ten
GO

--DELETE 
CREATE PROC DeleteTheLoai
@id int
AS
BEGIN
	DELETE from THELOAI WHERE ID = @id
END

GO


--UPDATE
CREATE PROC UpdateTheLoai
@id int, @maloai varchar(50), @tenloai nvarchar(100)
AS
BEGIN
	 UPDATE THELOAI SET MaLoai=@maloai, TenLoai=@tenloai WHERE ID = @id
END
GO

--DECLARE @MA INT =2, @ML VARCHAR(50) = 'TH', @TL NVARCHAR(100) = N'TIN HỌC'
		
--EXEC UpdateTheLoai @Ma, @ML, @TL


CREATE PROC SearchTheLoai
@Search nvarchar(50)
AS
BEGIN

	select * from THELOAI WHERE MaLoai LIKE '%' + @Search + '%'OR TenLoai LIKE '%' + @Search + '%'  
END
GO



-----------Ca
--ALL
CREATE PROC AllCa
AS
BEGIN
	select * from Ca
END
GO

--ONE
CREATE PROC OneCa
@maca int
AS
BEGIN
	select * from Ca WHERE ID = @maca
END

GO

--INSERT
CREATE PROC InsertCa
@maca varchar(50), @tenca nvarchar(100)
AS
BEGIN
	insert into Ca(MaCa, TenCa) 
	values(@maca, @tenca)
END
GO

--vd
DECLARE @ma varchar(50) = 'CaS1', @ten nvarchar(100) =N'Sáng Thứ 2 4 6'
EXEC InsertCa @ma, @ten
GO
DECLARE @ma varchar(50) = 'CaC1', @ten nvarchar(100) =N'Tối Thứ 2 4 6'
EXEC InsertCa @ma, @ten
GO
DECLARE @ma varchar(50) = 'CaS2', @ten nvarchar(100) =N'Sáng Thứ 3 5 7'
EXEC InsertCa @ma, @ten
GO
DECLARE @ma varchar(50) = 'CaC2', @ten nvarchar(100) =N'Tối Thứ 3 5 7'
EXEC InsertCa @ma, @ten
GO


CREATE PROC DeleteCa
@id int
AS
BEGIN
	DELETE from Ca WHERE ID = @id
END
GO

--UPDATE
CREATE PROC UpdateCa
@id int, @maca varchar(50), @tenca nvarchar(100)
AS
BEGIN
	 UPDATE Ca SET MaCa=@maca, TenCa=@tenca WHERE ID = @id
END
GO

CREATE PROC SearchCa
@Search nvarchar(50)
AS
BEGIN

	select * from Ca WHERE MaCa LIKE '%' + @Search + '%'OR TenCa LIKE '%' + @Search + '%'  
END
GO

-----------HinhThuc
--ALL
CREATE PROC AllHinhThuc
AS
BEGIN
	select * from HINHTHUC
END
go

--ONE
CREATE PROC OneHinhThuc
@maht int
AS
BEGIN
	select * from HINHTHUC WHERE ID = @maht
END
go

--INSERT
CREATE PROC InsertHinhThuc
@maht varchar(50), @tenht nvarchar(100)
AS
BEGIN
	insert into HINHTHUC(MaHT, TenHT) 
	values(@maht, @tenht)
END

--vd
DECLARE @ma varchar(50) = 'OFF', @ten nvarchar(100) =N'OFFLINE'
EXEC InsertHinhThuc @ma, @ten
go


CREATE PROC DeleteHinhThuc
@id int
AS
BEGIN
	DELETE from HINHTHUC WHERE ID = @id
END
go

--UPDATE
CREATE PROC UpdateHinhThuc
@id int, @maht varchar(50), @tenht nvarchar(100)
AS
BEGIN
	 UPDATE HINHTHUC SET MaHT=@maht, TenHT=@tenht WHERE ID = @id
END
go

CREATE PROC SearchHinhThuc
@Search varchar(50)
AS
BEGIN

	select * from HINHTHUC WHERE MaHT LIKE '%' + @Search + '%'OR TenHT LIKE '%' + @Search + '%'  
END
GO

-----------Khóa Học
--ALL
CREATE PROC AllKhoaHoc
AS
BEGIN
	select kh.id id, ht.maht maht, tenht, kh.maloai maloai, kh.makh as makh, tenkh,gia, tenloai, TGBD, TGKT from Khoahoc kh join theloai l on kh.maloai=l.maloai join hinhthuc ht on ht.maht=kh.maht;
	
END
go

CREATE PROC AllKhoaHocGoc
AS
BEGIN
	select * from Khoahoc 
	
END
go
--select * from Khoahoc kh join theloai l on kh.id=l.id join hinhthuc ht on ht.id=kh.id;
--ONE
CREATE PROC OneKhoaHoc
@id int
AS
BEGIN
	select * from KhoaHoc WHERE ID = @id
END
go



CREATE PROC OneKhoaHocCT
@id int
AS
BEGIN
	select kh.id id, ht.maht maht, tenht, kh.maloai maloai, kh.makh as makh, tenkh,gia, tenloai, TGBD, TGKT from Khoahoc kh join theloai l on kh.maloai=l.id join hinhthuc ht on ht.id=kh.maht where kh.ID=@id;
END
go
--INSERT
CREATE PROC InsertKhoaHoc
@makh varchar(50), @tenkh nvarchar(100), @gia float, @tgbd datetime, @tgkt datetime, @maloai varchar(50), @maht varchar(50)
AS
BEGIN
	insert into KHOAHOC(MaKH, TenKH, Gia, TGBD, TGKT, MALOAI, MAHT) 
	values(@makh, @tenkh, @gia, @tgbd, @tgkt, @maloai, @maht)
END
go
--vd
--DECLARE @makh varchar(50) ='KH01', @tenkh nvarchar(100)=N'Ứng dụng CNTT Cơ Bản', @gia float='1000000', @tgbd datetime ='2022/10/20', @tgkt datetime='2022/12/20', @maloai int = 2, @maht int =1
--EXEC InsertKhoaHoc @makh, @tenkh, @gia, @tgbd, @tgkt, @maloai, @maht



CREATE PROC DeleteKhoaHoc
@id int
AS
BEGIN
	DELETE from KhoaHoc WHERE ID = @id
END
go

--UPDATE
CREATE PROC UpdateKhoaHoc
@id int, @makh varchar(50), @tenkh nvarchar(100), @gia float, @tgbd datetime, @tgkt datetime, @maloai varchar(50), @maht varchar(50)
AS
BEGIN
	 UPDATE KhoaHoc SET MaKH = @makh, TenKH = @tenkh, Gia = @gia, TGBD = @tgbd, TGKT = @tgkt, MALOAI= @maloai, MAHT = @maht WHERE ID = @id
END
go

CREATE PROC SearchKhoaHoc
@Search nvarchar(50)
AS
BEGIN
	
	select * from KhoaHoc WHERE MaKH LIKE '%' + @Search + '%'OR TenKH LIKE '%' + @Search + '%'  OR Gia LIKE '%' + @Search +'%'  OR TGBD LIKE '%' + @Search +'%' 
	OR TGKT LIKE '%' + @Search +'%'   OR MaLoai LIKE '%' + @Search +'%' 
	  OR MaHT LIKE '%' + @Search +'%'
END
GO



-----------Chi Tiet Dang Ky Khoa Học
--ALL
CREATE PROC AllChiTietDangKyHoc
AS
BEGIN
	select * FROM chitietdangkyhoc ct join khoahoc kh on kh.makh=ct.makh join hocvien hv on hv.mahv = ct.mahv
	
END
go

--ONE
CREATE PROC OneChiTietDangKyHocFull
@id int
AS
BEGIN
	select * FROM chitietdangkyhoc ct join khoahoc kh on kh.makh=ct.makh join hocvien hv on hv.mahv = ct.mahv
END
go

CREATE PROC OneChiTietDangKyHoc
@id int
AS
BEGIN
	select * from ChiTietDangKyHoc WHERE ID = @id
END
go

--INSERT
CREATE PROC InsertChiTietDangKyHoc
@makh varchar(50), @mahv varchar(50), @anhck varchar(100), @ngaydk datetime
AS
BEGIN
	insert into ChiTietDangKyHoc(MaKH, MaHV, AnhCK, NgayDK, STATUS) 
	values(@makh, @mahv, @anhck, @ngaydk, 1)
END
go
--vd
--DECLARE @makh varchar(50)='', @mahv varchar(50)='', @anhck varchar(100) = 'anhck.jpg', @ngaydk datetime =getdate()
--EXEC InsertChiTietDangKyHoc @makh, @mahv, @anhck, @ngaydk



CREATE PROC DeleteChiTietDangKyHoc
@id int
AS
BEGIN
	DELETE from ChiTietDangKyHoc WHERE ID = @id
END
go
--UPDATE
CREATE PROC UpdateChiTietDangKyHoc
@id int, @makh varchar(50), @mahv varchar(50), @anhck varchar(100), @ngaydk datetime
AS
BEGIN
	 UPDATE ChiTietDangKyHoc SET MaKH = @makh, MaHV = @mahv, AnhCK = @anhck, NgayDK = @ngaydk  WHERE ID = @id
END
go

CREATE PROC SearchChiTietDangKyHoc
@Search nvarchar(50)
AS
BEGIN

	select * from ChiTietDangKyHoc WHERE MaKH LIKE '%' + @Search + '%'
	OR NgayDK LIKE '%' + @Search + '%'  OR MaHV LIKE '%' + @Search +'%'  
END
GO

CREATE PROC SearchChiTietDangKyHocFULL
@Search nvarchar(50)
AS
BEGIN

	select * from ChiTietDangKyHoc WHERE MaKH LIKE '%' + @Search + '%'
	OR NgayDK LIKE '%' + @Search + '%'  OR MaHV LIKE '%' + @Search +'%'  
END
GO
--



--select * from Khoahoc kh join theloai l on kh.id=l.id join hinhthuc ht on ht.id=kh.id;
--ONE
--CREATE PROC OneKhoaHoc
--@id int
--AS
--BEGIN
--	select * from KhoaHoc WHERE ID = @id
--END
--go

--INSERT
--CREATE PROC InsertKhoaHoc
--@makh varchar(50), @tenkh nvarchar(100), @gia float, @tgbd datetime, @tgkt datetime, @maloai int, @maht int
--AS
--BEGIN
--	insert into KHOAHOC(MaKH, TenKH, Gia, TGBD, TGKT, MALOAI, MAHT) 
--	values(@makh, @tenkh, @gia, @tgbd, @tgkt, @maloai, @maht)
--END

--vd
--DECLARE @makh varchar(50) ='KH01', @tenkh nvarchar(100)=N'Ứng dụng CNTT Cơ Bản', @gia float='1000000', @tgbd datetime ='2022/10/20', @tgkt datetime='2022/12/20', @maloai int = 2, @maht int =1
--EXEC InsertKhoaHoc @makh, @tenkh, @gia, @tgbd, @tgkt, @maloai, @maht



--CREATE PROC DeleteKhoaHoc
--@id int
--AS
--BEGIN
--	DELETE from KhoaHoc WHERE ID = @id
--END
--go

----UPDATE
--CREATE PROC UpdateKhoaHoc
--@id int, @makh varchar(50), @tenkh nvarchar(100), @gia float, @tgbd datetime, @tgkt datetime, @maloai varchar(50), @maht varchar(50)
--AS
--BEGIN
--	 UPDATE KhoaHoc SET MaKH = @makh, TenKH = @tenkh, Gia = @gia, TGBD = @tgbd, TGKT = @tgkt, MALOAI= @maloai, MAHT = @maht WHERE ID = @id
--END
--go


-----------Lớp Học
--ALL
CREATE PROC AllLopHoc
AS
BEGIN
	select * from lop
	
END
go

CREATE PROC OneLopHoc
@id int
AS
BEGIN
	select * from lop where ID=@id;
END
go
--INSERT
CREATE PROC InsertLopHoc
@malop varchar(50), @tenlop nvarchar(100), @soluong int, @sophong varchar(50)
AS
BEGIN
	insert into lop(malop, tenlop, soluong, sophong) 
	values(@malop, @tenlop, @soluong, @sophong)
END
go




CREATE PROC DeleteLopHoc
@id int
AS
BEGIN
	DELETE from lop WHERE ID = @id
END
go

--UPDATE
CREATE PROC UpdateLopHoc
@id int, @malop varchar(50), @tenlop nvarchar(100), @soluong int, @sophong varchar(50)
AS
BEGIN
	 UPDATE lop SET malop = @malop, tenlop = @tenlop, soluong = @soluong, sophong = @sophong WHERE ID = @id
END
go


-----------Lớp Học
--ALL
CREATE PROC AllLopHoc
AS
BEGIN
	select * from lop 
	
END
go

CREATE PROC OneLopHoc
@id int
AS
BEGIN
	select * from lop where ID=@id;
END
go
--INSERT
CREATE PROC InsertLopHoc
@malop varchar(50), @tenlop nvarchar(100), @soluong int, @sophong varchar(50), @makh varchar(50)
AS
BEGIN
	insert into lop(malop, tenlop, soluong, sophong, makh) 
	values(@malop, @tenlop, @soluong, @sophong, @makh)
END
go



CREATE PROC DeleteLopHoc
@id int
AS
BEGIN
	DELETE from lop WHERE ID = @id
END
go

--UPDATE
CREATE PROC UpdateLopHoc
@id int, @malop varchar(50), @tenlop nvarchar(100), @soluong int, @sophong varchar(50), @makh varchar(50)
AS
BEGIN
	 UPDATE lop SET malop = @malop, tenlop = @tenlop, soluong = @soluong, sophong = @sophong, makh=@makh WHERE ID = @id
END

go

CREATE PROC SearchLopHoc
@Search nvarchar(50)
AS
BEGIN
	select * from Lop WHERE MaLop LIKE '%' + @Search + '%'OR TenLop LIKE '%' + @Search + '%' 
	OR SoPhong LIKE '%' + @Search +'%' OR SoLuong LIKE '%' + @Search +'%' 
END
GO


-----------Học Viên
--ALL
CREATE PROC AllHocVien
AS
BEGIN
	select * from hocvien where status=1
	
END
go

CREATE PROC OneHocVien
@id int
AS
BEGIN
	select * from hocvien where ID=@id;
END
go
--INSERT
CREATE PROC InsertHocVien
@mahv varchar(50), @hoten nvarchar(100), @email varchar(100), @cccd varchar(50), 
@sdt varchar(50), @gioitinh varchar(50), @noisinh nvarchar(100),
@ngaysinh datetime, @lop varchar(50)
AS
BEGIN
	insert into hocvien(mahv, hoten, email, cccd, sdt, pass, gioitinh, noisinh, ngaysinh, lop, status) 
	values(@mahv, @hoten, @email, @cccd , @sdt,'tnmt12345', @gioitinh, @noisinh, @ngaysinh, @lop,1 )
END
go




CREATE PROC DeleteHocVien
@id int
AS
BEGIN
	UPDATE hocvien SET status= 0 WHERE ID = @id
END
go

--UPDATE
CREATE PROC UpdateHocVien
@id int, @mahv varchar(50), @hoten nvarchar(100), @email varchar(100), @cccd varchar(50), @sdt varchar(50), @gioitinh varchar(50), @noisinh nvarchar(100),
@ngaysinh datetime, @lop varchar(50)
AS
BEGIN
	 UPDATE hocvien SET mahv = @mahv, hoten = @hoten, email = @email, cccd = @cccd, sdt = @sdt , gioitinh= @gioitinh,
	 noisinh= @noisinh, ngaysinh= @ngaysinh, lop = @lop WHERE ID = @id
END
go

CREATE PROC SearchHocVien
@Search nvarchar(50)
AS
BEGIN

	select * from HocVien WHERE HoTen LIKE '%' + @Search + '%'OR MaHV LIKE '%' + @Search + '%'  
	OR SDT LIKE '%' + @Search +'%'  OR Email LIKE '%' + @Search +'%'  
	OR CCCD LIKE '%' + @Search +'%'   OR NoiSinh LIKE '%' + @Search +'%' 
	  
END
GO



-----------Giang Viên
--ALL
CREATE PROC AllGiangVien
AS
BEGIN
	select * from GiangVien where status=1
	
END
go

CREATE PROC OneGiangVien
@id int
AS
BEGIN
	select * from GiangVien where ID=@id;
END
go
--INSERT
CREATE PROC InsertGiangVien
@magv varchar(50), @hoten nvarchar(100), @email varchar(100), @cccd varchar(50), @sdt varchar(50), @gioitinh varchar(50), @noisinh nvarchar(100),
@ngaysinh datetime, @trinhdo varchar(50), @isforeign int
AS
BEGIN
	insert into giangvien(magv, hoten, email, cccd, sdt, pass, gioitinh, noisinh, ngaysinh, trinhdo, isforeign, status) 
	values(@magv, @hoten, @email, @cccd , @sdt,'hcmunre', @gioitinh, @noisinh, @ngaysinh, @trinhdo, @isforeign, 1 )
END
go


CREATE PROC DeleteGiangVien
@id int
AS
BEGIN
	UPDATE giangvien SET status= 0 WHERE ID = @id
END
go

--UPDATE
CREATE PROC UpdateGiangVien
@id int,@magv varchar(50), @hoten nvarchar(100), @email varchar(100), @cccd varchar(50), @sdt varchar(50), @gioitinh varchar(50), @noisinh nvarchar(100),
@ngaysinh datetime, @trinhdo varchar(50), @isforeign int
AS
BEGIN
	 UPDATE giangvien SET magv = @magv, hoten = @hoten, email = @email, cccd = @cccd, sdt = @sdt , gioitinh= @gioitinh,
	 noisinh= @noisinh, ngaysinh= @ngaysinh, trinhdo = @trinhdo, isforeign = @isforeign WHERE ID = @id
END
go

CREATE PROC SearchGiangVien
@Search nvarchar(50)
AS
BEGIN

	select * from GiangVien WHERE HoTen LIKE '%' + @Search + '%'OR MaGV LIKE '%' + @Search + '%'  OR SDT LIKE '%' + @Search +'%'  OR Email LIKE '%' + @Search +'%'  OR CCCD LIKE '%' + @Search +'%'   OR NoiSinh LIKE '%' + @Search +'%' 
	  OR TrinhDo LIKE '%' + @Search +'%'
END
GO