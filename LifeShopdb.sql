USE [master]
GO

CREATE LOGIN [IIS APPPOOL\LifeShop] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Database [LifeShopData]    Script Date: 6/19/2023 12:31:23 AM ******/
CREATE DATABASE [LifeShopData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LifeShopData', FILENAME = N'C:\Users\Public\LifeShopData.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LifeShopData_log', FILENAME = N'C:\Users\Public\LifeShopData_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [LifeShopData] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LifeShopData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LifeShopData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LifeShopData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LifeShopData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LifeShopData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LifeShopData] SET ARITHABORT OFF 
GO
ALTER DATABASE [LifeShopData] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LifeShopData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LifeShopData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LifeShopData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LifeShopData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LifeShopData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LifeShopData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LifeShopData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LifeShopData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LifeShopData] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LifeShopData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LifeShopData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LifeShopData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LifeShopData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LifeShopData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LifeShopData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LifeShopData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LifeShopData] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LifeShopData] SET  MULTI_USER 
GO
ALTER DATABASE [LifeShopData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LifeShopData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LifeShopData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LifeShopData] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LifeShopData] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LifeShopData] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [LifeShopData] SET QUERY_STORE = ON
GO
ALTER DATABASE [LifeShopData] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [LifeShopData]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SessionID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NULL,
	[TotalCost] [money] NULL,
 CONSTRAINT [PK_CartItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerFirstName] [varchar](50) NULL,
	[CustomerLastName] [varchar](50) NULL,
	[CustomerEmail] [varchar](50) NULL,
	[CustomerPhone] [varchar](10) NULL,
	[CustomerUsername] [varchar](50) NOT NULL,
	[CustomerPassword] [varchar](50) NOT NULL,
	[CustomerAddress] [varchar](50) NULL,
	[CustomerCity] [varchar](25) NULL,
	[CustomerState] [varchar](25) NULL,
	[CustomerZip] [varchar](25) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeFirstName] [varchar](50) NOT NULL,
	[EmployeeLastName] [varchar](50) NOT NULL,
	[EmployeeEmail] [varchar](50) NOT NULL,
	[EmployeePhone] [varchar](10) NOT NULL,
	[EmployeeUsername] [varchar](50) NOT NULL,
	[EmployeePassword] [varchar](50) NOT NULL,
	[EmployeeAddress] [varchar](50) NOT NULL,
	[EmployeeCity] [varchar](25) NOT NULL,
	[EmployeeState] [varchar](25) NOT NULL,
	[EmployeeZip] [varchar](25) NOT NULL,
	[EmployeeRole] [varchar](50) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[TotalItems] [int] NOT NULL,
	[TotalCost] [money] NOT NULL,
	[OrderDate] [date] NOT NULL,
	[PaymentID] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[CardType] [varchar](50) NOT NULL,
	[CardNumber] [int] NOT NULL,
	[CVC] [int] NOT NULL,
	[BillAddress] [varchar](50) NOT NULL,
	[BillState] [varchar](25) NOT NULL,
	[BillCity] [varchar](25) NOT NULL,
	[BillZip] [varchar](25) NOT NULL,
	[Amount] [money] NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [varchar](50) NOT NULL,
	[ItemDesc] [text] NOT NULL,
	[Price] [money] NOT NULL,
	[Picture] [varchar](max) NULL,
	[Discount] [int] NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shipping]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ShippingStatus] [nchar](10) NOT NULL,
	[ShippingAddress] [nchar](10) NOT NULL,
	[DeliveryDate] [date] NULL,
	[ShippingCity] [nchar](10) NOT NULL,
	[ShippingState] [nchar](10) NOT NULL,
	[ShippingZip] [int] NOT NULL,
 CONSTRAINT [PK_Shipping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingCart]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCart](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[TotalItems] [int] NOT NULL,
	[TotalCost] [money] NOT NULL,
 CONSTRAINT [PK_ShoppingCart] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_Product]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_ShoppingCart] FOREIGN KEY([SessionID])
