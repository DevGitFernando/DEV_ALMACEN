-------------------------------------------------------------------------------------------------------------------------------------------------- 
--Set DateTime YMD

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_IME__Vales_001_Encabezado' and xType = 'P' ) 
   Drop Proc spp_INT_IME__Vales_001_Encabezado
Go--#SQL 

Create Proc spp_INT_IME__Vales_001_Encabezado 
( 
	@IdSocioComercial varchar(8) = '' output, 
	@IdSucursal varchar(8) = '' output, 		
	@Folio_Vale varchar(30) = '',  
	@FechaEmision_Vale varchar(10) = '', 	
	@EsValeManual bit = 0, 
	
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '',
	@IdPersonal varchar(4) = '',
	@Beneficiario_Nombre varchar(150) = '',
	@Beneficiario_ApPaterno varchar(150) = '',
	@Beneficiario_ApMaterno varchar(150) = '',
	@Beneficiario_Sexo varchar(1) = '',
	@Beneficiario_FechaNacimiento Varchar(10) = '2012-01-01',
	@Beneficiario_FolioReferencia varchar(20) = '',
	@Beneficiario_FechaFinVigencia Varchar(10) = '2012-01-01',
	@IdTipoDeDispensacion varchar(2) = '',
	@NumReceta varchar(30) = '',
	@FechaReceta Varchar(10) = '2012-01-01',
	@Medico_Nombre varchar(150) = '',
	@Medico_ApPaterno varchar(150) = '',
	@Medico_ApMaterno varchar(150) = '',
	@Medico_NumCedula varchar(30) = '',
	@IdBeneficio varchar(4) = '',
	@IdDiagnostico varchar(6) = '',
	@RefObservaciones varchar(100) = '',
	@IdEmpresa_IME varchar(3) = '', 
	@IdEstado_IME varchar(2) = '', 
	@IdFarmacia_IME varchar(4) = ''
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200), 
	@IdEstadoRegistra Varchar(2), 
	@IdFarmaciaRegistra Varchar(4)  
	

	Select @sFolio = cast(max(cast(Folio as int)) + 1 as varchar) From INT_IME__RegistroDeVales_001_Encabezado (NoLock) 
	Set @sFolio = IsNull(@sFolio, '1') 
	Set @sFolio = right('000000000000000' + @sFolio, 12) 
	Set @sMensaje = 'Información guardada satisfactoriamente'
	
	If(@IdSocioComercial = '')
	Begin
		Select 	@IdSocioComercial = IdSocioComercial, @IdSucursal = IdSucursal
		From CatSociosComerciales_Sucursales (NoLock)
		Where IdEmpresa_IME = @IdEmpresa_IME And IdEstado_IME = @IdEstado_IME And IdFarmacia_IME = @IdFarmacia_IME 
	End 

	
	If Not Exists 
	( 
		Select * From INT_IME__RegistroDeVales_001_Encabezado (NoLock) 
		Where IdSocioComercial = @IdSocioComercial and IdSucursal = @IdSucursal and Folio_Vale = @Folio_Vale 
	) 
	Begin 
		Insert Into INT_IME__RegistroDeVales_001_Encabezado 
		( 
			Folio, IdSocioComercial, IdSucursal, Folio_Vale, FechaEmision_Vale, EsValeManual, 
			IdEmpresa, IdEstado, IdFarmacia, IdPersonal, Beneficiario_Nombre, Beneficiario_ApPaterno,
			Beneficiario_ApMaterno, Beneficiario_Sexo, Beneficiario_FechaNacimiento,
			Beneficiario_FolioReferencia, Beneficiario_FechaFinVigencia, IdTipoDeDispensacion,
			NumReceta, FechaReceta, Medico_Nombre, Medico_ApPaterno, Medico_ApMaterno,
			Medico_NumCedula, IdBeneficio, IdDiagnostico, RefObservaciones
		) 
		Select 
			@sFolio, @IdSocioComercial, @IdSucursal, @Folio_Vale, @FechaEmision_Vale, @EsValeManual, 
			@IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @Beneficiario_Nombre, @Beneficiario_ApPaterno,
			@Beneficiario_ApMaterno, @Beneficiario_Sexo, @Beneficiario_FechaNacimiento,
			@Beneficiario_FolioReferencia, @Beneficiario_FechaFinVigencia, @IdTipoDeDispensacion,
			@NumReceta, @FechaReceta, @Medico_Nombre, @Medico_ApPaterno, @Medico_ApMaterno,
			@Medico_NumCedula, @IdBeneficio, @IdDiagnostico, @RefObservaciones
	End 
	

------------------------- SALIDA 
	Select @sFolio as Folio, @sMensaje as Mensaje, @IdSocioComercial As IdSocioComercial, @IdSucursal As IdSucursal


End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_IME__Vales__002_ClavesSSA' and xType = 'P' ) 
   Drop Proc spp_INT_IME__Vales__002_ClavesSSA
Go--#SQL 

Create Proc spp_INT_IME__Vales__002_ClavesSSA 
( 
	@IdSocioComercial varchar(8) = '', 
	@IdSucursal varchar(8) = '', 		
	@Folio_Vale varchar(30) = '',  
	@Partida int = 0, 
	@ClaveSSA varchar(30) = '',  
	@CantidadSolicitada int = 0, 
	@CantidadSurtida int = 0 
	
--	@Folio_MA varchar(30) = '1', @Partida int = 0, @CodigoEAN varchar(30) = '', @CantidadSolicitada int = 0  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(10), 
	@sMensaje varchar(200),   
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4) 

/* 

	IdSocioComercial varchar(8) Not Null, 
	IdSucursal varchar(8) Not Null, 		
	Folio_Vale varchar(30) Not Null Default '',  
	Partida int Not Null Default 0, 
	ClaveSSA varchar(30) Not Null Default '',  
	CantidadSolicitada int Not Null Default 0, 
	CantidadSurtida int Not Null Default 0 

*/ 

	If Not Exists 
	( 
		Select * From INT_IME__RegistroDeVales_002_Claves (NoLock) 
		Where IdSocioComercial = @IdSocioComercial and IdSucursal = @IdSucursal 
			  and Folio_Vale = @Folio_Vale and Partida = @Partida and ClaveSSA = @ClaveSSA 
	) 
	Begin 
		Insert Into INT_IME__RegistroDeVales_002_Claves 
		( 
			   IdSocioComercial, IdSucursal, Folio_Vale, Partida, ClaveSSA, CantidadSolicitada, CantidadSurtida 
	    ) 
		Select @IdSocioComercial, @IdSucursal, @Folio_Vale, @Partida, @ClaveSSA, @CantidadSolicitada, 0 as CantidadSurtida 
	End 
	


End 
Go--#SQL 
	
	