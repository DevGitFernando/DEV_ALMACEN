-------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_ListadoOC_RecepcionDiaria' And xType = 'P' )  
   Drop Proc spp_Rpt_ListadoOC_RecepcionDiaria
Go--#SQL 

----		Exec spp_Rpt_ListadoOC_RecepcionDiaria '001', '11', '0003'   

Create Procedure spp_Rpt_ListadoOC_RecepcionDiaria 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @EntregarEn varchar(4) = '2182' 
)
As 
Begin
Set DateFormat YMD 


Declare 
	@sSql varchar(8000), 
	@dFechaInicial datetime,  
	@dFechaFinal datetime 


	Set @dFechaInicial = dateadd(dd, 1 * -1, getdate() )
	Set @dFechaFinal = dateadd(dd, 7, getdate() ) 


	Select 
		Folio, IdProveedor, Proveedor, Convert(varchar(10), FechaRequeridaEntrega, 120) as FechaEntrega,
		Count(Distinct ClaveSSA) AS Claves, Sum(Cantidad) as Piezas, Sum(CantidadCajas) as Cajas, Sum(Importe) as Importe
	Into #tmpListadoOC
	FROM vw_Impresion_OrdenesCompras_CodigosEAN (nolock)  
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = '0001' and EntregarEn = @EntregarEn and Status = 'OC'
		and Convert(varchar(10), FechaRequeridaEntrega, 120) 
			between Convert(varchar(10), @dFechaInicial, 120) and Convert(varchar(10), @dFechaFinal, 120) 		
	Group By Folio, IdProveedor, Proveedor, FechaRequeridaEntrega	


	---------------------------- SALIDA  
	Select * From #tmpListadoOC (Nolock)	


End
Go--#SQL

