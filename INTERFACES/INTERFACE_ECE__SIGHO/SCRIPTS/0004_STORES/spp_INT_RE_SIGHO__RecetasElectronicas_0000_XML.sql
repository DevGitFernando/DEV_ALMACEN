-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_RE_SIGHO__RecetasElectronicas_001_XML' and xType = 'P' ) 
   Drop Proc spp_INT_RE_SIGHO__RecetasElectronicas_001_XML
Go--#SQL 

Create Proc spp_INT_RE_SIGHO__RecetasElectronicas_001_XML 
( 
	@UMedica varchar(50) = '', 
	@Folio_SIADISSEP varchar(50) = '', 
	@InformacionXML varchar(max) = '',
	@TipoDeProceso smallint = 0  	
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  
	
Declare 
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4) 
	
	
----- Obtener los datos de la farmacia

	Select Distinct IdEmpresa, IdEstado, IdFarmacia, Referencia_SIADISSEP
	Into #INT_RE_SIGHO__CFG_Farmacias_UMedicas
	From INT_RE_SIGHO__CFG_Farmacias_UMedicas
	
	Select @IdEmpresa = IdEmpresa, @IdEstado = IdEstado, @IdFarmacia = IdFarmacia 
	From #INT_RE_SIGHO__CFG_Farmacias_UMedicas (NoLock)
	--Where Referencia_SIADISSEP = @UMedica 
	
	Set @IdEmpresa = IsNull(@IdEmpresa, '') 
	Set @IdEstado = IsNull(@IdEstado, '') 
	Set @IdFarmacia = IsNull(@IdFarmacia, '') 
			


	Select @sFolio = cast(max(cast(Folio as int)) + 1 as varchar) 
	From INT_RE_SIGHO__RecetasElectronicas_XML (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
	
	Set @sFolio = IsNull(@sFolio, '1') 
	Set @sFolio = right('000000000000000' + @sFolio, 12) 
	Set @sMensaje = 'Información guardada satisfactoriamente' 
	
	
	If Not Exists ( Select * 
					From INT_RE_SIGHO__RecetasElectronicas_XML (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio_SIADISSEP = @Folio_SIADISSEP And uMedica = @UMedica ) 
		Begin 
			Insert Into INT_RE_SIGHO__RecetasElectronicas_XML 
			(
				IdEmpresa, IdEstado, IdFarmacia, Folio, FechaRegistro, UMedica, Folio_SIADISSEP, TipoDeProceso, 
				DisponibleSurtido, Surtidos, Surtidos_Aplicados, InformacionXML  
			)  
			Select 
				@IdEmpresa, @IdEstado, @IdFarmacia, @sFolio, getdate() as FechaRegistro, 
				@UMedica, @Folio_SIADISSEP, @TipoDeProceso, 1, 1, 0, @InformacionXML 
		End
	Else
		Begin
			Update INT_RE_SIGHO__RecetasElectronicas_XML Set FechaRegistro = getdate()
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio_SIADISSEP = @Folio_SIADISSEP And uMedica = @UMedica
		End
	
--------------- Salida Final 	
	Select 
		@IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, 
		@sFolio as Folio, @sMensaje as Mensaje  
	
End 
Go--#SQL 
	