If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_TransferenciasEstatales_EnvioValidar' and xType = 'P' ) 
	Drop Proc spp_Mtto_TransferenciasEstatales_EnvioValidar 
Go--#SQL

Create Proc spp_Mtto_TransferenciasEstatales_EnvioValidar 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1006', @IdEstadoEnvia varchar(2) = '21', 
	@IdFarmaciaEnvia varchar(4) = '1006', @FolioTransferencia varchar(20) = '284' 
) 
With Encryption 
As 
Set NoCount On 
Declare 
	@sFolioTransferencia varchar(20),  
	@iCantidadesCorrectas bit, 
	@iExistenLotes bit, @iError bit  
	
	Set @sFolioTransferencia = 'TS' + right('00000000000000000' + @FolioTransferencia, 8) 
	Set @iCantidadesCorrectas = 0 
	Set @iExistenLotes = 0  
	Set @iError = 0

	Select * 
	into #tmpEnc 
	From TransferenciasEnvioEnc (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstadoRecibe = @IdEstado and IdFarmaciaRecibe = @IdFarmacia 
		and IdEstadoEnvia = @IdEstadoEnvia and IdFarmaciaEnvia = @IdFarmaciaEnvia and FolioTransferencia = @sFolioTransferencia 
	--FolioTransferencia like '%284%'  
	
	
	Select IdProducto, CodigoEAN, cast(CantidadEnviada as int) as Cantidad, 0 as Lotes, 0 as EsCorrecto   
	into #tmpEAN 
	From TransferenciasEnvioDet (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstadoRecibe = @IdEstado and IdFarmaciaRecibe = @IdFarmacia 
		and IdEstadoEnvia = @IdEstadoEnvia and IdFarmaciaEnvia = @IdFarmaciaEnvia and FolioTransferencia = @sFolioTransferencia 
		-- and IdProducto = 10991   
		
		
	Select IdSubFarmaciaEnvia as  IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, sum(cast(CantidadEnviada as int)) as Cantidad, 0 as Registro  
	into #tmpLotes 
	From TransferenciasEnvioDet_Lotes (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstadoRecibe = @IdEstado and IdFarmaciaRecibe = @IdFarmacia 
		and IdEstadoEnvia = @IdEstadoEnvia and IdFarmaciaEnvia = @IdFarmaciaEnvia and FolioTransferencia = @sFolioTransferencia 		
		-- and IdProducto = 10991   		
	group by IdSubFarmaciaEnvia, IdProducto, CodigoEAN, ClaveLote 	
			
	
	Select IdSubFarmaciaEnvia as IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote 
	into #tmpRegistro 
	From TransferenciasEnvioDet_LotesRegistrar (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstadoRecibe = @IdEstado and IdFarmaciaRecibe = @IdFarmacia 
		and IdEstadoEnvia = @IdEstadoEnvia and IdFarmaciaEnvia = @IdFarmaciaEnvia and FolioTransferencia = @sFolioTransferencia 			
		-- and IdProducto = 10991   

---		spp_Mtto_TransferenciasEstatales_EnvioValidar  
	
	Update E Set Lotes = IsNull(( select sum(Cantidad) From #tmpLotes L Where L.IdProducto = E.IdProducto and L.CodigoEAN = E.CodigoEAN ), 0) 	
	From #tmpEAN E 
	
	Update E Set EsCorrecto = 1 From #tmpEAN E Where Cantidad = Lotes  
	
	Update L Set Registro = 1 
	From #tmpLotes L 
	Inner Join #tmpRegistro R 
		On ( L.IdSubFarmacia = R.IdSubFarmacia and L.IdProducto = R.IdProducto and L.CodigoEAN = R.CodigoEAN and L.ClaveLote= R.ClaveLote )  


----------------------- Revision final 
	If Not Exists ( Select top 1 * From #tmpEAN E where EsCorrecto = 0 ) 
	   Set @iCantidadesCorrectas = 1 

	If Not Exists ( Select top 1 * From #tmpLotes E where Registro = 0 ) 
	   Set @iExistenLotes = 1 
----------------------- Revision final 	   

	If @iCantidadesCorrectas = 0 OR @iExistenLotes = 0
		Set @iError = 1

	Select @iCantidadesCorrectas as CantidadesCorrectas, @iExistenLotes as ExistenTodosLosLotes, @iError as ExisteError
	select * from 	#tmpEAN 
	select * from 	#tmpLotes 
--	select * from   #tmpRegistro 

Go--#SQL 
	