If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFG_PrepararDatos' and xType = 'P' )
   Drop Proc spp_CFG_PrepararDatos 
Go--#SQL 

Create Proc spp_CFG_PrepararDatos ( @sTabla varchar(100), @sDato varchar(1), @sDatoWhere varchar(1), @sWhereTransf varchar(max) = '' ) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sSql varchar(max),  
		@sWhereTransferencias varchar(max) 
		
	Set @sWhereTransferencias = ''
	If @sWhereTransf <> '' 
	   Set @sWhereTransferencias = ' and ' + @sWhereTransf 

	Set @sSql = 'Update ' + @sTabla + ' Set Actualizado = ' + char(39) + @sDato + char(39) + ' where Actualizado = ' + char(39) + @sDatoWhere + char(39) + '  ' + @sWhereTransferencias 
	Exec(@sSql) 
	-- print @sSql 
End 
Go--#SQL 
