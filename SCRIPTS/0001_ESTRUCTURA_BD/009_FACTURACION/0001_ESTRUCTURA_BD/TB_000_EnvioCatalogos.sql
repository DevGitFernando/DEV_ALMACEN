Set NoCount On
Go--#SQL   

---------------------------------------------- 
/* 
	Informacion enviada a Puntos de Venta 
*/ 
---------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGSC_EnvioCatalogos__Facturacion' and xType = 'U' ) 
   Drop Table CFGSC_EnvioCatalogos__Facturacion 
Go--#SQL   

Create Table CFGSC_EnvioCatalogos__Facturacion 
(
	IdEnvio int identity(1,1), 
	NombreTabla varchar(100) Not Null, 
	IdOrden int Not Null Default 0, 
	IdGrupo int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL   

Alter Table CFGSC_EnvioCatalogos__Facturacion Add Constraint PK_CFGSC_EnvioCatalogos__Facturacion Primary Key ( NombreTabla ) 
Go--#SQL   

 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatEmpresas' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatEstados' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatMunicipios' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatColonias' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatJurisdicciones' )  
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatEmpresasEstados' )  
 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatRegiones' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatSubRegiones' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatFamilias' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatSubFamilias' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatSegmentosSubFamilias' )  
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatUnidadesMedicas' )  
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatTiposUnidades' )   

 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatFarmacias' )  
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatEstados_SubFarmacias' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatFarmacias_SubFarmacias' )   
 -- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatFarmacias_ConvenioVales' )   
 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatPersonal' )  
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatTipoCatalogoClaves' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatGruposTerapeuticos' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatTiposDeProducto' )  
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatClavesSSA_Sales' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_ClavesSSA_ClavesRelacionadas' )  
 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatClasificacionesSSA' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatProgramas' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatSubProgramas' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatSubProgramas_Farmacias' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatLaboratorios' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatPresentaciones' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatProductos' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatProductos_CodigosRelacionados' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatProductos_Estado' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatTiposDeClientes' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatClientes' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatSubClientes' ) 
 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatProveedores' )  
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatDistribuidores' ) 
 
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatCajeros' ) 
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatCajas' ) 
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'Movtos_Inv_Tipos' ) 
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'Movtos_Inv_Tipos_Farmacia' ) 

 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatEspecialidades' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatServicios' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatServicios_Areas' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatServicios_Areas_Farmacias' )  
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatTiposDispensacion' )  
 
 
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatCentrosDeSalud' ) 
 
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatCompaniasTiempoAire' ) 
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatCompaniasTA_Montos' ) 
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CatPersonalTA' ) 

 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_Clientes_Claves' )  
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_Clientes_SubClientes_Claves' )  
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_ClavesSSA_Precios' )  
 
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_Claves_Excluir_NivelAbasto' )  
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_CB_NivelesAtencion' )  
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_CB_NivelesAtencion_Miembros' )   
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_CB_CuadroBasico_Claves' )    
 
 

 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_EmpresasFarmacias' )   
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_EstadosClientes' )   
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_EstadosFarmaciasClientes' )      
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_EstadosFarmaciasClientesSubClientes' )      
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFG_EstadosFarmaciasProgramasSubProgramas' )       
 
 
----------- Tablas de sistema 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'Net_Arboles' )  
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'Net_Navegacion' )      
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'Net_Usuarios' )   
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'Net_Grupos_De_Usuarios' )   
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'Net_Grupos_Usuarios_Miembros' )     
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'Net_Privilegios_Grupo' )      

---- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'Net_CFGC_TipoCambio' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'Net_Operaciones_Supervisadas' ) 
 Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'Net_Permisos_Operaciones_Supervisadas' ) 

-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFGC_EnvioDetalles' )     
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFGC_EnvioDetallesTrans' )      
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFGS_ConfigurarConexiones' )      
-- Insert Into CFGSC_EnvioCatalogos__Facturacion ( NombreTabla ) values ( 'CFGS_EnvioCatalogos' )      

Update CFGSC_EnvioCatalogos__Facturacion Set IdOrden = IdEnvio + 100 
Go--#SQL  
