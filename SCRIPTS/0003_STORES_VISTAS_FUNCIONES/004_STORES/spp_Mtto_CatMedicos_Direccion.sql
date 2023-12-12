-----------------------------------------------------------------------------------------------------------------------
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatMedicos_Direccion' and xType = 'P')
    Drop Proc spp_Mtto_CatMedicos_Direccion
Go--#SQL
/*
	Exec spp_Mtto_CatMedicos_Direccion  @IdEstado = '09', @IdFarmacia = '0013', @IdMedico = '000002', @IdDireccion = '01', @Pais = 'BRASIL', @IdMunicipio = '0002', @IdColonia = '0002',  @Calle = 'desconocida', @NumeroExterior = '284', @NumeroInterior = '408', @CodigoPostal = '02008', @Referencia = '', @iOpcion = '1'
	SeleCt * From CatMedicos  Where IdEstado = '09' And IdFarmacia = '0013' And IdMedico = '000002'
*/

Create Proc spp_Mtto_CatMedicos_Direccion 
(	
	@IdEstado varchar(4) = '', @IdFarmacia varchar(6) = '', @IdMedico varchar(6) = '', 
	@IdDireccion varchar(2) = '*', @Pais varchar(100) = '', @IdMunicipio varchar(4) = '', @IdColonia varchar(4) = '', 
	@Calle varchar(100) = '', @NumeroExterior varchar(20) = '', @NumeroInterior varchar(20) = '', 
	@CodigoPostal varchar(10) = '', @Referencia varchar(100) = '', 
	@iOpcion smallint 	
)
With Encryption 
As
Begin 
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado tinyint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdDireccion = '*' 
	Begin 
	   Select @IdDireccion = cast( (max(IdDireccion) + 1) as varchar)  
	   From CatMedicos_Direccion (NoLock) 
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdMedico = @IdMedico and IdDireccion = @IdDireccion 
	End 

	-- Asegurar que IdEstado sea valido y formatear la cadena 
	Set @IdDireccion = IsNull(@IdDireccion, '1')
	Set @IdDireccion = right(replicate('0', 2) + @IdDireccion, 2)			

	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CatMedicos_Direccion (NoLock) 
				Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdMedico = @IdMedico and IdDireccion = @IdDireccion ) 
			  Begin 
				 Insert Into CatMedicos_Direccion ( IdEstado, IdFarmacia, IdMedico, IdDireccion, Pais, IdMunicipio, IdColonia, Calle, 
					NumeroExterior, NumeroInterior, CodigoPostal, Referencia, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @IdMedico, @IdDireccion, @Pais, @IdMunicipio, @IdColonia, @Calle, 
					@NumeroExterior, @NumeroInterior, @CodigoPostal, @Referencia, @sStatus, @iActualizado 
				 Select @IdEstado, @IdFarmacia, @IdMedico, @IdDireccion, @Pais, @IdMunicipio, @IdColonia, @Calle, 
					@NumeroExterior, @NumeroInterior, @CodigoPostal, @Referencia, @sStatus, @iActualizado 

              End  
		   Else 
			  Begin 
			     Update CatMedicos_Direccion Set 
					Pais = @Pais, IdMunicipio = @IdMunicipio, IdColonia = @IdColonia, 
					Calle = @Calle, NumeroExterior = @NumeroExterior, NumeroInterior = @NumeroInterior, CodigoPostal = @CodigoPostal, 
					Referencia = @Referencia, 
					Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdMedico = @IdMedico and IdDireccion = @IdDireccion  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente'  
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update CatMedicos_Direccion Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdMedico = @IdMedico and IdDireccion = @IdDireccion 
			Set @sMensaje = 'La información ha sido cancelada satisfactoriamente.' 
	   End 


	-- Regresar la Clave Generada
    --Select @id as Folio, @sMensaje as Mensaje 
    
End
Go--#SQL