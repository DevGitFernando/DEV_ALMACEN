-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__ConsultarFolioDeReceta' and xType = 'P' ) 
   Drop Proc spp_INT_MA__ConsultarFolioDeReceta
Go--#SQL   

Create Proc spp_INT_MA__ConsultarFolioDeReceta 
( 
	@Elegibilidad varchar(50) = 'E006493943', 
	@Folio_MA varchar(30) = '21', @Consecutivo Varchar(2) = '01'  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sElegibilidad varchar(50), 
	@iFolio_MA varchar(50),
	@bEsRecetaManual bit,
	@iNumDeRecetas Int, 
	@iSurtido int, 
	@CIE_10__Principal varchar(20), 
	@FechaEmisionReceta varchar(20)
	
	Set @sElegibilidad = ''
	Set @iFolio_MA = '0'
	Set @CIE_10__Principal = ''  
	Set @iSurtido = 0 
	Set @FechaEmisionReceta = '' 
	Set @bEsRecetaManual = 0
	Set @iNumDeRecetas = 0
	
	If @Consecutivo = '' 
	   Set @Consecutivo = '01' 
	
	Select @sElegibilidad = Elegibilidad, @iFolio_MA = Folio_MA, @iSurtido = Surtido    
	From INT_MA__RecetasElectronicas_001_Encabezado (NoLock) 
	Where Elegibilidad = @Elegibilidad -- and Folio_MA = @Folio_MA

	
	Select @iNumDeRecetas = COUNT(*)
	From INT_MA__RecetasElectronicas_001_Encabezado (NoLock) 
	Where Elegibilidad = @Elegibilidad -- and Folio_MA = @Folio_MA 

	Select 
		@iFolio_MA = Folio_MA, 
		@bEsRecetaManual = EsRecetaManual, 
		@FechaEmisionReceta = FechaEmision, 
		@CIE_10__Principal = CIE_01, 
		@iSurtido = Surtido    
	From INT_MA__RecetasElectronicas_001_Encabezado (NoLock) 
	Where Elegibilidad = @Elegibilidad and Folio_MA = @Folio_MA And Consecutivo = @Consecutivo  
	

	If IsNull(@sElegibilidad, '0') = '0' or IsNull(@sElegibilidad, '0') = '' 
	   Set @sElegibilidad = @Elegibilidad 

	If IsNull(@iFolio_MA, '0') = '0' or IsNull(@iFolio_MA, '0') = '' 
	   Set @iFolio_MA = @Folio_MA 

	Set @sElegibilidad = IsNull(@sElegibilidad, '0') 
	Set @iFolio_MA = IsNull(@iFolio_MA, '0') 
	Set @iSurtido = IsNull(@iSurtido, 2) 



--		spp_INT_MA__ConsultarFolioDeReceta 
	
	--------------------------------------------------- 
	Select 
		@sElegibilidad as Elegibilidad, 
		cast(@iFolio_MA as varchar)as FolioReceta, 
		@bEsRecetaManual as EsRecetaManual, 
		@FechaEmisionReceta as FechaEmisionReceta, 
		@CIE_10__Principal as CIE_10_Principal, 
		@iSurtido as StatusSurtido, 
		P.ClaveSSA, sum(R.CantidadSolicitada) as CantidadSolicitada, sum(R.CantidadSurtida) as CantidadSurtida,
		@iNumDeRecetas As NumDeRecetas
	From INT_MA__RecetasElectronicas_002_Productos R (NoLock) 
	Inner Join vw_Productos_CodigoEAN P On ( R.CodigoEAN = P.IdProducto )
	Where Folio_MA = @Folio_MA And Consecutivo = @Consecutivo
	Group by P.ClaveSSA 
	
	
End 
Go--#SQL 

