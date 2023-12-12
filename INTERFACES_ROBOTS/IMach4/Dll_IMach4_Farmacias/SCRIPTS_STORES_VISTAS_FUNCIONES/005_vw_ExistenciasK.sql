If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_IMach_ExistenciaK_EAN' And xType = 'V' )
	Drop view vw_IMach_ExistenciaK_EAN
Go

Create view vw_IMach_ExistenciaK_EAN
As 
	Select E.CodigoEAN, 
		 P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionSal as DescClaveSSA, 
		 P.Descripcion as NombreComercial, E.Cantidad as Existencia 
    From IMach_ExistenciaK E (NoLock) 
    Left Join vw_Productos_CodigoEAN P (NoLock) On ( E.CodigoEAN = P.CodigoEAN ) 

Go

----------------------- 
If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_IMach_ExistenciaK_Clave' And xType = 'V' )
	Drop view vw_IMach_ExistenciaK_Clave
Go

Create view vw_IMach_ExistenciaK_Clave
As 
	Select E.IdClaveSSA, E.ClaveSSA, E.DescClaveSSA, sum(E.Existencia) as Existencia  
    From vw_IMach_ExistenciaK_EAN E (NoLock) 
	Group by E.IdClaveSSA, E.ClaveSSA, E.DescClaveSSA 
	
Go 
