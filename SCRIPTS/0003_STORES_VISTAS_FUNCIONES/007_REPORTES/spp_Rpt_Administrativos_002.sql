If Exists ( select * from sysobjects (nolock) Where Name = 'tmpRptAdmonDispensacion' and xType = 'U' ) 
	Drop table tmpRptAdmonDispensacion 
Go--#SQL  

If Exists ( Select Name From Sysobjects Where Name = 'tmpDatosInicialesLotes' and xType = 'U' )
      Drop Table tmpDatosInicialesLotes
Go--#SQL  

-----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_002' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_002 
Go--#SQL 
 
Create Proc spp_Rpt_Administrativos_002 
( 
	@TipoDispensacion tinyint = 0, 
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-06-30', 
	@TipoInsumoMedicamento tinyint = 0  
)  
With Encryption 
As 
Begin 
Set NoCount Off 
Set DateFormat YMD 

Declare @sIdEstado varchar(2), 
	@dPorcentaje numeric(14,4) 

--- Obtener el Procentaje para los Productos No Licicitados/Conveniados 
	Select Top 1 @sIdEstado = IdEstado From tmpRptAdmonDispensacion (NoLock) 
	Select @dPorcentaje = dbo.fg_ObtenerPorcentajePrecios(@sIdEstado) 

----------------------- FARMACIAS Y CLAVES 
----	Select * 
----	Into #vw_ClavesSSA_Sales 
----	From vw_ClavesSSA_Sales 
----------------------- FARMACIAS Y CLAVES 


----------------------------------  
	Update B Set Empresa = I.Nombre 
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join CatEmpresas I (NoLock) On ( B.IdEmpresa = I.IdEmpresa ) 

	Update B Set IdClaveSSA_Sal = I.IdClaveSSA_Sal, ClaveSSA = I.ClaveSSA, DescripcionSal = I.DescripcionSal, 
		 DescProducto = I.DescripcionClave, DescripcionCorta = I.DescripcionCortaClave, 
		 EsControlado = I.EsControlado  
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join vw_Productos_CodigoEAN I (NoLock) On ( B.IdProducto = I.IdProducto and B.CodigoEAN = I.CodigoEAN ) 

--	Select top 1 * From vw_Productos_CodigoEAN 


----------------------------------  Marcar solo para Precios de Lista 
----	Update B Set UsaPrecioLicitacion = 0 
----	From tmpRptAdmonDispensacion B (NoLock) 
	

----------------- 2K111121.1310 Jesus Diaz 
------------- Se modifica función para que tome el precio por ClaveSSA ( Todas las Claves Relacionadas )  
------- Reemplazo de Claves 
	Update B Set IdClaveSSA_Sal = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionSal = C.Descripcion -- , IdGrupoPrecios = 3 
