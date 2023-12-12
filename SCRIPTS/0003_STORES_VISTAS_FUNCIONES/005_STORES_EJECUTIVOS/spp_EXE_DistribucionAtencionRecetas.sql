If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_EXE_DistribucionAtencionRecetas' and xType = 'P' ) 
   Drop Proc spp_EXE_DistribucionAtencionRecetas 
Go--#SQL    

Create Proc spp_EXE_DistribucionAtencionRecetas 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0012', 
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2011-01-31'
) 
with Encryption 
As 
Begin 
Set NoCount Off  
Set Dateformat YMD 
Declare 
	@iEnero int, @iFebrero int, @iMarzo int, @iAbril int, @iMayo int, @iJunio int, 
	@iJulio int, @iAgosto int, @iSeptiembre int, @iOctubre int, @iNoviembre int, @iDiciembre int 
	
--- Inicialiar los Meses 	
	Select @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Select @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
	
--	Drop table #tmpEmisionRecetas 
--	Drop table #tmpEmisionRecetas_Cruze 		
	
	Select *  
	Into #tmpEmisionRecetas 
	From 
	( 
		select V.IdEmpresa, V.IdEstado, V.IdFarmacia, 
			datepart(yy, V.FechaRegistro) as A�o, datepart(mm, V.FechaRegistro) as Mes, 
			-- I.FechaReceta, 
			datepart(yy, I.FechaReceta) as A�oReceta, datepart(mm, I.FechaReceta) as MesReceta,  
			count(*) as Folios  
		from VentasEnc V (NoLock) 
		Inner Join VentasInformacionAdicional I (NoLock) 
			On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.FolioVenta = I.FolioVenta ) 
		Where -- datepart(yy, V.FechaRegistro) = 2010 and datepart(mm, V.FechaRegistro) = 3  
			V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado -- and V.IdFarmacia = @IdFarmacia 
			-- and V.FechaRegistro Between @FechaInicial and @FechaFinal 
		Group by V.IdEmpresa, V.IdEstado, V.IdFarmacia, 
			datepart(yy, V.FechaRegistro), datepart(mm, V.FechaRegistro), 
			datepart(yy, I.FechaReceta), datepart(mm, I.FechaReceta), 
			I.FechaReceta
	) as R 		

------ Crear Tabla de Referencia Cruzada 
	Select Top 0 
		 IdEmpresa, IdEstado, IdFarmacia, 
		 -- space(100) as IdPrograma, space(100) as Programa, space(100) as IdSubPrograma, space(100) as SubPrograma,	
		 -- IdClaveSSA, ClaveSSA, DescripcionClave, 
		 A�o, Mes, 
		 0 as A�oReceta,  
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total 
	into #tmpEmisionRecetas_Cruze	 
	From #tmpEmisionRecetas 


--- Agregar cada Clave Localizada 
    Insert Into #tmpEmisionRecetas_Cruze 
	Select Distinct 
		 IdEmpresa, IdEstado, IdFarmacia, 
		 A�o, Mes, A�oReceta,  
		 -- 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 as Total  	
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total 		 
	From #tmpEmisionRecetas 
	Order by A�o, Mes  


------------------------------------------ Distribuir recetas 
----------------- Asignar los totales por Mes 
	Update T Set Enero = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iEnero ), 0 )  
	From #tmpEmisionRecetas_Cruze T 

	Update T Set Febrero = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iFebrero ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Marzo = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iMarzo ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Abril = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iAbril ), 0 )  
	From #tmpEmisionRecetas_Cruze T 

	Update T Set Mayo = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iMayo ), 0 )  
	From #tmpEmisionRecetas_Cruze T 

	Update T Set Junio = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iJunio ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Julio = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iJulio ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Agosto = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iAgosto ), 0 )  
	From #tmpEmisionRecetas_Cruze T 


	Update T Set Septiembre = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iSeptiembre ), 0 )  
	From #tmpEmisionRecetas_Cruze T 

	Update T Set Octubre = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iOctubre ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Noviembre = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iNoviembre ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Diciembre = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta and X.MesReceta = @iDiciembre ), 0 )  
	From #tmpEmisionRecetas_Cruze T 


	Update T Set Total = IsNull(( select sum(Folios) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.A�o = T.A�o and X.Mes = T.Mes and T.A�oReceta = X.A�oReceta ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
----------------- Asignar los totales por Mes 


---- Salida Final 
	Select A�o, Mes, A�oReceta, sum(Folios) as Folios 
	From #tmpEmisionRecetas  
	Group By A�o, Mes, A�oReceta  

	Select * 
	From #tmpEmisionRecetas_Cruze  
	Order by IdEmpresa, IdEstado, IdFarmacia, A�o, Mes 

--		spp_EXE_DistribucionAtencionRecetas 

End 
Go--#SQL 
		