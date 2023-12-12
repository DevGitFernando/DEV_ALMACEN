------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__012__Vales_Emitidos_y_Surtidos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__012__Vales_Emitidos_y_Surtidos 
Go--#SQL 

Create Proc spp_BI_RPT__012__Vales_Emitidos_y_Surtidos 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '12', @ClaveSSA varchar(20) = '', 
	@Benefeciario varchar(200) = '', @Poliza varchar(200) = '', 
	@FechaInicial varchar(10) = '2015-10-01', @FechaFinal varchar(10) = '2015-11-30' 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


------------------------------------------ Generar tablas de catalogos     	   	
	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos  


----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  
	Print @sSql 




----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		B.NombreCompleto as Beneficiario, B.FolioReferencia, P.ClaveSSA, P.DescripcionClave, 
		cast(D.CantidadRecibida as int) as Cantidad, 
		I.NumReceta, convert(varchar(10), I.FechaReceta, 120) as FechaReceta, 
		E.FolioVale, convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		R.Folio as FolioCanjeVale, convert(varchar(10), R.FechaRegistro, 120) as FechaRegistroCanjeVale 
	Into #tmp_ValesEmitidos 
	From Vales_EmisionEnc E (NoLock) 
	Inner Join ValesEnc R (NoLock) 
		On ( E.IdEmpresa = R.IdEmpresa and E.IdEstado = R.IdEstado and E.IdFarmacia = R.IdFarmacia and E.FolioVale = R.FolioVale ) 	
	Inner Join ValesDet D (NoLock) 
		On ( R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmacia and R.Folio = D.Folio ) 
	Inner Join Vales_Emision_InformacionAdicional I (NoLock) 
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVale = I.FolioVale ) 	
	Inner Join vw_Beneficiarios__PRCS B (NoLock) 
		On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = E.IdSubCliente 
			and I.IdBeneficiario = B.IdBeneficiario ) 	
	Inner Join vw_Productos_CodigoEAN__PRCS P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Inner Join #vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  
		and E.IdEmpresa = @IdEmpresa 
		and ( B.NombreCompleto like '%' + @Benefeciario + '%' or FolioReferencia like '%' + @Poliza + '%' ) 
		and P.ClaveSSA like '%' + @ClaveSSA + '%' 		
	----Group by 	
	----	P.ClaveSSA, P.DescripcionClave  


	
--------------------------------- 	Select * From #tmp_ValesEmitidos 
	
	
---------------------		spp_BI_RPT__012__Vales_Emitidos_y_Surtidos  


		
----------------------------------------------------- SALIDA FINAL 
	Select 
		'Folio de vale' = FolioVale, 
		'Fecha emisión de vale' = FechaRegistro, 
		'Folio de canje de vale' = FolioCanjeVale,
		'Fecha de surtimiento vale' = FechaRegistroCanjeVale, 		
		'Nombre del beneficiario' = Beneficiario, 
		'Número de poliza' = FolioReferencia, 
		'Número de receta' = NumReceta, 
		'Fecha de emisión de receta' = FechaReceta, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		--'Presentación' = Presentacion, 
		'Cantidad' = (Cantidad) 
	From #tmp_ValesEmitidos 
	Order By   
		FolioVale, Beneficiario, ClaveSSA  



End 
Go--#SQL 


