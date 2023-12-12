If Exists (Select Name From SysObjects(NoLock) Where Name = 'vw_COM_OCEN_ListaDePrecios_ClavesSSA' And xType = 'V')
	Drop View vw_COM_OCEN_ListaDePrecios_ClavesSSA
Go--#SQL

Create View vw_COM_OCEN_ListaDePrecios_ClavesSSA
With Encryption
As 

	Select L.IdProveedor, P.Nombre, L.IdClaveSSA, V.ClaveSSA_Base, V.DescripcionSal, 
		   StatusPrecioAux = (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End), 
		   L.Precio, L.Descuento, L.TasaIva, L.Iva, L.PrecioUnitario, L.FechaRegistro, 
		   L.FechaFinVigencia

	From   COM_OCEN_ListaDePrecios_Claves L(NoLock)

	Inner Join vw_Proveedores P(NoLock) On (P.IdProveedor = L.IdProveedor)
	Inner Join vw_ClavesSSA_Sales V(NoLock) On (V.IdClaveSSA_Sal = L.IdClaveSSA)

Go--#SQL