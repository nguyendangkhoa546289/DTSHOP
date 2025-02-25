Create database CuaHangNoiThat
go
use CuaHangNoiThat
go

/*TÀI KHOẢN*/
Create table TaiKhoan
(MaNguoiDung int IDENTITY(1,1) NOT NULL,
	HoTen nvarchar(50) NULL,
	Email varchar(50) NULL,
	Dienthoai varchar(50) NULL,
	Matkhau varchar(50) NULL,
	IDQuyen int NULL,
	Diachi nvarchar(255) NULL,
	primary key(MaNguoiDung));

/*PHÂN QUYỀN*/
Create table PhanQuyen
(IDQuyen int IDENTITY(1,1) NOT NULL,
	TenQuyen nvarchar(20) NULL,
	primary key(IDQuyen));


/*LOẠI HÀNG*/
Create table LoaiHang
(MaLoai int IDENTITY(1,1) NOT NULL,
	TenLoai nvarchar(100) DEFAULT NULL,
	Primary key(MaLoai));

/*SẢN PHẨM*/
CREATE TABLE SanPham(
	MaSP int IDENTITY(1,1) NOT NULL,
	TenSP nvarchar (100) NULL,
	GiaBan decimal(18,0) NULL,	
	Soluong int NULL,
	MoTa ntext NULL,
	MaLoai int NULL,
	AnhSP nvarchar(100) NULL,
	Primary key(MaSP));


/*ĐƠN HÀNG*/
CREATE TABLE DonHang(
	Madon int IDENTITY(1,1) NOT NULL,	
	NgayDat  datetime NULL,
	TinhTrang  int NULL,
	ThanhToan int NULL,
	DiaChiNhanHang Nvarchar(100) NULL,
	MaNguoiDung int NULL,
	TongTien decimal(18,0),
	Primary key(Madon));

/*CT ĐƠN HÀNG*/
CREATE TABLE ChiTietDonHang(
	CTMaDon int IDENTITY(1,1) NOT NULL,
	MaDon int NOT NULL,
	MaSP int NOT NULL,
	SoLuong int NULL,
	DonGia decimal(18,0) NULL,
	ThanhTien decimal(18,0)  NULL,
	PhuongThucThanhToan int Null,
	Primary key(CTMaDon));

CREATE TABLE TinTuc(
	MaTT int IDENTITY(1,1) NOT NULL,
	TieuDe nvarchar(100),
	NoiDung ntext,
	Primary key(MaTT));



/* TÀI KHOẢN -> PHÂN QUYỀN */
ALTER TABLE TaiKhoan
ADD CONSTRAINT FK_tk_pq FOREIGN KEY (IDQuyen) REFERENCES PhanQuyen(IDQuyen);

/* SẢN PHẨM -> LOẠI HÀNG */
ALTER TABLE SanPham
ADD CONSTRAINT FK_sp_loai FOREIGN KEY (MaLoai) REFERENCES LoaiHang(MaLoai) ON DELETE CASCADE;

/* ĐƠN HÀNG -> TÀI KHOẢN */
ALTER TABLE DonHang
ADD CONSTRAINT FK_hd_tk FOREIGN KEY (MaNguoiDung) REFERENCES TaiKhoan(MaNguoiDung) ON DELETE SET NULL;

/* CHI TIẾT ĐƠN HÀNG -> ĐƠN HÀNG */
ALTER TABLE ChiTietDonHang
ADD CONSTRAINT FK_cthd_hd FOREIGN KEY (MaDon) REFERENCES DonHang(MaDon) ON DELETE CASCADE;

/* CHI TIẾT ĐƠN HÀNG -> SẢN PHẨM */
ALTER TABLE ChiTietDonHang
ADD CONSTRAINT FK_cthd_sp FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP) ON DELETE CASCADE;

/*Phân quyền*/
insert into PhanQuyen values ('Adminstrator');
insert into PhanQuyen values ('Member');

/*Tài khoảng*/
insert into TaiKhoan values (N'Nguyễn Đăng Khoa','Admin@gmail.com','0962391074','123456',1,N'Đồng Nai');
insert into TaiKhoan values (N'Nguyễn Thành Đạt','nguyenthanhdat@gmail.com','0398929108','123456!',2,N'Vũng Tàu');

/*Loại hàng*/
insert into LoaiHang values (N'Nội thất phòng khách');
insert into LoaiHang values (N'Nội thất phòng ngủ');
insert into LoaiHang values (N'Nội thất phòng ăn');


/*Sản phẩm*/

