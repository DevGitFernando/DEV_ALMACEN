
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CEDIS_Procesar_Claves_Surtir' and xType = 'P' ) 
   Drop Proc spp_CEDIS_Procesar_Claves_Surtir
Go--#SQL

Create Proc spp_CEDIS_Procesar_Claves_Surtir (  @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @Ejecutar smallint = 0 ) 
With Encryption 
As 
Begin 
Set NoCount On 

	-- Se crea la tabla temporal.
	Create Table #tmpClaves
	(
		IdEmpresa varchar(3) Not Null Default '',
		IdEstado varchar(2) Not Null Default '',
		IdFarmacia varchar(4) Not Null Default '', 
		FolioPedido varchar(6) Not Null Default '', 
		IdClaveSSA_Sal varchar(4) Not Null Default '', 	
		ExistenciaDisponible numeric(14, 4) Not Null Default 0,	
		CantidadRequerida numeric(14, 4) Not Null Default 0,		
        CantidadSugerida numeric(14, 4) Not Null Default 0,
		PorcentajeAsignado numeric(14, 4) Not Null Default 0,
		CantidadAsignada numeric(14, 4) Not Null Default 0,
		CantidadDistribuida numeric(14, 4) Not Null Default 0,
	)
	
	-- Se llena la tabla temporal
	Insert Into #tmpClaves
	Select IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdClaveSSA, 0, 0, CantidadSugerida, 0, 0, 0
	From Pedidos_Cedis_Det_Surtido(NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado

	-- Se obtiene la Existencia Disponible.
	Update C
	Set ExistenciaDisponible = E.ExistenciaDisponible
	From #tmpClaves C(NoLock)
	Inner Join CEDIS_Existencia_Det E(NoLock) On ( C.IdEmpresa = E.IdEmpresa And C.IdEstado = E.IdEstado And C.IdClaveSSA_Sal = E.IdClaveSSA )
	Where Status = 'A'
	
	-- Se obtienen las cantidades totales de cada Clave
	Update C
	Set CantidadRequerida = ( Select Sum(CantidadSugerida) From #tmpClaves N (NoLock) Where C.IdClaveSSA_Sal = N.IdClaveSSA_Sal) 
	From #tmpClaves C(NoLock)

	-- Se obtienen los porcentajes
	Update C
	Set PorcentajeAsignado = ( CantidadSugerida / CantidadRequerida ) * 100 
	From #tmpClaves C(NoLock)
	
	-- Se obtienen las cantidades asignadas.
	Update C
	Set CantidadAsignada = Floor( ( (PorcentajeAsignado / 100) * CantidadRequerida) ) 
	From #tmpClaves C(NoLock)

	-- Se obtienen las cantidades distribuidas de cada Clave
	Update C
	Set CantidadDistribuida = ( Select Sum(CantidadAsignada) From #tmpClaves N (NoLock) Where C.IdClaveSSA_Sal = N.IdClaveSSA_Sal) 
	From #tmpClaves C(NoLock)


	-- Se inserta en una tabla temporal cada una de las claves y su cantidad distribuida.
	Select Distinct IdClaveSSA_Sal, CantidadDistribuida
	Into #tmpCantidades
	From #tmpClaves(NoLock)

	If @Ejecutar = 1
	  Begin
		-- Se actualiza la existencia del CEDIS
		Update E
		Set E.ExistenciaDisponible = E.ExistenciaDisponible - C.CantidadDistribuida
		From #tmpCantidades C(NoLock)
		Inner Join CEDIS_Existencia_Det E(NoLock) On ( C.IdClaveSSA_Sal = E.IdClaveSSA )
		Where E.Status = 'A' And IdEmpresa = @IdEmpresa And IdEstado = @IdEstado
	  End
	Else
	  Begin

		-- Se verifica si existe la tabla. En caso de existir se elimina. En caso contrario se crea.
		If Exists ( Select Name From Sysobjects(NoLock) Where Name = 'tmpDistribucion' and xType = 'U' ) 
		  Begin
			Drop Table tmpDistribucion
		  End

		-- Se insertan los datos en la tabla
		Select * Into tmpDistribucion From #tmpClaves(NoLock) Order By IdClaveSSA_Sal, PorcentajeAsignado

		-- Se devuelven los datos para verificacion
		Select * From tmpDistribucion(NoLock) Order By IdClaveSSA_Sal, PorcentajeAsignado
	  End

End 
Go--#SQL



