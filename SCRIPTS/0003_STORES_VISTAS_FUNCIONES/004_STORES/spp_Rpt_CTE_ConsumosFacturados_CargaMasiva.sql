------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_CTE_ConsumosFacturados_CargaMasiva' And xType = 'P' )
	Drop Proc spp_CTE_ConsumosFacturados_CargaMasiva
Go--#SQL

Create Procedure spp_CTE_ConsumosFacturados_CargaMasiva 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @Cliente varchar(4) = '0023', @IdSubCliente varchar(4) = '0001', 
	@MesesRevision int = 1
) 
With Encryption 	
As 
Begin 
Set Dateformat YMD 
Declare 
	@bCrearTablaBase int 

	Set @bCrearTablaBase = 1   


------------------------------------------ Generar tablas de catalogos     
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__PRCS' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From sysobjects (NoLock) 
		Where Name = 'vw_Productos_CodigoEAN__PRCS' and xType = 'U' and datediff(dd, crDate, getdate()) > 1
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 
	End 
	
	
	If @bCrearTablaBase = 1 
	Begin 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__PRCS' and xType = 'U' ) 
		   Drop Table vw_Productos_CodigoEAN__PRCS 
		   
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Farmacias__PRCS' and xType = 'U' ) 
		   Drop Table vw_Farmacias__PRCS 
 
		   
		Select * 
		Into vw_Productos_CodigoEAN__PRCS  
		From vw_Productos_CodigoEAN  
		
		
		Select * 
		Into vw_Farmacias__PRCS 
		From vw_Farmacias 
		Where IdEstado = @IdEstado
		
	End 	   	
------------------------------------------ Generar tablas de catalogos     	
		   	
		
		   	
	-------------------------------------------- Informacion de Farmacias 
	Select 
		E.IdEmpresa, E.IdCliente, E.IdSubCliente, E.IdEstado, F.Estado, IdJurisdiccion, Jurisdiccion,
		E.IdFarmacia, F.Farmacia, E.FolioVenta, convert(varchar(10), E.FechaRegistro,120) As FechaRegistro,
		Cast('' As varchar(100)) As NumReceta, Cast('' As varchar(20)) As IdBeneficiario, Cast('' As varchar(200)) as Beneficiario,
		Cast('' As varchar(100)) As NumCedula, Cast('' As varchar(400)) As NombreMedico,
		C.IdClaveSSA_Sal, C.ClaveSSA As ClaveSSA_Larga,  C.ClaveSSA_Base As ClaveSSA, C.DescripcionClave as DescripcionClaveSSA, 
		Sum(L.CantidadVendida) As Cantidad, Sum(L.CantidadVendida) As Cantidad_A_Validar, cast(1 as numeric(14,2)) as Multiplo, 
		Cast(0 As Numeric(14,4)) As PrecioUnitario,Cast(0 As Numeric(14,4)) As Importe, D.TasaIva, L.EsConsignacion As EsDeConsignacion,
		Cast('' As varchar(20)) As ClaveSSA_Aux, Cast('' As varchar(max)) As DescripcionClaveSSA_Aux, 
		0 as EsFacturado, Cast('' As Varchar(10)) As FechaDeProcesamiento
	Into #Temp_Rpt_CTE_ConsumosFacturados
	From VentasEnc E (NoLock) 
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioVenta = D.FolioVenta )
	Inner Join VentasDet_Lotes L (NoLock)
		On ( E.IdEmpresa = L.IdEmpresa And E.IdEstado = L.IdEstado And E.IdFarmacia = L.IdFarmacia And E.FolioVenta = L.FolioVenta And
			D.CodigoEAn = L.CodigoEAN )
	Inner Join vw_Productos_CodigoEAN__PRCS C (NoLock) On ( L.CodigoEAN = C.CodigoEAN )
	Inner Join vw_Farmacias__PRCS F (NoLock) On ( E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia )
	Where 
		convert(varchar(10), E.FechaRegistro, 120) >= '2016-03-01' and  
		( 
			E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And  E.IdFarmacia Between '0011' And '0019' And
			E.IdCliente = @Cliente And E.IdSubCliente = @IdSubCliente And		
			convert(varchar(7), E.FechaRegistro, 120) 
			Between convert(varchar(7), DATEADD(MM, -1 * @MesesRevision, GETDATE()), 120) And convert(varchar(7), GETDATE(), 120) 
		) 
		--and E.IdFarmacia = 14 
	Group By E.IdEmpresa, E.IdCliente, E.IdSubCliente, E.IdEstado, F.Estado, IdJurisdiccion, Jurisdiccion, 
		E.IdFarmacia, F.Farmacia, E.FolioVenta, E.FechaRegistro,
		IdClaveSSA_Sal, C.ClaveSSA, C.ClaveSSA_Base, C.DescripcionClave, D.TasaIva, L.EsConsignacion  
	-------------------------------------------- Informacion de Farmacias 


