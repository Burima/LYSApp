USE [LYSAdmin]
GO
/****** Object:  StoredProcedure [dbo].[GetHouseListByPGDetailsID]    Script Date: 11-11-2015 11:43:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetHouseListByPGDetailsID]
(
@pgDetailsId INT  
)
AS
BEGIN
	select * from houses h where h.PGDetailID = @pgDetailsId AND 
	h.HouseID IN (select HouseID from rooms r where r.MonthlyRent in (select distinct monthlyrent from rooms) )

END
