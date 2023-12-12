If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_EXE_Distribucion_SurtidoRecetas' and xType = 'P' ) 
   Drop Proc spp_EXE_Distribucion_SurtidoRecetas
Go--#SQL    

Create Proc spp_EXE_Distribucion_SurtidoRecetas 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0010', 
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2012-01-31'
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
	
--  spp_EXE_Distribucion_SurtidoRecetas 	


----	Select Top 0 @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, space(4) as IdFarmacia 
----	Into #tmpFarmacias 
----	
----	
----	Insert Into #tmpFarmacias 
----	Select @IdEmpresa, @IdEstado, @IdFarmacia 
----	        	
----	If @IdFarmacia != '*' 
----	   Begin 
----	        Insert Into #tmpFarmacias 
----	        Select @IdEmpresa, @IdEstado, 
----	        From CatFarmacias 
----	   End 
	
	
	
	Select *  
	Into #tmpEmisionRecetas 
	From 
	( 
		select V.IdEmpresa, V.IdEstado, V.IdFarmacia, 
			datepart(yy, V.FechaRegistro) as Año, datepart(mm, V.FechaRegistro) as Mes, 
			-- I.FechaReceta, 
			datepart(yy, I.FechaReceta) as AñoReceta, datepart(mm, I.FechaReceta) as MesReceta,  
			count(Distinct V.FolioVenta) as Folios
			, 
			P.IdClaveSSA_Sal as IdClaveSSA, 
			'' as IdProducto, '' as CodigoEAN, 
			-- D.IdProducto, D.CodigoEAN, 
			sum(D.CantidadVendida) as Cantidad   
			-- count(V.FolioVenta) as Folios  			
		from VentasEnc V (NoLock) 
		Inner Join VentasInformacionAdicional I (NoLock) 
			On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.FolioVenta = I.FolioVenta ) 

		Inner Join VentasDet D (NoLock) 
			On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 			
		Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 	
			
		Where -- datepart(yy, V.FechaRegistro) = 2010 and datepart(mm, V.FechaRegistro) = 3  
			V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado 
			
			-- and V.IdFarmacia = @IdFarmacia   --- FILTRO DE FARMACIAS 
			 
			and V.FechaRegistro Between @FechaInicial and @FechaFinal 
		Group by V.IdEmpresa, V.IdEstado, V.IdFarmacia, 
			datepart(yy, V.FechaRegistro), datepart(mm, V.FechaRegistro), 
			datepart(yy, I.FechaReceta), datepart(mm, I.FechaReceta), 
			I.FechaReceta 
			-- , D.IdProducto, D.CodigoEAN
			, P.IdClaveSSA_Sal 
	) as R 		

--- Totalizar registros a procesar 
    Select count(*) From #tmpEmisionRecetas 

------ Crear Tabla de Referencia Cruzada 
	Select Top 0 
		 IdEmpresa, IdEstado, IdFarmacia, 
		 -- IdProducto, CodigoEAN, 
		 IdClaveSSA, space(20) as ClaveSSA, space(7900) as DescripcionClave, 
		 -- space(100) as IdPrograma, space(100) as Programa, space(100) as IdSubPrograma, space(100) as SubPrograma,	
		 -- IdClaveSSA, ClaveSSA, DescripcionClave, 
		 Año, Mes, 
		 0 as AñoReceta,  
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total 
	into #tmpEmisionRecetas_Cruze	 
	From #tmpEmisionRecetas 

--  spp_EXE_Distribucion_SurtidoRecetas 	

--- Agregar cada Clave Localizada 
    Insert Into #tmpEmisionRecetas_Cruze 
	Select Distinct 
		 IdEmpresa, IdEstado, IdFarmacia, 
		 -- IdProducto, CodigoEAN, 
		 IdClaveSSA, '', '', 
		 Año, Mes, AñoReceta,  
		 -- 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 as Total  	
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total 		 
	From #tmpEmisionRecetas 
	Order by Año, Mes  


------------------------------------------ Distribuir recetas 
-- sum(Cantidad)
----------------- Asignar los totales por Mes 
	Update T Set Enero = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iEnero and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 

	Update T Set Febrero = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iFebrero and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Marzo = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iMarzo and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Abril = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iAbril and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 

	Update T Set Mayo = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iMayo and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 

	Update T Set Junio = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iJunio and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Julio = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iJulio and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Agosto = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iAgosto and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 


	Update T Set Septiembre = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iSeptiembre and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 

	Update T Set Octubre = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iOctubre and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Noviembre = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iNoviembre and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
	
	Update T Set Diciembre = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.MesReceta = @iDiciembre and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 


	Update T Set Total = IsNull(( select sum(Cantidad) 
			From #tmpEmisionRecetas X  
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = T.Mes and T.AñoReceta = X.AñoReceta and X.IdClaveSSA = T.IdClaveSSA ), 0 )  
	From #tmpEmisionRecetas_Cruze T 
----------------- Asignar los totales por Mes 

--------------- Asignar datos de Claves 
    Update R Set ClaveSSA = C.ClaveSSA, DescripcionClave = C.Descripcion 
    From #tmpEmisionRecetas_Cruze R 
    Inner Join CatClavesSSA_Sales C (NoLock) On ( R.IdClaveSSA = C.IdClaveSSA_Sal ) 
--------------- Asignar datos de Claves 


-------- Salida Final 
----	Select IdEmpresa, IdEstado, IdFarmacia, Año, Mes, AñoReceta, sum(Cantidad) as Folios 
----	From #tmpEmisionRecetas  
----	Group By IdEmpresa, IdEstado, IdFarmacia, Año, Mes, AñoReceta  
----	Order by IdEmpresa, IdEstado, IdFarmacia, Año, Mes, AñoReceta   


------------------------------------------------------- 
    If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Distribucion_SurtimientoRecetas' and xType = 'U' )
       Drop Table Rpt_Distribucion_SurtimientoRecetas 


	Select * 
	Into Rpt_Distribucion_SurtimientoRecetas 
	From #tmpEmisionRecetas_Cruze  
	Order by IdEmpresa, IdEstado, IdFarmacia, Año, Mes, AñoReceta, DescripcionClave    

    
    Select * 
    From Rpt_Distribucion_SurtimientoRecetas (NoLock) 
	Order by IdEmpresa, IdEstado, IdFarmacia, Año, Mes, AñoReceta, DescripcionClave    
	
--		spp_EXE_Distribucion_SurtidoRecetas  

End 
Go--#SQL 
		