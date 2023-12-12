Set NoCount On
Go--#SQL   

---------------------------------------------- 
/* 
	Informacion enviada a Puntos de Venta 
*/ 
---------------------------------------------- 

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGSC_EnvioCatalogos' and xType = 'U' ) 
   Drop Table CFGSC_EnvioCatalogos 
Go--#SQL   

Create Table CFGSC_EnvioCatalogos 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL   

Alter Table CFGSC_EnvioCatalogos Add Constraint PK_CFGSC_EnvioCatalogos Primary Key ( NombreTabla ) 
Go--#SQL   

 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatEmpresas', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatEstados', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatMunicipios', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatColonias', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatJurisdicciones', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatEmpresasEstados', 'A' )  
 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatRegiones', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSubRegiones', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatFamilias', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSubFamilias', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSegmentosSubFamilias', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatUnidadesMedicas', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatTiposUnidades', 'A' )   

 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatFarmacias', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatEstados_SubFarmacias', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatFarmacias_SubFarmacias', 'A' )   
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatFarmacias_ConvenioVales', 'A' )   
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_Farmacias_ConvenioVales', 'A' )
 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatProveedores', 'A' )
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatDistribuidores', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatFarmacias_ProveedoresVales', 'A' )    
 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatPersonal', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatTipoCatalogoClaves', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatGruposTerapeuticos', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatTiposDeProducto', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatClavesSSA_Sales', 'A' ) 

 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatClasificacionesSSA', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatProgramas', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSubProgramas', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSubProgramas_Farmacias', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatLaboratorios', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatPresentaciones', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatProductos', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatProductos_CodigosRelacionados', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatProductos_Estado', 'C' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatTiposDeClientes', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatClientes', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSubClientes', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_ClavesSSA_ClavesRelacionadas', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_ClaveSSA_Mascara', 'A' )    
 

 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatCajeros', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatCajas', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'Movtos_Inv_Tipos', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'Movtos_Inv_Tipos_Farmacia', 'A' ) 

 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatBeneficios', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatEspecialidades', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatServicios', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatServicios_Areas', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatServicios_Areas_Farmacias', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatTiposDispensacion', 'A' )  
 
 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatCentrosDeSalud', 'A' ) 
 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatCompaniasTiempoAire', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatCompaniasTA_Montos', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatPersonalTA', 'A' ) 

 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_Clientes_Claves', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_Clientes_SubClientes_Claves', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_ClavesSSA_Precios', 'A' )  
 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_Claves_Excluir_NivelAbasto', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_CB_NivelesAtencion', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_CB_NivelesAtencion_Miembros', 'A' )   
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_CB_CuadroBasico_Claves', 'A' )    
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_CB_EmisionVales', 'A' )      
 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EX_Validacion_Titulos', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EX_Validacion_Titulos_Beneficiarios', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EX_Validacion_Titulos_Reportes', 'C' )  

 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EmpresasFarmacias', 'A' )   
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EstadosClientes', 'A' )   
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EstadosFarmaciasClientes', 'A' )      
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EstadosFarmaciasClientesSubClientes', 'A' )      
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EstadosFarmaciasProgramasSubProgramas', 'A' )       
 
 
----------- Tablas de sistema 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Arboles', 'A' )  
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Navegacion', 'A' )      
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Usuarios', 'A' )   
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Grupos_De_Usuarios', 'A' )   
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Grupos_Usuarios_Miembros', 'A' )     
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Privilegios_Grupo', 'A' )      

---- Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_CFGC_TipoCambio', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Operaciones_Supervisadas', 'A' ) 
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Permisos_Operaciones_Supervisadas', 'A' ) 

 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFGC_EnvioDetalles', 'A' )     
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFGC_EnvioDetallesTrans', 'A' )      
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFGS_ConfigurarConexiones', 'A' )      
 Insert Into CFGSC_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFGS_EnvioCatalogos', 'A' )      

----------- Tablas de Transferencias 
-- Insert Into CFGSC_EnvioCatalogos ( NombreTabla, IdOrden ) values ( 'TransferenciasEnvioEnc', 160 ) 
-- Insert Into CFGSC_EnvioCatalogos ( NombreTabla, IdOrden ) values ( 'TransferenciasEnvioDet', 161 ) 
-- Insert Into CFGSC_EnvioCatalogos ( NombreTabla, IdOrden ) values ( 'TransferenciasEnvioDet_Lotes', 162 ) 
-- Insert Into CFGSC_EnvioCatalogos ( NombreTabla, IdOrden ) values ( 'TransferenciasEnvioDet_LotesRegistrar', 163 )   

