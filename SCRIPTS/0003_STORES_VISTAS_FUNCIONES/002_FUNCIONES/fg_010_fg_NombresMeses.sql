If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_NombresMes' and xType = 'TF' )
   Drop Function fg_NombresMes 
Go--#SQL 
  
Create Function dbo.fg_NombresMes()
returns @Tabla Table 
( 
	NumeroMes int, 
	NombreMes varchar(30) 
)
With Encryption 
As 
Begin 

	Insert Into @Tabla values ( 1, 'Enero' ) 
	Insert Into @Tabla values ( 2, 'Febrero' ) 
	Insert Into @Tabla values ( 3, 'Marzo' ) 
	Insert Into @Tabla values ( 4, 'Abril' )  
	Insert Into @Tabla values ( 5, 'Mayo' )  
	Insert Into @Tabla values ( 6, 'Junio' ) 
	Insert Into @Tabla values ( 7, 'Julio' ) 
	Insert Into @Tabla values ( 8, 'Agosto' )  
	Insert Into @Tabla values ( 9, 'Septiembre' )  
	Insert Into @Tabla values ( 10, 'Octubre' ) 
	Insert Into @Tabla values ( 11, 'Noviembre' ) 
	Insert Into @Tabla values ( 12, 'Diciembre' ) 		
	
	return 

End
Go--#SQL 
   
------------------------------------------------------------------------------------------------------- 
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

------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_NombresDeMesNumero' and xType = 'FN' ) 
   Drop Function fg_NombresDeMesNumero  
Go--#SQL 
      
Create Function dbo.fg_NombresDeMesNumero(@NumeroMes Int)       
Returns varchar(30)
With Encryption 
As 
Begin 
Declare @iMes int, 
        @sNombreMes varchar(30) 
        
    -- Set @iMes = datepart(mm, @Fecha)     
    -- Set @sNombreMes = ''
    
    Select @sNombreMes = case 
                            when @NumeroMes = 1 then 'Enero' 
                            when @NumeroMes = 2 then 'Febrero' 
                            when @NumeroMes = 3 then 'Marzo' 
                            when @NumeroMes = 4 then 'Abril' 
                            when @NumeroMes = 5 then 'Mayo' 
                            when @NumeroMes = 6 then 'Junio' 
                            when @NumeroMes = 7 then 'Julio' 
                            when @NumeroMes = 8 then 'Agosto' 
                            when @NumeroMes = 9 then 'Septiembre' 
                            when @NumeroMes = 10 then 'Octubre' 
                            when @NumeroMes = 11 then 'Noviembre' 
                            when @NumeroMes = 12 then 'Diciembre' 
                            else '' End  
    return @sNombreMes 
          
End 
Go--#SQL 

------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects Where Name = 'fg_NumeroDiasFecha' and xtype = 'FN')
    Drop Function fg_NumeroDiasFecha
Go--#SQL 

Create Function dbo.fg_NumeroDiasFecha ( @Fecha datetime )
-- Create Function dbo.fg_NumeroDiasAñoMes ( @Fecha datetime )
Returns int
As
Begin 
Declare 
		@iYear int, @iMes int, @iDiaMax int   

	Set @iYear = datepart(yy, @Fecha)  
	Set @iMes = datepart(mm, @Fecha)  
	
	Select @iDiaMax = dbo.fg_NumeroDiasAñoMes ( @iYear, @iMes )
	
    return @iDiaMax
    	
End
Go--#SQL 


------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects Where Name = 'fg_NumeroDiasAñoMes' and xtype = 'FN')
    Drop Function fg_NumeroDiasAñoMes
Go--#SQL 

---		select dbo.fg_NumeroDiasAñoMes(2011,2)  

Create Function dbo.fg_NumeroDiasAñoMes ( @iYear int, @iMes int )
-- Create Function dbo.fg_NumeroDiasAñoMes ( @Fecha datetime )
Returns int
As
Begin 

Declare 
		-- @iYear int, @iMes int,  
		@iDiaComparar int, 
        @iDiaMax int,
        @iYearBis numeric(8,2),
        @sYear varchar(10)

----	Set @iYear = datepart(yy, @Fecha)  
----	Set @iMes = datepart(yy, @Fecha)  
	
    Set @iDiaComparar = 31
    Set @iDiaMax = 31
    Set @iYearBis = 0

    Select @iDiaMax = case when @iMes in ( 1, 3, 5, 7, 8, 10, 12 ) then 31 else 30 end

    If @iMes = 2
    Begin         
        Set @iDiaMax = 28     
        If @iYear % 4 = 0 
            Set @iDiaMax = 29 
    End

--    If @iDiaComparar > @iDiaMax 
--        Set @iDiaComparar = @iDiaMax 

    return @iDiaMax
End
Go--#SQL 



---------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_Mes' and xType = 'TF' )
   Drop Function fg_Mes  
Go--#SQL 
  
Create Function dbo.fg_Mes(@Year int = 2012, @Mes int = 1)
returns @Tabla Table 
( 
	Año int, 
	Mes int, 
	Dia int 
)
With Encryption 
As 
Begin 
Declare 
	@iDiasMes int, 
	@iDia int  
	
	
	Select @iDiasMes =  dbo.fg_NumeroDiasAñoMes(@Year, @Mes ) 
	Set @iDia = 1 
	
	while ( @iDia <= @iDiasMes ) 
	Begin 
		Insert Into @Tabla values ( @Year, @Mes, @iDia ) 
		Set @iDia = @iDia + 1
	End 
		
	return 

End
Go--#SQL 


