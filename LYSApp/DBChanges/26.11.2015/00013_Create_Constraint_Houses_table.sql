use [LYSAdmin]

alter table Houses
add constraint FK_Houses_BlockID
Foreign Key (BlockID)
references Blocks(BlockID)