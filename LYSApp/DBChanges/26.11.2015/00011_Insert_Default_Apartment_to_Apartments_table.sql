SET IDENTITY_INSERT [dbo].[Apartments] ON 
INSERT [dbo].[Apartments] (ApartmentID, ApartmentName, HouseNo, Description, Status, CreatedOn, LastUpdatedOn, IsDeleted, DeletedBy, DeletedOn, CreatedBy) VALUES 
							(0, 'Default_Apartment','XX', 'Default', 1, CAST(0x0000A504017B35C6 AS DateTime), CAST(0x0000A504017B5166 AS DateTime), 0, NULL, NULL, 1)

SET IDENTITY_INSERT [dbo].[Apartments] OFF 