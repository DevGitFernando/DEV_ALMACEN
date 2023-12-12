---------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_PRCS_OCEN__Plantilla_CargaDePrecios' and xType = 'P' ) 
   Drop Proc spp_PRCS_OCEN__Plantilla_CargaDePrecios 
Go--#SQL 

Create Proc spp_PRCS_OCEN__Plantilla_CargaDePrecios  
( 
	@IdEstado varchar(2) = '28', @IdCliente varchar(4) = '2', @IdSubCliente varchar(4) = '11' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Set @IdEstado = right('00' +  @IdEstado, 2) 
	Set @IdCliente = right('000000' +  @IdCliente, 4) 
	Set @IdSubCliente = right('00000' +  @IdSubCliente, 4) 		



----------------------- SALIDA FINAL	
	select 
		IdEstado, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		ClaveSSA, 'Descripcion Clave' = DescripcionClave, TipoDeClave, TipoClaveDescripcion, 
		Presentacion, ContenidoPaquete_ClaveSSA,  
		Precio_Licitacion as Precio, Status, Factor,    
		ContenidoPaquete_Licitado, IdPresentacion_Licitado, Presentacion_Licitado, Dispensacion_CajasCompletas,    
		SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida     
	from vw_Claves_Precios_Asignados 
	Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente  
		and Status = 'A' 


----------------------- SALIDA FINAL	

--IdEstado	IdCliente	 IdSubCliente 	ClaveSSA	Descripcion Clave	Precio	Status	Factor	ContenidoPaquete_Licitado	IdPresentacion_Licitado	Dispensacion_CajasCompletas	SAT_UnidadDeMedida	SAT_ClaveDeProducto_Servicio


---		spp_PRCS_OCEN__Plantilla_CargaDePrecios


End 
Go--#SQL 



