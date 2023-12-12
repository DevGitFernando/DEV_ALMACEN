---	Set @sComplemento = '' -- '-' + right('0000000' + cast(@Iteracion as varchar(10)), 2) -----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Ticket_Vale_Custom' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Ticket_Vale_Custom 
Go--#SQL  
			 
Create Proc	 spp_Rpt_Impresion_Ticket_Vale_Custom
(
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '3132', @Folio varchar(8) = '21445', 
	@Iteracion int = 0, @ClaveSSA varchar(50) = '', @Cantidad int = 0 
)    
With Encryption 
As 
Begin 
-- Set NoCount On  
Set DateFormat YMD 
Declare 
	@sSql varchar(max), 
	@sComplemento varchar(20), 
	@sFiltro_ClaveSSA varchar(max) 
	
	  

	Set @sSql = '' 
	Set @sFiltro_ClaveSSA = '' 
	Set @sComplemento = '' -- '-' + right('0000000' + cast(@Iteracion as varchar(10)), 2) 


	If @Iteracion > 0 
	Begin 
		Set @sComplemento = '-' + right('0000000' + cast(@Iteracion as varchar(10)), 2) 
	End 


	Set @IdEmpresa = right('0000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000000000' + @IdFarmacia, 4) 
	Set @Folio = right('0000000000' + @Folio, 8) 


	if @ClaveSSA <>  '' 
	Begin 
		Set @sFiltro_ClaveSSA = ' and ClaveSSA = ' + char(39) + @ClaveSSA + char(39)  
	End 


	Select 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, CLUES, NombrePropio_UMedica, 
		Folio, 
		Folio as FolioIteracion, FolioVenta, EsSegundoVale, FolioValeReembolso, IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, 
		FechaSistema, FechaRegistro, FechaCanje, IdPersonal, NombrePersonal, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, Status, 
		IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, DescripcionSal, DescripcionCortaClave, IdPresentacion, Presentacion, Cantidad, CantidadSegundoVale, 
		IdMunicipio, Municipio, IdColonia, Colonia, Domicilio,  
		getdate() as FechaImpresion  
	Into #tmpDetalleFolio_Vale 
	From vw_Impresion_Vales (Nolock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio  
		and 1 = 0 


	Set @sSql = 
	'Insert Into #tmpDetalleFolio_Vale ' + char(13) + 
	'Select ' + char(13) + 
	'	IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, CLUES, NombrePropio_UMedica, ' + char(13) + 
	'	Folio, (Folio + ' + char(39) + @sComplemento + char(39) + ') as FolioIteracion , FolioVenta, EsSegundoVale, FolioValeReembolso, IdBeneficiario, Beneficiario, FolioReferencia, NumReceta, FechaReceta, ' + char(13) + 
	'	FechaSistema, FechaRegistro, FechaCanje, IdPersonal, NombrePersonal, ' + char(13) + 
	'	IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, Status, ' + char(13) + 
	'	IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, DescripcionSal, DescripcionCortaClave, IdPresentacion, Presentacion, Cantidad, CantidadSegundoVale, ' + char(13) + 
	'	IdMunicipio, Municipio, IdColonia, Colonia, Domicilio,  ' + char(13) + 
	'	getdate() as FechaImpresion ' + char(13) + 
	'From vw_Impresion_Vales (Nolock) ' + char(13) + 
	'Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) + ' and IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + ' and Folio = ' + char(39) + @Folio  + char(39) + ' ' + char(13) + 
		@sFiltro_ClaveSSA 
	Exec( @sSql  ) 
	Print @sSql 


	------ Solo aplica Clave - Piezas 
	Update V Set Cantidad = @Cantidad 
	From #tmpDetalleFolio_Vale V 
	Where @Cantidad = 1 


	-------------------------------- SALIDA FINAL 
	Select * 
	From #tmpDetalleFolio_Vale (Nolock) 
	Where Cantidad > 0 
	
 
End 
Go--#SQL  

