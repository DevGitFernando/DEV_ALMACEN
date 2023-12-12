	    
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Proceso_Historial_EdoJuris_NoCauses_Surtimiento_Recetas' and xType = 'P') 
    Drop Proc spp_Proceso_Historial_EdoJuris_NoCauses_Surtimiento_Recetas 
Go--#SQL 
  
--  Exec spp_Proceso_Historial_EdoJuris_NoCauses_Surtimiento_Recetas '21', '2011-10-01', '2012-01-01'
  
Create Proc spp_Proceso_Historial_EdoJuris_NoCauses_Surtimiento_Recetas 
(   
    @IdEstado varchar(2) = '21', @FechaInicial varchar(10) = '2011-10-01', @FechaFinal varchar(10) = '2011-10-10', 
    @Porcentaje numeric(14,4) = 20 
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
	Set @IdCliente = '0002'

	Declare #cSurtimiento Cursor For Select @FechaRegistro 
	Open #cSurtimiento Fetch #cSurtimiento Into @FechaRegistro
		While (@FechaRegistro <= @FechaFinal ) 
			Begin

				If @FechaRegistro <= @FechaFinal
				  Begin
					Exec spp_Mtto_Historial_EdoJuris_NoCauses_Surtimiento_Recetas @IdEstado, @IdJurisdiccion, @IdCliente, @FechaRegistro, @Porcentaje 
					Set @FechaRegistro = Convert( varchar(10), ( Select Cast( @FechaRegistro as datetime )  + 1 ), 120 )
					--Select @FechaRegistro
				  End
				Fetch #cSurtimiento Into @FechaRegistro
			End		
	Close #cSurtimiento
	DeAllocate #cSurtimiento

End
Go--#SQL 


