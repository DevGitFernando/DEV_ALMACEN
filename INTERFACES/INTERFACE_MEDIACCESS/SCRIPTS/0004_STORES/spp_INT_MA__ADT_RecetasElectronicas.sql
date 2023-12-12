If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__ADT_RecetasElectronicas' and xType = 'P' ) 
   Drop Proc spp_INT_MA__ADT_RecetasElectronicas
Go--#SQL 

Create Proc spp_INT_MA__ADT_RecetasElectronicas
( 
	@Folio_MA varchar(30) = '1', @IdEstadoModifica Varchar(2), @IdFarmaciaModifica Varchar(4), @IdPersonal Varchar(4)
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(10), 
	@sMensaje varchar(200),   
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4) 

		
	Insert Into INT_MA__ADT_RecetasElectronicas_002_Productos ( Folio_MA, Partida, CodigoEAN, CantidadSolicitada, CantidadSurtida ) 
	Select Folio_MA, Partida, CodigoEAN, CantidadSolicitada, CantidadSurtida
	From INT_MA__RecetasElectronicas_002_Productos
	Where Folio_MA = @Folio_MA
	
	Select Folio_MA, Partida, CodigoEAN, CantidadSolicitada, CantidadSurtida
	From INT_MA__RecetasElectronicas_002_Productos
	Where Folio_MA = @Folio_MA



	--------------- Reserva de Existencia 
	Select R.IdEmpresa, R.IdEstado, R.IdFarmacia, P.CodigoEAN as IdProducto, P.CantidadSolicitada
	Into #tmpProducto 
	From INT_MA__CFG_FarmaciasClinicas R (NoLock) 
	Inner Join INT_MA__RecetasElectronicas_001_Encabezado E (NoLock) On ( E.Folio_MA = @Folio_MA and E.IdFarmacia = R.Referencia_MA )
	Inner Join INT_MA__RecetasElectronicas_002_Productos P (NoLock) On (E.Folio_MA = P.Folio_MA)
	
	Update F Set CantidadReservada = ( F.CantidadReservada - CantidadSolicitada ) 
	From INT_MA__RecetasElectronicas_003_ReservaExistencia F (NoLock) 
	Inner Join #tmpProducto R (NoLock) 
		On ( F.IdEmpresa = R.IdEmpresa and F.IdEstado = R.IdEstado and F.IdFarmacia = R.IdFarmacia and F.IdProducto = R.IdProducto )
	-- Where  F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and F.IdProducto = @CodigoEAN
	
	--Select CantidadSolicitada
	--From INT_MA__RecetasElectronicas_003_ReservaExistencia F (NoLock) 
	--Inner Join #tmpProducto R (NoLock) 
	--	On ( F.IdEmpresa = R.IdEmpresa and F.IdEstado = R.IdEstado and F.IdFarmacia = R.IdFarmacia and F.IdProducto = R.IdProducto ) 		

	
	Delete INT_MA__RecetasElectronicas_002_Productos
	Where Folio_MA = @Folio_MA
--	select * from INT_MA__RecetasElectronicas_003_ReservaExistencia

	Insert Into INT_MA__ADT_RecetasElectronicas_001_Encabezado
		(Folio, Folio_MA, IdFarmacia, NombrePaciente, NombreMedico, Especialidad, Copago, PlanBeneficiario, FechaEmision, Elegibilidad, 
		CIE_01, CIE_02, CIE_03, CIE_04, EsRecetaManual, IdEstadoModifica, IdFarmaciaModifica, IdPersonalModifica)
	Select Folio, Folio_MA, IdFarmacia, NombrePaciente, NombreMedico, Especialidad, Copago, PlanBeneficiario, FechaEmision, Elegibilidad, 
		CIE_01, CIE_02, CIE_03, CIE_04, EsRecetaManual, @IdEstadoModifica, @IdFarmaciaModifica, @IdPersonal
		From INT_MA__RecetasElectronicas_001_Encabezado
	Where Folio_MA = @Folio_MA
	
	Delete INT_MA__RecetasElectronicas_001_Encabezado
	Where Folio_MA = @Folio_MA
	

End 
Go--#SQL 
	