Update CFGSC_EnvioCatalogos Set IdOrden = IdEnvio + 100 
Go--#SQL  


---------------------------------------------- 
/* 
	Informacion enviada a Puntos de Venta
*/ 
---------------------------------------------- 
------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_EnvioCatalogos' and xType = 'U' ) 
   Drop Table CFGS_EnvioCatalogos 
Go--#SQL   

Create Table CFGS_EnvioCatalogos 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL   

Alter Table CFGS_EnvioCatalogos Add Constraint PK_CFGS_EnvioCatalogos Primary Key ( NombreTabla ) 
Go--#SQL   

 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatEmpresas', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatEstados', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatMunicipios', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatColonias', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatJurisdicciones', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatEmpresasEstados', 'A' )  
 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatRegiones', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSubRegiones', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatFamilias', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSubFamilias', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSegmentosSubFamilias', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatUnidadesMedicas', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatTiposUnidades', 'A' )   

 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatFarmacias', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatEstados_SubFarmacias', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatFarmacias_SubFarmacias', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatFarmacias_ConvenioVales', 'A' )    
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_Farmacias_ConvenioVales', 'A' )        

 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatPersonal', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatTipoCatalogoClaves', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatGruposTerapeuticos', 'A' )
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatTiposDeProducto', 'A' )   
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatClavesSSA_Sales', 'A' )

 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatClasificacionesSSA', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatProgramas', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSubProgramas', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSubProgramas_Farmacias', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatLaboratorios', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatPresentaciones', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatProductos', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatProductos_CodigosRelacionados', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatProductos_Estado', 'C' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatTiposDeClientes', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatClientes', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatSubClientes', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_ClavesSSA_ClavesRelacionadas', 'A' )    
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_ClaveSSA_Mascara', 'A' )    

 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatProveedores', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatFarmacias_ProveedoresVales', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatDistribuidores', 'A' )


 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatCajeros', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatCajas', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Movtos_Inv_Tipos', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Movtos_Inv_Tipos_Farmacia', 'A' ) 

 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatBeneficios', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatEspecialidades', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatServicios', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatServicios_Areas', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatServicios_Areas_Farmacias', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatTiposDispensacion', 'A' )   
 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatCentrosDeSalud', 'A' ) 
 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatCompaniasTiempoAire', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatCompaniasTA_Montos', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatPersonalTA', 'A' ) 

 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_Clientes_Claves', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_Clientes_SubClientes_Claves', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_ClavesSSA_Precios', 'A' )  

 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_Claves_Excluir_NivelAbasto', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_CB_NivelesAtencion', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_CB_NivelesAtencion_Miembros', 'A' )   
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_CB_CuadroBasico_Claves', 'A' )    
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_CB_EmisionVales', 'A' )     


 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EX_Validacion_Titulos', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EX_Validacion_Titulos_Beneficiarios', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EX_Validacion_Titulos_Reportes', 'C' )  

 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EmpresasFarmacias', 'A' )   
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EstadosClientes', 'A' )   
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EstadosFarmaciasClientes', 'A' )      
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EstadosFarmaciasClientesSubClientes', 'A' )      
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFG_EstadosFarmaciasProgramasSubProgramas', 'A' )       
 
 
----------- Tablas de sistema 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Arboles', 'A' )  
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Navegacion', 'A' )      
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Usuarios', 'A' )   
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Grupos_De_Usuarios', 'A' )   
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Grupos_Usuarios_Miembros', 'A' )     
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Privilegios_Grupo', 'A' )      

---- Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_CFGC_TipoCambio', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Operaciones_Supervisadas', 'A' ) 
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Permisos_Operaciones_Supervisadas', 'A' ) 

 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFGC_EnvioDetalles', 'A' )     
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFGC_EnvioDetallesTrans', 'A' )      
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFGS_ConfigurarConexiones', 'A' )      
 Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CFGS_EnvioCatalogos', 'A' )      

----------- Tablas de Transferencias 
-- Insert Into CFGS_EnvioCatalogos ( NombreTabla, IdOrden ) values ( 'TransferenciasEnvioEnc', 160 ) 
-- Insert Into CFGS_EnvioCatalogos ( NombreTabla, IdOrden ) values ( 'TransferenciasEnvioDet', 161 ) 
-- Insert Into CFGS_EnvioCatalogos ( NombreTabla, IdOrden ) values ( 'TransferenciasEnvioDet_Lotes', 162 ) 
-- Insert Into CFGS_EnvioCatalogos ( NombreTabla, IdOrden ) values ( 'TransferenciasEnvioDet_LotesRegistrar', 163 )   

