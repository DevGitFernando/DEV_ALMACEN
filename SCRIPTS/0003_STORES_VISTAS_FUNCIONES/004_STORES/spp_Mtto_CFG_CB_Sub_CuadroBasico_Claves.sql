


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_CB_Sub_CuadroBasico_Claves' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFG_CB_Sub_CuadroBasico_Claves 
Go--#SQL

Create Proc spp_Mtto_CFG_CB_Sub_CuadroBasico_Claves 
	( 
		@IdEstado varchar(2) = '21', @IdCliente varchar(4) = '0002', @IdNivel int = 2,
		@IdFarmacia varchar(4) = '1006', @IdPrograma varchar(4) = '0002', @IdSubPrograma varchar(4) = '0001', 
		@IdClaveSSA varchar(4), @Cantidad int = 0   
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
	Set @iActualizado = 0 
	Set @sStatus = 'A'
       
	   If Not Exists ( Select * From CFG_CB_Sub_CuadroBasico_Claves  (NoLock) Where IdEstado = @IdEstado and IdCliente = @IdCliente 
						and IdNivel = @IdNivel and IdFarmacia = @IdFarmacia and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma
						and IdClaveSSA = @IdClaveSSA  ) 
		  Begin 
			 Insert Into CFG_CB_Sub_CuadroBasico_Claves  ( IdEstado, IdCliente, IdNivel, IdFarmacia, IdPrograma, IdSubPrograma, IdClaveSSA, 
														Cantidad, FechaUpdate, Status, Actualizado ) 
			 Select @IdEstado, @IdCliente, @IdNivel, @IdFarmacia, @IdPrograma, @IdSubPrograma, @IdClaveSSA, @Cantidad, GetDate(), @sStatus, @iActualizado 
          End 
	   Else 
		  Begin 
			Set @sStatus =  ( Select Status From CFG_CB_Sub_CuadroBasico_Claves  (NoLock) Where IdEstado = @IdEstado and IdCliente = @IdCliente 
						and IdNivel = @IdNivel and IdFarmacia = @IdFarmacia and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma
						and IdClaveSSA = @IdClaveSSA  )
						
			If @sStatus = 'A'
				Begin
					Set @sStatus = 'C'
				End
			Else 
				Begin
					Set @sStatus = 'A'					
				End
			
			Update CFG_CB_Sub_CuadroBasico_Claves  Set Cantidad = @Cantidad, Status = @sStatus
			Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdNivel = @IdNivel and IdFarmacia = @IdFarmacia 
			and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma and IdClaveSSA = @IdClaveSSA
			  
          End 
          
End 
Go--#SQL