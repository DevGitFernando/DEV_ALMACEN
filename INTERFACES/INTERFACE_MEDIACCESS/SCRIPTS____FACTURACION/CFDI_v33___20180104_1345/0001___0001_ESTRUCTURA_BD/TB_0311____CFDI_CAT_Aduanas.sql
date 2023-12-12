
-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Aduanas' and xType = 'U' ) 
	Drop Table CFDI_Aduanas  
Go--#SQL  

Create Table CFDI_Aduanas 
(	
	Clave varchar(4) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '',  
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_Aduanas Add Constraint PK_CFDI_Aduanas Primary	Key ( Clave ) 
Go--#SQL 


If Not Exists ( Select * From CFDI_Aduanas Where Clave = '01' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '01', 'ACAPULCO, ACAPULCO DE JUAREZ, GUERRERO.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'ACAPULCO, ACAPULCO DE JUAREZ, GUERRERO.', Status = 'A' Where Clave = '01'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '02' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '02', 'AGUA PRIETA, AGUA PRIETA, SONORA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'AGUA PRIETA, AGUA PRIETA, SONORA.', Status = 'A' Where Clave = '02'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '05' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '05', 'SUBTENIENTE LOPEZ, SUBTENIENTE LOPEZ, QUINTANA ROO.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'SUBTENIENTE LOPEZ, SUBTENIENTE LOPEZ, QUINTANA ROO.', Status = 'A' Where Clave = '05'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '06' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '06', 'CIUDAD DEL CARMEN, CIUDAD DEL CARMEN, CAMPECHE.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'CIUDAD DEL CARMEN, CIUDAD DEL CARMEN, CAMPECHE.', Status = 'A' Where Clave = '06'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '07' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '07', 'CIUDAD JUAREZ, CIUDAD JUAREZ, CHIHUAHUA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'CIUDAD JUAREZ, CIUDAD JUAREZ, CHIHUAHUA.', Status = 'A' Where Clave = '07'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '08' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '08', 'COATZACOALCOS, COATZACOALCOS, VERACRUZ.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'COATZACOALCOS, COATZACOALCOS, VERACRUZ.', Status = 'A' Where Clave = '08'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '11' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '11', 'ENSENADA, ENSENADA, BAJA CALIFORNIA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'ENSENADA, ENSENADA, BAJA CALIFORNIA.', Status = 'A' Where Clave = '11'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '12' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '12', 'GUAYMAS, GUAYMAS, SONORA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'GUAYMAS, GUAYMAS, SONORA.', Status = 'A' Where Clave = '12'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '14' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '14', 'LA PAZ, LA PAZ, BAJA CALIFORNIA SUR.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'LA PAZ, LA PAZ, BAJA CALIFORNIA SUR.', Status = 'A' Where Clave = '14'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '16' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '16', 'MANZANILLO, MANZANILLO, COLIMA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'MANZANILLO, MANZANILLO, COLIMA.', Status = 'A' Where Clave = '16'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '17' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '17', 'MATAMOROS, MATAMOROS, TAMAULIPAS.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'MATAMOROS, MATAMOROS, TAMAULIPAS.', Status = 'A' Where Clave = '17'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '18' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '18', 'MAZATLAN, MAZATLAN, SINALOA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'MAZATLAN, MAZATLAN, SINALOA.', Status = 'A' Where Clave = '18'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '19' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '19', 'MEXICALI, MEXICALI, BAJA CALIFORNIA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'MEXICALI, MEXICALI, BAJA CALIFORNIA.', Status = 'A' Where Clave = '19'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '20' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '20', 'MEXICO, DISTRITO FEDERAL.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'MEXICO, DISTRITO FEDERAL.', Status = 'A' Where Clave = '20'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '22' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '22', 'NACO, NACO, SONORA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'NACO, NACO, SONORA.', Status = 'A' Where Clave = '22'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '23' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '23', 'NOGALES, NOGALES, SONORA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'NOGALES, NOGALES, SONORA.', Status = 'A' Where Clave = '23'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '24' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '24', 'NUEVO LAREDO, NUEVO LAREDO, TAMAULIPAS.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'NUEVO LAREDO, NUEVO LAREDO, TAMAULIPAS.', Status = 'A' Where Clave = '24'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '25' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '25', 'OJINAGA, OJINAGA, CHIHUAHUA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'OJINAGA, OJINAGA, CHIHUAHUA.', Status = 'A' Where Clave = '25'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '26' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '26', 'PUERTO PALOMAS, PUERTO PALOMAS, CHIHUAHUA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'PUERTO PALOMAS, PUERTO PALOMAS, CHIHUAHUA.', Status = 'A' Where Clave = '26'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '27' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '27', 'PIEDRAS NEGRAS, PIEDRAS NEGRAS, COAHUILA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'PIEDRAS NEGRAS, PIEDRAS NEGRAS, COAHUILA.', Status = 'A' Where Clave = '27'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '28' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '28', 'PROGRESO, PROGRESO, YUCATAN.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'PROGRESO, PROGRESO, YUCATAN.', Status = 'A' Where Clave = '28'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '30' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '30', 'CIUDAD REYNOSA, CIUDAD REYNOSA, TAMAULIPAS.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'CIUDAD REYNOSA, CIUDAD REYNOSA, TAMAULIPAS.', Status = 'A' Where Clave = '30'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '31' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '31', 'SALINA CRUZ, SALINA CRUZ, OAXACA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'SALINA CRUZ, SALINA CRUZ, OAXACA.', Status = 'A' Where Clave = '31'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '33' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '33', 'SAN LUIS RIO COLORADO, SAN LUIS RIO COLORADO, SONORA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'SAN LUIS RIO COLORADO, SAN LUIS RIO COLORADO, SONORA.', Status = 'A' Where Clave = '33'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '34' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '34', 'CIUDAD MIGUEL ALEMAN, CIUDAD MIGUEL ALEMAN, TAMAULIPAS.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'CIUDAD MIGUEL ALEMAN, CIUDAD MIGUEL ALEMAN, TAMAULIPAS.', Status = 'A' Where Clave = '34'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '37' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '37', 'CIUDAD HIDALGO, CIUDAD HIDALGO, CHIAPAS.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'CIUDAD HIDALGO, CIUDAD HIDALGO, CHIAPAS.', Status = 'A' Where Clave = '37'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '38' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '38', 'TAMPICO, TAMPICO, TAMAULIPAS.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'TAMPICO, TAMPICO, TAMAULIPAS.', Status = 'A' Where Clave = '38'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '39' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '39', 'TECATE, TECATE, BAJA CALIFORNIA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'TECATE, TECATE, BAJA CALIFORNIA.', Status = 'A' Where Clave = '39'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '40' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '40', 'TIJUANA, TIJUANA, BAJA CALIFORNIA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'TIJUANA, TIJUANA, BAJA CALIFORNIA.', Status = 'A' Where Clave = '40'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '42' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '42', 'TUXPAN, TUXPAN DE RODRIGUEZ CANO, VERACRUZ.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'TUXPAN, TUXPAN DE RODRIGUEZ CANO, VERACRUZ.', Status = 'A' Where Clave = '42'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '43' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '43', 'VERACRUZ, VERACRUZ, VERACRUZ.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'VERACRUZ, VERACRUZ, VERACRUZ.', Status = 'A' Where Clave = '43'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '44' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '44', 'CIUDAD ACUÑA, CIUDAD ACUÑA, COAHUILA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'CIUDAD ACUÑA, CIUDAD ACUÑA, COAHUILA.', Status = 'A' Where Clave = '44'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '46' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '46', 'TORREON, TORREON, COAHUILA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'TORREON, TORREON, COAHUILA.', Status = 'A' Where Clave = '46'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '47' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '47', 'AEROPUERTO INTERNACIONAL DE LA CIUDAD DE MEXICO,', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'AEROPUERTO INTERNACIONAL DE LA CIUDAD DE MEXICO,', Status = 'A' Where Clave = '47'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '48' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '48', 'GUADALAJARA, TLACOMULCO DE ZUÑIGA, JALISCO.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'GUADALAJARA, TLACOMULCO DE ZUÑIGA, JALISCO.', Status = 'A' Where Clave = '48'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '50' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '50', 'SONOYTA, SONOYTA, SONORA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'SONOYTA, SONOYTA, SONORA.', Status = 'A' Where Clave = '50'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '51' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '51', 'LAZARO CARDENAS, LAZARO CARDENAS, MICHOACAN.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'LAZARO CARDENAS, LAZARO CARDENAS, MICHOACAN.', Status = 'A' Where Clave = '51'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '52' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '52', 'MONTERREY, GENERAL MARIANO ESCOBEDO, NUEVO LEON.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'MONTERREY, GENERAL MARIANO ESCOBEDO, NUEVO LEON.', Status = 'A' Where Clave = '52'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '53' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '53', 'CANCUN, CANCUN, QUINTANA ROO.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'CANCUN, CANCUN, QUINTANA ROO.', Status = 'A' Where Clave = '53'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '64' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '64', 'QUERÉTARO, EL MARQUÉS Y COLON, QUERÉTARO.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'QUERÉTARO, EL MARQUÉS Y COLON, QUERÉTARO.', Status = 'A' Where Clave = '64'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '65' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '65', 'TOLUCA, TOLUCA, ESTADO DE MEXICO.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'TOLUCA, TOLUCA, ESTADO DE MEXICO.', Status = 'A' Where Clave = '65'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '67' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '67', 'CHIHUAHUA, CHIHUAHUA, CHIHUAHUA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'CHIHUAHUA, CHIHUAHUA, CHIHUAHUA.', Status = 'A' Where Clave = '67'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '73' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '73', 'AGUASCALIENTES, AGUASCALIENTES, AGUASCALIENTES.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'AGUASCALIENTES, AGUASCALIENTES, AGUASCALIENTES.', Status = 'A' Where Clave = '73'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '75' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '75', 'PUEBLA, HEROICA PUEBLA DE ZARAGOZA, PUEBLA.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'PUEBLA, HEROICA PUEBLA DE ZARAGOZA, PUEBLA.', Status = 'A' Where Clave = '75'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '80' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '80', 'COLOMBIA, COLOMBIA, NUEVO LEON.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'COLOMBIA, COLOMBIA, NUEVO LEON.', Status = 'A' Where Clave = '80'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '81' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '81', 'ALTAMIRA, ALTAMIRA, TAMAULIPAS.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'ALTAMIRA, ALTAMIRA, TAMAULIPAS.', Status = 'A' Where Clave = '81'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '82' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '82', 'CIUDAD CAMARGO, CIUDAD CAMARGO, TAMAULIPAS.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'CIUDAD CAMARGO, CIUDAD CAMARGO, TAMAULIPAS.', Status = 'A' Where Clave = '82'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '83' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '83', 'DOS BOCAS, PARAISO, TABASCO.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'DOS BOCAS, PARAISO, TABASCO.', Status = 'A' Where Clave = '83'  
If Not Exists ( Select * From CFDI_Aduanas Where Clave = '84' )  Insert Into CFDI_Aduanas (  Clave, Descripcion, Status )  Values ( '84', 'GUANAJUATO, SILAO, GUANAJUATO.', 'A' ) 
 Else Update CFDI_Aduanas Set Descripcion = 'GUANAJUATO, SILAO, GUANAJUATO.', Status = 'A' Where Clave = '84'  
Go--#SQL 

