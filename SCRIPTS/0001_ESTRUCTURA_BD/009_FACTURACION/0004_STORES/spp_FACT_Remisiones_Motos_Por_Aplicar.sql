
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_FACT_Remisiones_Motos_Por_Aplicar' and xType = 'P')
    Drop Proc spp_FACT_Remisiones_Motos_Por_Aplicar
Go--#SQL
  
Create Proc spp_FACT_Remisiones_Motos_Por_Aplicar ( @IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '0001' )
With Encryption 
As
Begin
Set NoCount On
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint, 
		@iMonto_Rubro numeric(14,4),
		@iMonto_Concepto numeric(14,4),
		@iDisponible_Rubro numeric(14,4),
		@iDisponible_Concepto numeric(14,4),
		@iAplicado_Rubro numeric(14,4),
		@iAplicado_Concepto numeric(14,4),
		@sSql varchar(8000)

	Set DateFormat YMD
	Set @iMonto_Rubro = 0.0000
	Set @iMonto_Concepto = 0.0000
	Set @iDisponible_Rubro = 0.0000
	Set @iDisponible_Concepto = 0.0000
	Set @iAplicado_Rubro = 0.0000
	Set @iAplicado_Concepto = 0.0000
	Set @sSql = ''

	----------------------------------------------------------
	-- Se obtiene el Monto de la Fuente y el Financiamiento --
	----------------------------------------------------------
	Select @iMonto_Rubro = IsNull(Monto, 0 )
	From FACT_Fuentes_De_Financiamiento(NoLock) 
	Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento

	Select @iMonto_Concepto = IsNull(Monto, 0 ) 
	From FACT_Fuentes_De_Financiamiento_Detalles(NoLock) 
	Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento

	--------------------------------------------------------------
	-- Se obtienen los montos aplicados del Rubro y el Concepto --
	--------------------------------------------------------------
	Select @iAplicado_Rubro = IsNull( Sum( Total ), 0 ) 
	From FACT_Remisiones(NoLock) 
	Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento

	Select @iAplicado_Concepto = IsNull( Sum( Total ), 0 ) 
	From FACT_Remisiones(NoLock) 
	Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento And IdFinanciamiento = @IdFinanciamiento 

	---------------------------------------------------------------------
	-- Se Calcula el Monto Disponible de la Fuente y el Financiamiento --
	---------------------------------------------------------------------
	Select @iDisponible_Rubro = ( @iMonto_Rubro - @iAplicado_Rubro )
	Select @iDisponible_Concepto = ( @iMonto_Concepto - @iAplicado_Concepto )
	
	----------------------------
	-- Se devuelven los datos --
	----------------------------
	Select @iDisponible_Rubro as Disponible_Rubro, @iDisponible_Concepto as Disponible_Concepto 


End
Go--#SQL
