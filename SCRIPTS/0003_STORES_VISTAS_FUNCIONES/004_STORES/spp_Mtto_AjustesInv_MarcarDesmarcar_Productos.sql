If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_AjustesInv_MarcarDesmarcar_Productos' and xType = 'P' ) 
   Drop Proc spp_Mtto_AjustesInv_MarcarDesmarcar_Productos  
Go--#SQL  
    
Create Proc spp_Mtto_AjustesInv_MarcarDesmarcar_Productos 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0008', @iOpcion int = 0 
) 
With Encryption 
As
Begin 
Set NoCount On 
Declare 
    @StatusAsignar varchar(2),  
    @StatusBase varchar(2) 

--    select count(*)      from FarmaciaProductos     where IdFarmacia = 8 
    
    Set @StatusAsignar = 'I' 
    Set @StatusBase = 'A'     

    If @iOpcion <> 1 
        Begin 
            Set @StatusAsignar = 'A' 
            Set @StatusBase = 'I' 
        End  

                 
    Update F Set Status = @StatusAsignar   
    From FarmaciaProductos F 
    Where 
        IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and F.Status = @StatusBase 
        and Not Exists 
        ( 
            Select * 
            From vw_Productos_Bloqueados_Por_Inventario I (NoLock) 
            Where F.IdEmpresa = I.IdEmpresa and F.IdEstado = F.IdEstado and F.IdFarmacia = I.IdFarmacia and  F.IdProducto = I.IdProducto
        )      
                 
----    Update F Set Status = 'I'     
----    From FarmaciaProductos F 
----    Where 
----        IdEmpresa = 1 and IdEstado = 25 and IdFarmacia = 8 and IdProducto in ( 1, 149 ) 
End     
Go--#SQL  

                  