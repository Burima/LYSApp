USE [LYSAdmin]
GO

ALTER TABLE [dbo].[Houses] DROP CONSTRAINT [FK_Houses_PGDetailID]
GO

/****** Object:  Table [dbo].[Houses]    Script Date: 26-11-2015 23:20:44 ******/
DROP TABLE [dbo].[Houses]
GO

/****** Object:  Table [dbo].[Houses]    Script Date: 26-11-2015 23:20:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Houses](
	[HouseID] [int] IDENTITY(1,1) NOT NULL,
	[HouseName] [varchar](100) NULL,
	[Status] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[LastUpdatedOn] [datetime] NULL,
	[isDeleted] [bit] NULL,
	[DeletedBy] [int] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[DisplayName] [varchar](50) NULL,
	[PGDetailID] [int] NOT NULL,
	[Gender] [int] NULL,
	[NoOfBathrooms] [int] NULL,
	[NoOfBalconnies] [int] NULL,
	[BlockID] [int] NOT NULL,
	[HouseNo] [varchar](50) NULL,
 CONSTRAINT [PK_Houses_HouseID] PRIMARY KEY CLUSTERED 
(
	[HouseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Houses]  WITH CHECK ADD  CONSTRAINT [FK_Houses_PGDetailID] FOREIGN KEY([PGDetailID])
REFERENCES [dbo].[PGDetails] ([PGDetailID])
GO

ALTER TABLE [dbo].[Houses] CHECK CONSTRAINT [FK_Houses_PGDetailID]
GO


