USE [master]
GO
/****** Object:  Database [AgnejaTest]    Script Date: 14-06-2023 21:30:45 ******/
CREATE DATABASE [AgnejaTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AgnejaTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AgnejaTest.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AgnejaTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AgnejaTest_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AgnejaTest] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AgnejaTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AgnejaTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AgnejaTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AgnejaTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AgnejaTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AgnejaTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [AgnejaTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AgnejaTest] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [AgnejaTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AgnejaTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AgnejaTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AgnejaTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AgnejaTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AgnejaTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AgnejaTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AgnejaTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AgnejaTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AgnejaTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AgnejaTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AgnejaTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AgnejaTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AgnejaTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AgnejaTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AgnejaTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AgnejaTest] SET RECOVERY FULL 
GO
ALTER DATABASE [AgnejaTest] SET  MULTI_USER 
GO
ALTER DATABASE [AgnejaTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AgnejaTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AgnejaTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AgnejaTest] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AgnejaTest', N'ON'
GO
USE [AgnejaTest]
GO
/****** Object:  StoredProcedure [dbo].[AddEmployee]    Script Date: 14-06-2023 21:30:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddEmployee]
@EmployeeID int,
@FirstName varchar(30)='',
@LastName varchar(30)='',
@Address1 varchar(100)='',
@PayPerHour decimal(18,2)=null,
@ManagerID int=null,
@SupervisorID int=null,
@AnnualSalary decimal(18,2)=null,
@MaxExpenseAmount decimal(18,2)=null,
@IsManager bit,
@IsSupervisor bit,
@Action varchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @empId int 

	
    -- Insert statements for procedure here
	INSERT INTO [dbo].[Employee]
           ([FirstName]
           ,[LastName]
           ,[Address1]
           ,[PayPerHour]
           ,[ManagerID]
           ,[SupervisorID]
           ,[AnnualSalary]
           ,[MaxExpenseAmount]
		   ,[IsManager]
		   ,[IsSupervisor]
		   ,[CreatedBy]
		   ,[CreatedOn])
     VALUES
           (@FirstName
		   ,@LastName
		   ,@Address1
		   ,@PayPerHour
		   ,@ManagerID
		   ,@SupervisorID
		   ,@AnnualSalary
		   ,@MaxExpenseAmount
		   ,@IsManager
		   ,@IsSupervisor
		   ,1
		   ,Getdate()
		   )

		   IF(@IsManager=1)
		   BEGIN
		   	select @empId = Scope_Identity()
			UPDATE [dbo].[Employee] SET ManagerID=@empId WHERE EmployeeID=@empId
		   END

		    IF(@IsSupervisor=1)
		   BEGIN
		   	select @empId = Scope_Identity()
			UPDATE [dbo].[Employee] SET SupervisorID=@empId WHERE EmployeeID=@empId
		   END

END

GO
/****** Object:  StoredProcedure [dbo].[GetAllEmployee]    Script Date: 14-06-2023 21:30:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllEmployee]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT emp.EmployeeID,
	ISNULL((emp.FirstName+' '+ emp.LastName),'') EmployeeName 
	,ISNULL(emp.Address1,'') AS EmployeeAddress1
	,ISNULL(emp.PayPerHour,0) AS PayPerHour

	,ISNULL((Manager.FirstName+' '+ Manager.LastName),'') ManagerName
	,ISNULL(Manager.Address1,'') AS ManagerAddress1
	,ISNULL(Manager.AnnualSalary,0) AS ManagerAnnualSalary
	,ISNULL(Manager.MaxExpenseAmount,0) AS MaxExpenseAmount

	,ISNULL((Supervisor.FirstName+' '+ Supervisor.LastName),'')  SupervisorName
	,ISNULL(Supervisor.Address1,'') As SupervisorAddress1
	,ISNULL(Supervisor.AnnualSalary,0) AS SupervisorAnnualSalary
	FROM Employee emp 
	LEFT JOIN Employee as Manager ON emp.ManagerID=Manager.ManagerID
	LEFT JOIN Employee as Supervisor ON emp.EmployeeID=Supervisor.SupervisorID

END

GO
/****** Object:  StoredProcedure [dbo].[GetManagerList]    Script Date: 14-06-2023 21:30:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetManagerList]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ManagerID ,FirstName+''+ LastName AS ManagerName FROM Employee where IsManager=1
END

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 14-06-2023 21:30:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](30) NULL,
	[LastName] [varchar](30) NULL,
	[Address1] [varchar](100) NULL,
	[PayPerHour] [decimal](18, 2) NULL,
	[ManagerID] [int] NULL,
	[SupervisorID] [int] NULL,
	[AnnualSalary] [decimal](18, 2) NULL,
	[MaxExpenseAmount] [decimal](18, 2) NULL,
	[IsManager] [bit] NULL,
	[IsSupervisor] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

GO
INSERT [dbo].[Employee] ([EmployeeID], [FirstName], [LastName], [Address1], [PayPerHour], [ManagerID], [SupervisorID], [AnnualSalary], [MaxExpenseAmount], [IsManager], [IsSupervisor], [CreatedBy], [CreatedOn]) VALUES (12, N'Abhishek', N'Chaddha', N'Pune', NULL, 12, 0, CAST(1800000.00 AS Decimal(18, 2)), CAST(10000.00 AS Decimal(18, 2)), 1, 0, 1, CAST(0x0000B021015E12EA AS DateTime))
GO
INSERT [dbo].[Employee] ([EmployeeID], [FirstName], [LastName], [Address1], [PayPerHour], [ManagerID], [SupervisorID], [AnnualSalary], [MaxExpenseAmount], [IsManager], [IsSupervisor], [CreatedBy], [CreatedOn]) VALUES (13, N'Kuntal', N'Patel', N'Pune', CAST(1200.00 AS Decimal(18, 2)), 12, 0, NULL, NULL, 0, 0, 1, CAST(0x0000B021015E341E AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
USE [master]
GO
ALTER DATABASE [AgnejaTest] SET  READ_WRITE 
GO
