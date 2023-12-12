If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_EX_Validacion_Titulos_Reportes_Reportes' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFG_EX_Validacion_Titulos_Reportes_Reportes 
Go--#SQL   

--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_EX_Validacion_Titulos_Reportes' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFG_EX_Validacion_Titulos_Reportes 
Go--#SQL   

Create Proc spp_Mtto_CFG_EX_Validacion_Titulos_Reportes 
( 
    @IdEstado varchar(2) = '21', @IdTitulo varchar(2) = '11', @TituloEncabezadoReporte varchar(200) = '', 	@iActivo tinyint = 1 
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

	Set @sMensaje = ''
	Set @sStatus = (case when @iActivo = 1 then 'A' else 'C' end)
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso     
    
	If Not Exists ( Select * From CFG_EX_Validacion_Titulos_Reportes  (NoLock) 
					   Where IdEstado = @IdEstado and IdTitulo = @IdTitulo ) 
		Begin 
			Insert Into CFG_EX_Validacion_Titulos_Reportes  ( IdEstado, IdTitulo, TituloEncabezadoReporte, Status, Actualizado ) 
			Select @IdEstado, @IdTitulo, @TituloEncabezadoReporte, @sStatus, @iActualizado 
		End 
	
	
	If @iActivo = 1 
		Begin 
			Update C Set TituloEncabezadoReporte = @TituloEncabezadoReporte, Status = @sStatus, Actualizado = @iActualizado 
			From CFG_EX_Validacion_Titulos_Reportes  C 	
			Where IdEstado = @IdEstado and IdTitulo = @IdTitulo 
		End 				 
	Else 
		Begin 
			Update C Set Status = @sStatus, Actualizado = @iActualizado 
			From CFG_EX_Validacion_Titulos_Reportes  C 	
			Where IdEstado = @IdEstado and IdTitulo = @IdTitulo
		End 				 	
    
	-- Se devuelve el resultado.
	Select @sMensaje as Mensaje 
    
End 
Go--#SQL   


