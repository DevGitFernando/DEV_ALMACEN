If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CierreDeInventario_006' and xType = 'P' ) 
   Drop Proc spp_Mtto_CierreDeInventario_006 
Go--#SQL 

Create Proc spp_Mtto_CierreDeInventario_006 (@IdEmpresa Varchar(3), @IdEstado Varchar(2), @IdFarmacia Varchar(4))
With Encryption 
As 
Begin 
	Set NoCount On 
	Set DateFormat YMD

	Declare @FolioEdo Varchar(4)

	Select @FolioEdo = Max(IdEmpresaEdo) From CatEmpresasEstados
	Set @FolioEdo = @FolioEdo + 1
	Set @FolioEdo = dbo.fg_FormatearCadena(@FolioEdo,'0', 4)

	---- Relación EmpresaEstado
	If Not Exists (Select * From CatEmpresasEstados Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado)
		Begin
			Insert CatEmpresasEstados (IdEmpresaEdo, IdEmpresa, IdEstado)
			Select @FolioEdo As IdEmpresaEdo, @IdEmpresa As IdEmpresa, @IdEstado As IdEstado
		End
	Update CatEmpresasEstados Set Status = 'A', Actualizado = 0 Where IdEstado = @IdEstado And IdEmpresa = @IdEmpresa
	Update CatEmpresasEstados Set Status = 'C', Actualizado = 0 Where IdEstado = @IdEstado And IdEmpresa <> @IdEmpresa
		-------------- Relación EmpresaEstado


		------ Relacion EmpresaFarmacia
	If Not Exists (Select * From CFG_EmpresasFarmacias Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia)
		Begin
			Insert CFG_EmpresasFarmacias (  IdEmpresa, IdEstado, IdFarmacia )
			Select @IdEmpresa As IdEmpresa, @IdEstado As IdEstado, @IdFarmacia As IdFarmacia
		End
	Update CFG_EmpresasFarmacias Set Status = 'A', Actualizado = 0 Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	Update CFG_EmpresasFarmacias Set Status = 'C', Actualizado = 0 Where IdEmpresa <> @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
		-------------- Relacion  EmpresaFarmacia

End
Go--#SQL