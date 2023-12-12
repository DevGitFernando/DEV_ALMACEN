----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Empresas' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_Empresas
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_Empresas 
(
	@IdEmpresa varchar(3) = '002', @Nombre varchar(200) = '', 
	@RFC varchar(15) = '', @KeyLicencia varchar(200) = '', 
	@NombreProveedor varchar(50) = '', @DireccionUrl varchar(200) = '', 
	@Telefonos varchar(100) = '', @Status varchar(1) = 'A' 
) 
As 
Begin 
Set NoCount On 

	If @Status = 'A' 
		Begin 
		If Not Exists ( Select * From FACT_CFD_Empresas (NoLock) Where IdEmpresa = @IdEmpresa ) 
			Begin 
				Select @IdEmpresa, @Nombre, @RFC, @KeyLicencia, @NombreProveedor, @DireccionUrl, @Telefonos, @Status, 0 as Actualizado 	
				Insert Into FACT_CFD_Empresas ( IdEmpresa, Nombre, RFC, KeyLicencia, NombreProveedor, DireccionUrl, Telefonos, Status, Actualizado ) 
				Select @IdEmpresa, @Nombre, @RFC, @KeyLicencia, @NombreProveedor, @DireccionUrl, @Telefonos, @Status, 0 as Actualizado 
			End 
		Else 
			Begin 		
				Update E Set Nombre = @Nombre, RFC = @RFC, KeyLicencia = @KeyLicencia, 
					NombreProveedor = @NombreProveedor, DireccionUrl = @DireccionUrl, Telefonos = @Telefonos, 
					Status = @Status 
				From FACT_CFD_Empresas E (NoLock) Where IdEmpresa = @IdEmpresa  			
			End 
		End 
	Else 	
		Begin 
			Update E Set Status = @Status 
			From FACT_CFD_Empresas (NoLock) Where IdEmpresa = @IdEmpresa  
		End 

    	
End 
Go--#SQL 

----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Sucursales' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_Sucursales 
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_Sucursales 
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001',  
	@Nombre varchar(200) = '', @RFC varchar(15) = '', @Status varchar(1) = 'A' 
) 
As 
Begin 
Set NoCount On 

	If @Status = 'A' 
		Begin 
		If Not Exists ( Select * From FACT_CFD_Sucursales (NoLock) 
			Where IdEmpresa = @IdEmpresa  and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  ) 
			Begin 
				Insert Into FACT_CFD_Sucursales ( IdEmpresa, IdEstado, Nombre, IdFarmacia, RFC, Status, Actualizado ) 
				Select @IdEmpresa, @IdEstado, @Nombre, @IdFarmacia, @RFC, @Status, 0 as Actualizado 
			End 
		Else 
			Begin 		
				Update E Set Nombre = @Nombre, RFC = @RFC, Status = @Status, Actualizado = 0  
				From FACT_CFD_Sucursales E (NoLock) 
				Where IdEmpresa = @IdEmpresa  and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia   			
			End 
		End 
	Else 	
		Begin 
			Update E Set Status = @Status 
			From FACT_CFD_Sucursales (NoLock) 
			Where IdEmpresa = @IdEmpresa  and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  		
		End 

    	
End 
Go--#SQL 

----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Domicilios' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_Domicilios 
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_Domicilios 
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0001', 
	@Pais varchar(100) = '', @Estado varchar(100) = '', @Municipio varchar(100) = '', @Localidad varchar(100) = '', 
	@Colonia varchar(100) = '', @Calle varchar(100) = '', @NoExterior varchar(8) = '', @NoInterior varchar(8) = '', 
	@CodigoPostal varchar(100) = '', @Referencia varchar(100) = '', @Opcion int = 1 
) 
As 
Begin 
Set NoCount On 

	If @Opcion =  1 
	Begin 
		Delete From FACT_CFD_Empresas_DomicilioFiscal Where IdEmpresa = @IdEmpresa 
		
		Insert Into FACT_CFD_Empresas_DomicilioFiscal ( IdEmpresa, Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia )  
		Select @IdEmpresa, @Pais, @Estado, @Municipio, @Localidad, @Colonia, @Calle, @NoExterior, @NoInterior, @CodigoPostal, @Referencia 
	End 
	
	If @Opcion =  2 
	Begin 
		Delete From FACT_CFD_Sucursales_Domicilio 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		
		Insert Into FACT_CFD_Sucursales_Domicilio ( IdEmpresa, IdEstado, IdFarmacia, Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia )  
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @Pais, @Estado, @Municipio, @Localidad, @Colonia, @Calle, @NoExterior, @NoInterior, @CodigoPostal, @Referencia 
	End 	

