use [LYSAdmin]

alter table PGDetails
add constraint FK_PGDetails_UserID
Foreign Key (UserID)
references Users(UserID)