--	select count(*) 	 
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On ( B.IdEstado = C.IdEstado and B.IdClaveSSA_Sal = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	
------- Reemplazo de Claves 


------- Identificar todas la claves que son tipo Seguro Popular 	
	Update B Set EsSeguroPopular = I.EsSeguroPopular, TituloSeguroPopular = 'SEGURO POPULAR'  
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join CatClavesSSA_SeguroPopular I (NoLock) On ( B.IdClaveSSA_Sal = I.IdClaveSSA ) 

	Update B Set TituloSeguroPopular = 'NO SEGURO POPULAR'  
	From tmpRptAdmonDispensacion B (NoLock) 
	Where EsSeguroPopular  = 2 
	
	
	if @TipoInsumoMedicamento <> 0 
	   Begin
	      if @TipoInsumoMedicamento = 1 
	         Delete from tmpRptAdmonDispensacion Where EsSeguroPopular = 2 
	         
	      if @TipoInsumoMedicamento = 2 
	         Delete from tmpRptAdmonDispensacion Where EsSeguroPopular = 1 	         
	   End 
	
------- Identificar todas la claves que son tipo Seguro Popular 		
	
--- Identificar el Tipo de Insumo 
	Update B Set TipoDeInsumo = 'MEDICAMENTO'	
	From tmpRptAdmonDispensacion B (NoLock) 	
	Where TipoInsumo = 1 

	Update B Set TipoDeInsumo = 'MATERIAL DE CURACIÓN Y OTROS'	
	From tmpRptAdmonDispensacion B (NoLock) 	
	Where TipoInsumo = 2 


----------------
	Update B Set NombreCliente = I.NombreCliente, NombreSubCliente = I.NombreSubCliente 
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join vw_Clientes_SubClientes I (NoLock) On ( B.IdCliente = I.IdCliente and B.IdSubCliente = I.IdSubCliente ) 

	Update B Set Programa = I.Programa, SubPrograma = I.SubPrograma 
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join vw_Programas_SubProgramas I (NoLock) On ( B.IdPrograma = I.IdPrograma and B.IdSubPrograma = I.IdSubPrograma ) 

 
----	Update B Set IdBeneficiario = V.IdBeneficiario, NumReceta = V.NumReceta, FechaReceta = V.FechaReceta, 
----		IdMedico = V.IdMedico, Medico = I.NombreCompleto, 
----		IdServicio = V.IdServicio, IdArea = V.IdArea, 
----		IdDiagnostico = right('00000000' + V.IdDiagnostico, 6)      
----	From tmpRptAdmonDispensacion B (NoLock) 
----	Inner Join VentasInformacionAdicional V (NoLock) 
----		on ( B.IdEmpresa = V.IdEmpresa and B.IdEstado = V.IdEstado and B.IdFarmacia = V.IdFarmacia and B.Folio = V.FolioVenta )
----	Inner Join vw_Medicos I (NoLock) On ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia and V.IdMedico = I.IdMedico ) 
----
----	Update B Set Beneficiario = I.NombreCompleto, FolioReferencia = I.FolioReferencia  
----	From tmpRptAdmonDispensacion B (NoLock) 
----	Inner Join vw_Beneficiarios I (NoLock) 
----		on ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia 
----			 and B.IdCliente = I.IdCliente and B.IdSubCliente = I.IdSubCliente and B.IdBeneficiario = I.IdBeneficiario )
----	
----	Update B Set Servicio = I.Servicio, Area = I.Area_Servicio  
----	From tmpRptAdmonDispensacion B (NoLock) 
----	Inner Join vw_Servicios_Areas I (NoLock) On ( B.IdServicio = I.IdServicio and B.IdArea = I.IdArea ) 
----	
----	Update B Set IdGrupo = I.IdGrupo, GrupoClaves = I.GrupoClaves, DescripcionGrupo = I.DescripcionGrupo, 
----		  SubGrupo = I.SubGrupo, ClaveSubGrupo = I.ClaveSubGrupo, DescripcionSubGrupo = I.DescripcionSubGrupo 
----	From tmpRptAdmonDispensacion B (NoLock) 
----	Inner Join vw_CIE10_Diagnosticos I (NoLock) On ( B.IdDiagnostico = I.IdDiagnostico ) 
 		
 		
-------------------------------- Reportes Especiales 	
--- Agregar informacion Adicional 
	Update B Set IdBeneficiario = V.IdBeneficiario, NumReceta = V.NumReceta, FechaReceta = V.FechaReceta, 
		IdMedico = V.IdMedico, -- Medico = I.NombreCompleto, 
		IdServicio = V.IdServicio, IdArea = V.IdArea, 
		-- IdDiagnostico = right('00000000' + V.IdDiagnostico, 6)      
		IdDiagnostico = V.IdDiagnostico 
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join VentasInformacionAdicional V (NoLock) 
		on ( -- B.IdEmpresa = V.IdEmpresa and 
			B.IdEstado = V.IdEstado and B.IdFarmacia = V.IdFarmacia and B.Folio = V.FolioVenta )
	--Inner Join vw_Medicos I (NoLock) On ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia and V.IdMedico = I.IdMedico ) 

