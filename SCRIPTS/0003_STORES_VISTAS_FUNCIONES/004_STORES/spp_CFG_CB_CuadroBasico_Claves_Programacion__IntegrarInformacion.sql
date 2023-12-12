------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFG_CB_CuadroBasico_Claves_Programacion__IntegrarInformacion' and xType = 'P' ) 
	Drop Proc spp_CFG_CB_CuadroBasico_Claves_Programacion__IntegrarInformacion
Go--#SQL 

Create Proc spp_CFG_CB_CuadroBasico_Claves_Programacion__IntegrarInformacion 
With Encryption 
As 
Begin 
--Set NoCount On 
Declare 
	@IdCliente varchar(4), 
	@IdSubCliente varchar(4), 
	@IdExcepcion int,  
	@dFecha datetime, 
	@Año int, 
	@Mes int  


	Set @Año = 0 
	Set @Mes = 0 
	Set @IdExcepcion = 0 
	Set @dFecha = getdate() 



---------------- Obtener el maximo de la excepcion 
	Select top 1 @IdCliente = IdCliente, @IdSubCliente = IdSubCliente, @Año = Año, @Mes = Mes 
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva (NoLock) 

	Select @IdExcepcion = max(IdExcepcion) + 1 
	From CFG_CB_CuadroBasico_Claves_Programacion_Excepciones 
	Where IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and Año = @Año and Mes = @Mes 
	

	Set @IdExcepcion = IsNull(@IdExcepcion, 1) 




--	Select top 2 * from CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva 


------------------------ Programacion 

	----Update C Set IdExcepcion = @IdExcepcion 
	----From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva C (NoLock)
	----Where IdExcepcion = 0 


	Insert Into CFG_CB_CuadroBasico_Claves_Programacion 
		(  
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Cantidad, FechaRegistro, Status, IdEstadoRegistra, IdFarmaciaRegistra, IdPersonalRegistra, Actualizado 
		)  
	Select  
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, sum(Cantidad) as Cantidad, @dFecha as FechaRegistro, Status, 
		IdEstadoRegistra, IdFarmaciaRegistra, IdPersonalRegistra, 0 as Actualizado 
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva C (NoLock)  
	Where Not Exists 
	(	
		Select * 
		From CFG_CB_CuadroBasico_Claves_Programacion E (NoLock) 
		Where C.IdEstado = E.IdEstado and C.IdFarmacia = E.IdFarmacia And C.IdCliente = E.IdCliente and C.IdSubCliente = E.IdSubCliente 
			  and C.IdClaveSSA = E.IdClaveSSA and C.Año = E.Año and C.Mes = E.Mes  
	) 
	Group by 
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Status, IdEstadoRegistra, IdFarmaciaRegistra, IdPersonalRegistra  

---	sp_listacolumnas CFG_CB_CuadroBasico_Claves_Programacion_Excepciones



------------------------ Ampliaciones 
	Insert Into CFG_CB_CuadroBasico_Claves_Programacion_Excepciones 
	( 
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, IdExcepcion, FechaRegistro, Cantidad, Status, IdEstadoRegistra, IdFarmaciaRegistra, IdPersonalRegistra, Actualizado
	) 
	Select 
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, IdExcepcion, @dFecha as FechaRegistro, 
		sum(Cantidad) as Cantidad, Status, IdEstadoRegistra, IdFarmaciaRegistra, IdPersonalRegistra, 0 as Actualizado
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva C (NoLock)  
	Where IdExcepcion > 0 and 
		Not Exists 
		(	
			Select * 
			From CFG_CB_CuadroBasico_Claves_Programacion_Excepciones E (NoLock) 
			Where C.IdEstado = E.IdEstado and C.IdFarmacia = E.IdFarmacia And C.IdCliente = E.IdCliente and C.IdSubCliente = E.IdSubCliente 
				  and C.IdClaveSSA = E.IdClaveSSA and C.Año = E.Año and C.Mes = E.Mes and C.IdExcepcion = E.IdExcepcion 
		) 
	Group by 
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, IdExcepcion, Status, IdEstadoRegistra, IdFarmaciaRegistra, IdPersonalRegistra  

End 
Go--#SQL 


