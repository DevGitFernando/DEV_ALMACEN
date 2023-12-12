------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_IFarmatel___GetServiciosADomicilio__Test' and xType = 'P') 
    Drop Proc spp_INT_IFarmatel___GetServiciosADomicilio__Test 
Go--#SQL 
  
--  ExCB spp_INT_IFarmatel___GetServiciosADomicilio__Test '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_IFarmatel___GetServiciosADomicilio__Test 
(   
    @IdEmpresa varchar(3) = '002', 
    @IdEstado varchar(2) = '09', 
    @IdFarmacia varchar(4) = '0011' 
) 
With Encryption 
As 
Begin 
Set NoCount On  
Set 
DateFormat YMD  


	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000' + @IdFarmacia, 4) 		


---------------------------------------------- Obtener los folios de servicios a domicilio 
	Select 
		OrigenDeServicio, 
		(D.IdEmpresa + D.IdEstado + D.IdFarmacia + D.FolioServicioDomicilio) as UUID, 	
		D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioServicioDomicilio, 
		D.FolioVale, D.FolioVentaGenerado, 
		D.FechaRegistro, D.IdPersonal, D.HoraVisita_Desde, D.HoraVisita_Hasta, D.ServicioConfirmado, 
		D.FechaConfirmacion, D.IdPersonalConfirma, D.TipoSurtimiento, D.ReferenciaSurtimiento, 
		D.Status, D.Actualizado, D.FechaControl, 
		D.PedidoEnviado, D.FechaEnvioPedido, 
		
		I.FolioVenta, 
		I.IdCliente, 
		I.IdSubCliente, 		
						
		cast('' as varchar(20)) as IdMedico, 
		cast('' as varchar(100)) as NombreMedico,
		cast('' as varchar(100)) as ApPaternoMedico, 
		cast('' as varchar(100)) as ApMaternoMedico, 
		cast('' as varchar(100)) as NumCedula, 		
				
		cast('' as varchar(20)) as IdBeneficiario, 		
		cast('' as varchar(20)) as ReferenciaAfiliacion, 
		cast('' as varchar(100)) as NumeroReceta, 
		cast('' as varchar(20)) as FechaReceta, 
		cast('' as varchar(20)) as TipoServicio, 
		cast('' as varchar(100)) as Nombre, 
		cast('' as varchar(100)) as ApPaterno, 
		cast('' as varchar(100)) as ApMaterno, 
		cast('' as varchar(100)) as Telefono, 		
		cast('' as varchar(100)) as TelefonoCasa, 
		cast('' as varchar(100)) as TelefonoMovil, 
		cast('' as varchar(100)) as FechaNacimiento, 			
		cast('' as varchar(100)) as Sexo, 			
			
		cast('' as varchar(100)) as CodigoPostal, 
		cast('' as varchar(100)) as Calle, 
		cast('.' as varchar(100)) as NumeroExterior, 
		cast('.' as varchar(100)) as NumeroInterior, 
		cast('' as varchar(100)) as EntreCalles, 
		cast('' as varchar(100)) as Colonia, 
		cast('' as varchar(100)) as Referencia, 
		cast('' as varchar(100)) as Estado, 
		cast('' as varchar(100)) as DelegacionMunicipio  
	Into #tmp_Servicios 
	From Vales_Servicio_A_Domicilio D (NoLock)  
	Inner Join Vales_EmisionEnc I (NoLock) 
		On ( D.IdEmpresa = I.IdEmpresa and D.IdEstado = I.IdEstado and D.IdFarmacia = I.IdFarmacia and D.FolioVale = I.FolioVale ) 	
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia -- and D.PedidoEnviado = 0  
		and OrigenDeServicio = 1 


	Insert Into #tmp_Servicios 
	Select 
		OrigenDeServicio, 
		(D.IdEmpresa + D.IdEstado + D.IdFarmacia + D.FolioServicioDomicilio) as UUID, 	
		D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioServicioDomicilio, 
		I.FolioVenta, D.FolioVentaGenerado, 
		D.FechaRegistro, D.IdPersonal, D.HoraVisita_Desde, D.HoraVisita_Hasta, D.ServicioConfirmado, 
		D.FechaConfirmacion, D.IdPersonalConfirma, D.TipoSurtimiento, D.ReferenciaSurtimiento, 
		D.Status, D.Actualizado, D.FechaControl, 
		D.PedidoEnviado, D.FechaEnvioPedido, 
		
		I.FolioVenta, 
		I.IdCliente, 
		I.IdSubCliente, 		
						
		cast('' as varchar(20)) as IdMedico, 
		cast('' as varchar(100)) as NombreMedico, 
		cast('' as varchar(100)) as ApPaternoMedico, 
		cast('' as varchar(100)) as ApMaternoMedico, 				
		cast('' as varchar(100)) as NumCedula, 		
				
		cast('' as varchar(20)) as IdBeneficiario, 		
		cast('' as varchar(20)) as ReferenciaAfiliacion, 
		cast('' as varchar(100)) as NumeroReceta, 
		cast('' as varchar(20)) as FechaReceta, 
		cast('' as varchar(20)) as TipoServicio, 
		cast('' as varchar(100)) as Nombre, 
		cast('' as varchar(100)) as ApPaterno, 
		cast('' as varchar(100)) as ApMaterno, 
		cast('' as varchar(100)) as Telefono, 				
		cast('' as varchar(100)) as TelefonoCasa, 
		cast('' as varchar(100)) as TelefonoMovil, 
		cast('' as varchar(100)) as FechaNacimiento, 			
		cast('' as varchar(100)) as Sexo, 			
			
		cast('' as varchar(100)) as CodigoPostal, 
		cast('' as varchar(100)) as Calle, 
		cast('.' as varchar(100)) as NumeroExterior, 
		cast('.' as varchar(100)) as NumeroInterior, 
		cast('' as varchar(100)) as EntreCalles, 
		cast('' as varchar(100)) as Colonia, 
		cast('' as varchar(100)) as Referencia, 
		cast('' as varchar(100)) as Estado, 
		cast('' as varchar(100)) as DelegacionMunicipio   
	From Vales_Servicio_A_Domicilio D (NoLock)  
	Inner Join VentasEnc I (NoLock) 
		On ( D.IdEmpresa = I.IdEmpresa and D.IdEstado = I.IdEstado and D.IdFarmacia = I.IdFarmacia and D.FolioVale = I.FolioVenta ) 	
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.PedidoEnviado = 0  
		and OrigenDeServicio = 2 



	Select 
		FolioServicioDomicilio, 
		cast('' as varchar(100)) as IdProducto, 
		cast('' as varchar(100)) as SKU, 
		IdClaveSSA_Sal, 
		cast('' as varchar(20)) as ClaveSSA, 
		cast('' as varchar(max)) as DescripcionClaveSSA, 
		cast(Cantidad as int) as CantidadRequerida  
	Into #tmp_Detalles 
	from Vales_EmisionDet D (NoLock) 
	Inner Join #tmp_Servicios S (NoLock) 
		On ( D.IdEmpresa = S.IdEmpresa and D.IdEstado = S.IdEstado and D.IdFarmacia = S.IdFarmacia and D.FolioVale = S.FolioVale )
	Where OrigenDeServicio = 1 

	Insert Into #tmp_Detalles 
	Select 
		S.FolioServicioDomicilio, 
		'' IdProducto, '' as SKU, 
		P.IdClaveSSA_Sal, 
		'' as ClaveSSA, '' as DescripcionClaveSSA, 
		cast(sum(D.CantidadVendida) as int) as CantidadRequerida  
	from VentasDet D (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )  
	Inner Join #tmp_Servicios S (NoLock) 
		On ( D.IdEmpresa = S.IdEmpresa and D.IdEstado = S.IdEstado and D.IdFarmacia = S.IdFarmacia and D.FolioVenta = S.FolioVale )
	Where OrigenDeServicio = 2  
	Group by 
		S.FolioServicioDomicilio, P.IdClaveSSA_Sal 
	
	
