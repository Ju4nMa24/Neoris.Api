USE [master]
GO
/****** Object:  Database [NeorisDB]    Script Date: 20/7/2023 23:03:31 ******/
CREATE DATABASE [NeorisDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NeorisDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\NeorisDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NeorisDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\NeorisDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NeorisDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NeorisDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NeorisDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NeorisDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NeorisDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NeorisDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NeorisDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [NeorisDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [NeorisDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NeorisDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NeorisDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NeorisDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NeorisDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NeorisDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NeorisDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NeorisDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NeorisDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [NeorisDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NeorisDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NeorisDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NeorisDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NeorisDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NeorisDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [NeorisDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NeorisDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NeorisDB] SET  MULTI_USER 
GO
ALTER DATABASE [NeorisDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NeorisDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NeorisDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NeorisDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NeorisDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NeorisDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [NeorisDB] SET QUERY_STORE = OFF
GO
USE [NeorisDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 20/7/2023 23:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 20/7/2023 23:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[AccountNumber] [nvarchar](50) NOT NULL,
	[AccountType] [nvarchar](15) NOT NULL,
	[OpeningBalance] [float] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 20/7/2023 23:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ClientId] [uniqueidentifier] NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 20/7/2023 23:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PersonId] [uniqueidentifier] NOT NULL,
	[PersonName] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](20) NOT NULL,
	[Years] [int] NOT NULL,
	[Identification] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 20/7/2023 23:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[TransactionId] [uniqueidentifier] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
	[TransactionDate] [datetime2](7) NOT NULL,
	[TransactionType] [nvarchar](20) NOT NULL,
	[TransactionValue] [float] NOT NULL,
	[Balance] [float] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230721003741_Initial', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230721010520_Initial', N'7.0.9')
GO
INSERT [dbo].[Account] ([AccountId], [ClientId], [AccountNumber], [AccountType], [OpeningBalance], [Status]) VALUES (N'a3abce31-8793-4ee4-699a-08db899d4a84', N'080476f1-a5f4-45d0-3e3f-08db8992b763', N'478758', N'Ahorro', 2000, 1)
GO
INSERT [dbo].[Client] ([ClientId], [PersonId], [Password], [Status]) VALUES (N'080476f1-a5f4-45d0-3e3f-08db8992b763', N'0d62edc9-f020-4ec7-97cf-08db8986c32b', N'1234', 1)
GO
INSERT [dbo].[Person] ([PersonId], [PersonName], [Gender], [Years], [Identification], [Address], [Phone]) VALUES (N'0d62edc9-f020-4ec7-97cf-08db8986c32b', N'Jose Lema', N'Masculino', 30, N'1932404293', N'Otavalo sn y principal', N'098254785')
INSERT [dbo].[Person] ([PersonId], [PersonName], [Gender], [Years], [Identification], [Address], [Phone]) VALUES (N'1df8c570-629b-4829-6528-08db89872ccc', N'Marianela Montalvo', N'Femenino', 30, N'19324293', N'Amazonas y NNUU', N'097548965')
INSERT [dbo].[Person] ([PersonId], [PersonName], [Gender], [Years], [Identification], [Address], [Phone]) VALUES (N'8a1905e1-c3e5-4b49-6529-08db89872ccc', N'Juan Osorio', N'Masculino', 30, N'29324293', N'13 junio y Equinoccial', N'098874587')
GO
/****** Object:  Index [IX_Account_ClientId]    Script Date: 20/7/2023 23:03:32 ******/
CREATE NONCLUSTERED INDEX [IX_Account_ClientId] ON [dbo].[Account]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Client_PersonId]    Script Date: 20/7/2023 23:03:32 ******/
CREATE NONCLUSTERED INDEX [IX_Client_PersonId] ON [dbo].[Client]
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transaction_AccountId]    Script Date: 20/7/2023 23:03:32 ******/
CREATE NONCLUSTERED INDEX [IX_Transaction_AccountId] ON [dbo].[Transaction]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ('98d91f05-21de-467b-be91-a275d57ff123') FOR [AccountId]
GO
ALTER TABLE [dbo].[Client] ADD  DEFAULT ('a48029ec-da36-496c-8fff-502c1900156c') FOR [ClientId]
GO
ALTER TABLE [dbo].[Person] ADD  DEFAULT ('7443432a-fe07-4b47-aa4b-72caccce82ea') FOR [PersonId]
GO
ALTER TABLE [dbo].[Transaction] ADD  DEFAULT ('88627fb0-e1a9-49b7-948c-935e7723f4f6') FOR [TransactionId]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Client_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Client_ClientId]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Person_PersonId] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Person_PersonId]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Account_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Account_AccountId]
GO
USE [master]
GO
ALTER DATABASE [NeorisDB] SET  READ_WRITE 
GO
