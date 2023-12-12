If Exists ( Select Name From SysObjects(NoLock) Where Name = 'Spp_GenerarConsultaUltimaFechaReplicacion' And xType = 'P' )
	Drop Proc Spp_GenerarConsultaUltimaFechaReplicacion 
Go--#SQL


Create Procedure dbo.Spp_GenerarConsultaUltimaFechaReplicacion 
With Encryption 	
As
Begin
	Declare
		@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacias varchar(4),
		@sValor varchar (800), @sSql varchar(2000)
	
	Select distinct IdEmpresa, IdEstado, IdFarmacia As IdFarmacias
	Into #TempFar
	From MovtosInv_Det_CodigosEAN
	
	
	Set @sValor = '('
	Declare #cursor  
	Cursor For 
		Select IdEmpresa, IdEstado, IdFarmacias From #TempFar Order BY 1
	Open #cursor
	FETCH NEXT FROM #cursor Into @IdEmpresa, @IdEstado, @IdFarmacias 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			
			Set @sValor = @sValor + Char(39) + @IdFarmacias  + Char(39)
			
			FETCH NEXT FROM #cursor Into @IdEmpresa, @IdEstado, @IdFarmacias    
			if @@FETCH_STATUS = 0 
				Set @sValor = @sValor + ','
		END	 
	Close #cursor
	Deallocate #cursor
	
	Set @sValor = @sValor + ')'

	
	Set @sSql =  'Select 1 As Correcto, IsNull(Max(FechaUpdate), dateADD(yy, -1, Getdate())) As Fecha
	From Ctl_Replicaciones_Historico (NoLock)
	Where
		( RegistrosVentasAdicional = RegionalVentasAdcional And RegistrosVentasLotes = RegionalVentasLotes	)
		And RegistrosVentasLotes > 0
		And IdEmpresa = ' + CHAR(39) + @IdEmpresa + CHAR(39)+ ' And IdEstado = ' + CHAR(39) + @IdEstado + CHAR(39)+ ' And IdFarmacia In ' + @sValor
	
	Select @sSql As Consulta
		
	   	   
End 
Go--#SQL