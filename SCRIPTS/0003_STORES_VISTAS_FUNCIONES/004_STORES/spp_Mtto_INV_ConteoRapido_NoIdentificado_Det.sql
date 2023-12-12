If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_INV_ConteoRapido_NoIdentificado_Det' and xType = 'P' ) 
   Drop Proc spp_Mtto_INV_ConteoRapido_NoIdentificado_Det 
Go--#SQL 
 
Create Proc spp_Mtto_INV_ConteoRapido_NoIdentificado_Det 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0101', 
    @Folio varchar(8) = '000001', @ClaveSSA varchar(30) = '0106',  @Descripcion varchar(7500) = 'Paracetamol 100',
	@CodigoEAN varchar(30) = '75123456789012', @NombreComercial varchar(7500) = 'Tempra Forte', @Laboratorio varchar(100) = 'Aco', 
	@Cantidad int = 100, @Status varchar(1) = 'A'
)  
With Encryption 
As 
Begin 
Set NoCount On 
	
	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/
	
	If Not Exists ( Select * From INV_ConteoRapido_NoIdentificado_Det (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
					and IdFarmacia = @IdFarmacia and Folio = @Folio and ClaveSSA = @ClaveSSA and CodigoEAN = @CodigoEAN )
		Begin
		
			Insert Into INV_ConteoRapido_NoIdentificado_Det  
			( 
				IdEmpresa, IdEstado, IdFarmacia, Folio, ClaveSSA, Descripcion, CodigoEAN, NombreComercial, Laboratorio, 
				Cantidad, FechaRegistro, Status, Actualizado 
			) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @ClaveSSA, @Descripcion, @CodigoEAN, @NombreComercial, @Laboratorio, 
			@Cantidad, getdate() as FechaRegistro, @Status, 0 as Actualizado 
			
		End

End 
Go--#SQL   

