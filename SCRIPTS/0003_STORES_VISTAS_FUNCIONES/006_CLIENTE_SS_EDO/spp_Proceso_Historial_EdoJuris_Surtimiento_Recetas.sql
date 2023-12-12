	    
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Proceso_Historial_EdoJuris_Surtimiento_Recetas' and xType = 'P') 
    Drop Proc spp_Proceso_Historial_EdoJuris_Surtimiento_Recetas 
Go--#SQL 
  
--  Exec spp_Proceso_Historial_EdoJuris_Surtimiento_Recetas '21', '006', '0002', '2011-10-01', '2012-01-01'
  
Create Proc spp_Proceso_Historial_EdoJuris_Surtimiento_Recetas 
(   
    @IdEstado varchar(2) = '09', @FechaInicial varchar(10) = '2011-10-01', @FechaFinal varchar(10) = '2015-01-01'
) 
With Encryption 
As 
Begin 
	Declare @FechaRegistro varchar(10), 
			@IdJurisdiccion varchar(3),
			@IdCliente varchar(4)

	Set NoCount On 
	Set DateFormat YMD 
	Set @FechaRegistro = @FechaInicial 
	Set @IdJurisdiccion = '*'
	Set @IdCliente = '0023'

	Declare #cSurtimiento Cursor For Select @FechaRegistro 
	Open #cSurtimiento Fetch #cSurtimiento Into @FechaRegistro
		While (@FechaRegistro <= @FechaFinal ) 
			Begin

				If @FechaRegistro <= @FechaFinal
				  Begin
					Exec spp_Mtto_Historial_EdoJuris_Surtimiento_Recetas @IdEstado, @IdJurisdiccion, @IdCliente, @FechaRegistro
					Set @FechaRegistro = Convert( varchar(10), ( Select Cast( @FechaRegistro as datetime )  + 1 ), 120 )
					--Select @FechaRegistro
				  End
				Fetch #cSurtimiento Into @FechaRegistro
			End		
	Close #cSurtimiento
	DeAllocate #cSurtimiento

End
Go--#SQL 


