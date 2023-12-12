

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_Excepciones_Precios' and xType = 'P')
    Drop Proc spp_Mtto_COM_Excepciones_Precios
Go--#SQL

--		Exec spp_Mtto_COM_Excepciones_Precios '001', '21', '0001', '000001', '0031', '0001', '##701A042DA85F_20120214_102450'
  
Create Proc spp_Mtto_COM_Excepciones_Precios 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FolioOrden varchar(8),
	@IdPersonal varchar(4), @IdProveedor varchar(4), @TablaPublica varchar(100), @CodigoEAN varchar(30)
)
With Encryption 
As
Begin
Set NoCount On

Declare @sSql varchar(7500)
	
	Set @sSql = ''	
	
--		Select * From COM_Excepciones_Precios (Nolock)
		   
		Set @sSql = ' Insert Into COM_Excepciones_Precios ' + 
		' Select ' + Char(39) + @IdEmpresa + Char(39) + ' As IdEmpresa, ' 
		+ Char(39) + @IdEstado + Char(39) + ' As IdEstado, ' + Char(39) + @IdFarmacia + Char(39) + ' As IdFarmacia, ' 
		+ Char(39) +  @FolioOrden + Char(39) + 'As FolioOrden, ' +
		' IdProveedor, IdClaveSSA, CodigoEAN, ' + Char(39) + @IdPersonal + Char(39) + 'As IdPersonal, ' + 
		' GetDate(), Precio, PrecioMin, PrecioMax, Cant_A_Pedir, ObservacionesPrecios ' +
		' From ' + @TablaPublica + ' (Nolock) Where IdProveedor = ' + Char(39) + @IdProveedor + Char(39) +
		' And CodigoEAN = ' + Char(39) + @CodigoEAN + Char(39) + ' And EsSobrePrecio = 1 And Cant_A_Pedir > 0 '			
	    Exec (@sSql) 
--		Print (@sSql)    

End
Go--#SQL	