INSERT INTO SanPham VALUES (N'Trọn bộ phòng khách Moretti', 1700000, 50, N'Với chiếc sofa góc lớn màu xanh nhẹ làm điểm nhấn, phòng khách Moretti mang đến cảm giác thư giãn và dễ chịu ngay từ cái nhìn đầu tiên.', 1, N'\Images\PhongKhach\Trọn bộ phòng khách Moretti.jpg');
INSERT INTO SanPham VALUES (N'Phòng khách hiện đại – Cảm hứng mùa thu tinh tế', 1550000, 50, N'Phòng khách hiện đại với những tông gỗ ấm, chất liệu da và nỉ mềm mại từ Nhà Xinh trở thành nơi lý tưởng để quây quần, tận hưởng.', 1, N'\Images\PhongKhach\Phòng khách hiện đại – Cảm hứng mùa thu tinh tế.jpg');
INSERT INTO SanPham VALUES (N'Jazz – Phòng khách phóng khoáng và tinh tế', 1600000, 50, N'Được thiết kế theo phong cách Industrial, sofa Jazz phù hợp cho nhiều loại căn hộ, mang đậm chất thu với tông gỗ Sồi ấm áp và chất liệu da cao cấp.', 1, N'\Images\PhongKhach\Jazz – Phòng khách phóng khoáng và tinh tế.jpg');
INSERT INTO SanPham VALUES (N'Bridge – Cầu nối của sự ấm áp và hiện đại', 1800000, 50, N'Bridge kết hợp khung gỗ oak vững chãi với thiết kế tinh tế, gợi lên không khí mùa thu dịu dàng.', 1, N'\Images\PhongKhach\Bridge – Cầu nối của sự ấm áp và hiện đại.jpg');

INSERT INTO SanPham VALUES (N'Phòng ngủ PIO – Lựa chọn phù hợp cho không gian hiện đại, tối giản', 1750000, 50, N'Gỗ dẻ gai là một chất liệu quen thuộc, chất gỗ cứng, chắc chắn nên thường được sử dụng rộng rãi trong sản xuất đồ nội thất, bao gồm cả giường ngủ.', 2, N'\Images\PhongNgu\PHÒNG NGỦ PIO – LỰA CHỌN PHÙ HỢP CHO KHÔNG GIAN HIỆN ĐẠI, TỐI GIẢN.jpg');
INSERT INTO SanPham VALUES (N'Bồng bềnh trên sóng cùng giường COASTAL', 1650000, 50, N'Tông màu xanh thanh mát mang sắc biển lan tỏa khắp không gian phòng ngủ.', 2, N'\Images\PhongNgu\BỒNG BỀNH TRÊN SÓNG CÙNG GIƯỜNG COASTAL.jpg');
INSERT INTO SanPham VALUES (N'Phòng ngủ thoáng mát với Dubai', 1700000, 50, N'Không khí có phần oi bức cùng những cơn mưa mùa hè đôi lúc sẽ khiến bạn cảm thấy khó đi vào giấc ngủ.', 2, N'\Images\PhongNgu\Phòng ngủ thoáng mát với Dubai.jpg');
INSERT INTO SanPham VALUES (N'Phòng ngủ mây – Vẻ đẹp bền vững từ chất liệu thân quen', 1600000, 50, N'Một trong những niềm tự hào của Nhà Xinh chính là bộ sưu tập Mây.', 2, N'\Images\PhongNgu\PHÒNG NGỦ MÂY – VẺ ĐẸP BỀN VỮNG TỪ CHẤT LIỆU THÂN QUEN.jpg');

INSERT INTO SanPham VALUES (N'Phòng ăn hiện đại, nổi bật với thiết kế độc đáo từ bàn ăn Moretti', 1850000, 50, N'Phòng ăn hiện đại, mới mẻ sẽ mang lại nhiều cảm xúc và nguồn năng lượng mới cho mỗi bữa ăn.', 3, N'\Images\PhongAn\Phòng ăn hiện đại, nổi bật với thiết kế độc đáo từ bàn ăn Moretti.jpg');
INSERT INTO SanPham VALUES (N'Tái tạo phòng ăn với BST JAZZ hiện đại', 1500000, 50, N'Bàn ăn Jazz với bề mặt gỗ sồi tự nhiên được gia công tỉ mỉ.', 3, N'\Images\PhongAn\TÁI TẠO PHÒNG ĂN VỚI BST JAZZ HIỆN ĐẠI.jpg');
INSERT INTO SanPham VALUES (N'Phòng ăn sang trọng với bàn mở rộng DELTA', 1750000, 50, N'Trái ngược với những không gian nhỏ hẹp, không gian rộng rãi có quá nhiều khoảng trống.', 3, N'\Images\PhongAn\PHÒNG ĂN SANG TRỌNG VỚI BÀN MỞ RỘNG DELTA .jpg');
INSERT INTO SanPham VALUES (N'Tạo lập phòng ăn hiện đại tinh tế', 1650000, 50, N'Màu sắc ấm áp, mang đậm nét Bắc Âu với tông màu gỗ trầm thu hút.', 3, N'\Images\PhongAn\TẠO LẬP PHÒNG ĂN HIỆN ĐẠI, TINH TẾ.jpg');
/*ĐƠN HÀNG*/
insert into DonHang values ('2023-02-07',1,1,N'Đại Học VLUTE',2,27000);
insert into DonHang values ('2023-02-07',NULL,1,N'Vũng Liêm',2,18000);

