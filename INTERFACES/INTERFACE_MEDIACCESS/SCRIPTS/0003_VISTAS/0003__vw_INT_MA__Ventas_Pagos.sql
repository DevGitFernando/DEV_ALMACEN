
--------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INT_MA__Ventas_Pagos' and xType = 'V' ) 
   Drop View vw_INT_MA__Ventas_Pagos
Go--#SQL

Create View vw_INT_MA__Ventas_Pagos
With Encryption 
As 

	Select V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, V.FechaRegistro, 
		P.IdFormasDePago, F.Descripcion as FormaDePago, P.Importe, P.PagoCon, P.Cambio, P.Referencia 
	From VentasEnc V (NoLock) 
	Inner Join INT_MA_VentasPago P (NoLock) 
		On ( V.IdEmpresa = P.IdEmpresa and V.IdEstado = P.IdEstado and V.IdFarmacia = P.IdFarmacia and V.FolioVenta = P.FolioVenta ) 	
	Inner Join CatFormasDepago F (NoLock) On ( P.IdFormasDePago = F.IdFormasDePago )  	
	
Go--#SQL 

select * 
from vw_INT_MA__Ventas_Pagos 
where IdEstado = 9 and IdFarmacia = 11 
order by FolioVenta 
