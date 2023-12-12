------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_GenerarTomaDeExistencias' and xType = 'P') 
    Drop Proc spp_INT_ND_GenerarTomaDeExistencias 
Go--#SQL 
  
--  ExCB spp_INT_ND_GenerarTomaDeExistencias '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_GenerarTomaDeExistencias 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @CodigoCliente varchar(20) = '2181002', 
    @IdFarmacia varchar(4) = '11', @FechaDeProceso varchar(10) = '2014-10-31'  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  

Declare 
	@Folio varchar(8), 
	@sFCBha varchar(8), 
	@sConsCButivo varchar(3), 
	@sMensaje varchar(1000), 
	@IdSubFarmacia varchar(2)  
	
	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000' + @IdFarmacia, 4) 
	Set @IdSubFarmacia = '05' 

---------------------- Farmacias a procesar   
	Select * 
	Into #tmpClientes 
	From vw_INT_ND_Clientes F 
	Where F.IdEstado = @IdEstado and CodigoCliente = @CodigoCliente 

	Select top 1 @IdFarmacia = IdFarmacia From #tmpClientes 
---------------------- Farmacias a procesar   


---------------------- Base de existencias 
	Select 
		IdEmpresa, Empresa, 
		IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
		Poliza as FolioDeToma, 	
		0 as Relacionado, 
		IdClaveSSA, ClaveSSA_Base, ClaveSSA, ClaveSSA as ClaveSSA_Aux, 
		ClaveSSA as ClaveSSA_ND, 
		DescripcionClave, 
		space(6000) as DescripcionClave_ND,
		DescripcionClave as DescripcionComercial, 
		-- TipoDeClave, TipoDeClaveDescripcion, 
		IdProducto, CodigoEAN, DescProducto as DescripcionProducto, ContenidoPaquete, 
		ClaveLote, 

		cast(0 as int) as PiezasTotales, 
		floor((ExistenciaSistema / (ContenidoPaquete*1.0))) as ExistenciaTeorica,  
		floor((ExistenciaFisica / (ContenidoPaquete*1.0))) as ExistenciaFisica,  		
		(0 - (floor((0 / (ContenidoPaquete*1.0))) * ContenidoPaquete)) as PiezasSueltas,  
				
		NombrePersonal as UsuarioToma, NombrePersonal as UsuarioAutoriza, 
		space(100) as MotivoDiferencias,  
		
		FechaCad as FechaCaducidad 
	Into #tmpExistencia 
	From vw_AjustesInv_Det_Lotes V (NoLock) 
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia and V.IdSubFarmacia = '05'
		and convert(varchar(10), V.FechaRegistro, 120) = @FechaDeProceso   
		and Not Exists 
			( 
				Select * 
				From INT_ND_SubFarmaciasConsigna C (NoLock) 
				Where V.IdEstado = C.IdEstado and V.IdSubFarmacia = C.IdSubFarmacia  		
			) 		
			
	------Where F.IdEmpresa = @IdEmpresa 
	------	and F.IdSubFarmacia = '05'  
	------	and exists (	Select *  From #tmpClientes C (NoLock) 
	------					Where  F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia  ) 

---	select * from #tmpExistencia 

	Update E Set MotivoDiferencias = 'SIN DIFERENCIAS'
	From #tmpExistencia E (NoLock) 
	Where MotivoDiferencias = '' and ExistenciaTeorica = ExistenciaFisica 

	Update E Set MotivoDiferencias = 'DIFERENCIAS NEGATIVAS'
	From #tmpExistencia E (NoLock) 
	Where MotivoDiferencias = '' and ExistenciaTeorica > ExistenciaFisica 

	Update E Set MotivoDiferencias = 'DIFERENCIAS POSITIVAS'
	From #tmpExistencia E (NoLock) 
	Where MotivoDiferencias = '' and ExistenciaTeorica < ExistenciaFisica 

						
---------------------- Base de existencias 



	
--------------------------------------------------------	Agregar descripciones NADRO  	
	Update E Set ClaveSSA_ND = '', DescripcionClave = '', DescripcionComercial = '' 
	From #tmpExistencia E 	

------------ AJUSTES 		
	Update E Set ClaveSSA_ND = H.ClaveSSA_ND 
	From #tmpExistencia E 	
	Inner Join INT_ND_CFG_ClavesSSA	 H (NoLock) 
		On ( E.ClaveSSA = H.ClaveSSA or replace(E.ClaveSSA_Base, '.', '') = H.ClaveSSA_ND )  
	
	Update E Set CodigoEAN = H.CodigoEAN_ND  
	From #tmpExistencia E 
	Inner Join INT_ND_CFG_CodigosEAN H (NoLock) On ( E.CodigoEAN = H.CodigoEAN )   		
	
	Update E Set ClaveSSA_ND = H.ClaveSSA_ND 
	From #tmpExistencia E 
	Inner Join INT_ND_Productos H (NoLock) On ( E.CodigoEAN = H.CodigoEAN )   	
