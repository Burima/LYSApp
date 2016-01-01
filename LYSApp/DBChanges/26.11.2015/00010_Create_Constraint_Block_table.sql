use [LYSAdmin]

alter table Blocks
add constraint FK_Blocks_ApartmentID
Foreign Key (ApartmentID)
references Apartments(ApartmentID)