--- Datos del Medico 
	Update B Set Medico = I.NombreCompleto  
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join vw_Medicos I (NoLock) On ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia and B.IdMedico = I.IdMedico ) 

	
---------------- Datos de Beneficiario 
	Select 
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, FolioReferencia, 
		Nombre, ApPaterno, ApMaterno, ltrim(rtrim( (ApPaterno + ' ' + ApMaterno + ' ' + Nombre))) as NombreCompleto  
	Into #tmpBeneficiarios 	
	From CatBeneficiarios B (NoLock) 
	Where Exists ( Select IdEstado, IdFarmacia From tmpRptAdmonDispensacion T (NoLock) 
		Where T.IdEstado = B.IdEstado and T.IdFarmacia = B.IdFarmacia )
	
	Update B Set Beneficiario = I.NombreCompleto, FolioReferencia = I.FolioReferencia  
	From tmpRptAdmonDispensacion B (NoLock) 
	-- Inner Join vw_Beneficiarios I (NoLock) 
	Inner Join #tmpBeneficiarios I (NoLock) 	
		on ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia 
			 and B.IdCliente = I.IdCliente and B.IdSubCliente = I.IdSubCliente and B.IdBeneficiario = I.IdBeneficiario ) 	
---------------- Datos de Beneficiario 

	
	
	Update B Set Servicio = I.Servicio, Area = I.Area_Servicio  
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join vw_Servicios_Areas I (NoLock) On ( B.IdServicio = I.IdServicio and B.IdArea = I.IdArea ) 
	
	
	
	Update B Set IdGrupo = I.IdGrupo, GrupoClaves = I.GrupoClaves, DescripcionGrupo = I.DescripcionGrupo, 
		  SubGrupo = I.SubGrupo, ClaveSubGrupo = I.ClaveSubGrupo, DescripcionSubGrupo = I.DescripcionSubGrupo, 
		  ClaveDiagnostico = I.ClaveDiagnostico, Diagnostico = I.Diagnostico  	
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join vw_CIE10_Diagnosticos I (NoLock) On ( B.IdDiagnostico = I.ClaveDiagnostico )  
	

	Update B Set IdDiagnostico = '0001' 
	From tmpRptAdmonDispensacion B (NoLock) 
	-- Inner Join vw_CIE10_Diagnosticos I (NoLock) On ( B.IdDiagnostico = I.ClaveDiagnostico ) 	
	Where B.Diagnostico = '' 

	Update B Set IdGrupo = I.IdGrupo, GrupoClaves = I.GrupoClaves, DescripcionGrupo = I.DescripcionGrupo, 
		  SubGrupo = I.SubGrupo, ClaveSubGrupo = I.ClaveSubGrupo, DescripcionSubGrupo = I.DescripcionSubGrupo, 
		  ClaveDiagnostico = I.ClaveDiagnostico, Diagnostico = I.Diagnostico  	
	From tmpRptAdmonDispensacion B (NoLock) 
	Inner Join vw_CIE10_Diagnosticos I (NoLock) On ( B.IdDiagnostico = I.ClaveDiagnostico )  
	Where B.Diagnostico = '' 	
-------------------------------- Reportes Especiales 	 		

