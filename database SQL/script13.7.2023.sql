USE [master]
GO
/****** Object:  Database [BAWebManager]    Script Date: 7/13/2023 10:50:00 AM ******/
CREATE DATABASE [BAWebManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BAWebManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BAWebManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BAWebManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BAWebManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BAWebManager] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BAWebManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BAWebManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BAWebManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BAWebManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BAWebManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BAWebManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [BAWebManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BAWebManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BAWebManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BAWebManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BAWebManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BAWebManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BAWebManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BAWebManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BAWebManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BAWebManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BAWebManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BAWebManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BAWebManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BAWebManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BAWebManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BAWebManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BAWebManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BAWebManager] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BAWebManager] SET  MULTI_USER 
GO
ALTER DATABASE [BAWebManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BAWebManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BAWebManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BAWebManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BAWebManager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BAWebManager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BAWebManager] SET QUERY_STORE = ON
GO
ALTER DATABASE [BAWebManager] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BAWebManager]
GO
/****** Object:  User [trungnq2]    Script Date: 7/13/2023 10:50:01 AM ******/
CREATE USER [trungnq2] FOR LOGIN [trungnq2] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[Order] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateEdited] [datetime2](7) NOT NULL,
	[Creator] [int] NOT NULL,
	[Editor] [int] NOT NULL,
 CONSTRAINT [PK_SysMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_SysRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sex]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sex](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_GioiTinh] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[SexId] [int] NOT NULL,
	[Phone] [nchar](15) NOT NULL,
	[Birthday] [datetime2](7) NOT NULL,
	[Email] [nchar](200) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[Creator] [int] NOT NULL,
	[Editor] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateEdited] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SysUserInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[RoleId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SysUserRole] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[UserId] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[UserId] [int] NOT NULL,
	[Token] [nvarchar](50) NOT NULL,
	[ExpiredDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SysUserToken] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([Id], [Name], [IsActive], [IsDelete], [Order], [DateCreated], [DateEdited], [Creator], [Editor]) VALUES (1, N'Quản lý người dùng', 1, 0, N'1', CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), 1, 1)
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Action], [Name]) VALUES (1, N'show', N'xem')
INSERT [dbo].[Role] ([Id], [Action], [Name]) VALUES (2, N'add', N'thêm')
INSERT [dbo].[Role] ([Id], [Action], [Name]) VALUES (3, N'edit', N'sửa')
INSERT [dbo].[Role] ([Id], [Action], [Name]) VALUES (4, N'delete', N'xóa')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
INSERT [dbo].[Sex] ([Id], [Name]) VALUES (1, N'Nam')
INSERT [dbo].[Sex] ([Id], [Name]) VALUES (2, N'Nữ')
INSERT [dbo].[Sex] ([Id], [Name]) VALUES (3, N'Khác')
GO
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (2, N'sa1                                               ', N'96E79218965EB72C92A549DD5A330112', N'Lê A', 1, N'432423234      ', CAST(N'1999-12-31T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 0, 1, 1, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-28T11:27:01.4866667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (3, N'sa2                                               ', N'96E79218965EB72C92A549DD5A330112', N'ceeeeeeee', 1, N'2222222222     ', CAST(N'1994-03-02T03:00:00.0000000' AS DateTime2), N'ewqewq@mm.com                                                                                                                                                                                           ', 0, 1, 1, 1, 1, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-28T11:35:09.5866667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (4, N'sa3                                               ', N'C4CA4238A0B923820DCC509A6F75849B', N'Lê C', 3, N'789789798      ', CAST(N'2002-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 1, 2, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-07-12T14:34:35.7800000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (5, N'sa4                                               ', N'C4CA4238A0B923820DCC509A6F75849B', N'Lê d', 1, N'46564644       ', CAST(N'2004-01-01T00:00:00.0000000' AS DateTime2), N'vxcvxc@gmail.com                                                                                                                                                                                        ', 1, 1, 1, 1, 2, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-07-12T14:34:35.7800000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (6, N'sa5                                               ', N'C4CA4238A0B923820DCC509A6F75849B', N'Lee Tee', 2, N'8688685        ', CAST(N'2002-02-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 1, 2, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-07-12T14:34:35.7833333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (7, N'sa11                                              ', N'C4CA4238A0B923820DCC509A6F75849B', N'Paul Lee', 3, N'65454545       ', CAST(N'2002-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 1, 2, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-12T09:45:20.3966667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (8, N'cx1                                               ', N'C4CA4238A0B923820DCC509A6F75849B', N'Ball Pool', 1, N'2121212        ', CAST(N'2002-01-09T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 1, 3, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-26T18:04:10.7133333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (9, N'ad1                                               ', N'C4CA4238A0B923820DCC509A6F75849B', N'Anh Tran', 3, N'234234         ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 1, 3, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-26T18:04:10.7266667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (10, N'ad2                                               ', N'C4CA4238A0B923820DCC509A6F75849B', N'Hoan Vu 99', 3, N'5858585858     ', CAST(N'2003-01-30T10:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 0, 1, 1, 2, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-22T11:49:39.9100000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (11, N'ad3                                               ', N'C4CA4238A0B923820DCC509A6F75849B', N'Deen Acc', 2, N'21212121       ', CAST(N'2002-01-02T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 1, 2, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-12T09:20:41.4100000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (12, N'ad4                                               ', N'D41D8CD98F00B204E9800998ECF8427E', N'An Na11', 2, N'8888888888     ', CAST(N'2000-02-02T00:00:00.0000000' AS DateTime2), N'1@zz.mn                                                                                                                                                                                                 ', 0, 0, 1, 1, 2, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-12T09:25:09.1000000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (13, N'sa12                                              ', N'D41D8CD98F00B204E9800998ECF8427E', N'Kean Roy', 1, N'5454544        ', CAST(N'2002-11-01T00:00:00.0000000' AS DateTime2), N'ew@vc.nm                                                                                                                                                                                                ', 0, 1, 1, 1, 3, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-07-11T08:38:08.5978688' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (14, N'sa13                                              ', N'C4CA4238A0B923820DCC509A6F75849B', N'Reeb Bob', 1, N'69692226       ', CAST(N'2002-01-29T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 1, 2, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-13T11:44:11.9400000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (15, N'sa14                                              ', N'C4CA4238A0B923820DCC509A6F75849B', N'Reen Tea', 3, N'43434333       ', CAST(N'2001-01-29T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 1, 2, CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-19T08:32:56.6666667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (16, N'kl1                                               ', N'C4CA4238A0B923820DCC509A6F75849B', N'Cao Lan111', 1, N'456468         ', CAST(N'2000-02-03T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-07T11:58:32.2833333' AS DateTime2), CAST(N'2023-06-12T09:41:51.4666667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (17, N'33232r                                            ', N'D41D8CD98F00B204E9800998ECF8427E', N'lplp', 1, N'2323323        ', CAST(N'2000-01-05T00:00:00.0000000' AS DateTime2), N'r23r3@mk.bn                                                                                                                                                                                             ', 0, 0, 1, 2, 2, CAST(N'2023-06-09T13:40:03.8800000' AS DateTime2), CAST(N'2023-06-22T16:35:13.9666667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (18, N'xcvxcv                                            ', N'C4CA4238A0B923820DCC509A6F75849B', N'lplp', 1, N'432423         ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'cvbvcbc@ed.nm                                                                                                                                                                                           ', 1, 1, 1, 2, 2, CAST(N'2023-06-09T13:41:51.7400000' AS DateTime2), CAST(N'2023-07-12T14:31:14.1733333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (19, N'trungnq                                           ', N'C4CA4238A0B923820DCC509A6F75849B', N'lplp', 1, N'2121212        ', CAST(N'1988-06-08T17:00:00.0000000' AS DateTime2), N'xzxzx                                                                                                                                                                                                   ', 1, 1, 1, 2, 2, CAST(N'2023-06-09T14:34:28.3933333' AS DateTime2), CAST(N'2023-07-12T14:34:35.7833333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (20, N'trungnq1                                          ', N'C4CA4238A0B923820DCC509A6F75849B', N'lplp', 1, N'3213123        ', CAST(N'2023-06-09T07:37:36.4140000' AS DateTime2), N'xcvcxxc                                                                                                                                                                                                 ', 1, 1, 1, 2, 2, CAST(N'2023-06-09T14:38:05.9900000' AS DateTime2), CAST(N'2023-07-12T14:31:14.1733333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (21, N'3232e                                             ', N'C4CA4238A0B923820DCC509A6F75849B', N'lplp', 1, N'312312         ', CAST(N'2023-06-08T04:18:07.7790000' AS DateTime2), N'2312e@bv.bn                                                                                                                                                                                             ', 1, 1, 1, 2, 2, CAST(N'2023-06-09T15:18:25.9700000' AS DateTime2), CAST(N'2023-06-23T09:38:02.1966667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (22, N'cvbvcvv                                           ', N'C4CA4238A0B923820DCC509A6F75849B', N'lplp', 1, N'3213232        ', CAST(N'2023-06-29T17:00:00.0000000' AS DateTime2), N'bvcbcv                                                                                                                                                                                                  ', 1, 1, 1, 2, 2, CAST(N'2023-06-09T16:26:57.8266667' AS DateTime2), CAST(N'2023-07-12T14:18:12.7300000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (23, N'khanhnq                                           ', N'C4CA4238A0B923820DCC509A6F75849B', N'Khánh', 1, N'312312         ', CAST(N'1999-09-08T17:00:00.0000000' AS DateTime2), N'cxz                                                                                                                                                                                                     ', 0, 1, 1, 2, 2, CAST(N'2023-06-09T16:40:50.1700000' AS DateTime2), CAST(N'2023-06-09T16:40:50.1700000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (25, N'ad123                                             ', N'C4CA4238A0B923820DCC509A6F75849B', N'Dec Za', 1, N'23432432       ', CAST(N'1993-02-02T17:00:00.0000000' AS DateTime2), N'321vxc@gmail.com                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-12T09:27:01.2900000' AS DateTime2), CAST(N'2023-07-04T14:08:52.5000000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (26, N'czx                                               ', N'C4CA4238A0B923820DCC509A6F75849B', N'cxz1123', 1, N'432            ', CAST(N'1994-03-02T17:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-13T10:32:46.9733333' AS DateTime2), CAST(N'2023-06-13T10:44:33.2100000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (27, N'4234                                              ', N'C4CA4238A0B923820DCC509A6F75849B', N'4324', 3, N'6666666666     ', CAST(N'1994-03-21T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-13T10:52:14.2666667' AS DateTime2), CAST(N'2023-07-04T14:08:46.1500000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (28, N'z111                                              ', N'96E79218965EB72C92A549DD5A330112', N'Lê Hoa', 1, N'2121           ', CAST(N'1994-03-02T17:00:00.0000000' AS DateTime2), N'a@gmail.com                                                                                                                                                                                             ', 1, 1, 1, 2, 2, CAST(N'2023-06-13T11:08:47.5333333' AS DateTime2), CAST(N'2023-07-12T14:31:14.1766667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (29, N'23432rr                                           ', N'96E79218965EB72C92A549DD5A330112', N'bcvbcv', 1, N'45435          ', CAST(N'1994-03-02T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 0, 2, 2, CAST(N'2023-06-13T11:27:21.4200000' AS DateTime2), CAST(N'2023-06-29T10:40:24.5666667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (30, N'321e                                              ', N'96E79218965EB72C92A549DD5A330112', N'321e1', 1, N'321312         ', CAST(N'1993-01-03T17:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-13T15:29:29.6766667' AS DateTime2), CAST(N'2023-06-23T15:25:45.6200000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (31, N'312e                                              ', N'96E79218965EB72C92A549DD5A330112', N'e222', 1, N'32321          ', CAST(N'1994-03-02T03:00:00.0000000' AS DateTime2), N'ewqewq@mm.com                                                                                                                                                                                           ', 1, 0, 1, 2, 2, CAST(N'2023-06-13T17:06:39.6066667' AS DateTime2), CAST(N'2023-06-29T10:40:24.5700000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (32, N'trungnq12                                         ', N'96E79218965EB72C92A549DD5A330112', N'trungnq12', 1, N'423432         ', CAST(N'1997-02-04T17:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-13T17:42:24.5233333' AS DateTime2), CAST(N'2023-06-29T10:39:22.4633333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (33, N'trungnq4                                          ', N'96E79218965EB72C92A549DD5A330112', N'Trung Nguyễn11', 1, N'4324324342     ', CAST(N'1993-02-02T17:00:00.0000000' AS DateTime2), N'auio@iouio.vn                                                                                                                                                                                           ', 1, 1, 1, 2, 2, CAST(N'2023-06-14T09:11:10.2300000' AS DateTime2), CAST(N'2023-07-12T14:31:14.1733333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (34, N'                                                  ', N'96E79218965EB72C92A549DD5A330112', N'', 1, N'sa2            ', CAST(N'1900-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-14T17:12:17.4066667' AS DateTime2), CAST(N'2023-06-22T17:04:34.4466667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (39, N'321r                                              ', N'96E79218965EB72C92A549DD5A330112', N'312t', 1, N'321            ', CAST(N'2023-06-09T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-14T17:36:41.6300000' AS DateTime2), CAST(N'2023-06-23T13:02:03.5166667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (43, N'ad123456                                          ', N'96E79218965EB72C92A549DD5A330112', N'cxz', 1, N'43243          ', CAST(N'1999-03-04T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-15T08:17:58.7533333' AS DateTime2), CAST(N'2023-06-15T08:17:58.7533333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (47, N'sa13212                                           ', N'96E79218965EB72C92A549DD5A330112', N'vxc', 1, N'7977987        ', CAST(N'1997-08-15T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-15T11:46:05.3300000' AS DateTime2), CAST(N'2023-06-15T11:46:05.3300000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (48, N'4234r                                             ', N'96E79218965EB72C92A549DD5A330112', N'bvcbcv443', 1, N'432            ', CAST(N'1997-01-31T03:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-16T10:24:06.8766667' AS DateTime2), CAST(N'2023-06-23T15:26:11.7033333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (49, N'432tyt                                            ', N'96E79218965EB72C92A549DD5A330112', N'oiu5', 1, N'432            ', CAST(N'1992-04-03T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 3, CAST(N'2023-06-16T10:39:18.4800000' AS DateTime2), CAST(N'2023-06-26T18:01:56.4733333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (50, N'mlb                                               ', N'96E79218965EB72C92A549DD5A330112', N'bnm1', 1, N'432423         ', CAST(N'1994-03-02T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-16T16:00:46.5200000' AS DateTime2), CAST(N'2023-06-16T16:00:46.5200000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (51, N'mnb4                                              ', N'96E79218965EB72C92A549DD5A330112', N'534t', 1, N'543            ', CAST(N'2000-03-31T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-16T16:18:01.4600000' AS DateTime2), CAST(N'2023-06-29T09:35:38.5100000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (52, N'423t44                                            ', N'96E79218965EB72C92A549DD5A330112', N'654654uu', 1, N'534            ', CAST(N'1997-02-27T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-16T16:18:37.8800000' AS DateTime2), CAST(N'2023-07-04T14:08:46.1533333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (55, N'aq                                                ', N'D41D8CD98F00B204E9800998ECF8427E', N'aq', 1, N'321312         ', CAST(N'1995-03-02T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 3, CAST(N'2023-06-19T08:47:10.8900000' AS DateTime2), CAST(N'2023-07-11T16:18:47.5512182' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (56, N'aq1                                               ', N'96E79218965EB72C92A549DD5A330112', N'aq1', 1, N'312312         ', CAST(N'1996-08-09T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-19T08:47:34.2000000' AS DateTime2), CAST(N'2023-06-19T08:47:34.2000000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (57, N'aq2                                               ', N'96E79218965EB72C92A549DD5A330112', N'aq2', 1, N'312312         ', CAST(N'1996-10-11T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-19T08:48:22.9900000' AS DateTime2), CAST(N'2023-06-19T08:48:22.9900000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (58, N'aq3                                               ', N'96E79218965EB72C92A549DD5A330112', N'aq3', 1, N'321            ', CAST(N'1998-11-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-19T08:49:22.2366667' AS DateTime2), CAST(N'2023-06-19T08:49:22.2366667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (59, N'aq4                                               ', N'96E79218965EB72C92A549DD5A330112', N'aq4', 1, N'312312         ', CAST(N'1988-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-19T08:49:42.4800000' AS DateTime2), CAST(N'2023-06-19T08:49:42.4800000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (62, N'aq11                                              ', N'96E79218965EB72C92A549DD5A330112', N'aq11', 1, N'13123          ', CAST(N'1992-04-03T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-19T16:09:45.7033333' AS DateTime2), CAST(N'2023-06-19T16:09:45.7033333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (65, N'sa22                                              ', N'96E79218965EB72C92A549DD5A330112', N'sa2', 1, N'43243          ', CAST(N'1993-03-04T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 1, CAST(N'2023-06-19T17:12:42.6333333' AS DateTime2), CAST(N'2023-06-28T16:02:54.5666667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (66, N'sa23                                              ', N'96E79218965EB72C92A549DD5A330112', N'sa23', 1, N'324324         ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 1, CAST(N'2023-06-19T17:13:40.2166667' AS DateTime2), CAST(N'2023-06-28T16:02:54.5666667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (67, N'az1111                                            ', N'96E79218965EB72C92A549DD5A330112', N'az11111', 1, N'798798798      ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-20T09:27:08.3500000' AS DateTime2), CAST(N'2023-06-20T09:27:08.3500000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (68, N'vxc                                               ', N'5796170C5CB22D3D40C92C56C6421A25', N'432w', 1, N'323232132      ', CAST(N'1992-04-03T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-22T11:38:12.2933333' AS DateTime2), CAST(N'2023-07-12T14:31:14.1733333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (69, N'czxxcccc                                          ', N'96E79218965EB72C92A549DD5A330112', N'czx', 1, N'43223          ', CAST(N'1993-07-08T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 2, 2, CAST(N'2023-06-22T11:43:17.5700000' AS DateTime2), CAST(N'2023-06-22T11:43:17.5700000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1068, N'321                                               ', N'D41D8CD98F00B204E9800998ECF8427E', N'321e', 1, N'22222          ', CAST(N'2000-01-02T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 0, 0, 2, 2, CAST(N'2023-06-22T15:25:14.5766667' AS DateTime2), CAST(N'2023-06-29T10:40:24.5700000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1069, N'324r                                              ', N'96E79218965EB72C92A549DD5A330112', N'423r', 1, N'43242          ', CAST(N'2001-01-30T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-22T16:31:33.2666667' AS DateTime2), CAST(N'2023-06-23T15:25:45.6200000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1070, N'3213333rree                                       ', N'D41D8CD98F00B204E9800998ECF8427E', N'432423rrr2121', 2, N'8888888888     ', CAST(N'2003-03-04T00:00:00.0000000' AS DateTime2), N'a@a.aa                                                                                                                                                                                                  ', 0, 0, 1, 2, 2, CAST(N'2023-06-22T16:32:04.3333333' AS DateTime2), CAST(N'2023-06-22T17:03:15.3400000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1073, N'324r111111                                        ', N'96E79218965EB72C92A549DD5A330112', N'321e', 1, N'123123         ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-23T09:19:45.9300000' AS DateTime2), CAST(N'2023-07-04T14:08:40.0400000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1082, N'3219696                                           ', N'96E79218965EB72C92A549DD5A330112', N'q1w1', 1, N'312321         ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-23T13:54:02.6066667' AS DateTime2), CAST(N'2023-06-29T09:30:15.0200000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1083, N'mnbmnbm                                           ', N'96E79218965EB72C92A549DD5A330112', N'mnbmbnmnb', 1, N'2432423432     ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-23T15:26:41.2766667' AS DateTime2), CAST(N'2023-07-12T14:19:18.5933333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1084, N'zcxczx                                            ', N'96E79218965EB72C92A549DD5A330112', N'zxcxzc', 1, N'234324         ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-23T15:27:11.1400000' AS DateTime2), CAST(N'2023-07-12T14:19:44.2133333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1085, N'bnbvbcbcv                                         ', N'96E79218965EB72C92A549DD5A330112', N'312e', 1, N'3131232        ', CAST(N'2001-02-07T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 2, 2, CAST(N'2023-06-23T15:27:34.2233333' AS DateTime2), CAST(N'2023-07-12T14:18:12.7266667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1087, N'sa22222222222                                     ', N'C4CA4238A0B923820DCC509A6F75849B', N'cvzcxz', 1, N'312312         ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 1, 2, CAST(N'2023-06-26T17:07:37.3633333' AS DateTime2), CAST(N'2023-07-12T14:34:35.7800000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1088, N'111                                               ', N'D41D8CD98F00B204E9800998ECF8427E', N'', 1, N'               ', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 0, 0, 0, 2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-30T08:13:17.8333333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1089, N'zazza                                             ', N'C4CA4238A0B923820DCC509A6F75849B', N'zzzzzzzz', 1, N'2121           ', CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 0, 0, CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2), CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1091, N'zazza1                                            ', N'C4CA4238A0B923820DCC509A6F75849B', N'zzzzzzzz', 1, N'2121           ', CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 0, 0, CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2), CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1094, N'zazza11                                           ', N'C4CA4238A0B923820DCC509A6F75849B', N'zzzzzzzz111', 1, N'2121           ', CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 0, 0, CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2), CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1095, N'zazza111                                          ', N'C4CA4238A0B923820DCC509A6F75849B', N'sssssss', 1, N'2121           ', CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 0, 0, CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2), CAST(N'2023-06-28T08:17:54.1960000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1097, N'mnbmbn11                                          ', N'96E79218965EB72C92A549DD5A330112', N'mnbmnb88', 1, N'23432          ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 0, 2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-29T09:35:38.5100000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1099, N'aq2e1e1e1                                         ', N'D41D8CD98F00B204E9800998ECF8427E', N'aq2222', 1, N'43423          ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 0, 2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-29T09:34:10.1833333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1100, N'zzz222222                                         ', N'96E79218965EB72C92A549DD5A330112', N'bmw', 1, N'234234         ', CAST(N'1999-07-04T03:39:48.8790000' AS DateTime2), N'string                                                                                                                                                                                                  ', 1, 1, 1, 2, 2, CAST(N'2023-07-04T03:39:48.8790000' AS DateTime2), CAST(N'2023-07-04T03:39:48.8790000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1101, N'zzz33333333333333                                 ', N'D41D8CD98F00B204E9800998ECF8427E', N'elon musk', 1, N'0999999999     ', CAST(N'1986-02-01T00:00:00.0000000' AS DateTime2), N'musk@mk.nm                                                                                                                                                                                              ', 1, 1, 1, 0, 2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-07-12T14:19:44.2166667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1102, N're3                                               ', N'96E79218965EB72C92A549DD5A330112', N'32w', 1, N'32323          ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1103, N'aq1111111111111                                   ', N'96E79218965EB72C92A549DD5A330112', N'cxzczxcxz', 1, N'32432          ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 0, 2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-07-05T10:59:38.3933333' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (1104, N'ouioiuoiu                                         ', N'52C69E3A57331081823331C4E69D3F2E', N'oiu9', 1, N'867867         ', CAST(N'2000-01-05T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (2101, N'sa                                                ', N'96E79218965EB72C92A549DD5A330112', N'1', 1, N'sa2            ', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (2102, N'Moon                                              ', N'96E79218965EB72C92A549DD5A330112', N'1e', 1, N'21             ', CAST(N'2000-02-03T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (2105, N'q1q1q1q1                                          ', N'96E79218965EB72C92A549DD5A330112', N'1', 1, N'1              ', CAST(N'2000-02-04T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 0, 1, 1, 0, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (2106, N'1zczczczczczcz                                    ', N'D41D8CD98F00B204E9800998ECF8427E', N'12222222222222222', 1, N'1              ', CAST(N'2000-01-30T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 0, 2, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-07-11T08:13:41.1666667' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (2107, N'xz11111111111111111                               ', N'C4CA4238A0B923820DCC509A6F75849B', N'1', 1, N'1              ', CAST(N'2023-07-10T10:51:38.5420000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 0, 0, CAST(N'2023-07-10T17:52:33.9345631' AS DateTime2), CAST(N'2023-07-10T17:52:35.4999956' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (2108, N'212121111111111111111111111111111111111111        ', N'D41D8CD98F00B204E9800998ECF8427E', N'13455435345345345', 1, N'1              ', CAST(N'2000-01-05T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 3, 2, CAST(N'2023-07-10T17:59:33.2150103' AS DateTime2), CAST(N'2023-07-11T08:13:41.1700000' AS DateTime2))
INSERT [dbo].[UserInfo] ([Id], [Username], [Password], [FullName], [SexId], [Phone], [Birthday], [Email], [IsDelete], [IsActive], [IsAdmin], [Creator], [Editor], [DateCreated], [DateEdited]) VALUES (2109, N'admin12                                           ', N'96E79218965EB72C92A549DD5A330112', N'nva', 1, N'1              ', CAST(N'2000-01-04T00:00:00.0000000' AS DateTime2), N'                                                                                                                                                                                                        ', 1, 1, 1, 3, 2, CAST(N'2023-07-11T08:37:27.1102197' AS DateTime2), CAST(N'2023-07-11T16:16:36.9233333' AS DateTime2))
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
GO
INSERT [dbo].[UserRole] ([RoleId], [UserId], [MenuId], [Action]) VALUES (1, 2, 1, N'show')
INSERT [dbo].[UserRole] ([RoleId], [UserId], [MenuId], [Action]) VALUES (2, 2, 1, N'add')
INSERT [dbo].[UserRole] ([RoleId], [UserId], [MenuId], [Action]) VALUES (3, 2, 1, N'edit')
GO
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'04FB7670-3760-458A-B80E-ABC994654B43', CAST(N'2023-06-13T14:25:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'05FA76F9-9E1B-49AC-8A74-B7F4F6D353BB', CAST(N'2023-06-12T16:03:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'087F0836-7131-4FEB-8928-56C05DDF0F8D', CAST(N'2023-06-12T16:35:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'0955BBFB-8513-4D4F-84B0-63EB5451E00E', CAST(N'2023-06-13T15:32:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'0EEAACF4-DF2A-4D17-AE04-557398DC2506', CAST(N'2023-06-12T15:49:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'275856C6-75DD-4F41-ABE0-9AD8D662D9CD', CAST(N'2023-06-12T15:59:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'2BE20D29-7150-4433-8277-217EBB1FD361', CAST(N'2023-06-13T16:53:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'32901860-DA40-4AFC-B204-063DD8D3B883', CAST(N'2023-06-13T13:50:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'403F6FDD-5D18-41B8-B79E-14E4D4047D19', CAST(N'2023-06-13T11:03:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'47D06770-8232-49EC-A66E-4A0B4E62A6CA', CAST(N'2023-06-13T14:25:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'539EB9A5-7D64-454D-A87C-604A17942279', CAST(N'2023-06-12T15:01:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'55F47D18-DD92-4153-B468-6C45FEB6F90F', CAST(N'2023-06-13T15:14:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'565146B8-2A6E-41C7-B0F6-15066DEDEB54', CAST(N'2023-06-13T09:55:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'5A72A0E7-083D-402E-A0B8-80DC9AEB789F', CAST(N'2023-06-13T16:44:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'5D768E9F-A2CF-4F8C-9469-196B3041E2C2', CAST(N'2023-06-13T10:53:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'78F08D0E-A783-434A-AA5D-875B65A95445', CAST(N'2023-06-13T08:39:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'79383AB5-49DB-4835-BF33-211826D74CC4', CAST(N'2023-06-13T11:48:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'81683C7E-23B8-41B2-BE69-C6B25C24FD19', CAST(N'2023-06-12T17:19:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'82252EC5-44AF-4714-9ED7-6807AF6CD10A', CAST(N'2023-06-12T16:39:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'8E3715F8-5AF0-4C38-8826-578502183C83', CAST(N'2023-06-13T09:21:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'A4B1C776-5187-4E2B-8241-35110242C8C7', CAST(N'2023-06-12T15:05:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'AFA48752-7343-47C8-BC23-5AD7CAA110D0', CAST(N'2023-06-13T14:24:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'B7CEDBDE-E3C5-4588-83E3-6A84D719239C', CAST(N'2023-06-13T09:21:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'BE8C2600-F26C-46D4-BB93-548A5F6D5314', CAST(N'2023-06-13T14:22:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'CC65400D-A685-46A4-BD38-5054AD3881A8', CAST(N'2023-06-13T14:26:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'CFA15877-AAF9-4DDC-BAF5-955E538A8FAC', CAST(N'2023-06-12T17:58:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'D75E24DD-D563-4E81-B7B8-066C068C35FB', CAST(N'2023-06-12T16:41:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'E573E20A-3F09-4B1C-B7FE-1D353438EA58', CAST(N'2023-06-13T14:56:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'E9C3ECAC-3B5D-45BA-BEBB-ADF7FCF0817A', CAST(N'2023-06-13T14:47:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'F715E2FC-E7FA-4347-8781-06BDC087E545', CAST(N'2023-06-30T17:18:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'FE1C5398-D46D-418A-90D6-68BD44E6AB04', CAST(N'2023-06-12T15:03:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (2, N'newid()', CAST(N'2025-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'060BAD8C-6AE7-4465-96F0-42EF5E876C1B', CAST(N'2023-06-14T10:23:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'081D8F7F-DF01-4B18-8F3A-4ABFFA2FC25E', CAST(N'2023-07-05T13:36:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'09A70866-D9A0-4BF5-8197-A7D96D53F976', CAST(N'2023-06-27T15:12:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'0A27E98B-5A3F-4450-8ED0-F656FE603D7D', CAST(N'2023-07-05T13:21:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'0AD6110C-C151-4AF4-84F0-DDE1F3B6034D', CAST(N'2023-06-13T16:43:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'0EE00F47-137D-42B7-BD21-105FE6585F83', CAST(N'2023-07-05T10:58:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'0FE1846B-54F4-4330-A612-60CB6C2D59A0', CAST(N'2023-06-14T11:15:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'125C7808-4F53-4FD7-8E28-DA085B898644', CAST(N'2023-06-14T10:06:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'1281E7B6-238B-48DB-A038-0400BEA8FDE0', CAST(N'2023-06-13T12:13:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'12D593D2-3093-44AC-A1BC-5BEE8501D84C', CAST(N'2023-06-13T14:19:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'13585CC1-4FDE-40FE-86C0-E37F459693E0', CAST(N'2023-06-13T16:52:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'1800A5A0-1345-4E20-AA0A-7D9F20C82FA5', CAST(N'2023-06-14T16:34:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'206022EF-4AD3-4FCF-B5D3-35984AFBAC88', CAST(N'2023-06-13T16:37:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'24E454C3-46BF-4659-B59D-2B9FF1C96566', CAST(N'2023-06-13T16:45:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'26E2D96D-A219-414A-8720-FD3A54BE6920', CAST(N'2023-06-13T16:48:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'280F63EB-B2A0-4E15-9B94-2026F74CC400', CAST(N'2023-07-12T16:13:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'2B326313-9185-4FEF-BDE5-1EB6677F61EB', CAST(N'2023-06-14T13:33:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'2C261436-5E7F-4FC4-A166-1A4F7A3FF191', CAST(N'2023-06-14T17:08:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'2C7F1E71-5094-419B-A145-7FF042FFEAAE', CAST(N'2023-06-24T08:12:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'30A731DA-E217-4133-B498-9F625E4DCE17', CAST(N'2023-06-14T11:09:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'30F7CF94-75B5-47D4-BAA1-78DA002C4F7A', CAST(N'2023-06-13T17:59:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'387E2E13-653D-4882-8B13-F7E5D42AF377', CAST(N'2023-07-05T10:39:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'395EE261-E481-4097-9704-04957193E7C4', CAST(N'2023-07-05T11:19:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'3B1F61E4-236B-4314-AF97-B3C58604EA9F', CAST(N'2023-06-30T17:18:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'3E0CC521-AE8B-4B93-9C25-5F3B6E7601CD', CAST(N'2023-06-30T17:50:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'3F12A8D4-7A3C-4B7E-B6E4-977AE84B9AFD', CAST(N'2023-06-20T08:10:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'421B72DB-4253-424F-A709-D94287E9184A', CAST(N'2023-06-27T15:54:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'43F077AF-B0A8-47B5-8DF8-E2418850BD00', CAST(N'2023-06-13T11:59:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'4673121D-F865-4323-B239-F7B4A19941F2', CAST(N'2023-06-13T17:35:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'473A40AA-722C-499D-8478-71A29DA7A298', CAST(N'2023-07-05T13:44:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'47EFAA20-DA0A-4ED1-B14D-70D16D9E233B', CAST(N'2023-06-20T18:00:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'4C6BF576-1D84-42E7-9C17-3D9D012F1755', CAST(N'2023-06-13T10:53:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'4DFC0BDA-B348-4202-A4CB-3A29DBB2DCFB', CAST(N'2023-06-13T12:13:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'4E7DD29A-7682-4D4C-8589-90B7C64B0B38', CAST(N'2023-07-05T08:16:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'4F44D6C0-C508-4B5D-8730-5FEC11543FEC', CAST(N'2023-06-13T18:24:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'501C98B4-7173-4B8B-BD76-A4D1EECEEF9B', CAST(N'2023-07-05T13:33:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'5108D825-BE44-4EEE-9352-3382FE191628', CAST(N'2023-06-14T11:22:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'5433910C-7DD3-442B-ABD7-3AD712156F18', CAST(N'2023-06-13T14:21:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'54709916-2B9F-496E-9760-C6E44809AC14', CAST(N'2023-06-29T14:20:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'548D6E8A-D0E1-4645-B75A-79A8CDC92608', CAST(N'2023-07-12T08:13:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'5844A226-258F-4BE0-AF60-9C2052F95320', CAST(N'2023-07-05T13:21:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'5ACB28A8-2F34-4AA2-8D5E-4312982D9033', CAST(N'2023-07-12T16:57:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'5DFA6098-06AE-4BE4-9708-CA4375088D63', CAST(N'2023-06-14T16:02:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'5FCA6C03-5104-4AAC-909F-F18331437DFF', CAST(N'2023-07-05T13:39:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'63B7B0E0-B30F-41C1-8AA5-FFFC428EC516', CAST(N'2023-06-13T12:28:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'65C7CAE8-106E-433D-8508-A95E26CE54B0', CAST(N'2023-06-29T14:28:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'6751DDF3-5222-48F5-AEF4-BDB1355FF504', CAST(N'2023-06-29T16:50:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'6F490697-5895-43C8-A36A-A11CC18CCAEC', CAST(N'2023-07-13T13:51:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'71B383BF-2EE6-4F7D-9167-039A70B3AC47', CAST(N'2023-06-14T09:27:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'7287C3AA-F760-47DF-A800-77D8A5807A82', CAST(N'2023-06-24T08:30:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'7C4F979E-5B1D-4FAF-9382-A5FE7E26682F', CAST(N'2023-06-30T16:57:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'7FFEAD87-779A-4ABB-902E-A38E9264C019', CAST(N'2023-06-13T14:33:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'82162084-A157-485D-B69D-1C98A5EACB98', CAST(N'2023-06-13T14:17:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'85517315-DF09-4A10-A748-1FECEB7A183B', CAST(N'2023-07-06T10:59:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'86BFB811-4627-42B5-8CED-D7FDC8E75607', CAST(N'2023-06-14T15:27:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'872395F0-F38A-4EC2-AE23-D86B062824F0', CAST(N'2023-06-27T15:59:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'8B79FBDF-D33D-41C8-BC60-97B2B65DB068', CAST(N'2023-07-05T10:37:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'8BC05EB9-28F4-4E2C-955B-329E3AB8F9C2', CAST(N'2023-06-30T17:54:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'8D57708C-1B32-4120-A551-849E73704E7A', CAST(N'2023-06-20T17:45:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'8EF14F62-BC11-44ED-B048-FCEB4F6BD5CB', CAST(N'2023-07-05T10:54:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'977E6C54-5C32-4C8C-BAF2-68174A77AC4F', CAST(N'2023-07-05T13:41:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'99607705-79A5-4AEE-B882-F0FAFA900B63', CAST(N'2023-06-14T17:42:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'9D6F0A19-D1E0-46E0-B271-3EFC7AD382D4', CAST(N'2023-06-14T14:47:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'A02C7582-3E64-426C-A081-7DF0BFA6A7C5', CAST(N'2023-07-05T13:35:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'A12894CB-1C80-4D73-828C-C332DF79D26E', CAST(N'2023-06-27T16:05:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'A1ACFC9E-6B61-4DFF-BA12-48E86DD989AD', CAST(N'2023-06-13T16:52:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'A21C2BD4-1FEB-43B4-9EAF-33C260379C93', CAST(N'2023-06-14T18:15:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'A34C2349-31C2-4709-B32D-B58E9838DBDD', CAST(N'2023-06-13T18:19:00.0000000' AS DateTime2))
GO
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'A7A0B633-7BB8-43A3-BE49-DCBBF4B06592', CAST(N'2023-06-12T17:25:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'A89E999A-2BCE-4F7D-B083-D6882C6E3806', CAST(N'2023-07-05T13:37:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'AAD4A020-BEDB-42C7-BD9E-1D00EF6130F3', CAST(N'2023-06-13T18:16:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'AE86001A-C758-438D-AD28-CCD75866BAE6', CAST(N'2023-06-30T17:09:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'AF896E93-DD60-4E6A-A1CB-1BD8D19083EB', CAST(N'2023-06-13T15:59:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'B319CF15-614F-4C58-81A8-CFDFEB1D320E', CAST(N'2023-06-14T11:23:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'B70FCAC6-C589-498F-8A89-8DB262684782', CAST(N'2023-06-13T12:07:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'BB342ADD-9D95-4BBD-ADF9-AA59E55F3D76', CAST(N'2023-06-13T18:06:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'BB69C46B-7AE0-43A6-A3A6-BB01AB54AFCA', CAST(N'2023-06-17T13:11:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'C0528701-CADF-4A28-AC7F-E9C818332343', CAST(N'2023-06-14T12:04:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'C5E640CF-36A4-459C-8A0F-331B4C36350B', CAST(N'2023-06-13T13:48:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'C5FAC84F-8A42-4FCD-89B4-7CFE3A5972BD', CAST(N'2023-06-14T14:13:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'C90CE3E2-DC04-46E0-BA2A-E96E50F2AE87', CAST(N'2023-06-13T10:20:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'CC500900-FFDA-437B-B082-6B767C590612', CAST(N'2023-06-21T09:45:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'CDF33E23-02FA-4379-AAAD-C37798A5CBC1', CAST(N'2023-06-14T08:38:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'D053B2E3-2639-4D8E-9401-959C3CF63CB1', CAST(N'2023-06-14T11:23:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'D1B9814C-1299-43C6-BDF0-11C6D5DBAFFC', CAST(N'2023-07-05T13:16:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'D24B16DE-3FD9-45C7-8EAE-0EB17BF32B1C', CAST(N'2023-06-13T13:40:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'D251753D-A915-49DE-B5FD-757A04B1935D', CAST(N'2023-06-13T16:25:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'D9E323E8-65FA-4F56-823E-E3FB0A332A62', CAST(N'2023-06-15T08:43:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'DA000EA1-9EE5-4ECC-8BB9-4A9A8D54912C', CAST(N'2023-06-13T12:10:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'DAF6F18A-BBA2-43A9-8394-21E90CB6A01E', CAST(N'2023-06-13T12:06:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'DD4D1858-8F47-4204-B677-A1DEEA66F746', CAST(N'2023-06-30T17:52:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'DDFBAA86-D79F-4767-9796-DB1C0217098A', CAST(N'2023-06-29T15:13:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'E705423B-CE36-4D67-A57B-8C9729556181', CAST(N'2023-06-14T11:46:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'E95D3443-E40A-4531-80DB-964A6D3C50A8', CAST(N'2023-06-13T16:09:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'EA56A471-5D30-49A8-9C20-6E76880420BA', CAST(N'2023-06-27T15:56:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'EDA39929-CBA0-4EE7-AC2F-DBCD77CDC0A3', CAST(N'2023-06-12T16:35:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'EEF2DF42-4DEB-442A-9735-4EAF945CFC7C', CAST(N'2023-06-13T12:30:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'F162C3CF-2EF2-4B82-A857-9C602C1AF895', CAST(N'2023-07-05T13:33:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'F54B9C35-E277-41D2-802E-D0DC797D7EE3', CAST(N'2023-06-13T15:58:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'F6D704F9-A3F9-473E-8344-CB6B50055BA2', CAST(N'2023-06-12T17:58:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'F8033123-977C-4B6E-80D9-AE6D94099CF6', CAST(N'2023-07-05T13:35:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'F99F2911-05EF-4BFA-8B40-C102FBFFF00D', CAST(N'2023-06-16T09:19:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (3, N'FD6BFFEF-4D66-4847-B68D-89FE2C4309CB', CAST(N'2023-06-15T09:06:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (17, N'37D54029-B5A8-42AF-BE38-ED34C7C65532', CAST(N'2023-06-12T16:38:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (19, N'03E37A5B-A5E9-483D-B5B9-99E38FA8153B', CAST(N'2023-06-12T17:25:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (19, N'D89EDA04-133E-403B-8CF3-9A01A49C5C41', CAST(N'2023-06-13T10:20:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (26, N'82B119D8-F468-40AB-B8D3-2320E31A7715', CAST(N'2023-06-13T11:03:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (26, N'B05DA3C5-6170-4142-B19C-1C7938ADA617', CAST(N'2023-06-13T11:12:00.0000000' AS DateTime2))
INSERT [dbo].[UserToken] ([UserId], [Token], [ExpiredDate]) VALUES (32, N'4E523462-88FB-4DC4-B3B8-9955C54FC82C', CAST(N'2023-06-13T18:13:00.0000000' AS DateTime2))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__SysUserI__F3DBC5726AB40EDB]    Script Date: 7/13/2023 10:50:01 AM ******/
ALTER TABLE [dbo].[UserInfo] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_SysUserInfo_username]  DEFAULT ('') FOR [Username]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_SysUserInfo_email]  DEFAULT ('') FOR [Email]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_SysUserInfo_cuser]  DEFAULT ((0)) FOR [Creator]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_SysUserInfo_luser]  DEFAULT ((0)) FOR [Editor]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_SysUserInfo_cdate]  DEFAULT ('1-1-1900') FOR [DateCreated]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_SysUserInfo_ldate]  DEFAULT ('1-1-1900') FOR [DateEdited]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_UserInfo] FOREIGN KEY([SexId])
REFERENCES [dbo].[Sex] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_UserInfo]
GO
/****** Object:  StoredProcedure [dbo].[BAWeb_User_CheckTokenLoginGetRole]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		trungnq	
-- Create date: 8/6/2023
-- Description:	check token login và get quyền theo menuid
-- =============================================
CREATE PROCEDURE [dbo].[BAWeb_User_CheckTokenLoginGetRole] 
	-- Add the parameters for the stored procedure here
	@token NVARCHAR(50) = 'trungnq', 
	@menuId INT = 1,
	 
	@ret INT =0 OUTPUT , -- = 0 đăng nhập thất bại, = 1 đăng nhập thành công
	@isAdmin BIT = false OUTPUT -- 1 -là admin, 0 - ko là admin
AS
BEGIN
	 DECLARE @count INT = 0, @userId INT
	SELECT @count = COUNT(1),@userId = userId FROM UserToken token
	left join UserInfo inf ON token.userId = inf.id and inf.isActive = 1 and inf.isDelete = 0
	WHERE token = @token and expiredDate > GETDATE() and inf.username is not null GROUP BY userId
	SET @ret = @count
	--print(@pret)
	IF @ret = 0
	SET @isAdmin = 0
	ELSE

	SELECT @isAdmin =  isAdmin FROM UserInfo WHERE  id = @userId

	SELECT [roleId]
      ,[userId]
      ,[menuId]
      ,[action] FROM UserRole WHERE userId = @userId
  
END
GO
/****** Object:  StoredProcedure [dbo].[BAWeb_User_DeleteUserInfo]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		trungnq3
-- Create date: 7/6/2023
-- Description:	thêm mới user
-- =============================================
CREATE PROCEDURE [dbo].[BAWeb_User_DeleteUserInfo]
 
	 @id INT,
	  @username NCHAR(50),
	 @userId INT,
	 @ret INT OUTPUT
	 
AS
BEGIN
	 UPDATE UserInfo SET 
	  isDelete = 1 ,
	 editor = @userId, dateEdited = GETDATE()
	 WHERE id = @id and username = @username
 
	 SET @ret = @@ERROR
END
 
GO
/****** Object:  StoredProcedure [dbo].[BAWeb_User_GetRole]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		trungnq	
-- Create date: 8/6/2023
-- Description:	get quyền theo menuid
-- =============================================
CREATE PROCEDURE [dbo].[BAWeb_User_GetRole] 
	
	@userId INT = 2, 
	@menuId INT = 1,
	@isAdmin BIT = 0 OUTPUT
AS
BEGIN
	 
	SELECT @isAdmin = isAdmin FROM UserInfo WHERE  id = @userId

	IF @isAdmin is null 
	SET @isAdmin = 0

	SELECT [roleId]
      ,[userId]
      ,[menuId]
      ,[action] FROM UserRole WHERE userId = @userId
 
END
GO
/****** Object:  StoredProcedure [dbo].[BAWeb_User_GetUserInfo]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<trungnq3>
-- Create date: <6/6/2023>
-- Description:	<lấy danh dách user phân trang>
-- =============================================
CREATE PROCEDURE [dbo].[BAWeb_User_GetUserInfo]
	 @userId INT = 1,
	 @pageNumber INT = 2,
	 @pageSize INT = 10,
	 @textSearch NVARCHAR(200) = '',
	 @birthdayFrom DATETIME2 = '1/1/1900',
	 @birthdayTo DATETIME2 = '1/1/1900',
	 @gioiTinhSearch INT = 0,
	 @count INT = 0 OUTPUT
	 
	 AS
BEGIN


 SELECT @count = COUNT(*)   FROM UserInfo

	 WHERE ( @textSearch  = '' or 
	 (fullName like '%' +@textSearch + '%'
	 or email like  '%'+@textSearch + '%'  or
	 phone like  '%'+@textSearch + '%' 
	 or username like  '%'+@textSearch + '%' )
	 ) and (@birthdayFrom is null or CAST( birthday AS DATE)>= CAST( @birthdayFrom AS DATE))
	  and (@birthdayTo is null  or birthday <=  @birthdayTo  )
	  and (@gioiTinhSearch = 0 or sexId =  @gioiTinhSearch )
	  and isDelete = 0
	   
	 SELECT 
	  [id]
      ,[username]
	  ,row_number() OVER (ORDER BY username)  stt
      ,'' [password]
      ,fullName
      ,[sexId]
      ,phone
      ,[birthday]
	  --,CONVERT (varchar(10), [ngay_sinh], 103) AS [ngay_sinh_text] 
      ,[email]
      ,[isDelete]
      ,[isActive]
      ,[isAdmin]
      ,creator
      ,editor
      ,dateCreated
      ,dateEdited
	  ,CASE
    WHEN sexId = 1 THEN N'Nam'
	WHEN sexId = 2 THEN N'Nữ'
	WHEN sexId = 3 THEN N'Khác'
    
    ELSE N'Không xác định'
END AS gioiTinhText
	  
	  FROM UserInfo

	 WHERE ( @textSearch  = '' or 
	 (fullName like '%' +@textSearch + '%'
	 or email like  '%'+@textSearch + '%'  or
	 phone like  '%'+@textSearch + '%' 
	 or username like  '%'+@textSearch + '%' )
	 ) and (@birthdayFrom is null or CAST( birthday AS DATE)>= CAST( @birthdayFrom AS DATE))
	  and (@birthdayTo is null  or birthday <=  @birthdayTo  )
	  and (@gioiTinhSearch = 0 or sexId =  @gioiTinhSearch )
	  and isDelete = 0

ORDER BY  username
OFFSET (@pageNumber-1)*@pageSize ROWS

FETCH NEXT @pageSize ROWS ONLY

 
 END




GO
/****** Object:  StoredProcedure [dbo].[BAWeb_User_Login]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		trungnq	
-- Create date: 8/6/2023
-- Description:	login đăng nhập
-- =============================================
CREATE PROCEDURE [dbo].[BAWeb_User_Login]
	-- Add the parameters for the stored procedure here
	@username NCHAR(50) = 'trungnq', 
	@pass NVARCHAR(MAX) = '1',
	@isRemember BIT = 0,
	@ret INT OUTPUT
AS
BEGIN
	 DECLARE @count INT = 0, @userId INT
	SELECT @count = COUNT(1),@userId = id FROM UserInfo 
	WHERE username = @Username and password = @Pass 
	and isDelete = 0 and isActive = 1 GROUP BY id	
	SET @ret = @count

	DECLARE @myId UNIQUEIDENTIFIER   ,@expiredDate SMALLDATETIME

	 

	if(@isRemember = 1 and @count <> 0)
	BEGIN
	
	SET @expiredDate = DATEADD(DAY,1,GETDATE())
SET @myId = NEWID() 

 
	INSERT INTO UserToken (userId,token,expiredDate) 
	VALUES (@userId,CAST(@myId AS NVARCHAR(50)),@expiredDate)
	SELECT  CAST(@myId AS NVARCHAR(50)) token, @userId userId,@Username username, @expiredDate expiredDate
	END
	ELSE
	BEGIN

 	SELECT  @userId userId,@Username username 

	END
	 
END
GO
/****** Object:  StoredProcedure [dbo].[BAWeb_User_UpdatePassUserInfo]    Script Date: 7/13/2023 10:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		trungnq3
-- Create date: 7/6/2023
-- Description:	đổi pass user
-- =============================================
CREATE PROCEDURE [dbo].[BAWeb_User_UpdatePassUserInfo]
  
	 @password NVARCHAR(max) = '',
	 @passwordOld NVARCHAR(max) = '',
	 @username NCHAR(50) = '',
	 @userId INT,
	 @ret INT OUTPUT
	 
AS
BEGIN
DECLARE @count INT = 0
  SELECT @count = COUNT(*) FROM UserInfo WHERE id = @userId and username = @username 
  and password = @passwordOld
   
   IF @count>0
   BEGIN 
   UPDATE UserInfo SET password = @password WHERE id = @userId and username = @username 
   and password = @passwordOld
   END
    
	 SET @Ret = @@ERROR
	 IF @count = 0
	 SET @Ret = 1
END
 
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id của menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tên của menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'có sử dụng menu này không' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'IsActive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'có xóa menu này không' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'dùng để sắp xếp' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'Order'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ngày tạo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'DateCreated'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ngày sửa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'DateEdited'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'người tạo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'người sửa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'Editor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id của quyền' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tên action rút gọn của quyền' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Action'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tên của quyền' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id của user' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tên đăng nhập' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Username'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'mật khẩu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'họ tên' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'FullName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'giới tính: 1: nam, 2: nữ, 3: khác' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'SexId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'số điện thoại' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ngày sinh' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Birthday'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'email' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'trạng thái xóa của user' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'trạng thái hoạt động của user' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'IsActive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'user có phải admin không' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'IsAdmin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'người tạo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'người sửa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Editor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ngày tạo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'DateCreated'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ngày sửa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'DateEdited'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id quyền' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRole', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id user' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRole', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRole', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'action tương ứng với rode id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRole', @level2type=N'COLUMN',@level2name=N'Action'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id của user' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserToken', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'token ở web' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserToken', @level2type=N'COLUMN',@level2name=N'Token'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'thời gian hết hạn' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserToken', @level2type=N'COLUMN',@level2name=N'ExpiredDate'
GO
USE [master]
GO
ALTER DATABASE [BAWebManager] SET  READ_WRITE 
GO