End 
Go--#SQL 

----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Certificados' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_Certificados 
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_Certificados 
(
	@IdEmpresa varchar(3) = '002', 
	@NumeroDeCertificado varchar (100) = '', 
	@NombreCertificado varchar(100) = '', @Certificado varchar(max) = '', 
	@NombreLlavePrivada varchar(100) = '', @LlavePrivada varchar(max) = '', @PasswordPublico varchar(100) = '', 
	@NombreCertificadoPfx varchar(100) = '', @CertificadoPfx varchar(max) = '', 		
	@Status varchar(1) = 'A' 
)
As 
Begin 
Set NoCount On 

Declare 
	@iTipo int,
	@Mensaje varchar(200) 
	
	Delete From FACT_CFD_Certificados Where IdEmpresa = @IdEmpresa 

	Insert Into FACT_CFD_Certificados ( IdEmpresa, NumeroDeCertificado, NombreCertificado, Certificado, NombreLlavePrivada, LlavePrivada, PasswordPublico, NombreCertificadoPfx, CertificadoPfx, Status ) 
	Select @IdEmpresa, @NumeroDeCertificado, @NombreCertificado, @Certificado, @NombreLlavePrivada, @LlavePrivada, @PasswordPublico, @NombreCertificadoPfx, @CertificadoPfx, @Status 	

End 
Go--#SQL 

		

----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_SeriesFolios' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_SeriesFolios 
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_SeriesFolios 
(
	@IdEmpresa varchar(3) = '002', 
	@AñoAprobacion int = 2012, @NumAprobacion int = 1, 
	@Serie varchar(20) = '', @IdTipoDocumento varchar(3) = '', @NombreDocumento varchar(50) = '', 
	@FolioInicial int = 1, @FolioFinal int = 100, @FolioUtilizado int = 0, @Status varchar(1) = 'A'
) 
As 
Begin 
Set NoCount On 

	If @Status = 'A' 
		Begin 
		If Not Exists ( Select * From FACT_CFD_SeriesFolios (NoLock) Where IdEmpresa = @IdEmpresa and Serie = @Serie ) 
			Begin 
				Select @FolioUtilizado = 0
				Insert Into FACT_CFD_SeriesFolios ( IdEmpresa, AñoAprobacion, NumAprobacion, Serie, IdTipoDocumento, NombreDocumento, FolioInicial, FolioFinal, FolioUtilizado, Status, Actualizado ) 
				Select @IdEmpresa, @AñoAprobacion, @NumAprobacion, @Serie, @IdTipoDocumento, @NombreDocumento, @FolioInicial, @FolioFinal, @FolioUtilizado, @Status, 0 as Actualizado
			End 
		Else 
			Begin 
				Update S Set AñoAprobacion = @AñoAprobacion, NumAprobacion = @NumAprobacion, 
					Serie = @Serie, IdTipoDocumento = @IdTipoDocumento, NombreDocumento = @NombreDocumento, -- FolioInicial = @FolioInicial, FolioFinal = @FolioFinal, FolioUtilizado = @FolioUtilizado, 
					Status = @Status 
				From FACT_CFD_SeriesFolios S (NoLock) 
				Where IdEmpresa = @IdEmpresa and Serie = @Serie 
			End 
		End 
	Else
		Begin 
			Update S Set Status = @Status 
			From FACT_CFD_SeriesFolios S (NoLock) 
			Where IdEmpresa = @IdEmpresa and Serie = @Serie 
		End 	
		