--	select top 1 * from #Temp_Rpt_CTE_ConsumosFacturados 


	-------------------------------------------- Informacion de Almacenes ( Ventas Directas ) 
	Insert Into #Temp_Rpt_CTE_ConsumosFacturados 	
	Select 
		E.IdEmpresa, E.IdCliente, E.IdSubCliente, E.IdEstado, F.Estado, IdJurisdiccion, Jurisdiccion,
		E.IdFarmacia, F.Farmacia, E.FolioVenta, convert(varchar(10), E.FechaRegistro,120) As FechaRegistro,
		Cast('' As varchar(100)) As NumReceta, Cast('' As varchar(20)) As IdBeneficiario, Cast('' As varchar(200)) as Beneficiario,
		Cast('' As varchar(100)) As NumCedula, Cast('' As varchar(400)) As NombreMedico,
		C.IdClaveSSA_Sal, C.ClaveSSA As ClaveSSA_Larga,  C.ClaveSSA_Base As ClaveSSA, C.DescripcionClave as DescripcionClaveSSA, 
		Sum(L.CantidadVendida) As Cantidad, Sum(L.CantidadVendida) As Cantidad_A_Validar, cast(1 as numeric(14,2)) as Multiplo, 
		Cast(0 As Numeric(14,4)) As PrecioUnitario, Cast(0 As Numeric(14,4)) As Importe, D.TasaIva, L.EsConsignacion As EsDeConsignacion,
		Cast('' As varchar(20)) As ClaveSSA_Aux, Cast('' As varchar(max)) As DescripcionClaveSSA_Aux,
		0 as EsFacturado, Cast('' As Varchar(10)) As FechaDeProcesamiento  
	From VentasEnc E (NoLock) 
	Inner Join VentasDet D (NoLock)
		On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioVenta = D.FolioVenta)
	Inner Join VentasDet_Lotes L (NoLock)
		On (E.IdEmpresa = L.IdEmpresa And E.IdEstado = L.IdEstado And E.IdFarmacia = L.IdFarmacia And E.FolioVenta = L.FolioVenta And
			D.CodigoEAn = L.CodigoEAN)
	Inner Join vw_Productos_CodigoEAN__PRCS C (NoLock) On ( L.CodigoEAN = C.CodigoEAN )
	Inner Join vw_Farmacias__PRCS F (NoLock) On ( E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia )
	Where 
		convert(varchar(10), E.FechaRegistro, 120) >= '2016-03-01' and  
		( 	
			E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And  
			E.IdFarmacia Between '0002' And '0002' 
			and IdPrograma in ( '0086' ) and IdSubPrograma in ( '0001' ) 
			And E.IdCliente = @Cliente And E.IdSubCliente = @IdSubCliente And		
			convert(varchar(7), E.FechaRegistro, 120) 
			Between convert(varchar(7), DATEADD(MM, -1 * @MesesRevision, GETDATE()), 120) And convert(varchar(7), GETDATE(), 120)  
		) 
		--and E.IdFarmacia = 14 		
	Group By E.IdEmpresa, E.IdCliente, E.IdSubCliente, E.IdEstado, F.Estado, IdJurisdiccion, Jurisdiccion, 
		E.IdFarmacia, F.Farmacia, E.FolioVenta, E.FechaRegistro,
		IdClaveSSA_Sal, C.ClaveSSA, C.ClaveSSA_Base, C.DescripcionClave, D.TasaIva, L.EsConsignacion  
	-------------------------------------------- Informacion de Almacenes ( Ventas Directas ) 
		

