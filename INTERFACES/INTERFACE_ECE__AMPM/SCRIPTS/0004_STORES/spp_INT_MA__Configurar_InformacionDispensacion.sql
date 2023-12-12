-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__Configurar_InformacionDispensacion' and xType = 'P' ) 
   Drop Proc spp_INT_MA__Configurar_InformacionDispensacion
Go--#SQL 

Create Proc spp_INT_MA__Configurar_InformacionDispensacion 
( 
	@IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	@IdPrograma varchar(4) = '', @IdSubPrograma varchar(4) = '', @Status varchar(1) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 

	Exec spp_Mtto_CFG_EstadosClientes @IdEstado, @IdCliente, @Status 
	Exec spp_Mtto_CFG_EstadosFarmaciasClientes @IdEstado, @IdFarmacia, @IdCliente, @Status 
	Exec spp_Mtto_CFG_EstadosFarmaciasClientesSubClientes @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @Status 
	Exec spp_Mtto_CFG_EstadosFarmaciasProgramasSubProgramas 
		@IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, @Status  

End 
Go--#SQL 
