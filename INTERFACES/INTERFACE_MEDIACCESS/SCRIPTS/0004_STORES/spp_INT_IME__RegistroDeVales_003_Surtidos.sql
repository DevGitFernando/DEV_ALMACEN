If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_IME__RegistroDeVales_003_Surtidos' and xType = 'P' ) 
   Drop Proc spp_INT_IME__RegistroDeVales_003_Surtidos
Go--#SQL 

Create Proc spp_INT_IME__RegistroDeVales_003_Surtidos
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioVenta Varchar(30),
	@IdSocioComercial varchar(8), @IdSucursal varchar(8), @Folio_Vale varchar(30)

) 
With Encryption 
As 
Begin 
Set NoCount On 
		
	Insert Into INT_IME__RegistroDeVales_003_Surtidos ( IdSocioComercial, IdSucursal, Folio_Vale, IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
	Select @IdSocioComercial, @IdSucursal, @Folio_Vale, @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta

End 
Go--#SQL 
	