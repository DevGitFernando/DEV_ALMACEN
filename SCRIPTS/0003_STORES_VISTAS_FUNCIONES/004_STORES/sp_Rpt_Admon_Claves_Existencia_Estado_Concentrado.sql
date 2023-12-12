


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Rpt_Admon_Claves_Existencia_Estado_Concentrado' and xType = 'P' )
   Drop Proc sp_Rpt_Admon_Claves_Existencia_Estado_Concentrado 
Go--#SQL 

Create Proc sp_Rpt_Admon_Claves_Existencia_Estado_Concentrado 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11'  
) 
With Encryption 
As 
Begin 
Declare @sSql varchar(7500), 
	@IdFarmacia varchar(4)
	
	
	Set @IdFarmacia = '0001' 
	
	
	If @IdEstado = '21'
	Begin
		Set @IdFarmacia = '1000'
	End
	
	If @IdEstado = '20'
	Begin
		Set @IdFarmacia = '0100'
	End
	
	Select Distinct ClaveSSA, DescripcionSal, Presentacion_ClaveSSA, 0 as ContenidoPaquete, 
	0 as Admon_Causes, 0 as Admon_NoCauses, 0 as Venta_Causes, 0 as Venta_NoCauses,
	sum(Existencia) as ExistenciaGeneral
	Into #tmpClaves_Existencia_Concentrado
	From SVR_INV_Generar_Existencia_Detallado
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia >= @IdFarmacia
	Group By  ClaveSSA, DescripcionSal, Presentacion_ClaveSSA
	Order by DescripcionSal	
	
	---- Se actualiza el campo contenido paquete de la clave ssa 
    Update T Set ContenidoPaquete = S.ContenidoPaquete
    From #tmpClaves_Existencia_Concentrado T
    Inner Join CatClavesSSA_Sales S On ( T.ClaveSSA = S.ClaveSSA )
    
    ---------------------------------------------------------------------------------------------------------------------------------------
    -----------------   se concentran las existencias para las columnas de administracion y ventas de Causes y NO Causes ------------------
    
    ----  Se concentra la existencia para administracion de causes
	Select E.ClaveSSA, sum(E.Existencia) as Existencia
	Into #tmpExistenciaAdmon_Causes
	From SVR_INV_Generar_Existencia_Detallado E
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia >= @IdFarmacia AND E.ClaveLote LIKE '%*%'
	AND Exists ( Select ClaveSSA From vw_Claves_Precios_Asignados C Where C.IdEstado = @IdEstado and C.ClaveSSA = E.ClaveSSA )
	Group By E.ClaveSSA
	
	
	----  Se concentra la existencia para administracion de NO causes
	Select E.ClaveSSA, sum(E.Existencia) as Existencia
	Into #tmpExistenciaAdmon_No_Causes
	From SVR_INV_Generar_Existencia_Detallado E
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia >= @IdFarmacia AND E.ClaveLote LIKE '%*%'
	AND Not Exists ( Select ClaveSSA From vw_Claves_Precios_Asignados C Where C.IdEstado = @IdEstado and C.ClaveSSA = E.ClaveSSA )
	Group By E.ClaveSSA
	
	
	----  Se concentra la existencia para venta de causes
	Select E.ClaveSSA, sum(E.Existencia) as Existencia
	Into #tmpExistenciaVenta_Causes
	From SVR_INV_Generar_Existencia_Detallado E
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia >= @IdFarmacia AND E.ClaveLote NOT LIKE '%*%'
	AND Exists ( Select ClaveSSA From vw_Claves_Precios_Asignados C Where C.IdEstado = @IdEstado and C.ClaveSSA = E.ClaveSSA )
	Group By E.ClaveSSA
	
	
	----  Se concentra la existencia para venta de NO causes
	Select E.ClaveSSA, sum(E.Existencia) as Existencia
	Into #tmpExistenciaVenta_No_Causes
	From SVR_INV_Generar_Existencia_Detallado E
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia >= @IdFarmacia AND E.ClaveLote NOT LIKE '%*%'
	AND Not Exists ( Select ClaveSSA From vw_Claves_Precios_Asignados C Where C.IdEstado = @IdEstado and C.ClaveSSA = E.ClaveSSA )
	Group By E.ClaveSSA
    
    
    ----------------------------------------------------------------------------------------------------------------------------------------
    ----------------------------------------------------------------------------------------------------------------------------------------
    
    ---------------------------------------------------------------------------------------------------------------------------------------
    -----------------   se actualizan las existencias para las columnas de administracion y ventas de Causes y NO Causes ------------------
    
    Update T Set T.Admon_Causes = E.Existencia
    From #tmpClaves_Existencia_Concentrado T
    Inner Join #tmpExistenciaAdmon_Causes E On ( T.ClaveSSA = E.ClaveSSA )
    
    Update T Set T.Admon_NoCauses = E.Existencia
    From #tmpClaves_Existencia_Concentrado T
    Inner Join #tmpExistenciaAdmon_No_Causes E On ( T.ClaveSSA = E.ClaveSSA )
    
    Update T Set T.Venta_Causes = E.Existencia
    From #tmpClaves_Existencia_Concentrado T
    Inner Join #tmpExistenciaVenta_Causes E On ( T.ClaveSSA = E.ClaveSSA )
    
    Update T Set T.Venta_NoCauses = E.Existencia
    From #tmpClaves_Existencia_Concentrado T
    Inner Join #tmpExistenciaVenta_No_Causes E On ( T.ClaveSSA = E.ClaveSSA )
    
    ----------------------------------------------------------------------------------------------------------------------------------------
    ----------------------------------------------------------------------------------------------------------------------------------------
    
    Select * 
    From #tmpClaves_Existencia_Concentrado 
    
End 
Go--#SQL     	
