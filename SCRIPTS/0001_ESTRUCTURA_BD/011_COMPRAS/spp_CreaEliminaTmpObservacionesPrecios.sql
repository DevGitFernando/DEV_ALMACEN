



If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_CreaEliminaTmpObservacionesPrecios' And xType = 'P' )
	Drop Proc spp_CreaEliminaTmpObservacionesPrecios
Go--#SQL

Create Procedure spp_CreaEliminaTmpObservacionesPrecios 
( @iOpcion tinyint )
As
Begin
	Declare
		@sSql varchar(8000)

	Set @sSql = ''

	If @iOpcion = 1
		Begin
			If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpObservacionesPrecios' and xType = 'U' )
			Drop Table tmpObservacionesPrecios 

            Select top 0 space(4) as IdClaveSSA, space(30) as CodigoEAN, space(4) as IdProveedor, 
			cast(0 as numeric(14, 4)) as PrecioMin, cast(0 as numeric(14, 4)) as PrecioMax, 
            space(100) as ObservacionesPrecios, 0 As EsSobrePrecio 
            Into tmpObservacionesPrecios

		End
	Else
		Begin
			If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpObservacionesPrecios' and xType = 'U' )
			Drop Table tmpObservacionesPrecios
		End

End
Go--#SQL



