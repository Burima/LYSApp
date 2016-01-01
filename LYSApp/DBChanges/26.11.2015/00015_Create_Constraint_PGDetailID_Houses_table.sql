use [LYSAdmin]

alter table Houses
add constraint FK_Houses_PGDetailID
foreign key (PGDetailID)
references PGDetails(PGDetailID)
