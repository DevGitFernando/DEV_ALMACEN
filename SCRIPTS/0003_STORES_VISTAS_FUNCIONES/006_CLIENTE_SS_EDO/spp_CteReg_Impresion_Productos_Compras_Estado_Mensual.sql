
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CteReg_Impresion_Productos_Compras_Estado_Mensual' and xType = 'P' ) 
   Drop Proc spp_CteReg_Impresion_Productos_Compras_Estado_Mensual
Go--#SQL 

Create Proc spp_CteReg_Impresion_Productos_Compras_Estado_Mensual 	
With Encryption 
As 
Begin 
Set NoCount Off  
Set Dateformat YMD 
Declare 
	@iEnero int, @iFebrero int, @iMarzo int, @iAbril int, @iMayo int, @iJunio int, 
	@iJulio int, @iAgosto int, @iSeptiembre int, @iOctubre int, @iNoviembre int, @iDiciembre int 											

	--------------------------------------
	-- Se asignan los valores iniciales --
	--------------------------------------
	Select @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Select @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
	
	---------------------------------------
	-- Se obtienen los Datos Principales --
	---------------------------------------
	Select  IdEmpresa, Empresa, IdEstado, Estado, Datepart(yy, FechaRegistro) as Año, Datepart(mm, FechaRegistro) as Mes, 
			IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, Sum(Cantidad) as CantidadComprada, 
			FechaInicial, FechaFinal  
	Into #tmpComprasProv 		   
	From tmpCteReg_Productos_Compras_Estado(NoLock)
	Group by  IdEmpresa, Empresa, IdEstado, Estado, Datepart(yy, FechaRegistro), Datepart(mm, FechaRegistro), 
		   IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, FechaInicial, FechaFinal  

	Select	IdEmpresa, Empresa, IdEstado, Estado, Año, Mes, IdClaveSSA, ClaveSSA, DescripcionSal, 
			Sum(CantidadComprada) as CantidadComprada, FechaInicial, FechaFinal  
	Into  #tmpComprasProv_Claves	
	From #tmpComprasProv 	   		   	
	Group by IdEmpresa, Empresa, IdEstado, Estado, Año, Mes, IdClaveSSA, ClaveSSA, DescripcionSal, FechaInicial, FechaFinal   
		   		   	
	---------------------------------------
	-- Crear Tabla de Referencia Cruzada --
	---------------------------------------
	Select Top 0 
		 IdEmpresa, Empresa, IdEstado, Estado, -- IdFarmacia, 
		 IdClaveSSA, ClaveSSA, DescripcionSal, Año, 
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total,
		 GetDate() as FechaImpresion, FechaInicial, FechaFinal  
	Into #tmpComprasProv_Claves_Cruze	 
	From #tmpComprasProv_Claves 

	-----------------------------------
	-- Agregar cada Clave Localizada --
	-----------------------------------
    Insert Into #tmpComprasProv_Claves_Cruze 
	Select Distinct 
		 IdEmpresa, Empresa, IdEstado, Estado, IdClaveSSA, ClaveSSA, DescripcionSal, Año, 
		 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 as Total, GetDate(), FechaInicial, FechaFinal  	
	From #tmpComprasProv_Claves 
	Order by  Año 
	
	---------------------------------
	-- Asignar los totales por Mes --
	---------------------------------
	Update T Set Enero = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iEnero ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 

	Update T Set Febrero = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iFebrero ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 
	
	Update T Set Marzo = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iMarzo ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 
	
	Update T Set Abril = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iAbril ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 

	Update T Set Mayo = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iMayo ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 

	Update T Set Junio = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iJunio ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 
	
	Update T Set Julio = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iJulio ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 
	
	Update T Set Agosto = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iAgosto ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 


	Update T Set Septiembre = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iSeptiembre ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 

	Update T Set Octubre = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iOctubre ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 
	
	Update T Set Noviembre = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iNoviembre ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 
	
	Update T Set Diciembre = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iDiciembre ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 


	Update T Set Total = IsNull(( select sum(CantidadComprada) 
			From #tmpComprasProv_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año ), 0 )  
	From #tmpComprasProv_Claves_Cruze T 

	-------------------------------------------------------
	-- Se inserta el resultado en una tabla del servidor --
	-------------------------------------------------------	
	If Exists ( Select Name From SysObjects(NoLock) Where Name = 'tmpCteReg_Productos_Compras_Estado_Mensual' And xType = 'U' )
	  Begin
		Drop Table tmpCteReg_Productos_Compras_Estado_Mensual
	  End	
	
	Select *
	Into tmpCteReg_Productos_Compras_Estado_Mensual
	From #tmpComprasProv_Claves_Cruze(NoLock)
	Order By Año, DescripcionSal

	------------------------------
	-- Salida Final del Proceso --
	------------------------------
--	Select 
--		 IdEmpresa, IdEstado, IdProveedor, Proveedor, IdClaveSSA, ClaveSSA, DescripcionSal, Año, 
--		 Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre, Total   
--	From tmpCteReg_Productos_Compras_Estado_Mensual 
	
End	
Go--#SQL 
	