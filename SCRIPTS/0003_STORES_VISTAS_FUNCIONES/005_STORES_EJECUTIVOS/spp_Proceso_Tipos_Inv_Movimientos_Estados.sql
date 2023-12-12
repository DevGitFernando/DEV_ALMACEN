


If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Proceso_Tipos_Inv_Movimientos_Estados' and xType = 'P') 
    Drop Proc spp_Proceso_Tipos_Inv_Movimientos_Estados 
Go--#SQL 
  
--  Exec spp_Proceso_Tipos_Inv_Movimientos_Estados '21', '2011-10-01', '2012-01-01'
  
Create Proc spp_Proceso_Tipos_Inv_Movimientos_Estados 
(   
    @IdEstado varchar(2) = '21', @FechaInicial varchar(10) = '2011-10-01', @FechaFinal varchar(10) = '2012-01-01'
) 
With Encryption 
As 
Begin 
	Declare @FechaRegistro varchar(10)

	Set NoCount On 
	Set DateFormat YMD 
	Set @FechaRegistro = @FechaInicial
	Set @FechaFinal = ( convert(varchar(10), getdate() - 30, 120 ) ) 
	

	Declare #cTipoMovtos Cursor For Select @FechaRegistro 
	Open #cTipoMovtos Fetch #cTipoMovtos Into @FechaRegistro
		While (@FechaRegistro <= @FechaFinal ) 
			Begin
				If @FechaRegistro <= @FechaFinal
				  Begin
					Exec spp_Rpt_TipoMovimientos_Totales_Estados @IdEstado, @FechaRegistro --, @FechaFinal
					Set @FechaRegistro = Convert( varchar(10), ( Select Cast( @FechaRegistro as datetime )  + 30 ), 120 )
					--Select @FechaRegistro
				  End
				Fetch #cTipoMovtos Into @FechaRegistro
			End		
	Close #cTipoMovtos
	DeAllocate #cTipoMovtos

--	Exec spp_Rpt_TipoMovimientos_Totales_Estados @IdEstado, @FechaInicial, @FechaFinal

End
Go--#SQL 
