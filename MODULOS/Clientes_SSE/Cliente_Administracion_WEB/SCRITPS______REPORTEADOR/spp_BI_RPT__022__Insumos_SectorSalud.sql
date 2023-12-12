------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__022__Insumos_SectorSalud' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__022__Insumos_SectorSalud
Go--#SQL 


Create Proc spp_BI_RPT__022__Insumos_SectorSalud 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21' 
) 	
With Encryption 
As 
Begin 
Set NoCount On  
Set Dateformat YMD 


	Select ClaveSSA, DescripcionClave, CodigoEAN, Descripcion as NombreComercial, Presentacion, Laboratorio 
	Into #tmp_SectorSalud 
	From vw_Productos_CodigoEAN__PRCS (NoLock)  
	Where EsSectorSalud = 1 


	Select * 
	From #tmp_SectorSalud C (NoLock) 
	Where Exists 
	( 
		Select * 
		From FarmaciaProductos_CodigoEAN E (NoLock) 
		Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and C.CodigoEAN = E.CodigoEAN 
	)  
	Order by ClaveSSA, DescripcionClave 


End	
Go--#SQL 
	
