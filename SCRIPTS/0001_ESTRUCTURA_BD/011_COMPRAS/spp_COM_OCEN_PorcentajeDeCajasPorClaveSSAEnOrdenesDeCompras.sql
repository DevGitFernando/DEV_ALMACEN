
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_COM_OCEN_PorcentajeDeCajasPorClaveSSAEnOrdenesDeCompras' and xType = 'P' ) 
   Drop Proc spp_COM_OCEN_PorcentajeDeCajasPorClaveSSAEnOrdenesDeCompras
Go--#SQL   

Create Proc spp_COM_OCEN_PorcentajeDeCajasPorClaveSSAEnOrdenesDeCompras
( 
	@IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2) = '11' ,@IdFarmacia Varchar(4) = '0001',
		@NumCompras varchar(4) = '3000', @Porcentaje Varchar(200) = 10, @Tabla Varchar(200) = 'Temp_65e15520c0d1475482051320434603'
)  
With Encryption 
As 
Begin 
Set NoCount On
    
		Declare @iPzas Int,
				@iProm Numeric(14, 4),
				@sSql Varchar(5000)


		--Drop table #TempConcentradoClaveSSA
		create table #TempConcentradoClaveSSA
		(
			IdClaveSSA Varchar(8) Default '',
			ClaveSSA Varchar(100),
			Piezas Int,
			NumCompras varchar(4),
			Promedio Numeric(14, 4)
		)
		
		--Drop table #TempClaves
		create table #TempClaves
		(
			IdClaveSSA Varchar(8) Default '',
			ClaveSSA Varchar(100),
			Cantidad Int,
		)
		
		Set @sSql  =
		'Update X Set X.IdClaveSSA = P.IdClaveSSA_Sal,  X.ClaveSSA = P.ClaveSSA ' + CHAR(13) +
		'From ' + @Tabla + ' X(NoLock) ' + CHAR(13) +
		'Inner Join vw_Productos_CodigoEAN P (NoLock) On (X.CodigoEAN = P.CodigoEAN)' 
		Exec (@sSql)

		--Drop table #TempClaves
		Set @sSql  =
		'Insert #TempClaves' + CHAR(13) +
		'Select IdClaveSSA, ClaveSSA, Sum(Cantidad) As Cantidad' + CHAR(13) +	
		'From ' + @Tabla + ' X(NoLock) ' + CHAR(13) +
		'Group By IdClaveSSA, ClaveSSA'
		Exec (@sSql)
		

		Set @sSql  =
		'Declare @campoClaveSSA Varchar(100) ' + CHAR(13) + CHAR(13) +
		'Declare query_Cursor Cursor For ' + CHAR(13) +
		'	Select ClaveSSA  From #TempClaves' + Char(13) +
		'		open Query_cursor Fetch From Query_cursor into @campoClaveSSA ' + Char(13) +
		'			while @@Fetch_status = 0 ' + Char(13) +
		'			begin' + Char(13) +
		'				Insert #TempConcentradoClaveSSA' + Char(13) +
		'				Exec spp_COM_OCEN_PromedioDeCajasPorOrdenesDeComprasPorClaveSSA ' + Char(39) + @IdEmpresa + Char(39) + ', ' + Char(39) + @IdEstado + Char(39) + ', ' + CHAR(13) +
		'					' + Char(39) + @IdFarmacia + Char(39) + ', @campoClaveSSA, ' + @NumCompras + Char(13) +
		'				Fetch next From query_cursor into  @campoClaveSSA ' + Char(13) +
		'			End' + Char(13) +
		'		close Query_cursor' + Char(13) +
		'deallocate Query_cursor'
		Print (@sSql)
		Exec(@sSql)


		Select C.IdClaveSSA, C.ClaveSSA, S.DescripcionCortaClave As Descripcion, (((Cantidad * 100)/NULLIF(Promedio, 0))- 100) Porcentaje
			From  #TempConcentradoClaveSSA C (NoLock)
			Inner Join #TempClaves T (NoLock) On (C.ClaveSSA = T.ClaveSSA)
			Inner Join vw_ClavesSSA_Sales S (NoLock) On (C.ClaveSSA = S.ClaveSSA)
			Where (((Cantidad * 100)/NULLIF(Promedio, 0))- 100) > + @Porcentaje

End
Go--#SQL