-----------------------------------------	Completar la información de Mascara y Multiplos 
	Update T Set ---- T.ClaveSSA_Aux = IsNull(M.Mascara, T.ClaveSSA_Aux), 
		T.DescripcionClaveSSA = IsNull(M.DescripcionCorta, T.DescripcionClaveSSA)
		--, Multiplo = M.Multiplo 
	From #Temp_Rpt_CTE_ConsumosFacturados T (Nolock) 
	Inner Join CFG_ClaveSSA_Mascara M (Nolock) 
		On ( T.IdEstado = M.IdEstado and M.IdCliente = T.IdCliente and M.IdSubCliente = T.IdSubCliente 
			and T.IdClaveSSA_Sal = M.IdClaveSSA and M.Status = 'A' )	
			
	
	
	Update T Set T.Multiplo = C.Multiplo
	From #Temp_Rpt_CTE_ConsumosFacturados T (Nolock)
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On (C.IdEstado = T.IdEstado and C.IdClaveSSA_Relacionada = T.IdClaveSSA_Sal)
	Where C.Status = 'A' 
	
	
	Update D Set Cantidad_A_Validar = ceiling((Cantidad / Multiplo)) 
	From #Temp_Rpt_CTE_ConsumosFacturados D 			
-----------------------------------------	Completar la información de Mascara y Multiplos 


-----------------------------------------	Completar la información de Catalogos 		
	Update R  
		Set R.NumReceta = I.NumReceta, R.IdBeneficiario = I.IdBeneficiario, R.NumCedula = M.NumCedula, R.NombreMedico = M.NombreCompleto,
			R.FechaDeProcesamiento = Convert(Varchar(10), I.FechaReceta,120)
	From #Temp_Rpt_CTE_ConsumosFacturados R
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( R.IdEmpresa = I.IdEmpresa and R.IdEstado = I.IdEstado and R.IdFarmacia = I.IdFarmacia and R.FolioVenta = I.FolioVenta ) 
	Inner Join vw_medicos M (NoLock) On ( I.IdEstado = M.IdEstado and I.IdFarmacia = M.IdFarmacia and I.IdMedico = M.IdMedico )

	Update R 
		Set R.Beneficiario = (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre), IdBeneficiario = FolioReferencia 
	From #Temp_Rpt_CTE_ConsumosFacturados R
	Inner Join CatBeneficiarios B (NoLock) 
		On ( R.IdEstado = B.IdEstado and R.IdFarmacia = B.IdFarmacia and R.IdCliente = B.IdCliente 
				and R.IdSubCliente = B.IdSubCliente and R.IdBeneficiario = B.IdBeneficiario ) 
	
	Update R 
		Set R.PrecioUnitario = cast(round(((p.Precio / (S.ContenidoPaquete * 1.0000)) * Multiplo), 4) as numeric(14, 4)),
		R.Importe = (R.Cantidad * cast(round(((p.Precio / (S.ContenidoPaquete * 1.0000))), 4) as numeric(14, 4)))
	From #Temp_Rpt_CTE_ConsumosFacturados R
	Inner Join CFG_ClavesSSA_Precios P
		On ( R.IdEstado = P.IdEstado and R.IdCliente = P.IdCliente and R.IdSubCliente = P.IdSubCliente and R.IdClaveSSA_Sal = P.IdClaveSSA_Sal )
	Inner Join CatClavesSSA_Sales S (NoLock) On ( p.IdClaveSSA_Sal = S.IdClaveSSA_Sal )
	Where P.Status = 'A'
 
	--SELECT * FROM #Temp_Rpt_CTE_ConsumosFacturados where Multiplo > 1 NumReceta = 'SAD-90698-4417' And ClaveSSA like '%4483%'
		
	Update R 
		Set R.ClaveSSA_Aux = C.Clave_Cliente
	From #Temp_Rpt_CTE_ConsumosFacturados R
	Inner Join CTE_CFG_CB_CAPREPA C (NoLock) On ( R.IdEmpresa = C.IdEmpresa And R.IdEstado = C.IdEstado and R.ClaveSSA_Larga = C.ClaveSSA )
	Where ClaveSSA_Aux = '' 

	Update R 
		Set R.ClaveSSA_Aux = C.Clave_Cliente 
	From #Temp_Rpt_CTE_ConsumosFacturados R
	Inner Join CTE_CFG_CB_CAPREPA C (NoLock) On ( R.IdEmpresa = C.IdEmpresa And R.IdEstado = C.IdEstado and R.ClaveSSA = C.ClaveSSA )
	Where ClaveSSA_Aux = '' 
	
	Update R 
		Set R.ClaveSSA_Aux = R.ClaveSSA_Larga 
	From #Temp_Rpt_CTE_ConsumosFacturados R
	Where ClaveSSA_Aux = ''	
-----------------------------------------	Completar la información de Catalogos 			
	
	
	
