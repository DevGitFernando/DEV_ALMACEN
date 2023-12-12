----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_BancosCuentas__Emisor' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFDI_BancosCuentas__Emisor
Go--#SQL    

Create Proc spp_Mtto_CFDI_BancosCuentas__Emisor
(
	
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2)  = '13', @IdFarmacia varchar(4) = '0001', 
	@RFC_Emisor varchar(15) = '', @RFC_Banco varchar(15) = '',  @NumeroDeCuenta varchar(50) = '', @iOpcion int = 0
) 
As 
Begin 
Set NoCount On 


	declare @Status Varchar(1) = 'A'

	If @iOpcion = 1 
		Begin 
			If Not Exists ( Select *
							From CFDI_BancosCuentas__Emisor (NoLock)
							Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And
								  RFC_Emisor = @RFC_Emisor And RFC_Banco = @RFC_Banco And NumeroDeCuenta = @NumeroDeCuenta) 
				Begin 
					--Select IdEmpresa, IdEstado, IdFarmacia, RFC_Emisor, RFC_Banco, NumeroDeCuenta, @Status, 0 as Actualizado 	
					Insert Into CFDI_BancosCuentas__Emisor ( IdEmpresa, IdEstado, IdFarmacia, RFC_Emisor, RFC_Banco, NumeroDeCuenta, Status )
					Select @IdEmpresa, @IdEstado, @IdFarmacia, @RFC_Emisor, @RFC_Banco, @NumeroDeCuenta, @Status
				End
			--Else
			--	Begin
			--		Update E Set Status = @Status
			--		From FACT_CFDI_BancosCuentas__Emisor E (NoLock)
			--		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And RFC_Emisor = @RFC_Emisor And RFC_Banco = @RFC_Banco And NumeroDeCuenta = @NumeroDeCuenta
			--	End
		End
	Else 	
		Begin
			Set @Status = 'C'

			--Select *
			Update E Set Status = @Status 
			From CFDI_BancosCuentas__Emisor E (NoLock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And RFC_Emisor = @RFC_Emisor And RFC_Banco = @RFC_Banco And NumeroDeCuenta = @NumeroDeCuenta
		End

    	
End 
Go--#SQL 

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_BancosCuentas__Receptor' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFDI_BancosCuentas__Receptor
Go--#SQL    

Create Proc spp_Mtto_CFDI_BancosCuentas__Receptor
(
	
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2)  = '13', @IdFarmacia varchar(4) = '0001', 
	@RFC_Banco varchar(15) = '',  @NumeroDeCuenta varchar(50) = '', @iOpcion int = 0
) 
As 
Begin 
Set NoCount On 


	declare @Status Varchar(1) = 'A'

	If @iOpcion = 1 
		Begin 
			If Not Exists ( Select *
							From CFDI_BancosCuentas__Receptor (NoLock)
							Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And
								  RFC_Banco = @RFC_Banco And NumeroDeCuenta = @NumeroDeCuenta) 
				Begin 
					Insert Into CFDI_BancosCuentas__Receptor ( IdEmpresa, IdEstado, IdFarmacia, RFC_Banco, NumeroDeCuenta, Status )
					Select @IdEmpresa, @IdEstado, @IdFarmacia, @RFC_Banco, @NumeroDeCuenta, @Status
				End
			--Else
			--	Begin
			--		Update E Set Status = @Status
			--		From FACT_CFDI_BancosCuentas__Receptor E (NoLock)
			--		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And RFC_Banco = @RFC_Banco And NumeroDeCuenta = @NumeroDeCuenta
			--	End
		End
	Else 	
		Begin
			Set @Status = 'C'

			--Select *
			Update E Set Status = @Status 
			From CFDI_BancosCuentas__Receptor E (NoLock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And RFC_Banco = @RFC_Banco And NumeroDeCuenta = @NumeroDeCuenta
		End

    	
End 
Go--#SQL 