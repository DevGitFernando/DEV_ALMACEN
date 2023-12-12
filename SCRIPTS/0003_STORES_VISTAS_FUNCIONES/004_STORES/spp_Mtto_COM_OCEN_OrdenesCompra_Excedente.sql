If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_COM_OCEN_OrdenesCompra_Excedente' and xType = 'P' ) 
   Drop Proc spp_Mtto_COM_OCEN_OrdenesCompra_Excedente
Go--#SQL

Create Proc spp_Mtto_COM_OCEN_OrdenesCompra_Excedente
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioOrden varchar(8), @IdPersonal varchar(4),
	@IdClaveSSA varchar(8), @Porcentaje Numeric(14, 4), @Observaciones varchar(300), @iOpcion int
)

With Encryption 
As 
Begin 
Set NoCount On 

Declare @Mensaje varchar(1000), 
		@Status varchar(1), 
		@Actualizado smallint

	Set @Mensaje = ''
	Set @Status = 'A'
	Set @Actualizado = 0

	If @iOpcion = 1
	Begin
		Insert Into COM_OCEN_OrdenesCompra_Excedente ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdPersonal, IdClaveSSA, Porcentaje, Observaciones, Status, Actualizado)
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrden, @IdPersonal, @IdClaveSSA, @Porcentaje, @Observaciones, @Status, @Actualizado
	End

	-- Devolver el resultado
	Select @FolioOrden as Folio
End
Go--#SQL 
