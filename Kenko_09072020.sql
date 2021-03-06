USE [Kenko]
GO
/****** Object:  StoredProcedure [dbo].[sp_Check_UserPass]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Check_UserPass]
	@id_user varchar(50),
	@password varchar(255)
AS
BEGIN
	DECLARE @returnCheck int

	SELECT @returnCheck = COUNT(*) FROM Tabel_User WHERE id_user = @id_user AND password = @password

	RETURN @returnCheck
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Dokter_Create]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Dokter_Create]
	@no_sip varchar(50),
	@nama_dokter varchar(100),
	@spesialisasi varchar(30),
	@jenis_kelamin varchar(20),
	@alamat varchar(255),
	@no_telp varchar(16),
	@email varchar(50),
	@password varchar(255)
AS
BEGIN
INSERT INTO Dokter (
	no_sip,
	nama_dokter,
	spesialisasi,
	jenis_kelamin,
	alamat,
	no_telp,
	email, password)
  VALUES (
	@no_sip,
	@nama_dokter,
	@spesialisasi,
	@jenis_kelamin,
	@alamat,
	@no_telp,
	@email,
	@password)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Dokter_Delete]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Dokter_Delete]
	@id_dokter int
AS
BEGIN
	UPDATE Dokter SET status = 0
	WHERE id_dokter = @id_dokter
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Dokter_GetDokter]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Dokter_GetDokter]
	@cari varchar(100)
AS
BEGIN
	SELECT * FROM Dokter 
	WHERE status = 1 AND id_dokter = @cari
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Dokter_Login]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Dokter_Login]
	@email varchar(100),
	@password varchar(100)
as
BEGIN
	select * from Dokter where email=@email and password=@password
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Dokter_Read]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Dokter_Read]
	@cari varchar(100)
AS
BEGIN
	SELECT * FROM Dokter 
	WHERE status = 1 AND (no_sip LIKE '%' + @cari + '%' OR
	nama_dokter LIKE '%' + @cari + '%' OR
	jenis_kelamin LIKE '%' + @cari + '%' OR
	spesialisasi LIKE '%' + @cari + '%' OR
	alamat LIKE '%' + @cari + '%' OR
	no_telp LIKE '%' + @cari + '%' OR
	email LIKE '%' + @cari + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Dokter_Update]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Dokter_Update]
	@id_dokter integer,
	@no_sip varchar(50),
	@nama_dokter varchar(100),
	@spesialisasi varchar(30),
	@jenis_kelamin varchar(20),
	@alamat varchar(255),
	@no_telp varchar(16),
	@email varchar(50),
	@password varchar(255)
AS
BEGIN
UPDATE Dokter SET 
	no_sip = @no_sip,
	nama_dokter = @nama_dokter,
	spesialisasi = @spesialisasi,
	alamat = @alamat,
	jenis_kelamin = @jenis_kelamin,
	no_telp = @no_telp,
	email = @email,
	password = @password
	WHERE id_dokter = @id_dokter
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Kategori_Create]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Kategori_Create]
	@nama_kategori varchar(20),
	@singkatan varchar(8),
	@keterangan varchar(255)
AS
BEGIN
INSERT INTO Kategori (
	nama_kategori,
	singkatan,
	keterangan)
  VALUES (
	@nama_kategori,
	@singkatan,
	@keterangan)

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Kategori_Delete]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Kategori_Delete]
	@id_kategori int
AS
BEGIN
	UPDATE Kategori SET status = 0
	WHERE id_kategori = @id_kategori
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Kategori_Read]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Kategori_Read]
	@cari varchar(100)
AS
BEGIN
	SELECT id_kategori, nama_kategori, ISNULL(singkatan, '-') as singkatan, ISNULL(keterangan, '-') as keterangan FROM Kategori
	WHERE status = 1 AND (nama_kategori LIKE '%' + @cari + '%' OR
	singkatan LIKE '%' + @cari + '%' OR
	keterangan LIKE '%' + @cari + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Kategori_Update]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Kategori_Update]
	@id_kategori integer,
	@nama_kategori varchar(20),
	@singkatan varchar(5),
	@keterangan varchar(255)
AS
BEGIN
	UPDATE Kategori
	SET nama_kategori = @nama_kategori,
	singkatan = @singkatan,
	keterangan = @keterangan
	WHERE id_kategori = @id_kategori
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Member_Create]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Member_Create]
	@id_member varchar(12),
	@nik varchar(16),
	@nama varchar(255),
	@jenis_kelamin varchar(10),
	@no_telp varchar(15),
	@tgl_bergabung date,
	@poin int
AS
BEGIN
INSERT INTO Member (
	id_member,
	nik,
	nama,
	jenis_kelamin,
	no_telepon,
	tgl_bergabung,
	poin)
  VALUES (
	@id_member,
	@nik,
	@nama,
	@jenis_kelamin,
	@no_telp,
	@tgl_bergabung,
	@poin)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Member_Delete]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Member_Delete]
	@id_member varchar(12)
AS
BEGIN
	UPDATE Member SET status = 0
	WHERE id_member = @id_member
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Member_GetLast]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Member_GetLast]
	@cari varchar(15)
	
AS
BEGIN
	SELECT TOP(1) id_member FROM Member WHERE id_member LIKE @cari + '%' ORDER BY id_member DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Member_GetMember]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Member_GetMember]
	@cari varchar(100)
AS
BEGIN
	SELECT TOP(1) *, FORMAT (tgl_bergabung, 'dd MMMM yyyy', 'id-ID') as tglFormatted, FORMAT(poin, '#,###', 'id-ID') as poinMember FROM Member
	WHERE status = 1 AND (id_member LIKE '%' + @cari + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Member_Read]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Member_Read]
	@cari varchar(100)
AS
BEGIN
	SELECT *, FORMAT (tgl_bergabung, 'dd MMMM yyyy', 'id-ID') as tglFormatted, FORMAT(poin, '#,###', 'id-ID') as poinMember FROM Member
	WHERE status = 1 AND (nik LIKE '%' + @cari + '%' OR
	nama LIKE '%' + @cari + '%' OR
	tgl_bergabung LIKE '%' + @cari + '%' OR
	jumlah_transaksi LIKE '%' + @cari + '%' OR
	poin LIKE '%' + @cari + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Member_Update]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Member_Update]
	@id_member varchar(12),
	@nik varchar(16),
	@nama varchar(255),
	@jenis_kelamin varchar(10),
	@no_telp varchar(15)
AS
BEGIN
	UPDATE Member
	SET nama = @nama,
	nik = @nik,
	jenis_kelamin = @jenis_kelamin,
	no_telepon = @no_telp
	WHERE id_member = @id_member
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Obat_Create]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Obat_Create]
	@id_obat varchar(14),
	@nama_obat varchar(100),
	@id_kategori integer,
	@id_satuan integer,
	@tgl_masuk date,
	@tgl_expired date,
	@stok integer,
	@harga_beli money,
	@harga_jual money,
	@het money,
	@produsen varchar(100),
	@deskripsi text,
	@updated_at datetime
AS
BEGIN
INSERT INTO Obat (
	id_obat,
	nama_obat,
	id_kategori,
	id_satuan,
	tgl_masuk,
	tgl_expired,
	stok,
	harga_beli,
	harga_jual,
	het,
	produsen,
	deskripsi,
	updated_at)
  VALUES (
	@id_obat,
	@nama_obat,
	@id_kategori,
	@id_satuan,
	@tgl_masuk,
	@tgl_expired,
	@stok,
	@harga_beli,
	@harga_jual,
	@het,
	@produsen,
	@deskripsi,
	@updated_at)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Obat_Delete]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Obat_Delete]
	@id_obat varchar(14)
AS
BEGIN
	UPDATE Obat  SET status = 0 WHERE id_obat = @id_obat
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Obat_GetKategori]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Obat_GetKategori]
	@cari varchar(14)
AS
BEGIN
	SELECT *, FORMAT(tgl_expired, 'dd MMM yyyy', 'id-ID') as tglExpired, FORMAT(harga_jual, 'Rp#,###.00', 'id-ID') as hargaJual FROM Obat WHERE id_kategori LIKE '%' + @cari + '%' ORDER BY id_obat
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Obat_GetLast]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Obat_GetLast]
	@cari varchar(14)
AS
BEGIN
	SELECT TOP(1) id_obat FROM Obat WHERE id_obat LIKE @cari + '%' ORDER BY id_obat DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Obat_Read]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Obat_Read]
	@cari varchar(100)
AS
BEGIN
	SELECT *, FORMAT(tgl_masuk, 'dd MMM yyyy', 'id-ID') as tglMasuk, 
	FORMAT(tgl_expired, 'dd MMM yyyy', 'id-ID') as tglExpired,
	FORMAT(harga_beli, 'Rp#,###.00', 'id-ID') as hargaBeli, 
	FORMAT(harga_jual, 'Rp#,###.00', 'id-ID') as hargaJual,
	FORMAT(het, 'Rp#,###.00', 'id-ID') as hetFormatted 
	FROM Obat o JOIN Kategori k ON o.id_kategori = k.id_kategori
	JOIN Satuan s ON o.id_satuan = s.id_satuan
	WHERE o.status = 1 AND (nama_obat LIKE '%' + @cari + '%' OR
	k.nama_kategori LIKE '%' + @cari + '%' OR
	s.satuan LIKE '%' + @cari + '%' OR
	tgl_masuk LIKE '%' + @cari + '%' OR
	tgl_expired LIKE '%' + @cari + '%' OR
	stok LIKE '%' + @cari + '%' OR
	harga_beli LIKE '%' + @cari + '%' OR
	harga_jual LIKE '%' + @cari + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Obat_Update]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Obat_Update]
	@id_obat varchar(14),
	@nama_obat varchar(100),
	@id_kategori integer,
	@id_satuan integer,
	@tgl_expired date,
	@stok integer,
	@harga_beli money,
	@harga_jual money,
	@het money,
	@produsen varchar(100),
	@deskripsi text,
	@updated_at datetime
AS
BEGIN
UPDATE Obat SET
	nama_obat = @nama_obat,
	id_kategori = @id_kategori,
	id_satuan = @id_satuan,
	tgl_expired = @tgl_expired,
	stok = @stok,
	harga_beli = @harga_beli,
	harga_jual = @harga_jual,
	het = @het,
	produsen = @produsen,
	deskripsi = @deskripsi,
	updated_at = @updated_at
	WHERE id_obat = @id_obat
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Pasien_Create]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Pasien_Create]
	@nama_pasien varchar(100),
	@jenis_kelamin varchar(10),
	@tgl_lahir date,
	@alamat varchar(100),
	@no_telp varchar(16),
	@golongan_darah varchar(2),
	@pekerjaan varchar(50)
AS
BEGIN
INSERT INTO Pasien (
	nama_pasien,
	jenis_kelamin,
	tgl_lahir,
	alamat,
	no_telp,
	golongan_darah,
	pekerjaan)
  VALUES (
	@nama_pasien,
	@jenis_kelamin,
	@tgl_lahir,
	@alamat,
	@no_telp,
	@golongan_darah,
	@pekerjaan)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Pasien_Delete]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Pasien_Delete]
	@id_pasien int
AS
BEGIN
	UPDATE Pasien SET status = 0
	WHERE id_pasien = @id_pasien
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Pasien_Read]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Pasien_Read]
	@cari varchar(100)
AS
BEGIN
	SELECT *, ISNULL(pekerjaan, '-') as pekerjaanFormatted, FORMAT(tgl_lahir, 'dd MMM yyyy', 'id-ID') as tglLahir,
	Floor(DateDiff(d, tgl_lahir, GetDate()) / 365.25) as umur
	FROM Pasien 
	WHERE status = 1 AND (nama_pasien LIKE '%' + @cari + '%' OR
	jenis_kelamin LIKE '%' + @cari + '%' OR
	tgl_lahir LIKE '%' + @cari + '%' OR
	alamat LIKE '%' + @cari + '%' OR
	no_telp LIKE '%' + @cari + '%' OR
	golongan_darah LIKE '%' + @cari + '%' OR
	pekerjaan LIKE '%' + @cari + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Pasien_Update]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Pasien_Update]
	@id_pasien integer,
	@nama_pasien varchar(100),
	@jenis_kelamin varchar(10),
	@tgl_lahir date,
	@alamat varchar(100),
	@no_telp varchar(16),
	@golongan_darah varchar(2),
	@pekerjaan varchar(50)
AS
BEGIN
	UPDATE Pasien SET
	nama_pasien = @nama_pasien,
	jenis_kelamin = @jenis_kelamin,
	tgl_lahir = @tgl_lahir,
	alamat = @alamat,
	no_telp = @no_telp,
	golongan_darah = @golongan_darah,
	pekerjaan = @pekerjaan
	WHERE id_pasien = @id_pasien
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Satuan_Create]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Satuan_Create]
	@satuan varchar(10),
	@keterangan varchar(255)
AS
BEGIN
INSERT INTO Satuan (
	satuan,
	keterangan)
  VALUES (
	@satuan,
	@keterangan)

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Satuan_Delete]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Satuan_Delete]
	@id_satuan int
AS
BEGIN
	UPDATE Satuan SET status = 0
	WHERE id_satuan = @id_satuan
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Satuan_Read]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Satuan_Read]
	@cari varchar(100)
AS
BEGIN
	SELECT id_satuan, satuan, ISNULL(keterangan, '-') as keterangan FROM Satuan 
	WHERE status = 1 AND (satuan LIKE '%' + @cari + '%' OR
	keterangan LIKE '%' + @cari + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Satuan_Update]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Satuan_Update]
	@id_satuan integer,
	@satuan varchar(10),
	@keterangan varchar(255)
AS
BEGIN
	UPDATE Satuan
	SET satuan = @satuan,
	keterangan = @keterangan
	WHERE id_satuan = @id_satuan
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Supplier_Create]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Supplier_Create]
	@nama_supplier varchar(100),
	@nama_kontak varchar(100),
	@alamat varchar(255),
	@no_telp varchar(16),
	@bank varchar(20),
	@no_rekening varchar(25),
	@email varchar(50),
	@keterangan varchar(255)
AS
BEGIN
INSERT INTO Supplier (
	nama_supplier,
	nama_kontak,
	alamat,
	no_telp,
	bank,
	no_rekening,
	email,
	keterangan)
  VALUES (
	@nama_supplier,
	@nama_kontak,
	@alamat,
	@no_telp,
	@bank,
	@no_rekening,
	@email,
	@keterangan)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Supplier_Delete]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Supplier_Delete]
	@id_supplier int
AS
BEGIN
	UPDATE Supplier SET status = 0
	WHERE id_supplier = @id_supplier
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Supplier_Read]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Supplier_Read]
	@cari varchar(100)
AS
BEGIN
	SELECT *, ISNULL(email, '-') as emailFormatted, ISNULL(keterangan, '-') as keteranganFormatted FROM Supplier 
	WHERE status = 1 AND (nama_supplier LIKE '%' + @cari + '%' OR
	nama_kontak LIKE '%' + @cari + '%' OR
	alamat LIKE '%' + @cari + '%' OR
	no_telp LIKE '%' + @cari + '%' OR
	no_rekening LIKE '%' + @cari + '%' OR
	bank LIKE '%' + @cari + '%' OR
	keterangan LIKE '%' + @cari + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Supplier_Update]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Supplier_Update]
	@id_supplier integer,
	@nama_supplier varchar(100),
	@nama_kontak varchar(100),
	@alamat varchar(255),
	@no_telp varchar(16),
	@bank varchar(20),
	@no_rekening varchar(25),
	@email varchar(50),
	@keterangan varchar(255)
AS
BEGIN
	UPDATE Supplier
	SET 
	nama_supplier = @nama_supplier,
	nama_kontak = @nama_kontak,
	alamat = @alamat,
	no_telp = @no_telp,
	bank = @bank,
	no_rekening = @no_rekening,
	email = @email,
	keterangan = @keterangan
	WHERE id_supplier = @id_supplier
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Beli_Obat]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Beli_Obat]
	@no_pembelian varchar(14),
	@nama_obat varchar(100),
	@id_obat1 varchar(14),
	@id_obat2 varchar(14),
	@tgl_expired date,
	@jumlah_beli integer,
	@harga_beli money,
	@harga_jual money,
	@total_harga money
AS
BEGIN
	DECLARE @cekobat INT,
	@id_obat3 varchar(14),
	@id_kategori integer,
	@id_satuan integer,
	@het money,
	@produsen varchar(100),
	@deskripsi varchar(255);

    SELECT @cekobat = COUNT(*) FROM Obat WHERE nama_obat = @nama_obat AND tgl_expired = @tgl_expired;

	SELECT @id_kategori = id_kategori, @id_satuan = id_satuan,
	@het = het, @produsen = produsen, @deskripsi = deskripsi
	FROM Obat WHERE id_obat = @id_obat1;

    IF @cekobat > 0
	BEGIN
		SELECT @id_obat3 = id_obat FROM Obat WHERE nama_obat = @nama_obat AND tgl_expired = @tgl_expired;
		UPDATE Obat SET stok = stok + @jumlah_beli WHERE id_obat = @id_obat3
		INSERT INTO DetilPembelian(
			no_pembelian,
			id_obat,
			jumlah, 
			total_harga
		)
		  VALUES (
			@no_pembelian,
			@id_obat3,
			@jumlah_beli,
			@total_harga
		  )
	END
	ELSE
	BEGIN
		INSERT INTO Obat (
			id_obat,
			nama_obat,
			id_kategori,
			id_satuan,
			tgl_masuk,
			tgl_expired,
			stok,
			harga_beli,
			harga_jual,
			het,
			produsen,
			deskripsi,
			updated_at)
		  VALUES (
			@id_obat2,
			@nama_obat,
			@id_kategori,
			@id_satuan,
			GETDATE(),
			@tgl_expired,
			@jumlah_beli,
			@harga_beli,
			@harga_jual,
			@het,
			@produsen,
			@deskripsi,
			GETDATE())

		INSERT INTO DetilPembelian(
			no_pembelian,
			id_obat,
			jumlah, 
			total_harga)
		  VALUES (
			@no_pembelian,
			@id_obat2,
			@jumlah_beli,
			@total_harga)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Pembelian]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Pembelian]
	@no_pembelian varchar(50),
	@total_harga money,
	@id_supplier int,
	@id_user int
AS
BEGIN
INSERT INTO Pembelian(
	no_pembelian,
	total_harga,
	id_supplier, 
	id_user
)
  VALUES (
	@no_pembelian,
	@total_harga,
	@id_supplier,
	@id_user
  )

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Pembelian_Detail]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Pembelian_Detail]
	@no_pembelian varchar(50),
	@id_obat varchar(14),
	@jumlah int,
	@total_harga money
AS
BEGIN
INSERT INTO DetilPembelian(
	no_pembelian,
	id_obat,
	jumlah, 
	total_harga
)
  VALUES (
	@no_pembelian,
	@id_obat,
	@jumlah,
	@total_harga
  )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Pembelian_GetLast]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Pembelian_GetLast]
	@cari varchar(50)

AS
BEGIN
	SELECT TOP(1) no_pembelian FROM Pembelian WHERE no_pembelian LIKE @cari + '%' ORDER BY no_pembelian DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Penjualan]    Script Date: 7/9/2020 11:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Penjualan]
	@no_penjualan varchar(50),
	@tgl_beli date,
	@total_harga money,
	@id_member varchar(12),
	@poin_terpakai int,
	@id_user varchar(12)

AS
BEGIN
INSERT INTO Penjualan (
	no_penjualan,
	tgl_beli,
	total_harga, 
	id_member,
	poin_terpakai,
	id_user
)
  VALUES (
	@no_penjualan,
	@tgl_beli,
	@total_harga,
	@id_member,
	@poin_terpakai,
	@id_user
  )

  IF(@id_member IS NOT NULL)
  BEGIN
  UPDATE Member SET poin = poin - @poin_terpakai WHERE id_member = @id_member

  UPDATE Member SET poin = poin + (@total_harga/100) WHERE id_member = @id_member
  END

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Penjualan_Detail]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Penjualan_Detail]
	@no_penjualan varchar(50),
	@id_obat varchar(14),
	@jumlah int,
	@total_harga money
AS
BEGIN
INSERT INTO Penjualan_Obat (
	no_penjualan,
	id_obat,
	jumlah,
	total_harga
)
  VALUES (
	@no_penjualan,
	@id_obat,
	@jumlah,
	@total_harga
  )
  UPDATE Obat SET stok = stok - @jumlah WHERE id_obat = @id_obat
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Penjualan_GetLast]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Penjualan_GetLast]
	@cari varchar(50)

AS
BEGIN
	SELECT TOP(1) no_penjualan FROM Penjualan WHERE no_penjualan LIKE @cari + '%' ORDER BY no_penjualan DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Resep]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Resep]
	@id_resep varchar(50),
	@id_dokter int,
	@id_pasien int
AS
BEGIN
INSERT INTO Resep(
	id_resep,
	id_dokter,
	id_pasien)
  VALUES (
	@id_resep,
	@id_dokter,
	@id_pasien)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Resep_Detail]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Resep_Detail]
	@id_resep varchar(14),
	@id_obat varchar(14),
	@jumlah int,
	@keterangan varchar(255)
AS
BEGIN
INSERT INTO DetilResep(
	id_resep,
	id_obat,
	jumlah,
	keterangan)
  VALUES (
	@id_resep,
	@id_obat,
	@jumlah,
	@keterangan)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Resep_GetLast]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Resep_GetLast]
	@cari varchar(50)
AS
BEGIN
	SELECT TOP(1) id_resep FROM Resep WHERE id_resep LIKE @cari + '%' ORDER BY id_resep DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Reservasi]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Reservasi]
	@no_reservasi varchar(50),
	@id_dokter int,
	@id_pasien int,
	@keterangan varchar(255)
AS
BEGIN
INSERT INTO Reservasi(
	no_reservasi,
	id_dokter,
	id_pasien,
	keterangan)
  VALUES (
	@no_reservasi,
	@id_dokter,
	@id_pasien,
	@keterangan)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Reservasi_GetCount]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Reservasi_GetCount]
AS
BEGIN
 SELECT COUNT(*) as jumlah FROM Reservasi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Reservasi_GetData]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Reservasi_GetData]
	@cari varchar(50)
AS
BEGIN
	SELECT r.*, p.id_pasien, p.nama_pasien, p.jenis_kelamin, p.golongan_darah, ISNULL(p.pekerjaan, '-') as pekerjaan, d.no_sip,
	d.nama_dokter, d.spesialisasi, Floor(DateDiff(d, tgl_lahir, GetDate()) / 365.25) as umur, FORMAT(tgl_reservasi, 'dd MMM yyyy', 'id-ID') as tglReservasi 
	FROM Reservasi r JOIN Pasien p ON r.id_pasien = p.id_pasien JOIN Dokter d ON d.id_dokter = r.id_dokter
	WHERE r.status_reservasi='M'
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Reservasi_GetLast]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Reservasi_GetLast]
	@cari varchar(15)
	
AS
BEGIN
	SELECT TOP(1) no_reservasi FROM Reservasi WHERE no_reservasi LIKE @cari + '%' ORDER BY no_reservasi DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Reservasi_UpdateStatus]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Transaksi_Reservasi_UpdateStatus]
	@no_reservasi varchar(14)
AS
BEGIN
	UPDATE Reservasi SET status_reservasi = 'S' WHERE no_reservasi = @no_reservasi
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Create]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_User_Create]
	@nama varchar(255),
	@username varchar(50),
	@password varchar(50),
	@email varchar(50),
	@role int
AS
BEGIN
INSERT INTO Tabel_User (
	nama,
	username,
	password,
	email,
	role)
  VALUES (
	@nama,
	@username,
	@password,
	@email,
	@role)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Delete]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_User_Delete]
	@id_user int
AS
BEGIN
	UPDATE Tabel_User SET status = 0
	WHERE id_user = @id_user
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Login]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_User_Login]
	@email varchar(100),
	@password varchar(100)
as
BEGIN
	select * from Tabel_User where username=@email and password=@password
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Read]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_User_Read]
	@cari varchar(100)
AS
BEGIN
	SELECT *, (CASE WHEN role = 1 then 'Admin' WHEN role = 2 then 'Apoteker' else 'Manager' END) as RoleName INTO #MyUser FROM Tabel_User u

	SELECT * FROM #MyUser 
	WHERE status = 1 AND (nama LIKE '%' + @cari + '%' OR
	username LIKE '%' + @cari + '%' OR
	password LIKE '%' + @cari + '%' OR
	email LIKE '%' + @cari + '%' OR
	RoleName LIKE '%' + @cari + '%' OR
	foto LIKE '%' + @cari + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Update]    Script Date: 7/9/2020 11:10:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_User_Update]
	@id_user integer,
	@nama varchar(255),
	@username varchar(50),
	@password varchar(50),
	@email varchar(50),
	@role int,
	@foto varchar(255)
AS
BEGIN

	IF (@password IS NULL)
	BEGIN
		UPDATE Tabel_User
		SET nama = @nama,
		username = @username,
		email = @email,
		role = @role,
		foto = @foto
		WHERE id_user = @id_user
	END
	ELSE
	BEGIN
		UPDATE Tabel_User
		SET nama = @nama,
		username = @username,
		password = @password,
		email = @email,
		role = @role,
		foto = @foto
		WHERE id_user = @id_user
	END
END
GO
