USE [LYSAdmin]
GO

ALTER TABLE [dbo].[PGReviews] DROP CONSTRAINT [FK_HouseReviews_UserID]
GO

ALTER TABLE [dbo].[PGReviews] DROP CONSTRAINT [FK_HouseReviews_HouseID]
GO

/****** Object:  Table [dbo].[PGReviews]    Script Date: 26-11-2015 22:31:51 ******/
DROP TABLE [dbo].[PGReviews]
GO

/****** Object:  Table [dbo].[PGReviews]    Script Date: 26-11-2015 22:31:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PGReviews](
	[PGReviewID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[HouseID] [int] NOT NULL,
	[Comments] [varchar](500) NULL,
	[Rating] [decimal](3, 2) NULL,
	[CommentTime] [datetime] NULL,
 CONSTRAINT [PK_PGReview] PRIMARY KEY CLUSTERED 
(
	[PGReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PGReviews]  WITH CHECK ADD  CONSTRAINT [FK_HouseReviews_HouseID] FOREIGN KEY([HouseID])
REFERENCES [dbo].[Houses] ([HouseID])
GO

ALTER TABLE [dbo].[PGReviews] CHECK CONSTRAINT [FK_HouseReviews_HouseID]
GO

ALTER TABLE [dbo].[PGReviews]  WITH CHECK ADD  CONSTRAINT [FK_HouseReviews_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[PGReviews] CHECK CONSTRAINT [FK_HouseReviews_UserID]
GO


