USE [master]
GO
/****** Object:  Database [IdentityDb]    Script Date: 11/2/2019 1:24:38 AM ******/
CREATE DATABASE [IdentityDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IdentityDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\IdentityDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IdentityDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\IdentityDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [IdentityDb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IdentityDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IdentityDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IdentityDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IdentityDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IdentityDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IdentityDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [IdentityDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IdentityDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IdentityDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IdentityDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IdentityDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IdentityDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IdentityDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IdentityDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IdentityDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IdentityDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [IdentityDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IdentityDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IdentityDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IdentityDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IdentityDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IdentityDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [IdentityDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IdentityDb] SET RECOVERY FULL 
GO
ALTER DATABASE [IdentityDb] SET  MULTI_USER 
GO
ALTER DATABASE [IdentityDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IdentityDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IdentityDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IdentityDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IdentityDb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'IdentityDb', N'ON'
GO
ALTER DATABASE [IdentityDb] SET QUERY_STORE = OFF
GO
USE [IdentityDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/2/2019 1:24:38 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/2/2019 1:24:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoleses]    Script Date: 11/2/2019 1:24:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleses](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoleses] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/2/2019 1:24:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[Gender] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191101210719_initial_identity_db', N'3.0.0')
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name], [Description]) VALUES (1, N'ADMIN', N'system administrator')
INSERT [dbo].[Roles] ([Id], [Name], [Description]) VALUES (2, N'WRITER', N'blog writer')
INSERT [dbo].[Roles] ([Id], [Name], [Description]) VALUES (3, N'SUPER-ADMIN', N'system super administrator')
SET IDENTITY_INSERT [dbo].[Roles] OFF
INSERT [dbo].[UserRoleses] ([UserId], [RoleId]) VALUES (N'08a029db-1b12-4535-863a-f4ad30422219', 1)
INSERT [dbo].[UserRoleses] ([UserId], [RoleId]) VALUES (N'08a029db-1b12-4535-863a-f4ad30422219', 2)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [FullName], [Gender], [SecurityStamp]) VALUES (N'08a029db-1b12-4535-863a-f4ad30422219', N'admin', N'123456', N'Yaser Balaghi', 1, N'a4459390-dda1-4404-8c65-61c5e2f9070f')
/****** Object:  Index [IX_UserRoleses_RoleId]    Script Date: 11/2/2019 1:24:38 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoleses_RoleId] ON [dbo].[UserRoleses]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserRoleses]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleses_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleses] CHECK CONSTRAINT [FK_UserRoleses_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoleses]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleses_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleses] CHECK CONSTRAINT [FK_UserRoleses_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [IdentityDb] SET  READ_WRITE 
GO
