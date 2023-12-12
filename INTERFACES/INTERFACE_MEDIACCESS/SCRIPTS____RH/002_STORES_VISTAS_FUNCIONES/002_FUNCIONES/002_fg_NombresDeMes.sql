

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_NombresDeMes' and xType = 'FN' ) 
   Drop Function fg_NombresDeMes 
Go--#SQL 
      
Create Function dbo.fg_NombresDeMes(@Fecha datetime)       
Returns varchar(30)
With Encryption 
As 
Begin 
Declare @iMes int, 
        @sNombreMes varchar(30) 
        
    Set @iMes = datepart(mm, @Fecha)     
    Set @sNombreMes = ''
    
    Select @sNombreMes = case 
                            when @iMes = 1 then 'Enero' 
                            when @iMes = 2 then 'Febrero' 
                            when @iMes = 3 then 'Marzo' 
                            when @iMes = 4 then 'Abril' 
                            when @iMes = 5 then 'Mayo' 
                            when @iMes = 6 then 'Junio' 
                            when @iMes = 7 then 'Julio' 
                            when @iMes = 8 then 'Agosto' 
                            when @iMes = 9 then 'Septiembre' 
                            when @iMes = 10 then 'Octubre' 
                            when @iMes = 11 then 'Noviembre' 
                            when @iMes = 12 then 'Diciembre' 
                            else '' End  
    return @sNombreMes 
          
End 
Go--#SQL 