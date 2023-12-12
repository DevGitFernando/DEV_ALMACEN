Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_SESEQ__CentrosDeCostos' and xType = 'U' ) 
   Drop Table INT_SESEQ__CentrosDeCostos 
Go--#SQL   

Create Table INT_SESEQ__CentrosDeCostos 
(
	Clave varchar(20) Not Null Default '', 
	Nombre varchar(100) Not Null Default '' unique
)
Go--#SQL   

Alter Table INT_SESEQ__CentrosDeCostos 
	Add Constraint PK_INT_SESEQ__CentrosDeCostos Primary Key ( Clave ) 
Go--#SQL  


-- sp_generainserts INT_SESEQ__CentrosDeCostos, 1 
If Not Exists ( Select * From INT_SESEQ__CentrosDeCostos Where Clave = '999' )  Insert Into INT_SESEQ__CentrosDeCostos (  Clave, Nombre )  Values ( '999', 'ALMACEN INTERMED' )  Else Update INT_SESEQ__CentrosDeCostos Set Nombre = 'ALMACEN INTERMED' Where Clave = '999'  
If Not Exists ( Select * From INT_SESEQ__CentrosDeCostos Where Clave = '939' )  Insert Into INT_SESEQ__CentrosDeCostos (  Clave, Nombre )  Values ( '939', 'CENTRO DE DISTRIBUCION ESTATAL' )  Else Update INT_SESEQ__CentrosDeCostos Set Nombre = 'CENTRO DE DISTRIBUCION ESTATAL' Where Clave = '939'  
If Not Exists ( Select * From INT_SESEQ__CentrosDeCostos Where Clave = '926' )  Insert Into INT_SESEQ__CentrosDeCostos (  Clave, Nombre )  Values ( '926', 'HOSPITAL COVID-19' )  Else Update INT_SESEQ__CentrosDeCostos Set Nombre = 'HOSPITAL COVID-19' Where Clave = '926'  
If Not Exists ( Select * From INT_SESEQ__CentrosDeCostos Where Clave = '934' )  Insert Into INT_SESEQ__CentrosDeCostos (  Clave, Nombre )  Values ( '934', 'HOSPITAL DE ESPECIALIDADES DEL NIÑO Y LA MUJER' )  Else Update INT_SESEQ__CentrosDeCostos Set Nombre = 'HOSPITAL DE ESPECIALIDADES DEL NIÑO Y LA MUJER' Where Clave = '934'  
If Not Exists ( Select * From INT_SESEQ__CentrosDeCostos Where Clave = '930' )  Insert Into INT_SESEQ__CentrosDeCostos (  Clave, Nombre )  Values ( '930', 'HOSPITAL GENERAL CADEREYTA' )  Else Update INT_SESEQ__CentrosDeCostos Set Nombre = 'HOSPITAL GENERAL CADEREYTA' Where Clave = '930'  
If Not Exists ( Select * From INT_SESEQ__CentrosDeCostos Where Clave = '925' )  Insert Into INT_SESEQ__CentrosDeCostos (  Clave, Nombre )  Values ( '925', 'HOSPITAL GENERAL JALPAN' )  Else Update INT_SESEQ__CentrosDeCostos Set Nombre = 'HOSPITAL GENERAL JALPAN' Where Clave = '925'  
If Not Exists ( Select * From INT_SESEQ__CentrosDeCostos Where Clave = '928' )  Insert Into INT_SESEQ__CentrosDeCostos (  Clave, Nombre )  Values ( '928', 'HOSPITAL GENERAL SAN JUAN DEL RIO' )  Else Update INT_SESEQ__CentrosDeCostos Set Nombre = 'HOSPITAL GENERAL SAN JUAN DEL RIO' Where Clave = '928'  
If Not Exists ( Select * From INT_SESEQ__CentrosDeCostos Where Clave = '1466' )  Insert Into INT_SESEQ__CentrosDeCostos (  Clave, Nombre )  Values ( '1466', 'NUEVO HOSPITAL GENERAL QUERETARO' )  Else Update INT_SESEQ__CentrosDeCostos Set Nombre = 'NUEVO HOSPITAL GENERAL QUERETARO' Where Clave = '1466'  

