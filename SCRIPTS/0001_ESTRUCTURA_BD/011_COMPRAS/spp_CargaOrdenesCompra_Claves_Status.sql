
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CargaOrdenesCompra_Claves_Status' and xType = 'P' ) 
  Drop Proc spp_CargaOrdenesCompra_Claves_Status
Go--#SQL   

Create Proc spp_CargaOrdenesCompra_Claves_Status
With Encryption 
As 
Begin 
Set NoCount On
Set dateFormat YMD

		Declare @IdStatus Varchar(2),
				@Observaciones Varchar(500),
				@sSql varchar(5000),
				@IdEmpresa Varchar(3),
				@IdEstado Varchar(2),
				@IdFarmacia Varchar(4),
				@FolioOrden Varchar(8),
				@IdPersonal Varchar(4)
		
		Select @IdStatus = MIN(IdStatus) From Cat_StatusDeOrdenesDeCompras Where Status = 'A'
		Set @Observaciones = ''
			
		Declare query_Cursor
		Cursor For
			Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioOrden, E.IdPersonal
			From COM_OCEN_OrdenesCompra_Claves_Enc E (NoLock)
			Left Join COM_OCEN_OrdenesCompra_Claves_Status S(NoLock)
				On (E.IdEmpresa = S.IdEmpresa And E.IdEstado = S.IdEstado And E.IdFarmacia = S.IdFarmacia And E.FolioOrden = S.FolioOrden)
			Where S.FolioOrden Is Null And E.status = 'OC'
		open Query_cursor
		Fetch From Query_cursor into @IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrden, @IdPersonal
		
			while @@Fetch_status = 0 
				begin
			
			Set @sSql = 'Exec spp_OrdenesCompra_Claves_Status '			
					Select @sSql = @sSql + Char(39 ) + @IdEmpresa + Char(39 ) + ', ' + Char(39 ) +@IdEstado + Char(39 ) + ', ' + Char(39 ) + 
					@IdFarmacia + Char(39 ) + ', ' + Char(39 ) + @FolioOrden + Char(39 ) + ', ' + Char(39 ) + @IdPersonal + Char(39 ) + ', ' +
					Char(39 ) + @IdStatus + Char(39) + ', ' + Char(39)+ @Observaciones + Char(39)
			Exec(@sSql)
			
			Fetch From Query_cursor into @IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrden, @IdPersonal
			End 
		close Query_cursor              
		deallocate Query_cursor	

End
Go--#SQL