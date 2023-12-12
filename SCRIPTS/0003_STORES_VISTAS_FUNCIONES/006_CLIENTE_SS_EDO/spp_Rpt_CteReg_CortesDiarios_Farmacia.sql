
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_CteReg_CortesDiarios_Farmacia' and xType = 'P')
    Drop Proc spp_Rpt_CteReg_CortesDiarios_Farmacia
Go--#SQL
  
-- Set Dateformat YMD Exec  spp_Rpt_CteReg_Existencia_Claves_Farmacia '21', '006', '1182', '1', '1', '2', '1' 
-- Set Dateformat YMD Exec  spp_Rpt_CteReg_Existencia_Claves_Farmacia '21', '006', '1182', '0', '0', '0', '0' 

Create Proc spp_Rpt_CteReg_CortesDiarios_Farmacia 
( 
	@IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '006', @IdFarmacia varchar(4) = '1224', @Fecha varchar(10) = '2013-01-01'
) 
With Encryption 
As 
Begin  
Set DateFormat YMD 


	-- Select * 	into #vw_Productos_CodigoEAN  	From vw_Productos_CodigoEAN 


	Select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioVenta, 
		E.IdPrograma, E.IdSubPrograma, 
		D.IdProducto, D.CodigoEAN, cast(sum(D.CantidadVendida) as int) as Cantidad 
	Into #tmpDetalles 	
	From VentasEnc E (NoLock)  
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Where E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and convert(varchar(10), FechaRegistro, 120) = @Fecha
	Group by 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioVenta, E.IdPrograma, E.IdSubPrograma,  
		D.IdProducto, D.CodigoEAN 


	Select E.IdPrograma, space(200) as Programa, E.IdSubPrograma, space(200) as SubPrograma, 
		P.ClaveSSA, P.DescripcionClave, sum(Cantidad) as Cantidad  
	Into #tmpConcentrado 	
	From #tmpDetalles E 
	Inner Join vw_Productos_CodigoEAN P On ( E.IdProducto = P.IdProducto and E.CodigoEAN = P.CodigoEAN ) 
	Group by 
		E.IdPrograma, E.IdSubPrograma, P.ClaveSSA, P.DescripcionClave


	Update C Set Programa = P.Programa, SubPrograma = P.SubPrograma 
	From #tmpConcentrado C 
	Inner Join vw_Programas_SubProgramas P On ( C.IdPrograma = P.IdPrograma and C.IdSubPrograma = P.IdSubPrograma ) 


----------------------- Salida Final 
	Select 
		'Clave de Programa' = IdPrograma, Programa, 'Clave de SubPrograma' = IdSubPrograma, SubPrograma, 
		'Clave SSA' = ClaveSSA, 'Descripción clave' = DescripcionClave, Cantidad  
	From #tmpConcentrado 
	Order by IdPrograma, IdSubPrograma, DescripcionClave 

--		spp_Rpt_CteReg_CortesDiarios_Farmacia 


End 
Go--#SQL 
