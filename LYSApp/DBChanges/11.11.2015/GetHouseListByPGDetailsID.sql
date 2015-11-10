-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetHouseListByPGDetailsID
(
@pgdetailsid INT  
)
AS
BEGIN
	select * from houses h where h.PGDetailID = @pgdetailsid AND 
	h.HouseID IN (select HouseID from rooms r where r.MonthlyRent in (select distinct monthlyrent from rooms ))

END
GO
