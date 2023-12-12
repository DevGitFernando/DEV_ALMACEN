If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_UbicacionesVacias' And xType = 'P' )
	Drop Proc spp_Rpt_UbicacionesVacias 
Go--#SQL 

Create Procedure spp_Rpt_UbicacionesVacias 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182', @TipoReporte varchar(1) = '1'  
) 
With Encryption 
As 
Begin 	

--- Ubicaciones 
    Select 
        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
        IdPasillo, DescripcionPasillo, StatusPasillo, 
        IdEstante, DescripcionEstante, StatusEstante, 
        IdEntrepaño, DescripcionEntrepaño, StatusEntrepaño, 0 as Existencia   
    Into #tmpUbicaciones  
    From vw_Pasillos_Estantes_Entrepaños P 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And StatusPasillo = 'A' And StatusEstante = 'A' And StatusEntrepaño = 'A'
    Order by IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño         
    
    
--- Existencia     
    Select IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño, sum(Existencia) as Existencia  
    Into #tmpExistencia  
    From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
    Group by IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño     
        
--- Asignacion de Existencias 
    Update U Set Existencia = E.Existencia 
    From #tmpUbicaciones U 
    Inner Join #tmpExistencia E 
        On ( 
            U.IdEmpresa = E.IdEmpresa and U.IdEstado = E.IdEstado and U.IdFarmacia = E.IdFarmacia 
            and U.IdPasillo = E.IdPasillo and U.IdEstante = E.IdEstante and U.IdEntrepaño = E.IdEntrepaño 
           ) 
    
--- Salida Final     
--    If @TipoReporte = 1 
--       Begin 
            Select 
                IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
                IdPasillo as IdRack, DescripcionPasillo as Rack, -- StatusPasillo, 
                IdEstante as IdNivel, DescripcionEstante as Nivel, -- StatusEstante, 
                IdEntrepaño, DescripcionEntrepaño as Entrepaño  -- , StatusEntrepaño 
            From #tmpUbicaciones  
            Where Existencia <= 0 
--       End 


----    If @TipoReporte = 2  
----       Begin 
----            Select 
----                -- IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
----                IdPasillo, DescripcionPasillo as Pasillo, -- StatusPasillo, 
----                IdEstante, DescripcionEstante as Estante, -- StatusEstante, 
----                IdEntrepaño, DescripcionEntrepaño as Entrepaño  -- , StatusEntrepaño 
----            From #tmpUbicaciones  
----            Where Existencia <= 0 
----       End 

        
End 
Go--#SQL    
--    sp_listacolumnas vw_Pasillos_Estantes_Entrepaños 
    
--    vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 