Go--#SQL  


------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_SESEQ__CFG_Farmacias_UMedicas' and xType = 'U' ) 
   Drop Table INT_SESEQ__CFG_Farmacias_UMedicas 
Go--#SQL   

Create Table INT_SESEQ__CFG_Farmacias_UMedicas 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Referencia_SESEQ varchar(20) Not Null Default '', 
	URL_Interface varchar(1000) Not Null Default '', 
	CapturaInformacion bit Not Null Default 'true', 
	Referencia_SESEQ_CentroDeCostos varchar(10) Not NUll default '',
	Referencia_SESEQ_CCC varchar(10) Not NUll default '',
	URL_InformacionOperacion Varchar(1000) Not NUll default '', 

	EnviarInformacion bit Not Null Default 'true', 
	EnviarDigitalizacion bit Not Null Default 'false' 
)
Go--#SQL   

Alter Table INT_SESEQ__CFG_Farmacias_UMedicas 
	Add Constraint PK_INT_SESEQ__CFG_Farmacias_UMedicas Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ ) 
Go--#SQL   

--		sp_generainserts INT_SESEQ__CFG_Farmacias_UMedicas ,   1 

Go--#SQL 


--		QTSSA001752-U  



	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '001' and IdEstado = '22' and IdFarmacia = '0003' and Referencia_SESEQ = 'QTSSA000000' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '001', '22', '0003', 'QTSSA000000', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '001' and IdEstado = '22' and IdFarmacia = '0003' and Referencia_SESEQ = 'QTSSA000000'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0104' and Referencia_SESEQ = 'QTSSA000000' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0104', 'QTSSA000000', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0104' and Referencia_SESEQ = 'QTSSA000000'  



	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '001' and IdEstado = '11' and IdFarmacia = '3005' and Referencia_SESEQ = 'QTSSA000000' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '001', '11', '3005', 'QTSSA000000', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '001' and IdEstado = '11' and IdFarmacia = '3005' and Referencia_SESEQ = 'QTSSA000000'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '001' and IdEstado = '11' and IdFarmacia = '1008' and Referencia_SESEQ = 'QTSSA000000' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '001', '11', '1008', 'QTSSA000000', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '001' and IdEstado = '11' and IdFarmacia = '1008' and Referencia_SESEQ = 'QTSSA000000'  


	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0111' and Referencia_SESEQ = 'QTSSA001752' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0111', 'QTSSA001752', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0111' and Referencia_SESEQ = 'QTSSA001752'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0112' and Referencia_SESEQ = 'QTSSA001752' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0112', 'QTSSA001752', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0112' and Referencia_SESEQ = 'QTSSA001752'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0113' and Referencia_SESEQ = 'QTSSA001740' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0113', 'QTSSA001740', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0113' and Referencia_SESEQ = 'QTSSA001740'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0114' and Referencia_SESEQ = 'QTSSA001740' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0114', 'QTSSA001740', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0114' and Referencia_SESEQ = 'QTSSA001740'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0115' and Referencia_SESEQ = 'QTSSA012935' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0115', 'QTSSA012935', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0115' and Referencia_SESEQ = 'QTSSA012935'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0116' and Referencia_SESEQ = 'QTSSA000475' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0116', 'QTSSA000475', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0116' and Referencia_SESEQ = 'QTSSA000475'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0117' and Referencia_SESEQ = 'QTSSA001052' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0117', 'QTSSA001052', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0117' and Referencia_SESEQ = 'QTSSA001052'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0118' and Referencia_SESEQ = 'QTSSA001752' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0118', 'QTSSA001752', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0118' and Referencia_SESEQ = 'QTSSA001752'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0119' and Referencia_SESEQ = 'QTSSA001740' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0119', 'QTSSA001740', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0119' and Referencia_SESEQ = 'QTSSA001740'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0120' and Referencia_SESEQ = 'QTSSA012935' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0120', 'QTSSA012935', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0120' and Referencia_SESEQ = 'QTSSA012935'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0121' and Referencia_SESEQ = 'QTSSA000475' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0121', 'QTSSA000475', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0121' and Referencia_SESEQ = 'QTSSA000475'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0122' and Referencia_SESEQ = 'QTSSA001052' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0122', 'QTSSA001052', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0122' and Referencia_SESEQ = 'QTSSA001052'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0123' and Referencia_SESEQ = 'QTSSA001752' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0123', 'QTSSA001752', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0123' and Referencia_SESEQ = 'QTSSA001752'  


	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0125' and Referencia_SESEQ = 'QTSSA002901' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0125', 'QTSSA002901', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0125' and Referencia_SESEQ = 'QTSSA002901'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0126' and Referencia_SESEQ = 'QTSSA002901' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0126', 'QTSSA002901', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0126' and Referencia_SESEQ = 'QTSSA002901'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0127' and Referencia_SESEQ = 'QTSSA002901' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0127', 'QTSSA002901', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0127' and Referencia_SESEQ = 'QTSSA002901'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0128' and Referencia_SESEQ = 'QTSSA002901' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0128', 'QTSSA002901', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0128' and Referencia_SESEQ = 'QTSSA002901'  

	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0129' and Referencia_SESEQ = 'QTSSA001752' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0129', 'QTSSA001752', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0129' and Referencia_SESEQ = 'QTSSA001752'  
	If Not Exists ( Select * From INT_SESEQ__CFG_Farmacias_UMedicas Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0130' and Referencia_SESEQ = 'QTSSA001735' )  Insert Into INT_SESEQ__CFG_Farmacias_UMedicas (  IdEmpresa, IdEstado, IdFarmacia, Referencia_SESEQ, URL_Interface, CapturaInformacion )  Values ( '004', '22', '0130', 'QTSSA001735', 'http://DEV/SERVICIOS/SESEQ/wsRecepcionRecetasColectivosFarmacia/wsISESEQ.asmx', 1 )  Else Update INT_SESEQ__CFG_Farmacias_UMedicas Set CapturaInformacion = 1 Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0130' and Referencia_SESEQ = 'QTSSA001735'  

Go--#SQL 


		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '999', Referencia_SESEQ_CCC = '999.1' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0104' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '999', Referencia_SESEQ_CCC = '999.2' Where IdEmpresa = '001' and IdEstado = '22' and IdFarmacia = '0003' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '999', Referencia_SESEQ_CCC = '999.3' Where IdEmpresa = '001' and IdEstado = '11' and IdFarmacia = '3005' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '999', Referencia_SESEQ_CCC = '999.4' Where IdEmpresa = '001' and IdEstado = '11' and IdFarmacia = '1008' 


		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '926', Referencia_SESEQ_CCC = '926.1' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0111' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '934', Referencia_SESEQ_CCC = '934.1' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0113' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '934', Referencia_SESEQ_CCC = '934.3' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0114' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '928', Referencia_SESEQ_CCC = '928.1' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0115' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '930', Referencia_SESEQ_CCC = '930.1' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0116' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '925', Referencia_SESEQ_CCC = '925.1' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0117' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '926', Referencia_SESEQ_CCC = '926.2' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0118' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '934', Referencia_SESEQ_CCC = '934.2' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0119' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '928', Referencia_SESEQ_CCC = '928.2' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0120' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '930', Referencia_SESEQ_CCC = '930.2' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0121' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '925', Referencia_SESEQ_CCC = '925.2' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0122' 

		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '1466', Referencia_SESEQ_CCC = '1466.5' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0124' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '1466', Referencia_SESEQ_CCC = '1466.1' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0125' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '1466', Referencia_SESEQ_CCC = '1466.2' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0126' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '1466', Referencia_SESEQ_CCC = '1466.3' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0127' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '1466', Referencia_SESEQ_CCC = '1466.4' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0128' 

		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '1466', Referencia_SESEQ_CCC = '1466.4' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0129' 
		Update INT_SESEQ__CFG_Farmacias_UMedicas Set Referencia_SESEQ_CentroDeCostos = '937', Referencia_SESEQ_CCC = '937' Where IdEmpresa = '004' and IdEstado = '22' and IdFarmacia = '0130' 

Go--#SQL 

