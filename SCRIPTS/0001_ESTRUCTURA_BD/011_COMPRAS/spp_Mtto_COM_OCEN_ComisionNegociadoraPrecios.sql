
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_ComisionNegociadoraPrecios' And xType = 'P' )
	Drop Proc spp_Mtto_COM_OCEN_ComisionNegociadoraPrecios 
Go--#SQL

Create Procedure spp_Mtto_COM_OCEN_ComisionNegociadoraPrecios ( @IdClaveSSA varchar(4), @IdLaboratorio varchar(4), @Precio Numeric(14,4), @iOpcion int )
As
Begin
	Declare
	  @Actualizado tinyint,
	  @sMensaje varchar(1000),
	  @Status varchar(1)

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar 
	*/

	Set @Actualizado = 0
	Set @sMensaje = ''
	Set @Status = 'A'

	if @iOpcion = 1 
	  Begin
		-- Se guarda la informacion en la tabla spp_Mtto_COM_OCEN_ComisionNegociadoraPrecios
		If Not Exists ( Select * From COM_OCEN_ComisionNegociadoraPrecios (NoLock) 
						Where IdClaveSSA_Sal = @IdClaveSSA And IdLaboratorio = @IdLaboratorio )
		  Begin
			Insert Into COM_OCEN_ComisionNegociadoraPrecios ( IdClaveSSA_Sal, IdLaboratorio, Precio, Status, Actualizado )
			Select @IdClaveSSA, @IdLaboratorio, @Precio, @Status, @Actualizado
		  End
		Else
		  Begin
			Update COM_OCEN_ComisionNegociadoraPrecios 
			Set Precio = @Precio, Status = @Status, Actualizado = @Actualizado
			Where IdClaveSSA_Sal = @IdClaveSSA And IdLaboratorio = @IdLaboratorio
		  End

		Set @sMensaje = 'El precio de la Clave - Laboratorio ' + @IdClaveSSA + ' - ' + @IdLaboratorio + ' se guardo satisfactoriamente. ' 
	  End
	Else
	  Begin
		Set @Status = 'C'
		Update COM_OCEN_ComisionNegociadoraPrecios Set Status = @Status, Actualizado = @Actualizado
		Where IdClaveSSA_Sal = @IdClaveSSA And IdLaboratorio = @IdLaboratorio

		Set @sMensaje = 'El precio de la Clave - Laboratorio ' + @IdClaveSSA + ' - ' + @IdLaboratorio + ' ha sido cancelada satisfactoriamente.'
	  End 
	  
	-- Se guarda la informacion en la tabla COM_OCEN_ComisionNegociadoraPrecios_Historico
	 
	Insert Into COM_OCEN_ComisionNegociadoraPrecios_Historico ( IdClaveSSA_Sal, IdLaboratorio, Precio, Status ) 
	Select IdClaveSSA_Sal, IdLaboratorio, Precio, Status
	From COM_OCEN_ComisionNegociadoraPrecios (Nolock)
	Where IdClaveSSA_Sal = @IdClaveSSA And IdLaboratorio = @IdLaboratorio
			
	Select @sMensaje as Mensaje  
	
End
Go--#SQL
