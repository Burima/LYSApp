USE [LYSAdmin]
GO

ALTER TABLE [dbo].[Beds] DROP CONSTRAINT [FK_Beds_UserID]
GO

ALTER TABLE [dbo].[Beds] DROP CONSTRAINT [FK_Beds_RoomID]
GO

/****** Object:  Table [dbo].[Beds]    Script Date: 26-11-2015 22:35:52 ******/
DROP TABLE [dbo].[Beds]
GO

/****** Object:  Table [dbo].[Beds]    Script Date: 26-11-2015 22:35:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Beds](
	[BedID] [int] IDENTITY(1,1) NOT NULL,
	[RoomID] [int] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[LastUpdatedOn] [datetime] NULL,
	[BedStatus] [int] NULL,
	[StatusUpdateDate] [datetime] NULL,
	[BookingFromDate] [datetime] NULL,
	[BookingToDate] [datetime] NULL,
 CONSTRAINT [PK_Bed] PRIMARY KEY CLUSTERED 
(
	[BedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Beds]  WITH CHECK ADD  CONSTRAINT [FK_Beds_RoomID] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([RoomID])
GO

ALTER TABLE [dbo].[Beds] CHECK CONSTRAINT [FK_Beds_RoomID]
GO

ALTER TABLE [dbo].[Beds]  WITH CHECK ADD  CONSTRAINT [FK_Beds_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Beds] CHECK CONSTRAINT [FK_Beds_UserID]
GO


