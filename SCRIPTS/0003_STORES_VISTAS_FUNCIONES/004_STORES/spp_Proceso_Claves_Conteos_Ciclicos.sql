

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Proceso_Claves_Conteos_Ciclicos' and xType = 'P' )
   Drop proc spp_Proceso_Claves_Conteos_Ciclicos
Go--#SQL 

-----		Exec spp_Proceso_Claves_Conteos_Ciclicos '001', '11', '0005', '0001'

Create Proc spp_Proceso_Claves_Conteos_Ciclicos 
(  
	@IdEmpresa  varchar(3)= '001', @IdEstado varchar(2)= '11', @IdFarmacia varchar(4) = '0005', @IdPersonal varchar(4) = '0005' 
) 
With Encryption 
As 
Begin 
	Set dateformat YMD
	Set NoCount On 

	Declare @Claves int, @Año int, @Mes int, 
	@PiezasTotal int, 
	@A_Claves int, @B_Claves int, @C_Claves int, 
	@A_Pzas int, @B_Pzas int, @C_Pzas int, 
	@A_Frec int, @B_Frec int, @C_Frec int, 
	@Porc_Conteo_A numeric(14, 2), @Porc_Conteo_B numeric(14, 2), @Porc_Conteo_C numeric(14, 2),
	@ClavesAContar int
	
	Set @ClavesAContar = 0
	
	Set @Claves = 0
	Set @Año = 0
	Set @Mes = 0
	Set @PiezasTotal = 0	
	
	Set @A_Claves = 0	
	Set @B_Claves = 0	
	Set @C_Claves = 0	
	
	Set @A_Pzas = 0	
	Set @B_Pzas = 0	
	Set @C_Pzas = 0	
	
	Set @A_Frec = 0	
	Set @B_Frec = 0	
	Set @C_Frec = 0	
	
	Set @Porc_Conteo_A = 0
	Set @Porc_Conteo_B = 0
	Set @Porc_Conteo_C = 0
	
	Select @Año = datepart(yyyy, getdate())
	Select @Mes = (datepart(mm, getdate()) - 1)
	
	If @Mes = 0
	Begin
		Set @Año = @Año - 1
		Set @Mes = 12
	End
	
	Select * Into #tmp_vw_Productos_CodigoEAN From vw_Productos_CodigoEAN 	
	
	-----------------  se obtienen los montos de las claves de las ventas  --------------------------------------------------------------
	Select P.ClaveSSA, P.DescripcionSal, SUM(L.CantidadVendida) as Cantidad
	Into #tmpVentasClaves
	From VentasEnc E 
	Inner Join VentasDet D 
		On ( D.IdEmpresa = E.IdEmpresa And D.IdEstado = E.IdEstado And D.IdFarmacia = E.IdFarmacia And D.FolioVenta = E.FolioVenta )
	Inner Join VentasDet_Lotes L
		On ( L.IdEmpresa = D.IdEmpresa And L.IdEstado = D.IdEstado And L.IdFarmacia = D.IdFarmacia And L.FolioVenta = D.FolioVenta
			and L.IdProducto = D.IdProducto and L.CodigoEAN = D.CodigoEAN )
	Inner Join vw_Productos_CodigoEAN P (NOLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )
	Where L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia
	and DATEPART(yyyy, E.FechaRegistro) = @Año and DATEPART(mm, E.FechaRegistro) = @Mes
	Group By P.ClaveSSA, P.DescripcionSal
	
	-----------------  se obtienen los montos de las claves de las Transferencias  --------------------------------------------------------------
	Select P.ClaveSSA, P.DescripcionSal, SUM(L.CantidadEnviada) as Cantidad
	Into #tmpTransferenciasClaves
	From TransferenciasEnc E 
	Inner Join TransferenciasDet D 
		On ( D.IdEmpresa = E.IdEmpresa And D.IdEstado = E.IdEstado And D.IdFarmacia = E.IdFarmacia And D.FolioTransferencia = E.FolioTransferencia )
	Inner Join TransferenciasDet_Lotes L
		On ( L.IdEmpresa = D.IdEmpresa And L.IdEstado = D.IdEstado And L.IdFarmacia = D.IdFarmacia And L.FolioTransferencia = D.FolioTransferencia
			and L.IdProducto = D.IdProducto and L.CodigoEAN = D.CodigoEAN )
	Inner Join vw_Productos_CodigoEAN P (NOLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )
	Where L.IdEmpresa = @IdEmpresa and L.IdEstado = @IdEstado and L.IdFarmacia = @IdFarmacia and E.TipoTransferencia = 'TS'
	and DATEPART(yyyy, E.FechaRegistro) = @Año and DATEPART(mm, E.FechaRegistro) = @Mes and E.Status Not In ('C')
	Group By P.ClaveSSA, P.DescripcionSal
	
	---------------------------------------------------------------------------------------------------------------------------------------------
	
	Select *, @PiezasTotal as PiezasTotal, 
	cast(0 as float) as Participacion, space(1) as Categoria  
	Into #tmpClavesParticipacion From #tmpVentasClaves (Nolock)
	
	Update T Set T.Cantidad = ( T.Cantidad + TS.Cantidad )
	From #tmpVentasClaves T (Nolock)
	Inner Join #tmpTransferenciasClaves TS (Nolock) On ( T.ClaveSSA = TS.ClaveSSA )	
	
	Insert Into #tmpClavesParticipacion ( ClaveSSA, DescripcionSal, Cantidad )
	Select T.ClaveSSA, T.DescripcionSal, T.Cantidad
	From #tmpTransferenciasClaves T (Nolock)
	Where Not Exists ( Select * From #tmpClavesParticipacion TS (Nolock) Where T.ClaveSSA = TS.ClaveSSA )	

	---------------------------------------------------------------------------------------------------------------------------------------------
	Select @PiezasTotal = SUM(Cantidad) From #tmpClavesParticipacion 
	
	------------  SE OBTIENE LA PARTICIPACION Y LA CATEGORIZACION DE LAS CLAVES ----------------------------------------------------
			
	Update T Set T.PiezasTotal = @PiezasTotal
	From #tmpClavesParticipacion T
	
	Update T Set T.Participacion = IsNull(((T.Cantidad/@PiezasTotal) * 100.00), 0)
	From #tmpClavesParticipacion T
		
	Update T Set T.Categoria = 'A'
	From #tmpClavesParticipacion T Where Participacion Between 0 and 15
	
	Update T Set T.Categoria = 'B'
	From #tmpClavesParticipacion T Where Participacion Between 16 and 45
	
	Update T Set T.Categoria = 'C'
	From #tmpClavesParticipacion T Where Participacion Between 46 and 100
	
	------------------------------------------------------------------------
	
	Select @A_Claves = IsNull(count(distinct ClaveSSA), 0) From #tmpClavesParticipacion Where Categoria = 'A'	
	Select @B_Claves = IsNull(count(distinct ClaveSSA), 0) From #tmpClavesParticipacion Where Categoria = 'B'	
	Select @C_Claves = IsNull(count(distinct ClaveSSA), 0) From #tmpClavesParticipacion Where Categoria = 'C'
		
	Select @A_Frec = 4	
	Select @B_Frec = 2	
	Select @C_Frec = 1	
	
	-----------------------------------------------------------------------------------------------------------------------	
	Set @A_Pzas = (@A_Claves * @A_Frec)
	Set @B_Pzas = (@B_Claves * @B_Frec)
	Set @C_Pzas = (@C_Claves * @C_Frec)
	
	Declare @Total int
	Set @Total = 0
	
	Set @Total = (@A_Pzas + @B_Pzas + @C_Pzas)
	-----------------------------------------------------------------------------------------------------------------------
	
	If @A_Pzas > 0
		Set @Porc_Conteo_A = IsNull((@A_Pzas/(@Total * 1.00)), 0)
	
	If @B_Pzas > 0
		Set @Porc_Conteo_B = IsNull((@B_Pzas/(@Total * 1.00)), 0)
	
	If @C_Pzas > 0
		Set @Porc_Conteo_C = IsNull((@C_Pzas/(@Total * 1.00)), 0)
	
	------------------------------------------------------------------------------------------------------------------------
	Select top 0 space(1) as Categoria, @A_Claves as Claves, @A_Frec AS Frecuencia, 
	@A_Pzas as Total, @Porc_Conteo_A as Porc_Conteo, 0 as Conteos
	Into #tmpConteosResumen
	------------------------------------------------------------------------------------------------------------------------
	
	Insert Into #tmpConteosResumen
	Select 'A', @A_Claves, @A_Frec, @A_Pzas, @Porc_Conteo_A, 0
	
	Insert Into #tmpConteosResumen
	Select 'B', @B_Claves, @B_Frec, @B_Pzas, @Porc_Conteo_B, 0
	
	Insert Into #tmpConteosResumen
	Select 'C', @C_Claves, @C_Frec, @C_Pzas, @Porc_Conteo_C, 0	
		
	Set @ClavesAContar = (@Total/312)
		
	Update #tmpConteosResumen Set Conteos = IsNull((Porc_Conteo * @ClavesAContar), 0)
	-----====================================================================================================================	
		
	----- ============== GENERAR TABLA PARA EL FOLIO DEL CONTEO  ============================================================
	Declare @FolioConteo varchar(30), @Mensaje varchar(8000)
	
	Set @FolioConteo = ''
	Set @Mensaje = ''
	
	Select top 0 FolioConteo as Folio, space(8000) as Mensaje 
	Into #tmpFolioConteo
	From Inv_ConteosCiclicosEnc 
	
	Insert Into #tmpFolioConteo
	Exec spp_Mtto_Inv_ConteosCiclicosEnc @IdEmpresa, @IdEstado, @IdFarmacia, '*', @IdPersonal, 1
	
	If Exists ( Select * From #tmpFolioConteo ) 
	Begin
	
		Select @FolioConteo = Folio From #tmpFolioConteo
		
		Insert Into Inv_ConteosCiclicosDet (IdEmpresa, IdEstado, IdFarmacia, FolioConteo, ClaveSSA, Cantidad, Total_Piezas, Participacion, Categoria)
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioConteo, ClaveSSA, Cantidad, PiezasTotal, Participacion, Categoria  
		From #tmpClavesParticipacion
		
		Insert Into Inv_ConteosCiclicos_Resumen 
		(
			IdEmpresa, IdEstado, IdFarmacia, FolioConteo, Categoria, Claves, Frecuencia, Total_Claves, Porc_Conteo, Conteos
		)
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioConteo, Categoria, Claves, Frecuencia, Total, Porc_Conteo, Conteos  
		From #tmpConteosResumen
		 
		Set @Mensaje = 'La Información de guardo con el Folio: ' + @FolioConteo + '  satisfactoriamente'
	End
		
	----------------------------------------------------------------------------------------------------------------------------------------
	--Select * From #tmpClavesParticipacion

	--Select * From #tmpSalidaEjemplo
	
	--------    spp_Proceso_Claves_Conteos_Ciclicos
	
	Select @FolioConteo as Folio, @Mensaje as Mensaje 

End 
Go--#SQL 