CREATE TABLE OwnerPropertyListingRequests
(
OwnerPropertyListingRequestID int IDENTITY(1,1)  CONSTRAINT PK_OwnerPropertyListingRequests_OwnerPropertyListingRequestID PRIMARY KEY,
FirstName varchar(20) NOT NULL,
LastName varchar(20) NOT NULL,
Email varchar(50) NOT NULL,
Mobile varchar(10) NOT NULL,
Address varchar(200) NOT NULL
)