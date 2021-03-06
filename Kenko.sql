USE [Kenko]
GO
/****** Object:  Table [dbo].[DetilPembelian]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetilPembelian](
	[no_pembelian] [varchar](14) NOT NULL,
	[id_obat] [varchar](14) NOT NULL,
	[jumlah] [int] NOT NULL,
	[total_harga] [money] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetilResep]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetilResep](
	[id_resep] [varchar](14) NULL,
	[id_obat] [varchar](14) NULL,
	[jumlah] [int] NULL,
	[keterangan] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dokter]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dokter](
	[id_dokter] [int] IDENTITY(1,1) NOT NULL,
	[no_sip] [varchar](50) NOT NULL,
	[nama_dokter] [varchar](100) NOT NULL,
	[jenis_kelamin] [varchar](10) NOT NULL,
	[spesialisasi] [varchar](30) NOT NULL,
	[alamat] [varchar](255) NOT NULL,
	[no_telp] [varchar](16) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Dokter] PRIMARY KEY CLUSTERED 
(
	[id_dokter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kategori]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategori](
	[id_kategori] [int] IDENTITY(1,1) NOT NULL,
	[nama_kategori] [varchar](20) NOT NULL,
	[singkatan] [varchar](8) NULL,
	[keterangan] [varchar](255) NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Kategori] PRIMARY KEY CLUSTERED 
(
	[id_kategori] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[id_member] [varchar](12) NOT NULL,
	[nik] [varchar](16) NOT NULL,
	[nama] [varchar](255) NOT NULL,
	[jenis_kelamin] [varchar](10) NOT NULL,
	[no_telepon] [varchar](14) NOT NULL,
	[tgl_bergabung] [date] NOT NULL,
	[jumlah_transaksi] [int] NOT NULL,
	[poin] [int] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[id_member] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Obat]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Obat](
	[id_obat] [varchar](14) NOT NULL,
	[nama_obat] [varchar](100) NOT NULL,
	[id_kategori] [int] NOT NULL,
	[id_satuan] [int] NOT NULL,
	[tgl_masuk] [date] NOT NULL,
	[tgl_expired] [date] NOT NULL,
	[stok] [int] NOT NULL,
	[harga_beli] [money] NOT NULL,
	[harga_jual] [money] NOT NULL,
	[het] [money] NOT NULL,
	[produsen] [varchar](50) NULL,
	[deskripsi] [text] NULL,
	[updated_at] [datetime] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK__Obat__3DE3354C892A26E6] PRIMARY KEY CLUSTERED 
(
	[id_obat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pasien]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pasien](
	[id_pasien] [int] IDENTITY(1,1) NOT NULL,
	[nama_pasien] [varchar](100) NOT NULL,
	[jenis_kelamin] [varchar](10) NOT NULL,
	[tgl_lahir] [date] NOT NULL,
	[alamat] [varchar](100) NOT NULL,
	[no_telp] [varchar](16) NOT NULL,
	[golongan_darah] [varchar](2) NOT NULL,
	[pekerjaan] [varchar](50) NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Pasien] PRIMARY KEY CLUSTERED 
(
	[id_pasien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pembelian]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pembelian](
	[no_pembelian] [varchar](14) NOT NULL,
	[tgl_masuk] [date] NOT NULL,
	[total_harga] [money] NULL,
	[id_supplier] [int] NULL,
	[id_user] [int] NULL,
 CONSTRAINT [PK__Pembelia__99ACC7F9D5B7BE59] PRIMARY KEY CLUSTERED 
(
	[no_pembelian] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Penjualan]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Penjualan](
	[no_penjualan] [varchar](14) NOT NULL,
	[tgl_beli] [date] NULL,
	[total_harga] [money] NULL,
	[id_member] [varchar](12) NULL,
	[poin_terpakai] [int] NULL,
	[id_user] [int] NULL,
 CONSTRAINT [PK__Penjuala__506EEC70FB4A5C34] PRIMARY KEY CLUSTERED 
(
	[no_penjualan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Penjualan_Obat]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Penjualan_Obat](
	[no_penjualan] [varchar](14) NOT NULL,
	[id_obat] [varchar](14) NOT NULL,
	[jumlah] [int] NULL,
	[total_harga] [money] NULL,
 CONSTRAINT [PK_Penjualan_Obat] PRIMARY KEY CLUSTERED 
(
	[no_penjualan] ASC,
	[id_obat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resep]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resep](
	[id_resep] [varchar](14) NOT NULL,
	[id_dokter] [int] NULL,
	[id_pasien] [int] NULL,
	[tgl_resep] [date] NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Resep] PRIMARY KEY CLUSTERED 
(
	[id_resep] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservasi]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservasi](
	[no_reservasi] [varchar](14) NOT NULL,
	[no_antrian] [int] NULL,
	[id_dokter] [int] NOT NULL,
	[id_pasien] [int] NOT NULL,
	[tgl_reservasi] [date] NOT NULL,
	[keterangan] [varchar](255) NULL,
	[status_reservasi] [char](1) NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[no_reservasi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Satuan]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Satuan](
	[id_satuan] [int] IDENTITY(1,1) NOT NULL,
	[satuan] [varchar](10) NULL,
	[keterangan] [varchar](255) NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Satuan] PRIMARY KEY CLUSTERED 
(
	[id_satuan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[id_supplier] [int] IDENTITY(1,1) NOT NULL,
	[nama_supplier] [varchar](100) NOT NULL,
	[nama_kontak] [varchar](100) NOT NULL,
	[alamat] [varchar](255) NOT NULL,
	[no_telp] [varchar](16) NOT NULL,
	[bank] [varchar](20) NOT NULL,
	[no_rekening] [varchar](25) NOT NULL,
	[email] [varchar](50) NULL,
	[keterangan] [varchar](255) NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[id_supplier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tabel_User]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tabel_User](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[nama] [varchar](255) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[role] [int] NOT NULL,
	[foto] [varchar](50) NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[DetilPembelian] ([no_pembelian], [id_obat], [jumlah], [total_harga]) VALUES (N'PB202007020002', N'OB202005300001', 10, 250000.0000)
INSERT [dbo].[DetilPembelian] ([no_pembelian], [id_obat], [jumlah], [total_harga]) VALUES (N'PB202007020002', N'OB202007020001', 5, 125000.0000)
INSERT [dbo].[DetilPembelian] ([no_pembelian], [id_obat], [jumlah], [total_harga]) VALUES (N'PB202007020003', N'OB202005300001', 5, 125000.0000)
INSERT [dbo].[DetilPembelian] ([no_pembelian], [id_obat], [jumlah], [total_harga]) VALUES (N'PB202007020001', N'OB202006090001', 5, 125000.0000)
INSERT [dbo].[DetilPembelian] ([no_pembelian], [id_obat], [jumlah], [total_harga]) VALUES (N'PB202007020001', N'OB202007020001', 5, 125000.0000)
INSERT [dbo].[DetilPembelian] ([no_pembelian], [id_obat], [jumlah], [total_harga]) VALUES (N'PB202007020001', N'OB202005300001', 5, 125000.0000)
GO
INSERT [dbo].[DetilResep] ([id_resep], [id_obat], [jumlah], [keterangan]) VALUES (N'RS202006300001', N'OB202005300001', 1, N'3 x 1 sendok makan')
INSERT [dbo].[DetilResep] ([id_resep], [id_obat], [jumlah], [keterangan]) VALUES (N'RS202006300001', N'OB20200628', 1, N'3 x 1 sendok makan')
INSERT [dbo].[DetilResep] ([id_resep], [id_obat], [jumlah], [keterangan]) VALUES (N'RS202007020001', N'OB202005300001', 2, N'3 x 1 sendok makan')
GO
SET IDENTITY_INSERT [dbo].[Dokter] ON 

INSERT [dbo].[Dokter] ([id_dokter], [no_sip], [nama_dokter], [jenis_kelamin], [spesialisasi], [alamat], [no_telp], [email], [password], [status]) VALUES (1, N'123/abc/345/2013', N'ALI TIRTA', N'Laki-Laki', N'Mata', N'Jl. Jati V D No. 7', N'0813452712923', N'dokter', N'dokter', 1)
INSERT [dbo].[Dokter] ([id_dokter], [no_sip], [nama_dokter], [jenis_kelamin], [spesialisasi], [alamat], [no_telp], [email], [password], [status]) VALUES (2, N'153/oef/237/9820', N'Kusumawardhani', N'Perempuan', N'Kulit', N'Jl. Sungai Bambu 5 A, Test', N'0812364623123', N'kusumawardhani@gmail.com', N'Kusuma123', 1)
INSERT [dbo].[Dokter] ([id_dokter], [no_sip], [nama_dokter], [jenis_kelamin], [spesialisasi], [alamat], [no_telp], [email], [password], [status]) VALUES (4, N'123/asd/1283/1203', N'Mulya Nugraha', N'Laki-Laki', N'Kulit', N'123Testing', N'91238098123', N'mulya@gmail.com', N'12837213', 1)
INSERT [dbo].[Dokter] ([id_dokter], [no_sip], [nama_dokter], [jenis_kelamin], [spesialisasi], [alamat], [no_telp], [email], [password], [status]) VALUES (5, N'123/ghg/986/rt', N'Test 1', N'Perempuan', N'Mata', N'Jl Jati 67', N'08123773647112', N'test@gmail.com', N'12345567', 1)
SET IDENTITY_INSERT [dbo].[Dokter] OFF
GO
SET IDENTITY_INSERT [dbo].[Kategori] ON 

INSERT [dbo].[Kategori] ([id_kategori], [nama_kategori], [singkatan], [keterangan], [status]) VALUES (1, N'Acyclovir', NULL, N'Anti Virus', 1)
INSERT [dbo].[Kategori] ([id_kategori], [nama_kategori], [singkatan], [keterangan], [status]) VALUES (2, N'Dipenhidrinate', NULL, NULL, 1)
INSERT [dbo].[Kategori] ([id_kategori], [nama_kategori], [singkatan], [keterangan], [status]) VALUES (3, N'Saluran Pencernaan', NULL, NULL, 1)
INSERT [dbo].[Kategori] ([id_kategori], [nama_kategori], [singkatan], [keterangan], [status]) VALUES (4, N'Test', N'test', N'test', 0)
INSERT [dbo].[Kategori] ([id_kategori], [nama_kategori], [singkatan], [keterangan], [status]) VALUES (5, N'asdas', NULL, NULL, 0)
INSERT [dbo].[Kategori] ([id_kategori], [nama_kategori], [singkatan], [keterangan], [status]) VALUES (6, N'asdasd', NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Kategori] OFF
GO
INSERT [dbo].[Member] ([id_member], [nik], [nama], [jenis_kelamin], [no_telepon], [tgl_bergabung], [jumlah_transaksi], [poin], [status]) VALUES (N'202006040001', N'12783917239', N'Samodra', N'Perempuan', N'213123123', CAST(N'2020-06-04' AS Date), 0, 1000, 1)
INSERT [dbo].[Member] ([id_member], [nik], [nama], [jenis_kelamin], [no_telepon], [tgl_bergabung], [jumlah_transaksi], [poin], [status]) VALUES (N'202006090001', N'62136127', N'Test', N'Perempuan', N'123123123', CAST(N'2020-06-09' AS Date), 0, 0, 0)
INSERT [dbo].[Member] ([id_member], [nik], [nama], [jenis_kelamin], [no_telepon], [tgl_bergabung], [jumlah_transaksi], [poin], [status]) VALUES (N'202006270001', N'8291371972312839', N'Samodraaszxc', N'Perempuan', N'0812365713267', CAST(N'2020-06-27' AS Date), 0, 0, 0)
GO
INSERT [dbo].[Obat] ([id_obat], [nama_obat], [id_kategori], [id_satuan], [tgl_masuk], [tgl_expired], [stok], [harga_beli], [harga_jual], [het], [produsen], [deskripsi], [updated_at], [status]) VALUES (N'OB202005300001', N'Acitral', 3, 2, CAST(N'2020-05-30' AS Date), CAST(N'2021-07-15' AS Date), 55, 38102.0000, 41200.0000, 41230.0000, N'PT. Kalbe Farma', N'ACITRAL SIRUP merupakan obat yang mengandung Mg(OH)2, Al(OH)3, dan Simethicone. Obat ini digunakan untuk mengatasi gejala akibat kelebihan asam lambung, gastritis, tukak lambung, usus dua belas jari, dengan gejala seperti mual dan nyeri ulu hati.', CAST(N'2020-05-30T12:04:57.103' AS DateTime), 1)
INSERT [dbo].[Obat] ([id_obat], [nama_obat], [id_kategori], [id_satuan], [tgl_masuk], [tgl_expired], [stok], [harga_beli], [harga_jual], [het], [produsen], [deskripsi], [updated_at], [status]) VALUES (N'OB202006090001', N'Acitral', 1, 1, CAST(N'2020-06-09' AS Date), CAST(N'2020-06-20' AS Date), 150, 1000.0000, 5000.0000, 1200.0000, N'test', N'test', CAST(N'2020-06-09T14:01:32.237' AS DateTime), 1)
INSERT [dbo].[Obat] ([id_obat], [nama_obat], [id_kategori], [id_satuan], [tgl_masuk], [tgl_expired], [stok], [harga_beli], [harga_jual], [het], [produsen], [deskripsi], [updated_at], [status]) VALUES (N'OB20200628', N'sad', 2, 2, CAST(N'2020-06-28' AS Date), CAST(N'2020-06-12' AS Date), 123, 123.0000, 123500.0000, 123500.0000, N'asd', N'asd', CAST(N'2020-06-28T01:33:55.870' AS DateTime), 1)
INSERT [dbo].[Obat] ([id_obat], [nama_obat], [id_kategori], [id_satuan], [tgl_masuk], [tgl_expired], [stok], [harga_beli], [harga_jual], [het], [produsen], [deskripsi], [updated_at], [status]) VALUES (N'OB202006280001', N'Alkaline', 3, 2, CAST(N'2020-06-28' AS Date), CAST(N'2020-06-11' AS Date), 123, 182937.0000, 14000.0000, 14000.0000, N'asdhaksjhd', N'askdhaksdh', CAST(N'2020-06-28T00:48:21.083' AS DateTime), 1)
INSERT [dbo].[Obat] ([id_obat], [nama_obat], [id_kategori], [id_satuan], [tgl_masuk], [tgl_expired], [stok], [harga_beli], [harga_jual], [het], [produsen], [deskripsi], [updated_at], [status]) VALUES (N'OB202006280002', N'Alkaline', 3, 1, CAST(N'2020-06-28' AS Date), CAST(N'2020-06-04' AS Date), 213, 213.0000, 123500.0000, 123500.0000, N'321', N'asdasdasd', CAST(N'2020-06-28T01:46:07.820' AS DateTime), 0)
INSERT [dbo].[Obat] ([id_obat], [nama_obat], [id_kategori], [id_satuan], [tgl_masuk], [tgl_expired], [stok], [harga_beli], [harga_jual], [het], [produsen], [deskripsi], [updated_at], [status]) VALUES (N'OB202006280003', N'nojo', 3, 2, CAST(N'2020-06-28' AS Date), CAST(N'2020-06-03' AS Date), 89, 89080980.0000, 90000.0000, 90000.0000, N'dsffd', N'trergfdfg', CAST(N'2020-06-28T02:33:04.403' AS DateTime), 0)
INSERT [dbo].[Obat] ([id_obat], [nama_obat], [id_kategori], [id_satuan], [tgl_masuk], [tgl_expired], [stok], [harga_beli], [harga_jual], [het], [produsen], [deskripsi], [updated_at], [status]) VALUES (N'OB202007020001', N'Acitral', 3, 2, CAST(N'2020-07-02' AS Date), CAST(N'2020-07-02' AS Date), 10, 25000.0000, 25000.0000, 41230.0000, N'PT. Kalbe Farma', N'ACITRAL SIRUP merupakan obat yang mengandung Mg(OH)2, Al(OH)3, dan Simethicone. Obat ini digunakan untuk mengatasi gejala akibat kelebihan asam lambung, gastritis, tukak lambung, usus dua belas jari, dengan gejala seperti mual dan nyeri ulu hati.', CAST(N'2020-07-02T10:16:31.763' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Pasien] ON 

INSERT [dbo].[Pasien] ([id_pasien], [nama_pasien], [jenis_kelamin], [tgl_lahir], [alamat], [no_telp], [golongan_darah], [pekerjaan], [status]) VALUES (1, N'Rudi', N'Laki-Laki', CAST(N'1991-08-01' AS Date), N'Penjor', N'0812374927123', N'AB', NULL, 1)
INSERT [dbo].[Pasien] ([id_pasien], [nama_pasien], [jenis_kelamin], [tgl_lahir], [alamat], [no_telp], [golongan_darah], [pekerjaan], [status]) VALUES (3, N'test', N'Perempuan', CAST(N'2005-07-06' AS Date), N'test', N'0856342322123', N'O', N'-', 1)
INSERT [dbo].[Pasien] ([id_pasien], [nama_pasien], [jenis_kelamin], [tgl_lahir], [alamat], [no_telp], [golongan_darah], [pekerjaan], [status]) VALUES (4, N'Samodra', N'Laki-Laki', CAST(N'2000-12-09' AS Date), N'Jl. Jati V D No. 9', N'12093810283', N'-', NULL, 1)
SET IDENTITY_INSERT [dbo].[Pasien] OFF
GO
INSERT [dbo].[Pembelian] ([no_pembelian], [tgl_masuk], [total_harga], [id_supplier], [id_user]) VALUES (N'PB202007020001', CAST(N'2020-07-02' AS Date), 375000.0000, 1, 2)
INSERT [dbo].[Pembelian] ([no_pembelian], [tgl_masuk], [total_harga], [id_supplier], [id_user]) VALUES (N'PB202007020002', CAST(N'2020-07-02' AS Date), 375000.0000, 1, 2)
INSERT [dbo].[Pembelian] ([no_pembelian], [tgl_masuk], [total_harga], [id_supplier], [id_user]) VALUES (N'PB202007020003', CAST(N'2020-07-02' AS Date), 125000.0000, 1, 2)
GO
INSERT [dbo].[Penjualan] ([no_penjualan], [tgl_beli], [total_harga], [id_member], [poin_terpakai], [id_user]) VALUES (N'PJ202006290001', CAST(N'2020-06-29' AS Date), 164800.0000, N'202006040001', 0, 2)
INSERT [dbo].[Penjualan] ([no_penjualan], [tgl_beli], [total_harga], [id_member], [poin_terpakai], [id_user]) VALUES (N'PJ202006290002', CAST(N'2020-06-29' AS Date), 137600.0000, NULL, 0, 2)
INSERT [dbo].[Penjualan] ([no_penjualan], [tgl_beli], [total_harga], [id_member], [poin_terpakai], [id_user]) VALUES (N'PJ202006290003', CAST(N'2020-06-29' AS Date), 137600.0000, NULL, 0, 2)
INSERT [dbo].[Penjualan] ([no_penjualan], [tgl_beli], [total_harga], [id_member], [poin_terpakai], [id_user]) VALUES (N'PJ202006290004', CAST(N'2020-06-29' AS Date), 96400.0000, NULL, 0, 2)
INSERT [dbo].[Penjualan] ([no_penjualan], [tgl_beli], [total_harga], [id_member], [poin_terpakai], [id_user]) VALUES (N'PJ202006290005', CAST(N'2020-06-29' AS Date), 96400.0000, NULL, 0, 2)
GO
INSERT [dbo].[Penjualan_Obat] ([no_penjualan], [id_obat], [jumlah], [total_harga]) VALUES (N'PJ202006290005', N'OB202005300001', 2, 82400.0000)
INSERT [dbo].[Penjualan_Obat] ([no_penjualan], [id_obat], [jumlah], [total_harga]) VALUES (N'PJ202006290005', N'OB202006280001', 1, 14000.0000)
GO
INSERT [dbo].[Resep] ([id_resep], [id_dokter], [id_pasien], [tgl_resep], [status]) VALUES (N'RS202006300001', 1, 1, CAST(N'2020-06-30' AS Date), 1)
INSERT [dbo].[Resep] ([id_resep], [id_dokter], [id_pasien], [tgl_resep], [status]) VALUES (N'RS202007020001', 1, 1, CAST(N'2020-07-02' AS Date), 1)
GO
INSERT [dbo].[Reservasi] ([no_reservasi], [no_antrian], [id_dokter], [id_pasien], [tgl_reservasi], [keterangan], [status_reservasi]) VALUES (N'RV202006300001', 1, 1, 1, CAST(N'2020-06-30' AS Date), N'Masuk angin', N'S')
INSERT [dbo].[Reservasi] ([no_reservasi], [no_antrian], [id_dokter], [id_pasien], [tgl_reservasi], [keterangan], [status_reservasi]) VALUES (N'RV202007010001', NULL, 1, 3, CAST(N'2020-07-01' AS Date), N'Mata saya sakit dok', N'S')
GO
SET IDENTITY_INSERT [dbo].[Satuan] ON 

INSERT [dbo].[Satuan] ([id_satuan], [satuan], [keterangan], [status]) VALUES (1, N'Kapsul', N'Kapsul', 1)
INSERT [dbo].[Satuan] ([id_satuan], [satuan], [keterangan], [status]) VALUES (2, N'Sirup', N'Sirup', 1)
INSERT [dbo].[Satuan] ([id_satuan], [satuan], [keterangan], [status]) VALUES (4, N'Test 1', N'12tes', 0)
INSERT [dbo].[Satuan] ([id_satuan], [satuan], [keterangan], [status]) VALUES (5, N'Test12', N'test12', 0)
INSERT [dbo].[Satuan] ([id_satuan], [satuan], [keterangan], [status]) VALUES (6, N'sd', N'asd', 1)
INSERT [dbo].[Satuan] ([id_satuan], [satuan], [keterangan], [status]) VALUES (7, N'ajskdhkasd', N'asdkg', 0)
SET IDENTITY_INSERT [dbo].[Satuan] OFF
GO
SET IDENTITY_INSERT [dbo].[Supplier] ON 

INSERT [dbo].[Supplier] ([id_supplier], [nama_supplier], [nama_kontak], [alamat], [no_telp], [bank], [no_rekening], [email], [keterangan], [status]) VALUES (1, N'PT. Sapta Ari', N'Wahyu Hidayat', N'Jl. Sungai Bambu 1 A, Tanjung Priok', N'01238091823', N'BRI', N'01238109283', N'saptaari@gmail.com', NULL, 1)
INSERT [dbo].[Supplier] ([id_supplier], [nama_supplier], [nama_kontak], [alamat], [no_telp], [bank], [no_rekening], [email], [keterangan], [status]) VALUES (4, N'tes', N'tes', N'Jl Jati V D', N'081233433212', N'BNI', N'823718273123123', N'test@gmail.com', N'Test', 1)
INSERT [dbo].[Supplier] ([id_supplier], [nama_supplier], [nama_kontak], [alamat], [no_telp], [bank], [no_rekening], [email], [keterangan], [status]) VALUES (5, N'asdasd', N'aaklsdnasdnmasd', N'asdaaksdnasnkdlasd', N'0128301283123', N'asdaskldjajs123', N'12039810923712331123', N'asdaksld@gmailco.com', N'asdaaksdnaksdasdasd', 0)
SET IDENTITY_INSERT [dbo].[Supplier] OFF
GO
SET IDENTITY_INSERT [dbo].[Tabel_User] ON 

INSERT [dbo].[Tabel_User] ([id_user], [nama], [username], [password], [email], [role], [foto], [status]) VALUES (2, N'Samodra', N'szsamodra12', N'samodra', N'samodra30@gmail.com', 2, N'default.jpg', 1)
INSERT [dbo].[Tabel_User] ([id_user], [nama], [username], [password], [email], [role], [foto], [status]) VALUES (4, N'Admin pertama', N'admin', N'admin', N'admin1@admin.com', 2, N'202006272154024.png', 1)
INSERT [dbo].[Tabel_User] ([id_user], [nama], [username], [password], [email], [role], [foto], [status]) VALUES (5, N'Reza Fahlevi', N'rezaa', N'reza', N'rezafah@gmail.com', 2, N'202005290201280005.png', 0)
INSERT [dbo].[Tabel_User] ([id_user], [nama], [username], [password], [email], [role], [foto], [status]) VALUES (7, N'Samodra', N'samodra@gmail.com', N'samodra1234', N'samodra@gmail.com', 2, N'202006272133450006.png', 0)
INSERT [dbo].[Tabel_User] ([id_user], [nama], [username], [password], [email], [role], [foto], [status]) VALUES (1007, N'Samo', N'szsamodra', N'Samodra12', N'samodra@gmail.com', 1, N'default.jpg', 1)
SET IDENTITY_INSERT [dbo].[Tabel_User] OFF
GO
ALTER TABLE [dbo].[Dokter] ADD  CONSTRAINT [DF_Dokter_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Kategori] ADD  CONSTRAINT [DF_Kategori_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_jumlah_transaksi]  DEFAULT ((0)) FOR [jumlah_transaksi]
GO
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_poin]  DEFAULT ((0)) FOR [poin]
GO
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Obat] ADD  CONSTRAINT [DF_Obat_tgl_masuk]  DEFAULT (getdate()) FOR [tgl_masuk]
GO
ALTER TABLE [dbo].[Obat] ADD  CONSTRAINT [DF_Obat_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Pasien] ADD  CONSTRAINT [DF_Pasien_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Pembelian] ADD  CONSTRAINT [DF_Pembelian_tgl_masuk]  DEFAULT (getdate()) FOR [tgl_masuk]
GO
ALTER TABLE [dbo].[Resep] ADD  CONSTRAINT [DF_Resep_tgl_resep]  DEFAULT (getdate()) FOR [tgl_resep]
GO
ALTER TABLE [dbo].[Resep] ADD  CONSTRAINT [DF_Resep_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Reservasi] ADD  CONSTRAINT [DF_Appointment_tgl_appointment]  DEFAULT (getdate()) FOR [tgl_reservasi]
GO
ALTER TABLE [dbo].[Reservasi] ADD  CONSTRAINT [DF_Appointment_status]  DEFAULT ('M') FOR [status_reservasi]
GO
ALTER TABLE [dbo].[Satuan] ADD  CONSTRAINT [DF_Satuan_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Supplier] ADD  CONSTRAINT [DF_Supplier_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Tabel_User] ADD  CONSTRAINT [DF_Tabel_User_foto]  DEFAULT ('default.jpg') FOR [foto]
GO
ALTER TABLE [dbo].[Tabel_User] ADD  CONSTRAINT [DF_Tabel_User_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[DetilPembelian]  WITH CHECK ADD  CONSTRAINT [FK_ID_Obat_Detail] FOREIGN KEY([id_obat])
REFERENCES [dbo].[Obat] ([id_obat])
GO
ALTER TABLE [dbo].[DetilPembelian] CHECK CONSTRAINT [FK_ID_Obat_Detail]
GO
ALTER TABLE [dbo].[DetilPembelian]  WITH CHECK ADD  CONSTRAINT [FK_No_Pembelian_Detail] FOREIGN KEY([no_pembelian])
REFERENCES [dbo].[Pembelian] ([no_pembelian])
GO
ALTER TABLE [dbo].[DetilPembelian] CHECK CONSTRAINT [FK_No_Pembelian_Detail]
GO
ALTER TABLE [dbo].[DetilResep]  WITH CHECK ADD  CONSTRAINT [FK_Detil_Resep] FOREIGN KEY([id_resep])
REFERENCES [dbo].[Resep] ([id_resep])
GO
ALTER TABLE [dbo].[DetilResep] CHECK CONSTRAINT [FK_Detil_Resep]
GO
ALTER TABLE [dbo].[DetilResep]  WITH CHECK ADD  CONSTRAINT [FK_DetilResep_Obat] FOREIGN KEY([id_obat])
REFERENCES [dbo].[Obat] ([id_obat])
GO
ALTER TABLE [dbo].[DetilResep] CHECK CONSTRAINT [FK_DetilResep_Obat]
GO
ALTER TABLE [dbo].[Obat]  WITH CHECK ADD  CONSTRAINT [FK_Kategori_Obat] FOREIGN KEY([id_kategori])
REFERENCES [dbo].[Kategori] ([id_kategori])
GO
ALTER TABLE [dbo].[Obat] CHECK CONSTRAINT [FK_Kategori_Obat]
GO
ALTER TABLE [dbo].[Obat]  WITH CHECK ADD  CONSTRAINT [FK_Satuan_Obat] FOREIGN KEY([id_satuan])
REFERENCES [dbo].[Satuan] ([id_satuan])
GO
ALTER TABLE [dbo].[Obat] CHECK CONSTRAINT [FK_Satuan_Obat]
GO
ALTER TABLE [dbo].[Pembelian]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Pembelian] FOREIGN KEY([id_supplier])
REFERENCES [dbo].[Supplier] ([id_supplier])
GO
ALTER TABLE [dbo].[Pembelian] CHECK CONSTRAINT [FK_Supplier_Pembelian]
GO
ALTER TABLE [dbo].[Pembelian]  WITH CHECK ADD  CONSTRAINT [FK_User_Pembelian] FOREIGN KEY([id_user])
REFERENCES [dbo].[Tabel_User] ([id_user])
GO
ALTER TABLE [dbo].[Pembelian] CHECK CONSTRAINT [FK_User_Pembelian]
GO
ALTER TABLE [dbo].[Penjualan]  WITH CHECK ADD  CONSTRAINT [FK_Member_Penjualan] FOREIGN KEY([id_member])
REFERENCES [dbo].[Member] ([id_member])
GO
ALTER TABLE [dbo].[Penjualan] CHECK CONSTRAINT [FK_Member_Penjualan]
GO
ALTER TABLE [dbo].[Penjualan]  WITH CHECK ADD  CONSTRAINT [FK_User_Penjualan] FOREIGN KEY([id_user])
REFERENCES [dbo].[Tabel_User] ([id_user])
GO
ALTER TABLE [dbo].[Penjualan] CHECK CONSTRAINT [FK_User_Penjualan]
GO
ALTER TABLE [dbo].[Penjualan_Obat]  WITH CHECK ADD  CONSTRAINT [FK_ID_Obat_Penjualan] FOREIGN KEY([id_obat])
REFERENCES [dbo].[Obat] ([id_obat])
GO
ALTER TABLE [dbo].[Penjualan_Obat] CHECK CONSTRAINT [FK_ID_Obat_Penjualan]
GO
ALTER TABLE [dbo].[Penjualan_Obat]  WITH CHECK ADD  CONSTRAINT [FK_No_Penjualan_Obat] FOREIGN KEY([no_penjualan])
REFERENCES [dbo].[Penjualan] ([no_penjualan])
GO
ALTER TABLE [dbo].[Penjualan_Obat] CHECK CONSTRAINT [FK_No_Penjualan_Obat]
GO
ALTER TABLE [dbo].[Resep]  WITH CHECK ADD  CONSTRAINT [FK_Dokter_Resep] FOREIGN KEY([id_dokter])
REFERENCES [dbo].[Dokter] ([id_dokter])
GO
ALTER TABLE [dbo].[Resep] CHECK CONSTRAINT [FK_Dokter_Resep]
GO
ALTER TABLE [dbo].[Resep]  WITH CHECK ADD  CONSTRAINT [FK_Pasien_Resep] FOREIGN KEY([id_pasien])
REFERENCES [dbo].[Pasien] ([id_pasien])
GO
ALTER TABLE [dbo].[Resep] CHECK CONSTRAINT [FK_Pasien_Resep]
GO
ALTER TABLE [dbo].[Reservasi]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Pasien] FOREIGN KEY([id_pasien])
REFERENCES [dbo].[Pasien] ([id_pasien])
GO
ALTER TABLE [dbo].[Reservasi] CHECK CONSTRAINT [FK_Appointment_Pasien]
GO
ALTER TABLE [dbo].[Reservasi]  WITH CHECK ADD  CONSTRAINT [FK_Dokter] FOREIGN KEY([id_dokter])
REFERENCES [dbo].[Dokter] ([id_dokter])
GO
ALTER TABLE [dbo].[Reservasi] CHECK CONSTRAINT [FK_Dokter]
GO
/****** Object:  StoredProcedure [dbo].[sp_Check_UserPass]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Dokter_Create]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Dokter_Delete]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Dokter_GetDokter]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Dokter_Login]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Dokter_Read]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Dokter_Update]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Kategori_Create]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Kategori_Delete]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Kategori_Read]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Kategori_Update]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Member_Create]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Member_Delete]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Member_GetLast]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Member_GetMember]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Member_Read]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Member_Update]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Obat_Create]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Obat_Delete]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Obat_GetKategori]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Obat_GetLast]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Obat_Read]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Obat_Update]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Pasien_Create]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Pasien_Delete]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Pasien_Read]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Pasien_Update]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Penjualan_Detail]    Script Date: 7/2/2020 2:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Penjualan_Detail]
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

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Satuan_Create]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Satuan_Delete]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Satuan_Read]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Satuan_Update]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Supplier_Create]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Supplier_Delete]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Supplier_Read]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Supplier_Update]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Beli_Obat]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Pembelian]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Pembelian_Detail]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Pembelian_GetLast]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Penjualan]    Script Date: 7/2/2020 2:18:13 PM ******/
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

END
GO
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Penjualan_GetLast]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Resep]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Resep_Detail]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Resep_GetLast]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Reservasi]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Reservasi_GetCount]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Reservasi_GetData]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Reservasi_GetLast]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_Transaksi_Reservasi_UpdateStatus]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_User_Create]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_User_Delete]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_User_Login]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_User_Read]    Script Date: 7/2/2020 2:18:13 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_User_Update]    Script Date: 7/2/2020 2:18:13 PM ******/
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
