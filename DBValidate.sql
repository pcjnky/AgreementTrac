SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ANSI_PADDING ON
IF OBJECT_ID (N'dbo.ATCustInfo', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[ATCustInfo](
	[CustomerIdx] [uniqueidentifier] NOT NULL,
	[FName] [varchar](35) NULL,
	[LName] [varchar](35) NULL,
	[Address] [varchar](128) NULL,
	[City] [varchar](25) NULL,
	[State] [varchar](2) NULL,
	[Zip] [varchar](10) NULL,
	[Phone] [varchar](15) NULL,
 CONSTRAINT [PK_ATCustInfo] PRIMARY KEY CLUSTERED 
(
	[CustomerIdx] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 40) ON [PRIMARY]
) ON [PRIMARY]
END
IF OBJECT_ID (N'dbo.ATVehicleInfo', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[ATVehicleInfo](
	[VehicleIdx] [uniqueidentifier] NOT NULL,
	[StockNo] [varchar](25) NULL,
	[Make] [varchar](25) NULL,
	[Model] [varchar](25) NULL,
	[VehYear] [varchar](4) NULL,
	[Vin] [varchar](17) NULL,
 CONSTRAINT [PK_ATVehicleInfo] PRIMARY KEY CLUSTERED 
(
	[VehicleIdx] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 40) ON [PRIMARY]
) ON [PRIMARY]
END
IF OBJECT_ID (N'dbo.ATVideoInfo', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[ATVideoInfo](
	[VideoIdx] [uniqueidentifier] NOT NULL,
	[vidFileLength] [bigint] NULL,
	[vidSaveDir] [varchar](256) NULL,
	[vidFileName] [varchar](50) NULL,
	[vidFileSize] [int] NULL,
 CONSTRAINT [PK_ATVideoInfo] PRIMARY KEY CLUSTERED 
(
	[VideoIdx] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 40) ON [PRIMARY]
) ON [PRIMARY]
END
IF OBJECT_ID (N'dbo.ATLoginInfo', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[ATLoginInfo](
	[LoginIdx] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ATLoginInfo] PRIMARY KEY CLUSTERED 
(
	[LoginIdx] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 40) ON [PRIMARY]
) ON [PRIMARY]
END
IF OBJECT_ID (N'dbo.ATDealInfo', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[ATDealInfo](
	[DealIdx] [uniqueidentifier] NOT NULL,
	[ComputerName] [varchar](15) NULL,
	[IPAddress] [varchar](15) NULL,
	[MgrName] [varchar](50) NULL,
	[CustomerIdx] [uniqueidentifier] NULL,
	[VideoIdx] [uniqueidentifier] NULL,
	[VehicleIdx] [uniqueidentifier] NULL,
	[DealNumber] [varchar](25) NULL,
	[DealershipName] [varchar] (50) NULL,
	[TransactionDate] [datetime] NULL,
	[VSC] [int] NULL,
	[AH] [int] NULL,
	[ETCH] [int] NULL,
	[GAP] [int] NULL,
	[MAINT] [int] NULL,
	[Notes] [text] NULL,
 CONSTRAINT [PK_ATDealInfo] PRIMARY KEY CLUSTERED 
(
	[DealIdx] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 40) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ATDealInfo_ATCustInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[ATDealInfo]'))
ALTER TABLE [dbo].[ATDealInfo] DROP CONSTRAINT [FK_ATDealInfo_ATCustInfo]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ATDealInfo_ATVehicleInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[ATDealInfo]'))
ALTER TABLE [dbo].[ATDealInfo] DROP CONSTRAINT [FK_ATDealInfo_ATVehicleInfo]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ATDealInfo_ATVideoInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[ATDealInfo]'))
ALTER TABLE [dbo].[ATDealInfo] DROP CONSTRAINT [FK_ATDealInfo_ATVideoInfo]

ALTER TABLE [dbo].[ATDealInfo]  WITH NOCHECK ADD  CONSTRAINT [FK_ATDealInfo_ATCustInfo] FOREIGN KEY([CustomerIdx])
REFERENCES [dbo].[ATCustInfo] ([CustomerIdx])
ON DELETE CASCADE
ALTER TABLE [dbo].[ATDealInfo] CHECK CONSTRAINT [FK_ATDealInfo_ATCustInfo]

ALTER TABLE [dbo].[ATDealInfo]  WITH NOCHECK ADD  CONSTRAINT [FK_ATDealInfo_ATVehicleInfo] FOREIGN KEY([VehicleIdx])
REFERENCES [dbo].[ATVehicleInfo] ([VehicleIdx])
ON DELETE CASCADE
ALTER TABLE [dbo].[ATDealInfo] CHECK CONSTRAINT [FK_ATDealInfo_ATVehicleInfo]

ALTER TABLE [dbo].[ATDealInfo]  WITH NOCHECK ADD  CONSTRAINT [FK_ATDealInfo_ATVideoInfo] FOREIGN KEY([VideoIdx])
REFERENCES [dbo].[ATVideoInfo] ([VideoIdx])
ON DELETE CASCADE
ALTER TABLE [dbo].[ATDealInfo] CHECK CONSTRAINT [FK_ATDealInfo_ATVideoInfo]