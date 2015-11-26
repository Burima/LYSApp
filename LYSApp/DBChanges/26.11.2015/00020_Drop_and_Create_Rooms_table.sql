USE [LYSAdmin]
GO

/****** Object:  Table [dbo].[Rooms]    Script Date: 26-11-2015 22:36:37 ******/
DROP TABLE [dbo].[Rooms]
GO

/****** Object:  Table [dbo].[Rooms]    Script Date: 26-11-2015 22:36:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rooms](
	[RoomID] [int] IDENTITY(1,1) NOT NULL,
	[HouseID] [int] NOT NULL,
	[RoomNumber] [int] NOT NULL,
	[MonthlyRent] [int] NOT NULL,
	[Deposit] [int] NOT NULL,
	[NoOfBeds] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Rooms_RoomID] PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_HouseID] FOREIGN KEY([HouseID])
REFERENCES [dbo].[Houses] ([HouseID])
GO

ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Rooms_HouseID]
GO