----------------------------------------- REVISAR LO FACTURADO 	  
	Update T Set EsFacturado = 1 
	From Rpt_CTE_ConsumosFacturados R ( NoLock) 
	Inner Join #Temp_Rpt_CTE_ConsumosFacturados T (NoLock) 
		On ( R.IdEstado = T.IdEstado and R.IdFarmacia = T.IdFarmacia And R.FolioVenta = T.FolioVenta And R.ClaveSSA_Aux = T.ClaveSSA_Aux )
	Where R.EsFacturado = 1 	
	
	Delete R 
	From Rpt_CTE_ConsumosFacturados R
	Inner Join #Temp_Rpt_CTE_ConsumosFacturados T (NoLock) 
		On ( R.IdEstado = T.IdEstado and R.IdFarmacia = T.IdFarmacia And R.FolioVenta = T.FolioVenta And R.ClaveSSA_Aux = T.ClaveSSA_Aux )
	Where R.EsFacturado = 0 
	
	------Delete R 
	------From #Temp_Rpt_CTE_ConsumosFacturados R
	------Inner Join Rpt_CTE_ConsumosFacturados T (NoLock)
	------	On ( R.IdEstado = T.IdEstado and R.IdFarmacia = T.IdFarmacia And R.FolioVenta = T.FolioVenta And R.ClaveSSA_Aux = T.ClaveSSA_Aux )
	------Where EsFacturado = 1 
----------------------------------------- REVISAR LO FACTURADO 	  


	----select top 100 
	----	CHAR(39) + ClaveSSA_Aux + CHAR(39), 
	----	CHAR(39) + IdEstado + CHAR(39), 
	----	CHAR(39) + IdFarmacia + CHAR(39), 
	----	CHAR(39) + FolioVenta + CHAR(39), 		
	----* 
	----from Rpt_CTE_ConsumosFacturados (nolock) 
	----where Idfarmacia = 14 and Folioventa = '00091624'  

	----select top 100 
	----	CHAR(39) + ClaveSSA_Aux + CHAR(39), 
	----	CHAR(39) + IdEstado + CHAR(39), 
	----	CHAR(39) + IdFarmacia + CHAR(39), 
	----	CHAR(39) + FolioVenta + CHAR(39), 		
	----* 
	----from #Temp_Rpt_CTE_ConsumosFacturados (nolock) 
	----where Idfarmacia = 14 and Folioventa = '00091624'  

	----select * 
	----From Rpt_CTE_ConsumosFacturados R( NoLock) 
	----Left Join #Temp_Rpt_CTE_ConsumosFacturados T (NoLock) 
	----	On ( R.IdEstado = T.IdEstado and R.IdFarmacia = T.IdFarmacia And R.FolioVenta = T.FolioVenta And R.ClaveSSA_Aux = T.ClaveSSA_Aux )
	----Where R.EsFacturado = 1 and R.IdFarmacia = 14 and R.FolioVenta = '00091624'

--		spp_CTE_ConsumosFacturados_CargaMasiva	


	----if exists ( select * from sysobjects (nolock) where name = 'tmp___Rpt_CTE_ConsumosFacturados' and xtype = 'U' ) 
	----   drop table tmp___Rpt_CTE_ConsumosFacturados 
	   
	--------------- Insercion de datos 
	Insert Into Rpt_CTE_ConsumosFacturados (IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, FolioVenta, FechaRegistro,
		FechaDeProcesamiento, NumReceta, IdBeneficiario, Beneficiario, NumCedula, NombreMedico,
		ClaveSSA, DescripcionClaveSSA, Cantidad, PrecioUnitario,Importe, TasaIva, EsDeConsignacion, EsFacturado, ClaveSSA_Aux, DescripcionClaveSSA_Aux)
	Select IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, FolioVenta, FechaRegistro,
		 FechaDeProcesamiento, NumReceta, IdBeneficiario, Beneficiario, NumCedula, NombreMedico,
		ClaveSSA_Larga, DescripcionClaveSSA, 
		Cantidad_A_Validar as Cantidad, 
		PrecioUnitario,Importe, TasaIva, EsDeConsignacion, EsFacturado, ClaveSSA_Aux, DescripcionClaveSSA	
	From #Temp_Rpt_CTE_ConsumosFacturados 
	where -- YEAR(FechaRegistro) = 2016 and MONTH(FechaRegistro) = 1 
		EsFacturado = 0 and Cantidad_A_Validar > 0  
	
	
	
	
End
Go--#SQL


