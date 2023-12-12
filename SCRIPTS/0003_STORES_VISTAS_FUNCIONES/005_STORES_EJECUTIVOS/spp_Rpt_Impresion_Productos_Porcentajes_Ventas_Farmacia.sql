
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_Impresion_Productos_Porcentajes_Ventas_Farmacia' And xType = 'P' )
	Drop Proc spp_Rpt_Impresion_Productos_Porcentajes_Ventas_Farmacia
Go--#SQL

Create Procedure spp_Rpt_Impresion_Productos_Porcentajes_Ventas_Farmacia ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', 
	@IdFarmacia varchar(4) = '0008',
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-01-31' )
With Encryption
As
Begin

	Create Table #tmpPorcentajes
	(
		 IdEmpresa varchar(3) Not Null Default '',
		 Empresa varchar(100) Not Null Default '',
		 IdFarmacia varchar(4) Not Null Default '',
		 Farmacia varchar(50) Not Null Default '',
		 IdClaveSSA_Sal varchar(4) Not Null Default '',
		 ClaveSSA varchar(50) Not Null Default '',
		 DescripcionSal varchar(7500) Not Null Default '',
		 Cantidad numeric(14, 4) Not Null Default 0,
		 CantidadGeneral numeric(14, 4) Not Null Default 0,
		 PorcentajeParticipacion float Not Null Default 0,
		 TipoRotacion smallint Not Null Default 0,
		 --Keyx int identity(1,1)
	)

	-----------------------------------------
	-- Se obtienen los datos de las Claves --
	-----------------------------------------
		
	-- Se insertan las claves
	Insert Into #tmpPorcentajes ( IdEmpresa, Empresa, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Cantidad )
	Select IdEmpresa, Empresa, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Sum(  Ceiling( ( Cantidad / ContenidoPaquete ) ) ) as Cantidad 
	From tmpRpt_Productos_Ventas_Farmacia
	Group By IdEmpresa, Empresa, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal	

	-- Se obtiene la cantidad total general vendida
	Update #tmpPorcentajes Set CantidadGeneral = ( Select Sum(Cantidad) From #tmpPorcentajes(NoLock) )

	-- Se obtiene el porcentaje de participacion de cada producto
	Update C
	Set PorcentajeParticipacion = ( Cantidad / CantidadGeneral ) * 100
	From #tmpPorcentajes C 
	
	-------------------------------------------------------
	-- Se inserta el resultado en una tabla del servidor --
	-------------------------------------------------------	

	If Exists ( Select Name From SysObjects(NoLock) Where Name = 'tmpRpt_Productos_Ventas_Porcentajes_Farmacia' And xType = 'U' )
	  Begin
		Drop Table tmpRpt_Productos_Ventas_Porcentajes_Farmacia
	  End	

	Select *, identity(int, 1, 1) as Keyx   
	Into tmpRpt_Productos_Ventas_Porcentajes_Farmacia
	From #tmpPorcentajes(NoLock)
	Order By PorcentajeParticipacion Desc

	----------------------------------------
	-- Se devuelven los datos del reporte --
	----------------------------------------

	-- Select * From tmpRpt_Productos_Ventas_Porcentajes_Farmacia(NoLock) Order By PorcentajeParticipacion Desc, IdFarmacia, DescripcionSal

End
Go--#SQL

