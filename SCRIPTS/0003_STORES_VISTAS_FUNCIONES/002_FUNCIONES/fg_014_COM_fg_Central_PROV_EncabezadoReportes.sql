If Exists ( Select Name, * From Sysobjects (NoLock) Where Name = 'fg_Central_PROV_EncabezadoReportes' and xType = 'TF' ) 
   Drop Function fg_Central_PROV_EncabezadoReportes 
Go--#SQL 

Create Function dbo.fg_Central_PROV_EncabezadoReportes()
returns @Tabla Table 
( 
	EncPrin varchar(200), 
	EncSec varchar(200) 
)
With Encryption 
As 
Begin 
Declare @sModulo varchar(5) 
	Set @sModulo = 'PROV' 

	Insert Into @Tabla ( EncPrin, EncSec ) 
	Select ( Select Valor From Net_CFGS_Parametros (NoLock) Where ArbolModulo = @sModulo and NombreParametro = 'EncReportesPrin' ) as EncPrin, 
		   ( Select Valor From Net_CFGS_Parametros (NoLock) Where ArbolModulo = @sModulo and NombreParametro = 'EncReportesSec' ) as EncSec  

	return 

End
Go--#SQL  