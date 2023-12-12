-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0000_XML__Log' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0000_XML__Log
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0000_XML__Log 
( 
	@UMedica varchar(50) = '', 
	@Folio_SESEQ varchar(50) = '', 
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
	Select @IdEmpresa = IdEmpresa, @IdEstado = IdEstado, @IdFarmacia = IdFarmacia 
	From INT_SESEQ__CFG_Farmacias_UMedicas (NoLock)
	Where Referencia_SESEQ = @UMedica 
	

	Select @sFolio = cast(max(cast(Folio as int)) + 1 as varchar) 
	From INT_SESEQ__RecetasElectronicas_XML__Log (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
	
	Set @sFolio = IsNull(@sFolio, '1') 
	Set @sFolio = right('000000000000000' + @sFolio, 12) 
	Set @sMensaje = 'Información guardada satisfactoriamente' 
	
	
	If Not Exists ( Select * 
					From INT_SESEQ__RecetasElectronicas_XML__Log (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @sFolio ) 
	Begin 
		Insert Into INT_SESEQ__RecetasElectronicas_XML__Log 
		(
			IdEmpresa, IdEstado, IdFarmacia, Folio, FechaRegistro, UMedica, Folio_SESEQ, TipoDeProceso, 
			DisponibleSurtido, Surtidos, Surtidos_Aplicados, InformacionXML  
		)  
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @sFolio, getdate() as FechaRegistro, 
			@UMedica, @Folio_SESEQ, @TipoDeProceso, 1, 1, 0, @InformacionXML 
	End 
	
--------------- Salida Final 	
	Select 
		@IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, 
		@sFolio as Folio, @sMensaje as Mensaje  
	
End 
Go--#SQL 
	