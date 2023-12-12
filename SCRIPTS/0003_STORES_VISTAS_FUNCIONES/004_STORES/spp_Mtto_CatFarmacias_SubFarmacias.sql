
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatFarmacias_SubFarmacias' and xType = 'P')
    Drop Proc spp_Mtto_CatFarmacias_SubFarmacias
Go--#SQL

Create Proc spp_Mtto_CatFarmacias_SubFarmacias ( @IdEstado varchar(2) = '25', @iOpcion smallint = 1 )
With Encryption 
As
Begin
Set NoCount On 

	Declare @sMensaje varchar(1000), 
			@sStatus varchar(1), 
			@iActualizado smallint,
			@sIdEstado varchar(2),
			@IdFarmacia varchar(4),
			@IdSubFarmacia varchar(2),
			@Descripcion varchar(50)

	/*Opciones*/
	/*
	   1.- Inserción/Actualización
	   2.- Cancelación(Eliminación Lógica)
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0

	Set @sIdEstado = ''
	Set @IdFarmacia = ''
	Set @IdSubFarmacia = ''
	Set @Descripcion = ''

	If @iOpcion = 1
		Begin
			-- Se obtienen las farmacias del Estado.
			Select E.IdEstado, F.IdFarmacia, E.IdSubFarmacia, E.Descripcion, 'A' as Status, 0 as Actualizado  
			Into #tmpSubFarmacias 
			From CatEstados_SubFarmacias E (NoLock)
			Inner Join CatFarmacias F(NoLock) On( E.IdEstado = F.IdEstado )
			Where E.IdEstado = @IdEstado and F.IdFarmacia <> '0001'
			Order By F.IdFarmacia, E.IdSubFarmacia 

			--  Select * 			from #tmpSubFarmacias 


			----- Actualizar los datos Existentes 
			Update S Set S.Descripcion = T.Descripcion, S.Status = T.Status, S.Actualizado = T.Actualizado 
			From CatFarmacias_SubFarmacias S (noLock) 
			Inner Join #tmpSubFarmacias T (NoLock) On ( S.IdEstado = T.IdEstado and S.IdFarmacia = T.IdFarmacia and S.IdSubFarmacia = T.IdSubFarmacia ) 

			----- Agregar las Farmacias-SubFarmacias faltantes 
			Insert Into CatFarmacias_SubFarmacias ( IdEstado, IdFarmacia, IdSubFarmacia, Descripcion, Status, Actualizado ) 
			Select T.IdEstado, T.IdFarmacia, T.IdSubFarmacia, T.Descripcion, T.Status, T.Actualizado 
			From #tmpSubFarmacias T (NoLock) 
			Where Not Exists ( Select * From CatFarmacias_SubFarmacias S (NoLock) 
							   Where S.IdEstado = T.IdEstado and S.IdFarmacia = T.IdFarmacia and S.IdSubFarmacia = T.IdSubFarmacia ) 


----			Declare tmpCol Cursor For Select IdEstado, IdFarmacia, IdSubFarmacia, Descripcion From #tmpSubFarmacias Order By IdFarmacia, IdSubFarmacia	
----				Open tmpCol
----				FETCH NEXT FROM tmpCol Into @sIdEstado, @IdFarmacia, @IdSubFarmacia, @Descripcion 
----					WHILE @@FETCH_STATUS = 0
----					BEGIN
----
----					If Not Exists ( Select * From  CatFarmacias_SubFarmacias (NoLock) Where IdEstado = @sIdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia )
----						Begin
----							Insert Into CatFarmacias_SubFarmacias ( IdEstado, IdFarmacia, IdSubFarmacia, Descripcion, Status, Actualizado )
----							Select @sIdEstado, @IdFarmacia, @IdSubFarmacia, @Descripcion, @sStatus, @iActualizado
----						End
----					Else
----						Begin
----							Update CatFarmacias_SubFarmacias Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
----							Where  IdEstado = @sIdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia
----						End					   
----			           	           
----					   FETCH NEXT FROM tmpCol Into @sIdEstado, @IdFarmacia, @IdSubFarmacia, @Descripcion  
----					END
----				Close tmpCol
----				Deallocate tmpCol 

----				Set @sMensaje = 'La información se guardó satisfactoriamente con la clave ' + @IdSubFarmacia
		End
	Else
		Begin			
			Set @sStatus = 'C'
			Update CatFarmacias_SubFarmacias Set Status = @sStatus, Actualizado = @iActualizado
			Where  IdEstado = @IdEstado 
			Set @sMensaje = 'La información de la SubFarmacias del Estado ' + @IdEstado + ' ha sido cancelada satisfactoriamente.'
		End

	-- Regresar la Clave Generada
    Select @IdSubFarmacia as Clave, @sMensaje as Mensaje 
End
Go--#SQL
	
