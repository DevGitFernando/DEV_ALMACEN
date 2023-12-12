--------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Establecimientos' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_Establecimientos 
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_Establecimientos 
(
	@TipoDeEstablecimiento int = 0, 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0001', 
	@IdCliente varchar(6) = '', 
	@IdEstablecimiento varchar(6) = '', @NombreEstablecimiento varchar(500) = '', 
	@Pais varchar(100) = '', @Estado varchar(100) = '', @Municipio varchar(100) = '', @Localidad varchar(100) = '', 
	@Colonia varchar(100) = '', @Calle varchar(100) = '', @NoExterior varchar(8) = '', @NoInterior varchar(8) = '', 
	@CodigoPostal varchar(100) = '', @Referencia varchar(100) = '', @Opcion int = 1 
) 
As 
Begin 
Set NoCount On 


	If @TipoDeEstablecimiento =  1 
	Begin 
		Exec spp_Mtto_FACT_CFD_Establecimientos_Emisor 
			@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, 
			@IdEstablecimiento = @IdEstablecimiento, @NombreEstablecimiento = @NombreEstablecimiento, 
			@Pais = @Pais, @Estado = @Estado, @Municipio = @Municipio, @Localidad = @Localidad, @Colonia = @Colonia, 
			@Calle = @Calle, @NoExterior = @NoExterior, @NoInterior = @NoInterior, @CodigoPostal = @CodigoPostal, @Referencia = @Referencia, 
			@Opcion = @Opcion
	End 
	

	If @TipoDeEstablecimiento =  2 
	Begin 
		Exec spp_Mtto_FACT_CFD_Establecimientos_Receptores 
			@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, 
			@IdCliente = @IdCliente, @IdEstablecimiento = @IdEstablecimiento, @NombreEstablecimiento = @NombreEstablecimiento, 
			@Pais = @Pais, @Estado = @Estado, @Municipio = @Municipio, @Localidad = @Localidad, @Colonia = @Colonia, 
			@Calle = @Calle, @NoExterior = @NoExterior, @NoInterior = @NoInterior, @CodigoPostal = @CodigoPostal, @Referencia = @Referencia, 
			@Opcion = @Opcion
	End 	


End 
Go--#SQL 





--------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Establecimientos_Emisor' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_Establecimientos_Emisor 
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_Establecimientos_Emisor 
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0001', 
	@IdEstablecimiento varchar(6) = '', @NombreEstablecimiento varchar(500) = '', 
	@Pais varchar(100) = '', @Estado varchar(100) = '', @Municipio varchar(100) = '', @Localidad varchar(100) = '', 
	@Colonia varchar(100) = '', @Calle varchar(100) = '', @NoExterior varchar(8) = '', @NoInterior varchar(8) = '', 
	@CodigoPostal varchar(100) = '', @Referencia varchar(100) = '', @Opcion int = 1 
) 
As 
Begin 
Set NoCount On 
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdEstablecimiento = '*'
		Begin
			Select @IdEstablecimiento = cast( (max(IdEstablecimiento) + 1) as varchar)  
			From FACT_CFD_Establecimientos (NoLock)
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		End 

	Set @IdEstablecimiento = IsNull(@IdEstablecimiento, '1')
	Set @IdEstablecimiento = dbo.fg_FormatearCadena(@IdEstablecimiento, '0', 6)


	If @Opcion = 1 
       Begin
		   If Not Exists ( Select * From FACT_CFD_Establecimientos (NoLock) 
							Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdEstablecimiento =  @IdEstablecimiento ) 
			  Begin 
				Insert Into FACT_CFD_Establecimientos 
				( 
					IdEmpresa, IdEstado, IdFarmacia, IdEstablecimiento, NombreEstablecimiento, 
					Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia 
				)  
				Select 
					@IdEmpresa, @IdEstado, @IdFarmacia, @IdEstablecimiento, @NombreEstablecimiento, 
					@Pais, @Estado, @Municipio, @Localidad, @Colonia, @Calle, @NoExterior, @NoInterior, @CodigoPostal, @Referencia 
              End
		   Else 
			  Begin 
			     Update FACT_CFD_Establecimientos 
					Set 
						NombreEstablecimiento = @NombreEstablecimiento, 
						Pais = @Pais, Estado = @Estado, Municipio = @Municipio, Localidad = @Municipio, Colonia = @Colonia, 
						Calle = @Calle, NoExterior = @NoExterior, NoInterior = @NoInterior, CodigoPostal = @CodigoPostal, Referencia = @Referencia 
			     Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdEstablecimiento =  @IdEstablecimiento
              End 
			Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdEstablecimiento 
	   End
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update FACT_CFD_Establecimientos Set Status = @sStatus 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdEstablecimiento =  @IdEstablecimiento  
	   End 

	---- Regresar la Clave Generada
    Select @IdEstablecimiento as Clave, @sMensaje as Mensaje 
	

