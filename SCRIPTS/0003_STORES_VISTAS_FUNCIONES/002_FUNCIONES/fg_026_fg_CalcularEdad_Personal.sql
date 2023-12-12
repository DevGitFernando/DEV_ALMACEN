	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_CalcularEdad_Personal' and xType = 'FN' ) 
   Drop Function fg_CalcularEdad_Personal 
Go--#SQL	 	

-- Select dbo.fg_CalcularEdad_Personal('1979-12-31') 

Create Function dbo.fg_CalcularEdad_Personal(@FechaNacimiento varchar(10) ) 
returns varchar(20) 
With Encryption 
As 
Begin 
Declare @FechaActual varchar(10), 
		@iAñoNacimiento int, 
		@iMesNacimiento int, 
		@iDiaNacimiento int,
		@iAñoActual int, 
		@iMesActual int, 
		@iDiaActual int,
		@iAños int

	--Set DateFormat YMD

	-- Se obtiene la Fecha Actual
	Set @FechaActual = Convert( varchar(10), Getdate(), 120 ) 
	
	-- Se obtiene el Año, Mes y Dia de nacimiento de la persona.
	Set @iAñoNacimiento = Year( Convert( Datetime, @FechaNacimiento, 120 ) )
	Set @iMesNacimiento = Month(Convert( Datetime, @FechaNacimiento, 120 ) )
	Set @iDiaNacimiento = Day(Convert( Datetime, @FechaNacimiento, 120 ) )

	-- Se obtiene el Año, Mes y Dia actual.
	Set @iAñoActual = Year(Convert( Datetime, @FechaActual, 120 ) )
	Set @iMesActual = Month(Convert( Datetime, @FechaActual, 120 ) )
	Set @iDiaActual = Day(Convert( Datetime, @FechaActual, 120 ) )

	-- Se obtiene el numero de años
	Set @iAños = @iAñoActual - @iAñoNacimiento

	If (@iMesActual - @iMesNacimiento) < 0
	  Begin
		If @iAñoNacimiento < @iAñoActual
		  Begin
		   Set @iAños = @iAños-1 
		  End
	  End

	If @iMesActual = @iMesNacimiento
	  Begin
	   If @iDiaNacimiento > @iDiaActual
		Begin
		 set @iAños = @iAños-1 
		End
	  End
	       
	Return @iAños 
End 
Go--#SQL	
 
	