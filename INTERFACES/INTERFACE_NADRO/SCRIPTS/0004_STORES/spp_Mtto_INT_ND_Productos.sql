------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_Productos' and xType = 'P') 
    Drop Proc spp_Mtto_INT_ND_Productos
Go--#SQL 
  
--  Exec spp_Mtto_INT_ND_Productos '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_Mtto_INT_ND_Productos 
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
	Set @sMensaje = 'Información de productos integrada satisfactoriamente.' 
	
--------------------------------------------------- Insercion de datos 
	Select 
		identity(int, 1, 1) as Keyx_Auxiliar, 
		@IdEmpresa as IdEmpresa, @IdEstado as IdEstado, 
		ClaveSSA_ND, Codigo, Descripcion, Proveedor, CodigoEAN_ND, ContenidoPaquete, ClaveSSA, IdProducto, CodigoEAN 
	Into #tmpProductos 	
	From INT_ND_Productos_CargaMasiva 
	--Where CodigoEAN_Existe = 1 


	Truncate Table INT_ND_Productos 	
	Insert Into INT_ND_Productos 
	( 
		Keyx_Auxiliar, IdEstado, ClaveSSA_ND, Codigo, Descripcion, Proveedor, CodigoEAN_ND, ContenidoPaquete, ClaveSSA, IdProducto, CodigoEAN 
	) 
	Select 
		Keyx_Auxiliar, IdEstado, ClaveSSA_ND, Codigo, Descripcion, Proveedor, CodigoEAN_ND, ContenidoPaquete, ClaveSSA, IdProducto, CodigoEAN 
	From #tmpProductos 

			
--------------------------------------------------- Insercion de datos 

------------------------------- SALIDA FINAL 
	Select @FolioIntegracion as FolioIntegracion, @sMensaje as Mensaje 

---		spp_Mtto_INT_ND_Productos  	
			
	
End  
Go--#SQL 

