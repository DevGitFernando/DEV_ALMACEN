-----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_008' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_008 
Go--#SQL 

--		Exec spp_Rpt_Administrativos	
 
Create Proc spp_Rpt_Administrativos_008 
( 
	@IdEstado varchar(2) = '21', @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005' 
)
With Encryption 
As 
Begin 
Set NoCount Off 
Set DateFormat YMD 


/* 
	Select * -- count(*) 
	From RptAdmonDispensacion_NoSurtido  (NoLock)  
	where Folio = '00003651' 

	Select * -- count(*) 
	From tmpRptAdmonDispensacion  (NoLock)  
	where Folio = '00003651' 

	Select * -- count(*) 
	From tmpRptAdmonDispensacion V (NoLock)  
	Inner Join RptAdmonDispensacion_NoSurtido N (NoLock) 
		On ( V.IdEmpresa = N.IdEmpresa and V.IdEstado = N.IdEstado and V.IdFarmacia = N.IdFarmacia
			 and V.Folio = N.Folio and V.ClaveSSA = N.ClaveSSA ) 
*/ 

--		delete from tmpRptAdmonDispensacion Where  IdGrupoPrecios = 3 


------------------------------------ Descontar las Claves con Vale generado   -- Jesús Díaz 2K121218.1715 
---	Validar sólo Emision de Vale 
	Update NS Set Cantidad = NS.Cantidad - DG.Cantidad 
	From RptAdmonDispensacion_NoSurtido NS (NoLock) 
	Inner Join RptAdmonDispensacion_Documentos DG (NoLock) 
		On ( NS.IdEmpresa = DG.IdEmpresa and NS.IdEstado = DG.IdEstado and NS.IdFarmacia = DG.IdFarmacia
			 and NS.Folio = DG.Folio )  

---	Validar Emision de Vale por clave 
	Update NS Set Cantidad = NS.Cantidad - DG.Cantidad 
	From RptAdmonDispensacion_NoSurtido NS (NoLock) 
	Inner Join RptAdmonDispensacion_Documentos DG (NoLock) 
		On ( NS.IdEmpresa = DG.IdEmpresa and NS.IdEstado = DG.IdEstado and NS.IdFarmacia = DG.IdFarmacia
			 and NS.Folio = DG.Folio and NS.ClaveSSA = DG.ClaveSSA )  
	
	Update NS Set Cantidad = 0 
	From RptAdmonDispensacion_NoSurtido NS (NoLock) 
	Where Cantidad < 0 	
	
	Update NS Set ImporteEAN__Negado = ( Cantidad * PrecioLicitacion__Negado ) 
	From RptAdmonDispensacion_NoSurtido NS (NoLock) 	
------------------------------------ Descontar las Claves con Vale generado 


 
	Select ClaveSSA 
	Into #tmpClaves 
	From vw_Claves_Precios_Asignados 
	Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente
	
 
	Update NS Set IdGrupoPrecios = 1 
	From RptAdmonDispensacion_NoSurtido NS 
 
 	Update NS Set IdGrupoPrecios = 2 
	From RptAdmonDispensacion_NoSurtido NS 
	Inner Join #tmpClaves C On ( NS.ClaveSSA = C.ClaveSSA )  
 
 
	Insert Into tmpRptAdmonDispensacion 
	( 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, Folio, FechaSistema, FechaRegistro, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, 
		SubTotal, Descuento, Iva, Total, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, EsConsignacion, EsControlado, Renglon, 
		DescProducto, DescripcionCorta, UnidadDeSalida, 
		TasaIva, Cantidad, PrecioUnitario, PrecioLicitacion, ImporteEAN, ImporteEAN_Licitado, 
		SubTotalLicitacion_0, SubTotalLicitacion, IvaLicitacion, TotalLicitacion, 
		Cantidad__Negado, PrecioLicitacion__Negado, ImporteEAN__Negado, 
		SubTotalLicitacion_0__Negado, SubTotalLicitacion__Negado, IvaLicitacion__Negado, TotalLicitacion__Negado, 
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal ) 
	Select 
		IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdFarmacia, Farmacia, '00' as IdSubFarmacia, 'NO SURTIDO' as SubFarmacia, Folio, FechaSistema, FechaRegistro, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, StatusVenta, 
		SubTotal, Descuento, Iva, Total, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, EsConsignacion, EsControlado, Renglon, 
		DescProducto, DescripcionCorta, UnidadDeSalida, 
		TasaIva, 0 as Cantidad, PrecioUnitario, PrecioLicitacion, 0 as ImporteEAN, 0 as ImporteEAN_Licitado, 
		SubTotalLicitacion_0, SubTotalLicitacion, IvaLicitacion, TotalLicitacion, 
		Cantidad as Cantidad__Negado, PrecioLicitacion__Negado, ImporteEAN__Negado, 
		SubTotalLicitacion_0__Negado, SubTotalLicitacion__Negado, IvaLicitacion__Negado, TotalLicitacion__Negado, 
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal
	From RptAdmonDispensacion_NoSurtido 
	
--		select * from RptAdmonDispensacion_NoSurtido   	

--		sp_listacolumnas  tmpRptAdmonDispensacion  

--		spp_Rpt_Administrativos_008 

--		spp_Rpt_Administrativos  


	

End 
Go--#SQL 

	
	