----------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_RPT_RegistrosSanitarios_Pantalla' and xType = 'P' ) 
   Drop Proc spp_RPT_RegistrosSanitarios_Pantalla 
Go--#SQL 

Create Proc spp_RPT_RegistrosSanitarios_Pantalla
(
	@Tipo Varchar(30) = '', @ClaveSSA varchar(30) = '', @Laboratorio varchar(50) = '', @Registro varchar(30) = '200'
)
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On
	Declare @sSql Varchar(1000),
			@sWhere Varchar(400)
	
	
	Set @sWhere = '1 = 1'
	
	If (@Tipo <> '')
	Set @sWhere = ' TipoDeClaveDescripcion like ' + Char(39) + '%' + @Tipo + '%' + Char(39)
	
	If (@ClaveSSA <> '')
	Set @sWhere = @sWhere + ' And ClaveSSA like ' + Char(39) + '%' + @ClaveSSA + '%' + Char(39)
	
	Set @sSql = 'Select *
	Into #vw_ClavesSSA_Sales
	From vw_ClavesSSA_Sales
	Where ' + @sWhere
	
	
	Set @sWhere = '1 = 1'
	
	If (@Laboratorio <> '')
	Set @sWhere = ' Descripcion like ' + Char(39) + '%' + @Laboratorio + '%' + Char(39)
	
	
	Set @sSql = @sSql + 'Select *
	Into #CatLaboratorios
	From CatLaboratorios
	Where ' + @sWhere
	
	
	Set @sWhere = '1 = 1'

	If (@Registro <> '')
	Set @sWhere = ' FolioRegistroSanitario like ' + Char(39) + '%' + @Registro + '%' + Char(39)



	Set @sSql = @sSql +
	'Select 
		distinct C.TipoDeClaveDescripcion, C.ClaveSSA, L.IdLaboratorio, Laboratorio, R.Descripcion, FolioRegistroSanitario,
		MD5, NombreDocto, Convert(Varchar(10), FechaVigencia, 120) As FechaVigencia, EsVigente
	From vw_RegistrosSanitarios R (NoLock)
	Inner Join #vw_ClavesSSA_Sales C (NoLock) On (R.IdClaveSSA_Sal = C.IdClaveSSA_Sal)
	Inner Join #CatLaboratorios L (NoLock) ON (R.IdLaboratorio = L.IdLaboratorio) 
	Where ' + @sWhere
	
	Print(@sSql)
	Exec(@sSql)

End 
Go--#SQL  



