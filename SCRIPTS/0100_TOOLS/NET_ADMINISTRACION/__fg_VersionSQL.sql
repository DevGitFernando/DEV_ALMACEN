If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_VersionSQL' and xType = 'TF' )
   Drop Function fg_VersionSQL 
Go--#SQL 
  
Create Function fg_VersionSQL()
returns @Tabla Table 
( 
	VersionSQL varchar(30), 
	Version int 
)
With Encryption 
As 
Begin 
Declare @svrVersion varchar(50) 

	Select @svrVersion = ltrim(rtrim(convert(varchar(50), ServerProperty('productversion')))) 
	-- Select @svrVersion as VersionSQL, Left(@svrVersion, PatIndex('%.%', @svrVersion) - 1)  


	Insert Into @Tabla values ( @svrVersion, Left(@svrVersion, PatIndex('%.%', @svrVersion) - 1) ) 
	return 

End
Go--#SQL 
   