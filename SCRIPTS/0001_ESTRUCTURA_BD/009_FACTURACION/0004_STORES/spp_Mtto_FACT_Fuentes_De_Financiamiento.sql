---------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_Fuentes_De_Financiamiento' and xType = 'P' )
    Drop Proc spp_Mtto_FACT_Fuentes_De_Financiamiento
Go--#SQL 
  
Create Proc spp_Mtto_FACT_Fuentes_De_Financiamiento 
( 
	@IdFuenteFinanciamiento varchar(4) = '*', @IdEstado varchar(2) = '21', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005', 
	@NumeroDeContrato varchar(100) = '', @NumeroDeLicitacion varchar(100) = '', 
	@FechaInicial varchar(10) = '2012-01-01', @FechaFinal varchar(10) = '2012-12-01', 
	@iMonto numeric(14,4) = 100.0000, @iPiezas int = 10, @iOpcion smallint = 1, 
	@TipoClasificacionSSA int = 0, @Alias varchar(50) = '', @EsParaExcedente smallint = 0, 
	@EsDiferencial smallint = 0, @TipoDeUnidades smallint = 0
)
With Encryption 
As 
Begin 
Set NoCount On
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set DateFormat YMD
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdFuenteFinanciamiento = '*' 
	  Begin
	   Select @IdFuenteFinanciamiento = cast( (max(IdFuenteFinanciamiento) + 1) as varchar) From FACT_Fuentes_De_Financiamiento (NoLock) 
	  End 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdFuenteFinanciamiento = IsNull(@IdFuenteFinanciamiento, '1')
	Set @IdFuenteFinanciamiento = right(replicate('0', 4) + @IdFuenteFinanciamiento, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From FACT_Fuentes_De_Financiamiento (NoLock) Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento ) 
			  Begin 
				 Insert Into FACT_Fuentes_De_Financiamiento ( IdFuenteFinanciamiento, IdEstado, IdCliente, IdSubCliente, NumeroDeContrato, NumeroDeLicitacion, 
					FechaInicial, FechaFinal, Monto, Piezas, Status, Actualizado, TipoClasificacionSSA, Alias, EsParaExcedente, EsDiferencial, TipoDeUnidades ) 
				 Select @IdFuenteFinanciamiento, @IdEstado, @IdCliente, @IdSubCliente, @NumeroDeContrato, @NumeroDeLicitacion, 
					@FechaInicial, @FechaFinal, @iMonto, @iPiezas, @sStatus, @iActualizado, @TipoClasificacionSSA, @Alias, @EsParaExcedente, @EsDiferencial, @TipoDeUnidades 
              End 
		   Else 
			  Begin 
			     Update FACT_Fuentes_De_Financiamiento Set IdEstado = @IdEstado, IdCliente = @IdCliente, IdSubCliente = @IdSubCliente, 
						NumeroDeContrato = @NumeroDeContrato, NumeroDeLicitacion = @NumeroDeLicitacion,
						FechaInicial = @FechaInicial, FechaFinal = @FechaFinal, Monto = @iMonto, Piezas = @iPiezas, 
						Status = @sStatus, Actualizado = @iActualizado, 
						Alias = @Alias, EsParaExcedente = @EsParaExcedente,  EsDiferencial = @EsDiferencial, 
						TipoClasificacionSSA = TipoClasificacionSSA, ---- Este valor no cambia, solo se asigna al momento de crear el registro 
						@TipoDeUnidades = @TipoDeUnidades 
				 Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @IdFuenteFinanciamiento 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update FACT_Fuentes_De_Financiamiento Set Status = @sStatus, Actualizado = @iActualizado Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento 
		   Set @sMensaje = 'La información del Financiamiento ' + @IdFuenteFinanciamiento + ' ha sido cancelada satisfactoriamente.' 
	   End 


--- Aplicar cambios a la Administración 
	Exec spp_Mtto_FACT_Fuentes_De_Financiamiento_Admon @IdFuenteFinanciamiento, @IdEstado, 
		@IdCliente, @IdSubCliente, @FechaInicial, @FechaFinal,@iMonto, @iPiezas, @iOpcion, 0 
	

	-- Regresar la Clave Generada  
    Select @IdFuenteFinanciamiento as Folio, @sMensaje as Mensaje  

End 
Go--#SQL
