USE [LYSAdmin]
GO

ALTER TABLE [dbo].[HouseImages] DROP CONSTRAINT [FK_HouseImages_HouseID]
GO

/****** Object:  Table [dbo].[HouseImages]    Script Date: 26-11-2015 22:29:47 ******/
DROP TABLE [dbo].[HouseImages]
GO

/****** Object:  Table [dbo].[HouseImages]    Script Date: 26-11-2015 22:29:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[HouseImages](
	[HouseImageID] [int] IDENTITY(1,1) NOT NULL,
	[ImagePath] [varchar](50) NOT NULL,
	[HouseID] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[LastUpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_HouseImageID] PRIMARY KEY CLUSTERED 
(
	[HouseImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[HouseImages]  WITH CHECK ADD  CONSTRAINT [FK_HouseImages_HouseID] FOREIGN KEY([HouseID])
REFERENCES [dbo].[Houses] ([HouseID])
GO

ALTER TABLE [dbo].[HouseImages] CHECK CONSTRAINT [FK_HouseImages_HouseID]
GO


