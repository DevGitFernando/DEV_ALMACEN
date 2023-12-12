If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_TipoMovimientos_Totales_Estados' and xType = 'P' ) 
   Drop Proc spp_Rpt_TipoMovimientos_Totales_Estados
Go--#SQL 

----  Exec spp_Rpt_TipoMovimientos_Totales_Estados  '21', '2011-07-01', '2011-10-31' 

Create Proc spp_Rpt_TipoMovimientos_Totales_Estados 
( 
	@IdEstado varchar(2) = '21', @FechaInicial varchar(10) = '2011-07-01' , @FechaFinal varchar(10) = '2011-10-31'
) 	
With Encryption 
As 
Begin 
Set NoCount On  
Set Dateformat YMD
 
Declare 
	@iOpcion int,
	@Año int,  
	@Mes int

	Select @Año = datepart(yy, @FechaInicial) 
	Select @Mes = datepart(mm, @FechaInicial) 

	---- Se procesa para sacar los totales de los diferentes tipos de Movimientos
	Select E.IdEstado, D.IdSubFarmacia, 
	E.IdTipoMovto_Inv As TipoMovto, datepart(yy, E.FechaRegistro) as Año, datepart(mm, E.FechaRegistro) as Mes,
	sum(D.Cantidad) As Piezas, Count(Distinct C.ClaveSSA) As TotalClaves
	Into #tmpTotalesTipoMovtosEdos
	From MovtosInv_Enc E (Nolock) 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes D (Nolock)
		On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioMovtoInv = D.FolioMovtoInv )
	Inner Join vw_Productos_CodigoEAN C (Nolock) On ( D.IdProducto = C.IdProducto And D.CodigoEAN = C.CodigoEAN )	
	Where E.IdEstado = @IdEstado -- And Convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal
	And datepart(yy, E.FechaRegistro) = @Año And datepart(mm, E.FechaRegistro) = @Mes
	Group By E.IdEmpresa, E.IdEstado, D.IdSubFarmacia, E.IdTipoMovto_Inv, datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro)
	Order By E.IdEstado, D.IdSubFarmacia, datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro)

	---- Se borra la informacion de la tabla base donde se guardan los totales de los tipos de movimientos.
	Delete From Rpt_Claves_TipoMovimientos_Estados 
	Where  IdEstado = @IdEstado And Año = @Año And Mes = @Mes

	---- Se llena la tabla base donde se guardan los totales de los tipos de movimientos.
	Insert Into Rpt_Claves_TipoMovimientos_Estados
	(
		IdEstado, Estado, IdSubFarmacia, SubFarmacia, 
		IdTipoMovto, DescripcionTipoMovto, Efecto_Movto,
		Año, Mes, NombreMes, Piezas, TotalClaves
	) 
	Select T.IdEstado, E.Nombre As Estado, T.IdSubFarmacia, S.Descripcion As SubFarmacia, 
	T.TipoMovto, M.Descripcion As DescripcionTipoMovto, M.Efecto_Movto,  
	T.Año, T.Mes, dbo.fg_NombresDeMesNumero(T.Mes) as NombreMes, T.Piezas, T.TotalClaves	
	From #tmpTotalesTipoMovtosEdos T (Nolock)
	Inner Join Movtos_Inv_Tipos M (Nolock) On ( T.TipoMovto = M.IdTipoMovto_Inv )
	Inner Join CatEstados E (Nolock) On ( T.IdEstado = E.IdEstado )	
	Inner Join CatEstados_SubFarmacias S (Nolock) On ( T.IdEstado = S.IdEstado And T.IdSubFarmacia = S.IdSubFarmacia )
	Order By T.IdEstado, T.IdSubFarmacia, T.Año, T.Mes, M.Efecto_Movto		
	
--	spp_Rpt_TipoMovimientos_Totales_Estados 
--	Select * From Rpt_Claves_TipoMovimientos_Estados (Nolock)


End	
Go--#SQL 
	