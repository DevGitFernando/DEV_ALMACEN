
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_ADMI_Impresion_Productos_Porcentajes_Compras' And xType = 'P' )
	Drop Proc spp_ADMI_Impresion_Productos_Porcentajes_Compras
Go--#SQL

Create Procedure spp_ADMI_Impresion_Productos_Porcentajes_Compras 
With Encryption
As
Begin

	Create Table #tmpPorcentajes
	(
		 IdEmpresa varchar(3) Not Null Default '',
		 Empresa varchar(100) Not Null Default '',
		 IdEstado varchar(2) Not Null Default '',
		 Estado varchar(50) Not Null Default '',
		 IdFarmacia varchar(4) Not Null Default '',
		 Farmacia varchar(50) Not Null Default '',
		 IdClaveSSA_Sal varchar(4) Not Null Default '',
		 ClaveSSA varchar(50) Not Null Default '',
		 DescripcionSal varchar(7500) Not Null Default '',
		 Cantidad numeric(14, 4) Not Null Default 0,
		 CantidadGeneral numeric(14, 4) Not Null Default 0,
		 PorcentajeParticipacion float Not Null Default 0,
		 TipoRotacion smallint Not Null Default 0,
		 FechaImpresion datetime Not Null Default GetDate(),
		 FechaInicial varchar(10) Not Null Default '',
		 FechaFinal varchar(10) Not Null Default ''
	)

	-----------------------------------------
	-- Se obtienen los datos de las Claves --
	-----------------------------------------
		
	-- Se insertan las claves
	Insert Into #tmpPorcentajes ( IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Cantidad, FechaInicial, FechaFinal )
	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Sum(  Ceiling ( ( Cantidad / ContenidoPaquete ) ) ) as Cantidad, FechaInicial, FechaFinal  
	From tmpADMI_Productos_Compras_Estado
	Group By IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, FechaInicial, FechaFinal	

	-- Se obtiene la cantidad total general vendida
	Update #tmpPorcentajes Set CantidadGeneral = ( Select Sum(Cantidad) From #tmpPorcentajes(NoLock) )

	-- Se obtiene el porcentaje de participacion de cada producto
	Update C
	Set PorcentajeParticipacion = ( Cantidad / CantidadGeneral ) * 100
	From #tmpPorcentajes C 
	
	-------------------------------------------------------
	-- Se inserta el resultado en una tabla del servidor --
	-------------------------------------------------------	

	If Exists ( Select Name From SysObjects(NoLock) Where Name = 'tmpADMI_Productos_Compras_Porcentajes_Estado' And xType = 'U' )
	  Begin
		Drop Table tmpADMI_Productos_Compras_Porcentajes_Estado
	  End	

	Select *, identity(int, 1, 1) as Keyx   
	Into tmpADMI_Productos_Compras_Porcentajes_Estado
	From #tmpPorcentajes(NoLock)
	Order By PorcentajeParticipacion Desc 

	----------------------------------------
	-- Se devuelven los datos del reporte --
	----------------------------------------

	-- Select * From tmpADMI_Productos_Compras_Porcentajes_Estado(NoLock) Order By PorcentajeParticipacion Desc, IdFarmacia, DescripcionSal

End
Go--#SQL