--------- Tablas de permisos con Huella
Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'CatPersonalHuellas', 'C' ) 
Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Permisos_Operaciones_SupervisadasHuellas', 'C' ) 
Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Operaciones_SupervisadasHuellas', 'C' ) 
Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Permisos_Operaciones_SupervisadasPorFarmaciaHuellas', 'C' ) 
Insert Into CFGS_EnvioCatalogos ( NombreTabla, Status ) values ( 'Net_Operaciones_SupervisadasPorFarmaciaHuellas', 'C' ) 

Update CFGS_EnvioCatalogos Set IdOrden = IdEnvio + 100 
Go--#SQL  

--    Select * From CFGS_EnvioCatalogos (nolock) 

---   Update CFGS_EnvioCatalogos Set IdOrden = IdEnvio + 100 

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGSC_EnvioDetallesTrans' and xType = 'U' ) 
   Drop Table CFGSC_EnvioDetallesTrans  
Go--#SQL   

Create Table CFGSC_EnvioDetallesTrans 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGSC_EnvioDetallesTrans Add Constraint PK_CFGSC_EnvioDetallesTrans Primary Key ( NombreTabla ) 
Go--#SQL   

 Insert Into CFGSC_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet', 'A' ) 
 Insert Into CFGSC_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet_LotesRegistrar', 'A' )  
 Insert Into CFGSC_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEstadisticaClavesDispensadas', 'A' )   
 
 Update CFGSC_EnvioDetallesTrans Set IdOrden = IdEnvio + 100 
Go--#SQL  

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGS_EnvioDetallesTrans' and xType = 'U'  ) 
   Drop Table CFGS_EnvioDetallesTrans  
Go--#SQL   

Create Table CFGS_EnvioDetallesTrans 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGS_EnvioDetallesTrans Add Constraint PK_CFGS_EnvioDetallesTrans Primary Key ( NombreTabla ) 
Go--#SQL   

 Insert Into CFGS_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioEnc', 'A' ) 
 Insert Into CFGS_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet', 'A' ) 
 Insert Into CFGS_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet_Lotes', 'A' ) 
 Insert Into CFGS_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet_LotesRegistrar', 'A' )  
 Insert Into CFGS_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEstadisticaClavesDispensadas', 'A' )    
 
 Update CFGS_EnvioDetallesTrans Set IdOrden = IdEnvio + 100 
Go--#SQL  



---------------------------------------------- 
/* 
	Informacion enviada a Oficina Central
*/ 
---------------------------------------------- 
------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_EnvioDetalles' and xType = 'U' ) 
   Drop Table CFGC_EnvioDetalles  
Go--#SQL   

Create Table CFGC_EnvioDetalles 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGC_EnvioDetalles Add Constraint PK_CFGC_EnvioDetalles Primary Key ( NombreTabla ) 
Go--#SQL   

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Net_Usuarios', 'C' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Net_CFGC_Parametros', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'CtlCortesParciales', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'CtlCortesDiarios', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Ctl_CierresDePeriodos', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Ctl_CierresPeriodosDetalles', 'A' )  
 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatPasillos', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatPasillos_Estantes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatPasillos_Estantes_Entrepaños', 'A' )   
 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'FarmaciaProductos', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'FarmaciaProductos_CodigoEAN', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'FarmaciaProductos_CodigoEAN_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones', 'A' )  
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'FarmaciaProductos_CodigoEAN_Lotes__Historico', 'A' )  ----- 


 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Movtos_Inv_Tipos_Farmacia', 'C' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'MovtosInv_Enc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'MovtosInv_Det_CodigosEAN', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'MovtosInv_Det_CodigosEAN_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones', 'A' ) 


 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'EntradasEnc_Consignacion', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'EntradasDet_Consignacion', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'EntradasDet_Consignacion_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'EntradasDet_Consignacion_Lotes_Ubicaciones', 'A' )  


 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'AjustesInv_Enc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'AjustesInv_Det', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'AjustesInv_Det_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'AjustesInv_Det_Lotes_Ubicaciones', 'A' )  
 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'CambiosProv_Enc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'CambiosProv_Det_CodigosEAN', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'CambiosProv_Det_CodigosEAN_Lotes', 'A' )  

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatMedicos', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatBeneficiarios', 'A' ) 

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasDet_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasDet_Lotes_Ubicaciones', 'A' )  
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasEstDispensacion', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasInformacionAdicional', 'A' )  
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasEstadisticaClavesDispensadas', 'A' )  

-- Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Ventas_ValesEnc', 'A' )   
-- Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Ventas_ValesDet', 'A' )    

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Vales_EmisionEnc', 'A' )   
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Vales_EmisionDet', 'A' )    
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Vales_Emision_InformacionAdicional', 'A' )   
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Vales_Cancelacion', 'A' )    
 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ValesEnc', 'A' )    
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ValesDet', 'A' )    
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ValesDet_Lotes', 'A' )     


 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'RemisionesEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'RemisionesDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'RemisionesDet_Lotes', 'A' ) 

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ComprasEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ComprasDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ComprasDet_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ComprasDet_Lotes_Ubicaciones', 'A' ) 

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'OrdenesDeComprasEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'OrdenesDeComprasDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'OrdenesDeComprasDet_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'OrdenesDeComprasDet_Lotes_Ubicaciones', 'A' ) 

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasDet_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasDet_Lotes_Ubicaciones', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEstadisticaClavesDispensadas', 'A' )   

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEnvioEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet_LotesRegistrar', 'A' )  

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionTransferenciasEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionTransferenciasDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionTransferenciasDet_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionTransferenciasDet_Lotes_Ubicaciones', 'A' ) 
 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TraspasosEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TraspasosDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TraspasosDet_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'TraspasosDet_Lotes_Ubicaciones', 'A' ) 

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionesEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionesDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionesDet_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionesDet_Lotes_Ubicaciones', 'A' ) 
 

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PreSalidasPedidosEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PreSalidasPedidosDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PreSalidasPedidosDet_Lotes_Ubicaciones', 'A' ) 

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDet_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDet_Lotes_Ubicaciones', 'A' )   
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosEnvioEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosEnvioDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosEnvioDet_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosEnvioDet_Lotes_Registrar', 'A' ) 


 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDistEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDistDet', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDistDet_Lotes', 'A' ) 

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatPersonalCEDIS', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Enc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Det', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Det_Pedido_Distribuidor', 'A' )  

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Enc_Surtido', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Enc_Surtido_Atenciones', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Det_Surtido', 'A' )   
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Det_Surtido_Distribucion', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso', 'A' ) 
 

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ALMJ_Pedidos_RC', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ALMJ_Pedidos_RC_Det', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ALMJ_Concentrado_PedidosRC', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ALMJ_Concentrado_PedidosRC_Claves', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'ALMJ_Concentrado_PedidosRC_Pedidos', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Ventas_ALMJ_PedidosRC_Surtido', 'A' )  

 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Ventas_TiempoAire', 'A' ) 
   
--------- Estas siempre van al final 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Adt_VentasEnc', 'A' ) 
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Adt_VentasInformacionAdicional', 'A' )  
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Adt_ComprasEnc', 'A' )  
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Adt_FarmaciaProductos_CodigoEAN_Lotes', 'A' )     
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Adt_OrdenesDeComprasEnc', 'A' )
 Insert Into CFGC_EnvioDetalles ( NombreTabla, Status ) values ( 'Ctl_Replicaciones', 'A' )          
   
Update CFGC_EnvioDetalles Set IdOrden = IdEnvio + 100 
Go--#SQL  

--    Select * From CFGC_EnvioDetalles (nolock) 

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_EnvioDetallesTrans' and xType = 'U' ) 
   Drop Table CFGC_EnvioDetallesTrans  
Go--#SQL   

Create Table CFGC_EnvioDetallesTrans 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGC_EnvioDetallesTrans Add Constraint PK_CFGC_EnvioDetallesTrans Primary Key ( NombreTabla ) 
Go--#SQL   

 Insert Into CFGC_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioEnc', 'A' ) 
 Insert Into CFGC_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet', 'A' ) 
 Insert Into CFGC_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet_Lotes', 'A' ) 
 Insert Into CFGC_EnvioDetallesTrans ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet_LotesRegistrar', 'A' )  
 
 Update CFGC_EnvioDetallesTrans Set IdOrden = IdEnvio + 100 
Go--#SQL  


-------------------------------------------------------------------------------------------------------- 
---------------------------------------------- 
/* 
	Informacion enviada a Oficina Central 
*/ 
---------------------------------------------- 
------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGSC_EnvioDetalles' and xType = 'U' ) 
   Drop Table CFGSC_EnvioDetalles  
Go--#SQL   

Create Table CFGSC_EnvioDetalles 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  	
)
Go--#SQL   

