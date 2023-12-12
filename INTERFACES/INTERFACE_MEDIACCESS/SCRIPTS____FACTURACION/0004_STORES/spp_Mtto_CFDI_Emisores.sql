If Exists ( Select Name From sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_Emisores' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFDI_Emisores 
Go--#SQL 

Create Proc spp_Mtto_CFDI_Emisores 
( 
	@IdEmpresa varchar(3) = '002', 
	-- @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0001', 
	@NombreFiscal varchar(100) = '', @NombreComercial varchar(100) = '', 
	@RFC varchar(15) = '', @Telefonos varchar(100) = '', @Fax varchar(100) = '', @Email varchar(100) = '', @DomExpedicion_DomFiscal bit = 0, 
	@Pais varchar(100) = '', @Estado varchar(100) = '', @Municipio varchar(100) = '', @Colonia varchar(100) = '', 
	@Calle varchar(100) = '', @NoExterior varchar(8) = '', @NoInterior varchar(8) = '', @CodigoPostal varchar(100) = '', @Referencia varchar(100) = '', 
	
	@EPais varchar(100) = '', @EEstado varchar(100) = '', @EMunicipio varchar(100) = '', @EColonia varchar(100) = '', 
	@ECalle varchar(100) = '', @ENoExterior varchar(8) = '', @ENoInterior varchar(8) = '', @ECodigoPostal varchar(100) = '', @EReferencia varchar(100) = '', 	
	
	@Status varchar(1) = 'A', 
	@EsPersonaFisica int = 0, @PublicoGeneral_AplicaIva int = 0 
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@iTipo int,
	@Mensaje varchar(200) 
	
	Set @iTipo = 0 
	Set @Mensaje = '' 
	
	If @IdEmpresa = '*' 
	Begin 
		If Not Exists ( Select * From CFDI_Emisores (NoLock) Where RFC = @RFC )
			Begin
				Select @IdEmpresa = cast(max(cast(IdEmpresa as int) + 1) as varchar) From CFDI_Emisores (NoLock) 
			End 
		Else 
			Begin 
				Set @iTipo = 1 
				Set @Mensaje = 'El RFC [   ' +  @RFC + '   ] ya se encuentra registrado, verifique.'
			End 
	End 

	Set @IdEmpresa = IsNull(@IdEmpresa, 1) 
	Set @IdEmpresa = right(replicate('0', 3) + @IdEmpresa, 3) 

	If @iTipo <> 0 
	Begin 
		Select 	@iTipo as Codigo, @IdEmpresa as Folio, @Mensaje as Mensaje  	
		Return 
	End 	
	

	If @Status = 'A' 
	Begin 
		If Not Exists ( Select * From CFDI_Emisores (NoLock) Where IdEmpresa = @IdEmpresa ) 
			Begin 
				Insert Into CFDI_Emisores 
				( 
					IdEmpresa, NombreFiscal, NombreComercial, RFC, EsPersonaFisica, PublicoGeneral_AplicaIva, 
					Telefonos, Fax, Email, DomExpedicion_DomFiscal, 
					Pais, Estado, Municipio, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, 
					EPais, EEstado, EMunicipio, EColonia, ECalle, ENoExterior, ENoInterior, ECodigoPostal, EReferencia, 					
					Status 
				) 
				Select 
					@IdEmpresa, @NombreFiscal, @NombreComercial, @RFC, @EsPersonaFisica, @PublicoGeneral_AplicaIva, 
					@Telefonos, @Fax, @Email, @DomExpedicion_DomFiscal, 
					@Pais, @Estado, @Municipio, @Colonia, @Calle, @NoExterior, @NoInterior, @CodigoPostal, @Referencia, 
					@EPais, @EEstado, @EMunicipio, @EColonia, @ECalle, @ENoExterior, @ENoInterior, @ECodigoPostal, @EReferencia, 					
					@Status 
					
				Set @Mensaje = 'Se ha registrado satisfactoriamente el RFC [  ' +  @RFC + '  ] con la Clave [' +  @IdEmpresa + '] '  	
			End 
		Else 
			Begin 
				Update C Set 
					NombreFiscal = @NombreFiscal, NombreComercial = @NombreComercial, RFC = @RFC, 
					EsPersonaFisica = @EsPersonaFisica, PublicoGeneral_AplicaIva = @PublicoGeneral_AplicaIva, 
					Telefonos = @Telefonos, Fax = @Fax, EMail = @Email, DomExpedicion_DomFiscal = @DomExpedicion_DomFiscal,   
					Pais = @Pais, Estado = @Estado, Municipio = @Municipio, Colonia = @Colonia, Calle = @Calle, 
					NoExterior = @NoExterior, NoInterior = @NoInterior, CodigoPostal = @CodigoPostal, Referencia = @Referencia, 
					EPais = @EPais, EEstado = @EEstado, EMunicipio = @EMunicipio, EColonia = @EColonia, ECalle = @ECalle, 
					ENoExterior = @ENoExterior, ENoInterior = @ENoInterior, ECodigoPostal = @ECodigoPostal, EReferencia = @EReferencia, 					
					Status = @Status
				From CFDI_Emisores C (NoLock) 
				Where IdEmpresa = @IdEmpresa 
				
				Set @Mensaje = 'Se actualizo satisfactoriamente el RFC [  ' +  @RFC + '  ] con la Clave [' +  @IdEmpresa + '] ' 
			End 
			
		----Delete From CFDI_Emisores_DomicilioFiscal Where IdEmpresa = @IdEmpresa  
		----Delete From CFDI_Emisores_ExpedidoEn Where IdEmpresa = @IdEmpresa  	
		
		----Insert Into CFDI_Emisores_DomicilioFiscal ( IdEmpresa, Pais, Estado, Municipio, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia )  		
		----select @IdEmpresa, @Pais, @Estado, @Municipio, @Colonia, @Calle, @NoExterior, @NoInterior, @CodigoPostal, @Referencia 	
			
		----Insert Into CFDI_Emisores_ExpedidoEn ( IdEmpresa, EPais, EEstado, EMunicipio, EColonia, ECalle, ENoExterior, ENoInterior, ECodigoPostal, EReferencia )  		
		----select @IdEmpresa, @EPais, @EEstado, @EMunicipio, @EColonia, @ECalle, @ENoExterior, @ENoInterior, @ECodigoPostal, @EReferencia  	
					
	End 

	If @Status = 'C' 
	Begin 
		Update C Set Status = 'C' 
		From CFDI_Emisores C (NoLock)  
		Where IdEmpresa = @IdEmpresa 	
		
		Set @Mensaje = 'Se cancelo satisfactoriamente el RFC  [  ' +  @RFC + '  ] con la Clave [' +  @IdEmpresa + '] ' 	
	End 
	
--- Salida final 	
	Select 	@iTipo as Codigo, @IdEmpresa as Folio, @Mensaje as Mensaje  
		
End 
Go--#SQL 
   
--		sp_listacolumnas CFDI_Emisores 

--		sp_listacolumnas__Stores spp_Mtto_CFDI_Emisores , 1 
 
	