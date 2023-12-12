
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_COM_OCEN_TablaTrabajoCalculoDePromedioPorOrdenDeCompra' and xType = 'P' ) 
   Drop Proc spp_COM_OCEN_TablaTrabajoCalculoDePromedioPorOrdenDeCompra
Go--#SQL   

Create Proc spp_COM_OCEN_TablaTrabajoCalculoDePromedioPorOrdenDeCompra
( 
	@CodigoEAN Varchar(100) = '12erf24', @Cantidad Varchar(100) = 1, @Tabla Varchar(200) = 'tempdf'
)  
With Encryption 
As 
Begin 
Set NoCount On 	

	Declare @iPzas Int,
			@iProm Numeric(14, 4),
			@sSql Varchar(5000)
	
	If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = @Tabla and xType = 'U' )
	Begin 
		Set @sSql = 'create Table ' + @Tabla + CHAR(13)
		Set @sSql += '(' + CHAR(13)
		Set @sSql += '	CodigoEAN Varchar(30),' + CHAR(13)
		Set @sSql += '	Cantidad Numeric(14,4),' + CHAR(13)
		Set @sSql += '	IdClaveSSA Varchar(8) Default ' + CHAR(39) + CHAR(39) +', ' + CHAR(13)
		Set @sSql += '	ClaveSSA Varchar(50) Default ' + CHAR(39) + CHAR(39) + CHAR(13)
		Set @sSql += ')' + CHAR(13)
		Exec (@sSql)
	End

	Set @sSql = 'Insert Into ' + @Tabla + '(CodigoEAN, Cantidad)' + CHAR(13)
	
	Set @sSql = @sSql + 'Select  ' + Char(39) +  @CodigoEAN + Char(39) + ', ' + @Cantidad 
		
	Exec (@sSql)	
	
End
Go--#SQL