--------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFGSC_EnvioFuentesFinanciamiento' and xType = 'U' ) 
   Drop Table CFGSC_EnvioFuentesFinanciamiento  
Go--#SQL   

Create Table CFGSC_EnvioFuentesFinanciamiento 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGSC_EnvioFuentesFinanciamiento Add Constraint PK_CFGSC_EnvioFuentesFinanciamiento Primary Key ( NombreTabla ) 
Go--#SQL   


	----- Producto 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento' ) 


	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Detalles' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia' ) 

	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias_Remisiones' ) 

	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Beneficiarios' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Detalles_Documentos' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Detalles_Referencias' ) 


	----- Servicio  
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Admon' ) 

	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Admon_Detalles' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario' ) 
	--Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( '' ) 


	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Remisiones__RelacionFacturas' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Remisiones__RelacionFacturas_x_Farmacia' ) 
	Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( 'FACT_Remisiones__RelacionFacturas_x_Importes' ) 
	--Insert Into CFGSC_EnvioFuentesFinanciamiento ( NombreTabla ) values ( '' ) 



 Update CFGSC_EnvioFuentesFinanciamiento Set IdOrden = IdEnvio + 100 
Go--#SQL  

