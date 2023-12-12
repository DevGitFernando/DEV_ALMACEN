If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Ventas_AsignarPrecioLicitacion' and xType = 'P' )
    Drop Proc spp_Mtto_Ventas_AsignarPrecioLicitacion 
Go--#SQL 
  
Create Proc spp_Mtto_Ventas_AsignarPrecioLicitacion 
(	
    @IdEmpresa varchar(3) = '001',  @IdEstado varchar(4) = '25', @IdFarmacia varchar(6) = '0008', @FolioVenta varchar(32) = '00000002'
) 
With Encryption 
As
Begin
Set NoCount On 
Declare 
    @sIdCliente varchar(4), 
    @sIdSubCliente varchar(4)  

--- Obtener los datos Iniciales 
    Select @sIdCliente = IdCliente, @sIdSubCliente = IdSubCliente 
    From VentasEnc V (NoLock) 
    Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia and V.FolioVenta = @FolioVenta 

--- Asignar el Precio segun licitacion 
    Update V Set PrecioLicitacion = dbo.fg_CalcularPrecioLicitacion(@IdEstado, @sIdCliente, @sIdSubCliente, V.IdProducto) 
    From VentasDet V (NoLock) 
    Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia and V.FolioVenta = @FolioVenta 

End 
Go--#SQL

/* 	
    Update D Set PrecioLicitacion = dbo.fg_CalcularPrecioLicitacion(V.IdEstado, V.IdCliente, V.IdSubCliente, D.IdProducto) 	
    From VentasDet D (NoLock) 
    Inner Join VentasEnc V (NoLock) On 
        ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
    Where V.IdEmpresa = 1 and V.IdEstado = 25 and V.IdFarmacia = 8 -- and V.FolioVenta = 2 
*/       
    
    	