--		spp_INT_IFarmatel___GetServiciosADomicilio__Test 

---------------------------------------------- Obtener los folios de servicios a domicilio 
--		spp_INT_IFarmatel___GetServiciosADomicilio__Test  


---------------------------------------------- Completar la informacion del Pedido 
	Update S Set IdBeneficiario = I.IdBeneficiario, NumeroReceta = I.NumReceta, FechaReceta = convert(varchar(10), I.FechaReceta, 120), 
		IdMedico = I.IdMedico  
	From #tmp_Servicios S 
	Inner Join Vales_Emision_InformacionAdicional I (NoLock) 
		On ( S.IdEmpresa = I.IdEmpresa and S.IdEstado = I.IdEstado and S.IdFarmacia = I.IdFarmacia and S.FolioVale = I.FolioVale ) 
	Where OrigenDeServicio = 1 
	
	Update S Set IdBeneficiario = I.IdBeneficiario, NumeroReceta = I.NumReceta, FechaReceta = convert(varchar(10), I.FechaReceta, 120), 
		IdMedico = I.IdMedico  
	From #tmp_Servicios S 
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( S.IdEmpresa = I.IdEmpresa and S.IdEstado = I.IdEstado and S.IdFarmacia = I.IdFarmacia and S.FolioVale = I.FolioVenta ) 
	Where OrigenDeServicio = 2 	


	Update S Set -- NombreMedico = (M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre), NumCedula = M.NumCedula 
		NombreMedico = M.Nombre, ApPaternoMedico = M.ApPaterno, ApMaternoMedico = M.ApMaterno  
	From #tmp_Servicios S 
	Inner Join CatMedicos M (NoLock) 
		On ( S.IdEstado = M.IdEstado and S.IdFarmacia = M.IdFarmacia and S.IdMedico = M.IdMedico ) 
			
	Update S Set 
		ReferenciaAfiliacion = B.FolioReferencia, 
		--ReferenciaAfiliacion = D.IdBeneficiario, 		
		Nombre = B.Nombre, ApPaterno = B.ApPaterno, ApMaterno = B.ApMaterno,  
		-- FechaNacimiento = convert(varchar(10), B.FechaNacimiento, 120), 
		FechaNacimiento = '1900-01-01', 	
		Sexo = B.Sexo, 
		CodigoPostal = D.CodigoPostal, Calle = D.Direccion, 
		NumeroExterior = '.', NumeroInterior = '.', EntreCalles = '.', 	
		Telefono= D.Telefonos, TelefonoMovil = D.Telefonos, TelefonoCasa = D.Telefonos, 
		Colonia = G.Colonia, Referencia = D.Referencia, Estado = G.Estado, DelegacionMunicipio = G.Municipio  
	From #tmp_Servicios S 
	Inner Join CatBeneficiarios B (NoLock) 
		On ( S.IdEstado = B.IdEstado and S.IdFarmacia = B.IdFarmacia 
			and S.IdCliente = B.IdCliente and S.IdSubCliente = B.IdSubCliente and S.IdBeneficiario = B.IdBeneficiario ) 
	Inner Join CatBeneficiarios_Domicilios D (NoLock) 
		On ( S.IdEstado = D.IdEstado and S.IdFarmacia = D.IdFarmacia 
			and S.IdCliente = D.IdCliente and S.IdSubCliente = D.IdSubCliente and S.IdBeneficiario = D.IdBeneficiario ) 
	Inner Join vw_Geograficos G (NoLock) 
		On ( D.IdEstado_D = G.IdEstado and D.IdMunicipio_D = G.IdMunicipio and D.IdColonia_D = G.IdColonia ) 


	Update D Set -- IdProducto = S.ClaveSSA, SKU = S.ClaveSSA, 
		ClaveSSA = S.ClaveSSA, 
		DescripcionClaveSSA = S.Descripcion 
	From #tmp_Detalles D 
	Inner Join CatClavesSSA_Sales S On ( D.IdClaveSSA_Sal = S.IdClaveSSA_Sal )  
