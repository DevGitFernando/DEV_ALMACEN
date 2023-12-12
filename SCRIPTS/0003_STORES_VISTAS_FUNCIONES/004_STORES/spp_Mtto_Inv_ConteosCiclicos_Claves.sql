


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Inv_ConteosCiclicos_Claves' And xType = 'P' )
	Drop Proc spp_Mtto_Inv_ConteosCiclicos_Claves
Go--#SQL

		----		Exec spp_Mtto_Inv_ConteosCiclicos_Claves '001', '11', '0005', '00000001', '010.000.0022.00'

Create Procedure spp_Mtto_Inv_ConteosCiclicos_Claves 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0005', 
	@FolioConteo varchar(30) = '00000001', @ClaveSSA varchar(30) = '010.000.0022.00', @Categoria varchar(3) = 'A'
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
	
	
	If Not Exists ( Select * From Inv_ConteosCiclicos_Claves  Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
					and FolioConteo = @FolioConteo and ClaveSSA = @ClaveSSA )
	Begin
		Insert Into Inv_ConteosCiclicos_Claves  ( IdEmpresa, IdEstado, IdFarmacia, FolioConteo, ClaveSSA, FechaRegistro, Categoria, Status, Actualizado )
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioConteo, @ClaveSSA, GetDate(), @Categoria, @Status, @Actualizado
		
		Set @sMensaje = 'La información del Folio ' + @FolioConteo + ' se guardo exitosamente'
	End		
	
	-- Regresar la Clave Generada
    Select @FolioConteo as Folio, @sMensaje as Mensaje 

End
Go--#SQL


