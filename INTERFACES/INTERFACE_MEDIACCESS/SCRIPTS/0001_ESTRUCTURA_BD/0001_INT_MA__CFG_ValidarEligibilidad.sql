Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__CFG_ValidarEligibilidad' and xType = 'U' ) 
   Drop Table INT_MA__CFG_ValidarEligibilidad 
Go--#SQL   

Create Table INT_MA__CFG_ValidarEligibilidad 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	URL_Produccion varchar(200) Not Null Default '',  
	URL_Pruebas varchar(200) Not Null Default ''	
)
Go--#SQL   

Alter Table INT_MA__CFG_ValidarEligibilidad 
	Add Constraint PK_INT_MA__CFG_ValidarEligibilidad Primary Key ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL   


------------------------------------------------------------------------------- 
	Insert Into INT_MA__CFG_ValidarEligibilidad ( IdEmpresa, IdEstado, IdFarmacia, URL_Produccion, URL_Pruebas )
	Select '002' as IdEmpresa, '09' as IdEstado, '0011' as IdFarmacia, 
		'http://www.mediaccess.com.mx:8081/wsoperaciones/VerificaElegibilidadConCopago.asmx' as URL_Produccion, 
		'http://www.mediaccess.com.mx/wsoperaciones/VerificaElegibilidadConCopago.asmx' as URL_Pruebas	
Go--#SQL   

	
	----sp_listacolumnas INT_MA__CFG_ValidarEligibilidad
	