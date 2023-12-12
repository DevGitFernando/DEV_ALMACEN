


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_NombresDeDia' and xType = 'FN' ) 
   Drop Function fg_NombresDeDia 
Go--#SQL 
      
Create Function dbo.fg_NombresDeDia(@Fecha datetime)       
Returns varchar(30)
With Encryption 
As 
Begin 
Declare @iDia int, 
        @sNombreDia varchar(30) 
        
    Set @iDia = datepart(DW, @Fecha)     
    Set @sNombreDia = ''
    
    Select @sNombreDia = case 
                            when @iDia = 1 then 'Lunes' 
                            when @iDia = 2 then 'Martes' 
                            when @iDia = 3 then 'Miercoles' 
                            when @iDia = 4 then 'Jueves' 
                            when @iDia = 5 then 'Viernes' 
                            when @iDia = 6 then 'Sabado' 
                            when @iDia = 7 then 'Domingo'                             
                            else '' End       
                    
                   
                    
    return @sNombreDia 
          
End 
Go--#SQL 