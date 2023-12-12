

Set dateformat YMD 
Set nocount on 
Go--#SQL 


---- Comentariar todo el Script 
Declare 
    @sSql varchar(8000), 
    @sTabla varchar(200), 
    @sTriggerName varchar(250), 
    @iBorrar bit 

    Set @sSql = '' 
    Set @sTabla = '' 
    Set @sTriggerName = '' 
    Set @iBorrar = 1     

--    Select NombreTabla 
--    From CFGC_EnvioDetalles (NoLock) 
--    Where NombreTabla <> 'Net_Usuarios' 
--    Order By IdOrden -    
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFGC_EnvioDetalles___ProcesoBorrado' and xType = 'U' ) 
	   Drop Table CFGC_EnvioDetalles___ProcesoBorrado 

    Select IdOrden, NombreTabla, 0 as Procesado    
	Into CFGC_EnvioDetalles___ProcesoBorrado 
    From CFGC_EnvioDetalles E (NoLock)  --- CFGC_EnvioDetalles | CFGC_EnvioDetallesTrans 
    Inner Join Sysobjects S (NoLock) On ( E.NombreTabla = S.Name ) 
    Where NombreTabla <> 'Net_Usuarios' 
	Order by IdOrden 

	/* 

	Delete from INT_IME__RegistroDeVales_003_Surtidos 
	Delete from MovtosInv_Adt_Devoluciones 

	Delete from INT_MA_VentasPago 
	Delete from INT_MA_Ventas_Importes 
	Delete from CatBeneficiarios_Historico 
	Delete from CatMedicos_Direccion 
	Delete from CatMedicos_Historico 
	Delete from MovtosInv_ADT 

	*/ 


    Declare Tabla_TR Cursor For 
    Select NombreTabla   
    From CFGC_EnvioDetalles___ProcesoBorrado E (NoLock)  --- CFGC_EnvioDetalles | CFGC_EnvioDetallesTrans 
    --Inner Join Sysobjects S (NoLock) On ( E.NombreTabla = S.Name ) 
    --Where NombreTabla <> 'Net_Usuarios' 
    Order By IdOrden Desc 
	Open Tabla_TR 
	Fetch From Tabla_TR into @sTabla  
	While @@Fetch_status = 0 
		begin 
		    Set @sSql = '' -- '----------------------------------------------------------------------------------------------------' + char(13) 
		    Set @sSql = @sSql + 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + char(39) + @sTabla + char(39) + ' and xType = ' + char(39) + 'U' + char(39) + ' ) ' -- + char(10) 
            Set @sSql = replicate('-', len(@sSql) + 5 ) + char(10) + @sSql     
		    Set @sSql = @sSql + '   Delete From ' + @sTabla + ' ' -- + char(10) 
		    --Set @sSql = @sSql + 'Go--#S#QL ' 
		    
			-- Print '' 	
			If @iBorrar = 1 
			   Exec ( @sSql ) 
			Else    
			   Print @sSql 	

			Update P Set Procesado = 1 
			From CFGC_EnvioDetalles___ProcesoBorrado P 
			Where NombreTabla = @sTabla 
		    
		    Fetch next From Tabla_TR into @sTabla   
		end 
    Close Tabla_TR  
	Deallocate Tabla_TR 


