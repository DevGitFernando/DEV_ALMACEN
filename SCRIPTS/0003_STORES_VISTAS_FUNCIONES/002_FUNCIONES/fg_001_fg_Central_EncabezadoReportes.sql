--	select * from Net_CFGS_Parametros  

If Exists ( Select Name, * From Sysobjects (NoLock) Where Name = 'fg_Central_EncabezadoReportes' and xType = 'TF' ) 
   Drop Function fg_Central_EncabezadoReportes 
Go--#SQL
 

Create Function dbo.fg_Central_EncabezadoReportes()
returns @Tabla Table 
( 
	EncPrin varchar(200), 
	EncSec varchar(200) 
)
With Encryption 
As 
Begin 
Declare @sModulo varchar(5) 
	Set @sModulo = 'OCEN' 

	Insert Into @Tabla ( EncPrin, EncSec ) 
	Select IsNull(( Select Valor From Net_CFGS_Parametros (NoLock) Where ArbolModulo = @sModulo and NombreParametro = 'EncReportesPrin' ), 'SISTEMA INTEGRAL DE INFORMACIÓN') as EncPrin, 
		   IsNull(( Select Valor From Net_CFGS_Parametros (NoLock) Where ArbolModulo = @sModulo and NombreParametro = 'EncReportesSec' ), 'ADMINISTRACION') as EncSec  

	return 

End
Go--#SQL
 