If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFG_AsignarPrecios_ClavesSSA_Por_Programa' and xType = 'P' ) 
   Drop Proc spp_CFG_AsignarPrecios_ClavesSSA_Por_Programa
Go--#SQL	
 
-- Exec spp_CFG_AsignarPrecios_ClavesSSA_Por_Programa '11', '0002', '0006', '0002', '0001', '0013', '234ffg', 'fgedrhndhned', 24.1234, '25', '0001', '0025'
Create Proc spp_CFG_AsignarPrecios_ClavesSSA_Por_Programa 
(
	@IdEstado varchar(2), @IdCliente varchar(4), @IdSubCliente varchar(4), @IdPrograma Varchar(4), @IdSubprograma varchar(4), @IdClaveSSA_Sal varchar(4),
	@Precio numeric(14, 4), @IdEstadoPersonal varchar(2), @IdFarmaciaPersonal varchar(4), @IdPersonal varchar(4), @iOpcion tinyint
) 
With Encryption  
As 
Begin 
Set NoCount On 
Declare @sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	
	
	if @iOpcion = 1 
	   Begin 
	       If Not Exists ( Select * From CFG_ClavesSSA_Precios_Programas (NoLock) 
							Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente And
							IdPrograma = @IdPrograma And IdSubPrograma = @IdSubprograma and IdClaveSSA_Sal = @IdClaveSSA_Sal 
						 )  
	          Begin 
				 Insert Into CFG_ClavesSSA_Precios_Programas ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, IdPrograma, IdSubPrograma,
								Precio, Status, Actualizado ) 
				 Select @IdEstado, @IdCliente, @IdSubCliente, @IdClaveSSA_Sal, @IdPrograma, @IdSubprograma,
								@Precio, @sStatus, @iActualizado 
	          End 
	       Else 
	          Begin 
				  Update CFG_ClavesSSA_Precios_Programas Set Precio = @Precio, Status = @sStatus, Actualizado = @iActualizado 
				  Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal 
						And IdPrograma = @IdPrograma And IdSubPrograma = @IdSubprograma
	          End    
	   End 
	Else 
	   Begin 
	      Set @sStatus = 'C' 
	      Update CFG_ClavesSSA_Precios_Programas Set Status = @sStatus, Actualizado = @iActualizado 
	      Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal
				And IdPrograma = @IdPrograma And IdSubPrograma = @IdSubprograma
	   End 
	      
	
	-- Insertar la información en el historico 
	Insert Into CFG_ClavesSSA_Precios_Programas_Historico 
		( IdEstado, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, IdClaveSSA_Sal,
		  FechaUpdate, Precio, IdEstadoPersonal, IdFarmaciaPersonal, IdPersonal, Status, Actualizado ) 
	Select @IdEstado, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubprograma, @IdClaveSSA_Sal, 
	      getdate() as FechaRegistro, Precio, @IdEstadoPersonal, @IdFarmaciaPersonal, @IdPersonal, Status, Actualizado 
    From CFG_ClavesSSA_Precios_Programas (NoLock) 
    Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal 
			And IdPrograma = @IdPrograma And IdSubPrograma = @IdSubprograma
	
End 
Go--#SQL 	
 

