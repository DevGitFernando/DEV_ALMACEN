-------------------------------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_Paises_02_CodigosPostales' and xType = 'U' ) 
   Drop Table FACT_CFDI_Paises_02_CodigosPostales
Go--#SQL


If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFDI_Paises_01_Estados' and xType = 'U' ) 
   Drop Table CFDI_Paises_01_Estados 
Go--#SQL 



-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_Paises' and xType = 'U' ) 
	Drop Table FACT_CFDI_Paises  
Go--#SQL  

Create Table FACT_CFDI_Paises 
(	
	Clave varchar(4) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '', -- Unique, 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table FACT_CFDI_Paises Add Constraint PK_FACT_CFDI_Paises Primary	Key ( Clave ) 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFDI_Paises_01_Estados' and xType = 'U' ) 
   Drop Table CFDI_Paises_01_Estados 
Go--#SQL 

Create Table CFDI_Paises_01_Estados 
( 
	ClavePais varchar(4) Not Null,  
	Clave varchar(5) Not Null, 
	Descripcion varchar(500) Not Null Default '',  -- Unique 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_Paises_01_Estados Add Primary Key ( ClavePais, Clave )
Go--#SQL 

Alter Table CFDI_Paises_01_Estados Add Constraint FK_CFDI_Paises_01_Estados___FACT_CFDI_Paises
	Foreign Key ( ClavePais ) References FACT_CFDI_Paises ( Clave ) 
Go--#SQL  	



---------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_Paises_02_CodigosPostales' and xType = 'U' ) 
   Drop Table FACT_CFDI_Paises_02_CodigosPostales
Go--#SQL 

Create Table FACT_CFDI_Paises_02_CodigosPostales 
( 
	ClavePais varchar(4) Not Null,  
	ClaveEstado varchar(5) Not Null, 
	ClaveCodigoPostal varchar(10) Not Null, 
	Municipio varchar(10) Not Null Default '', 
	Localidad varchar(10) Not Null Default '', 
	Status varchar(1) Not Null Default '', 
) 
Go--#SQL 

Alter Table FACT_CFDI_Paises_02_CodigosPostales Add Primary Key ( ClavePais, ClaveEstado, ClaveCodigoPostal )
Go--#SQL 

Alter Table FACT_CFDI_Paises_02_CodigosPostales Add Constraint FK_FACT_CFDI_Paises_02_CodigosPostales___CFDI_Paises_01_Estados
	Foreign Key ( ClavePais, ClaveEstado ) References CFDI_Paises_01_Estados ( ClavePais, Clave ) 
Go--#SQL  



---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ABW' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ABW', 'Aruba', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Aruba', Status = 'A' Where Clave = 'ABW'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'AFG' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'AFG', 'Afganistán', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Afganistán', Status = 'A' Where Clave = 'AFG'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'AGO' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'AGO', 'Angola', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Angola', Status = 'A' Where Clave = 'AGO'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'AIA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'AIA', 'Anguila', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Anguila', Status = 'A' Where Clave = 'AIA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ALA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ALA', 'Islas Åland', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Åland', Status = 'A' Where Clave = 'ALA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ALB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ALB', 'Albania', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Albania', Status = 'A' Where Clave = 'ALB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'AND' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'AND', 'Andorra', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Andorra', Status = 'A' Where Clave = 'AND'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ARE' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ARE', 'Emiratos Árabes Unidos (Los)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Emiratos Árabes Unidos (Los)', Status = 'A' Where Clave = 'ARE'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ARG' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ARG', 'Argentina', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Argentina', Status = 'A' Where Clave = 'ARG'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ARM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ARM', 'Armenia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Armenia', Status = 'A' Where Clave = 'ARM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ASM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ASM', 'Samoa Americana', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Samoa Americana', Status = 'A' Where Clave = 'ASM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ATA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ATA', 'Antártida', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Antártida', Status = 'A' Where Clave = 'ATA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ATF' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ATF', 'Territorios Australes Franceses (los)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Territorios Australes Franceses (los)', Status = 'A' Where Clave = 'ATF'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ATG' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ATG', 'Antigua y Barbuda', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Antigua y Barbuda', Status = 'A' Where Clave = 'ATG'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'AUS' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'AUS', 'Australia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Australia', Status = 'A' Where Clave = 'AUS'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'AUT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'AUT', 'Austria', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Austria', Status = 'A' Where Clave = 'AUT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'AZE' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'AZE', 'Azerbaiyán', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Azerbaiyán', Status = 'A' Where Clave = 'AZE'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BDI' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BDI', 'Burundi', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Burundi', Status = 'A' Where Clave = 'BDI'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BEL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BEL', 'Bélgica', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Bélgica', Status = 'A' Where Clave = 'BEL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BEN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BEN', 'Benín', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Benín', Status = 'A' Where Clave = 'BEN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BES' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BES', 'Bonaire, San Eustaquio y Saba', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Bonaire, San Eustaquio y Saba', Status = 'A' Where Clave = 'BES'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BFA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BFA', 'Burkina Faso', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Burkina Faso', Status = 'A' Where Clave = 'BFA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BGD' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BGD', 'Bangladés', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Bangladés', Status = 'A' Where Clave = 'BGD'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BGR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BGR', 'Bulgaria', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Bulgaria', Status = 'A' Where Clave = 'BGR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BHR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BHR', 'Baréin', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Baréin', Status = 'A' Where Clave = 'BHR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BHS' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BHS', 'Bahamas (las)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Bahamas (las)', Status = 'A' Where Clave = 'BHS'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BIH' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BIH', 'Bosnia y Herzegovina', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Bosnia y Herzegovina', Status = 'A' Where Clave = 'BIH'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BLM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BLM', 'San Bartolomé', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'San Bartolomé', Status = 'A' Where Clave = 'BLM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BLR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BLR', 'Bielorrusia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Bielorrusia', Status = 'A' Where Clave = 'BLR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BLZ' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BLZ', 'Belice', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Belice', Status = 'A' Where Clave = 'BLZ'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BMU' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BMU', 'Bermudas', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Bermudas', Status = 'A' Where Clave = 'BMU'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BOL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BOL', 'Bolivia, Estado Plurinacional de', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Bolivia, Estado Plurinacional de', Status = 'A' Where Clave = 'BOL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BRA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BRA', 'Brasil', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Brasil', Status = 'A' Where Clave = 'BRA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BRB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BRB', 'Barbados', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Barbados', Status = 'A' Where Clave = 'BRB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BRN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BRN', 'Brunéi Darussalam', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Brunéi Darussalam', Status = 'A' Where Clave = 'BRN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BTN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BTN', 'Bután', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Bután', Status = 'A' Where Clave = 'BTN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BVT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BVT', 'Isla Bouvet', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Isla Bouvet', Status = 'A' Where Clave = 'BVT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'BWA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'BWA', 'Botsuana', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Botsuana', Status = 'A' Where Clave = 'BWA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CAF' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CAF', 'República Centroafricana (la)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'República Centroafricana (la)', Status = 'A' Where Clave = 'CAF'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CAN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CAN', 'Canadá', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Canadá', Status = 'A' Where Clave = 'CAN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CCK' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CCK', 'Islas Cocos (Keeling)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Cocos (Keeling)', Status = 'A' Where Clave = 'CCK'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CHE' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CHE', 'Suiza', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Suiza', Status = 'A' Where Clave = 'CHE'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CHL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CHL', 'Chile', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Chile', Status = 'A' Where Clave = 'CHL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CHN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CHN', 'China', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'China', Status = 'A' Where Clave = 'CHN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CIV' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CIV', 'Côte d´Ivoire', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Côte d´Ivoire', Status = 'A' Where Clave = 'CIV'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CMR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CMR', 'Camerún', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Camerún', Status = 'A' Where Clave = 'CMR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'COD' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'COD', 'Congo (la República Democrática del)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Congo (la República Democrática del)', Status = 'A' Where Clave = 'COD'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'COG' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'COG', 'Congo', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Congo', Status = 'A' Where Clave = 'COG'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'COK' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'COK', 'Islas Cook (las)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Cook (las)', Status = 'A' Where Clave = 'COK'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'COL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'COL', 'Colombia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Colombia', Status = 'A' Where Clave = 'COL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'COM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'COM', 'Comoras', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Comoras', Status = 'A' Where Clave = 'COM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CPV' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CPV', 'Cabo Verde', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Cabo Verde', Status = 'A' Where Clave = 'CPV'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CRI' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CRI', 'Costa Rica', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Costa Rica', Status = 'A' Where Clave = 'CRI'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CUB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CUB', 'Cuba', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Cuba', Status = 'A' Where Clave = 'CUB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CUW' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CUW', 'Curaçao', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Curaçao', Status = 'A' Where Clave = 'CUW'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CXR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CXR', 'Isla de Navidad', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Isla de Navidad', Status = 'A' Where Clave = 'CXR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CYM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CYM', 'Islas Caimán (las)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Caimán (las)', Status = 'A' Where Clave = 'CYM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CYP' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CYP', 'Chipre', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Chipre', Status = 'A' Where Clave = 'CYP'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'CZE' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'CZE', 'República Checa (la)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'República Checa (la)', Status = 'A' Where Clave = 'CZE'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'DEU' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'DEU', 'Alemania', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Alemania', Status = 'A' Where Clave = 'DEU'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'DJI' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'DJI', 'Yibuti', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Yibuti', Status = 'A' Where Clave = 'DJI'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'DMA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'DMA', 'Dominica', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Dominica', Status = 'A' Where Clave = 'DMA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'DNK' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'DNK', 'Dinamarca', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Dinamarca', Status = 'A' Where Clave = 'DNK'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'DOM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'DOM', 'República Dominicana (la)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'República Dominicana (la)', Status = 'A' Where Clave = 'DOM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'DZA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'DZA', 'Argelia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Argelia', Status = 'A' Where Clave = 'DZA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ECU' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ECU', 'Ecuador', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Ecuador', Status = 'A' Where Clave = 'ECU'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'EGY' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'EGY', 'Egipto', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Egipto', Status = 'A' Where Clave = 'EGY'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ERI' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ERI', 'Eritrea', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Eritrea', Status = 'A' Where Clave = 'ERI'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ESH' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ESH', 'Sahara Occidental', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Sahara Occidental', Status = 'A' Where Clave = 'ESH'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ESP' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ESP', 'España', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'España', Status = 'A' Where Clave = 'ESP'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'EST' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'EST', 'Estonia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Estonia', Status = 'A' Where Clave = 'EST'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ETH' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ETH', 'Etiopía', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Etiopía', Status = 'A' Where Clave = 'ETH'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'FIN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'FIN', 'Finlandia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Finlandia', Status = 'A' Where Clave = 'FIN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'FJI' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'FJI', 'Fiyi', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Fiyi', Status = 'A' Where Clave = 'FJI'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'FLK' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'FLK', 'Islas Malvinas [Falkland] (las)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Malvinas [Falkland] (las)', Status = 'A' Where Clave = 'FLK'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'FRA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'FRA', 'Francia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Francia', Status = 'A' Where Clave = 'FRA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'FRO' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'FRO', 'Islas Feroe (las)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Feroe (las)', Status = 'A' Where Clave = 'FRO'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'FSM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'FSM', 'Micronesia (los Estados Federados de)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Micronesia (los Estados Federados de)', Status = 'A' Where Clave = 'FSM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GAB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GAB', 'Gabón', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Gabón', Status = 'A' Where Clave = 'GAB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GBR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GBR', 'Reino Unido (el)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Reino Unido (el)', Status = 'A' Where Clave = 'GBR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GEO' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GEO', 'Georgia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Georgia', Status = 'A' Where Clave = 'GEO'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GGY' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GGY', 'Guernsey', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Guernsey', Status = 'A' Where Clave = 'GGY'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GHA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GHA', 'Ghana', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Ghana', Status = 'A' Where Clave = 'GHA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GIB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GIB', 'Gibraltar', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Gibraltar', Status = 'A' Where Clave = 'GIB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GIN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GIN', 'Guinea', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Guinea', Status = 'A' Where Clave = 'GIN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GLP' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GLP', 'Guadalupe', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Guadalupe', Status = 'A' Where Clave = 'GLP'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GMB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GMB', 'Gambia (La)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Gambia (La)', Status = 'A' Where Clave = 'GMB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GNB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GNB', 'Guinea-Bisáu', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Guinea-Bisáu', Status = 'A' Where Clave = 'GNB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GNQ' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GNQ', 'Guinea Ecuatorial', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Guinea Ecuatorial', Status = 'A' Where Clave = 'GNQ'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GRC' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GRC', 'Grecia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Grecia', Status = 'A' Where Clave = 'GRC'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GRD' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GRD', 'Granada', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Granada', Status = 'A' Where Clave = 'GRD'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GRL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GRL', 'Groenlandia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Groenlandia', Status = 'A' Where Clave = 'GRL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GTM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GTM', 'Guatemala', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Guatemala', Status = 'A' Where Clave = 'GTM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GUF' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GUF', 'Guayana Francesa', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Guayana Francesa', Status = 'A' Where Clave = 'GUF'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GUM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GUM', 'Guam', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Guam', Status = 'A' Where Clave = 'GUM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'GUY' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'GUY', 'Guyana', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Guyana', Status = 'A' Where Clave = 'GUY'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'HKG' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'HKG', 'Hong Kong', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Hong Kong', Status = 'A' Where Clave = 'HKG'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'HMD' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'HMD', 'Isla Heard e Islas McDonald', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Isla Heard e Islas McDonald', Status = 'A' Where Clave = 'HMD'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'HND' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'HND', 'Honduras', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Honduras', Status = 'A' Where Clave = 'HND'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'HRV' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'HRV', 'Croacia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Croacia', Status = 'A' Where Clave = 'HRV'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'HTI' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'HTI', 'Haití', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Haití', Status = 'A' Where Clave = 'HTI'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'HUN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'HUN', 'Hungría', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Hungría', Status = 'A' Where Clave = 'HUN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'IDN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'IDN', 'Indonesia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Indonesia', Status = 'A' Where Clave = 'IDN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'IMN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'IMN', 'Isla de Man', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Isla de Man', Status = 'A' Where Clave = 'IMN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'IND' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'IND', 'India', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'India', Status = 'A' Where Clave = 'IND'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'IOT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'IOT', 'Territorio Británico del Océano Índico (el)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Territorio Británico del Océano Índico (el)', Status = 'A' Where Clave = 'IOT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'IRL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'IRL', 'Irlanda', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Irlanda', Status = 'A' Where Clave = 'IRL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'IRN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'IRN', 'Irán (la República Islámica de)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Irán (la República Islámica de)', Status = 'A' Where Clave = 'IRN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'IRQ' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'IRQ', 'Irak', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Irak', Status = 'A' Where Clave = 'IRQ'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ISL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ISL', 'Islandia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islandia', Status = 'A' Where Clave = 'ISL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ISR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ISR', 'Israel', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Israel', Status = 'A' Where Clave = 'ISR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ITA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ITA', 'Italia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Italia', Status = 'A' Where Clave = 'ITA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'JAM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'JAM', 'Jamaica', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Jamaica', Status = 'A' Where Clave = 'JAM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'JEY' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'JEY', 'Jersey', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Jersey', Status = 'A' Where Clave = 'JEY'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'JOR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'JOR', 'Jordania', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Jordania', Status = 'A' Where Clave = 'JOR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'JPN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'JPN', 'Japón', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Japón', Status = 'A' Where Clave = 'JPN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'KAZ' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'KAZ', 'Kazajistán', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Kazajistán', Status = 'A' Where Clave = 'KAZ'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'KEN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'KEN', 'Kenia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Kenia', Status = 'A' Where Clave = 'KEN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'KGZ' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'KGZ', 'Kirguistán', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Kirguistán', Status = 'A' Where Clave = 'KGZ'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'KHM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'KHM', 'Camboya', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Camboya', Status = 'A' Where Clave = 'KHM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'KIR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'KIR', 'Kiribati', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Kiribati', Status = 'A' Where Clave = 'KIR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'KNA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'KNA', 'San Cristóbal y Nieves', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'San Cristóbal y Nieves', Status = 'A' Where Clave = 'KNA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'KOR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'KOR', 'Corea (la República de)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Corea (la República de)', Status = 'A' Where Clave = 'KOR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'KWT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'KWT', 'Kuwait', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Kuwait', Status = 'A' Where Clave = 'KWT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'LAO' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'LAO', 'Lao, (la) República Democrática Popular', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Lao, (la) República Democrática Popular', Status = 'A' Where Clave = 'LAO'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'LBN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'LBN', 'Líbano', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Líbano', Status = 'A' Where Clave = 'LBN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'LBR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'LBR', 'Liberia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Liberia', Status = 'A' Where Clave = 'LBR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'LBY' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'LBY', 'Libia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Libia', Status = 'A' Where Clave = 'LBY'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'LCA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'LCA', 'Santa Lucía', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Santa Lucía', Status = 'A' Where Clave = 'LCA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'LIE' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'LIE', 'Liechtenstein', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Liechtenstein', Status = 'A' Where Clave = 'LIE'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'LKA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'LKA', 'Sri Lanka', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Sri Lanka', Status = 'A' Where Clave = 'LKA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'LSO' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'LSO', 'Lesoto', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Lesoto', Status = 'A' Where Clave = 'LSO'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'LTU' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'LTU', 'Lituania', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Lituania', Status = 'A' Where Clave = 'LTU'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'LUX' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'LUX', 'Luxemburgo', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Luxemburgo', Status = 'A' Where Clave = 'LUX'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'LVA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'LVA', 'Letonia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Letonia', Status = 'A' Where Clave = 'LVA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MAC' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MAC', 'Macao', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Macao', Status = 'A' Where Clave = 'MAC'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MAF' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MAF', 'San Martín (parte francesa)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'San Martín (parte francesa)', Status = 'A' Where Clave = 'MAF'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MAR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MAR', 'Marruecos', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Marruecos', Status = 'A' Where Clave = 'MAR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MCO' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MCO', 'Mónaco', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Mónaco', Status = 'A' Where Clave = 'MCO'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MDA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MDA', 'Moldavia (la República de)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Moldavia (la República de)', Status = 'A' Where Clave = 'MDA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MDG' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MDG', 'Madagascar', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Madagascar', Status = 'A' Where Clave = 'MDG'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MDV' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MDV', 'Maldivas', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Maldivas', Status = 'A' Where Clave = 'MDV'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MEX' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MEX', 'México', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'México', Status = 'A' Where Clave = 'MEX'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MHL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MHL', 'Islas Marshall (las)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Marshall (las)', Status = 'A' Where Clave = 'MHL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MKD' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MKD', 'Macedonia (la antigua República Yugoslava de)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Macedonia (la antigua República Yugoslava de)', Status = 'A' Where Clave = 'MKD'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MLI' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MLI', 'Malí', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Malí', Status = 'A' Where Clave = 'MLI'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MLT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MLT', 'Malta', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Malta', Status = 'A' Where Clave = 'MLT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MMR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MMR', 'Myanmar', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Myanmar', Status = 'A' Where Clave = 'MMR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MNE' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MNE', 'Montenegro', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Montenegro', Status = 'A' Where Clave = 'MNE'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MNG' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MNG', 'Mongolia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Mongolia', Status = 'A' Where Clave = 'MNG'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MNP' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MNP', 'Islas Marianas del Norte (las)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Marianas del Norte (las)', Status = 'A' Where Clave = 'MNP'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MOZ' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MOZ', 'Mozambique', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Mozambique', Status = 'A' Where Clave = 'MOZ'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MRT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MRT', 'Mauritania', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Mauritania', Status = 'A' Where Clave = 'MRT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MSR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MSR', 'Montserrat', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Montserrat', Status = 'A' Where Clave = 'MSR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MTQ' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MTQ', 'Martinica', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Martinica', Status = 'A' Where Clave = 'MTQ'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MUS' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MUS', 'Mauricio', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Mauricio', Status = 'A' Where Clave = 'MUS'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MWI' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MWI', 'Malaui', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Malaui', Status = 'A' Where Clave = 'MWI'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MYS' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MYS', 'Malasia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Malasia', Status = 'A' Where Clave = 'MYS'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'MYT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'MYT', 'Mayotte', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Mayotte', Status = 'A' Where Clave = 'MYT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NAM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NAM', 'Namibia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Namibia', Status = 'A' Where Clave = 'NAM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NCL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NCL', 'Nueva Caledonia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Nueva Caledonia', Status = 'A' Where Clave = 'NCL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NER' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NER', 'Níger (el)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Níger (el)', Status = 'A' Where Clave = 'NER'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NFK' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NFK', 'Isla Norfolk', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Isla Norfolk', Status = 'A' Where Clave = 'NFK'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NGA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NGA', 'Nigeria', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Nigeria', Status = 'A' Where Clave = 'NGA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NIC' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NIC', 'Nicaragua', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Nicaragua', Status = 'A' Where Clave = 'NIC'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NIU' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NIU', 'Niue', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Niue', Status = 'A' Where Clave = 'NIU'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NLD' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NLD', 'Países Bajos (los)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Países Bajos (los)', Status = 'A' Where Clave = 'NLD'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NOR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NOR', 'Noruega', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Noruega', Status = 'A' Where Clave = 'NOR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NPL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NPL', 'Nepal', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Nepal', Status = 'A' Where Clave = 'NPL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NRU' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NRU', 'Nauru', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Nauru', Status = 'A' Where Clave = 'NRU'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'NZL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'NZL', 'Nueva Zelanda', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Nueva Zelanda', Status = 'A' Where Clave = 'NZL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'OMN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'OMN', 'Omán', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Omán', Status = 'A' Where Clave = 'OMN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PAK' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PAK', 'Pakistán', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Pakistán', Status = 'A' Where Clave = 'PAK'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PAN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PAN', 'Panamá', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Panamá', Status = 'A' Where Clave = 'PAN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PCN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PCN', 'Pitcairn', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Pitcairn', Status = 'A' Where Clave = 'PCN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PER' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PER', 'Perú', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Perú', Status = 'A' Where Clave = 'PER'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PHL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PHL', 'Filipinas (las)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Filipinas (las)', Status = 'A' Where Clave = 'PHL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PLW' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PLW', 'Palaos', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Palaos', Status = 'A' Where Clave = 'PLW'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PNG' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PNG', 'Papúa Nueva Guinea', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Papúa Nueva Guinea', Status = 'A' Where Clave = 'PNG'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'POL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'POL', 'Polonia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Polonia', Status = 'A' Where Clave = 'POL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PRI' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PRI', 'Puerto Rico', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Puerto Rico', Status = 'A' Where Clave = 'PRI'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PRK' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PRK', 'Corea (la República Democrática Popular de)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Corea (la República Democrática Popular de)', Status = 'A' Where Clave = 'PRK'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PRT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PRT', 'Portugal', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Portugal', Status = 'A' Where Clave = 'PRT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PRY' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PRY', 'Paraguay', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Paraguay', Status = 'A' Where Clave = 'PRY'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PSE' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PSE', 'Palestina, Estado de', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Palestina, Estado de', Status = 'A' Where Clave = 'PSE'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'PYF' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'PYF', 'Polinesia Francesa', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Polinesia Francesa', Status = 'A' Where Clave = 'PYF'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'QAT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'QAT', 'Catar', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Catar', Status = 'A' Where Clave = 'QAT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'REU' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'REU', 'Reunión', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Reunión', Status = 'A' Where Clave = 'REU'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ROU' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ROU', 'Rumania', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Rumania', Status = 'A' Where Clave = 'ROU'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'RUS' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'RUS', 'Rusia, (la) Federación de', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Rusia, (la) Federación de', Status = 'A' Where Clave = 'RUS'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'RWA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'RWA', 'Ruanda', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Ruanda', Status = 'A' Where Clave = 'RWA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SAU' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SAU', 'Arabia Saudita', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Arabia Saudita', Status = 'A' Where Clave = 'SAU'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SDN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SDN', 'Sudán (el)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Sudán (el)', Status = 'A' Where Clave = 'SDN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SEN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SEN', 'Senegal', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Senegal', Status = 'A' Where Clave = 'SEN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SGP' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SGP', 'Singapur', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Singapur', Status = 'A' Where Clave = 'SGP'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SGS' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SGS', 'Georgia del sur y las islas sandwich del sur', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Georgia del sur y las islas sandwich del sur', Status = 'A' Where Clave = 'SGS'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SHN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SHN', 'Santa Helena, Ascensión y Tristán de Acuña', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Santa Helena, Ascensión y Tristán de Acuña', Status = 'A' Where Clave = 'SHN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SJM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SJM', 'Svalbard y Jan Mayen', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Svalbard y Jan Mayen', Status = 'A' Where Clave = 'SJM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SLB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SLB', 'Islas Salomón (las)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Salomón (las)', Status = 'A' Where Clave = 'SLB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SLE' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SLE', 'Sierra leona', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Sierra leona', Status = 'A' Where Clave = 'SLE'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SLV' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SLV', 'El Salvador', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'El Salvador', Status = 'A' Where Clave = 'SLV'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SMR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SMR', 'San Marino', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'San Marino', Status = 'A' Where Clave = 'SMR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SOM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SOM', 'Somalia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Somalia', Status = 'A' Where Clave = 'SOM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SPM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SPM', 'San Pedro y Miquelón', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'San Pedro y Miquelón', Status = 'A' Where Clave = 'SPM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SRB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SRB', 'Serbia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Serbia', Status = 'A' Where Clave = 'SRB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SSD' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SSD', 'Sudán del Sur', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Sudán del Sur', Status = 'A' Where Clave = 'SSD'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'STP' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'STP', 'Santo Tomé y Príncipe', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Santo Tomé y Príncipe', Status = 'A' Where Clave = 'STP'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SUR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SUR', 'Surinam', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Surinam', Status = 'A' Where Clave = 'SUR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SVK' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SVK', 'Eslovaquia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Eslovaquia', Status = 'A' Where Clave = 'SVK'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SVN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SVN', 'Eslovenia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Eslovenia', Status = 'A' Where Clave = 'SVN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SWE' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SWE', 'Suecia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Suecia', Status = 'A' Where Clave = 'SWE'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SWZ' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SWZ', 'Suazilandia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Suazilandia', Status = 'A' Where Clave = 'SWZ'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SXM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SXM', 'Sint Maarten (parte holandesa)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Sint Maarten (parte holandesa)', Status = 'A' Where Clave = 'SXM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SYC' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SYC', 'Seychelles', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Seychelles', Status = 'A' Where Clave = 'SYC'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'SYR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'SYR', 'Siria, (la) República Árabe', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Siria, (la) República Árabe', Status = 'A' Where Clave = 'SYR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TCA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TCA', 'Islas Turcas y Caicos (las)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Turcas y Caicos (las)', Status = 'A' Where Clave = 'TCA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TCD' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TCD', 'Chad', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Chad', Status = 'A' Where Clave = 'TCD'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TGO' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TGO', 'Togo', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Togo', Status = 'A' Where Clave = 'TGO'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'THA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'THA', 'Tailandia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Tailandia', Status = 'A' Where Clave = 'THA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TJK' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TJK', 'Tayikistán', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Tayikistán', Status = 'A' Where Clave = 'TJK'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TKL' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TKL', 'Tokelau', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Tokelau', Status = 'A' Where Clave = 'TKL'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TKM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TKM', 'Turkmenistán', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Turkmenistán', Status = 'A' Where Clave = 'TKM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TLS' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TLS', 'Timor-Leste', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Timor-Leste', Status = 'A' Where Clave = 'TLS'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TON' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TON', 'Tonga', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Tonga', Status = 'A' Where Clave = 'TON'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TTO' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TTO', 'Trinidad y Tobago', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Trinidad y Tobago', Status = 'A' Where Clave = 'TTO'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TUN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TUN', 'Túnez', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Túnez', Status = 'A' Where Clave = 'TUN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TUR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TUR', 'Turquía', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Turquía', Status = 'A' Where Clave = 'TUR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TUV' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TUV', 'Tuvalu', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Tuvalu', Status = 'A' Where Clave = 'TUV'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TWN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TWN', 'Taiwán (Provincia de China)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Taiwán (Provincia de China)', Status = 'A' Where Clave = 'TWN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'TZA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'TZA', 'Tanzania, República Unida de', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Tanzania, República Unida de', Status = 'A' Where Clave = 'TZA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'UGA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'UGA', 'Uganda', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Uganda', Status = 'A' Where Clave = 'UGA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'UKR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'UKR', 'Ucrania', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Ucrania', Status = 'A' Where Clave = 'UKR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'UMI' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'UMI', 'Islas de Ultramar Menores de Estados Unidos (las)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas de Ultramar Menores de Estados Unidos (las)', Status = 'A' Where Clave = 'UMI'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'URY' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'URY', 'Uruguay', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Uruguay', Status = 'A' Where Clave = 'URY'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'USA' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'USA', 'Estados Unidos (los)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Estados Unidos (los)', Status = 'A' Where Clave = 'USA'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'UZB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'UZB', 'Uzbekistán', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Uzbekistán', Status = 'A' Where Clave = 'UZB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'VAT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'VAT', 'Santa Sede[Estado de la Ciudad del Vaticano] (la)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Santa Sede[Estado de la Ciudad del Vaticano] (la)', Status = 'A' Where Clave = 'VAT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'VCT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'VCT', 'San Vicente y las Granadinas', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'San Vicente y las Granadinas', Status = 'A' Where Clave = 'VCT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'VEN' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'VEN', 'Venezuela, República Bolivariana de', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Venezuela, República Bolivariana de', Status = 'A' Where Clave = 'VEN'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'VGB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'VGB', 'Islas Vírgenes (Británicas)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Vírgenes (Británicas)', Status = 'A' Where Clave = 'VGB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'VIR' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'VIR', 'Islas Vírgenes (EE.UU.)', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Islas Vírgenes (EE.UU.)', Status = 'A' Where Clave = 'VIR'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'VNM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'VNM', 'Viet Nam', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Viet Nam', Status = 'A' Where Clave = 'VNM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'VUT' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'VUT', 'Vanuatu', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Vanuatu', Status = 'A' Where Clave = 'VUT'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'WLF' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'WLF', 'Wallis y Futuna', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Wallis y Futuna', Status = 'A' Where Clave = 'WLF'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'WSM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'WSM', 'Samoa', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Samoa', Status = 'A' Where Clave = 'WSM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'YEM' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'YEM', 'Yemen', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Yemen', Status = 'A' Where Clave = 'YEM'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ZAF' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ZAF', 'Sudáfrica', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Sudáfrica', Status = 'A' Where Clave = 'ZAF'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ZMB' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ZMB', 'Zambia', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Zambia', Status = 'A' Where Clave = 'ZMB'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ZWE' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ZWE', 'Zimbabue', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Zimbabue', Status = 'A' Where Clave = 'ZWE'  
If Not Exists ( Select * From FACT_CFDI_Paises Where Clave = 'ZZZ' )  Insert Into FACT_CFDI_Paises (  Clave, Descripcion, Status )  Values ( 'ZZZ', 'Países no declarados', 'A' ) 
 Else Update FACT_CFDI_Paises Set Descripcion = 'Países no declarados', Status = 'A' Where Clave = 'ZZZ'  
Go--#SQL 


If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'AGU' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'AGU', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'AGU'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'BCN' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'BCN', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'BCN'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'BCS' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'BCS', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'BCS'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'CAM' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'CAM', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'CAM'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'CHH' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'CHH', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'CHH'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'CHP' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'CHP', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'CHP'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'COA' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'COA', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'COA'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'COL' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'COL', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'COL'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'DIF' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'DIF', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'DIF'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'DUR' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'DUR', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'DUR'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'GRO' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'GRO', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'GRO'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'GUA' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'GUA', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'GUA'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'HID' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'HID', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'HID'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'JAL' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'JAL', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'JAL'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'MEX' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'MEX', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'MEX'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'MIC' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'MIC', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'MIC'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'MOR' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'MOR', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'MOR'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'NAY' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'NAY', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'NAY'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'NLE' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'NLE', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'NLE'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'OAX' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'OAX', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'OAX'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'PUE' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'PUE', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'PUE'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'QUE' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'QUE', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'QUE'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'ROO' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'ROO', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'ROO'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'SIN' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'SIN', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'SIN'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'SLP' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'SLP', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'SLP'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'SON' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'SON', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'SON'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'TAB' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'TAB', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'TAB'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'TAM' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'TAM', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'TAM'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'TLA' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'TLA', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'TLA'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'VER' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'VER', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'VER'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'YUC' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'YUC', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'YUC'  
If Not Exists ( Select * From CFDI_Paises_01_Estados Where ClavePais = 'MEX' and Clave = 'ZAC' )  Insert Into CFDI_Paises_01_Estados (  ClavePais, Clave, Descripcion, Status )  Values ( 'MEX', 'ZAC', 'A', 'A' ) 
 Else Update CFDI_Paises_01_Estados Set Descripcion = 'A', Status = 'A' Where ClavePais = 'MEX' and Clave = 'ZAC'  

Go--#SQL 


