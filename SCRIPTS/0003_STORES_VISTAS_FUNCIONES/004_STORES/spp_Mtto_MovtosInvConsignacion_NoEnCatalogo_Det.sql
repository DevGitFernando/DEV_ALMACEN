If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_MovtosInvConsignacion_NoEnCatalogo_Det' and xType = 'P' ) 
   Drop Proc spp_Mtto_MovtosInvConsignacion_NoEnCatalogo_Det 
Go--#SQL 
 
Create Proc spp_Mtto_MovtosInvConsignacion_NoEnCatalogo_Det 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0101', 
    @Folio varchar(6) = '000001', @ClaveSSA varchar(20) = '0106',  @Descripcion varchar(100) = 'Paracetamol 100',
	@CodigoEAN varchar(30) = '75123456789012', @NombreComercial varchar(100) = 'Tempra Forte', @Laboratorio varchar(100) = 'Aco', 
	@ClaveLote varchar(30), @FechaCaducidad DateTime = "2012-01-17", @CantidadLote int = 100, @Status varchar(2) = 'A'
)  
With Encryption 
As 
Begin 
Set NoCount On 
	
	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/
	
		Insert Into MovtosInvConsignacion_NoEnCatalogo_Det 
			( IdEmpresa, IdEstado, IdFarmacia, Folio, ClaveSSA, Descripcion, CodigoEAN, NombreComercial, 
				Laboratorio, ClaveLote, FechaCaducidad, CantidadLote, FechaRegistro, Status, Actualizado ) 
		Select 
			  @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @ClaveSSA, @Descripcion, @CodigoEAN, @NombreComercial, 
			  @Laboratorio, @ClaveLote, @FechaCaducidad, @CantidadLote, getdate() as FechaRegistro, @Status, 0 as Actualizado 

End 
Go--#SQL   

