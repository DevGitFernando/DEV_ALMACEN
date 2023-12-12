------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_Claves' and xType = 'P') 
    Drop Proc spp_Mtto_INT_ND_Claves
Go--#SQL 
  
--  Exec spp_Mtto_INT_ND_Claves '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_Mtto_INT_ND_Claves 
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
		ClaveSSA_ND, Descripcion, TipoInsumo, TipoInsumoDescripcion, ClaveSSA_ND_Auxiliar, ClaveSSA_ND_Mascara, EsClaveSSA_Valida   
	Into #tmpClaves 	
	From INT_ND_Claves_CargaMasiva 
	--Where CodigoEAN_Existe = 1 


	Truncate Table INT_ND_Claves 	
	Insert Into INT_ND_Claves 
	( 
		Keyx_Auxiliar, ClaveSSA_ND, Descripcion, TipoInsumo, TipoInsumoDescripcion, ClaveSSA_ND_Auxiliar, ClaveSSA_ND_Mascara, EsClaveSSA_Valida 
	) 
	Select 
		Keyx_Auxiliar, ClaveSSA_ND, Descripcion, TipoInsumo, TipoInsumoDescripcion, ClaveSSA_ND_Auxiliar, ClaveSSA_ND_Mascara, EsClaveSSA_Valida 
	From #tmpClaves 


--		sp_listacolumnas__stores spp_Proceso_INT_ND_ClavesND_ValidarDatosDeEntrada , 1 
			
			
--------------------------------------------------- Insercion de datos 

------------------------------- SALIDA FINAL 
	Select @FolioIntegracion as FolioIntegracion, @sMensaje as Mensaje 

---		spp_Mtto_INT_ND_Claves  	
			
	
End  
Go--#SQL 

