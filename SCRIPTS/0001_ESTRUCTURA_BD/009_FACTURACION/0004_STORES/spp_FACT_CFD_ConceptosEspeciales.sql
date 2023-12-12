-------------------------------------------------------------------------------------------------- 
If Exists ( Select So.Name, *  From Sysobjects So (NoLock) Where So.Name = 'spp_FACT_CFD_ConceptosEspeciales' and So.xType = 'P' ) 
   Drop Proc spp_FACT_CFD_ConceptosEspeciales  
Go--#SQL 

Create Proc spp_FACT_CFD_ConceptosEspeciales 
( 
	@IdEstado varchar(2) = '11', @IdConcepto varchar(4) = '0001', 
	@Descripcion varchar(200) = '', @Opcion tinyint = 0  
) 
With Encryption 
As 
Begin 
Set NoCount On	
Set DateFormat YMD
	
Declare 
	@sMensaje varchar(100), 
	@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/ 
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	
	


End 
Go--#SQL 


