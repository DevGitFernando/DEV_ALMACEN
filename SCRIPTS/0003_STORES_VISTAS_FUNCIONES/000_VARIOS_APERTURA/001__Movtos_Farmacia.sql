

Declare 
    @sSql varchar(1000), 
    @sEstado varchar(2), 
    @sFarmacia varchar(4),   
    @iInsertar int 

    Set @sSql = '' 
    Set @sEstado = '21' 
    Set @sFarmacia = '0182' 
    Set @iInsertar = 0   
    
    
    If Exists ( Select * From tempdb..sysobjects (nolock) Where Name like '%#tmpMovtos__Clonacion%' ) 
       Drop Table tempdb..#tmpMovtos__Clonacion 
    
        
    Select F.IdEstado, F.IdFarmacia, M.IdTipoMovto_Inv, 0 as Consecutivo, 'A' as Status, 0 as Actualizado  
    Into #tmpMovtos__Clonacion 
    From Movtos_Inv_Tipos M, CatFarmacias F 
    Where F.IdEstado = @sEstado and F.IdFarmacia = @sFarmacia  
          and IdTipoMovto_Inv not in ( 'AEA' ) -- , 'SPR', 'EPR' ) 
          
          
    If @iInsertar = 1 
        Set @sSql = 'Insert Into Movtos_Inv_Tipos_Farmacia ' + char(10) 

    Set @sSql = @sSql + 'Select * ' + char(10)  
    Set @sSql = @sSql + 'From #tmpMovtos__Clonacion L ' + char(10)  
    Set @sSql = @sSql + 'Where Not Exists' + char(10)  
    Set @sSql = @sSql + '   (' + char(10)  
    Set @sSql = @sSql + '       Select * ' + char(10)  
    Set @sSql = @sSql + '       From Movtos_Inv_Tipos_Farmacia F  (NoLock) ' + char(10) 
    Set @sSql = @sSql + '       Where L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdTipoMovto_Inv = F.IdTipoMovto_Inv ' + char(10) 
    Set @sSql = @sSql + '   ) ' + char(10)  
    Exec ( @sSql ) 


----            Select * 
----            From #tmpMovtos__Clonacion L 
----            Where Not Exists 
----                ( 
----                    Select * 
----                    From Movtos_Inv_Tipos_Farmacia F  (NoLock) 
----                    Where L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdTipoMovto_Inv = F.IdTipoMovto_Inv 
----                )         



    
--    select top 1 *     from Movtos_Inv_Tipos_Farmacia   
     
    