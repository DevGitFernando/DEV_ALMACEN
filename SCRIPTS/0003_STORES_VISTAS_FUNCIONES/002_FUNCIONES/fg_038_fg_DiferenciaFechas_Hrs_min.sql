	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_DiferenciaFechas_Hrs_min' and xType = 'FN' ) 
   Drop Function fg_DiferenciaFechas_Hrs_min
Go--#SQL	
 	

Create Function dbo.fg_DiferenciaFechas_Hrs_min(@FechaInicial datetime, @FechaFinal datetime) 
returns varchar(40) 
With Encryption 
As 
Begin 
Declare @iHoras int, 
		@iMin int,
		@sFecha Varchar(40)	
		
		Set @sFecha = ''
	
	--- print cast(@iAnios as varchar) + '   ' + cast(@iMeses as varchar) + '   ' + cast(@iDias as varchar) 
	Set @iHoras = Cast((DATEDIFF(MI, @FechaInicial, @FechaFinal) / 60) As Int)
	Set @iMin = Cast((DATEDIFF(MI, @FechaInicial, @FechaFinal) - Cast((DATEDIFF(MI, @FechaInicial, @FechaFinal) / 60) As Int) * 60) As Varchar)
	
	if (@iHoras > 0)
	Begin
		 Set @sFecha = Cast (@iHoras As varchar(4)) +  + ' Hrs. '
	End
	
	Set @sFecha = @sFecha + Right('00' + Cast(@iMin As varchar(4)) , 2) +  + ' Mins' 
	
	return @sFecha 
End 
Go--#SQL	
 
	