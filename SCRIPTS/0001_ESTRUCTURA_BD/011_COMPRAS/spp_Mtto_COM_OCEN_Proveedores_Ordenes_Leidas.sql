---------------------------------------------------------------------
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_COM_OCEN_Proveedores_Ordenes_Leidas' and xType = 'P' )
   Drop Proc spp_Mtto_COM_OCEN_Proveedores_Ordenes_Leidas 
Go--#SQL

Create Proc spp_Mtto_COM_OCEN_Proveedores_Ordenes_Leidas 
( 
	@IdProveedor varchar(4) = '', @IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @FolioOrden varchar(8) = ''
) 
As 
Begin 
	If Not Exists 
	( 
		Select * From COM_OCEN_Proveedores_Ordenes_Leidas (NoLock) 
		Where IdProveedor = @IdProveedor and IdEmpresa = @IdEmpresa and 
			  IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioOrden = @FolioOrden 
	) 	
	Begin 
		Insert Into COM_OCEN_Proveedores_Ordenes_Leidas ( IdProveedor, IdEmpresa, IdEstado, IdFarmacia, FolioOrden ) 
		Select @IdProveedor, @IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrden 
	End 
End 
Go--#SQL 

---	sp_listacolumnas COM_OCEN_Proveedores_Ordenes_Leidas
