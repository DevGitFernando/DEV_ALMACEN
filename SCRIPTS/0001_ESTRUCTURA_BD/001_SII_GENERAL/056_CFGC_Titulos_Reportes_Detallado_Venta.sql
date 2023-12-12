-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_Titulos_Reportes_Detallado_Venta' and xType = 'U' ) 
   Drop Table CFGC_Titulos_Reportes_Detallado_Venta 
Go--#SQL 

Create Table CFGC_Titulos_Reportes_Detallado_Venta  
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	
	Mostrar_Cliente bit Not Null Default 'True', 
	Mostrar_Solo_Etiqueta_Cliente bit Not Null Default 'false', 				
	Mostrar_SubCliente bit Not Null Default 'True',
	Mostrar_Solo_Etiqueta_SubCliente bit Not Null Default 'false', 				 
	Mostrar_SubCliente_Como_Cliente bit Not Null Default 'false', 	
	Mostrar_Descripcion_Perfil bit Not Null Default 'false', 		
	
	Mostrar_Programa bit Not Null Default 'True', 
	Mostrar_Solo_Etiqueta_Programa bit Not Null Default 'false', 				
	Mostrar_SubPrograma bit Not Null Default 'True', 	
	Mostrar_Solo_Etiqueta_SubPrograma bit Not Null Default 'false', 			
		
	Mostrar_Beneficiario bit Not Null Default 'True', 		
	Mostrar_Solo_Etiqueta_Beneficiario bit Not Null Default 'false', 				
	Mostrar_FolioReferencia bit Not Null Default 'True', 
	Mostrar_Solo_Etiqueta_FolioReferencia bit Not Null Default 'false', 				
	Mostrar_FolioDocumento bit Not Null Default 'True', 
	Mostrar_Solo_Etiqueta_FolioDocumento bit Not Null Default 'false', 			
	
	Mostrar_Presentacion_ContenidoPaquete bit Not Null Default 'false', 						
			
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table CFGC_Titulos_Reportes_Detallado_Venta 
	Add Constraint PK_CFGC_Titulos_Reportes_Detallado_Venta Primary Key ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL 

-----------------------------------------------------------------------------------------------------------------  
	Insert Into CFGC_Titulos_Reportes_Detallado_Venta ( IdEmpresa, IdEstado, IdFarmacia  ) 
	Select IdEmpresa, IdEstado, IdFarmacia 
	From FarmaciaProductos F (NoLock) 
	Where Not Exists 
		( 
			Select * From CFGC_Titulos_Reportes_Detallado_Venta C (NoLock)  
			Where F.IdEmpresa = C.IdEmpresa and F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia 
		) 
		and IdFarmacia = 1182 
	Group by IdEmpresa, IdEstado, IdFarmacia 

Go--#SQL 

