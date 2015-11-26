use [LYSAdmin]

alter table PGDetails
add latitude decimal,
Longitude decimal,
Address varchar(100),
Landmark varchar(100) NULL,
IsPg bit NOT NULL DEFAULT(1),
PGReviewID int NOT NULL DEFAULT(0),
Description varchar(100)




