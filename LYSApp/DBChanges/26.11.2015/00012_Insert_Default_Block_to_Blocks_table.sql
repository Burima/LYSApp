SET IDENTITY_INSERT [dbo].[Blocks] ON 
INSERT [dbo].[Blocks] (BlockID ,BlockName,Description,
	[Status] ,
	[ApartmentID],
	[CreatedOn],
	[LastUpdatedOn],
	[isDeleted],
	[DeletedBy],
	[DeletedOn],
	[CreatedBy]) VALUES (0, 'Default_Block','Default', 1, 0, CAST(0x0000A504017B35C6 AS DateTime), CAST(0x0000A504017B5166 AS DateTime), 0, NULL, NULL, 1)

SET IDENTITY_INSERT [dbo].[Blocks] OFF 