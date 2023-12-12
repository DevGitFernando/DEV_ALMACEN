
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_ADMI_Vales_Estadisticas_Datos' And xType = 'P' )
	Drop Proc spp_ADMI_Vales_Estadisticas_Datos
Go--#SQL


Create Procedure spp_ADMI_Vales_Estadisticas_Datos ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
	@iAño int = 2012, @iMes int = 6, @iTopReporte int = 5 )
With Encryption
As
Begin
	Declare @sSql varchar(8000), 
			@sTopReporte varchar(8000)

	/*
		REPORTES
		1.- Concentrado.
		2.- Farmacias.
		3.- Claves.
		4.- Perdidas.
		5.- Proveedores.
	*/

	Set DateFormat YMD
	Set NoCount On
	Set @sSql = ''
	Set @sTopReporte = Cast( @iTopReporte as varchar ) 
	

	---------------------------------------
	-- Se obtiene el Concentrado del Mes --
	---------------------------------------
	Select Top 1 ValesEmitidos_Mes as 'Vales Emitidos', ValesRegistrados_Mes as 'Vales Registrados',  
				 PiezasEmitidas_Mes as 'Piezas Emitidas', PiezasRegistradas_Mes as 'Piezas Registradas',  
				 ImporteRegistrado_Mes as 'Importe Registrado'  
	Into #tmpConcentrado
	From ADMI_Vales_Estadisticas(NoLock)  
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And Año = @iAño And Mes = @iMes

	--------------------------------------------
	-- Se obtiene el concentrado de Farmacias --
	--------------------------------------------
	Select Distinct IdFarmacia, ValesEmitidos_Farmacia, ValesRegistrados_Farmacia,  PiezasEmitidas_Farmacia, 
					PiezasRegistradas_Farmacia, ImporteRegistrado_Farmacia
	Into #tmpDatos_Farmacias
	From ADMI_Vales_Estadisticas(NoLock)   
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And Año = @iAño And Mes = @iMes 
	Order By ValesEmitidos_Farmacia Desc 

	-- Se obtiene la sumatoria de las Farmacias.
	Select F.IdFarmacia, F.NombreFarmacia as Farmacia,  Sum( E.ValesEmitidos_Farmacia ) as 'Vales Emitidos', 
		Sum( E.ValesRegistrados_Farmacia ) as 'Vales Registrados', Sum( E.PiezasEmitidas_Farmacia ) as 'Piezas Emitidas', 
		Sum( E.PiezasRegistradas_Farmacia ) as 'Piezas Registradas', Sum( E.ImporteRegistrado_Farmacia ) as 'Importe Registrado'  
	Into #tmpFarmacias
	From #tmpDatos_Farmacias E(NoLock)  
	Inner Join CatFarmacias F(NoLock) On ( F.IdEstado = @IdEstado And E.IdFarmacia = F.IdFarmacia )  
	Group By F.IdFarmacia, F.NombreFarmacia
	Order By Sum( ValesEmitidos_Farmacia ) Desc
	
	-----------------------------------------
	-- Se obtiene el concentrado de Claves --
	-----------------------------------------
	Select Distinct ClaveSSA,  ValesEmitidos_Clave, ValesRegistrados_Clave,  PiezasEmitidas_Clave, PiezasRegistradas_Clave,  ImporteRegistrado_Clave
	Into #tmpDatos_Claves
	From ADMI_Vales_Estadisticas E(NoLock)  
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And Año = @iAño And Mes = @iMes 
	Order By ValesEmitidos_Clave Desc 

	-- Se obtiene la sumatoria por Clave
	Select	E.ClaveSSA, F.Descripcion as 'Descripción Clave',  Sum( E.ValesEmitidos_Clave ) as 'Vales Emitidos', 
			Sum( E.ValesRegistrados_Clave ) as 'Vales Registrados',  Sum( E.PiezasEmitidas_Clave ) as 'Piezas Emitidas', 
			Sum( E.PiezasRegistradas_Clave ) as 'Piezas Registradas',  Sum( E.ImporteRegistrado_Clave ) as 'Importe Registrado'  
	Into #tmpClaves
	From #tmpDatos_Claves E(NoLock)  
	Inner Join CatClavesSSA_Sales F(NoLock) On ( E.ClaveSSA = F.ClaveSSA)  
	Group By E.ClaveSSA, F.Descripcion
	Order By Sum( ValesEmitidos_Clave ) Desc 

	-------------------------------------------
	-- Se obtiene el concentrado de Perdidas -- 
	-------------------------------------------
	Select	E.ClaveSSA, F.Descripcion as 'Descripción Clave', PrecioLicitacion_Clave as 'Precio de Licitacion',  
			CostoMinimo_Clave as 'Costo Minimo de Compra por Vale',   CostoMaximo_Clave as 'Costo Maximo de Compra por Vale' 
	Into #tmpPerdidas
	From ADMI_Vales_Estadisticas E(NoLock)  
	Inner Join CatClavesSSA_Sales F(NoLock) On ( E.ClaveSSA = F.ClaveSSA)  
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.Año = @iAño And E.Mes = @iMes And E.CostoMinimo_Clave > E.PrecioLicitacion_Clave  
	Group By E.ClaveSSA, F.Descripcion, PrecioLicitacion_Clave, CostoMinimo_Clave, CostoMaximo_Clave  
	Order By CostoMaximo_Clave Desc 
	
	----------------------------------------------
	-- Se obtiene el concentrado de Proveedores -- 
	----------------------------------------------
	Select Distinct IdProveedor, ValesRegistrados_Proveedor, PiezasRegistradas_Proveedor, ImporteRegistrado_Proveedor
	Into #tmpDatos_Proveedores
	From ADMI_Vales_Estadisticas(NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And Año = @iAño And Mes = @iMes 
	Order By ImporteRegistrado_Proveedor Desc 

	Select	F.IdProveedor, F.Nombre as Proveedor, Sum( E.ValesRegistrados_Proveedor ) as 'Vales Registrados',  
			Sum( E.PiezasRegistradas_Proveedor ) as 'Piezas Registradas',  Sum( E.ImporteRegistrado_Proveedor ) as 'Importe Registrado' 
	Into #tmpProveedores
	From #tmpDatos_Proveedores E(NoLock)  
	Inner Join CatProveedores F(NoLock) On ( E.IdProveedor = F.IdProveedor )  
	Group By F.IdProveedor, F.Nombre 
	Order By Sum( ImporteRegistrado_Proveedor ) Desc 

	---------------------------------
	-- Se devuelven los resultados -- 
	---------------------------------
	Set @sSql = 'Select * From #tmpConcentrado(NoLock) ' + 
				'Select Top ' + @sTopReporte + ' * From #tmpFarmacias(NoLock) Order By ' + Char(39) + 'Vales Emitidos' + Char(39) + ' Desc ' + 
				'Select Top ' + @sTopReporte + ' * From #tmpClaves(NoLock) Order By ' + Char(39) + 'Vales Emitidos' + Char(39) + ' Desc ' + 
				'Select * From #tmpPerdidas(NoLock) Order By ' + Char(39) + 'Costo Maximo de Compra por Vale' + Char(39) + ' Desc ' + 
				'Select Top ' + @sTopReporte + ' * From #tmpProveedores(NoLock) Order By ' + Char(39) + 'Importe Registrado' + Char(39) + ' Desc ' 

	Exec (@sSql)

End
Go--#SQL

		