If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Impresion_CortesParciales' And xType = 'P' )
	Drop Proc spp_Impresion_CortesParciales
Go--#SQL  

Create Procedure spp_Impresion_CortesParciales 
(	
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0002', 
	@FechaDeSistema varchar(10) = '2009-10-12', @IdPersonal varchar(4) = '' -- '0014'   --- , @IdCorte varchar(2) = '02' 
) 
With Encryption   
As
Begin
Set NoCount On 
Set DateFormat YMD 
Declare @FechaSistema datetime, 
		@EsCorte int, 
		@TasaIvaBase int 
		 
Declare @GrupoVentas int, 
		@GrupoDevDelDia int, 
	    @GrupoDevDelDiaAnt int,  
	    @GrupoTiempoAire int  

	Set @GrupoVentas = 1 
	Set @GrupoDevDelDia = 2 
	Set @GrupoDevDelDiaAnt = 3 
	Set @GrupoTiempoAire = 4 
		
	Set @EsCorte = 1 
	Set @FechaSistema = cast(@FechaDeSistema as datetime) 
	Set @IdPersonal = IsNull(@IdPersonal, '') 
	Set @TasaIvaBase = 0 

	--------------------------------------
	-- Obtener la información de Ventas --
	--------------------------------------

	Select top 0 @FechaSistema as FechaSistema, 'Venta' + space(50) as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		0 as TipoDeVenta, cast(@TasaIvaBase as float) as TasaIva, 1 as Grupo, 'Venta' + space(50) as NombreGrupo, space(6) as IdPersonal,  
		identity(int, 1, 1) as Keyx   
	Into #tmpCorteParcial 
 
	--Ventas de Contado
	Insert Into #tmpCorteParcial 
	Select @FechaSistema as FechaSistema, 'Venta de Contado' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		1 as TipoDeVenta, cast(@TasaIvaBase as float) as TasaIva, @GrupoVentas, 'Venta' as NombreGrupo, ''  


	Insert Into #tmpCorteParcial 
	Select FechaSistema, 'Venta de Contado' as TipoMovto, 
		sum(Importe * Cantidad) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		TipoDeVenta, TasaIva, @GrupoVentas, 'Venta' as NombreGrupo, IdPersonal 
	-- Into #tmpCorteParcial 
	From vw_Impresion_Ventas_Contado 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- and IdPersonal = @IdPersonal 
		  and FechaSistema = @FechaSistema and EsCorte = @EsCorte  
	Group by FechaSistema, TipoDeVenta, TasaIva, IdPersonal   	  

	-- Ventas de Credito
	Insert Into #tmpCorteParcial 
	Select @FechaSistema as FechaSistema, 'Venta de Credito' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		2 as TipoDeVenta, cast(@TasaIvaBase as float) as TasaIva, @GrupoVentas, 'Venta' as NombreGrupo, '' 
		
	Insert Into #tmpCorteParcial 
	Select FechaSistema, 'Venta de Credito' as TipoMovto,  
		sum(Importe * Cantidad) as SubTotal, cast(0 as float) as Iva, cast(0 as float) as Total, 
		TipoDeVenta, TasaIva, @GrupoVentas, 'Venta' as NombreGrupo, IdPersonal      
	From vw_Impresion_Ventas_Credito 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- and IdPersonal = @IdPersonal 
		  and FechaSistema = @FechaSistema and EsCorte = @EsCorte  
	Group by FechaSistema, TipoDeVenta, TasaIva, IdPersonal   	  

	--------------------------------------------
	-- Obtener la informacion de Devoluciones --	
	-------------------------------------------- 
	
	Insert Into #tmpCorteParcial 
	Select @FechaSistema as FechaSistema, 'Devoluciones del Dia' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		0 as TipoDeVenta, cast(@TasaIvaBase as float) as TasaIva, @GrupoDevDelDia, 'Devolucion' as NombreGrupo, ''  

	/*************************
	** Devoluciones del dia **
	**************************/

	-- Devoluciones de Contado
	Insert Into #tmpCorteParcial 
	Select @FechaSistema as FechaSistema, 'Devolucion Venta Contado' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		1 as TipoDeVenta, cast(@TasaIvaBase as float) as TasaIva, @GrupoDevDelDia, 'Devoluciones del Dia' as NombreGrupo, ''  


	Insert Into #tmpCorteParcial 
	Select @FechaSistema, 'Devolucion Venta Contado' as TipoMovto, 
		sum(Costo * Cantidad) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		TipoDeVenta, TasaIva, @GrupoDevDelDia, 'Devoluciones del Dia' as NombreGrupo, IdPersonal 
	From vw_DevolucionesDet_CodigosEAN_Ventas 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- and IdPersonal = @IdPersonal 
		  And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema 
		  And Convert( varchar(10), FechaSistema, 120 ) = Convert( varchar(10), FechaSistemaDevol, 120 )
		  and Corte = @EsCorte  
		  And TipoDeDevolucion = 1 And TipoDeVenta = 1
	Group by FechaSistema, TipoDeVenta, TasaIva, IdPersonal  

	-- Devoluciones de Credito
	Insert Into #tmpCorteParcial 
	Select @FechaSistema as FechaSistema, 'Devolucion Venta Credito' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		2 as TipoDeVenta, cast(@TasaIvaBase as float) as TasaIva, @GrupoDevDelDia, 'Devoluciones del Dia' as NombreGrupo, ''  

	Insert Into #tmpCorteParcial 
	Select @FechaSistema, 'Devolucion Venta Credito' as TipoMovto, 
		sum(Costo * Cantidad) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		TipoDeVenta, TasaIva, @GrupoDevDelDia, 'Devoluciones del Dia' as NombreGrupo, IdPersonal 
	From vw_DevolucionesDet_CodigosEAN_Ventas 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- and IdPersonal = @IdPersonal 
		  And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema 
		  And Convert( varchar(10), FechaSistema, 120 ) = Convert( varchar(10), FechaSistemaDevol, 120 )
		  and Corte = @EsCorte  
		  And TipoDeDevolucion = 1 And TipoDeVenta = 2
	Group by FechaSistema, TipoDeVenta, TasaIva, IdPersonal   	  	  

	/************************************
	** Devoluciones de dias anteriores **
	*************************************/

	Insert Into #tmpCorteParcial 
	Select @FechaSistema as FechaSistema, 'Devoluciones de Dias Anteriores' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		0 as TipoDeVenta, cast(@TasaIvaBase as float) as TasaIva, @GrupoDevDelDiaAnt, 'Devoluciones de Dias Anteriores' as NombreGrupo, ''  

	-- Devoluciones de Contado dias anteriores
	Insert Into #tmpCorteParcial 
	Select @FechaSistema as FechaSistema, 'Devolucion Dia Anterior Contado' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		1 as TipoDeVenta, cast(@TasaIvaBase as float) as TasaIva, @GrupoDevDelDiaAnt, 'Devoluciones de Dias Anteriores' as NombreGrupo, ''  


	Insert Into #tmpCorteParcial 
	Select @FechaSistema, 'Devolucion Dia Anterior Contado' as TipoMovto, 
		sum(Costo * Cantidad) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		TipoDeVenta, TasaIva, @GrupoDevDelDiaAnt, 'Devoluciones de Dias Anteriores' as NombreGrupo, IdPersonal 
	From vw_DevolucionesDet_CodigosEAN_Ventas 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- and IdPersonal = @IdPersonal 
		  And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema 
		  And Convert( varchar(10), FechaSistema, 120 ) <> Convert( varchar(10), FechaSistemaDevol, 120 )
		  and Corte = @EsCorte  
		  And TipoDeDevolucion = 1 And TipoDeVenta = 1
	Group by FechaSistema, TipoDeVenta, TasaIva, IdPersonal  

	-- Devoluciones de Credito dias anteriores
	Insert Into #tmpCorteParcial 
	Select @FechaSistema as FechaSistema, 'Devolucion Dia Anterior Credito' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		2 as TipoDeVenta, cast(@TasaIvaBase as float) as TasaIva, @GrupoDevDelDiaAnt, 'Devoluciones de Dias Anteriores' as NombreGrupo, ''  

	Insert Into #tmpCorteParcial 
	Select @FechaSistema, 'Devolucion Dia Anterior Credito' as TipoMovto, 
		sum(Costo * Cantidad) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		TipoDeVenta, TasaIva, @GrupoDevDelDiaAnt, 'Devoluciones de Dias Anteriores' as NombreGrupo, IdPersonal 
	From vw_DevolucionesDet_CodigosEAN_Ventas 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- and IdPersonal = @IdPersonal 
		  And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema 
		  And Convert( varchar(10), FechaSistema, 120 ) <> Convert( varchar(10), FechaSistemaDevol, 120 )
		  and Corte = @EsCorte  
		  And TipoDeDevolucion = 1 And TipoDeVenta = 2
	Group by FechaSistema, TipoDeVenta, TasaIva, IdPersonal   	  	  

	-------------------------------------------------
	-- Calcular los datos en Base a la tasa de IVA --
	-------------------------------------------------
	Update C Set Iva = SubTotal * ((TasaIva / 100.0000)), 
				 Total = SubTotal + (SubTotal * ((TasaIva / 100.0000)) )
	From #tmpCorteParcial C  
	-- Update C Set Total = SubTotal + Iva From #tmpCorteParcial C 
	
		
	-----------------------------------------------------	
	-- Obtener la información de Ventas de Tiempo Aire --
	-----------------------------------------------------

	Insert Into #tmpCorteParcial 
	Select @FechaSistema as FechaSistema, 'Venta TA' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		3 as TipoDeVenta, cast(15 as float) as TasaIva, @GrupoTiempoAire, 'Venta TA' as NombreGrupo, ''  
		
	Insert Into #tmpCorteParcial 
	Select @FechaSistema as FechaSistema, 'Venta TA Contado' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		4 as TipoDeVenta, cast(15 as float) as TasaIva, @GrupoTiempoAire, 'Venta TA' as NombreGrupo, ''   

	Insert Into #tmpCorteParcial 
	Select FechaSistema, 'Venta TA Contado' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, sum(Monto) as Total, 
		4 as TipoDeVenta, 15.0000 as  TasaIva, @GrupoTiempoAire, 'Venta TA' as NombreGrupo, IdPersonal        
	From Ventas_TiempoAire  
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- and IdPersonal = @IdPersonal 
		  and FechaSistema = @FechaSistema and Corte = @EsCorte and TipoTA = 1  	
	Group by FechaSistema, IdPersonal  


	Insert Into #tmpCorteParcial 
	Select @FechaSistema as FechaSistema, 'Venta TA Personal' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, cast(0 as float)  as Total, 
		5 as TipoDeVenta, cast(15 as float) as TasaIva, @GrupoTiempoAire, 'Venta TA' as NombreGrupo, ''  

	Insert Into #tmpCorteParcial 
	Select FechaSistema, 'Venta TA Personal' as TipoMovto, 
		cast(0 as float) as SubTotal, cast(0 as float) as Iva, sum(Monto) as Total, 
		5 as TipoDeVenta, 15.0000 as  TasaIva, @GrupoTiempoAire, 'Venta TA' as NombreGrupo, IdPersonal        
	From Ventas_TiempoAire  
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- and IdPersonal = @IdPersonal 
		  and FechaSistema = @FechaSistema and Corte = @EsCorte and TipoTA = 2  	
	Group by FechaSistema, IdPersonal 

	-------------------------------- 
	-- Actualizar los datos de TA --
	--------------------------------
	Update C Set SubTotal = round(Total / (1 + (TasaIva / 100.0000)), 4),   
				 Iva = Total - round(Total / (1 + (TasaIva / 100.0000)), 4)
	From #tmpCorteParcial C  
	Where TipoDeVenta In ( 4, 5 )
 

	----------------------------
	-- Salida para el Reporte --
	----------------------------
 
	If @IdPersonal <> '' 
	   Update #tmpCorteParcial Set IdPersonal = @IdPersonal Where IdPersonal = '' 
		
		
	Select R.IdEmpresa, C.Nombre as Empresa, C.RFC, 
		   (F.Estado + ', ' + F.Municipio ) as EdoCiudad, 
		   F.Colonia, F.Domicilio, F.CodigoPostal, 
		   R.IdEstado, F.Estado, R.IdFarmacia, F.Farmacia, R.IdPersonal, IsNull(v.NombreCompleto, '') as NombreCompleto, 
		   R.FechaSistema, R.TipoMovto, R.SubTotal, R.Iva, R.Total, R.Grupo, R.NombreGrupo, R.TipoDeVenta, R.TasaIva, R.Keyx, 
		   getdate() as FechaImpresion  
	Into #tmpCorteParcialFinal 	   
	From 
	( 
		Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, IdPersonal,  
			   FechaSistema, TipoMovto, SubTotal, Iva, Total, Grupo, NombreGrupo, TipoDeVenta, TasaIva, Keyx
		From #tmpCorteParcial (NoLock) 
	) R 
	Inner Join CatEmpresas C (NoLock) On ( R.IdEmpresa = C.IdEmpresa )
	Inner Join vw_Farmacias F (NoLock) On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia ) 
	Left Join vw_Personal v (NoLock) On ( R.IdEstado = V.IdEstado and R.IdFarmacia = V.IdFarmacia and R.IdPersonal = V.IdPersonal ) 
	Order by Keyx 


	-- Select @IdPersonal 
	If @IdPersonal <> '' 
	   Delete From #tmpCorteParcialFinal Where IdPersonal <> '' and IdPersonal <> @IdPersonal  

	Select IdEmpresa, Empresa, RFC, EdoCiudad, Colonia, Domicilio, CodigoPostal, 
		   IdEstado, Estado, IdFarmacia, Farmacia, IdPersonal, NombreCompleto, FechaSistema, TipoMovto, 
		   SubTotal, Iva, Total, Grupo, NombreGrupo, TipoDeVenta, TasaIva, Keyx, FechaImpresion 
	From #tmpCorteParcialFinal 
	Order by Keyx 
		
--		spp_Impresion_CortesParciales 

End
Go--#SQL


