


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_Adt_Rechazos_Enc' And xType = 'P' )
	Drop Proc spp_Mtto_COM_Adt_Rechazos_Enc
Go--#SQL

		----		Exec spp_Mtto_COM_Adt_Rechazos_Enc '001', '21', '2182', '*', '00000001', '0001', 'PROVEEDOR', 'OBSERVACIONES', 1

Create Procedure spp_Mtto_COM_Adt_Rechazos_Enc 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2182', 
	@FolioRechazo varchar(30) = '*', @FolioOrden varchar(30) = '00000001', @IdPersonal varchar(4) = '0001',
	@NombreRecibeRechazo varchar(100) = 'PROVEEDOR', @Observaciones varchar(200) = 'OBSERVACIONES', 
 	@TipoProceso tinyint = 0, @FechaResurtido datetime = '2015-01-01', @Opcion tinyint = 1
)
With Encryption 
As
Begin
	Declare @Status varchar(1), 
			@Actualizado int,
			@sMensaje varchar(8000)

	Set @Status = 'A'
	Set @Actualizado = 0
	Set @sMensaje = ''
	
	If @FolioRechazo = '*' 
	   Select @FolioRechazo = cast( (max(FolioRechazo) + 1) as varchar)  From COM_Adt_Rechazos_Enc (NoLock) 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia

	-- Asegurar que @FolioRechazo sea valido y formatear la cadena 
	Set @FolioRechazo = IsNull(@FolioRechazo, '1')
	Set @FolioRechazo = right(replicate('0', 8) + @FolioRechazo, 8)
	
	If @Opcion = 1
	Begin
		If Not Exists ( Select * From COM_Adt_Rechazos_Enc  Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
						and FolioRechazo = @FolioRechazo )
		Begin
			Insert Into COM_Adt_Rechazos_Enc  ( IdEmpresa, IdEstado, IdFarmacia, FolioRechazo, FolioOrden, IdPersonal, FechaRegistro, 
								NombreRecibeRechazo, Observaciones, TipoProceso, FechaResurtido, Status, Actualizado )
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioRechazo, @FolioOrden, @IdPersonal, GetDate(), 
			@NombreRecibeRechazo, @Observaciones, @TipoProceso, @FechaResurtido, @Status, @Actualizado
			
			Set @sMensaje = 'La información del Folio ' + @FolioRechazo + ' se guardo exitosamente'
		End		
	End
	Else
	Begin
		Set @Status = 'C'
		Update COM_Adt_Rechazos_Enc Set Status = @Status, Actualizado = @Actualizado
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioRechazo = @FolioRechazo
		
		Set @sMensaje = 'La información del Folio ' + @FolioRechazo + ' se cancelo exitosamente'
		
	End	
	 
	-- Regresar la Clave Generada
    Select @FolioRechazo as Folio, @sMensaje as Mensaje 

End
Go--#SQL