Alter Table CFGSC_EnvioDetalles Add Constraint PK_CFGSC_EnvioDetalles Primary Key ( NombreTabla ) 
Go--#SQL   

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Net_CFGC_Parametros', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CtlCortesParciales', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CtlCortesDiarios', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Ctl_CierresDePeriodos', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Ctl_CierresPeriodosDetalles', 'A' )  
 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatPasillos', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatPasillos_Estantes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatPasillos_Estantes_Entrepaños', 'A' )    
 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'FarmaciaProductos', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'FarmaciaProductos_CodigoEAN', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'FarmaciaProductos_CodigoEAN_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones', 'A' )   

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Movtos_Inv_Tipos_Farmacia', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'MovtosInv_Enc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'MovtosInv_Det_CodigosEAN', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'MovtosInv_Det_CodigosEAN_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones', 'A' ) 


 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'EntradasEnc_Consignacion', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'EntradasDet_Consignacion', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'EntradasDet_Consignacion_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'EntradasDet_Consignacion_Lotes_Ubicaciones', 'A' )  

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'AjustesInv_Enc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'AjustesInv_Det', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'AjustesInv_Det_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'AjustesInv_Det_Lotes_Ubicaciones', 'A' )  
 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CambiosProv_Enc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CambiosProv_Det_CodigosEAN', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CambiosProv_Det_CodigosEAN_Lotes', 'A' )  


 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatMedicos', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatBeneficiarios', 'A' ) 

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasDet_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasDet_Lotes_Ubicaciones', 'A' )  
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasEstDispensacion', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasInformacionAdicional', 'A' )  
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'VentasEstadisticaClavesDispensadas', 'A' )   

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Vales_EmisionEnc', 'A' )   
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Vales_EmisionDet', 'A' )    
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Vales_Emision_InformacionAdicional', 'A' )   
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Vales_Cancelacion', 'A' )     
 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ValesEnc', 'A' )    
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ValesDet', 'A' )    
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ValesDet_Lotes', 'A' )     

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'RemisionesEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'RemisionesDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'RemisionesDet_Lotes', 'A' ) 

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ComprasEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ComprasDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ComprasDet_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ComprasDet_Lotes_Ubicaciones', 'A' ) 

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'OrdenesDeComprasEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'OrdenesDeComprasDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'OrdenesDeComprasDet_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'OrdenesDeComprasDet_Lotes_Ubicaciones', 'A' ) 

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasDet_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasDet_Lotes_Ubicaciones', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEstadisticaClavesDispensadas', 'A' )   

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEnvioEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TransferenciasEnvioDet_LotesRegistrar', 'A' )  

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TraspasosEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TraspasosDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TraspasosDet_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'TraspasosDet_Lotes_Ubicaciones', 'A' ) 

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionesEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionesDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionesDet_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'DevolucionesDet_Lotes_Ubicaciones', 'A' ) 


 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PreSalidasPedidosEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PreSalidasPedidosDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PreSalidasPedidosDet_Lotes_Ubicaciones', 'A' ) 

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDet_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDet_Lotes_Ubicaciones', 'A' )   
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosEnvioEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosEnvioDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosEnvioDet_Lotes', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosEnvioDet_Lotes_Registrar', 'A' ) 

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDistEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDistDet', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'PedidosDistDet_Lotes', 'A' ) 

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CatPersonalCEDIS', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Enc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Det', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Det_Pedido_Distribuidor', 'A' )  

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Enc_Surtido', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Enc_Surtido_Atenciones', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Det_Surtido', 'A' )   
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Det_Surtido_Distribucion', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Pedidos_Cedis_Det_Surtido_Distribucion_Reproceso', 'A' ) 

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ALMJ_Pedidos_RC', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ALMJ_Pedidos_RC_Det', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ALMJ_Concentrado_PedidosRC', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ALMJ_Concentrado_PedidosRC_Claves', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'ALMJ_Concentrado_PedidosRC_Pedidos', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Ventas_ALMJ_PedidosRC_Surtido', 'A' )  

 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Ventas_TiempoAire', 'A' ) 
 
--------- Estas siempre van al final  
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Adt_VentasEnc', 'A' ) 
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Adt_VentasInformacionAdicional', 'A' )  
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Adt_ComprasEnc', 'A' )  
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Adt_FarmaciaProductos_CodigoEAN_Lotes', 'A' )   
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'Adt_OrdenesDeComprasEnc', 'A' )      
   
   
------------- 2K110630.2022 Jesus Diaz
------ Tabla que contiene las Direcciones Url de las Unidades    
 Insert Into CFGSC_EnvioDetalles ( NombreTabla, Status ) values ( 'CFGS_ConfigurarConexiones', 'A' )     
   
   
Update CFGSC_EnvioDetalles Set IdOrden = IdEnvio + 100 
Go--#SQL  