------------ AJUSTES 		

	
	Update E Set ClaveSSA_ND = H.ClaveSSA_ND, DescripcionClave_ND = H.Descripcion_Mascara 
	From #tmpExistencia E 
	Inner Join INT_ND_CFG_CB_CuadrosBasicos H (NoLock) 
		On ( E.ClaveSSA = H.ClaveSSA or replace(E.ClaveSSA_Base, '.', '') = replace(H.ClaveSSA_ND, '.', '') )   

	------------------- Buscar en la tabla de Relación de Claves 
	Update E Set ClaveSSA_ND = C.ClaveSSA_ND, Relacionado = 1  
	From #tmpExistencia E 
	Inner Join INT_ND_CFG_ClavesSSA C On ( E.IdClaveSSA = C.IdClaveSSA ) 
	Where E.ClaveSSA_ND = '' 

	Update E Set ClaveSSA_ND = H.ClaveSSA_ND, DescripcionClave_ND = H.Descripcion_Mascara 
	From #tmpExistencia E 
	Inner Join INT_ND_CFG_CB_CuadrosBasicos H (NoLock) On ( E.ClaveSSA_ND = H.ClaveSSA_ND ) 
	Where E.DescripcionClave_ND = '' 
	------------------- Buscar en la table de Relación de Claves 
	
	
	------------------- Buscar en la tabla de Productos la ClaveSSA 
	Update E Set ClaveSSA_ND = H.ClaveSSA_ND, Relacionado = 2  
	From #tmpExistencia E 
	Inner Join INT_ND_Productos H (NoLock) On ( E.CodigoEAN = H.CodigoEAN )   
	Where E.DescripcionClave_ND = '' 
	
	Update E Set ClaveSSA_ND = H.ClaveSSA_ND, DescripcionClave_ND = H.Descripcion_Mascara 
	From #tmpExistencia E 
	Inner Join INT_ND_CFG_CB_CuadrosBasicos H (NoLock) On ( E.ClaveSSA_ND = H.ClaveSSA_ND ) 
	Where E.DescripcionClave_ND = '' 	
	------------------- Buscar en la tabla de Productos la ClaveSSA 
	
		

	Update E Set DescripcionComercial = H.Descripcion 
	From #tmpExistencia E 
	Inner Join INT_ND_Productos H (NoLock) On ( E.CodigoEAN = H.CodigoEAN )   
--------------------------------------------------------	Agregar descripciones NADRO  	


	
--	Select * From #tmpExistencia 

---		spp_INT_ND_GenerarTomaDeExistencias  

---------------------- Intermedio 				


	
------------------------------------- TABLA FINAL 	
------------------------------------- TABLA FINAL 	


	
	
	
------------------------- Salida Final 		
 
	Select 
		@CodigoCliente as CodigoCliente, '01' as Modulo, '02' as TipoDeToma, '03' as ConfiguracionCiclica, 
		FolioDeToma,  
		F.Relacionado, 
		F.ClaveSSA_Base, F.ClaveSSA, 
		F.ClaveSSA_ND, 
		F.DescripcionClave_ND as DescripcionClave, 
		F.DescripcionComercial, 
		F.CodigoEAN, 
		cast(F.ExistenciaTeorica as int) as ExistenciaTeorica, 
		cast(F.ExistenciaFisica as int) as ExistenciaFisica, 		
		F.ClaveLote, 
		
		replace(convert(varchar(10), F.FechaCaducidad, 120), '-', '') as Caducidad,  		
		UsuarioToma, UsuarioAutoriza, MotivoDiferencias,
		
		replace(convert(varchar(10), @FechaDeProceso, 120), '-', '') as FechaGeneracion 		
	From #tmpExistencia F (NoLock) -- On ( CB.IdEstado = F.IdEstado and CB.IdFarmacia = F.IdFarmacia and CB.ClaveSSA = F.ClaveSSA )  
	--- Inner Join vw_INT_ND_Clientes C (NoLock) On ( CB.IdEstado = C.IdEstado and CB.IdFarmacia = C.IdFarmacia ) 
	-- Where Relacionado <> 0  
	Order by F.IdEstado, F.IdFarmacia, F.DescripcionClave 	
 

------------------------- Salida Final 		
	
	
---		spp_INT_ND_GenerarTomaDeExistencias  	

	
End  
Go--#SQL 

