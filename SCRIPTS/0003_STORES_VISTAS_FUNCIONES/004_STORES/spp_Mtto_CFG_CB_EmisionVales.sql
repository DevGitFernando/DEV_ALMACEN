


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_CB_EmisionVales' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFG_CB_EmisionVales 
Go--#SQL

Create Proc spp_Mtto_CFG_CB_EmisionVales ( @IdEstado varchar(2), @IdCliente varchar(4), @IdSubCliente varchar(4),
											@IdClaveSSA_Sal varchar(4), @ClaveSSA varchar(30), @EmiteVales tinyint = 0   )  
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
	Set @iActualizado = 0 
	
       
	   If Not Exists ( Select * From CFG_CB_EmisionVales (NoLock) Where IdEstado = @IdEstado and IdCliente = @IdCliente 
						and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal and ClaveSSA = @ClaveSSA ) 
		  Begin 
			 Insert Into CFG_CB_EmisionVales ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, ClaveSSA, EmiteVales, Actualizado ) 
			 Select @IdEstado, @IdCliente, @IdSubCliente, @IdClaveSSA_Sal, @ClaveSSA, @EmiteVales, @iActualizado 
          End 
	   Else 
		  Begin 
		     Update CFG_CB_EmisionVales Set EmiteVales = @EmiteVales
			 Where IdEstado = @IdEstado and IdCliente = @IdCliente 
			 and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal and ClaveSSA = @ClaveSSA  
          End 
          
End 
Go--#SQL