End 
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Establecimientos_Receptores' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_Establecimientos_Receptores 
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_Establecimientos_Receptores 
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0001', 
	@IdCliente varchar(6) = '', 
	@IdEstablecimiento varchar(6) = '', @NombreEstablecimiento varchar(500) = '', 
	@Pais varchar(100) = '', @Estado varchar(100) = '', @Municipio varchar(100) = '', @Localidad varchar(100) = '', 
	@Colonia varchar(100) = '', @Calle varchar(100) = '', @NoExterior varchar(8) = '', @NoInterior varchar(8) = '', 
	@CodigoPostal varchar(100) = '', @Referencia varchar(100) = '', @Opcion int = 1 
) 
As 
Begin 
Set NoCount On 
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdEstablecimiento = '*'
		Begin
			Select @IdEstablecimiento = cast( (max(IdEstablecimiento) + 1) as varchar)  
			From FACT_CFD_Establecimientos_Receptor (NoLock)
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia --and IdCliente = @IdCliente 
		End 

	Set @IdEstablecimiento = IsNull(@IdEstablecimiento, '1')
	Set @IdEstablecimiento = dbo.fg_FormatearCadena(@IdEstablecimiento, '0', 6)


	If @Opcion = 1 
       Begin
		   If Not Exists ( Select * From FACT_CFD_Establecimientos_Receptor (NoLock) 
							Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdEstablecimiento =  @IdEstablecimiento --and IdCliente = @IdCliente 
						 ) 
			  Begin 
				Insert Into FACT_CFD_Establecimientos_Receptor 
				( 
					IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdEstablecimiento, NombreEstablecimiento, 
					Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia 
				)  
				Select 
					@IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdEstablecimiento, @NombreEstablecimiento, 
					@Pais, @Estado, @Municipio, @Localidad, @Colonia, @Calle, @NoExterior, @NoInterior, @CodigoPostal, @Referencia 
              End
		   Else 
			  Begin 
			     Update FACT_CFD_Establecimientos_Receptor 
					Set 
						NombreEstablecimiento = @NombreEstablecimiento, 
						Pais = @Pais, Estado = @Estado, Municipio = @Municipio, Localidad = @Municipio, Colonia = @Colonia, 
						Calle = @Calle, NoExterior = @NoExterior, NoInterior = @NoInterior, CodigoPostal = @CodigoPostal, Referencia = @Referencia 
			     Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
						-- and IdCliente = @IdCliente 
						and IdEstablecimiento =  @IdEstablecimiento
              End 
			Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdEstablecimiento 
	   End
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update FACT_CFD_Establecimientos_Receptor Set Status = @sStatus 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				--and IdCliente = @IdCliente 
				and IdEstablecimiento =  @IdEstablecimiento  
	   End 

	---- Regresar la Clave Generada
    Select @IdEstablecimiento as Clave, @sMensaje as Mensaje 
	

End 
Go--#SQL 

