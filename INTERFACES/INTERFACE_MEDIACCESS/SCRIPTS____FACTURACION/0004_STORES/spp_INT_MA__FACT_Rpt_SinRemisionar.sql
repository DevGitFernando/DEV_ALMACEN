If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_MA__FACT_Rpt_SinRemisionar' and xType = 'P' )
    Drop Proc spp_INT_MA__FACT_Rpt_SinRemisionar
Go--#SQL 
  
Create Proc spp_INT_MA__FACT_Rpt_SinRemisionar ( @IdEmpresa Varchar(3) = '002',  @IdEstado Varchar(2) = '09' ) 
With Encryption		
As 
Begin 
Set NoCount On 


	Select
		'Id Farmacia' = D.IdFarmacia, F.Farmacia,
		E.IdCliente As 'Clave Cliente', CAST('' As Varchar(200)) As Cliente, E.IdSubCliente As 'Clave Sub-Cliente', CAST('' As Varchar(200)) As 'Sub-Cliente',
		'Número de piezas' = cast(Sum(CantidadVendida) as int)
	Into #Reporte
	From VentasEnc E (NoLock)
	Inner Join VentasDet D (NoLock)
		On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioVenta = D.FolioVenta)
	Inner Join vw_Farmacias F (NoLock) On (D.IdEstado = F.IdEstado And D.IdFarmacia  = F.IdFarmacia)
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And D.EnRemision = 0 And D.Facturado = 0
	Group BY D.IdFarmacia, F.Farmacia, E.IdCliente, E.IdSubCliente

	Update T
	Set T.Cliente = V.NombreCliente, T.[Sub-Cliente] = V.NombreSubCliente
	From #Reporte T
	Inner Join vw_Clientes_SubClientes V (NoLock) On (T.[Clave Cliente] = V.IdCliente And T.[Clave Sub-Cliente] = V.IdSubCliente)
	Where V.IdEstado = @IdEstado


	Select * From #Reporte
	
	
End 
Go--#SQL