---------------------------------------------- Completar la informacion del Pedido 




------------------------------------------------- Salida final 
	Select 
		convert(varchar(10), FechaRegistro, 120) FechaRegistro, 
		(case when OrigenDeServicio = 1 then 'Vale' else 'Dispensación' end) as OrigenDeServicio, 
		FolioServicioDomicilio, 
		(ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as NombreBeneficiario, 
		(Calle + ' ' + NumeroExterior + ' ' + NumeroInterior + ', Col. ' + Colonia + ', C.P ' + CodigoPostal + ', ' + 
			DelegacionMunicipio + ' ' + Estado) as Domicilio,  
		0 as Enviar 
	From #tmp_Servicios 
	Order by OrigenDeServicio, FolioServicioDomicilio 
	
	
	Select *, (Calle + ' ' + NumeroExterior + ' ' + NumeroInterior + ', Col. ' + Colonia + ', C.P ' + CodigoPostal + ', ' + 
			DelegacionMunicipio + ' ' + Estado) as Domicilio  
	From #tmp_Servicios 

	select * 
	From #tmp_Detalles


	------Select top 1 * 
	------into #tmp_Vales 
	------From vw_Vales_Emision_InformacionAdicional  
	
	
	------Select * 
	------From #tmp_Vales 
	

End  
Go--#SQL 