/*CT_Hóa đơn*/
insert into ChiTietDonHang values (1,2,1,5000,5000,1);
insert into ChiTietDonHang values (1,3,1,22000,22000,1);
insert into ChiTietDonHang values (2,1,1,18000,18000,1);

/*Tin Tức*/
insert into TinTuc values (N'Nội thất mới!',N'Nơi nội thất nổi bật lên không gian sống của bạn!');
insert into TinTuc values (N'Nội thất mới2!',N'Nơi nội thất nổi bật lên không gian sống của bạn2!');



select * from PhanQuyen;
select * from TaiKhoan; 
select * from LoaiHang where TenLoai <> 'Thùng rác';

--select * from SanPham order by MaSP DESC where MaLoai='2';

select * from DonHang;
select * 
from DonHang,ChiTietDonHang 
where DonHang.MaDon=ChiTietDonHang.MaDon and DonHang.MaDon = 1 

select * from TinTuc

/*
select * from khachhang where SDT <> '0'

Hàm kiểm tra tồn tại
IF EXISTS (SELECT * FROM ct_phieuxuat Where MaSP = 'EX') 
BEGIN
	Update ct_kho SET Soluong = 
	 ((select 'Soluongnhap'=Sum(SoLuong) from ct_phieunhap where MaSP = 'EX') - 
	 (select 'Soluongxuat' =Sum(SoLuong) from ct_phieuxuat where MaSP = 'EX')) 
	 Where MaSP = 'EX'
END
ELSE 
	 Update ct_kho SET Soluong = (select 'Soluongnhap'=Sum(SoLuong) from ct_phieunhap where MaSP = 'EX') Where MaSP = 'EX'


-- Hàm kiểm tra tồn tại
IF EXISTS (SELECT * FROM ct_phieunhap Where MaPN = 4 AND MaSP = 'VS') 
BEGIN
	PRINT 'DA TON TAI'
END
ELSE INSERT INTO ct_phieunhap(MaPN,MaSP,Soluong,DonGiaNhap) VALUES ('4','VS','20','3000000')


Update ct_kho SET 

Soluong=
(select Sum(SoLuong) as 'Soluongnhap'
from ct_phieunhap
where MaSP = 'EX') 
-
(select Sum(SoLuong) as 'Soluongxuat'
from ct_phieuxuat
where MaSP = 'EX') 
from ct_kho
Where MaSP = 'EX'

Select Soluong From ct_kho Where MaSP = 'EX' 

delete from kho where MaKho = '1'	

select * from ct_hoadon where MaHoaDon = '1'

select SUM(TongTien) as 'TongTien'
from hoadon 
where Ngay between '2021-02-07' and '2021-02-09' 

SELECT 'TongTien'=SUM(TongTien) FROM hoadon WHERE Ngay between '2021-02-07' and '2021-02-07'

select TenSP,DonGia,'SoLuong'=SUM(SoLuong),'TongTien'=(DonGia * SUM(SoLuong))
from ct_hoadon,hoadon,sanpham  
where ct_hoadon.MaHoaDon = hoadon.MaHoaDon and sanpham.MaSP = ct_hoadon.MaSP and hoadon.Ngay between '2021-02-07' and '2021-02-07' 
Group by TenSP,DonGia

select 'SoLuong'=SUM(SoLuong), 'TongTien'=SUM(TongTien)
from ct_hoadon,hoadon
where ct_hoadon.MaHoaDon = hoadon.MaHoaDon and Ngay between '2021-02-07' and '2021-02-07' 

--tính tổng hoá đơn theo ngày
select * , 'tong'= (select 'TONG'=sum(TongTien) from hoadon where  Ngay between '2021-05-29' and '2021-05-30') 
from hoadon
where  Ngay between '2021-02-07' and '2021-05-29'

select MaHoaDon,Ngay,SDT,TongTien,Username,GhiChu
FROM hoadon
where Ngay between '2021-02-07' and '2021-05-29'
 
select 'TONG' = sum(TongTien) from hoadon where  Ngay between '2021-02-07' and '2021-02-09' 
group by MaHoaDon,Ngay,SDT,TongTien,Username,GhiChu

UPDATE ct_phieuxuat SET MaPX = '1',MaSP ='EX',SoLuong = '4',DonGia = '40000' WHERE MaCTPX = '1'

Select 'TongTien'=sum(DonGia) 
From ct_hoadon 
Where MaHoaDon = '12' 

select 'SoLuong'=SUM(SoLuong)
from ct_hoadon
where MaHoaDon = '12'


select 'DonGia'=sum(DonGia)
from ct_hoadon
where MaHoaDon = '12'


UPDATE hoadon 
SET TongTien = 
(select 'TongTien'=sum(SoLuong * DonGia)
from ct_hoadon
where MaHoaDon = '12'
Group by MaHoaDon)
where MaHoaDon = '12'

--Tính tổng chi tiết hoá đơn từng sản phẩm theo ngày
select TenSP, GiaBan, 'SoLuong'=SUM(SoLuong), 'TongTien'=(SUM(SoLuong) * GiaBan) 
from ct_hoadon,hoadon,sanpham  
where ct_hoadon.MaHoaDon = hoadon.MaHoaDon and sanpham.MaSP = ct_hoadon.MaSP and Ngay between '2021-05-29' and '2021-05-30' 
Group by TenSP,GiaBan

select TenSP, GiaBan, 'SoLuong'=SUM(SoLuong), 'TongTien'=(SUM(SoLuong) * GiaBan),
"TongĐG" = (SELECT "TONGGG"= sum(TongTien)
	FROM "dbo"."hoadon" hoadon
	WHERE Ngay between '2021-05-29' and '2021-05-30' ),
"TongCG" = (SELECT "TONGCG"= sum(ThanhTien)
	 FROM ct_hoadon,hoadon
	 WHERE ct_hoadon.MaHoaDon = hoadon.MaHoaDon and Ngay between '2021-05-29' and '2021-05-30'), 
"TongSL" = (SELECT "TONGSL"= sum(SoLuong)
            FROM hoadon,ct_hoadon
            WHERE ct_hoadon.MaHoaDon = hoadon.MaHoaDon and Ngay between '2021-05-29' and '2021-05-30'), 
"TongGG" = (
	(SELECT "TONGCG"= sum(ThanhTien)
	 FROM ct_hoadon,hoadon
	 WHERE ct_hoadon.MaHoaDon = hoadon.MaHoaDon and Ngay between '2021-05-29' and '2021-05-30')
-
	(SELECT "TONGGG"= sum(TongTien)
	FROM "dbo"."hoadon" hoadon
	WHERE Ngay between '2021-05-29' and '2021-05-30' ))
from ct_hoadon,hoadon,sanpham  
where ct_hoadon.MaHoaDon = hoadon.MaHoaDon and sanpham.MaSP = ct_hoadon.MaSP and Ngay between '2021-05-29' and '2021-05-30'  
Group by TenSP,GiaBan

--Lấy tổng tiền báo cáo
select DISTINCT 'SoLuong' = sum(ct_hoadon.ThanhTien),'TongTien'= (select 'TongTien'= sum(TongTien) from hoadon where Ngay between '09/06/2021' and '09/06/2021')
from ct_hoadon,hoadon
where ct_hoadon.MaHoaDon = hoadon.MaHoaDon and Ngay between '09/06/2021' and '09/06/2021'

--Lấy giảm tiền báo cáo
select DISTINCT 'SoLuong' = sum(ct_hoadon.ThanhTien),'TongTien'= sum(ct_hoadon.ThanhTien) - (select 'TongTien'= sum(TongTien) from hoadon where Ngay between '09/06/2021' and '09/06/2021')
from ct_hoadon,hoadon
where ct_hoadon.MaHoaDon = hoadon.MaHoaDon and Ngay between '09/06/2021' and '09/06/2021'
*/

/*Ràng buộc ĐƠN HÀNG*/
alter table Course
add constraint FK_c_u foreign key(LecturerId) references AspNetUsers(Id);
