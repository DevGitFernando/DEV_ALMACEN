
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_CalcularAntiguedad_Personal' and xType = 'FN' ) 
   Drop Function fg_CalcularAntiguedad_Personal 
Go--#SQL

Create Function dbo.fg_CalcularAntiguedad_Personal(@sFechaIngreso varchar(10), @sFechaEgreso varchar(10) ) 
returns varchar(50) 
With Encryption 
As 
Begin 
	Declare 
		@sAntiguedad varchar(50),
		@iA�os int,
		@iMeses int,
		@iDias int,
		@FechaIngreso DateTime,
		@FechaEgreso DateTime

	Set @sAntiguedad = ''
	Set @iA�os = 0
	Set @iMeses = 0
	Set @iDias = 0
	Set @FechaIngreso = Convert( DateTime, @sFechaIngreso, 120 )
	Set @FechaEgreso = Convert( DateTime, @sFechaEgreso, 120 )

	-- Se obtienen los A�os.
	Select @iA�os = DateDiff(Year, @FechaIngreso, @FechaEgreso) - Case When Month(@FechaEgreso) < Month(@FechaIngreso) or
				(Month(@FechaEgreso) = Month(@FechaIngreso) and Day(@FechaEgreso) < Day(@FechaIngreso)) Then 1 Else 0 End

	-- Se obtienen los Meses.
	Select @iMeses = DateDiff(Month ,DateAdd(Year, @iA�os, @FechaIngreso), @FechaEgreso) - Case When Day(@FechaEgreso) <
				Day(DateAdd(Year, @iA�os, @FechaIngreso)) Then 1 Else 0 End

	-- Se obtienen los Dias.
	Select @iDias = DateDiff(Day, DateAdd(Month, @iMeses, DateAdd(Year, @iA�os, @FechaIngreso)), @FechaEgreso)

	-- Se crea el mensaje de antiguedad.
	Set @sAntiguedad = LTrim(@iA�os) + ' A�o(s), ' + LTrim(@iMeses) + ' Mes(es), ' + LTrim(@iDias) + '	Dia(s)'

return @sAntiguedad
End 
Go--#SQL	
 
-- Select dbo.fg_CalcularAntiguedad_Personal('2011-12-31', '2012-11-27')
 
-- Select dbo.fg_CalcularAntiguedad_Personal('2008-12-15', '2009-12-16') 

-- Select dbo.fg_CalcularAntiguedad_Personal('2009-07-01', '2012-11-27') -- 3 a�os 4 meses 26 dias

-- Select dbo.fg_CalcularAntiguedad_Personal('2011-11-28', '2012-11-27') --  

-- Select dbo.fg_CalcularAntiguedad_Personal('2012-12-31', '2013-01-01') -- 

