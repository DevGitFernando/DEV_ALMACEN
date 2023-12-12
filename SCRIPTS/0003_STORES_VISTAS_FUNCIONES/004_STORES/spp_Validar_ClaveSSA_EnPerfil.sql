-----------------------------------------------------------------------------------------------------------------------------------------------------------------   
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Validar_ClaveSSA_EnPerfil' And xType = 'P' )
	Drop Proc spp_Validar_ClaveSSA_EnPerfil 
Go--#SQL 

-- Exec spp_Validar_ClaveSSA_EnPerfil '001', '11', '0003', 1, 100  

Create Procedure spp_Validar_ClaveSSA_EnPerfil 
( 
	@IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '0014', @ClaveSSA varchar(100) = '010.000.0101.00', @TipoUnidad int = 1, 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0010'  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@bValidarClaveEnPerfil bit, 
	@bForzarDispensacion_CajasCompletas bit  


	Set @bForzarDispensacion_CajasCompletas = 0 

---------------- Parametros 
	Select @bValidarClaveEnPerfil = dbo.fg_GetParametro_ValidarClaveEnPerfil( @IdEstado, @IdFarmacia, @TipoUnidad ) 
	--- Valor 0 indica que solo se busca en el Catalogo de claves 


	Select top 1 @bForzarDispensacion_CajasCompletas = Dispensacion_CajasCompletas 
	From vw_Claves_Precios_Asignados (NoLock) 
	Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and ClaveSSA = @ClaveSSA and Status = 'A' 

---------------- Tabla base 
	Select top 0 space(100) as ClaveSSA, space(7500) as DescripcionClave, 0 as Tipo Into #tmpDatos 
	
	

	If @bValidarClaveEnPerfil = 1 
	Begin 
		Insert into #tmpDatos 
		Select top 1 ClaveSSA, Descripcion, 1 
		From vw_Relacion_ClavesSSA_Claves P (NoLock) 
		Where IdEstado = @IdEstado --- and IdFarmacia = @IdFarmacia 
			and ClaveSSA_Relacionada = @ClaveSSA And Status = 'A' 	
	
	
		Insert into #tmpDatos 
		Select top 1 ClaveSSA, DescripcionClave, 1 
		From vw_CB_CuadroBasico_Farmacias P (NoLock) 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ClaveSSA_Aux = @ClaveSSA and IdCliente = @IdCliente 
			and StatusMiembro = 'A' and StatusClave = 'A' 
	End 
	
	If @bValidarClaveEnPerfil = 0  
	Begin 
		Insert into #tmpDatos  
		Select top 1 ClaveSSA, DescripcionClave, 2
		From vw_ClavesSSA_Sales P (NoLock) 
		Where ClaveSSA_Aux = @ClaveSSA  
	End 	
	

---------------------------------------------------------------------------------------------------------------
-------------------------------------	Salida Final 
	Select Top 1 @TipoUnidad as TipoUnidad, ClaveSSA, DescripcionClave, Tipo, @bForzarDispensacion_CajasCompletas as ForzarDispensacion_CajasCompletas
	From #tmpDatos  (NoLock) 



End 
Go--#SQL 

