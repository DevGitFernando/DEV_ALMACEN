------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CB_NivelesAtencion' and xType = 'V' ) 
	Drop View vw_CB_NivelesAtencion
Go--#SQL 

Create View vw_CB_NivelesAtencion 
With Encryption 
As 
	Select	M.IdEstado, E.Nombre as Estado, M.IdCliente, IsNull(C.Nombre, '') as Cliente, 
			M.IdNivel, M.Descripcion as Nivel, M.Status
	From CFG_CB_NivelesAtencion M (NoLock) 		
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatClientes C (NoLock) On ( M.IdCliente = C.IdCliente ) -- And M.IdEstado = C.IdEstado )
Go--#SQL 

------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CB_NivelesAtencion_Miembros' and xType = 'V' ) 
	Drop View vw_CB_NivelesAtencion_Miembros
Go--#SQL 

Create View vw_CB_NivelesAtencion_Miembros 
With Encryption 
As 
	Select	M.IdEstado, M.Estado, M.IdCliente, M.Cliente, M.IdNivel, M.Nivel, M.Status as StatusNivel, 
			E.IdFarmacia, F.NombreFarmacia as Farmacia, E.Status as StatusMiembro
	From vw_CB_NivelesAtencion M (NoLock) 		
	Inner Join CFG_CB_NivelesAtencion_Miembros E (NoLock) On ( M.IdEstado = E.IdEstado And M.IdCliente = E.IdCliente And M.IdNivel = E.IdNivel ) 
	Inner Join CatFarmacias F(NoLock) On ( E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia ) 

Go--#SQL 

------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CB_CuadroBasico_Claves' and xType = 'V' ) 
	Drop View vw_CB_CuadroBasico_Claves 
Go--#SQL 

Create View vw_CB_CuadroBasico_Claves 
With Encryption 
As 
	Select	M.IdEstado, M.Estado, M.IdCliente, M.Cliente, M.IdNivel, M.Nivel, M.Status as StatusNivel, 
			E.IdClaveSSA_Sal as IdClaveSSA, 
			-- C.ClaveSSA_Base, C.ClaveSSA_Base As ClaveSSA, C.ClaveSSA As ClaveSSA_Aux, 
			C.ClaveSSA_Base, C.ClaveSSA, C.ClaveSSA As ClaveSSA_Aux, 	
			C.Descripcion as DescripcionClave, C.DescripcionCortaClave, 
			C.EsAntibiotico, C.EsControlado, 
			C.IdTipoProducto as IdTipoDeClave, 
			(case when C.IdTipoProducto = '00' Then 'OTROS' 
				else 
					case when C.IdTipoProducto = '01' Then 'MATERIAL DE CURACION' else 'MEDICAMENTO' end
			end) as TipoDeClaveDescripcion, 
			
			C.IdPresentacion, P.Descripcion as Presentacion, 
			C.ContenidoPaquete, 
			E.Status as StatusMiembro, 
			E.Status as StatusClave 
	From vw_CB_NivelesAtencion M (NoLock) 		
	Inner Join CFG_CB_CuadroBasico_Claves E (NoLock) On ( M.IdEstado = E.IdEstado And M.IdCliente = E.IdCliente And M.IdNivel = E.IdNivel ) 
	Inner Join CatClavesSSA_Sales C (NoLock) On ( E.IdClaveSSA_Sal = C.IdClaveSSA_Sal ) 
	Inner Join CatPresentaciones P (NoLock) On ( C.IdPresentacion = P.IdPresentacion )

Go--#SQL 


------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CB_CuadroBasico_Farmacias' and xType = 'V' ) 
	Drop View vw_CB_CuadroBasico_Farmacias
Go--#SQL 

Create View vw_CB_CuadroBasico_Farmacias  
With Encryption 
As 
	Select 
	    C.IdEstado, C.Estado, C.IdCliente, C.Cliente, C.IdNivel, C.Nivel, StatusNivel, 
	    F.IdFarmacia, F.NombreFarmacia as Farmacia, 
	    F.IdTipoUnidad, U.Descripcion as TipoDeUnidad, 
	    C.IdClaveSSA, C.ClaveSSA_Base, C.ClaveSSA, C.ClaveSSA_Aux, C.DescripcionClave, C.DescripcionCortaClave, 
	    C.EsAntibiotico, C.EsControlado, 
	    C.IdTipoDeClave, C.TipoDeClaveDescripcion, 
	    C.IdPresentacion, C.Presentacion, C.ContenidoPaquete, 
	    E.Status As StatusMiembro, C.StatusClave  
	From vw_CB_CuadroBasico_Claves C (NoLock) 		
	Inner Join CFG_CB_NivelesAtencion_Miembros E (NoLock) On ( C.IdEstado = E.IdEstado And C.IdCliente = E.IdCliente And C.IdNivel = E.IdNivel ) 
	Inner Join CatFarmacias F(NoLock) On ( C.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia ) 
	Inner Join CatTiposUnidades U (NoLock) On ( F.IdTipoUnidad = U.IdTipoUnidad ) 

Go--#SQL 


------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFG_CB_Sub_CuadroBasico_Claves' and xType = 'V' ) 
	Drop View vw_CFG_CB_Sub_CuadroBasico_Claves
Go--#SQL 

Create View vw_CFG_CB_Sub_CuadroBasico_Claves
With Encryption 
As 
	Select 
	    C.IdEstado, C.Estado, C.IdCliente, C.Cliente, C.IdNivel, C.Nivel, C.StatusNivel, 
	    C.IdFarmacia, C.Farmacia, C.IdTipoUnidad, C.TipoDeUnidad, 
	    E.IdPrograma, P.Programa, E.IdSubPrograma, P.SubPrograma,
	    E.IdClaveSSA, C.ClaveSSA_Base, C.ClaveSSA, C.ClaveSSA_Aux, C.DescripcionClave, C.DescripcionCortaClave, 
	    E.Cantidad, E.FechaUpdate, 
	    C.IdTipoDeClave, C.TipoDeClaveDescripcion, 
	    C.IdPresentacion, C.Presentacion, 
	    C.StatusMiembro, C.StatusClave, E.Status as StatusSubClave  
	From vw_CB_CuadroBasico_Farmacias C (NoLock) 		
	Inner Join CFG_CB_Sub_CuadroBasico_Claves E (NoLock) 
		On ( C.IdEstado = E.IdEstado And C.IdCliente = E.IdCliente And C.IdNivel = E.IdNivel and C.IdFarmacia = E.IdFarmacia 
		and C.IdClaveSSA = E.IdClaveSSA ) 
	Inner Join vw_Programas_SubProgramas P (NoLock) 
		On ( E.IdPrograma = P.IdPrograma And E.IdSubPrograma = P.IdSubPrograma ) 

Go--#SQL 

