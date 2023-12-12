Set NoCount On 
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_IME__CFG_Validar_Vales' and xType = 'U' ) 
   Drop Table INT_IME__CFG_Validar_Vales 
Go--#SQL   

Create Table INT_IME__CFG_Validar_Vales 
( 
	IdSocioComercial varchar(8) Not Null, 
	IdSucursal varchar(8) Not Null, 
	URL_Produccion varchar(200) Not Null Default '',  
	URL_Pruebas varchar(200) Not Null Default ''	
) 
Go--#SQL   

Alter Table INT_IME__CFG_Validar_Vales 
	Add Constraint PK_INT_IME__CFG_Validar_Vales Primary Key ( IdSocioComercial, IdSucursal ) 
Go--#SQL   


------------------------------------------------------------------------------- 
	----Insert Into INT_IME__CFG_Validar_Vales ( IdEmpresa, IdEstado, IdFarmacia, URL_Produccion, URL_Pruebas )
	----Select '002' as IdEmpresa, '09' as IdEstado, '0011' as IdFarmacia, 
	----	'http://www.mediaccess.com.mx:8081/wsoperaciones/VerificaElegibilidadConCopago.asmx' as URL_Produccion, 
	----	'http://www.mediaccess.com.mx/wsoperaciones/VerificaElegibilidadConCopago.asmx' as URL_Pruebas	
Go--#SQL   

	
	----   sp_listacolumnas INT_IME__CFG_Validar_Vales
	