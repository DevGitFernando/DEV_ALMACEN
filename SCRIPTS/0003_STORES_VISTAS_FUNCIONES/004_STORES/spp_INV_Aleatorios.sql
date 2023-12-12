--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INV_Aleatorios' and xType = 'P' ) 
   Drop Proc spp_INV_Aleatorios 
Go--#SQL 

Create Proc spp_INV_Aleatorios 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0226', 
	@TipoInventario int = 1, @Claves int = 10, 
	@IdPersonal varchar(4) = '0012', @FechaSistema varchar(10) = '2012-05-15', @FolioCierre int = -1  
) 
With Encryption 
As 
Begin 
--Set NoCount On 
Declare 
	@sSql varchar(7500), 
	@sFiltro varchar(500) 


Declare 
	@iTipoInventario int, 
	@iClaves int, 
	@iClavesAgregadas int, 
	@iKeyx int, 
	@Folio varchar(6), 
	@sMensaje varchar(200) 

---	Parametros 	
	Set @iTipoInventario = @TipoInventario  
	Set @iClaves = @Claves  
	Set @iClavesAgregadas = 0 
	Set @iKeyx = 0 
	Set @Folio = '0' 
	Set @sSql = '' 
	Set @sFiltro = '' 
	Set @sMensaje = 'No se encontro información para generar el Inventario Aleatorio.' 
	
-------------- Preparar tabla base 	
	Select Top 0 @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, 
		   space(20) as ClaveSSA, space(7500) as DescripcionClave 
	Into #tmpClavesBase 
-------------- Preparar tabla base 	


-------------------------- Armar filtro 
---		spp_INV_Aleatorios 

	If (case when @TipoInventario in ( 1, 2, 3, 4 ) Then 1 else 0 end) = 0   
	Begin 
		RAISERROR ('No se ha especificado un Tipo válido de Inventario Aleatorio', 16, 1)  --;
		return 
	End 

	Set @sFiltro = ' Where ' + char(10) + 
		'	V.IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + char(10) + 
		'	And V.IdEstado = ' + char(39) + @IdEstado + char(39) + char(10) + 
		'	And V.IdFarmacia = ' + char(39) + @IdFarmacia + char(39)  

	If @TipoInventario = 1		---- Cierre de personal 
		Set @sFiltro = @sFiltro + ' And V.IdPersonal = ' + char(39) + @IdPersonal + char(39) + 
			' And convert(varchar(10), V.FechaSistema, 120) = ' + char(39) + @FechaSistema + char(39)   
		
	If @TipoInventario = 2		---- Cambio de dia 
		Set @sFiltro = @sFiltro + ' And convert(varchar(10), V.FechaSistema, 120) = ' + char(39) + @FechaSistema + char(39)   		
		
	If @TipoInventario = 3		---- Cierre de periodo  
		Set @sFiltro = @sFiltro + ' And FolioCierre = ' + char(39) + cast(@FolioCierre as varchar) + char(39)   				
	
	If @TipoInventario = 4		---- Inventario de un número de Claves, un mes a partir de la fecha de invocacion   
		Set @sFiltro = @sFiltro + ' And convert(varchar(10), V.FechaSistema, 120) between ' 
					   + char(39) + convert(varchar(10), dateadd(mm, -1, getdate()), 120) + char(39) 
					   + ' and ' + char(39) + convert(varchar(10), getdate(), 120) + char(39) 
		
-------------------------- Armar filtro 
		
		

--- Obetener el listado de claves a procesar 	
	Set @sSql = 'Insert Into #tmpClavesBase  
	Select ' + 
		char(39) + @IdEmpresa + char(39) + ' as IdEmpresa, ' + 
		char(39) + @IdEstado + char(39) + ' as IdEstado, ' + 
		char(39) + @IdFarmacia + char(39) +  ' as IdFarmacia,
		P.ClaveSSA, P.DescripcionClave 
	From VentasEnc V (NoLock) 
	Inner Join VentasDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) ' + char(10) + 
	@sFiltro + char(10) +  
	' Group by P.ClaveSSA, P.DescripcionClave  ' 
	Print @sSql 
	Exec(@sSql) 
	
---		spp_INV_Aleatorios 
	
	
---	Lista de claves a marcar como bloqueadas 
	Select B.IdEmpresa, B.IdEstado, B.IdFarmacia, B.ClaveSSA, B.DescripcionClave, 
		 cast(E.Existencia as int) as Existencia, 'S' as Status, 0 as Inventariar, identity(int, 1, 1) as Keyx   
	Into #tmpClaves  
	From #tmpClavesBase B (NoLock) 
	Inner Join  vw_ExistenciaPorSales E (NoLock) 
		On ( B.IdEmpresa = E.IdEmpresa and B.IdEstado = E.IdEstado and B.IdFarmacia = E.IdFarmacia and B.ClaveSSA = E.ClaveSSA ) 
	Order By B.DescripcionClave 
	
---	Select * From #tmpClavesBase 
	
	
--- Iniciar el proceso aleatorio de seleccion 		
	While @iClavesAgregadas < @iClaves  
	Begin 
		Select Top 1 @iKeyx = Keyx From #tmpClaves (NoLock) Where Inventariar  = 0 Order by NEWID() 

		Update C Set Inventariar = 1 
		From #tmpClaves C 
		Where Keyx = @iKeyx 
		
		Set @iClavesAgregadas = @iClavesAgregadas + 1  
	End 


----------------------------------------- Generar el registro 
--	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdProducto, 'S' as Status  
	If Exists ( Select Top 1 * From #tmpClaves Where Inventariar = 1 ) 
	Begin 
		Update E Set Status = 'S'
		From FarmaciaProductos E (NoLock) 
		Inner Join vw_Productos P (NoLock) On ( E.IdProducto = P.IdProducto ) 
		Inner Join #tmpClaves C (NoLock) 
			On ( E.IdEmpresa = C.IdEmpresa and E.IdEstado = C.IdEstado and E.IdFarmacia = C.IdFarmacia 
				 and P.ClaveSSA = C.ClaveSSA and C.Inventariar = 1 and E.Status = 'A') 

 		--	Select @Folio = dbo.fg_INV_FolioAleatorio( @IdEmpresa, @IdEstado, @IdFarmacia )
		Set @Folio = (Select dbo.fg_INV_FolioAleatorio( @IdEmpresa, @IdEstado, @IdFarmacia ))  
		Insert Into INV_AleatoriosEnc ( IdEmpresa, IdEstado, IdFarmacia, Folio, IdPersonal, FechaRegistro, TipoInventario )  
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @IdPersonal, getdate() as FechaRegistro, @iTipoInventario as TipoInventario 

		Insert Into INV_AleatoriosDet ( IdEmpresa, IdEstado, IdFarmacia, Folio, ClaveSSA, ExistenciaLogica ) 
		Select IdEmpresa, IdEstado, IdFarmacia, @Folio, ClaveSSA, Existencia as ExistenciaLogica  
		From #tmpClaves 
		Where Inventariar = 1  
		
		Set @sMensaje = 'Folio de Inventario aleatorio generado satisfactoriamente con el Folio [ ' + @Folio + ' ].' 
	End 	
----------------------------------------- Generar el registro 

-------------------- Salida final 
	Select @Folio as Folio, @sMensaje as Mensaje 
		
End 
Go--#SQL 

