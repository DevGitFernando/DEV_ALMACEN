------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_FACT_Rpt_ClavesFinanciamiento' and xType = 'P')
    Drop Proc spp_FACT_Rpt_ClavesFinanciamiento 
Go--#SQL
  
Create Proc spp_FACT_Rpt_ClavesFinanciamiento 
( 
	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '0001', @iOpcion tinyint = 1 
)
With Encryption 
As
Begin 
Set NoCount On 
Declare 
	@sMensaje varchar(1000)
	
	Set @sMensaje = '' 

-------------------------------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------------------------------- 
---	Insumos 		
	---  Opcion  = 1 .-  te regresa la lista de las claves sin precio licitacion o no estan asignadas al estado.	
	---  Opcion	 = 2 .-  te muestra el listado general de las claves de la fuente de financiamiento.
	If @iOpcion = 1 
		Begin 		
			Select top 0 'Clave SSA' = F.ClaveSSA, 'Descripción Clave' = F.DescripcionClaveSSA
			From vw_FACT_FuentesDeFinanciamiento_Claves F (NoLock)    
			Where F.IdFuenteFinanciamiento = @IdFuenteFinanciamiento And F.IdFinanciamiento = @IdFinanciamiento
			and F.ClaveSSA Not In ( Select C.ClaveSSA From vw_Claves_Precios_Asignados C (Nolock)
				Where  F.IdEstado = C.IdEstado and F.IdCliente = C.IdCliente and F.IdSubCliente = C.IdSubCliente OR (C.Precio = 0) )
		End
		
	If @iOpcion = 2 
		Begin 		
			Select 'Clave SSA' = F.ClaveSSA, 'Descripción Clave' = F.DescripcionClaveSSA
			From vw_FACT_FuentesDeFinanciamiento_Claves F (NoLock)    
			Where F.IdFuenteFinanciamiento = @IdFuenteFinanciamiento And F.IdFinanciamiento = @IdFinanciamiento			
		End
	
-------------------------------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------------------------------- 
---	Administración 	
	If @iOpcion = 3 
		Begin 		
			Select top 0 'Clave SSA' = F.ClaveSSA, 'Descripción Clave' = F.DescripcionClaveSSA
			From vw_FACT_FuentesDeFinanciamiento_Claves F (NoLock)    
			Where F.IdFuenteFinanciamiento = @IdFuenteFinanciamiento And F.IdFinanciamiento = @IdFinanciamiento
			and F.ClaveSSA Not In ( Select C.ClaveSSA From vw_Claves_Precios_Asignados C (Nolock)
				Where  F.IdEstado = C.IdEstado and F.IdCliente = C.IdCliente and F.IdSubCliente = C.IdSubCliente OR (C.Precio = 0) )
		End
		
			
End 
Go--#SQL 
