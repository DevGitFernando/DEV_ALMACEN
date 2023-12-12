If Exists (Select Name From SysObjects (NoLock) Where Name = 'spp_Mtto_ProductosSelectos' And xType = 'P')
	Drop Proc spp_Mtto_ProductosSelectos
Go--#SQL 

Create Proc spp_Mtto_ProductosSelectos
( 
	@IdEmpresa Varchar(3), @IdEstado Varchar(2), @IdFarmacia Varchar(4), @IdClaveSSA_Sal varchar(4)
) 
With Encryption 
As
Begin
Set NoCount On
Set DateFormat YMD

Declare 
	@Status varchar(1),
	@Actualizado int

	Set @Status = 'A'
	Set @Actualizado = 0

	If Not Exists ( Select *
					From CFG_ProductosSelectos (NoLock)
					Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdClaveSSA_Sal = @IdClaveSSA_Sal) 
	   Begin 
		  Insert Into CFG_ProductosSelectos ( IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA_Sal, Status, Actualizado )
		  Select  @IdEmpresa, @IdEstado, @IdFarmacia, @IdClaveSSA_Sal, @Status, @Actualizado
	   End
    Else
	   Begin
		  Update CFG_ProductosSelectos
		  Set Status = @Status, Actualizado = @Actualizado 
		  Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdClaveSSA_Sal = @IdClaveSSA_Sal
	   End
	
	-- Regresar la Clave Generada
    Select  @IdClaveSSA_Sal As IdClaveSSA_Sal
	
End
Go--#SQL 