End 
Go--#SQL 


----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Sucursales_Series' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_Sucursales_Series 
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_Sucursales_Series 
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001',  
	@IdentificadorSerie varchar(20) = '1', @Status varchar(1) = 'A'
) 
As 
Begin 
Set NoCount On 

	If @Status = 'A' 
		Begin 
			If Not Exists ( Select * From FACT_CFD_Sucursales_Series 
							Where IdEmpresa = @IdEmpresa  and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
								  and IdentificadorSerie = @IdentificadorSerie  ) 
				Begin 
					Insert Into FACT_CFD_Sucursales_Series ( IdEmpresa, IdEstado, IdFarmacia, IdentificadorSerie, Status  ) 
					Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdentificadorSerie, 'A' as Status 
				End 
			Else 
				Begin 
					Update S Set Status = 'A' 
					From FACT_CFD_Sucursales_Series S (NoLock) 
					Where IdEmpresa = @IdEmpresa  and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
						  and IdentificadorSerie = @IdentificadorSerie 
				End 
		End 
	Else 
		Begin 
			Update S Set Status = 'C' 
			From FACT_CFD_Sucursales_Series S (NoLock) 
			Where IdEmpresa = @IdEmpresa  and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				  and IdentificadorSerie = @IdentificadorSerie 
		End 

End 
Go--#SQL 


----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFD_Clientes' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFD_Clientes 
Go--#SQL    

Create Proc spp_Mtto_FACT_CFD_Clientes 
(
	@IdCliente varchar(6) = '002', @Nombre varchar(100) = '', @RFC varchar(15) = 'DIMJ19830516AIJ', 
	@Pais varchar(100) = '', @Estado varchar(100) = '', @Municipio varchar(100) = '', @Localidad varchar(100) = '', 
	@Colonia varchar(100) = '', @Calle varchar(100) = '', @NoExterior varchar(8) = '', @NoInterior varchar(8) = '', 
	@CodigoPostal varchar(100) = '', @Referencia varchar(100) = '', @Opcion int = 1, @IdRegimen varchar(4) = '' 
	
) 
As 
Begin 
Set NoCount On 
Declare 
	@iTipo int,
	@Mensaje varchar(200) 
	
	Set @iTipo = 0 
	Set @Mensaje = '' 
	
	If @IdCliente = '*' 
	Begin 
		If Not Exists ( Select * From FACT_CFD_Clientes (NoLock) Where RFC = @RFC )
			Begin
				Select @IdCliente = cast(max(cast(IdCliente as int) + 1) as varchar) From FACT_CFD_Clientes (NoLock) 
			End 
		Else 
			Begin 
				Set @iTipo = 1 
				Set @Mensaje = 'El RFC [   ' +  @RFC + '   ] ya se encuentra registrado, verifique.'
			End 
	End 
	
	Set @IdCliente = IsNull(@IdCliente, '1') 
	Select @IdCliente = dbo.fg_FormatearCadena(@IdCliente, '0', 6)
	
	If @iTipo <> 0 
	Begin 
		Select 	@iTipo as Codigo, @IdCliente as Folio, @Mensaje as Mensaje  	
		Return 
	End 	
	
	
	If @Opcion =  1 
		Begin 
			If Not Exists ( Select * From FACT_CFD_Clientes (NoLock)  Where IdCliente = @IdCliente ) 
				Begin 		
					Insert Into FACT_CFD_Clientes ( IdCliente, Nombre, RFC, Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, IdRegimen )  
					Select @IdCliente, @Nombre, @RFC, @Pais, @Estado, @Municipio, @Localidad, @Colonia, @Calle, @NoExterior, @NoInterior, @CodigoPostal, @Referencia, @IdRegimen 
					
					Set @Mensaje = 'Se ha registrado satisfactoriamente el RFC [  ' +  @RFC + '  ] con la Clave [' +  @IdCliente + '] ' 
				End 
			Else 
				Begin 
					Update C Set Nombre = @Nombre, RFC = @RFC, Pais = @Pais, 
						Estado = @Estado, Municipio = @Municipio, Localidad = @Localidad, 
						Colonia = @Colonia, Calle = @Calle, NoExterior = @NoExterior, NoInterior = @NoInterior, 
						CodigoPostal = @CodigoPostal, Referencia = @Referencia, Status = 'A', 
						IdRegimen = @IdRegimen 
					From FACT_CFD_Clientes C (NoLock)  Where IdCliente = @IdCliente 	
					
					Set @Mensaje = 'Se actualizo satisfactoriamente el RFC [  ' +  @RFC + '  ] con la Clave [' +  @IdCliente + '] ' 
				End 	
		End 
	Else 
		Begin 
			Update C Set Status = 'C'
			From FACT_CFD_Clientes C (NoLock)  Where IdCliente = @IdCliente 
			Set @Mensaje = 'Se cancelo satisfactoriamente el RFC  [  ' +  @RFC + '  ] con la Clave [' +  @IdCliente + '] ' 			
		End 


	Select 	@iTipo as Codigo, @IdCliente as Folio, @Mensaje as Mensaje  