REFERENCES [dbo].[ShoppingCart] ([ID])
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_ShoppingCart]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Payment] FOREIGN KEY([PaymentID])
REFERENCES [dbo].[Payment] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Payment]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Customer]
GO
ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [FK_Shipping_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [FK_Shipping_Order]
GO
ALTER TABLE [dbo].[ShoppingCart]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCart_Employee] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[ShoppingCart] CHECK CONSTRAINT [FK_ShoppingCart_Employee]
GO
/****** Object:  StoredProcedure [dbo].[CartItemUpdate]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maxwell Connor>
-- Create date: <02 June 2023>
-- Description:	<For adding or modifying CartItem data>
-- =============================================
CREATE PROCEDURE [dbo].[CartItemUpdate]
	@ID int = 0,
	@SessionID int = 0,
	@ProductID int = 0,
	@Quantity int = 0,
	@TotalCost money = 0,

	@NewID int output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @ID=0
	BEGIN
		INSERT INTO CartItem(SessionID, ProductID, Quantity, TotalCost) 
		VALUES (@SessionID, @ProductID, @Quantity, @TotalCost);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE CartItem SET
		SessionID=@SessionID, ProductID=@ProductID, Quantity=@Quantity, TotalCost=@TotalCost
		
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[CustomerUpdate]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maxwell Connor>
-- Create date: <02 June 2023>
-- Description:	<For adding or modifying customer data>
-- =============================================
CREATE PROCEDURE [dbo].[CustomerUpdate]
	@ID int = 0,
	@CustomerFirstName varchar(50),
	@CustomerLastName varchar(50),
	@CustomerEmail varchar(50),
	@CustomerPhone varchar(10),
	@CustomerAddress varchar (50),
	@CustomerCity varchar (50),
	@CustomerState varchar (50),
	@CustomerZip varchar(25),
	@CustomerUsername varchar(50),
	@CustomerPassword varchar(50),
	@NewID int output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @ID=0
	BEGIN
		INSERT INTO Customer(CustomerFirstName, CustomerLastName, CustomerEmail, CustomerPhone, CustomerAddress,
		CustomerCity, CustomerState, CustomerZip, CustomerUsername, CustomerPassword) 
		VALUES (@CustomerFirstName, @CustomerLastName, @CustomerEmail, @CustomerPhone, @CustomerAddress,
		@CustomerCity, @CustomerState, @CustomerZip, @CustomerUsername, @CustomerPassword);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Customer SET
		CustomerFirstName= @CustomerFirstName,CustomerLastName=@CustomerLastName, CustomerEmail=@CustomerEmail, CustomerPhone=@CustomerPhone,
		CustomerAddress=@CustomerAddress, CustomerCity = @CustomerCity, CustomerState=@CustomerState, CustomerZip=@CustomerZip,
		CustomerUsername = @CustomerUsername, CustomerPassword=@CustomerPassword
		
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeUpdate]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maxwell Connor>
-- Create date: <02 June 2023>
-- Description:	<For adding or modifying Employee data>
-- =============================================
CREATE PROCEDURE [dbo].[EmployeeUpdate]
	@ID int = 0,
	@EmployeeFirstName varchar(50),
	@EmployeeLastName varchar(50),
	@EmployeeEmail varchar(50),
	@EmployeePhone varchar(10),
	@EmployeeAddress varchar (50),
	@EmployeeCity varchar (50),
	@EmployeeState varchar (50),
	@EmployeeZip varchar(25),
	@EmployeeRole varchar (25),
	@EmployeeUsername varchar (50),
	@EmployeePassword varchar (50),
	@NewID int output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @ID=0
	BEGIN
		INSERT INTO Employee(EmployeeFirstName, EmployeeLastName, EmployeeEmail, EmployeePhone, EmployeeAddress,
		EmployeeCity, EmployeeState, EmployeeZip, EmployeeRole, EmployeeUsername, EmployeePassword) 
		VALUES (@EmployeeFirstName, @EmployeeLastName, @EmployeeEmail, @EmployeePhone, @EmployeeAddress,
		@EmployeeCity, @EmployeeState, @EmployeeZip, @EmployeeRole, @EmployeeUsername, @EmployeePassword);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Employee SET
		EmployeeFirstName= @EmployeeFirstName,EmployeeLastName=@EmployeeLastName, EmployeeEmail=@EmployeeEmail, EmployeePhone=@EmployeePhone,
		EmployeeAddress=@EmployeeAddress, EmployeeCity = @EmployeeCity, EmployeeState=@EmployeeState, EmployeeZip=@EmployeeZip, EmployeeRole=@EmployeeRole,
		EmployeeUsername = @EmployeeUsername, EmployeePassword=@EmployeePassword
		
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[OrderUpdate]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maxwell Connor>
-- Create date: <02 June 2023>
-- Description:	<For adding or modifying Order data>
-- =============================================
CREATE PROCEDURE [dbo].[OrderUpdate]
	@ID int = 0,
	@CustomerID int = 0,
	@TotalItems int = 0,
	@TotalCost money = 0,
	@OrderDate date,
	@PaymentID int = 0,

	@NewID int output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @ID=0
	BEGIN
		INSERT INTO [Order](CustomerID, TotalItems, TotalCost, OrderDate, PaymentID) 
		VALUES (@CustomerID, @TotalItems, @TotalCost, @OrderDate, @PaymentID);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE [Order] SET
		CustomerID=@CustomerID, TotalItems=@TotalItems, TotalCost=@TotalCost, OrderDate=@OrderDate, PaymentID=@PaymentID
		
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[PaymentUpdate]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maxwell Connor>
-- Create date: <02 June 2023>
-- Description:	<For adding or modifying Payment data>
-- =============================================
CREATE PROCEDURE [dbo].[PaymentUpdate]
	@ID int = 0,
	@CustomerID int = 0,
	@CardType varchar (50),
	@CardNumber int = 0,
	@CVC int=0,
	@BillAddress varchar(50),
	@BillCity varchar(25),
	@BillState varchar(25),
	@BillZip varchar(25) = 0,
	@Amount money = 0,

	@NewID int output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @ID=0
	BEGIN
		INSERT INTO [Payment](CustomerID,CardType, CardNumber, CVC, BillAddress, BillCity, BillState, BillZip, Amount) 
		VALUES (@CustomerID, @CardType, @CardNumber, @CVC, @BillAddress, @BillCity, @BillState, @BillZip, @Amount);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Payment SET
		CustomerID=@CustomerID, CardType=@CardType, CardNumber=@CardNumber, CVC=@CVC, BillAddress = @BillAddress, BillCity =@BillCity,
		BillState = @BillState, BillZip = @BillZip, Amount=@Amount
		
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ProductUpdate]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maxwell Connor>
-- Create date: <02 June 2023>
-- Description:	<For adding or modifying product data>
-- =============================================
CREATE PROCEDURE [dbo].[ProductUpdate]
	@ID int = 0,
	@ItemName varchar(50),
	@ItemDesc varchar(50),
	@Price money =0,
	@Picture varchar(Max),
	@Discount int =0,

	@NewID int output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @ID=0
	BEGIN
		INSERT INTO Product(ItemName, ItemDesc, Price, Picture, Discount) 
		VALUES (@ItemName, @ItemDesc, @Price, @Picture, @Discount);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Product SET
		ItemName=@ItemName, ItemDesc=@ItemDesc, Price=@Price, Picture=@Picture, Discount=@Discount
		
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ShippingUpdate]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maxwell Connor>
-- Create date: <02 June 2023>
-- Description:	<For adding or modifying Shipping data>
-- =============================================
CREATE PROCEDURE [dbo].[ShippingUpdate]
	@ID int = 0,
	@OrderID int = 0,
	@ShippingStatus varchar(10),
	@DeliveryDate date,
	@ShippingAddress varchar(50),
	@ShippingCity varchar(50),
	@ShippingState varchar(50),
	@ShippingZip int = 0,

	@NewID int output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @ID=0
	BEGIN
		INSERT INTO Shipping(OrderID, ShippingStatus, ShippingAddress, ShippingCity, ShippingState, ShippingZip) 
		VALUES (@OrderID, @ShippingStatus, @ShippingAddress, @ShippingCity, @ShippingState, @ShippingZip);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Shipping SET
		OrderID=@OrderID, ShippingStatus=@ShippingStatus,  ShippingAddress = @ShippingAddress, ShippingCity =@ShippingCity,
		ShippingState = @ShippingState, ShippingZip = @ShippingZip
		
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ShoppingCartUpdate]    Script Date: 6/19/2023 12:31:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Maxwell Connor>
-- Create date: <02 June 2023>
-- Description:	<For adding or modifying ShoppingCart data>
-- =============================================
CREATE PROCEDURE [dbo].[ShoppingCartUpdate]
	@ID int = 0,
	@CustomerID int = 0,
	@TotalItems int = 0,
	@TotalCost int = 0,

	@NewID int output

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @ID=0
	BEGIN
		INSERT INTO ShoppingCart(CustomerID, TotalItems, TotalCost) 
		VALUES (@CustomerID, @TotalItems, @TotalCost);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE ShoppingCart SET
		CustomerID = @CustomerID, TotalItems = @TotalItems, TotalCost=@TotalCost
		
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Testing my abilitys' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product'
GO
USE [master]
GO
ALTER DATABASE [LifeShopData] SET  READ_WRITE 
GO