--	  select top 10 * from tmpRptAdmonDispensacion (nolock) where IdGrupoPrecios = 1 
	
		
----
--- Asignar Precio 
	Update B Set PrecioLicitacion = IsNull(PC.Precio, 0) -- , IdGrupoPrecios = 1, DescripcionGrupoPrecios = '' 
	     , IdGrupoPrecios = (case when IsNull(PC.Precio, 0) <= 0 Then 1 Else 2 End), 
		 DescripcionGrupoPrecios = (case when IsNull(PC.Precio, 0)  <= 0 Then 'SIN PRECIO DE LICITACION' Else 'PRECIO LICITACION ASIGNADO' End) 
		 , DescProducto = (case when IsNull(PC.Precio, 0) <= 0 Then '.' + DescProducto Else DescProducto End)
		 , DescripcionCorta = (case when IsNull(PC.Precio, 0) <= 0 Then '.' + DescripcionCorta Else DescripcionCorta End)		 

	From tmpRptAdmonDispensacion B (NoLock) 
	Left Join vw_Claves_Precios_Asignados PC (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
		On ( B.IdEstado = PC.IdEstado and B.IdCliente = PC.IdCliente and B.IdSubCliente = PC.IdSubCliente 
		     -- and B.IdClaveSSA_Sal = PC.IdClaveSSA 
		     and B.ClaveSSA = PC.ClaveSSA 
		    ) 

-- between convert(varchar(10), @FechaInicial, 120) and convert(varchar(10), @FechaFinal, 120)

------  Marcar los Precios 		
	Update B Set FechaInicial = cast(@FechaInicial as datetime),  FechaFinal = cast(@FechaFinal as datetime), 
		 ImporteEAN = (Cantidad * PrecioLicitacion), 
	     IdGrupoPrecios	= (case when PrecioLicitacion <= 0 Then 1 Else 2 End), 
		 DescripcionGrupoPrecios = (case when PrecioLicitacion <= 0 Then 'SIN PRECIO DE LICITACION' Else 'PRECIO LICITACION ASIGNADO' End)
	From tmpRptAdmonDispensacion B (NoLock) 		
	-- Where IdGrupoPrecios <> 1 


-------------------------- Jesus Diaz 2K111122.1628 
-------------------- Habilitar solo cuando se manejen Precios de Venta para Dispensación 
-------------   Asignar Precios a Productos Faltantes 
--------	Update B Set PrecioLicitacion = (PrecioUnitario * ( 1 + (@dPorcentaje / 100.00)) ), 
--------		 ImporteEAN = (Cantidad * (PrecioUnitario * ( 1 + (@dPorcentaje / 100.00)) ))  
--------	From tmpRptAdmonDispensacion B (NoLock) 
--------	Where IdGrupoPrecios = 1 		 
-------------   Asignar Precios a Productos Faltantes 


---------------------------------------------------------------------------------------------------------
------------------- Informacion adicional para Validación y Facturacion 
	Update B Set SubTotalLicitacion_0 = ImporteEAN 
	From tmpRptAdmonDispensacion B 
	Where TipoInsumo = 1 
	
	Update B Set SubTotalLicitacion = ImporteEAN 
	From tmpRptAdmonDispensacion B 
	Where TipoInsumo = 2 	
	
	Update B Set IvaLicitacion = SubTotalLicitacion * ( (TasaIva / 100.00) )
	From tmpRptAdmonDispensacion B 
	Where TipoInsumo = 2 	
	
	Update B Set TotalLicitacion = SubTotalLicitacion_0 +  ( SubTotalLicitacion + IvaLicitacion )
	From tmpRptAdmonDispensacion B 	

--- Reporte Ambos asegurar q solo Venta muestre precios 		
	Update B Set 
		PrecioUnitario = 0, PrecioLicitacion = 0, ImporteEAN = 0,  
		SubTotalLicitacion_0 = 0 , SubTotalLicitacion = 0, IvaLicitacion = 0, TotalLicitacion = 0 
	From tmpRptAdmonDispensacion B 		
	Where EsConsignacion = 1 
------------------- Informacion adicional para Validación y Facturacion 
--------------------------------------------------------------------------------------------------------- 

--- No asignar ningun precio a lo no Licitado 	
    Update B Set 
        ImporteEAN = 0, PrecioLicitacion = 0, 
	    SubTotalLicitacion_0 = 0, SubTotalLicitacion = 0, 
	    IvaLicitacion = 0, TotalLicitacion = 0  
    From tmpRptAdmonDispensacion B (NoLock) 
    Where IdGrupoPrecios = 1     
----- Informacion adicional para Validación y Facturacion 	

----------------------------------------- Se pasa este proceso al store spp_Rpt_Administrativos_008 
----------------------------------------- Reemplazo de Titulos	
----------	Update B Set IdCliente = '0001', 
----------		NombreCliente = R.Cliente, NombreSubCliente = R.SubCliente, Programa = R.Programa, SubPrograma = R.SubPrograma
----------	From tmpRptAdmonDispensacion B (NoLock) 
----------	Inner Join CFG_EX_Validacion_Titulos R (NoLock) 
----------		On (
----------				B.IdEstado = R.IdEstado and B.IdCliente = R.IdCliente and B.IdSubCliente = R.IdSubCliente 
----------				and B.IdPrograma = R.IdPrograma and B.IdSubPrograma = R.IdSubPrograma 
----------		   )
----------------------------------------- Reemplazo de Titulos
----------
----------------------------------------- Reemplazo de Nombre de Beneficiario	
----------	Update B Set 
----------		FolioReferencia = (case when R.ReemplazarFolioReferencia = 1 Then R.FolioReferencia Else B.FolioReferencia End),  
----------		Beneficiario = (case when R.ReemplazarBeneficiario = 1 Then R.Beneficiario Else B.Beneficiario End) 		
----------	From tmpRptAdmonDispensacion B (NoLock) 
----------	Inner Join CFG_EX_Validacion_Titulos_Beneficiarios R (NoLock) 
----------		On (
----------				B.IdEstado = R.IdEstado and B.IdCliente = R.IdCliente and B.IdSubCliente = R.IdSubCliente 
----------				and B.IdPrograma = R.IdPrograma and B.IdSubPrograma = R.IdSubPrograma 
----------		   )
----------------------------------------- Reemplazo de Nombre de Beneficiario	

	
	If @TipoDispensacion = 1 ----  Consignacion 
	   Begin 
	       Update B Set 
	            IdGrupoPrecios = 1, DescripcionGrupoPrecios = 'PRECIO LICITACION ASIGNADO',  
	            ImporteEAN = 0, PrecioLicitacion = 0, 
				SubTotalLicitacion_0 = 0, SubTotalLicitacion = 0, 
				IvaLicitacion = 0, TotalLicitacion = 0  
	       From tmpRptAdmonDispensacion B (NoLock) 
	   End 


------------------------------------------------------ QUITAR LAS DEVOLUCIONES  
	
	Delete From tmpRptAdmonDispensacion Where Cantidad <= 0 
	
------------------------------------------------------ QUITAR LAS DEVOLUCIONES  


------------------------------------------------------ AJUSTE POR CLAVES 
--------------------- 	
	if exists ( select * from sysobjects (nolock) Where Name = 'tmpRptAdmonDispensacion_Detallado' and xType = 'U' ) 
	   Drop table tmpRptAdmonDispensacion_Detallado  
	   
	Select * 
	Into tmpRptAdmonDispensacion_Detallado 
	From tmpRptAdmonDispensacion  
--------------------- 	

	Select 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, 
		sum(SubTotal) as SubTotal, sum(Descuento) as Descuento, sum(Iva) as Iva, sum(Total) as Total, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, '' as IdProducto, '' as CodigoEAN, 
		EsConsignacion, EsControlado, 
		0 as Renglon, 
		DescProducto, DescripcionCorta, UnidadDeSalida, 
		max(TasaIva) as TasaIva, sum(Cantidad) as Cantidad, 
		PrecioUnitario, PrecioLicitacion, 
		sum(ImporteEAN) as ImporteEAN, sum(ImporteEAN_Licitado) as ImporteEAN_Licitado, 
		sum(SubTotalLicitacion_0) as SubTotalLicitacion_0, sum(SubTotalLicitacion) as SubTotalLicitacion, 
		sum(IvaLicitacion) as IvaLicitacion, sum(TotalLicitacion) as TotalLicitacion, 
		sum(Cantidad__Negado) as Cantidad__Negado, 
		PrecioLicitacion as PrecioLicitacion__Negado,      	
		sum(Cantidad__Negado * PrecioLicitacion) as ImporteEAN__Negado, 		
		sum(SubTotalLicitacion_0__Negado) as SubTotalLicitacion_0__Negado, 
		sum(SubTotalLicitacion__Negado) as SubTotalLicitacion__Negado, 
		sum(IvaLicitacion__Negado) as IvaLicitacion__Negado, sum(TotalLicitacion__Negado) as TotalLicitacion__Negado, 		
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal 
	Into #tmpRptAdmonDispensacion 
	From tmpRptAdmonDispensacion 
	Group by 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, 
		-- SubTotal, Descuento, Iva, Total, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, -- IdProducto, CodigoEAN, 
		EsConsignacion, EsControlado, 
		DescProducto, DescripcionCorta, UnidadDeSalida, 
		TasaIva, -- Cantidad, 
		PrecioUnitario, PrecioLicitacion, 
		-- Cantidad__Negado, SubTotalLicitacion_0__Negado, SubTotalLicitacion__Negado, IvaLicitacion__Negado, TotalLicitacion__Negado, 
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal 	
			
			
	if exists ( select * from sysobjects (nolock) Where Name = 'tmpRptAdmonDispensacion' and xType = 'U' ) 
	   Drop table tmpRptAdmonDispensacion 

	Select * 
	Into tmpRptAdmonDispensacion 
	From #tmpRptAdmonDispensacion 
------------------------------------------------------ AJUSTE POR CLAVES 
			
End 
Go--#SQL 


	
	