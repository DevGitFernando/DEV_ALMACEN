If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGS_Bitacora_Movimientos' and xType = 'P' ) 
	Drop Proc spp_CFGS_Bitacora_Movimientos
Go--#SQL  

Create Proc spp_CFGS_Bitacora_Movimientos ( @IdTipoArchivo varchar(2)='', @IdArchivo varchar(50)='', @Motivo varchar(100)='', @Descripcion varchar(300)='' ) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sFecha varchar(10), 
		@sFolio varchar(15), 
		@dFecha datetime  

	Set @dFecha = getdate() + 10 
	Select @sFolio = convert(varchar(10), @dFecha, 112)
	Select @sFecha = max(right(IdMovimiento, 4)) + 1 From CFGS_Bitacora_Movimientos (NoLock) 
		   Where convert(varchar(10), FechaMovimiento, 120) = convert(varchar(10), @dFecha, 120) 

	Set @sFecha = IsNull(@sFecha, '1') 
	Set @sFolio = @sFolio + '-' + right(replicate('0', 5) + @sFecha, 4)
	
	-- Agregar el registro a la Bitacora 
	Insert Into CFGS_Bitacora_Movimientos ( IdMovimiento, FechaMovimiento ) values ( @sFolio, @dFecha ) 
    Select @sFolio as Folio, @dFecha as Fecha 
	
End 
Go--#SQL  

--	Select getdate(), convert(varchar(10), getdate(), 112)  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGC_Bitacora_Movimientos' and xType = 'P' ) 
	Drop Proc spp_CFGC_Bitacora_Movimientos
Go--#SQL  

Create Proc spp_CFGC_Bitacora_Movimientos ( @IdTipoArchivo varchar(2)='', @IdArchivo varchar(50)='', @Motivo varchar(100)='', @Descripcion varchar(300)='' ) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sFecha varchar(10), 
		@sFolio varchar(15), 
		@dFecha datetime  

	Set @dFecha = getdate() + 10 
	Select @sFolio = convert(varchar(10), @dFecha, 112)
	Select @sFecha = max(right(IdMovimiento, 4)) + 1 From CFGC_Bitacora_Movimientos (NoLock) 
		   Where convert(varchar(10), FechaMovimiento, 120) = convert(varchar(10), @dFecha, 120) 

	Set @sFecha = IsNull(@sFecha, '1') 
	Set @sFolio = @sFolio + '-' + right(replicate('0', 5) + @sFecha, 4)
	
	-- Agregar el registro a la Bitacora 
	Insert Into CFGC_Bitacora_Movimientos ( IdMovimiento, FechaMovimiento ) values ( @sFolio, @dFecha ) 
    Select @sFolio as Folio, @dFecha as Fecha 
	
End 
Go--#SQL  
	
