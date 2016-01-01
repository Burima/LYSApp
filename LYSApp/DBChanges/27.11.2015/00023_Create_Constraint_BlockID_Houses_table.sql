use [LYSAdmin]

alter table Houses
add constraint FK_Houses_BlockID
foreign key (BlockID)
references Blocks(BlockID)