End 
Go--#SQL 

----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_FACT_CFD_Empresas_Configuracion' and xType = 'P' ) 
   Drop Proc spp_FACT_CFD_Empresas_Configuracion
Go--#SQL    

Create Proc spp_FACT_CFD_Empresas_Configuracion  
(
	@IdEmpresa varchar(3) = '001' 
) 
As 
Begin 
Set NoCount On 

	Select E.IdEmpresa, E.Nombre, E.RFC, 
		P.Password as KeyLicencia, 
		PC.IdPAC, 
		PC.NombrePAC as NombreProveedor, 
		P.Usuario, P.Password, P.EnProduccion, 
		(Case When P.EnProduccion = 1 Then PC.UrlProduccion Else PC.UrlPruebas End ) As DireccionUrl, 
		E.Telefonos, E.Status --, Actualizado 
	From FACT_CFD_Empresas E (NoLock) 
	Inner Join FACT_CFDI_Emisores_PAC P On ( E.IdEmpresa = P.IdEmpresa ) 
	Inner Join FACT_CFDI_PACs PC On ( PC.IdPAC = P.IdPAC ) 
	Where E.IdEmpresa = @IdEmpresa   

	----	select top 1 * from FACT_CFDI_PACs 
	----	select top 1 * from FACT_CFDI_Emisores_PAC 	

	Select IdEmpresa, Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia 
	From FACT_CFD_Empresas_DomicilioFiscal (NoLock) Where IdEmpresa = @IdEmpresa   

	Select IdEmpresa, NumeroDeCertificado, NombreCertificado, Certificado, ValidoDesde, ValidoHasta, 
		FechaInicio, FechaFinal, Serie, Serial, 
		NombreLlavePrivada, LlavePrivada, PasswordPublico, 
		NombreCertificadoPfx, CertificadoPfx, AvisoVencimiento, TiempoAviso, Status 
	From FACT_CFD_Certificados (NoLock) Where IdEmpresa = @IdEmpresa   

	Select 
		AñoAprobacion, NumAprobacion, 
		Serie, IdTipoDocumento, TipoDeDocumento, 
		NombreDocumento, FolioInicial, FolioFinal, 
		FolioUtilizado, Status 
	From vw_FACT_CFD_Series_y_Folios S (NoLock) 
	Where IdEmpresa = @IdEmpresa   


	Select * 
	From FACT_CFDI_Emisores_Logos 
	Where IdEmpresa = @IdEmpresa   	

-- IdEmpresa, AñoAprobacion, NumAprobacion, IdTipoDocumento, TipoDeDocumento, IdentificadorSerie, Serie, NombreDocumento, FolioInicial, FolioFinal, FolioUtilizado, Status 	
	
--	sp_listacolumnas vw_FACT_CFD_Series_y_Folios

End 
Go--#SQL


----------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'fg_SerieDisponible' and xType = 'FN' ) 
   Drop Function dbo.fg_SerieDisponible
Go--#SQL    

