
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_RPT_CFGC_ALMN__RutaDistribucion_Transferencia' and xType = 'P')
    Drop Proc spp_RPT_CFGC_ALMN__RutaDistribucion_Transferencia
Go--#SQL
  
Create Proc spp_RPT_CFGC_ALMN__RutaDistribucion_Transferencia( @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '2005', @IdRuta varchar(4) = '0003' )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), @sStatus varchar(1), @iActualizado smallint

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0


	Select *
	From CFGC_ALMN__RutaDistribucion (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And IdRuta = @IdRuta

	Select C.IdCliente, C.IdSubCliente, C.IdBeneficiario, B.NombreCompleto, B.FolioReferencia
	From CFGC_ALMN__RutaDistribucion_Beneficiario C (NoLock)
	Inner Join vw_Beneficiarios B (NoLock) On (C.IdEstado = B.IdEstado And C.IdFarmacia = B.IdFarmacia And C.IdCliente = B.IdCliente And C.IdSubCliente = B.IdSubCliente And C.IdBeneficiario = B.IdBeneficiario)
	Where C.IdEstado = @IdEstado and C.IdFarmacia = @IdFarmacia And IdRuta = @IdRuta And C.Status = 'A'

	Select F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia
	From CFGC_ALMN__RutaDistribucion_Transferencia C(NoLock) 
	Inner Join vw_Farmacias F (NoLock) On (C.IdEstadoEnvia = F.IdEstado And C.IdFarmaciaEnvia = F.IdFarmacia)
	Where C.IdEstado = @IdEstado and C.IdFarmacia = @IdFarmacia And IdRuta = @IdRuta And C.Status = 'A'
	
End
Go--#SQL
