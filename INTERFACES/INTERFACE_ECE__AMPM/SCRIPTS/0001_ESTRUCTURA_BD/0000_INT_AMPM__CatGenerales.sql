Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__CatGenerales' and xType = 'U' ) 
   Drop Table INT_AMPM__CatGenerales 
Go--#SQL   

Create Table INT_AMPM__CatGenerales
( 
	ID varchar(3) Not Null, 
	id_padre varchar(3) Not Null, 
	Tipo varchar(30) Not Null Default '',
	Descripcion varchar(200) Not Null Default '',
	Status Varchar(1) Not Null Default '',
	orden Varchar(3) Not Null Default '',
	valor varchar(8) Not Null Default ''
)
Go--#SQL   

Alter Table INT_AMPM__CatGenerales 
	Add Constraint PK_INT_AMPM__CatGenerales Primary Key ( ID ) 
Go--#SQL   

--	sp_generainserts INT_AMPM__CatGenerales ,1 

If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '1' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '1', '', 'unidad', 'mcg', 'A', '1', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'mcg', Status = 'A', orden = '1', valor = '' Where ID = '1'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '10' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '10', '', 'unidad', 'µg/ml', 'A', '10', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'µg/ml', Status = 'A', orden = '10', valor = '' Where ID = '10'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '100' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '100', '', 'unidad', 'U', 'A', '26', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'U', Status = 'A', orden = '26', valor = '' Where ID = '100'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '101' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '101', '', 'unidad', 'ml/ml', 'A', '27', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'ml/ml', Status = 'A', orden = '27', valor = '' Where ID = '101'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '102' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '102', '', 'unidad', 'U Speuwood', 'A', '28', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'U Speuwood', Status = 'A', orden = '28', valor = '' Where ID = '102'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '103' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '103', '', 'unidad', 'mg/dose', 'A', '29', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'mg/dose', Status = 'A', orden = '29', valor = '' Where ID = '103'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '104' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '104', '', 'unidad', 'UAH/ml', 'A', '30', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'UAH/ml', Status = 'A', orden = '30', valor = '' Where ID = '104'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '105' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '105', '', 'unidad', 'UI Axa', 'A', '31', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'UI Axa', Status = 'A', orden = '31', valor = '' Where ID = '105'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '106' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '106', '', 'unidad', 'kUI', 'A', '32', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'kUI', Status = 'A', orden = '32', valor = '' Where ID = '106'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '107' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '107', '', 'unidad', 'mg/goutte', 'A', '33', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'mg/goutte', Status = 'A', orden = '33', valor = '' Where ID = '107'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '108' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '108', '', 'unidad', 'UI/g', 'A', '34', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'UI/g', Status = 'A', orden = '34', valor = '' Where ID = '108'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '109' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '109', '', 'unidad', 'g/l', 'A', '35', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'g/l', Status = 'A', orden = '35', valor = '' Where ID = '109'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '11' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '11', '', 'unidad', 'M UI', 'A', '11', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'M UI', Status = 'A', orden = '11', valor = '' Where ID = '11'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '110' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '110', '', 'unidad', 'U/g', 'A', '36', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'U/g', Status = 'A', orden = '36', valor = '' Where ID = '110'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '111' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '111', '', 'unidad', 'mg/cm²', 'A', '37', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'mg/cm²', Status = 'A', orden = '37', valor = '' Where ID = '111'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '112' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '112', '', 'unidad', 'mmol/ml', 'A', '38', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'mmol/ml', Status = 'A', orden = '38', valor = '' Where ID = '112'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '113' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '113', '', 'unidad', 'g/g', 'A', '39', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'g/g', Status = 'A', orden = '39', valor = '' Where ID = '113'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '114' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '114', '', 'unidad', 'Gota(s)', 'A', '40', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'Gota(s)', Status = 'A', orden = '40', valor = '' Where ID = '114'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '115' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '115', '', 'unidad', 'cc', 'A', '41', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'cc', Status = 'A', orden = '41', valor = '' Where ID = '115'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '12' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '12', '', 'unidad', 'M UI/ml', 'A', '12', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'M UI/ml', Status = 'A', orden = '12', valor = '' Where ID = '12'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '13' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '13', '', 'unidad', 'mg/24 h', 'A', '13', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'mg/24 h', Status = 'A', orden = '13', valor = '' Where ID = '13'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '14' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '14', '', 'unidad', 'UI/ml', 'A', '14', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'UI/ml', Status = 'A', orden = '14', valor = '' Where ID = '14'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '15' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '15', '', 'unidad', 'UI', 'A', '15', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'UI', Status = 'A', orden = '15', valor = '' Where ID = '15'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '16' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '16', '', 'unidad', 'mg/g', 'A', '16', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'mg/g', Status = 'A', orden = '16', valor = '' Where ID = '16'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '17' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '17', '', 'unidad', 'ml', 'A', '17', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'ml', Status = 'A', orden = '17', valor = '' Where ID = '17'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '18' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '18', '', 'unidad', 'µg/µl', 'A', '18', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'µg/µl', Status = 'A', orden = '18', valor = '' Where ID = '18'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '19' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '19', '', 'unidad', 'µg/g', 'A', '19', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'µg/g', Status = 'A', orden = '19', valor = '' Where ID = '19'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '2' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '2', '', 'unidad', '%', 'A', '2', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = '%', Status = 'A', orden = '2', valor = '' Where ID = '2'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '20' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '20', '', 'unidad', 'UIK/ml', 'A', '20', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'UIK/ml', Status = 'A', orden = '20', valor = '' Where ID = '20'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '21' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '21', '', 'unidad', 'ppm mole/mole', 'A', '21', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'ppm mole/mole', Status = 'A', orden = '21', valor = '' Where ID = '21'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '22' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '22', '', 'unidad', 'µg/dose(s)', 'A', '22', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'µg/dose(s)', Status = 'A', orden = '22', valor = '' Where ID = '22'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '23' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '23', '', 'unidad', 'U/ml', 'A', '23', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'U/ml', Status = 'A', orden = '23', valor = '' Where ID = '23'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '24' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '24', '', 'unidad', 'mg/16h', 'A', '24', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'mg/16h', Status = 'A', orden = '24', valor = '' Where ID = '24'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '25' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '25', '', 'frecuencia', 'Dosis única', 'A', '1', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'Dosis única', Status = 'A', orden = '1', valor = '' Where ID = '25'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '26' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '26', '', 'frecuencia', 'c/30 min', 'A', '2', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/30 min', Status = 'A', orden = '2', valor = '' Where ID = '26'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '27' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '27', '', 'frecuencia', 'c/1 h', 'A', '3', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/1 h', Status = 'A', orden = '3', valor = '' Where ID = '27'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '28' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '28', '', 'frecuencia', 'c/2 h', 'A', '4', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/2 h', Status = 'A', orden = '4', valor = '' Where ID = '28'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '29' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '29', '', 'frecuencia', 'c/3 h', 'A', '5', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/3 h', Status = 'A', orden = '5', valor = '' Where ID = '29'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '3' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '3', '', 'unidad', 'mg', 'A', '3', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'mg', Status = 'A', orden = '3', valor = '' Where ID = '3'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '30' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '30', '', 'frecuencia', 'c/4 h', 'A', '6', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/4 h', Status = 'A', orden = '6', valor = '' Where ID = '30'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '31' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '31', '', 'frecuencia', 'c/5 h', 'A', '7', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/5 h', Status = 'A', orden = '7', valor = '' Where ID = '31'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '32' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '32', '', 'frecuencia', 'c/6 h', 'A', '8', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/6 h', Status = 'A', orden = '8', valor = '' Where ID = '32'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '33' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '33', '', 'frecuencia', 'c/7 h', 'A', '9', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/7 h', Status = 'A', orden = '9', valor = '' Where ID = '33'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '34' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '34', '', 'frecuencia', 'c/8 h', 'A', '10', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/8 h', Status = 'A', orden = '10', valor = '' Where ID = '34'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '35' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '35', '', 'frecuencia', 'c/9 h', 'A', '11', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/9 h', Status = 'A', orden = '11', valor = '' Where ID = '35'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '36' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '36', '', 'frecuencia', 'c/10 h', 'A', '12', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/10 h', Status = 'A', orden = '12', valor = '' Where ID = '36'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '37' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '37', '', 'frecuencia', 'c/11 h', 'A', '13', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/11 h', Status = 'A', orden = '13', valor = '' Where ID = '37'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '38' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '38', '', 'frecuencia', 'c/12 h', 'A', '14', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/12 h', Status = 'A', orden = '14', valor = '' Where ID = '38'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '39' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '39', '', 'frecuencia', 'c/13 h', 'A', '15', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/13 h', Status = 'A', orden = '15', valor = '' Where ID = '39'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '4' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '4', '', 'unidad', 'µg/h', 'A', '4', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'µg/h', Status = 'A', orden = '4', valor = '' Where ID = '4'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '40' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '40', '', 'frecuencia', 'c/14 h', 'A', '16', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/14 h', Status = 'A', orden = '16', valor = '' Where ID = '40'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '41' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '41', '', 'frecuencia', 'c/15 h', 'A', '17', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/15 h', Status = 'A', orden = '17', valor = '' Where ID = '41'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '42' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '42', '', 'frecuencia', 'c/16 h', 'A', '18', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/16 h', Status = 'A', orden = '18', valor = '' Where ID = '42'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '43' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '43', '', 'frecuencia', 'c/17 h', 'A', '19', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/17 h', Status = 'A', orden = '19', valor = '' Where ID = '43'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '44' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '44', '', 'frecuencia', 'c/18 h', 'A', '20', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/18 h', Status = 'A', orden = '20', valor = '' Where ID = '44'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '45' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '45', '', 'frecuencia', 'c/19 h', 'A', '21', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/19 h', Status = 'A', orden = '21', valor = '' Where ID = '45'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '46' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '46', '', 'frecuencia', 'c/20 h', 'A', '22', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/20 h', Status = 'A', orden = '22', valor = '' Where ID = '46'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '47' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '47', '', 'frecuencia', 'c/21 h', 'A', '23', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/21 h', Status = 'A', orden = '23', valor = '' Where ID = '47'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '48' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '48', '', 'frecuencia', 'c/21 h', 'C', '24', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/21 h', Status = 'C', orden = '24', valor = '' Where ID = '48'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '49' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '49', '', 'frecuencia', 'c/22 h', 'A', '25', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/22 h', Status = 'A', orden = '25', valor = '' Where ID = '49'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '5' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '5', '', 'unidad', 'mg/ml', 'A', '5', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'mg/ml', Status = 'A', orden = '5', valor = '' Where ID = '5'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '50' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '50', '', 'frecuencia', 'c/23 h', 'A', '26', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/23 h', Status = 'A', orden = '26', valor = '' Where ID = '50'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '51' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '51', '', 'frecuencia', 'c/24 h', 'A', '27', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/24 h', Status = 'A', orden = '27', valor = '' Where ID = '51'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '52' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '52', '', 'frecuencia', 'c/36 h', 'A', '28', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/36 h', Status = 'A', orden = '28', valor = '' Where ID = '52'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '53' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '53', '', 'frecuencia', 'c/48 h', 'A', '29', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/48 h', Status = 'A', orden = '29', valor = '' Where ID = '53'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '54' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '54', '', 'frecuencia', 'c/5 d', 'A', '30', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/5 d', Status = 'A', orden = '30', valor = '' Where ID = '54'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '55' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '55', '', 'frecuencia', 'c/7 d', 'A', '31', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/7 d', Status = 'A', orden = '31', valor = '' Where ID = '55'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '56' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '56', '', 'frecuencia', 'c/15 d', 'A', '32', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/15 d', Status = 'A', orden = '32', valor = '' Where ID = '56'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '57' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '57', '', 'frecuencia', 'c/30 d', 'A', '33', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'frecuencia', Descripcion = 'c/30 d', Status = 'A', orden = '33', valor = '' Where ID = '57'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '58' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '58', '', 'via_administracion', 'Cutánea en glándula mamaria', 'A', '1', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Cutánea en glándula mamaria', Status = 'A', orden = '1', valor = '' Where ID = '58'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '59' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '59', '', 'via_administracion', 'Cutánea/local', 'A', '2', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Cutánea/local', Status = 'A', orden = '2', valor = '' Where ID = '59'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '6' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '6', '', 'unidad', 'g', 'A', '6', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'g', Status = 'A', orden = '6', valor = '' Where ID = '6'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '60' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '60', '', 'via_administracion', 'Enteral a través de sonda', 'A', '3', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Enteral a través de sonda', Status = 'A', orden = '3', valor = '' Where ID = '60'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '61' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '61', '', 'via_administracion', 'Epidural', 'A', '4', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Epidural', Status = 'A', orden = '4', valor = '' Where ID = '61'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '62' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '62', '', 'via_administracion', 'Implante (en el sitio de infección)', 'A', '5', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Implante (en el sitio de infección)', Status = 'A', orden = '5', valor = '' Where ID = '62'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '63' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '63', '', 'via_administracion', 'Implante subcutáneo', 'A', '6', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Implante subcutáneo', Status = 'A', orden = '6', valor = '' Where ID = '63'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '64' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '64', '', 'via_administracion', 'Infiltración', 'A', '7', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Infiltración', Status = 'A', orden = '7', valor = '' Where ID = '64'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '65' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '65', '', 'via_administracion', 'Inhalación', 'A', '8', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Inhalación', Status = 'A', orden = '8', valor = '' Where ID = '65'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '66' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '66', '', 'via_administracion', 'Intraarticular', 'A', '9', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intraarticular', Status = 'A', orden = '9', valor = '' Where ID = '66'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '67' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '67', '', 'via_administracion', 'Intracavitaria', 'A', '10', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intracavitaria', Status = 'A', orden = '10', valor = '' Where ID = '67'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '68' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '68', '', 'via_administracion', 'Intradérmica', 'A', '11', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intradérmica', Status = 'A', orden = '11', valor = '' Where ID = '68'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '69' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '69', '', 'via_administracion', 'Intralesional', 'A', '12', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intralesional', Status = 'A', orden = '12', valor = '' Where ID = '69'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '7' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '7', '', 'unidad', 'µg', 'A', '7', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'µg', Status = 'A', orden = '7', valor = '' Where ID = '7'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '70' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '70', '', 'via_administracion', 'Intramuscular', 'A', '13', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intramuscular', Status = 'A', orden = '13', valor = '' Where ID = '70'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '71' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '71', '', 'via_administracion', 'Intramuscular profunda y lenta', 'A', '14', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intramuscular profunda y lenta', Status = 'A', orden = '14', valor = '' Where ID = '71'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '72' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '72', '', 'via_administracion', 'Intranasal', 'A', '15', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intranasal', Status = 'A', orden = '15', valor = '' Where ID = '72'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '73' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '73', '', 'via_administracion', 'Intraocular', 'A', '16', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intraocular', Status = 'A', orden = '16', valor = '' Where ID = '73'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '74' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '74', '', 'via_administracion', 'Intraperitoneal', 'A', '17', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intraperitoneal', Status = 'A', orden = '17', valor = '' Where ID = '74'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '75' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '75', '', 'via_administracion', 'Intrarraquídea', 'A', '18', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intrarraquídea', Status = 'A', orden = '18', valor = '' Where ID = '75'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '76' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '76', '', 'via_administracion', 'Intratecal', 'A', '19', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intratecal', Status = 'A', orden = '19', valor = '' Where ID = '76'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '77' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '77', '', 'via_administracion', 'Intratumoral', 'A', '20', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intratumoral', Status = 'A', orden = '20', valor = '' Where ID = '77'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '78' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '78', '', 'via_administracion', 'Intrauterina', 'A', '21', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intrauterina', Status = 'A', orden = '21', valor = '' Where ID = '78'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '79' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '79', '', 'via_administracion', 'Intravenosa', 'A', '22', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intravenosa', Status = 'A', orden = '22', valor = '' Where ID = '79'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '8' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '8', '', 'unidad', 'µg/24h', 'A', '8', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'µg/24h', Status = 'A', orden = '8', valor = '' Where ID = '8'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '80' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '80', '', 'via_administracion', 'Intravenosa en bolo', 'A', '23', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intravenosa en bolo', Status = 'A', orden = '23', valor = '' Where ID = '80'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '81' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '81', '', 'via_administracion', 'Intravenosa en infusión', 'A', '24', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intravenosa en infusión', Status = 'A', orden = '24', valor = '' Where ID = '81'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '82' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '82', '', 'via_administracion', 'Intravenosa lenta', 'A', '25', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intravenosa lenta', Status = 'A', orden = '25', valor = '' Where ID = '82'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '83' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '83', '', 'via_administracion', 'Intravenosa por infusión periférica', 'A', '26', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intravenosa por infusión periférica', Status = 'A', orden = '26', valor = '' Where ID = '83'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '84' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '84', '', 'via_administracion', 'Intravenosa, por catéter venoso central', 'A', '27', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intravenosa, por catéter venoso central', Status = 'A', orden = '27', valor = '' Where ID = '84'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '85' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '85', '', 'via_administracion', 'Intravesical', 'A', '28', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intravesical', Status = 'A', orden = '28', valor = '' Where ID = '85'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '86' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '86', '', 'via_administracion', 'Intravítrea.', 'A', '29', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Intravítrea.', Status = 'A', orden = '29', valor = '' Where ID = '86'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '87' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '87', '', 'via_administracion', 'Mucocutánea', 'A', '30', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Mucocutánea', Status = 'A', orden = '30', valor = '' Where ID = '87'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '88' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '88', '', 'via_administracion', 'Nasal', 'A', '31', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Nasal', Status = 'A', orden = '31', valor = '' Where ID = '88'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '89' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '89', '', 'via_administracion', 'Nasal con nebulización', 'A', '32', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Nasal con nebulización', Status = 'A', orden = '32', valor = '' Where ID = '89'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '9' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '9', '', 'unidad', 'g/ml', 'A', '9', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'g/ml', Status = 'A', orden = '9', valor = '' Where ID = '9'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '90' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '90', '', 'via_administracion', 'Oftálmica', 'A', '33', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Oftálmica', Status = 'A', orden = '33', valor = '' Where ID = '90'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '91' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '91', '', 'via_administracion', 'Oral', 'A', '34', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Oral', Status = 'A', orden = '34', valor = '' Where ID = '91'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '92' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '92', '', 'via_administracion', 'Otica', 'A', '35', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Otica', Status = 'A', orden = '35', valor = '' Where ID = '92'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '93' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '93', '', 'via_administracion', 'Rectal', 'A', '36', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Rectal', Status = 'A', orden = '36', valor = '' Where ID = '93'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '94' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '94', '', 'via_administracion', 'Subcutánea', 'A', '37', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Subcutánea', Status = 'A', orden = '37', valor = '' Where ID = '94'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '95' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '95', '', 'via_administracion', 'Sublingual', 'A', '38', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Sublingual', Status = 'A', orden = '38', valor = '' Where ID = '95'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '96' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '96', '', 'via_administracion', 'Tópica', 'A', '39', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Tópica', Status = 'A', orden = '39', valor = '' Where ID = '96'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '97' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '97', '', 'via_administracion', 'Transdermica', 'A', '40', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Transdermica', Status = 'A', orden = '40', valor = '' Where ID = '97'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '98' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '98', '', 'via_administracion', 'Vaginal', 'A', '41', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'via_administracion', Descripcion = 'Vaginal', Status = 'A', orden = '41', valor = '' Where ID = '98'  
If Not Exists ( Select * From INT_AMPM__CatGenerales Where ID = '99' )  Insert Into INT_AMPM__CatGenerales (  ID, id_padre, Tipo, Descripcion, Status, orden, valor )  Values ( '99', '', 'unidad', 'U Allergan', 'A', '25', '' ) 
 Else Update INT_AMPM__CatGenerales Set id_padre = '', Tipo = 'unidad', Descripcion = 'U Allergan', Status = 'A', orden = '25', valor = '' Where ID = '99'  


Go--#SQL 