Create Function fg_SerieDisponible ( @IdentificadorSerie int = 1 ) 
Returns bit   
With Encryption 
As 
Begin 
Declare 
	@SerieDisponible bit 
	
	Set @SerieDisponible = 1  	
	Select top 1 @SerieDisponible = count(*)  
	From FACT_CFD_Sucursales_Series S (NoLock)  
	Where S.IdentificadorSerie = @IdentificadorSerie and S.Status = 'A'
	
	Set @SerieDisponible = IsNull(@SerieDisponible, 0) 
	return  @SerieDisponible  
	
End 
Go--#SQL 


----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_FACT_CFD_Sucursales_Configuracion' and xType = 'P' ) 
   Drop Proc spp_FACT_CFD_Sucursales_Configuracion
Go--#SQL    

Create Proc spp_FACT_CFD_Sucursales_Configuracion  
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001' 
) 
As 
Begin 
Set NoCount On 
Declare 
	@SerieDisponible int 
	

---------------------------- SERIES 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, Bloqueado, Asignado, AñoAprobacion, NumAprobacion, 
		Serie, TipoDeDocumento, NombreDocumento, FolioInicial, FolioFinal, FolioUtilizado, IdentificadorSerie, Status 
	Into #tmpSeries 
	From vw_FACT_CFD_Sucursales_Series (NoLock) 	
	Where IdEmpresa = @IdEmpresa  	

----	Select 
----		F.IdEmpresa, IsNull(S.IdEstado, '') as IdEstado, IsNull(S.IdFarmacia, '') as IdFarmacia,  
----		(case when dbo.fg_SerieDisponible(F.IdentificadorSerie) = 1 Then 'SI' Else 'NO' End ) as Bloqueado, 
----		(case when IsNull(S.Status, '') = '' Then 'NO' Else (case when IsNull(S.Status, '') = 'A' Then 'SI' Else 'NO' End) End) as Asignado,   
----		cast(F.AñoAprobacion as varchar) as AñoAprobacion, cast(F.NumAprobacion as varchar) as NumAprobacion, 
----		cast(F.Serie as varchar) as Serie, cast(F.FolioInicial as varchar) as FolioInicial, cast(F.FolioFinal as varchar) as FolioFinal, 
----		cast(F.FolioUtilizado as varchar) as FolioUtilizado, F.IdentificadorSerie, IsNull(S.Status, F.Status) as Status -- , Actualizado 
----	Into #tmpSeries 
----	From FACT_CFD_SeriesFolios F (NoLock) 
----	Left Join FACT_CFD_Sucursales_Series S (NoLock) 
----		On ( S.IdEmpresa = F.IdEmpresa and S.IdEstado = @IdEstado and S.IdFarmacia = @IdFarmacia and 
----			 F.IdentificadorSerie = S.IdentificadorSerie ) 
----	Where F.IdEmpresa = @IdEmpresa   
---------------------------- SERIES 


---------------------------- SALIDA  	
	Select IdEmpresa, IdEstado, IdFarmacia, Nombre, RFC, Status --, Actualizado 
	From FACT_CFD_Sucursales (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  

	Select IdEmpresa, IdEstado, IdFarmacia, Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia 
	From FACT_CFD_Sucursales_Domicilio (NoLock) Where IdEmpresa = @IdEmpresa  and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  

	Select 
		-- IdEmpresa, IdEstado, IdFarmacia, 
		Asignado, Bloqueado, 
		AñoAprobacion, NumAprobacion, Serie, TipoDeDocumento, NombreDocumento, FolioInicial, FolioFinal, FolioUtilizado, IdentificadorSerie, Status 
		  
	From #tmpSeries 
---------------------------- SALIDA  	
	

--		spp_FACT_CFD_Sucursales_Configuracion  

End 
Go--#SQL 

--	select dbo.fg_SerieDisponible('002', '20', '0001', 1 ) 

--	sp_listacolumnas FACT_CFD_Empresas 

-- sp_listacolumnas__Stores spp_FACT_CFD_Sucursales_Configuracion , 1 


