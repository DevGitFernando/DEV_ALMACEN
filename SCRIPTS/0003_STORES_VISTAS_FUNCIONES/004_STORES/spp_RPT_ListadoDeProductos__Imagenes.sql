--------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_RPT_ListadoDeProductos__Imagenes' and xType = 'P')
    Drop Proc spp_RPT_ListadoDeProductos__Imagenes
Go--#SQL	  
  
Create Proc spp_RPT_ListadoDeProductos__Imagenes  
( 	
	@IdEmpresa varchar(3) = '001'  
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sFiltro varchar(max),  
	@sFiltro_Ventas varchar(max),  	
	@sFiltro_Like varchar(max),  
	@sFiltro_Existencias varchar(max),  	
	@sTop varchar(100),  	
	@sOrder varchar(100), 
	@sOrden_01 varchar(max),   
	@sOrden_02 varchar(max)
	
	
	Select top 20 
		identity(int, 1, 1) as Orden, 
		IP.IdProducto, IP.CodigoEAN, IP.Consecutivo, IP.NombreImagen, IP.FechaRegistro as FechaRegistroImagen,  
		P.ClaveSSA, P.DescripcionClave, P.Descripcion as NombreComercial, 
		P.Laboratorio, CAST(N'' AS XML).value('xs:base64Binary(sql:column("IP.Imagen"))', 'VARBINARY(MAX)') as ImagenProducto 
	Into #tmp_ImagenesProductos 
	From CatProductos_Imagenes IP (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( IP.IdProducto = P.IdProducto and IP.CodigoEAN = P.CodigoEAN ) 	
	Where IP.Status = 'A' 
	
	----Select *, 
	----	-- CAST(N'' AS XML).value('xs:base64Binary(sql:variable("@Base64"))', 'VARBINARY(MAX)')  
	----	CAST(N'' AS XML).value('xs:base64Binary(sql:column("IP.Imagen"))', 'VARBINARY(MAX)')  		
	----	---cast(N'' as xml).value('xs:base64Binary(xs:hexBinary(sql:column("Imagen")))', 'varchar(max)') as sql_handle_base64		
	----From CatProductos_Imagenes 
	
	
	Select *
	From #tmp_ImagenesProductos 
	Order by Orden  

End
Go--#SQL

