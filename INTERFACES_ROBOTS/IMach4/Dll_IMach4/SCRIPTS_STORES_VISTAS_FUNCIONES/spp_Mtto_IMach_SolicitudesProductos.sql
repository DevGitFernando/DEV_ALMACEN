-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_IMach_Solicitudes' and xType = 'P' )
   Drop Proc spp_Mtto_IMach_Solicitudes 
Go--#SQL   

-- spp_Mtto_IMach_Solicitudes '1'

Create Proc spp_Mtto_IMach_Solicitudes 
( 
	@FolioSolicitud varchar(8) = '*', @IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@FolioVenta varchar(8) = '' 
)
With Encryption 
As 
Begin 
Set NoCount On 

	If @FolioSolicitud = '*' 
	   Select @FolioSolicitud = cast(max(FolioSolicitud) + 1 as varchar) From IMach_Solicitudes (NoLock) 
	
	Set @FolioSolicitud = IsNull(@FolioSolicitud, '1') 
	Set @FolioSolicitud = right(replicate('0', 8) + @FolioSolicitud, 8)  
	
	
	If Not Exists ( Select FolioSolicitud From IMach_Solicitudes (NoLock)  Where FolioSolicitud = @FolioSolicitud ) 
	   Begin 
	      Insert Into IMach_Solicitudes ( FolioSolicitud, IdEmpresa, IdEstado, IdFarmacia, FolioVenta, Status, Actualizado ) 
	      Select @FolioSolicitud, @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, 'A', 0  
	   End 	
	Else 
	   Begin 
	      Update I Set FolioVenta = @FolioVenta, Status = 'T', Actualizado = 0 
	      From IMach_Solicitudes I (NoLock)  
	      Where FolioSolicitud = @FolioSolicitud 
	   End 
	
	
	--- Regresar el valor generado 
	Select @FolioSolicitud as FolioSolicitud 
End 
Go--#SQL      

------	FolioSolicitud varchar(8) Not Null, 
------	IdEmpresa varchar(3) Not Null, 
------	IdEstado varchar(2) Not Null, 
------	IdFarmacia varchar(4) Not Null, 
------	FolioVenta varchar(8) Not Null Default '', 


----	IdCliente varchar(4) Not Null, 		
----	FolioSolicitud varchar(8) Not Null, 
----	Consecutivo bigint identity(1,1), 
----	
----	IdTerminal varchar(3) Not Null, 
----	PuertoDeSalida varchar(3) Not Null, 		
----	
----	IdProducto varchar(8) Not Null, 
----	CodigoEAN varchar(30) Not Null, 
----	CantidadSolicitada int Not Null Default 0, 
----	CantidadSurtida int Not Null Default 0, 
----	
----	StatusIMach tinyint Not Null Default -1, 
	

-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_IMach_SolicitudesProductos' and xType = 'P' )
   Drop Proc spp_Mtto_IMach_SolicitudesProductos 
Go--#SQL   

Create Proc spp_Mtto_IMach_SolicitudesProductos 
(
	@IdCliente varchar(4), @FolioSolicitud varchar(8), @Consecutivo bigint, 
	@IdTerminal varchar(3), @PuertoDeSalida varchar(3), @IdProducto varchar(8), @CodigoEAN varchar(30), 
	@CantidadSolicitada int, @StatusIMach int   
)
With Encryption 
As 
Begin 
Set NoCount On 

	If IsNull(@Consecutivo, 0) = 0 
	   Begin 
	      Select @Consecutivo = IsNull(max(Consecutivo), 0) + 1 From IMach_SolicitudesProductos (NoLock) 
	      Insert Into IMach_SolicitudesProductos 
			   ( IdCliente, FolioSolicitud, Consecutivo, IdTerminal, PuertoDeSalida, IdProducto, CodigoEAN, CantidadSolicitada, CantidadSurtida ) 
	      Select @IdCliente, @FolioSolicitud, @Consecutivo, @IdTerminal, @PuertoDeSalida, @IdProducto, @CodigoEAN, @CantidadSolicitada, 0  
	   End 
----	Else 
----	   Begin 
----	      Update I Set StatusIMach = @StatusIMach, FechaSurtido = getdate(), 
----			   CantidadSurtida = (case when @StatusIMach = 4 then CantidadSolicitada else CantidadSurtida end)
----	      From IMach_SolicitudesProductos I (NoLock) 
----	      Where Consecutivo = @Consecutivo 
----	   End 

	Select @Consecutivo as NumeroDeOrden  

End 
Go--#SQL      


-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_IMach_SolicitudesProductosRespuesta' and xType = 'P' )
   Drop Proc spp_Mtto_IMach_SolicitudesProductosRespuesta 
Go--#SQL    

Create Proc spp_Mtto_IMach_SolicitudesProductosRespuesta ( @Consecutivo bigint, @CodigoEAN varchar(30), @StatusIMach int, @Cantidad int = 0) 
With Encryption 
As 
Begin 
Set NoCount On 
	
	
	Update I Set StatusIMach = @StatusIMach, FechaSurtido = getdate(), 
		CantidadSurtida = (case when @StatusIMach = 4 then @Cantidad else 0 end)
	From IMach_SolicitudesProductos I (NoLock) 
	Where Consecutivo = @Consecutivo and CodigoEAN = @CodigoEAN 

	
	If @StatusIMach = 4 
	Begin 
		Exec spp_Mtto_IMach_StockProductos @CodigoEAN, @Cantidad, 'A', 2, 'a'  
	End 
	
End 
Go--#SQL   
 
--    select * from IMach_SolicitudesProductos (nolock) 


--    select * from IMach_Solicitudes 
 