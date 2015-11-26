USE [LYSAdmin]
GO

ALTER TABLE [dbo].[HouseAmenities] DROP CONSTRAINT [FK__House_Amenities__Houses]
GO

/****** Object:  Table [dbo].[HouseAmenities]    Script Date: 26-11-2015 22:25:03 ******/
DROP TABLE [dbo].[HouseAmenities]
GO

/****** Object:  Table [dbo].[HouseAmenities]    Script Date: 26-11-2015 22:25:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HouseAmenities](
	[AminityID] [int] IDENTITY(1,1) NOT NULL,
	[HouseID] [int] NOT NULL,
	[AC] [bit] NOT NULL,
	[Fridge] [bit] NOT NULL,
	[Wifi] [bit] NOT NULL,
	[Washingmachine] [bit] NOT NULL,
	[AttachBathrooms] [bit] NOT NULL,
	[KitchenFacilityWithGas] [bit] NOT NULL,
	[CommonTV] [bit] NOT NULL,
	[IndividualTV] [bit] NOT NULL,
	[LCDTVCableConnection] [bit] NOT NULL,
	[Wardrobes] [bit] NOT NULL,
	[IntercomFacility] [bit] NOT NULL,
	[IroningWashingServices] [bit] NOT NULL,
	[LunchGiven] [bit] NOT NULL,
	[BreakFastGiven] [bit] NOT NULL,
	[DinnerGiven] [bit] NOT NULL,
	[WaterSupply] [bit] NOT NULL,
	[HotColdWaterSupply] [bit] NOT NULL,
	[MineralDrinkingWater] [bit] NOT NULL,
	[Aquaguard] [bit] NOT NULL,
	[Housekeeping] [bit] NOT NULL,
	[RoomService] [bit] NOT NULL,
	[Newspaper] [bit] NOT NULL,
	[TwoWheelerOpenParking] [bit] NOT NULL,
	[TwoWheelerCloseParking] [bit] NOT NULL,
	[FourWheelerOpenParking] [bit] NOT NULL,
	[FourWheelerCloseParking] [bit] NOT NULL,
	[Lockers] [bit] NOT NULL,
	[GYM] [bit] NOT NULL,
	[Lift] [bit] NOT NULL,
	[Playground] [bit] NOT NULL,
	[Clubhouse] [bit] NOT NULL,
	[Partyhall] [bit] NOT NULL,
	[Security] [bit] NOT NULL,
	[SwimmingPool] [bit] NOT NULL,
	[VideoSurveillance] [bit] NOT NULL,
	[Powerbackup] [bit] NOT NULL,
	[EmergencyMedicalServices] [bit] NOT NULL,
	[NonVegAllowed] [bit] NOT NULL,
	[GuardianEntry] [bit] NOT NULL,
	[NoSmoking] [bit] NOT NULL,
	[NoDrinking] [bit] NOT NULL,
	[NoBoysEntry] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK__House_Amenities] PRIMARY KEY CLUSTERED 
(
	[AminityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[HouseAmenities]  WITH CHECK ADD  CONSTRAINT [FK__House_Amenities__Houses] FOREIGN KEY([HouseID])
REFERENCES [dbo].[Houses] ([HouseID])
GO

ALTER TABLE [dbo].[HouseAmenities] CHECK CONSTRAINT [FK__House_Amenities__Houses]
GO


