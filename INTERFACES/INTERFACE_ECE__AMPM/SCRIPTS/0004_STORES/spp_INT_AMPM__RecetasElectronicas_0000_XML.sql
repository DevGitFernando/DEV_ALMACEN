-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_AMPM__RecetasElectronicas_001_XML' and xType = 'P' ) 
   Drop Proc spp_INT_AMPM__RecetasElectronicas_001_XML
Go--#SQL 

Create Proc spp_INT_AMPM__RecetasElectronicas_001_XML 
( 
	@Referencia_IdClinica varchar(100) = '0011', 
	@Folio_AMPM varchar(50) = '', 
	@InformacionXML_Base varchar(max) = '', 
	@InformacionXML varchar(max) = '',
	@TipoDeProceso smallint = 0, 
	@FolioGenerado varchar(12) = '' output   	
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
	Select @IdEmpresa = IdEmpresa, @IdEstado = IdEstado, @IdFarmacia = IdFarmacia 
	From INT_AMPM__CFG_FarmaciasClinicas (NoLock)
	Where --IdEstado = '09' and IdFarmacia = '0101' -- 
		Referencia_AMPM = @Referencia_IdClinica 
	
	Set @IdEmpresa = IsNull(@IdEmpresa, '') 
	Set @IdEstado = IsNull(@IdEstado, '') 
	Set @IdFarmacia = IsNull(@IdFarmacia, '') 
			


	Select @sFolio = cast(max(cast(Folio as int)) + 1 as varchar) 
	From INT_AMPM__RecetasElectronicas_XML (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
	
	Set @sFolio = IsNull(@sFolio, '1') 
	Set @sFolio = right('000000000000000' + @sFolio, 12) 
	Set @sMensaje = 'Información guardada satisfactoriamente' 
	
	
	If Not Exists ( Select * 
					From INT_AMPM__RecetasElectronicas_XML (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @sFolio ) 
	Begin 
		Insert Into INT_AMPM__RecetasElectronicas_XML 
		(
			IdEmpresa, IdEstado, IdFarmacia, Folio, FechaRegistro, UMedica, Folio_AMPM, TipoDeProceso, 
			DisponibleSurtido, Surtidos, Surtidos_Aplicados, InformacionXML_Base, InformacionXML  
		)  
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @sFolio, getdate() as FechaRegistro, 
			@Referencia_IdClinica, @Folio_AMPM, @TipoDeProceso, 1, 1, 0, @InformacionXML_Base, @InformacionXML 
	End 
	

------------------------------------------------------------
--------------- Salida Final 	
	Set @FolioGenerado = @sFolio 

	Select 
		@IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, 
		@sFolio as Folio, @sMensaje as Mensaje  
	

End 
Go--#SQL 
	