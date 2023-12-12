------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From SysObjects (NoLock) Where Name = 'spp_Mtto_RegistrosSanitarios' And xType = 'P' )
	Drop Proc spp_Mtto_RegistrosSanitarios
Go--#SQL 

Create Proc spp_Mtto_RegistrosSanitarios 
( 
	@Folio Varchar(8), @IdLaboratorio Varchar(4), @ClaveSSA Varchar(50), @Consecutivo varchar(4), @Tipo varchar(3), @Año varchar(4),
	@Vigencia DateTime, @Status varchar(4), @Documento varchar(max), @NombreDocto varchar(200), @iOpcion smallint, 
	@IdPaisFabricacion varchar(3) = '000', @IdPresentacion varchar(3) = '000', @TipoCaducidad smallint = 0, @Caducidad smallint = 0, 
	@MD5 varchar(100) = '', @FolioRegistroSanitario Varchar(30)
) 
With Encryption 
As
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare 
	@Mensaje varchar(1000),
	@Actualizado smallint,   
	@FechaVigencia datetime,  
	@sVigencia varchar(100),
	@IdClaveSSA_Sal Varchar(4), 
	@dValorSNK numeric(14,4) 

	/*Opciones
	  Opcion 1.- Insercion / Actualizacion*/ 
	  

	-- Set @dValorSNK = 1.0 / 0.0  ----- Asegurar que no registren nueva informacion hasta que se actualicen los clientes


	 If (@Folio = '*') 
		Begin
			Select @Folio = Max(Folio) + 1 From CatRegistrosSanitarios (NoLock)
			Set @Folio = ISNULL(@Folio, 1)
			Set @Folio = RIGHT('00000000' + @Folio, 8)
		End
		
	Select @IdClaveSSA_Sal = IdClaveSSA_Sal From CatClavesSSA_Sales (NoLock) Where ClaveSSA = @ClaveSSA


	Set @Mensaje = ''	
	Set @Actualizado = 0 
	Set @sVigencia = cast(year(@Vigencia) as varchar) + '-' + right('00' + cast(month(@Vigencia) as varchar), 2) + '-01' 
	Set @FechaVigencia = cast(@sVigencia as datetime) 


	If @MD5 = '' 
	Begin 
	   Set @MD5 = cast(newid() as varchar(100))
	End 

	Set @MD5 = upper(@MD5) 
	If @iOpcion = 1 
    Begin 
		If Not Exists ( Select * 
						From CatRegistrosSanitarios (NoLock)
						Where IdLaboratorio = @IdLaboratorio And IdClaveSSA_Sal = @IdClaveSSA_Sal )-- And Consecutivo = @Consecutivo And Tipo = @Tipo And Año = @Año) 
			Begin
			 
				Insert Into CatRegistrosSanitarios 
				( 
					Folio, IdLaboratorio, IdClaveSSA_Sal, Consecutivo, Tipo, Año, FechaVigencia, Status, NombreDocto, MD5, Documento, Actualizado, 
					IdPaisFabricacion, IdPresentacion, TipoCaducidad, Caducidad, FolioRegistroSanitario
				)
				Select  
					@Folio, @IdLaboratorio, @IdClaveSSA_Sal, @Consecutivo, @Tipo, @Año, @FechaVigencia, @Status, @NombreDocto, @MD5, '', @Actualizado, 
					@IdPaisFabricacion, @IdPresentacion, @TipoCaducidad, @Caducidad, @FolioRegistroSanitario 


				Insert Into SII_OficinaCentral__RegistrosSanitarios..CatRegistrosSanitarios 
				( 
					Folio, IdLaboratorio, IdClaveSSA_Sal, Consecutivo, Tipo, Año, FechaVigencia, Status, NombreDocto, MD5, Documento, Actualizado, 
					IdPaisFabricacion, IdPresentacion, TipoCaducidad, Caducidad, FolioRegistroSanitario
				)
				Select  
					@Folio, @IdLaboratorio, @IdClaveSSA_Sal, @Consecutivo, @Tipo, @Año, @FechaVigencia, @Status, @NombreDocto, @MD5, @Documento, @Actualizado, 
					@IdPaisFabricacion, @IdPresentacion, @TipoCaducidad, @Caducidad, @FolioRegistroSanitario 

			End 
        Else
			Begin
				Update CatRegistrosSanitarios 
					Set FechaUltimaActualizacion = getdate(), 
						FechaVigencia = @FechaVigencia, Status = @Status, Actualizado = @Actualizado,
						Consecutivo = @Consecutivo, Tipo = @Tipo, Año = @Año,  
						IdPaisFabricacion = @IdPaisFabricacion, IdPresentacion = @IdPresentacion, TipoCaducidad = @TipoCaducidad, Caducidad = @Caducidad,
						FolioRegistroSanitario = @FolioRegistroSanitario 
				Where  IdLaboratorio = @IdLaboratorio And IdClaveSSA_Sal = @IdClaveSSA_Sal -- And Consecuti@Caducidadvo = @Consecutivo And Tipo = @Tipo And Año = @Año
			  
				If (@Documento <> '')
				Begin 

					Update CatRegistrosSanitarios 
					Set MD5 = @MD5, Documento = '', NombreDocto = @NombreDocto
					Where  IdLaboratorio = @IdLaboratorio And IdClaveSSA_Sal = @IdClaveSSA_Sal -- And Consecuti@Caducidadvo = @Consecutivo And Tipo = @Tipo And Año = @Año 

					Update SII_OficinaCentral__RegistrosSanitarios..CatRegistrosSanitarios 
					Set MD5 = @MD5, Documento = @Documento, NombreDocto = @NombreDocto
					Where  IdLaboratorio = @IdLaboratorio And IdClaveSSA_Sal = @IdClaveSSA_Sal -- And Consecuti@Caducidadvo = @Consecutivo And Tipo = @Tipo And Año = @Año  

				End	

			End 
		   
			Update CatRegistrosSanitarios_CodigoEAN Set Status = 'C' Where  Folio = @Folio	
			----Update SII_OficinaCentral__RegistrosSanitarios..CatRegistrosSanitarios_CodigoEAN Set Status = 'C' Where  Folio = @Folio	

			Set @Mensaje = 'La información se guardo satisfactoriamente' 

	End 
	
	-- Regresar la Clave Generada
    Select  @Folio As Folio, @Mensaje as Mensaje
	
End 
Go--#SQL 

