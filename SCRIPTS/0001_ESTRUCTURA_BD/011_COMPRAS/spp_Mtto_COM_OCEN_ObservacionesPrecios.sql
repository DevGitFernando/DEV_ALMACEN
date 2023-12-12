

If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_ObservacionesPrecios' And xType = 'P' )
	Drop Proc spp_Mtto_COM_OCEN_ObservacionesPrecios
Go--#SQL


Create Procedure spp_Mtto_COM_OCEN_ObservacionesPrecios 
( @IdClaveSSA varchar(4), @CodigoEAN varchar(30), @IdProveedor varchar(8), @PrecioMin numeric(14, 4), 
	@PrecioMax numeric(14, 4), @Observaciones varchar(100), @TablaPublica varchar(50) )
As
Begin
	Declare
		@sSql varchar(8000),
		@sTabla varchar(20)

	-- Se Inicializan variables.
	Set @sSql = ''

	-- Se llena la tabla temporal
	Insert Into tmpObservacionesPrecios
	Select @IdClaveSSA, @CodigoEAN, @IdProveedor, @PrecioMin, @PrecioMax, @Observaciones, 1

End
Go--#SQL



