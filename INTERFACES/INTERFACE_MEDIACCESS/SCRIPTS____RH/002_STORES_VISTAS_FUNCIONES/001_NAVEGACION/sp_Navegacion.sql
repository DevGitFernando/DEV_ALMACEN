If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Navegacion' and xType = 'P' )
   Drop proc sp_Navegacion 
Go--#SQL  

Create Proc sp_Navegacion ( @sArbol varchar(4), @iMostrarResultado int = 1 )
With Encryption 
As 
Begin 
	Set dateformat YMD 

Declare @iRaiz int, 
		@sRaiz varchar(3) 

	Select @iRaiz = Rama From Net_Navegacion (NoLock) Where Arbol = @sArbol and Padre = -1 
	Set @sRaiz = cast(@iRaiz as varchar) 
	-- Select @iRaiz as Raiz_01

	If Exists ( Select Name From Sysobjects Where Name = 'tmpNavegacion' and xType = 'U' )
       Drop Table tmpNavegacion


	Create Table tmpNavegacion
	(
		Arbol varchar(4) Not Null,
		Rama int Not Null,
		Nombre varchar(255) Not Null,
		Padre int Not Null,
		FormaLoad varchar(100) Null,
		GrupoOpciones varchar(100),
		IdOrden int Not Null,  
		TipoRama varchar(20) null default '', 
		RutaCompleta varchar(50) null default '', 
		Status varchar(1) null default 'C', 
		keyx int identity(1,1)
    )

	--- Insertar la rama del arbol 
	Insert Into tmpNavegacion 
	Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, '', @sRaiz, Status  
	From Net_Navegacion (NoLock) Where Arbol = @sArbol and Padre = -1 

	Exec sp_NavegacionDetalles @sArbol, @iRaiz, @sRaiz, @@NESTLEVEL 


	--- Actualizar la ruta completa 
	Update tmpNavegacion Set RutaCompleta = Replace(RutaCompleta, '||', '|')

	--- Determinar el tipo de rama 
	Update tmpNavegacion Set TipoRama = '1' Where Keyx = 1  -- Raiz  
	Update tmpNavegacion Set TipoRama = '2' Where Keyx > 1  -- Nodos
	

    Update T Set TipoRama = '3' -- Terminales 
	From tmpNavegacion T (NoLock) 
	Where Keyx > 1 and len(FormaLoad) > 0 and 
		Not Exists ( Select Padre From tmpNavegacion R (NoLock) Where T.Rama = R.Padre )


	-- Actualizar la ruta de navegacion 
	Update N Set RutaCompleta = T.RutaCompleta
	From Net_Navegacion N (nolock) 
	Inner Join tmpNavegacion T (nolock) On ( N.Arbol = T.Arbol and N.Rama = T.Rama )


	If @iMostrarResultado = 1 
	   Begin 
		  Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, Status, Keyx 
		  Into #tmpSalida 
		  From tmpNavegacion (NoLock) 
		  Order by TipoRama, Keyx 		
	   
		  Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, Status, Keyx 
		  From #tmpSalida 
		  Order by TipoRama, Keyx 
	   End 

	-- Borrar la tabla 
	If Exists ( Select Name From Sysobjects Where Name = 'tmpNavegacion' and xType = 'U' )
       Drop Table tmpNavegacion
       
	   
End 
Go--#SQL  

