If Exists ( Select Name From sysobjects (NoLock) Where Name = 'sp_CheckDB' and xType = 'P' ) 
   Drop Proc sp_CheckDB 
Go--#SQL 

Create Proc sp_CheckDB ( @NombreBD varchar(100) = '' ) 
With Encryption 
As    
Begin 
Set NoCount On 

Declare @sBdName varchar(100) 

    If @NombreBD = '' 
       Set @sBdName = db_name()
    Else 
       Set @sBdName = @NombreBD  


    Exec sp_dboption @sBdName, 'single user', true  
    DBCC CHECKDB ( @sBdName, REPAIR_ALLOW_DATA_LOSS  )     
    Exec sp_dboption @sBdName, 'single user', false 

End 
Go--#SQL 
        