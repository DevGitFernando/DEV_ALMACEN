------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_CuadrosBasicos' and xType = 'P') 
    Drop Proc spp_Mtto_INT_ND_CuadrosBasicos
Go--#SQL 
  
--  Exec spp_Mtto_INT_ND_CuadrosBasicos '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_Mtto_INT_ND_CuadrosBasicos 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0002', @IdPersonal varchar(4) = '0001'
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD  

Declare 
	@FolioIntegracion int, 
	@sMensaje varchar(1000),			
	@sStatus varchar(1), 
	@iLenEAN smallint, 
	@sCadena varchar(100) 
		

	Set @FolioIntegracion  = 1 
	Set @sMensaje = 'Información de Cuadros Básicos integrada satisfactoriamente.' 		
		
--------------------------------- TABLAS INTERMEDIAS  
	Select *, Keyx as Keyx_Auxiliar  
	Into #tmpCB 
	From INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva 
	-- Where EsClaveSSA_Valida = 1 or ClaveSSA <> '' 
	
	Select distinct IdEstado, IdAnexo, NombreAnexo, NombrePrograma, Prioridad 
	Into #tmpAnexos 
	From INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva 
	Order By Prioridad 	
--------------------------------- TABLAS INTERMEDIAS  	


--------------------------------- PASAR LA INFORMACION A LAS TABLAS DE PROCESO 
	-- Delete From INT_ND_CFG_CB_Anexos_Miembros 
	Delete From INT_ND_CFG_CB_CuadrosBasicos 
	Delete From INT_ND_CFG_CB_Anexos  	
	
	
	Insert Into INT_ND_CFG_CB_Anexos (  IdEstado, IdAnexo, NombreAnexo, NombrePrograma, Prioridad, Status ) 
	Select IdEstado, IdAnexo, NombreAnexo, NombrePrograma, Prioridad, 'A' as Status 
	From #tmpAnexos  

  
	Insert Into INT_ND_CFG_CB_CuadrosBasicos 
		( FechaRegistro, Keyx_Auxiliar, IdEstado, IdAnexo, Prioridad, ClaveSSA, ClaveSSA_ND, ClaveSSA_Mascara, ManejaIva, PrecioVenta, PrecioServicio, 
		Descripcion_Mascara, Lote, UnidadDeMedida, ContenidoPaquete, Vigencia ) 
	Select getdate(), Keyx_Auxiliar, IdEstado, IdAnexo, Prioridad, ClaveSSA, ClaveSSA_ND, ClaveSSA_Mascara, ManejaIva, PrecioVenta, PrecioServicio, 
		Descripcion_Mascara, Lote, UnidadDeMedida, ContenidoPaquete, Vigencia 
	from #tmpCB   
 
--------------------------------- PASAR LA INFORMACION A LAS TABLAS DE PROCESO 

------------------------------- SALIDA FINAL 
	Select @FolioIntegracion as FolioIntegracion, @sMensaje as Mensaje 

--		sp_listacolumnas INT_ND_CFG_CB_CuadrosBasicos 

---		spp_Mtto_INT_ND_CuadrosBasicos  	
			
	
End  
Go--#SQL 

