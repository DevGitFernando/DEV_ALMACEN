	    
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Proceso_Historial__DispensacionPorClaveMensual' and xType = 'P') 
    Drop Proc spp_Proceso_Historial__DispensacionPorClaveMensual 
Go--#SQL 
  
--  Exec spp_Proceso_Historial__DispensacionPorClaveMensual '21', '006', '0002', '2011-10-01', '2012-01-01'
  
Create Proc spp_Proceso_Historial__DispensacionPorClaveMensual 
(   
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
	@FechaInicial varchar(10) = '2011-10-01', @FechaFinal varchar(10) = '2012-01-01'
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@IdFarmacia varchar(4)

	Select @FechaInicial, @FechaFinal 

    Declare #tmpConsumos  
    Cursor For 
    Select IdFarmacia 
    From CatFarmacias (NoLock) 
	Where IdEstado = @IdEstado and Status = 'A' 
	Order by IdTipoUnidad, IdJurisdiccion, IdFarmacia 
    Open #tmpConsumos 
    FETCH NEXT FROM #tmpConsumos Into @IdFarmacia   
        WHILE @@FETCH_STATUS = 0
        BEGIN          

		   Exec  spp_Rpt_Proceso_VentasPorClaveMensual @IdEmpresa, @IdEstado, '*', @IdFarmacia, @FechaInicial, @FechaFinal

           FETCH NEXT FROM #tmpConsumos Into  @IdFarmacia  
        END
    Close #tmpConsumos
    Deallocate #tmpConsumos


End
Go--#SQL 


