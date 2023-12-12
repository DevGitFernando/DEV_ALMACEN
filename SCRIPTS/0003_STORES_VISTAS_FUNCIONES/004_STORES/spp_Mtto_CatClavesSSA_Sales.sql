If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatClavesSSA_Sales' and xType = 'P')
    Drop Proc spp_Mtto_CatClavesSSA_Sales
Go--#SQL
  
Create Proc spp_Mtto_CatClavesSSA_Sales ( @IdClaveSSA_Sal varchar(4), @ClaveSSA varchar(52), 
	@Descripcion varchar(7500), @DescripcionCortaClave varchar(30), @IdGrupoTerapeutico varchar(3), @TipoCatalogo varchar(2), 
	@IdPresentacion varchar(3), @EsControlado smallint, @EsAntibiotico smallint, @ContenidoPaquete int, @ClaveSSA_Base varchar(50),
	@IdPersonal varchar(4), @IdTipoProducto varchar(2), @iOpcion smallint )
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

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdClaveSSA_Sal = '*' 
	   Select @IdClaveSSA_Sal = cast( (max(IdClaveSSA_Sal) + 1) as varchar)  From CatClavesSSA_Sales (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdClaveSSA_Sal = IsNull(@IdClaveSSA_Sal, '1')
	Set @IdClaveSSA_Sal = right(replicate('0', 4) + @IdClaveSSA_Sal, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatClavesSSA_Sales (NoLock) Where IdClaveSSA_Sal = @IdClaveSSA_Sal ) 
			  Begin 
				 Insert Into CatClavesSSA_Sales ( IdClaveSSA_Sal, ClaveSSA, Descripcion, DescripcionCortaClave, 
													IdGrupoTerapeutico, TipoCatalogo, Status, Actualizado,
													IdPresentacion, EsControlado, EsAntibiotico, ContenidoPaquete, ClaveSSA_Base, IdTipoProducto ) 
				 Select @IdClaveSSA_Sal, @ClaveSSA , @Descripcion, @DescripcionCortaClave, @IdGrupoTerapeutico, @TipoCatalogo, @sStatus, @iActualizado,
						@IdPresentacion, @EsControlado, @EsAntibiotico, @ContenidoPaquete, @ClaveSSA_Base, @IdTipoProducto 


				 Insert Into CatClavesSSA_Sales_Historico ( IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, Descripcion, DescripcionCortaClave, 
													IdGrupoTerapeutico, TipoCatalogo, IdPresentacion, ContenidoPaquete, EsControlado, 
													EsAntibiotico, IdPersonal, FechaRegistro, Status, Actualizado ) 
				 Select @IdClaveSSA_Sal, @ClaveSSA_Base, @ClaveSSA , @Descripcion, @DescripcionCortaClave, @IdGrupoTerapeutico, @TipoCatalogo, 
						@IdPresentacion, @ContenidoPaquete, @EsControlado, @EsAntibiotico, @IdPersonal, GetDate(), @sStatus, @iActualizado

              End 
		   Else 
			  Begin 
			     Update CatClavesSSA_Sales Set ClaveSSA  = @ClaveSSA , Descripcion = @Descripcion, DescripcionCortaClave = @DescripcionCortaClave,
					IdGrupoTerapeutico = @IdGrupoTerapeutico, TipoCatalogo = @TipoCatalogo, 
					Status = @sStatus, Actualizado = @iActualizado, IdPresentacion = @IdPresentacion, EsControlado = @EsControlado, 
					EsAntibiotico = @EsAntibiotico, ContenidoPaquete = @ContenidoPaquete, ClaveSSA_Base = @ClaveSSA_Base, IdTipoProducto = @IdTipoProducto
				 Where IdClaveSSA_Sal = @IdClaveSSA_Sal

				Insert Into CatClavesSSA_Sales_Historico ( IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, Descripcion, DescripcionCortaClave, 
													IdGrupoTerapeutico, TipoCatalogo, IdPresentacion, ContenidoPaquete, EsControlado, 
													EsAntibiotico, IdPersonal, FechaRegistro, Status, Actualizado ) 
				 Select @IdClaveSSA_Sal, @ClaveSSA_Base, @ClaveSSA , @Descripcion, @DescripcionCortaClave, @IdGrupoTerapeutico, @TipoCatalogo, 
						@IdPresentacion, @ContenidoPaquete, @EsControlado, @EsAntibiotico, @IdPersonal, GetDate(), @sStatus, @iActualizado
				  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdClaveSSA_Sal 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatClavesSSA_Sales Set Status = @sStatus, Actualizado = @iActualizado Where IdClaveSSA_Sal = @IdClaveSSA_Sal 
		   Set @sMensaje = 'La información de la ClaveSSA Sal ' + @IdClaveSSA_Sal + ' ha sido cancelada satisfactoriamente.'

			Insert Into CatClavesSSA_Sales_Historico ( IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, Descripcion, DescripcionCortaClave, 
													IdGrupoTerapeutico, TipoCatalogo, IdPresentacion, ContenidoPaquete, EsControlado, 
													EsAntibiotico, IdPersonal, FechaRegistro, Status, Actualizado ) 
				 Select IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA , Descripcion, DescripcionCortaClave, IdGrupoTerapeutico, TipoCatalogo, 
						IdPresentacion, ContenidoPaquete, EsControlado, EsAntibiotico, @IdPersonal, GetDate(), @sStatus, @iActualizado
				 From CatClavesSSA_Sales (Nolock) 
				 Where IdClaveSSA_Sal = @IdClaveSSA_Sal		
 
	   End 

	-- Regresar la Clave Generada
    Select @IdClaveSSA_Sal as Clave, @sMensaje as Mensaje 
End
Go--#SQL
