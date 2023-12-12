------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_Existencias' and xType = 'P') 
    Drop Proc spp_Mtto_INT_ND_Existencias
Go--#SQL 
  
--  Exec spp_Mtto_INT_ND_Existencias '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_Mtto_INT_ND_Existencias 
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
		

--------------------------------------------------- Obtener el folio 
	Select @FolioIntegracion = max(FolioIntegracion) + 1 
	From INT_ND_Existencias_Historico (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = 	@IdFarmacia 
	Set @FolioIntegracion = IsNull(@FolioIntegracion, 1) 	
	Set @sMensaje = 'Información guardada satisfactoriamente con el Folio ' + cast(@FolioIntegracion as varchar)	
--------------------------------------------------- Obtener el folio 	
	
	
--------------------------------------------------- Actualizar los datos historicos 
	Update H Set CantidadAsignada = E.CantidadAsignada 
	From INT_ND_Existencias_Historico H (NoLock) 
	Inner Join INT_ND_Existencias E (NoLock) 
		On ( H.IdEmpresa = E.IdEmpresa and H.IdEstado = E.IdEstado and H.IdFarmacia = E.IdFarmacia and H.FolioIntegracion = E.FolioIntegracion 
			 And H.IdProducto = E.IdProducto and H.CodigoEAN = E.CodigoEAN ) 
--------------------------------------------------- Actualizar los datos historicos 
	
	
	
--------------------------------------------------- Insercion de datos 
	Select 
		identity(int, 1, 1) as Keyx_Auxiliar, 
		getdate() as FechaRegistro, 
		@IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @IdPersonal as IdPersonal, 
		@FolioIntegracion as Folio, 
		ClaveSSA_ND, CodigoEAN_ND, Cantidad, ClaveSSA, IdProducto, CodigoEAN 
	Into #tmpExistencia 	
	From INT_ND_Existencias_CargaMasiva 
	-- Where CodigoEAN_Existe = 1 



	Truncate Table INT_ND_Existencias 	
	Insert Into INT_ND_Existencias 
	( 
		Keyx_Auxiliar, FechaRegistro, IdEmpresa, IdEstado, IdFarmacia, IdPersonal, FolioIntegracion, 
		ClaveSSA_ND, CodigoEAN_ND, Cantidad, ClaveSSA, IdProducto, CodigoEAN 
	) 
	Select 
		Keyx_Auxiliar, FechaRegistro, IdEmpresa, IdEstado, IdFarmacia, IdPersonal, Folio, 
		ClaveSSA_ND, CodigoEAN_ND, Cantidad, ClaveSSA, IdProducto, CodigoEAN 
	From #tmpExistencia 
	

	
	Insert Into INT_ND_Existencias_Historico 
	(	
		FechaRegistro, IdEmpresa, IdEstado, IdFarmacia, IdPersonal, FolioIntegracion, 
		ClaveSSA_ND, CodigoEAN_ND, Cantidad, ClaveSSA, IdProducto, CodigoEAN 
	) 
	Select 
		FechaRegistro, IdEmpresa, IdEstado, IdFarmacia, IdPersonal, Folio, 
		ClaveSSA_ND, CodigoEAN_ND, Cantidad, ClaveSSA, IdProducto, CodigoEAN 
	From #tmpExistencia 		
--------------------------------------------------- Insercion de datos 

------------------------------- SALIDA FINAL 
	Select @FolioIntegracion as FolioIntegracion, @sMensaje as Mensaje 

---		spp_Mtto_INT_ND_Existencias  	
			
	
End  
Go--#SQL 

