If Exists ( Select Name From Sysobjects (nolock) Where Name = 'spp_Net_Navegacion_Mtto' and xType = 'P' )
	Drop Proc spp_Net_Navegacion_Mtto 
Go--#SQL
/* 

Exec spp_Net_Navegacion_Mtto  @Arbol = 'OCEN', @Rama = '0', @Nombre = 'Catalogos generales', 
	@Padre = '1', @FormaLoad = '', @GrupoOpciones = '', @IdOrden = '1', @RutaCompleta = ''

*/ 

Create Proc spp_Net_Navegacion_Mtto (
    @Arbol varchar(5), @Rama int, @Nombre varchar(257), @Padre int, @FormaLoad varchar(102), 
    @GrupoOpciones varchar(102), @IdOrden int, @RutaCompleta varchar(102), @Status varchar(2) )
With Encryption 
As
Begin
Begin try 

		If @Rama = 0 
		   Select top 1 @Rama = max(Rama) + 1 From Net_Navegacion (NoLock) Where Arbol = @Arbol 

        If Not Exists ( Select top 1 Arbol From Net_Navegacion (NoLock) Where Arbol = @Arbol and Padre = -1 ) 
	       Begin 
	          Insert Into Net_Navegacion ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
		      Select @Arbol, 1, ( Select Nombre From Net_Arboles (NoLock) Where Arbol = @Arbol ), -1, '', '', 1, '1' 

			  Select top 1 @Rama = max(Rama) + 1 From Net_Navegacion (NoLock) Where Arbol = @Arbol
		   End 

        If Not Exists ( Select * From Net_Navegacion (NoLock) 
           Where 
               Arbol = @Arbol And 
               Rama = @Rama  )
        Begin
            Insert Into Net_Navegacion ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta )
            Values ( @Arbol, @Rama, @Nombre, @Padre, @FormaLoad, @GrupoOpciones, @IdOrden, @RutaCompleta )
        End
        Else
        Begin
            Update Net_Navegacion Set 
                Arbol = @Arbol, 
                Rama = @Rama, 
                Nombre = @Nombre, 
                Padre = @Padre, 
                FormaLoad = @FormaLoad, 
                GrupoOpciones = @GrupoOpciones, 
                IdOrden = @IdOrden, 
                RutaCompleta = @RutaCompleta, 
                Status = @Status 
           Where 
               Arbol = @Arbol And 
               Rama = @Rama 
            
        End
 
	-- Actualizar las rutas de navegacion     
    Exec sp_Navegacion @Arbol, 0  

	Select @Rama as Rama 


End try
Begin catch
	SELECT 
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() as ErrorState,
        ERROR_PROCEDURE() as ErrorProcedure,
        ERROR_LINE() as ErrorLine,
        ERROR_MESSAGE() as ErrorMessage;

   --RaisError ('Error al actualizar en la tabla Net_Navegacion', 16,10 )
End catch
End
Go--#SQL 
