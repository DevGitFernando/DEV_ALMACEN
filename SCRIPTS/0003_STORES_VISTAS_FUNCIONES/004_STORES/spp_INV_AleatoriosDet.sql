
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INV_AleatoriosDet' and xType = 'P')
    Drop Proc spp_Mtto_INV_AleatoriosDet
Go--#SQL
  
Create Proc spp_Mtto_INV_AleatoriosDet 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0226', 
	@Folio varchar(6) = '000001', @ClaveSSA varchar(20) = '402', @Existencia int = 204    
)
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint,		
		@EsConteo_01 tinyint, @EsConteo_02 tinyint, @EsConteo_03 tinyint, @Continua tinyint,
		@Conciliado tinyint,
		@sSql varchar(7500), 
		@sFiltro varchar(500)		  


	Set @sSql = '' 
	Set @sFiltro = ''
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 

	Set @EsConteo_01 = 0 
	Set @EsConteo_02 = 0 
	Set @EsConteo_03 = 0
	Set @Continua = 0
	
	Set @sFiltro = ' Where ' + char(10) + '	IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + char(10) + 
		'	and IdEstado = ' + char(39) + @IdEstado + char(39) + char(10) + '	and IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + char(10) +
		'  and Folio = ' + char(39) + @Folio + char(39) + char(10) + '	and ClaveSSA = ' + char(39) + @ClaveSSA + char(39) 

	Set @EsConteo_01 = (Select EsConteo_01 From INV_AleatoriosDet (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
						and IdFarmacia = @IdFarmacia  and Folio = @Folio and ClaveSSA = @ClaveSSA )

	Set @EsConteo_02 = (Select EsConteo_02 From INV_AleatoriosDet (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
						and IdFarmacia = @IdFarmacia  and Folio = @Folio and ClaveSSA = @ClaveSSA )

	Set @EsConteo_03 = (Select EsConteo_03 From INV_AleatoriosDet (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
						and IdFarmacia = @IdFarmacia  and Folio = @Folio and ClaveSSA = @ClaveSSA )

	If @EsConteo_01 = 0
	Begin		
		Set @Continua = 1
		Set @sSql = ' Update INV_AleatoriosDet Set Conteo_01 = ' + char(39) + cast(@Existencia as varchar) + char(39) + ', EsConteo_01 = 1, Fecha_01 = GetDate() ' + @sFiltro
		--Print @sSql
		Exec(@sSql)
		If ( Select (ExistenciaLogica - Conteo_01 ) From INV_AleatoriosDet (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
						and IdFarmacia = @IdFarmacia  and Folio = @Folio and ClaveSSA = @ClaveSSA ) = 0
		Begin 
			Update INV_AleatoriosDet Set Conciliado = 1
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  and Folio = @Folio and ClaveSSA = @ClaveSSA
		End
	End
	
	If @Continua = 0 and @EsConteo_02 = 0
	Begin 
		Set @Continua = 1
		Set @sSql = ' Update INV_AleatoriosDet Set Conteo_02 = ' + char(39) + cast(@Existencia as varchar) + char(39) + ', EsConteo_02 = 1, Fecha_02 = GetDate() ' + @sFiltro
		Exec(@sSql)
		If ( Select (ExistenciaLogica - Conteo_02 ) From INV_AleatoriosDet (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
						and IdFarmacia = @IdFarmacia  and Folio = @Folio and ClaveSSA = @ClaveSSA ) = 0
		Begin 
			Update INV_AleatoriosDet Set Conciliado = 1
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  and Folio = @Folio and ClaveSSA = @ClaveSSA
		End
	End

	If @Continua = 0 and @EsConteo_03 = 0
	Begin 
		Set @Continua = 1
		Set @sSql = ' Update INV_AleatoriosDet Set Conteo_03 = ' + char(39) + cast(@Existencia as varchar) + char(39) + ', EsConteo_03 = 1, Fecha_03 = GetDate() ' + @sFiltro
		Exec(@sSql)
		If ( Select (ExistenciaLogica - Conteo_03 ) From INV_AleatoriosDet (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
						and IdFarmacia = @IdFarmacia  and Folio = @Folio and ClaveSSA = @ClaveSSA ) = 0
		Begin 
			Update INV_AleatoriosDet Set Conciliado = 1
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  and Folio = @Folio and ClaveSSA = @ClaveSSA
		End
	End	

	-- Actualizar el status de los productos de las claves conciliadas
	Update E Set E.Status = @sStatus
	From FarmaciaProductos E (NoLock) 
	Inner Join vw_Productos P (NoLock) On ( E.IdProducto = P.IdProducto ) 
	Inner Join INV_AleatoriosDet C (NoLock) 
		On ( E.IdEmpresa = C.IdEmpresa and E.IdEstado = C.IdEstado and E.IdFarmacia = C.IdFarmacia 
			 and P.ClaveSSA = C.ClaveSSA and (C.Conciliado = 1 OR C.EsConteo_03 = 1) )
	Where C.IdEmpresa = @IdEmpresa and C.IdEstado = @IdEstado and C.IdFarmacia = @IdFarmacia 
	and C.Folio = @Folio and C.ClaveSSA = @ClaveSSA 

	Set @sMensaje = 'Información Guardada Satisfactoriamente....'

	--	Regresar la Clave Generada
    Select @Folio as Folio, @sMensaje as Mensaje 
	
End
Go--#SQL	



	