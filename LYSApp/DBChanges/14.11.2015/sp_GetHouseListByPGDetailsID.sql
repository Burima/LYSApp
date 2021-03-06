USE [LYSAdmin]
GO
/****** Object:  StoredProcedure [dbo].[GetHouseListByPGDetailsID]    Script Date: 14-11-2015 17:36:03 ******/
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
	select distinct(monthlyrent),roomid,houseid,noofbeds,status,roomnumber, deposit,createdon,lastupdatedon
	 from rooms where houseid in (select houseid from houses h where h.pgdetailid = @pgDetailsId) and status = 1
END
