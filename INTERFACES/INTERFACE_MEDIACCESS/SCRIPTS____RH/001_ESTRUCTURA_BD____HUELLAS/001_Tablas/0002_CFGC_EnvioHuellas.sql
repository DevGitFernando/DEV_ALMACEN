


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_EnvioHuellas' and xType = 'U' ) 
   Drop Table CFGC_EnvioHuellas  
Go--#SQL   

Create Table CFGC_EnvioHuellas 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGC_EnvioHuellas Add Constraint PK_CFGC_EnvioHuellas Primary Key ( NombreTabla ) 
Go--#SQL   

	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatEstados')
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatMunicipios') 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatColonias') 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatFarmacias') 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatPuestos') 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatDepartamentos') 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatEscolaridades')		 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatTipoDocumentos')
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatGruposSanguineos')		 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatEmpresas') 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatEmpresasEstados')	 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('CatPersonal')
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('FP_Manos') 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('FP_Dedos') 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('FP_Manos_Dedos') 
	Insert Into CFGC_EnvioHuellas ( NombreTabla ) values ('FP_Huellas')	  
 
 Update CFGC_EnvioHuellas Set IdOrden = IdEnvio + 100 
Go--#SQL  