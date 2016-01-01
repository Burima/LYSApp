EXEC sp_rename 'HouseReviews', 'PGReviews'

EXEC sp_RENAME 'PGReviews.HouseReviewID', 'PGReviewID', 'COLUMN'