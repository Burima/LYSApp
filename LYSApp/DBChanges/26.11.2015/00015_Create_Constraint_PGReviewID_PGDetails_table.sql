use [LYSAdmin]

alter table PGDetails
add constraint FK_PGDetails_PGReviewID
foreign key (PGReviewID)
references PGReviews(PGReviewID)
