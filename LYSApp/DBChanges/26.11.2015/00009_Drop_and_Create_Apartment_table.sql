USE [LYSAdmin]
GO

/****** Object:  Table [dbo].[Apartments]    Script Date: 26-11-2015 20:53:39 ******/
DROP TABLE [dbo].[Apartments]
GO

/****** Object:  Table [dbo].[Apartments]    Script Date: 26-11-2015 20:53:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Apartments](
	[ApartmentID] [int] IDENTITY(0,1) NOT NULL,
	[ApartmentName] [varchar](50) NULL,
	[HouseNo] [varchar](50) NULL,
	[Description] [varchar](500) NULL,
	[Status] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[LastUpdatedOn] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedBy] [int] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_Apartments] PRIMARY KEY CLUSTERED 
(
	[ApartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


