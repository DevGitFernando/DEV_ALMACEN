-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'sp_Proceso_Registro_Pedidos_CargaMasiva_000_Validar_Datos_De_Entrada' and xType = 'P' ) 
   Drop Proc sp_Proceso_Registro_Pedidos_CargaMasiva_000_Validar_Datos_De_Entrada  
Go--#SQL 

Create Proc sp_Proceso_Registro_Pedidos_CargaMasiva_000_Validar_Datos_De_Entrada
(
	@IdEmpresa varchar(3) = '001', @IdEstado Varchar(2) = '11', @IdFarmacia varchar(4) = '0005', @TipoPedido varchar(2) = '05' )
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@EsAlmacen bit,
	@ValidaClavePerfil bit
	
			
	Select @EsAlmacen = EsAlmacen From catfarmacias (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia		
	Select @ValidaClavePerfil = dbo.fg_GetParametro_ValidarClaveEnPerfil( @IdEstado, @IdFarmacia, @EsAlmacen )
	
	
	--Select @EsAlmacen as  EsAlmacen, @ValidaClavePerfil As ValidaClavePerfil

	Delete From Registro_Pedidos_CargaMasiva 
	Where ClaveSSA = ''
	
	If (@TipoPedido = '1')
	Begin
		Delete From Registro_Pedidos_CargaMasiva 
		Where ClaveSSA Not In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) Where TipoDeClave = '01' ) 
	End
	
	If (@TipoPedido = '2')
	Begin
		Delete From Registro_Pedidos_CargaMasiva 
		Where ClaveSSA Not In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) Where TipoDeClave = '02' )
	End
	
	If (@TipoPedido = '3')
	Begin
		Delete From Registro_Pedidos_CargaMasiva 
		Where ClaveSSA Not In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) Where TipoDeClave = '02' and EsControlado = 1 )
	End
	
	If (@TipoPedido = '4')
	Begin
		Delete From Registro_Pedidos_CargaMasiva 
		Where ClaveSSA Not In ( Select ClaveSSA From vw_ClavesSSA_Sales (Nolock) Where TipoDeClave = '02' and EsAntibiotico = 1 )
	End


	Update P Set P.IdClaveSSA = S.IdClaveSSA
	From Registro_Pedidos_CargaMasiva P 
	Inner Join vw_CB_CuadroBasico_Farmacias S (NoLock) On ( P.ClaveSSA = S.ClaveSSA_Aux )
	Where S.IdEstado = @IdEstado And s.IdFarmacia = @IdFarmacia
	
	
	--if (@EsAlmacen = 1 or @ValidaClavePerfil = 0)
	if ( @ValidaClavePerfil = 0 ) 
		Begin
			Update P Set P.IdClaveSSA = S.IdClaveSSA_Sal
			From Registro_Pedidos_CargaMasiva P 
			Inner Join vw_ClavesSSA_Sales S (NoLock) On ( P.ClaveSSA = S.ClaveSSA )  
		End
	
	
-------------------- Verificar la existencia y Multiplos de Cajas 	
	Update P Set CantidadCajas = (CantidadPzas / (ContenidoPaquete * 1.0))
	From Registro_Pedidos_CargaMasiva P 
	
	
	Update Registro_Pedidos_CargaMasiva Set Existencia = 0
	
	Update P Set P.Existencia = isNull(S.Existencia, 0) 
	From Registro_Pedidos_CargaMasiva P 
	Inner Join vw_ExistenciaPorClaves S (NoLock) On ( P.ClaveSSA = S.ClaveSSA )
	Where S.IdEmpresa = @idEmpresa And S.IdEstado = @IdEstado And s.IdFarmacia = @IdFarmacia


----------------------- SALIDA FINAL	
	Select * From Registro_Pedidos_CargaMasiva P (NoLock) Where P.IdClaveSSA = ''
	
----------------------- SALIDA FINAL	

End 
Go--#SQL 



