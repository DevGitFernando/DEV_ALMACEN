If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFGC_Titulos_Reportes_Detallado_Venta' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFGC_Titulos_Reportes_Detallado_Venta 
Go--#SQL 

Create Proc spp_Mtto_CFGC_Titulos_Reportes_Detallado_Venta 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', 
	
	@Mostrar_Cliente bit = 0, @Mostrar_Solo_Etiqueta_Cliente bit = 0,
	@Mostrar_SubCliente bit = 0, @Mostrar_Solo_Etiqueta_SubCliente bit = 0,
	@Mostrar_SubCliente_Como_Cliente bit = 0, 
	@Mostrar_Descripcion_Perfil bit = 0, 	
	@Mostrar_Programa bit = 0, @Mostrar_Solo_Etiqueta_Programa bit = 0,
	@Mostrar_SubPrograma bit = 0, @Mostrar_Solo_Etiqueta_SubPrograma bit = 0, 

	@Mostrar_Beneficiario bit = 0, @Mostrar_Solo_Etiqueta_Beneficiario bit = 0,
	@Mostrar_FolioReferencia bit = 0, @Mostrar_Solo_Etiqueta_FolioReferencia bit = 0,
	@Mostrar_FolioDocumento bit = 0, @Mostrar_Solo_Etiqueta_FolioDocumento bit = 0, 
	
	@Mostrar_Presentacion_ContenidoPaquete bit = 0  
) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CFGC_Titulos_Reportes_Detallado_Venta 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia ) 
		Begin 
			Insert Into CFGC_Titulos_Reportes_Detallado_Venta 
			( 
				IdEmpresa, IdEstado, IdFarmacia, 
				Mostrar_Cliente, Mostrar_Solo_Etiqueta_Cliente, 
				Mostrar_SubCliente, Mostrar_Solo_Etiqueta_SubCliente, 
				Mostrar_SubCliente_Como_Cliente, 
				Mostrar_Descripcion_Perfil, 
				Mostrar_Programa, Mostrar_Solo_Etiqueta_Programa, 
				Mostrar_SubPrograma, Mostrar_Solo_Etiqueta_SubPrograma, 
				Mostrar_Beneficiario, Mostrar_Solo_Etiqueta_Beneficiario, 
				Mostrar_FolioReferencia, Mostrar_Solo_Etiqueta_FolioReferencia, 
				Mostrar_FolioDocumento, Mostrar_Solo_Etiqueta_FolioDocumento, 
				Mostrar_Presentacion_ContenidoPaquete  
			) 
			Select 
				@IdEmpresa, @IdEstado, @IdFarmacia, 
				@Mostrar_Cliente, @Mostrar_Solo_Etiqueta_Cliente, 
				@Mostrar_SubCliente, @Mostrar_Solo_Etiqueta_SubCliente, 
				@Mostrar_SubCliente_Como_Cliente, 
				@Mostrar_Descripcion_Perfil, 
				@Mostrar_Programa, @Mostrar_Solo_Etiqueta_Programa, 
				@Mostrar_SubPrograma, @Mostrar_Solo_Etiqueta_SubPrograma, 
				@Mostrar_Beneficiario, @Mostrar_Solo_Etiqueta_Beneficiario, 
				@Mostrar_FolioReferencia, @Mostrar_Solo_Etiqueta_FolioReferencia, 
				@Mostrar_FolioDocumento, @Mostrar_Solo_Etiqueta_FolioDocumento, 
				@Mostrar_Presentacion_ContenidoPaquete   
		End 
	Else 
		Begin 
			Update C Set 
				Mostrar_Cliente = @Mostrar_Cliente, 
				Mostrar_Solo_Etiqueta_Cliente = @Mostrar_Solo_Etiqueta_Cliente, 
				Mostrar_SubCliente = @Mostrar_SubCliente, 
				Mostrar_Solo_Etiqueta_SubCliente = @Mostrar_Solo_Etiqueta_SubCliente, 
				Mostrar_SubCliente_Como_Cliente = @Mostrar_SubCliente_Como_Cliente, 
				Mostrar_Descripcion_Perfil = @Mostrar_Descripcion_Perfil, 
				Mostrar_Programa = @Mostrar_Programa, 
				Mostrar_Solo_Etiqueta_Programa = @Mostrar_Solo_Etiqueta_Programa, 
				Mostrar_SubPrograma = @Mostrar_SubPrograma, 
				Mostrar_Solo_Etiqueta_SubPrograma = @Mostrar_Solo_Etiqueta_SubPrograma, 
				Mostrar_Beneficiario = @Mostrar_Beneficiario, 
				Mostrar_Solo_Etiqueta_Beneficiario = @Mostrar_Solo_Etiqueta_Beneficiario, 
				Mostrar_FolioReferencia = @Mostrar_FolioReferencia, 
				Mostrar_Solo_Etiqueta_FolioReferencia = @Mostrar_Solo_Etiqueta_FolioReferencia, 
				Mostrar_FolioDocumento = @Mostrar_FolioDocumento,  
				Mostrar_Solo_Etiqueta_FolioDocumento = @Mostrar_Solo_Etiqueta_FolioDocumento, 
				Mostrar_Presentacion_ContenidoPaquete = @Mostrar_Presentacion_ContenidoPaquete   
			From CFGC_Titulos_Reportes_Detallado_Venta C (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia		
		End 

End 
Go--#SQL 
   