If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Impresion_Farmacia_Beneficiarios' and xType = 'P')
    Drop Proc spp_Mtto_Impresion_Farmacia_Beneficiarios
Go--#SQL
  
Create Proc spp_Mtto_Impresion_Farmacia_Beneficiarios ( @IdEstado varchar(2) = '25', @IdCliente varchar(4) = '0002' , @IdSubCliente varchar(4) = '0001'  )
With Encryption 
As
Begin

	Set DateFormat YMD

	-- Se elimina la tabla en caso de existir.
	If Exists (Select Name From SysObjects(NoLock) Where Name = 'rpt_Farmacias_Beneficiarios' and xType = 'U')
	  Begin
		Drop Table rpt_Farmacias_Beneficiarios
	  End
	
	-- Se insertan los beneficiarios en la tabla.
	Select *, GetDate() as FechaImpresion
	Into rpt_Farmacias_Beneficiarios
	From vw_Beneficiarios(NoLock)
	Where IdEstado = @IdEstado And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente
	And IdFarmacia In ( Select IdFarmacia From CTE_FarmaciasProcesar(NoLock) )
	Order By IdFarmacia, NombreCompleto

End
Go--#SQL
