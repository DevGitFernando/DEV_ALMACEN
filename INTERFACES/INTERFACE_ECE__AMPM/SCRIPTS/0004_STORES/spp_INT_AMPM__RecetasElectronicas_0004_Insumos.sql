-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_AMPM__RecetasElectronicas_0004_Insumos' and xType = 'P' ) 
   Drop Proc spp_INT_AMPM__RecetasElectronicas_0004_Insumos
Go--#SQL 

Create Proc spp_INT_AMPM__RecetasElectronicas_0004_Insumos
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', 	
	@Folio varchar(12) = '', 

	@CodigoEAN varchar(30) = '', 

	@CantidadRequerida int = 0, 
	@CantidadEntregada int = 0, 
	
	@IdMedicina varchar(10) = '', 
	@Via varchar(10) = '', 
	@Dosis varchar(10) = '', 
	@Unidades varchar(10) = '', 
	@Frecuencia varchar(10) = '', 
	@FechaInicio varchar(10) = '', 
	@FechaFin varchar(10) = '', 
	@Observaciones varchar(200) = '',
	@Existencia int = 0,
	@TextoLibre bit = 0,
	@DescripcionGenerica Varchar(500) = ''
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	--@sFolio varchar(20), 
	@sMensaje varchar(200),
	@iPartida int
	
	--Select @sFolio = Folio 
	--From INT_RE_INTERMED__RecetasElectronicas_XML (NoLock)
	--Where @IdEmpresa = IdEmpresa And @IdEstado = IdEstado And @IdFarmacia = IdFarmacia And Folio_SIADISSEP = @FolioReceta And  uMedica = @Clues_Emisor


	If Not Exists ( Select * From INT_AMPM__RecetasElectronicas_0004_Insumos (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio And CodigoEAN = @CodigoEAN) 
	Begin 
		Insert Into INT_AMPM__RecetasElectronicas_0004_Insumos
		( 
			IdEmpresa, IdEstado, IdFarmacia, Folio,
			CodigoEAN, CantidadRequerida,CantidadEntregada,
			IdMedicina, Via, Dosis, Unidades, Frecuencia, FechaInicio, FechaFin, Observaciones, Existencia
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @Folio,
			@CodigoEAN, @CantidadRequerida,@CantidadEntregada,
			@IdMedicina, @Via, @Dosis, @Unidades, @Frecuencia, @FechaInicio, @FechaFin, @Observaciones, @Existencia
	End

	if (@TextoLibre = 1)
	Begin

		Select @iPartida = IsNull(max(Partida), 0) + 1
		From INT_AMPM__RecetasElectronicas_0005_InsumosNoCatalogo
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio

		Insert Into INT_AMPM__RecetasElectronicas_0005_InsumosNoCatalogo
		( 
			IdEmpresa, IdEstado, IdFarmacia, Folio,
			Partida, DescripcionGenerica
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @Folio,
			@iPartida, @DescripcionGenerica
	End

	
End 
Go--#SQL 
	