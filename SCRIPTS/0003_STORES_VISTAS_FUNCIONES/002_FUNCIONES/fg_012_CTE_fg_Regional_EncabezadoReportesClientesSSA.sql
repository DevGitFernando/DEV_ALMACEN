If Exists ( Select Name, * From Sysobjects (NoLock) Where Name = 'fg_Regional_EncabezadoReportesClientesSSA' and xType = 'TF' ) 
   Drop Function fg_Regional_EncabezadoReportesClientesSSA 
Go--#SQL  

Create Function dbo.fg_Regional_EncabezadoReportesClientesSSA() 
returns @Tabla Table 
( 
	EncabezadoPrincipal varchar(200), 
	EncabezadoSecundario varchar(200) 
) 
With Encryption  
As 
Begin 
Declare @sModulo varchar(5) 
	Set @sModulo = 'CTER' 

	Insert Into @Tabla ( EncabezadoPrincipal, EncabezadoSecundario ) 
	Select IsNull(( Select Valor From Net_CFGS_Parametros (NoLock) Where ArbolModulo = @sModulo and NombreParametro = 'EncReportesPrin' ), 'SISTEMA INTEGRAL DE INFORMACIÓN') as EncPrin, 
		   IsNull(( Select Valor From Net_CFGS_Parametros (NoLock) Where ArbolModulo = @sModulo and NombreParametro = 'EncReportesSec' ), 'ADMINISTRACION') as EncSec  

	return 

End
Go--#SQL
   
	
	