Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__Clinicas' and xType = 'U' ) 
   Drop Table INT_AMPM__Clinicas 
Go--#SQL   

Create Table INT_AMPM__Clinicas 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	Referencia_AMPM varchar(20) Not Null Default '',
	Descripcion varchar(200) Not Null Default '',
	Direccion Varchar(500) Not Null Default '',
	Licencia_Sanitaria Varchar(30) Not Null Default '',
	Clue Varchar(30) Not Null Default '',
	id_tipo_ubicacion varchar(8) Not Null Default ''
)
Go--#SQL   

Alter Table INT_AMPM__Clinicas 
	Add Constraint PK_INT_AMPM__Clinicas Primary Key ( IdEmpresa, IdEstado, Referencia_AMPM ) 
Go--#SQL   

--	sp_generainserts INT_AMPM__Clinicas ,1 

If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '1' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '1', 'AMPM MATRIZ', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM MATRIZ', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '1'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '10' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '10', 'AMPM CENTRAL DE ABASTOS', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM CENTRAL DE ABASTOS', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '10'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '11' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '11', 'ampm guillermo roman', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'ampm guillermo roman', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '11'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '12' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '12', 'AMPM Maximiliano', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM Maximiliano', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '12'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '13' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '13', 'AMPM Quetzatcoatl', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM Quetzatcoatl', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '13'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '14' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '14', 'AMPM SANTA CATARINA', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM SANTA CATARINA', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '14'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '15' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '15', 'AMPM Caid', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM Caid', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '15'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '16' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '16', 'AMPM Zumpango Sauces', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM Zumpango Sauces', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '16'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '17' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '17', 'AMPM Zumpango Dif Centro', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM Zumpango Dif Centro', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '17'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '18' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '18', 'AMPM Zumpango San Sebastian', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM Zumpango San Sebastian', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '18'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '19' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '19', 'AMPM INVI', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM INVI', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '19'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '2' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '2', 'AMPM TACUBAYA', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM TACUBAYA', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '2'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '20' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '20', 'AMPM Tecamac Herores', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM Tecamac Herores', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '20'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '21' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '21', 'AMPM Tecamac Villas del Real', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM Tecamac Villas del Real', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '21'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '22' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '22', 'AMPM BRIGADAS', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM BRIGADAS', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '22'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '23' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '23', 'AMPM IZTAPALAPA', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM IZTAPALAPA', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '23'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '24' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '24', 'AMPM CALIMAYA', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM CALIMAYA', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '24'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '25' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '25', 'AMPM CAPACITACION', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM CAPACITACION', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '25'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '26' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '26', 'AMPM RAYON', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM RAYON', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '26'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '27' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '27', 'jardines tlahuac', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'jardines tlahuac', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '27'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '28' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '28', 'ampm cuauhtemoc', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'ampm cuauhtemoc', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '28'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '3' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '3', 'ampm tasqueña', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'ampm tasqueña', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '3'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '4' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '4', 'AMPM CANDELARIA', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM CANDELARIA', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '4'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '5' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '5', 'AMPM PANTITLAN', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM PANTITLAN', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '5'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '6' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '6', 'AMPM ZARAGOZA', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM ZARAGOZA', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '6'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '7' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '7', 'AMPM ROSARIO', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM ROSARIO', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '7'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '8' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '8', 'AMPM INSTITUTO DEL PETROLEO', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM INSTITUTO DEL PETROLEO', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '8'  
If Not Exists ( Select * From INT_AMPM__Clinicas Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '9' )  Insert Into INT_AMPM__Clinicas (  IdEmpresa, IdEstado, Referencia_AMPM, Descripcion, Direccion, Licencia_Sanitaria, Clue, id_tipo_ubicacion )  Values ( '002', '09', '9', 'AMPM MARTIN CARRERA', '', '', '', '3' ) 
 Else Update INT_AMPM__Clinicas Set Descripcion = 'AMPM MARTIN CARRERA', Direccion = '', Licencia_Sanitaria = '', Clue = '', id_tipo_ubicacion = '3' Where IdEmpresa = '002' and IdEstado = '09' and Referencia_AMPM = '9'  


Go--#SQL 




